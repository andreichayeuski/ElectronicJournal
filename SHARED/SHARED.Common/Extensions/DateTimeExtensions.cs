using System;
using System.Collections.Generic;
using System.Linq;
using SHARED.Common.Utils;

namespace SHARED.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetFirstDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = (DayOfWeek.Monday - (int)sourceDateTime.DayOfWeek);

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

        public static DateTime GetLastDayOfWeek(this DateTime sourceDateTime)
        {
            var daysAhead = DayOfWeek.Saturday - (int)sourceDateTime.DayOfWeek;

            sourceDateTime = sourceDateTime.AddDays((int)daysAhead);

            return sourceDateTime;
        }

        public static DateTime Before(this DateTime date, Time maxTime)
        {
            if (maxTime == null)
            {
                throw new ArgumentNullException("maxTime");
            }

            if (maxTime < Time.FromDateTime(date))
            {
                return new DateTime(date.Year, date.Month, date.Day, maxTime.Hour, maxTime.Minute, maxTime.Second, maxTime.Millisecond);
            }

            return date;
        }
        public static int GetDifferenceInYears(this DateTime startDate, DateTime endDate)
        {
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day)// BUT the end day is less than the start day
            {
                years--;
            }
            else if (endDate.Month < startDate.Month)// if the end month is less than the start month
            {
                years--;
            }

            return years;
        }

        public static DateTime SetYear(this DateTime date, int year)
        {
            return new DateTime(year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        public static DateTime GetFirstMonthDate(this DateTime currentDate)
        {
            return new DateTime(currentDate.Year, currentDate.Month, 1);
        }

        public static IEnumerable<DateTime> GetDaysInCurrentMonthBeforeThisDate(this DateTime currentDate)
        {
            var startDate=currentDate.GetFirstMonthDate();

            while (startDate<currentDate)
            {
                yield return startDate;

                startDate = startDate.AddDays(1);
            }
        }

        public static IEnumerable<DateTime> GetDaysInCurrentMonth(this DateTime currentDate)
        {
            var startDate = currentDate.GetFirstMonthDate();

            var month = startDate.Month;

            while (startDate.Month ==month)
            {
                yield return startDate;

                startDate = startDate.AddDays(1);
            }
        }

        public static IEnumerable<DateTime> Range(this DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d));
        }
    }
}
