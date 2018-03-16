using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class BerlinClockTimeConverter : ITimeConverter<string>
    {
        // output layout
        private static Dictionary<int, string> LampOnOutputByRow = new Dictionary<int, string>
        {
            { 0, "Y" },
            { 1, "RRRR" },
            { 2, "RRRR" },
            { 3, "YYRYYRYYRYY" },
            { 4, "YYYY" }
        };

        private StringBuilder _output;

        // Considerations about the string parameter instead of TimeSpan:
        //
        // 1) It would be better to expose the parameter as TimeSpan, then the parsing would not be covered by this class (too much responsabilities for a class).
        //    - Since this is a ITimeConverter, one can argue that we should only recognize a time type, and the scenario calls for TimeSpan.
        //
        // 2) But the time is string here for SpecFlow parsing reasons. Since there's no strong RegEx in the feature file, it would only get those values as strings.
        //    - After a quick read I think we're able to improve the SpecFlow parsing capabilities with Argument Transforms*, than this argument
        //      could be exposed as TimeSpan and SpecFlow would parse it accordingly to provide as argument in the test case(s).
        //        - But then the 24:00:00 scenario would be required to be removed, since "24:00:00" would be parsed as 24.00:00:00.
        //
        // * https://github.com/cucumber/cucumber/wiki/Step-Argument-Transforms
        public string ConvertFrom(string time)
        {
            if (!TimeSpan.TryParse(time, out TimeSpan timeSpan))
                throw new FormatException($"The provided value '{time}' does not represent a valid time.");

            return ConvertFrom(timeSpan);
        }

        public string ConvertFrom(TimeSpan time)
        {
            // 24:00:00 is not standard time format, it will be parsed as a 24 days TimeSpan, so I think this should be discussed and not accepted as a valid time.
            // If so, then the following check should be uncommented:
            // if (time.TotalDays >= 1)
            //     throw new ArgumentOutOfRangeException("time", time, "The value cannot be greater than '0.23:59:59'.");

            // For now 24:00:00 (parsed as TimeSpan as 24.00:00:00) is acceptable as stated in a Scenario, so...
            int hours = time.Hours,
                minutes = time.Minutes,
                seconds = time.Seconds;

            if (time.TotalDays >= 1)
            {
                // we only accept the 24 days case, other time with TotalDays greater than 1 it's invalid
                if (time == TimeSpan.FromDays(24))
                    hours = 24;
                else
                    throw new ArgumentOutOfRangeException("time", time, "The value is out of the supported range: From '00:00:00' to '23:59:59'.");
            }

            _output = new StringBuilder();

            WriteSeconds(seconds);
            WriteHours(hours);
            WriteMinutes(minutes);

            return _output.ToString().TrimEnd();
        }

        private void WriteHours(int hours)
        {
            var firstRowOnCount = hours / 5;
            var secondRowOnCount = hours % 5;

            WriteRow(1, firstRowOnCount);
            WriteRow(2, secondRowOnCount);
        }

        private void WriteMinutes(int minutes)
        {
            var thirdRowOnCount = minutes / 5;
            var forthRowOnCount = minutes % 5;

            WriteRow(3, thirdRowOnCount);
            WriteRow(4, forthRowOnCount);
        }

        private void WriteSeconds(int seconds)
        {
            var isEven = seconds % 2 == 0;
            var onCount = isEven ? 1 : 0;

            WriteRow(0, onCount);
        }

        private void WriteRow(int row, int lampsOnCount)
        {
            var rowLength = LampOnOutputByRow[row].Length;
            bool on;

            foreach (int lampIndex in Enumerable.Range(0, rowLength))
            {
                on = lampsOnCount > lampIndex;
                _output.Append(GetLampOutput(row, lampIndex, on));
            }

            _output.AppendLine();
        }

        private string GetLampOutput(int row, int column, bool on)
        {
            return on ?
                GetLampOnOutput(row, column) :
                GetLampOffOutput(row, column);
        }

        private string GetLampOnOutput(int row, int column)
        {
            return LampOnOutputByRow[row][column].ToString();
        }

        private string GetLampOffOutput(int row, int column)
        {
            return "O";
        }
    }
}
