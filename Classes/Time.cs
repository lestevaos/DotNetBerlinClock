using System;

namespace BerlinClock
{
    /// <summary>
    /// Represents a Point in Time information expressed by Hours, Minutes and Seconds.
    /// This struct justify its existence by the following reasons:
    /// - ISO 8601 accepts 24:00:00 as a valid Time to denote midnight at the end of a calendar day
    /// - System.TimeSpan does not assertively support representing a 24:00:00 ISO 8601 standard Time
    /// - System.TimeSpan does not represent a Point in Time but rather an interval of time
    /// </summary>
    public struct Time
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public Time(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 24)
                throw new ArgumentOutOfRangeException(nameof(hours), hours, "Hours should be in the 0-24 range.");

            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "Minutes should be in the 0-59 range.");

            if (seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException(nameof(seconds), seconds, "Seconds should be in the 0-59 range.");

            if (hours == 24)
            {
                if (minutes > 0)
                    throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "When Hours is 24, Minutes cannot be greater than 0.");

                if (seconds > 0)
                    throw new ArgumentOutOfRangeException(nameof(seconds), seconds, "When Hours is 24, Seconds cannot be greater than 0.");
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public static Time Parse(string time)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time));

            if (time.Trim() == "24:00:00")
                return new Time(24, 0, 0);

            // Using TimeSpan parsing capabilities since our Point in Time format is similar
            if (!TimeSpan.TryParse(time, out TimeSpan timeSpan))
                throw new ArgumentException($"The provided value '{time}' does not represent a valid time.", nameof(time));

            // Making sure to only represent a Point in Time inside the interval of a day (with 24 being the EoD flag)
            if (timeSpan.TotalDays >= 1)
                throw new ArgumentException($"The provided value '{time}' does not represent a valid Point in Time.", nameof(time));

            return new Time(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public override string ToString()
        {
            return $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}:{Seconds.ToString().PadLeft(2, '0')}";
        }
    }
}
