using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class AbsenceFormUi
    {
        public LessonUi Lesson { get; set; }
        public List<UserInfoUi> Users { get; set; }
        public List<bool> IsPresent { get; set; }
        public int CalendarSheduleTimeSpendingId { get; set; }
    }
}
