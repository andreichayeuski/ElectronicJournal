using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SHARED.Models
{
    public class BeginDateEndDateViewModel
    {
        [DisplayName("Начало действия")]
        [Required(ErrorMessage = "Необходимо ввести начало срока действия")]
        public DateTime BeginDate { get; set; }

        public int BeginDateHours { get; set; }
        public int BeginDateMinutes { get; set; }
        public int BeginDateSeconds { get; set; }

        [DisplayName("Окончание действия")]
        public DateTime EndDate { get; set; }

        public int EndDateHours { get; set; }
        public int EndDateMinutes { get; set; }
        public int EndDateSeconds { get; set; }

        [DisplayName("Бессрочно")]
        public bool Forever { get; set; }

        public bool Disabled { get; set; }

        public DateTime GetBeginDate()
        {
            return Disabled ? DateTime.Now : new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginDateHours, BeginDateMinutes, BeginDateSeconds);
        }

        public DateTime GetEndDate()
        {
            return Disabled ? DateTime.MaxValue : new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndDateHours, EndDateMinutes, EndDateSeconds);
        }

        public void SetBeginDate(DateTime dateTime)
        {
            BeginDate = dateTime;
            BeginDateHours = dateTime.Hour;
            BeginDateMinutes = dateTime.Minute;
            BeginDateSeconds = dateTime.Second;
        }
        public void SetEndDate(DateTime dateTime)
        {
            EndDate = dateTime;
            EndDateHours = dateTime.Hour;
            EndDateMinutes = dateTime.Minute;
            EndDateSeconds = dateTime.Second;

            Forever = EndDate.Year == DateTime.MaxValue.Year;
        }

    }
}
