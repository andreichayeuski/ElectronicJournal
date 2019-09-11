using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EJ.Domain.Services;
using EJ.Models.Enums;

namespace EJ.Web.BaseRazorPages
{
    public abstract class BaseRazorPage<TModel> : RazorPage<TModel>
    {
        [RazorInject] public IUserService UserService { get; set; }

        public bool IsCurrentUserInRole(RolesEnum role)
        {
            return UserService.CurrentUserRole == role;
        }

        public string CurrentControllerName =>
            ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) ViewContext.ActionDescriptor)
            .ControllerName;

        private long _ticks=0;

        public long UniqueTicksId
        {
            get
            {
                if (_ticks == 0)
                    _ticks = DateTime.Now.Ticks;

                return _ticks;
            }
        }
    }
}
