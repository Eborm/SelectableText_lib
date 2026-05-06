using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SelectableText_lib_namespace.Classes
{
    public class DatePicker
    {
        private DateTime SelectedDate = new DateTime();
        public DatePicker()
        {
        }

        public DateTime DatePickerSingleDate(DateTime startDate, DateTime endDate)
        {
            SelectableText_lib stlib = new SelectableText_lib(true);

            var startMonth = startDate.Month;
            var startYear = startDate.Year;
            var endMonth = endDate.Month;
            var endYear = endDate.Year;

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be before end date.");
            }
            else if (startDate.Year == endDate.Year)
            {
                foreach (var month in Enumerable.Range(startMonth, endMonth - startMonth + 1))
                {
                    foreach (var line in GetBetterTextForMonth(month, startYear))
                    {
                        stlib.AddText(line);
                    }
                }
            }

            return new DateTime();
        }

        public Dictionary<int, Dictionary<int, string>> GetFullAsciiArt(DateTime startDate, DateTime endDate)
        {
            //Devide infromation about what months should be added to the calander picking system so the appropriate months can be added
            var startMonth = startDate.Month;
            var startYear = startDate.Year;
            var endMonth = endDate.Month;
            var endYear = endDate.Year;
            //Create a dictonary to store the month ascii art and the corrosponing month number
            var yearlyAsciiArt = new Dictionary<int, Dictionary<int, string>>();
            //Loop trough each year
            for (int i = startYear; i <= endYear; i++)
            {
                if (i == startYear)
                {
                    var monthAsciiArt = new Dictionary<int, string>();
                    for (int j = startMonth; j < 13; j++)
                    {
                        //string ascii = GetAsciiArtForMonth(j, i);
                        //monthAsciiArt.Add(j, ascii);
                    }
                    yearlyAsciiArt.Add(i, monthAsciiArt);
                }
                else if (i == endYear)
                {
                    var monthAsciiArt = new Dictionary<int, string>();
                    for (int j = 1; j < endMonth+1; j++)
                    {
                        //string ascii = GetAsciiArtForMonth(j, i);
                        //monthAsciiArt.Add(j, ascii);
                    }
                    yearlyAsciiArt.Add(i, monthAsciiArt);
                }
                else
                {
                    var monthAsciiArt = new Dictionary<int, string>();
                    for (int j = 1; j < 13; j++)
                    {
                        //string ascii = //GetAsciiArtForMonth(j, i);
                        //monthAsciiArt.Add(j, ascii);
                    }
                    yearlyAsciiArt.Add(i, monthAsciiArt);
                }
            }
            return yearlyAsciiArt;
        }

        public List<BetterText> GetBetterTextForMonth(int month, int year)
        {
            var stringBuilder = new StringBuilder();
            DateTime firstDay = new DateTime(year, month, 1);
            int numberOfDays = DateTime.DaysInMonth(year, month);

            List<DateTime> dates = new List<DateTime>();
            foreach (var day in Enumerable.Range(1, numberOfDays))
            {
                dates.Add(new DateTime(year, month, day));
            }

            string monthNameAndYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";
            stringBuilder.AppendLine(monthNameAndYear.PadLeft((20 + monthNameAndYear.Length)/2).PadRight(20));
            stringBuilder.AppendLine("Su Mo Tu We Th Fr Sa");

            List<BetterText> betterTextList = new List<BetterText>();

            foreach (var line in stringBuilder.ToString().Split('\n'))
            {
                BetterText betterText = new BetterText(line);
                betterTextList.Add(betterText);
            }

            int Offset = (int)firstDay.DayOfWeek % 7;
            int collum = 0;

            var firstline = new StringBuilder();

            for (int i = 0; i < Offset; i++)
            {

                firstline.Append("   ");
                collum++;
            }

            List<Action> functions = new List<Action>();

            for (int day = 1; day <= numberOfDays; day++)
            {
                firstline.Append($"[{day,2} ]");
                collum++;
                Action action = () => SelectedDate = new DateTime(year, month, day);
                functions.Add(action);
                if (collum == 7)
                {
                    BetterText betterText = new BetterText(firstline.ToString(), functions);
                    betterTextList.Add(betterText);
                    firstline = new StringBuilder();
                    collum = 0;
                }
            }

            if (collum != 0)
            {
                stringBuilder.AppendLine();
            }
            string arrowkeys = " [<]     [>]";
            stringBuilder.AppendLine(arrowkeys.PadLeft((20 + arrowkeys.Length) / 2).PadRight(20));


            return betterTextList;
        }
    }
}
