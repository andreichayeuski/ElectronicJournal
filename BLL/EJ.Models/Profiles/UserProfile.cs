using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.Enums;
using EJ.Models.UI;
using SHARED.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInfoViewModel>()
                .ForMember(x => x.FName, x => x.MapFrom(e => e.FName))
                .ForMember(x => x.MName, x => x.MapFrom(e => e.MName))
                .ForMember(x => x.SName, x => x.MapFrom(e => e.SName))
                .ForMember(x => x.Email, x => x.MapFrom(e => e.Email))
                .ForMember(x => x.BirthDay, x => x.MapFrom(e => e.BirthDay))
                .ForMember(x => x.PersonalNumber, x => x.MapFrom(e => e.PersonalNumber))
                .ForMember(x => x.GroupId, x => x.MapFrom(e => e.GroupId))
                .ForMember(x => x.UserStateId, x => x.MapFrom(e => e.UserStateId))
                .ForMember(x => x.StartDate, x => x.MapFrom(e => e.StartDate))
                .ForMember(x => x.RemovalDate, x => x.MapFrom(e => e.RemovalDate))
                .ForMember(x => x.Sex, x => x.MapFrom(e => e.Sex))
                .ForMember(x => x.RoleId, x => x.MapFrom(e => e.RoleId))
                .ForMember(x => x.Role, x => x.MapFrom(e => (RolesEnum)e.RoleId))
                .ForMember(x => x.RoleName, x => x.MapFrom(e => ((RolesEnum)e.RoleId).GetDescription()));
        }
        
    }
}
