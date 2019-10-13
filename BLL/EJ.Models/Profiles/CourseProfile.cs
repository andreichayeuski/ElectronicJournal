using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseViewModel>()
            .ForMember(x => x.Number, x => x.MapFrom(e => e.Number))
            .ForMember(x => x.StartDate, x => x.MapFrom(e => e.StartDate))
            .ForMember(x => x.EndDate, x => x.MapFrom(e => e.EndDate))
            .ForMember(x => x.Groups, x => x.MapFrom(e => e.Groups));
        }
    }
}
