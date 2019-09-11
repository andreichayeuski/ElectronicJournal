using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Auditorium : EntityBase
    {
        public Auditorium()
        {
            SheduleTimeSpendings = new HashSet<SheduleTimeSpending>();
        }
        public string Number { get; set; }

        public virtual ICollection<SheduleTimeSpending> SheduleTimeSpendings { get; set; }
    }
}
