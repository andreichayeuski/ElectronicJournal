using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class SheduleTimeSpending : EntityBase
    {
        public SheduleTimeSpending()
        {
            CalendarSheduleTimeSpendings = new HashSet<CalendarSheduleTimeSpending>();
        }
        public int SheduleSubjectId { get; set; }
        public virtual SheduleSubject SheduleSubject { get; set; }

        public int WeekDayId { get; set; }
        public virtual WeekDay WeekDay { get; set; }

        public int TimeSpendingId { get; set; }
        public virtual TimeSpending TimeSpending { get; set; }

        public int AuditoriumId { get; set; }
        public virtual Auditorium Auditorium { get; set; }

        public int ClassTypeId { get; set; }
        public virtual ClassType ClassType { get; set; }

        public virtual ICollection<CalendarSheduleTimeSpending> CalendarSheduleTimeSpendings { get; set; }
    }
}
