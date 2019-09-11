using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class GroupUi
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public int CourseId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Подгруппа (0 - первая, 1 - вторая)
        /// </summary>
        public bool HalfGroup { get; set; }

        //public ICollection<UserInfoUi> Users { get; set; }
    }
}
