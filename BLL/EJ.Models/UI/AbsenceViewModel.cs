using System;
using System.Collections.Generic;
using System.Text;

namespace EJ.Models.UI
{
    public class AbsenceViewModel
    {
        public LessonViewModel Lesson { get; set; }
        public List<UserInfoViewModel> Users { get; set; }
        public List<bool> IsPresent { get; set; }
        public int CalendarSheduleTimeSpendingId { get; set; }
    }
}
