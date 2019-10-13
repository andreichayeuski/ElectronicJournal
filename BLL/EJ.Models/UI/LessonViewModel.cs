using System;
using System.Collections.Generic;
using System.Text;
using EJ.Models.Enums;

namespace EJ.Models.UI
{
    public class LessonViewModel
    {
        public int CalendarSheduleTimeSpendingId { get; set; }
        public string Subject { get; set; }
        public int WeekNumber { get; set; }
        public string Auditorium { get; set; }
        public int Number { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ClassTypeEnum ClassType { get; set; }
        public string WasAbsence { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CourseId { get; set; }
        public int Group { get; set; }
    }
}
