using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class WeekDay : EntityBase
    {
        public WeekDay()
        {
            SheduleTimeSpendings = new HashSet<SheduleTimeSpending>();
        }
        public string Day { get; set; }
        public int NumberOfWeek { get; set; }
        public virtual ICollection<SheduleTimeSpending> SheduleTimeSpendings { get; set; }
    }
}
