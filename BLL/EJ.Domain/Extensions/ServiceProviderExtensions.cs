using AutoMapper;
using EJ.Domain.Services;
using EJ.Domain.Services.AuthorizationServices;
using EJ.Domain.Services.DbContextScopeFactory;
using EJ.Entities;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SHARED.Web.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EJ.Web.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddBaseHttp(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public static void AddSessionCache(this IServiceCollection services)
        {
            services.AddScoped<ISessionCache, SessionCache>();
        }

        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddSingleton<IServiceCache, ServiceCache>();
            services.AddScoped<ISessionCache, SessionCache>();
            services.AddScoped<IBaseUserService, BaseUserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEMailService, EMailService>();
            services.AddScoped<IUserTwoFactorTokenProvider<User>, UserTwoFactorTokenProvider>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IConverterService, ConverterService>();
            services.AddScoped<ISheduleService, SheduleService>();

            services.AddScoped(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            var assemblies = new List<Assembly>();
            var ass = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var assem = Assembly.Load("EJ.Domain");
            ass.Add(assem);
            var assem1 = Assembly.Load("EJ.Models");
            ass.Add(assem1);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.DefinedTypes.Any(x => typeof(Profile).GetTypeInfo().IsAssignableFrom(x)))
                {
                    assemblies.Add(assembly);
                }
            }

            services.AddAutoMapper(assemblies);
        }

        public static void AddBaseDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EJContext, EJContext>(options =>
                    options
                      .UseLazyLoadingProxies()
                      .UseSqlServer(configuration.GetConnectionString("ElectronicJournalDatabase")));
        }

        public static void AddDbContextFactory(this IServiceCollection services)
        {
            services.AddScoped<IDbContextFactory, DbContextFactory>();
        }
    }
}
