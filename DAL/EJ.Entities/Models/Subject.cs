using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Subject : EntityBase
    {
        public Subject()
        {
            SheduleSubjects = new HashSet<SheduleSubject>();
        }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public virtual ICollection<SheduleSubject> SheduleSubjects { get; set; }
    }
}
