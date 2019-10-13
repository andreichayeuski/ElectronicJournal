using EJ.Entities.Models;
using Microsoft.EntityFrameworkCore;
using EJ.Entities.Mappings;

namespace EJ.Entities
{
    public class EJContext : DbContext
    {
        private readonly string _connectionString;

        public EJContext() { }

        public EJContext(DbContextOptions<EJContext> options) : base(options) { }

        public EJContext(string connectionString)
        {
            _connectionString = connectionString;
        }


        public virtual DbSet<Absence> Absences { get; set; }
        public virtual DbSet<AbsenceNotification> AbsenceNotifications { get; set; }
        public virtual DbSet<Auditorium> Auditoriums { get; set; }
        public virtual DbSet<ClassType> ClassTypes { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<CalendarSheduleTimeSpending> CalendarSheduleTimeSpendings { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupShedule> GroupShedules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<SheduleSubject> SheduleSubjects { get; set; }
        public virtual DbSet<SheduleTimeSpending> SheduleTimeSpendings { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TimeSpending> TimeSpendings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserHistory> UserHistories { get; set; }
        public virtual DbSet<UserRolesHistory> UserRolesHistories { get; set; }
        public virtual DbSet<UserState> UserStates { get; set; }
        public virtual DbSet<WeekDay> WeekDays { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AbsenceMap());
            modelBuilder.ApplyConfiguration(new AbsenceNotificationMap());
            modelBuilder.ApplyConfiguration(new AuditoriumMap());
            modelBuilder.ApplyConfiguration(new CalendarMap());
            modelBuilder.ApplyConfiguration(new CalendarSheduleTimeSpendingMap());
            modelBuilder.ApplyConfiguration(new ClassTypeMap());
            modelBuilder.ApplyConfiguration(new CourseMap());
            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new GroupSheduleMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new SemesterMap());
            modelBuilder.ApplyConfiguration(new SheduleSubjectMap());
            modelBuilder.ApplyConfiguration(new SheduleTimeSpendingMap());
            modelBuilder.ApplyConfiguration(new SubjectMap());
            modelBuilder.ApplyConfiguration(new TimeSpendingMap());
            modelBuilder.ApplyConfiguration(new UserHistoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserRolesHistoryMap());
            modelBuilder.ApplyConfiguration(new UserStateMap());
            modelBuilder.ApplyConfiguration(new WeekDayMap());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EJContext).Assembly);
        }
    }
}
