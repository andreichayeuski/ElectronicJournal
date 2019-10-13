using System.Linq;
using System.Security.Claims;
using AutoMapper;
using EJ.Domain.Services.DbContextScopeFactory;
using EJ.Entities;
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
        UserInfoViewModel GetUserBasicInfo(int userId);
        void ClearCache();
        void ClearCurrentUserCache();
    }

    public class BaseUserService : IBaseUserService
    {
        protected readonly IServiceCache Cache;
        protected readonly IMapper Mapper;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        private readonly EJContext _eJContext;

        public BaseUserService(
            IServiceCache cache,
            IDbContextFactory contextFactory,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            Cache = cache;
            _eJContext = contextFactory.CreateReadonlyDbContext<EJContext>();
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
                return _eJContext.Users.FirstOrDefault(c => c.Email.ToLower().StartsWith(CurrentUserLogin.ToLower()))?.Id ?? 0;
            }); 

        public RolesEnum CurrentUserRole => GetUserBasicInfo(CurrentUserId).Role;

        public string CurrentUserFio => GetUserBasicInfo(CurrentUserId).Fio;

        public virtual UserInfoViewModel GetUserBasicInfo(int userId)
        {
            return Cache.GetOrAdd($"UserBasicInfo_{userId}", () =>
            {
                var dbUser = _eJContext.Users.Find(userId);

                if (dbUser != null)
                {
                    return new UserInfoViewModel
                    {
                        Email = dbUser.Email,
                        Fio = dbUser.Fio,
                        Id = dbUser.Id,
                        Role = (RolesEnum)dbUser.RoleId
                    };
                }
                
                return new UserInfoViewModel
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
