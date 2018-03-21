using System;

namespace BerlinClock
{
    /// <summary>
    /// Represents a Point in Time information expressed by Hours, Minutes and Seconds.
    /// This struct justify its existence by the following reasons:
    /// - ISO 8601 accepts 24:00:00 as a valid Time to denote midnight at the end of a calendar day.
    /// - System.TimeSpan does not assertively support representing a 24:00:00 ISO 8601 standard Time.
    /// - System.TimeSpan does not represent a Point in Time but rather an interval of time.
    /// </summary>
    public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        #region Fields

        private readonly int _totalSeconds;

        public static readonly Time MinValue = new Time(0, 0, 0);
        public static readonly Time MaxValue = new Time(24, 0, 0);

        #endregion

        #region Properties

        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        #endregion

        #region Ctor

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

            _totalSeconds = (hours * 3600) + (minutes * 60) + seconds;
        }

        #endregion

        #region Methods

        #region IComparable

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (!(obj is Time))
                throw new ArgumentException($"The value is not an instance of {typeof(Time).FullName}.", nameof(obj));

            return CompareTo((Time)obj);
        }

        #endregion

        #region IComparable<Time>

        public int CompareTo(Time value)
        {
            return _totalSeconds.CompareTo(value._totalSeconds);
        }

        #endregion

        #region IEquatable

        public bool Equals(Time other)
        {
            return _totalSeconds == other._totalSeconds;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (!(obj is Time))
                return false;

            return Equals((Time)obj);
        }

        public override int GetHashCode()
        {
            return _totalSeconds * 7541;
        }

        public override string ToString()
        {
            return $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }

        #endregion

        #region Operators

        public static bool operator ==(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1 == t2);
        }

        public static bool operator <(Time t1, Time t2)
        {
            return t1.CompareTo(t2) == -1;
        }

        public static bool operator <=(Time t1, Time t2)
        {
            return t1.CompareTo(t2) <= 0;
        }

        public static bool operator >(Time t1, Time t2)
        {
            return t1.CompareTo(t2) == 1;
        }

        public static bool operator >=(Time t1, Time t2)
        {
            return t1.CompareTo(t2) >= 0;
        }

        #endregion

        public static Time Parse(string time)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time));

            if (time.Trim() == "24:00:00")
                return MaxValue;

            TimeSpan timeSpan;

            // Using TimeSpan parsing capabilities since our Point in Time format is similar
            if (!TimeSpan.TryParse(time, out timeSpan))
                throw new ArgumentException($"The provided value '{time}' does not represent a valid time.", nameof(time));

            // Making sure to only represent a Point in Time inside the interval of a day (with 24 being the EoD flag)
            if (timeSpan.TotalDays >= 1)
                throw new ArgumentException($"The provided value '{time}' does not represent a valid time.", nameof(time));

            return new Time(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public static bool TryParse(string time, out Time result)
        {
            try
            {
                result = Parse(time);
                return true;
            }
            catch { }

            result = default(Time);
            return false;
        }

        #endregion
    }
}
