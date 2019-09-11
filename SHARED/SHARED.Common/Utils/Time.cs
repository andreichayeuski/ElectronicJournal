using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils
{
    public class Time : IEquatable<Time>, IComparable<Time>
    {
        public static Time MinValue = new Time(0, 0, 0, 0);
        public static Time MaxValue = new Time(23, 59, 59, 999);

        private const int MillisPerSecond = 1000;
        private const int MillisPerMinute = MillisPerSecond * 60;
        private const int MillisPerHour = MillisPerMinute * 60;

        private const char ToStringMillisSeparator = ',';
        private const char ToStringTimePartsSeparator = ':';

        public Time(int hour, int minute, int second, int millisecond)
        {
            if (!ValidateTimeParameters(hour, minute, second, millisecond))
            {
                throw new ArgumentException();
            }

            AdjustTime(ref hour, ref minute, ref second, ref millisecond);

            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
        }

        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }
        public int Millisecond { get; private set; }

        public static Time Now
        {
            get { return FromDateTime(DateTime.Now); }
        }

        public static Time FromDateTime(DateTime dateTime)
        {
            var result = new Time(dateTime.Hour,
                                  dateTime.Minute,
                                  dateTime.Second,
                                  dateTime.Millisecond);

            return result;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Hour;
                hashCode = (hashCode * 397) ^ Minute;
                hashCode = (hashCode * 397) ^ Second;
                hashCode = (hashCode * 397) ^ Millisecond;

                return hashCode;
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.
        /// Zero This object is equal to <paramref name="other"/>. 
        /// Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Time other)
        {
            if (other == null)
            {
                return 1;
            }

            var thisMilliseconds = GetMillisecondsFromDayStart();
            var otherMilliseconds = other.GetMillisecondsFromDayStart();
            var result = thisMilliseconds - otherMilliseconds;

            return result;
        }

        public override bool Equals(object obj)
        {
            return (obj is Time && Equals((Time)obj));
        }

        public override string ToString()
        {
            var result = String.Format("{0:D2}:{1:D2}:{2:D2}, {3:D3}",
                                       Hour,
                                       Minute,
                                       Second,
                                       Millisecond);

            return result;
        }

        public bool Equals(Time other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            var result = other.Hour == Hour
                         && other.Minute == Minute
                         && other.Second == Second
                         && other.Millisecond == Millisecond;

            return result;
        }

        public Time AddHours(int hours)
        {
            var milliseconds = MillisPerHour * hours;
            var result = AddMilliseconds(milliseconds);

            return result;
        }

        public Time AddMinutes(int minutes)
        {
            var milliseconds = MillisPerMinute * minutes;
            var result = AddMilliseconds(milliseconds);

            return result;
        }

        public Time AddSeconds(int seconds)
        {
            var milliseconds = MillisPerSecond * seconds;
            var result = AddMilliseconds(milliseconds);

            return result;
        }

        public Time AddMilliseconds(int milliseconds)
        {
            var seconds = Math.DivRem(Millisecond + milliseconds, 1000, out milliseconds);
            var minutes = Math.DivRem(Second + seconds, 60, out seconds);
            var hours = Math.DivRem(Minute + minutes, 60, out minutes) % 24;

            var result = new Time(Hour + hours,
                                  minutes,
                                  seconds,
                                  milliseconds < 0 ? 1000 + milliseconds : milliseconds);

            return result;
        }

        public static Time Parse(string str)
        {
            Time result;
            if (!TryParse(str, out result))
            {
                throw new FormatException();
            }

            return result;
        }

        public static bool TryParse(string str, out Time result)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                result = null;
                return false;
            }

            var timeAndMillis = str.Split(ToStringMillisSeparator);
            if (1 < timeAndMillis.Length || timeAndMillis.Length > 2)
            {
                result = null;
                return false;
            }

            int millisecond = 0;
            if (timeAndMillis.Length == 2 && !Int32.TryParse(timeAndMillis[1], out millisecond))
            {
                result = null;
                return false;
            }

            var timeParts = timeAndMillis[0].Split(ToStringTimePartsSeparator);

            if (timeParts.Length != 3)
            {
                result = null;
                return false;
            }

            int hour = 0, minute = 0, second = 0;
            if (!Int32.TryParse(timeParts[0], out hour)
                || !Int32.TryParse(timeParts[1], out minute)
                || !Int32.TryParse(timeParts[2], out second))
            {
                result = null;
                return false;
            }

            if (!ValidateTimeParameters(hour, minute, second, millisecond))
            {
                result = null;
                return false;
            }

            result = new Time(hour, minute, second, millisecond);
            return true; ;
        }

        public static bool operator ==(Time fst, Time snd)
        {
            var result = Equals(fst, snd);
            return result;
        }

        public static bool operator !=(Time fst, Time snd)
        {
            var result = !(fst == snd);
            return result;
        }

        public static bool operator >(Time fst, Time snd)
        {
            var result = fst.CompareTo(snd) > 0;
            return result;
        }

        public static bool operator <(Time fst, Time snd)
        {
            var result = snd > fst;
            return result;
        }

        public static bool operator >=(Time fst, Time snd)
        {
            var result = fst > snd || fst == snd;
            return result;
        }

        public static bool operator <=(Time fst, Time snd)
        {
            var result = fst < snd || fst == snd;
            return result;
        }

        public static TimeSpan operator -(Time fst, Time snd)
        {
            var fstMilliseconds = fst.GetMillisecondsFromDayStart();
            var sndMilliseconds = snd.GetMillisecondsFromDayStart();
            var result = TimeSpan.FromMilliseconds(fstMilliseconds - sndMilliseconds);

            return result;
        }

        public static Time operator +(Time time, TimeSpan timeSpan)
        {
            var result = time.AddMilliseconds((int)timeSpan.TotalMilliseconds);
            return result;
        }

        public static Time operator -(Time time, TimeSpan timeSpan)
        {
            var result = time.AddMilliseconds(-(int)timeSpan.TotalMilliseconds);
            return result;
        }

        private int GetMillisecondsFromDayStart()
        {
            var minutes = Hour * 60 + Minute;
            var seconds = minutes * 60 + Second;
            var milliseconds = seconds * 1000 + Millisecond;

            return milliseconds;
        }

        private static bool ValidateTimeParameters(int hour, int minute, int second, int millisecond)
        {
            if (hour < 0 || hour > 24)
            {
                return false;
            }

            if (minute < 0 || minute > 60)
            {
                return false;
            }

            if (second < 0 || second > 60)
            {
                return false;
            }

            if (millisecond < 0 || millisecond > 1000)
            {
                return false;
            }

            return true;
        }

        private static void AdjustTime(ref int hour, ref int minute, ref int second, ref int millisecond)
        {
            // maybe return tuple instead of refs?
            if (millisecond == 1000)
            {
                second++;
                millisecond = 0;

                AdjustTime(ref hour, ref minute, ref second, ref millisecond);
            }

            if (second == 60)
            {
                minute++;
                second = 0;

                AdjustTime(ref hour, ref minute, ref second, ref millisecond);
            }

            if (minute == 60)
            {
                hour++;
                minute = 0;

                AdjustTime(ref hour, ref minute, ref second, ref millisecond);
            }

            if (hour == 24)
            {
                hour = 0;
            }
        }
    }
}
