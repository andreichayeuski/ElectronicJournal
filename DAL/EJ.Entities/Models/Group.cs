using System;
using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Group : EntityBase
    {
        public Group()
        {
            Users = new HashSet<User>();
            GroupShedules = new HashSet<GroupShedule>();
        }

        public int Number { get; set; }

        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Подгруппа (0 - первая, 1 - вторая)
        /// </summary>
        public bool HalfGroup { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<GroupShedule> GroupShedules { get; set; }
    }
}
