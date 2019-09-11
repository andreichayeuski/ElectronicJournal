namespace EJ.Entities.Models
{
    public class Absence : EntityBase
    {
        public int CalendarSheduleTimeSpendingId { get; set; }
        public virtual CalendarSheduleTimeSpending CalendarSheduleTimeSpending { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public bool IsRespectfulReason { get; set; }

        public string Description { get; set; }

        public int? AbsenceNotificationId { get; set; }
        public virtual AbsenceNotification AbsenceNotification { get; set; }
    }
}
