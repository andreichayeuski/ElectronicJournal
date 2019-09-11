using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class GroupShedule : EntityBase
    {
        public GroupShedule()
        {
            SheduleSubjects = new HashSet<SheduleSubject>();
        }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }

        public virtual ICollection<SheduleSubject> SheduleSubjects { get; set; }
    }
}
