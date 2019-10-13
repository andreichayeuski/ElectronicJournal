using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupViewModel>()
            .ForMember(x => x.Number, x => x.MapFrom(e => e.Number))
            .ForMember(x => x.CourseId, x => x.MapFrom(e => e.CourseId))
            .ForMember(x => x.StartDate, x => x.MapFrom(e => e.StartDate))
            .ForMember(x => x.EndDate, x => x.MapFrom(e => e.EndDate))
            .ForMember(x => x.HalfGroup, x => x.MapFrom(e => e.HalfGroup))
            .ForMember(x => x.Id, x => x.MapFrom(e => e.Id));
        }
    }
}
