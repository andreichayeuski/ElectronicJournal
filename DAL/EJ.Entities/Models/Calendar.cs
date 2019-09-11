using System;
using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Calendar : EntityBase
    {
        public Calendar()
        {
            CalendarSheduleTimeSpendings = new HashSet<CalendarSheduleTimeSpending>();
        }

        public DateTime Date { get; set; }

        public virtual ICollection<CalendarSheduleTimeSpending> CalendarSheduleTimeSpendings  { get; set; }
    }
}
