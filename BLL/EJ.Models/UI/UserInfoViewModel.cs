using System;
using System.Collections.Generic;
using System.Text;
using EJ.Models.Enums;

namespace EJ.Models.UI
{
    public class UserInfoViewModel
    {
        public RolesEnum Role { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string SName { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? RemovalDate { get; set; }
        public string PersonalNumber { get; set; }
        public int? GroupId { get; set; }
        public int UserStateId { get; set; }
        public int? RoleId { get; set; }
        public bool Sex { get; set; }
        public string RoleName { get; set; }
        
    }
}
