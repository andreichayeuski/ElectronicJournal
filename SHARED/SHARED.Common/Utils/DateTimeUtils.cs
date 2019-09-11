using System;

namespace SHARED.Common.Utils
{
    public class DateTimeUtils
    {
        public static DateTime GetEasterDay(int year)
        {
            var a = year%19;
            var b = year%4;
            var c = year%7;
            var d = (19*a + 15)%30;
            var e = (2*b + 4*c + 6*d + 6)%7;

            var day = (d + e) < 9
                ? new DateTime(year, 3, 22 + d + e).AddDays(13)
                : new DateTime(year, 4, d + e - 9).AddDays(13);

            return day;
        }

        public static DateTime GetRadunitsaDay(int year)
        {
            var easter = GetEasterDay(year);
            return easter.AddDays(9);
        }

        public static DateTime GetFirstWeekDate(DateTime date)
        {
            const DayOfWeek firstDay = DayOfWeek.Monday;
            var firstDayInWeek = date.Date;

            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        public static DateTime GetLastWeekDate(DateTime date)
        {
            const DayOfWeek lastDay = DayOfWeek.Sunday;
            var lastDayInWeek = date.Date;

            while (lastDayInWeek.DayOfWeek != lastDay)
            {
                lastDayInWeek = lastDayInWeek.AddDays(1);
            }

            return lastDayInWeek;
        }

        public static DateTime GetFirstMonthDate(int month, int year)
        {
            return new DateTime(year, month, 1).Date;
        }

        public static DateTime GetLastMonthDate(int month, int year)
        {
            return new DateTime(year, month, 1).AddMonths(1).AddDays(-1).Date;
        }

        public static DateTime GetFirstQuarterDate(int quarter, int year)
        {
            return new DateTime(year, (quarter - 1) * 3 + 1, 1).Date;
        }

        public static DateTime GetLastQuarterDate(int quarter, int year)
        {
            return new DateTime(year, (quarter - 1) * 3 + 1, 1).AddMonths(3).AddDays(-1).Date;
        }

        public static DateTime GetFirstHalfyearDate(int halfyear, int year)
        {
            return new DateTime(year, (halfyear - 1) * 6 + 1, 1).Date;
        }

        public static DateTime GetLastHalfyearDate(int halfyear, int year)
        {
            return new DateTime(year, (halfyear - 1) * 6 + 1, 1).AddMonths(6).AddDays(-1).Date;
        }

        public static int GetHalfyear(DateTime date)
        {
            return (date.Month + 5) / 6;
        }

        public static DateTime GetFirstYearDate(int year)
        {
            return new DateTime(year, 1, 1);
        }

        public static DateTime GetLastYearDate(int year)
        {
            return new DateTime(year, 12, DateTime.DaysInMonth(year, 12));
        }

        public static int GetQuarter(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        public static DateTime EndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static DateTime Next(DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }

        public static DateTime Prev(DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target >= start)
                target -= 7;
            return from.AddDays(target - start);
        }

        public static DateTime MaxDate(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }
    }
}