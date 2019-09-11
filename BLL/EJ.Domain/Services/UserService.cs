using System.Linq;
using AutoMapper;
using EJ.Domain.Services.AuthorizationServices;
using EJ.Entities.Models;
using EJ.Models.Configurations;
using EJ.Models.Enums;
using EJ.Models.Interfaces;
using EJ.Models.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SHARED.Web.Caching;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SHARED.Common.Extensions;
using System.Collections.Generic;
using System;

namespace EJ.Domain.Services
{
    public interface IUserService : IBaseUserService
    {
        Task<UserInfoUi> Login(LoginUI model);

        Task<bool> Exists(string model);

        Task<UserInfoUi> Register(RegisterUI model);

        IEnumerable<UserInfoUi> GetUsers();
        UserInfoUi GetUser(int id);
        UserInfoUi AddUser(UserInfoUi group);
        bool DeleteUser(int id);
        bool UpdateUser(int id, UserInfoUi group);
        Group GetCurrentUserGroup();
        List<User> GetCurrentUserAllGroup();
    }

    public class UserService : BaseUserService, IUserService
    {
        protected readonly IEMailService EMailService;
        protected readonly IAuthorizationService AuthorizationService;
        private readonly SaltConfiguration SaltConfiguration;
        private readonly IRepository<Group> _groupRepository;

        public UserService(
             IServiceCache cache,
             IEMailService eMailService,
             IRepository<User> userRepository,
             IRepository<Group> groupRepository,
             IMapper mapper, IHttpContextAccessor httpContext,
             IAuthorizationService authorizationService, IConfiguration configuration)
             : base(cache, userRepository, mapper, httpContext)
        {
            EMailService = eMailService;
            AuthorizationService = authorizationService;
            SaltConfiguration = configuration.GetSection("Salt").Get<SaltConfiguration>();
            _groupRepository = groupRepository;
        }

        public static Func<User, UserInfoUi> mapUserToUserInfoUi = x =>
         new UserInfoUi
         {
             Id = x.Id,
             FName = x.FName,
             MName = x.MName,
             SName = x.SName,
             Email = x.Email,
             BirthDay = x.BirthDay,
             PersonalNumber = x.PersonalNumber,
             GroupId = x.GroupId,
             UserStateId = x.UserStateId,
             StartDate = x.StartDate,
             RemovalDate = x.RemovalDate,
             Sex = x.Sex,
             RoleId = x.RoleId,
             RoleName = x.Role.Name,
             Fio = x.Fio
         };

        private byte[] GetPasswordBytes(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var newPassword = (SaltConfiguration.GlobalSalt + password).GetBytes();
            var sha1data = sha1.ComputeHash(newPassword);
            return sha1data;
        }
        public async Task<UserInfoUi> Login(LoginUI model)
        {
            var pwd = GetPasswordBytes(model.Password);
            var userFromRepository = UserRepository.FindFirst(x =>
                pwd.SequenceEqual(x.Password) && model.Email == x.Email);
            if (userFromRepository == null)
            {
                return new UserInfoUi
                {
                    ErrorMessage = "Неверные логин и (или) пароль"
                };
            }
            if (userFromRepository.EmailVerified == true)
            {
                return new UserInfoUi
                {
                    Fio = userFromRepository.Fio,
                    Role = (RolesEnum) userFromRepository.RoleId,
                    Email = userFromRepository.Email
                };
            }
            return new UserInfoUi
            {
                ErrorMessage = "Адрес электронной почты не подтверждён, пожалуйста, проверьте Ваш почтовый ящик."
            };
        }

        public async Task<bool> Exists(string model)
        {
            return await UserRepository.FindFirstAsync(x => model == x.Email) == null;
        }

        public async Task<UserInfoUi> Register(RegisterUI model)
        {
            var userFromRepository = await UserRepository.AddAsync(new User()
            {
                Email = model.Email,
                Password = GetPasswordBytes(model.Password),
                RoleId = (int) RolesEnum.Студент,
                BirthDay = model.Birthday,
                FName = model.FName,
                SName = model.SName,
                MName = model.MName,
                PersonalNumber = model.PersonalNumber,
                StartDate = model.StartDate,
                Sex = model.Sex,
                UserStateId = (int) UserStatesEnum.Studying,
                GroupId = model.GroupId
            });
            if (userFromRepository == null)
            {
                return null;
            }

            var code = await AuthorizationService.GenerateEmailConfirmationTokenAsync(userFromRepository);

            return new UserInfoUi
            {
                Fio = userFromRepository.Fio,
                Role = (RolesEnum) userFromRepository.RoleId,
                Email = userFromRepository.Email,
                Id = userFromRepository.Id,
                Code = code
            };
        }

        //public RolesEnum CurrentUserRole { get; }
        public string CurrentUserFio { get; }
        public UserInfoUi GetUserBasicInfo(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Group GetCurrentUserGroup()
        {
            return UserRepository.FindFirst(x => x.Id == CurrentUserId).Group;
        }
        public List<User> GetCurrentUserAllGroup()
        {
            var group = UserRepository.FindFirst(x => x.Id == CurrentUserId).Group;
            var users = new List<User>();
            foreach (var item in _groupRepository.Find(x => x.Number == group.Number && x.CourseId == group.CourseId))
            {
                users.AddRange(item.Users);
            }
            return users;
        }

        public IEnumerable<UserInfoUi> GetUsers()
        {
            return (UserRepository.GetAll()
                .OrderBy(y => y.Fio)
                .Select(mapUserToUserInfoUi));
        }

        public UserInfoUi GetUser(int id)
        {
            var user = UserRepository.Find(id);
            if (user != null)
            {
                return Mapper.Map<UserInfoUi>(user);
            }
            else
            {
                return new UserInfoUi();
            }
        }

        public UserInfoUi AddUser(UserInfoUi user)
        {
            return null;
        }

        public bool DeleteUser(int id)
        {
            var user = UserRepository.Find(id);
            if (user != null)
            {
                UserRepository.Remove(user);
                return (UserRepository.Find(id) == null);
            }
            throw new System.Exception("Group not found");
        }

        public bool UpdateUser(int id, UserInfoUi user)
        {
            foreach (var role in (RolesEnum[]) Enum.GetValues(typeof(RolesEnum)))
            {
                if (user.RoleName.ToLower() == role.ToString().ToLower())
                {
                    user.RoleId = (int) role;
                }
            }
            //var userFromRepository = UserRepository.FindFirst(x => x.Id == id);
            var userToRepository = new User
            {
                FName = user.FName,
                MName = user.MName,
                SName = user.SName,
                Email = user.Email,
                BirthDay = user.BirthDay,
                PersonalNumber = user.PersonalNumber,
                GroupId = user.GroupId,
                UserStateId = user.UserStateId,
                StartDate = user.StartDate,
                RemovalDate = user.RemovalDate,
                Sex = user.Sex,
                RoleId = user.RoleId,
                Id = id/*,
                Password = userFromRepository.Password,
                EmailVerified = userFromRepository.EmailVerified*/
            };
            UserRepository.Update(userToRepository, true, x => x.FName, x => x.MName, x => x.SName,
            x => x.Email, x => x.BirthDay, x => x.PersonalNumber, x => x.GroupId, x => x.UserStateId,
            x => x.StartDate, x => x.RemovalDate, x => x.Sex, x => x.RoleId);
            return true;
        }
    }
}
