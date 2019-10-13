using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.Profiles
{
    public class AbsenceProfile: Profile
    {
        public AbsenceProfile()
        {
            /*CreateMap<Absence, AbsenceViewModel>()
                .ForMember(x => x.Lesson, x => x.MapFrom(e => e.UserId))
                .ForMember(x => x.CalendarSheduleTimeSpendingId, x => x.MapFrom(e => e.FIO))
                .ForMember(x => x.Roles, x => x.MapFrom(e => e.UserRoles.Select(z => (RolesEnum)z.RoleId)))
                .ForMember(x => x.Tasks, x => x.MapFrom(e => e.UserTasks.Select(z => (TasksEnum)z.TaskId)));

            CreateMap<AbsenceViewModel, Absence>()
                .ForMember(x => x.FIO, x => x.MapFrom(e => e.Fio))
                .ForMember(x => x.UserRoles, x => x.MapFrom(e => e.Roles.Select(z => new UserRole { RoleId = (int)z })))
                .ForMember(x => x.UserTasks, x => x.MapFrom(e => e.Tasks.Select(z => new UserTask { TaskId = (int)z })));*/
        }
    }
}
