using System;

namespace EJ.Entities.Models
{
    public class UserHistory : EntityBase
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime EditingDate { get; set;}

        public int StateId { get; set; }
    }
}
