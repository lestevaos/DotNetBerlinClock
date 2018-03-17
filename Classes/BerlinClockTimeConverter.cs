using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class BerlinClockTimeConverter : ITimeConverter<string>
    {
        // Output layout
        private static readonly Dictionary<int, string> LampOnOutputByRow = new Dictionary<int, string>
        {
            { 0, "Y" },
            { 1, "RRRR" },
            { 2, "RRRR" },
            { 3, "YYRYYRYYRYY" },
            { 4, "YYYY" }
        };

        public string ConvertFrom(Time time)
        {
            var output = new StringBuilder();

            WriteSeconds(time.Seconds, output);
            WriteHours(time.Hours, output);
            WriteMinutes(time.Minutes, output);

            return output.ToString().TrimEnd();
        }

        private void WriteHours(int hours, StringBuilder output)
        {
            var firstRowOnCount = hours / 5;
            var secondRowOnCount = hours % 5;

            WriteRow(1, firstRowOnCount, output);
            WriteRow(2, secondRowOnCount, output);
        }

        private void WriteMinutes(int minutes, StringBuilder output)
        {
            var thirdRowOnCount = minutes / 5;
            var forthRowOnCount = minutes % 5;

            WriteRow(3, thirdRowOnCount, output);
            WriteRow(4, forthRowOnCount, output);
        }

        private void WriteSeconds(int seconds, StringBuilder output)
        {
            var isEven = seconds % 2 == 0;
            var onCount = isEven ? 1 : 0;

            WriteRow(0, onCount, output);
        }

        private void WriteRow(int row, int lampsOnCount, StringBuilder output)
        {
            var rowLength = LampOnOutputByRow[row].Length;
            bool on;

            foreach (int lampIndex in Enumerable.Range(0, rowLength))
            {
                on = lampsOnCount > lampIndex;
                output.Append(GetLampOutput(row, lampIndex, on));
            }

            output.AppendLine();
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
