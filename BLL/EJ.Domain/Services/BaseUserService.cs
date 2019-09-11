using System.Linq;
using System.Security.Claims;
using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.Enums;
using EJ.Models.Interfaces;
using EJ.Models.UI;
using Microsoft.AspNetCore.Http;
using SHARED.Web.Caching;

namespace EJ.Domain.Services
{
    public interface IBaseUserService
    {
        /// <summary>
        /// Ip адрес текущего пользователя
        /// </summary>
        string CurrentUserIp { get; }

        /// <summary>
        /// Логин текущего пользователя
        /// </summary>
        string CurrentUserLogin { get; }

        /// <summary>
        /// Идентификатор текущего пользователя
        /// </summary>
        int CurrentUserId { get; }

        /// <summary>
        /// Роль текущего пользователя
        /// </summary>
        RolesEnum CurrentUserRole { get; }

        /// <summary>
        /// ФИО текущего пользователя
        /// </summary>
        string CurrentUserFio { get; }

        /// <summary>
        /// Возвращает роль и таски переданного пользователя
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>Роль и таски</returns>
        UserInfoUi GetUserBasicInfo(int userId);
        void ClearCache();
        void ClearCurrentUserCache();
    }

    public class BaseUserService : IBaseUserService
    {
        protected readonly IServiceCache Cache;
        protected readonly IRepository<User> UserRepository;
        protected readonly IMapper Mapper;
        protected readonly IHttpContextAccessor HttpContextAccessor;

        public BaseUserService(
            IServiceCache cache,
            IRepository<Entities.Models.User> userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            Cache = cache;
            UserRepository = userRepository;
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
        }

        public string CurrentUserIp => HttpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.ToString();

        public string CurrentUserLogin
        {
            get
            {
                if (HttpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
                {
                    return HttpContextAccessor.HttpContext.User.Identities.SelectMany(t => t.Claims).FirstOrDefault(t => t.Type == ClaimTypes.Email)?.Value
                           ?? HttpContextAccessor.HttpContext.User.Identity.Name;
                }
                return "NotAuthUser";
            }
        }

        public int CurrentUserId => Cache.GetOrAdd($"CurrentUserId_{CurrentUserLogin}",
            () =>
            {
                var user = UserRepository.FindFirst(c => c.Email.ToLower().StartsWith(CurrentUserLogin.ToLower()));
                return user?.Id ?? 0;
            });

        public RolesEnum CurrentUserRole => GetUserBasicInfo(CurrentUserId).Role;

        public string CurrentUserFio => GetUserBasicInfo(CurrentUserId).Fio;

        public virtual UserInfoUi GetUserBasicInfo(int userId)
        {
            return Cache.GetOrAdd($"UserBasicInfo_{userId}", () =>
            {
                var dbUser = UserRepository.Find(userId);

                if (dbUser != null)
                {
                    return new UserInfoUi
                    {
                        Email = dbUser.Email,
                        Fio = dbUser.Fio,
                        Id = dbUser.Id,
                        Role = (RolesEnum)dbUser.RoleId
                    };
                }
                
                return new UserInfoUi
                {
                    Code = "",
                    Email = "",
                    Fio = "",
                    Role = RolesEnum.UnAuthorized
                };
                //throw new EntityNotFoundException($"Пользователь с id = <{userId}> не найден!");
            });
        }
        public void ClearCache()
        {
            Cache.RemoveStartWith("CurrentUserId");
            Cache.RemoveStartWith("UserBasicInfo");
        }
        public void ClearCurrentUserCache()
        {
            Cache.RemoveStartWith($"CurrentUserId_{CurrentUserLogin}");
            Cache.RemoveStartWith($"UserBasicInfo_{CurrentUserId}");
        }
    }
}
