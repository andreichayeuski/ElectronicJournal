using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class UserState : EntityBase
    {
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}
