using System.Collections.Generic;

namespace EJ.Entities.Models
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
