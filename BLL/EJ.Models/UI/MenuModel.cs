using EJ.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class MenuModel
    {
        public MenuModel()
        {
            MenuItems = new List<MenuLinkItem>();
        }

        public List<MenuLinkItem> MenuItems { get; set; }
    }

    public class MenuLinkItem
    {
        public MenuLinkItem()
        {
            Area = "";
            CSS = "";
            Class = "";
            IsDisplayed = true;
            ShowChildItems = true;
            Roles = new RolesEnum[0];
            //Tasks = new TasksEnum[0];
            ChildItems = new MenuLinkItem[0];
        }

        /// <summary>
        ///     Id меню
        /// </summary>
        public MenuItemIdEnum Id { get; set; }

        /// <summary>
        ///     Id родительского элемента меню
        /// </summary>
        public MenuItemIdEnum ParentId { get; set; }

        /// <summary>
        ///     Текст меню
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Класс для определения иконки
        /// </summary>
        public string Class { get; set; }


        /// <summary>
        ///     Area
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        ///     Controller
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        ///     Action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///     Список тасков, один из которых необходим для доступа к меню
        /// </summary>
       // public TasksEnum[] Tasks { get; set; }

        /// <summary>
        ///     Список ролей, для которых доступен элемент меню
        /// </summary>
        public RolesEnum[] Roles { get; set; }

        /// <summary>
        ///     CSS
        /// </summary>
        public string CSS { get; set; }

        /// <summary>
        ///     Отображать данное меню в списке, либо же оно является вспомогательным
        /// </summary>
        public bool IsDisplayed { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        ///     Определяет порядок при выборе ссылки для мею сервисов
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     дочерние ссылки
        /// </summary>
        public MenuLinkItem[] ChildItems { get; set; }

        public bool ShowChildItems { get; set; }
        public string GroupName { get; set; }
    }


    public enum MenuItemIdEnum
    {
        Administration = 1,
        Administration_Users = 100,
        Administration_Groups = 101,
        Administration_Courses = 102,

        Shedule = 2,

        UserChanges = 4,
        Login = 401,
        Register = 403,
        Logout = 499
    }
}
