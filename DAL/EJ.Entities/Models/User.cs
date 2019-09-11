using System;
using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class User : EntityBase
    {
        public User()
        {
            Absences = new HashSet<Absence>();
            UserHistories = new HashSet<UserHistory>();
            UserRolesHistories = new HashSet<UserRolesHistory>();
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string SName { get; set; }
        
        public string Fio => $"{SName} {FName} {MName}";
        public string Email { get; set; }

        public byte[] Password { get; set; }

        public DateTime BirthDay { get; set; }

        public string PersonalNumber { get; set; }

        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int UserStateId { get; set; }
        public virtual UserState UserState { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? RemovalDate { get; set; }

        /// <summary>
        /// Пол (1 - муж, 0 - жен)
        /// </summary>
        public bool Sex { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserHistory> UserHistories { get; set; }

        public virtual ICollection<UserRolesHistory> UserRolesHistories { get; set; }

        public virtual ICollection<Absence> Absences { get; set; }
        public bool EmailVerified { get; set; }
    }
}
 