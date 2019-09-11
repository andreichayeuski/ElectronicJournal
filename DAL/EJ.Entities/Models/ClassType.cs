using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class ClassType : EntityBase
    {
        public ClassType()
        {
            SheduleTimeSpendings = new HashSet<SheduleTimeSpending>();
        }

        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<SheduleTimeSpending> SheduleTimeSpendings { get; set; }
    }
}
