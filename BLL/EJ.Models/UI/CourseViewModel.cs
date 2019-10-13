using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<GroupViewModel> Groups { get; set; }
    }
}
