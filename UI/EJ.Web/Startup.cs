using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using EJ.Web.Extensions;
using Microsoft.AspNetCore.Localization;
//using Swashbuckle.AspNetCore.Swagger;
//using Microsoft.AspNetCore.SpaServices.Webpack;

namespace EJ.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "electronic_journal";
            });

            /*services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddNLog(Configuration);
            });*/


            services.AddBaseHttp();
            services.AddSessionCache();
            services.AddAutoMapperProfiles();
            services.AddBaseDbContexts(Configuration);
            services.AddDbContextFactory();
            services.AddUserInfo();
            services.AddWebServices();
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Electronic Journal API",
                    Description = "JSON REST endpoints for data",
                    Contact = new Contact
                    {
                        Name = "Andrei Chayeuski",
                        Email = string.Empty,
                        Url = "https://vk.com/andreichayeuski"
                    }
                });
            });*/
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // Automatically perform database migration
            //services.BuildServiceProvider().GetService<ApplicationContext>().Database.Migrate();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    options.LoginPath = "/Account/Login";
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    options.SlidingExpiration = true;
                });

            services.AddSession(opt =>
            {
            });

            services.AddWebServices();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions()
                {
                    HotModuleReplacement = true
                });*/
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            /*app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Electronic Journal");
            });*/
            var appCulture = new CultureInfo("ru-RU");
            //var secondAppCulture = new CultureInfo("en-US");
            //appCulture.NumberFormat.CurrencyDecimalSeparator = appCulture.NumberFormat.PercentDecimalSeparator = appCulture.NumberFormat.NumberDecimalSeparator = ".";
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(appCulture),

                SupportedCultures = new List<CultureInfo>
                {
                    appCulture
                },

                SupportedUICultures = new List<CultureInfo>
                {
                    appCulture
                }
            });

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            //app.UseMiddleware<RequestLoginMiddleware>();
            //app.UseMiddleware<EJActionExceptionCatcherMiddleware>();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("defaultArea", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            /*app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "defaultArea",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

            });*/
        }
    }
}
