using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EJ.Domain.Services.DbContextScopeFactory;
using EJ.Entities;
using EJ.Entities.Models;
using EJ.Models.Enums;
using EJ.Models.Interfaces;
using EJ.Models.UI;
using Microsoft.AspNetCore.Mvc;
using SHARED.Common.Extensions;

namespace EJ.Domain.Services
{
    public interface ISheduleService
    {
        ObjectResult LoadSheduleFromFile(Stream fileStream);

        ObjectResult SynchronizeSheduleAndSemester(DateTime startSemesterDate, int weekNumber);

        SheduleDateViewModel GetSheduleForUser(int userId, DateTime date);

        AbsenceViewModel GetAbsence(LessonViewModel lesson);

        Task<ObjectResult> SaveAbsence(List<bool> isPresent, int calendarSheduleTimeSpendingId, string baseUrl);

        SheduleDateViewModel GetAbsencesOnPeriod(DateTime startDate, DateTime endDate);

        DateTime GetSemesterDate();
    }

    public class SheduleService : ISheduleService
    {
        protected readonly IEMailService _eMailService;
        protected readonly IUserService _userService;
        protected readonly IConverterService _converterService;
        protected readonly IMapper Mapper;
        private readonly EJContext _eJContext;

        public SheduleService(IMapper mapper,
            IDbContextFactory contextFactory,
            IEMailService eMailService,
            IConverterService converterService, IUserService userService)
        {
            _eJContext = contextFactory.CreateReadonlyDbContext<EJContext>();
            _eMailService = eMailService;
            _converterService = converterService;
            _userService = userService;
            Mapper = mapper;
        }

        public ObjectResult LoadSheduleFromFile(Stream fileStream)
        {
            return _converterService.ParseSheduleFromFile(fileStream);
        }

        public ObjectResult SynchronizeSheduleAndSemester(DateTime startSemesterDate, int weekNumber)
        {
            var semester = _eJContext.Semesters.FirstOrDefault(x => x.StartDate == startSemesterDate);
            var culture = new System.Globalization.CultureInfo("ru-RU");

            if (semester != null)
            {
                var currentDate = startSemesterDate;
                while (currentDate != semester.EndDate.AddDays(1))
                {
                    var day = culture.DateTimeFormat.GetDayName(currentDate.DayOfWeek);
                    if (currentDate.DayOfWeek == 0)
                    {
                        weekNumber = weekNumber == 1 ? 2 : 1;
                        currentDate = currentDate.AddDays(1);
                    }
                    else
                    {
                        var weekDay = _eJContext.WeekDays.FirstOrDefault(x => x.NumberOfWeek == weekNumber && x.Day.ToLower() == day.ToLower());
                        if (weekDay != null)
                        {
                            var calendar = _eJContext.Calendars?.FirstOrDefault(x => x.Date == currentDate) ??
                                _eJContext.Calendars.Add(new Calendar { Date = currentDate }).Entity;
                            var sheduleTimeSpendings = _eJContext.SheduleTimeSpendings.Where(x => x.WeekDayId == weekDay.Id);
                            if (calendar != null && sheduleTimeSpendings.Count() != 0)
                            {
                                foreach (var sheduleTimeSpending in sheduleTimeSpendings)
                                {
                                    if (!_eJContext.CalendarSheduleTimeSpendings.Any(x => x.SheduleTimeSpendingId == sheduleTimeSpending.Id
                                        && x.CalendarId == calendar.Id))
                                    {
                                        _eJContext.CalendarSheduleTimeSpendings.Add(new CalendarSheduleTimeSpending
                                        {
                                            CalendarId = calendar.Id,
                                            SheduleTimeSpendingId = sheduleTimeSpending.Id
                                        });
                                    }
                                }
                            }
                            currentDate = currentDate.AddDays(1);
                        }
                    }
                }
                _eJContext.SaveChanges();
            }
            return new OkObjectResult(true);
        }

        public SheduleDateViewModel GetSheduleForUser(int userId, DateTime date)
        {
            var userGroup = _eJContext.Users.Find(userId)?.Group;
            if (date.Date == DateTime.Now.Date)
            {
                date = DateTime.Now;
            }
            if (userGroup != null)
            {
                var lessonsFromRepository = _eJContext.CalendarSheduleTimeSpendings
                    .Where(x => x.Calendar.Date.Date == date.Date
                        && x.SheduleTimeSpending.SheduleSubject.GroupShedule.GroupId == userGroup.Id);
                var absences = _eJContext.Absences.Where(x =>
                    x.CalendarSheduleTimeSpending.Calendar.Date.Date == date.Date
                    && x.UserId == _userService.CurrentUserId);
                var lessons = lessonsFromRepository.Select(x => new LessonViewModel
                {
                    CalendarSheduleTimeSpendingId = x.Id,
                    Auditorium = x.SheduleTimeSpending.Auditorium.Number,
                    ClassType = (ClassTypeEnum)x.SheduleTimeSpending.ClassTypeId,
                    Number = x.SheduleTimeSpending.TimeSpending.Number,
                    StartTime = x.SheduleTimeSpending.TimeSpending.StartTime,
                    EndTime = x.SheduleTimeSpending.TimeSpending.EndTime,
                    WeekNumber = x.SheduleTimeSpending.WeekDay.NumberOfWeek,
                    Subject = x.SheduleTimeSpending.SheduleSubject.Subject.Name
                       ?? x.SheduleTimeSpending.SheduleSubject.Subject.ShortName,
                    WasAbsence = ((DateTime.Now.Date > x.Calendar.Date) || (date.Date == x.Calendar.Date && date.TimeOfDay > x.SheduleTimeSpending.TimeSpending.EndTime)) ? (absences.Count(y => y.CalendarSheduleTimeSpendingId == x.Id) != 0
                        ? "отсутствовал" : "присутствовал") : "",
                    StartDate = null,
                    EndDate = null,
                    CourseId = userGroup.CourseId,
                    Group = userGroup.Number
                });
                userGroup.EndDate = DateTime.Now;
                return new SheduleDateViewModel
                {
                    Date = date,
                    Group = Mapper.Map<GroupViewModel>(userGroup),
                    Lessons = lessons
                };
            }
            else
            {
                return new SheduleDateViewModel();
            }
        }

        public AbsenceViewModel GetAbsence(LessonViewModel lesson)
        {
            var users = _userService.GetCurrentUserAllGroup();
            var userGroup = _userService.GetCurrentUserGroup();

            var calendarSheduleTimeSpending = _eJContext.CalendarSheduleTimeSpendings.FirstOrDefault(x =>
                x.Id == lesson.CalendarSheduleTimeSpendingId);
            var absenceAllGroup = new List<Absence>();
            if (calendarSheduleTimeSpending != null)
            {
                absenceAllGroup.AddRange(_eJContext.Absences.Where(x =>
                    x.CalendarSheduleTimeSpending.SheduleTimeSpending.TimeSpending.Number ==
                        calendarSheduleTimeSpending.SheduleTimeSpending.TimeSpending.Number
                    && x.CalendarSheduleTimeSpending.Calendar.Date ==
                        calendarSheduleTimeSpending.Calendar.Date
                    && x.CalendarSheduleTimeSpending.SheduleTimeSpending.SheduleSubject.GroupShedule.Group.Number ==
                        userGroup.Number
                    && x.CalendarSheduleTimeSpending.SheduleTimeSpending.SheduleSubject.GroupShedule.Group.CourseId ==
                        userGroup.CourseId));
            }

            var absence = new AbsenceViewModel
            {
                Users = users.Select(x => Mapper.Map<UserInfoViewModel>(x)).ToList(),
                Lesson = lesson,
                CalendarSheduleTimeSpendingId = lesson.CalendarSheduleTimeSpendingId
            };
            absence.IsPresent = new List<bool>(absence.Users.Count);
            for (int i = 0; i < absence.Users.Count(); i++)
            {
                absence.IsPresent.Add(absenceAllGroup.Count(x => x.UserId == absence.Users[i].Id) != 0);
            }
            return absence;
        }

        public async Task<ObjectResult> SaveAbsence(List<bool> isPresent, int calendarSheduleTimeSpendingId, string baseUrl)
        {
            var userGroup = _userService.GetCurrentUserGroup();
            var calendarSheduleTimeSpending = _eJContext.CalendarSheduleTimeSpendings.Find(calendarSheduleTimeSpendingId);
            var usersInGroup = _userService.GetCurrentUserAllGroup()
                .Select(x => new { x.Id, x.Email, x.GroupId }).ToList();
            var calendarSheduleTimeSpendings = _eJContext.CalendarSheduleTimeSpendings.Where(x =>
                 x.SheduleTimeSpending.TimeSpending.Number ==
                    calendarSheduleTimeSpending.SheduleTimeSpending.TimeSpending.Number
                 && x.Calendar.Date ==
                    calendarSheduleTimeSpending.Calendar.Date
                 && x.SheduleTimeSpending.SheduleSubject.GroupShedule.Group.Number ==
                    userGroup.Number
                 && x.SheduleTimeSpending.SheduleSubject.GroupShedule.Group.CourseId ==
                    userGroup.CourseId);

            var absenceAllGroup = _eJContext.Absences.Where(x => calendarSheduleTimeSpendings.Select(y => y.Id)
                .Contains(x.CalendarSheduleTimeSpendingId)).ToList();

            if (absenceAllGroup.Count != 0)
            {
                for (int i = 0; i < absenceAllGroup.Count(); i++)
                {
                    var indexOfUser = usersInGroup.Select(x => x.Id).ToList().IndexOf(absenceAllGroup[i].User.Id);
                    if (indexOfUser != -1)
                    {
                        var currentCalendarSheduleTimeSpending = calendarSheduleTimeSpendings.FirstOrDefault(x =>
                            x.SheduleTimeSpending.SheduleSubject.GroupShedule.GroupId == usersInGroup[indexOfUser].GroupId);
                        var classType = currentCalendarSheduleTimeSpending.SheduleTimeSpending.ClassType.Name;
                        var subjectName = currentCalendarSheduleTimeSpending.SheduleTimeSpending.SheduleSubject.Subject.Name
                            ?? currentCalendarSheduleTimeSpending.SheduleTimeSpending.SheduleSubject.Subject.ShortName;
                        if (!isPresent[indexOfUser])
                        {
                            var eMailSendingResult = await _eMailService.SendAsync(absenceAllGroup[i].User.Email,
                            $"Уведомление о пропуске занятия",
                            $"<a href='{baseUrl}'><h3>Web-приложение \"Электронный журнал\"</h3></a><br/>" +
                            $"Произошла ошибка - была неправильная отметка о Вашем пропуске занятия.<br />" +
                            $"Эта ошибка была исправлена. " +
                            $"Вы присутствовали на занятии {calendarSheduleTimeSpending.Calendar.Date.ToShortDateString()}<br />" +
                            $"{classType}<br/>{subjectName}" +
                            $"<h4>Автор - Чаевский Андрей</h4>");

                            if (eMailSendingResult)
                            {
                                _eJContext.Absences.Remove(absenceAllGroup[i]);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < usersInGroup.Count(); i++)
            {
                if (isPresent[i] && absenceAllGroup.Select(x => x.UserId).ToList()
                    .IndexOf(usersInGroup[i].Id) == -1)
                {
                    var currentCalendarSheduleTimeSpending = calendarSheduleTimeSpendings.FirstOrDefault(x =>
                            x.SheduleTimeSpending.SheduleSubject.GroupShedule.GroupId == usersInGroup[i].GroupId);
                    var classType = currentCalendarSheduleTimeSpending.SheduleTimeSpending.ClassType.Name;
                    var subjectName = currentCalendarSheduleTimeSpending.SheduleTimeSpending.SheduleSubject.Subject.Name
                        ?? currentCalendarSheduleTimeSpending.SheduleTimeSpending.SheduleSubject.Subject.ShortName;

                    var eMailSendingResult = await _eMailService.SendAsync(usersInGroup[i].Email,
                        $"Уведомление о пропуске занятия",
                        $"<a href='{baseUrl}'><h3>Web-приложение \"Электронный журнал\"</h3></a><br/>" +
                        $@"Вы отсутствовали на занятии {calendarSheduleTimeSpending.Calendar.Date
                            .ToShortDateString()}<br />" +
                        $"{classType}<br/>{subjectName}" +
                        $"<h4>Автор - Чаевский Андрей</h4>");

                    if (eMailSendingResult)
                    {
                        var absenceInRepository = _eJContext.Absences.Add(new Absence
                        {
                            UserId = usersInGroup[i].Id,
                            CalendarSheduleTimeSpendingId = currentCalendarSheduleTimeSpending.Id
                        });
                        _eJContext.AbsenceNotifications.Add(new AbsenceNotification
                        {
                            AbsenceId = absenceInRepository.Entity.Id,
                            SendDate = DateTime.Now
                        });
                    }
                }
            }
            _eJContext.SaveChanges();

            return await Task.Run(() =>
            {
                return new OkObjectResult(true);
            });
        }

        public SheduleDateViewModel GetAbsencesOnPeriod(DateTime startDate, DateTime endDate)
        {
            var userGroup = _eJContext.Users.Find(_userService.CurrentUserId)?.Group;
            if (userGroup != null)
            {
                var lessonsFromRepository = _eJContext.CalendarSheduleTimeSpendings.Where(x =>
                    x.Calendar.Date.Date >= startDate.Date && x.Calendar.Date.Date <= endDate.Date
                    && x.SheduleTimeSpending.SheduleSubject.GroupShedule.GroupId == userGroup.Id);
                var absences = _eJContext.Absences.Where(x =>
                    x.CalendarSheduleTimeSpending.Calendar.Date.Date >= startDate.Date
                    && x.CalendarSheduleTimeSpending.Calendar.Date.Date <= endDate.Date
                    && x.UserId == _userService.CurrentUserId);
                var lessons = lessonsFromRepository.Join(absences, lfr => lfr.Id, a => a.CalendarSheduleTimeSpendingId,
                (lesson, absence) => new
                {
                    CalendarSheduleTimeSpendingId = lesson.Id,
                    Auditorium = lesson.SheduleTimeSpending.Auditorium.Number,
                    ClassType = (ClassTypeEnum)lesson.SheduleTimeSpending.ClassTypeId,
                    lesson.SheduleTimeSpending.TimeSpending.Number,
                    lesson.SheduleTimeSpending.TimeSpending.StartTime,
                    lesson.SheduleTimeSpending.TimeSpending.EndTime,
                    WeekNumber = lesson.SheduleTimeSpending.WeekDay.NumberOfWeek,
                    Subject = lesson.SheduleTimeSpending.SheduleSubject.Subject.Name
                       ?? lesson.SheduleTimeSpending.SheduleSubject.Subject.ShortName,
                    WasAbsence = "отсутствовал"
                })
                .Select(x => new LessonViewModel
                {
                    CalendarSheduleTimeSpendingId = x.CalendarSheduleTimeSpendingId,
                    Auditorium = x.Auditorium,
                    ClassType = x.ClassType,
                    Number = x.Number,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    WeekNumber = x.WeekNumber,
                    Subject = x.Subject,
                    WasAbsence = x.WasAbsence,
                    StartDate = startDate,
                    EndDate = endDate
                });
                return new SheduleDateViewModel
                {
                    Date = endDate,
                    Group = Mapper.Map<GroupViewModel>(userGroup),
                    Lessons = lessons
                };
            }
            return new SheduleDateViewModel();
        }

        public DateTime GetSemesterDate()
        {
            var semester = _eJContext.Semesters.FirstOrDefault(x => x.StartDate <= DateTime.Now
                && x.EndDate >= DateTime.Now);
            return semester?.StartDate ?? DateTime.Now;
        }
    }
}
