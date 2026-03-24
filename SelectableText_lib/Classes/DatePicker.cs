using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SelectableText_lib_namespace.Classes
{
    public class DatePicker
    {
        public DatePicker()
        {
            
        }
        public Dictionary<int, string> GetFullAsciiArtSameYear(DateTime startDate, DateTime endDate)
        {
            //Devide infromation about what months should be added to the calander picking system so the appropriate months can be added
            var startMonth = startDate.Month;
            var startYear = startDate.Year;
            var endMonth = endDate.Month;
            var endYear = endDate.Year;

            //Create a dictonary to store the month ascii art and the corrosponing month number
            var monthAsciiArt = new Dictionary<int, string>();

            //Loop trough each month
            for (int i = startMonth; i < endMonth+1; i++)
            {
                //Get ascii art and add it to the dictionary
                string ascii = GetAsciiArtForMonth(i, startYear);
                monthAsciiArt.Add(i, ascii);
            }

            return monthAsciiArt;
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
                        string ascii = GetAsciiArtForMonth(j, i);
                        monthAsciiArt.Add(j, ascii);
                    }
                    yearlyAsciiArt.Add(i, monthAsciiArt);
                }
                else if (i == endYear)
                {
                    var monthAsciiArt = new Dictionary<int, string>();
                    for (int j = 1; j < endMonth+1; j++)
                    {
                        string ascii = GetAsciiArtForMonth(j, i);
                        monthAsciiArt.Add(j, ascii);
                    }
                    yearlyAsciiArt.Add(i, monthAsciiArt);
                }
                else
                {
                    var monthAsciiArt = new Dictionary<int, string>();
                    for (int j = 1; j < 13; j++)
                    {
                        string ascii = GetAsciiArtForMonth(j, i);
                        monthAsciiArt.Add(j, ascii);
                    }
                    yearlyAsciiArt.Add(i, monthAsciiArt);
                }
            }
            return yearlyAsciiArt;
        }

        public string GetAsciiArtForMonth(int month, int year)
        {
            var stringBuilder = new StringBuilder();
            DateTime firstDay = new DateTime(year, month, 1);
            int numberOfDays = DateTime.DaysInMonth(year, month);

            string monthNameAndYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";
            stringBuilder.AppendLine(monthNameAndYear.PadLeft((20 + monthNameAndYear.Length)/2).PadRight(20));
            stringBuilder.AppendLine("Su Mo Tu We Th Fr Sa");

            int Offset = (int)firstDay.DayOfWeek % 7;
            int collum = 0;

            for (int i = 0; i < Offset; i++)
            {
                stringBuilder.Append("   ");
                collum++;
            }

            for (int day = 1; day <= numberOfDays; day++)
            {
                stringBuilder.Append($"[{day,2} ]");
                collum++;
                if (collum == 7)
                {
                    stringBuilder.AppendLine();
                    collum = 0;
                }
            }

            if (collum != 0)
            {
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        public DateTime DateTimePicker(DateTime startDate, DateTime endDate)
        {
            SelectableText_libOLD st = new SelectableText_libOLD(true);
            //Throw error if start date is after end date as this is not valid input
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be before end date.");
            }
            else if (startDate.Year == endDate.Year)
            {
                var monthAsciiArt = GetFullAsciiArtSameYear(startDate, endDate);
                //Display ascii art and get user input to select month and day
                if (monthAsciiArt != null)
                {
                    foreach (var month in monthAsciiArt)
                    {

                    }
                }
            }
            return DateTime.Now;
        }
    }
}
