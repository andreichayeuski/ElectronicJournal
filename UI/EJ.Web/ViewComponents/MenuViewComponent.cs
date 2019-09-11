using EJ.Domain.Services;
using EJ.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using SHARED.Web.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EJ.Models.UI;

namespace EJ.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ISessionCache _sessionCache;
        private readonly IUserService _userService;

        public MenuViewComponent(ISessionCache sessionCache, IUserService userService)
        {
            _sessionCache = sessionCache;
            _userService = userService;
        }

        private string _key => "CurrentUserMainMenuModel_" + _userService.CurrentUserLogin;

        private IEnumerable<MenuLinkItem> GetMenuItems()
        {
            return new List<MenuLinkItem>
            {
                #region Top menu

                new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Shedule,
                    ParentId = 0,
                    Text = "Расписание",
                    CSS = @"nav-link text-dark",
                    Controller = "Shedule",
                    Action = "Index",
                    Roles = new[]
                    {
                        RolesEnum.Студент, RolesEnum.Староста, RolesEnum.ЗаместительCтаросты,
                        RolesEnum.Администратор
                    }
                },
                //new MenuLinkItem
                //{
                //    Id = MenuItemIdEnum.Shedule,
                //    ParentId = 0,
                //    Text = "Пропуски",
                //    CSS = @"nav-link text-dark",
                //    Area = "Absence",
                //    Controller = "Absence",
                //    Action = "Index",
                //    Roles = new[]
                //    {
                //        RolesEnum.Студент, RolesEnum.Староста, RolesEnum.ЗаместительCтаросты,
                //        RolesEnum.Администратор
                //    }
                //},
                new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Administration,
                    ParentId = 0,
                    Text = "Администрирование",
                    Area = "Administration",
                    Controller = "Administration",
                    Action = "Index",
                    CSS = @"nav-link text-dark",
                    Roles = new[]
                    {
                        RolesEnum.Администратор
                    }
                },
                new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Login,
                    ParentId = 0,
                    Text = "Войти",
                    Controller = "Account",
                    Action = "Login",
                    CSS = @"nav-link text-dark",
                    Roles = new[]
                    {
                        RolesEnum.UnAuthorized
                    }
                },
                new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Register,
                    ParentId = 0,
                    Text = "Зарегистрироваться",
                    Controller = "Account",
                    Action = "Register",
                    CSS = @"nav-link text-dark",
                    Roles = new[]
                    {
                        RolesEnum.UnAuthorized
                    }
                },
                new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Logout,
                    ParentId = 0,
                    Text = "Выйти",
                    Controller = "Account",
                    Action = "Logout",
                    CSS = @"nav-link text-dark",
                    Roles = new[]
                    {
                        RolesEnum.Администратор, RolesEnum.ЗаместительCтаросты, RolesEnum.Студент, RolesEnum.Староста
                    }
                },

                #region Submenu
                new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Administration_Users,
                    ParentId = MenuItemIdEnum.Administration,
                    Text = "Пользователи",
                    Controller = "User",
                    Action = "Index",
                    Area = "Administration",
                    Order = 0,
                    CSS = @"nav-link-sub text-dark",
                    Roles = new[]
                    {
                        RolesEnum.Администратор
                    }
                },
                 new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Administration_Groups,
                    ParentId = MenuItemIdEnum.Administration,
                    Text = "Группы",
                    Controller = "Group",
                    Action = "Index",
                    Area = "Administration",
                    Order = 0,
                    CSS = @"nav-link-sub text-dark",
                    Roles = new[]
                    {
                        RolesEnum.Администратор
                    }
                },
                 new MenuLinkItem
                {
                    Id = MenuItemIdEnum.Administration_Courses,
                    ParentId = MenuItemIdEnum.Administration,
                    Text = "Курсы",
                    Controller = "Course",
                    Action = "Index",
                    Area = "Administration",
                    Order = 0,
                    CSS = @"nav-link-sub text-dark",
                    Roles = new[]
                    {
                        RolesEnum.Администратор
                    }
                },

                #endregion
            };


            #endregion
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // TODO иньекция сервис и т.д
            var menuModel = GenerateMenuModel(GetMenuItems());

            return View("Main", menuModel);
        }
        private MenuModel GenerateMenuModel(IEnumerable<MenuLinkItem> items)
        {
            if (_sessionCache.TryGet(_key, out MenuModel cachedMenuModel))
                return
                    cachedMenuModel;

            var menuModel = new MenuModel();
            // Проходим по дереву до подменю и генерируем ссылку, на которую будет ссылаться элемент главного меню
            menuModel.MenuItems.AddRange(items
                .Where(c => c.ParentId == 0 && (!c.Roles.Any()
                    || c.Roles.Any(r => _userService.CurrentUserRole == r)))
                            .Select(t => GenerateSubMenuModel(items, t)).Where(t => t != null));

            _sessionCache.AddOrReplace(_key, menuModel);

            return menuModel;
        }

        private MenuLinkItem GenerateSubMenuModel(IEnumerable<MenuLinkItem> items, MenuLinkItem item)
        {
            item.ChildItems =
                items.Where(c =>
                        c.ParentId == item.Id && (!c.Roles.Any() || c.Roles.Any(r => _userService.CurrentUserRole == r))).OrderBy(c => c.Order)
                    .ToArray();

            foreach (var topMenuItem in item.ChildItems)
                topMenuItem.ChildItems = items.Where(c =>
                        c.ParentId == topMenuItem.Id && (!c.Roles.Any() || c.Roles.Any(r => _userService.CurrentUserRole == r)))
                    .Select(t => GenerateSubMenuModel(items, t)).Where(t => t != null).ToArray();

            return item;
        }
    }
}