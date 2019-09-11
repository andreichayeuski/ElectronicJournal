using System;
using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Semester : EntityBase
    {
        public Semester()
        {
            GroupShedules = new HashSet<GroupShedule>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<GroupShedule> GroupShedules { get; set; }
    }
}
