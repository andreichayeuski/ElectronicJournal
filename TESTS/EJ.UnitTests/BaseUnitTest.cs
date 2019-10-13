using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using EJ.Domain.Services;
using EJ.Entities;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using SHARED.Web.Caching;
using User = EJ.Entities.Models.User;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using EJ.Domain.Services.AuthorizationServices;
using EJ.Domain.Repositories;
using System;
using System.Linq;
using System.Reflection;

namespace EJ.UnitTests.Services
{
    public class BaseUnitTest
    {
        protected readonly IConfiguration Configuration;

        protected readonly IBaseUserService BaseUserService;
        protected readonly IEMailService EMailService;
        protected readonly IUserService UserService;
        protected readonly IConverterService ConverterService;
        protected readonly IGroupService GroupService;
        protected readonly IGroupService GroupService1;
        protected readonly IGroupService GroupService2;

        protected readonly ICourseService CourseService;
        protected readonly ICourseService CourseService1;
        protected readonly ICourseService CourseService2;

        protected readonly IMapper Mapper;
        protected readonly IRepository<Absence> AbsenceRepository;
        protected readonly IRepository<AbsenceNotification> AbsenceNotificationRepository;
        protected readonly IRepository<Auditorium> AuditoriumRepository;
        protected readonly IRepository<Calendar> CalendarRepository;
        protected readonly IRepository<CalendarSheduleTimeSpending> CalendarSheduleTimeSpendingRepository;
        protected readonly IRepository<TimeSpending> TimeSpendingRepository;
        protected readonly IRepository<Group> GroupRepository;
        protected readonly IRepository<GroupShedule> GroupSheduleRepository;
        protected readonly IRepository<Semester> SemesterRepository;
        protected readonly IRepository<SheduleSubject> SheduleSubjectRepository;
        protected readonly IRepository<SheduleTimeSpending> SheduleTimeSpendingRepository;
        protected readonly IRepository<Subject> SubjectRepository;
        protected readonly IRepository<WeekDay> WeekDayRepository;
        protected readonly IRepository<User> UserRepository;

        private Mock<ClaimsPrincipal> _mockClaimsPrincipal;
        protected readonly int UndefinedUserId;

        protected ServiceCollection services;

        protected virtual ServiceLifetime DbContextServiceLifetime => ServiceLifetime.Transient;
        protected virtual bool SetLifetimeAsInWeb => false;
        protected readonly ServiceProvider ServiceProvider;

        public BaseUnitTest()
        {
            Configuration = GetTestConfiguration();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new Mock<HttpContext>();
            _mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            const string fakeTenantId = "abcd";
            context.Setup(x => x.Connection.RemoteIpAddress).Returns(IPAddress.None);
            context.Setup(x => x.User).Returns(_mockClaimsPrincipal.Object);
            context.Setup(x => x.Request.Headers["Tenant-ID"]).Returns(fakeTenantId);
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context.Object);

            services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IConfiguration>(c => Configuration);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
                !assembly.FullName.StartsWith("Microsoft.VisualStudio.TraceDataCollector")));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
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
            ServiceLifetime dbContextLifeTime = DbContextServiceLifetime;

            services.AddDbContext<DbContext, EJContext>(options =>
               options
                 .UseLazyLoadingProxies()
                 .UseSqlServer(Configuration.GetConnectionString("ElectronicJournalDatabase")),
             ServiceLifetime.Transient);

            UndefinedUserId = Configuration.GetValue<int>("UndefinedUserId");

            var serviceProvider = ServiceProvider = services.BuildServiceProvider();
            
            serviceProvider.GetRequiredService<DbContext>().ChangeTracker
                .QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            EMailService = serviceProvider.GetService<IEMailService>();
            //UserRepository = serviceProvider.get<IRepository<User>>();
            BaseUserService = serviceProvider.GetService<IBaseUserService>();
            UserService = serviceProvider.GetService<IUserService>();
            GroupService = serviceProvider.GetService<IGroupService>();
            CourseService = serviceProvider.GetService<ICourseService>();
            GroupService1 = serviceProvider.GetService<IGroupService>();
            CourseService1 = serviceProvider.GetService<ICourseService>();

            GroupService2 = serviceProvider.GetService<IGroupService>();
            CourseService2 = serviceProvider.GetService<ICourseService>();

            ConverterService = serviceProvider.GetService<IConverterService>();
            Mapper = serviceProvider.GetService<IMapper>();
            AbsenceRepository = serviceProvider.GetService<IRepository<Absence>>();
            AbsenceNotificationRepository = serviceProvider.GetService<IRepository<AbsenceNotification>>();
            AuditoriumRepository = serviceProvider.GetService<IRepository<Auditorium>>();
            CalendarRepository = serviceProvider.GetService<IRepository<Calendar>>();
            CalendarSheduleTimeSpendingRepository = serviceProvider.GetService<IRepository<CalendarSheduleTimeSpending>>();
            TimeSpendingRepository = serviceProvider.GetService<IRepository<TimeSpending>>();
            GroupRepository = serviceProvider.GetService<IRepository<Group>>();
            GroupSheduleRepository = serviceProvider.GetService<IRepository<GroupShedule>>();
            SemesterRepository = serviceProvider.GetService<IRepository<Semester>>();
            SheduleSubjectRepository = serviceProvider.GetService<IRepository<SheduleSubject>>();
            SheduleTimeSpendingRepository = serviceProvider.GetService<IRepository<SheduleTimeSpending>>();
            SubjectRepository = serviceProvider.GetService<IRepository<Subject>>();
            WeekDayRepository = serviceProvider.GetService<IRepository<WeekDay>>();
        }

        protected IConfiguration GetTestConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();
        }

        
    }
}
