using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class CalendarSheduleTimeSpending : EntityBase
    {
        public int CalendarId { get; set; }
        public virtual Calendar Calendar { get; set; }

        public int SheduleTimeSpendingId { get; set; }
        public virtual SheduleTimeSpending SheduleTimeSpending { get; set; }

        public virtual ICollection<Absence> Absences { get; set; }
    }
}
