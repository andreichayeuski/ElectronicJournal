using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class SheduleDateViewModel
    {
        public DateTime Date { get; set; }
        public GroupViewModel Group { get; set; }
        public IEnumerable<LessonViewModel> Lessons { get; set; }
    }
}
