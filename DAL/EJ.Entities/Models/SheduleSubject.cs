using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class SheduleSubject : EntityBase
    {
        public SheduleSubject()
        {
            SheduleTimeSpendings = new HashSet<SheduleTimeSpending>();
        }
        public int GroupSheduleId { get; set; }
        public virtual GroupShedule GroupShedule { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<SheduleTimeSpending> SheduleTimeSpendings { get; set; }
    }
}
