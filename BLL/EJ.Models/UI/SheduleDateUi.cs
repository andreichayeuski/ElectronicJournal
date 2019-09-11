using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class SheduleDateUi
    {
        public DateTime Date { get; set; }
        public GroupUi Group { get; set; }
        public IEnumerable<LessonUi> Lessons { get; set; }
    }
}
