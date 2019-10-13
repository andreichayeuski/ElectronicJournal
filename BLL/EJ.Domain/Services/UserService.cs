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
using EJ.Entities;
using EJ.Domain.Services.DbContextScopeFactory;

namespace EJ.Domain.Services
{
    public interface IUserService : IBaseUserService
    {
        Task<UserInfoViewModel> Login(LoginViewModel model);

        Task<bool> Exists(string model);

        Task<UserInfoViewModel> Register(RegisterViewModel model);

        IEnumerable<UserInfoViewModel> GetUsers();
        UserInfoViewModel GetUser(int id);
        UserInfoViewModel AddUser(UserInfoViewModel group);
        bool DeleteUser(int id);
        bool UpdateUser(int id, UserInfoViewModel group);
        Group GetCurrentUserGroup();
        List<User> GetCurrentUserAllGroup();
    }

    public class UserService : BaseUserService, IUserService
    {
        protected readonly IEMailService EMailService;
        protected readonly IAuthorizationService AuthorizationService;
        private readonly SaltConfiguration SaltConfiguration;
        private readonly EJContext _eJContext;

        public UserService(
             IServiceCache cache,
             IEMailService eMailService,
             IDbContextFactory contextFactory,
             IMapper mapper, IHttpContextAccessor httpContext,
             IAuthorizationService authorizationService, IConfiguration configuration)
             : base(cache, contextFactory, mapper, httpContext)
        {
            EMailService = eMailService;
            AuthorizationService = authorizationService;
            SaltConfiguration = configuration.GetSection("Salt").Get<SaltConfiguration>();
            _eJContext = contextFactory.CreateReadonlyDbContext<EJContext>();
        }

        private byte[] GetPasswordBytes(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var newPassword = (SaltConfiguration.GlobalSalt + password).GetBytes();
            var sha1data = sha1.ComputeHash(newPassword);
            return sha1data;
        }
        public async Task<UserInfoViewModel> Login(LoginViewModel model)
        {
            var pwd = GetPasswordBytes(model.Password);
            var userFromRepository = _eJContext.Users.ToList().FirstOrDefault(x =>
                pwd.SequenceEqual(x.Password) && model.Email == x.Email);//.ToList()[0];
            if (userFromRepository == null)
            {
                return new UserInfoViewModel
                {
                    ErrorMessage = "Неверные логин и (или) пароль"
                };
            }
            if (userFromRepository.EmailVerified == true)
            {
                return new UserInfoViewModel
                {
                    Fio = userFromRepository.Fio,
                    Role = (RolesEnum) userFromRepository.RoleId,
                    Email = userFromRepository.Email
                };
            }
            return new UserInfoViewModel
            {
                ErrorMessage = "Адрес электронной почты не подтверждён, пожалуйста, проверьте Ваш почтовый ящик."
            };
        }

        public async Task<bool> Exists(string model)
        {
            return _eJContext.Users.Where(x => model == x.Email).Any();
        }

        public async Task<UserInfoViewModel> Register(RegisterViewModel model)
        {
            var userFromRepository = _eJContext.Users.Add(new User()
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

            var code = await AuthorizationService.GenerateEmailConfirmationTokenAsync(userFromRepository.Entity);
            _eJContext.SaveChanges();
            return new UserInfoViewModel
            {
                Fio = userFromRepository.Entity.Fio,
                Role = (RolesEnum) userFromRepository.Entity.RoleId,
                Email = userFromRepository.Entity.Email,
                Id = userFromRepository.Entity.Id,
                Code = code
            };
        }

        //public RolesEnum CurrentUserRole { get; }
        public string CurrentUserFio { get; }
        public UserInfoViewModel GetUserBasicInfo(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Group GetCurrentUserGroup()
        {
            return _eJContext.Users.FirstOrDefault(x => x.Id == CurrentUserId)?.Group;
        }
        public List<User> GetCurrentUserAllGroup()
        {
            var group = _eJContext.Users.FirstOrDefault(x => x.Id == CurrentUserId)?.Group;
            var users = new List<User>();
            var groups = _eJContext.Groups.Where(x => x.Number == group.Number && x.CourseId == group.CourseId).ToList();
            foreach (var item in groups)
            {
                users.AddRange(item.Users);
            }
            return users;
        }

        public IEnumerable<UserInfoViewModel> GetUsers()
        {
            return _eJContext.Users
                .OrderBy(nameof(User.SName))
                //.ThenBy(nameof(User.FName))
                //.ThenBy(nameof(User.MName))
                .Select(x => Mapper.Map<UserInfoViewModel>(x));
        }

        public UserInfoViewModel GetUser(int id)
        {
            var user = _eJContext.Users.Find(id);
            if (user != null)
            {
                return Mapper.Map<UserInfoViewModel>(user);
            }
            else
            {
                return new UserInfoViewModel();
            }
        }

        public UserInfoViewModel AddUser(UserInfoViewModel user)
        {
            return null;
        }

        public bool DeleteUser(int id)
        {
            var user = _eJContext.Users.Find(id);
            if (user != null)
            {
                _eJContext.Users.Remove(user);
                _eJContext.SaveChanges();
                return _eJContext.Users.Find(id) == null;
            }
            throw new Exception("User not found");
        }

        public bool UpdateUser(int id, UserInfoViewModel user)
        {
            foreach (var role in (RolesEnum[]) Enum.GetValues(typeof(RolesEnum)))
            {
                if (user.RoleName.ToLower() == role.ToString().ToLower())
                {
                    user.RoleId = (int) role;
                }
            }
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
            _eJContext.Users.Update(userToRepository);/*, true, x => x.FName, x => x.MName, x => x.SName,
            x => x.Email, x => x.BirthDay, x => x.PersonalNumber, x => x.GroupId, x => x.UserStateId,
            x => x.StartDate, x => x.RemovalDate, x => x.Sex, x => x.RoleId);*/
            _eJContext.SaveChanges();
            return true;
        }
    }
}
