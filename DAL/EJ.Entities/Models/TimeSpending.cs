using System;
using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class TimeSpending : EntityBase
    {
        public TimeSpending()
        {
            SheduleTimeSpendings = new HashSet<SheduleTimeSpending>();
        }
        public int Number { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<SheduleTimeSpending> SheduleTimeSpendings { get; set; }
    }
}
