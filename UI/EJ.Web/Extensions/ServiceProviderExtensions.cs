using EJ.Domain.Repositories;
using EJ.Domain.Services;
using EJ.Domain.Services.AuthorizationServices;
using EJ.Entities;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SHARED.Web.Caching;

namespace EJ.Web.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddSingleton<IServiceCache, ServiceCache>();
            services.AddScoped<ISessionCache, SessionCache>();
            services.AddScoped<IBaseUserService, BaseUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEMailService, EMailService>();
            services.AddScoped<IUserTwoFactorTokenProvider<User>, UserTwoFactorTokenProvider>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IConverterService, ConverterService>();
            services.AddScoped<ISheduleService, SheduleService>();
        }

        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            // инжект через DbContext
            services.AddDbContext<DbContext, ElectronicJournalContext>(options =>
                options
                  .UseLazyLoadingProxies()
                  .UseSqlServer(configuration.GetConnectionString("ElectronicJournalDatabase")),
              ServiceLifetime.Transient);
        }

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        }
    }
}
