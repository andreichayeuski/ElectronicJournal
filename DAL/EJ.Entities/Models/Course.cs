using System;
using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Course : EntityBase
    {
        public Course()
        {
            Groups = new HashSet<Group>();
        }

        public int Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
