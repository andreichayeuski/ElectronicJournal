using System;

namespace EJ.Entities.Models
{
    public class UserRolesHistory : EntityBase
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } 

        public int RoleId { get; set; }

        public DateTime EditingDate { get; set; }
    }
}
