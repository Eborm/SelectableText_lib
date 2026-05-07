using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SelectableText_lib_namespace.Classes
{
    public class DatePicker
    {
        private DateTime SelectedDate = new DateTime();
        private int CurrentMonth = 1;
        private int CurrentYear = 1;
        public DatePicker()
        {
        }

        public DateTime DatePickerSingleDate(DateTime startDate, DateTime endDate)
        {
            //Get the month and year of the start and end date to be able to display the appropriate months in the calander for the user to select a date from
            var startMonth = startDate.Month;
            CurrentMonth = startMonth;
            var startYear = startDate.Year;
            CurrentYear = startYear;
            var endMonth = endDate.Month;
            var endYear = endDate.Year;

            //Check if the start date is before the end date and if they are in the same year to be able to display the appropriate months in the calander for the user to select a date from
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be before end date.");
            }
            while (SelectedDate == new DateTime())
            {
                //Initialize the SelectableText_lib to be able to display the calander for the user to select a date
                SelectableText_lib stlib = new SelectableText_lib(true);

                List<int> menu = new List<int> { };
                int counter = 1;
                foreach (var line in GetBetterTextForMonth(CurrentMonth, CurrentYear))
                {
                    stlib.AddText(line);
                    menu.Add(counter);
                    counter++;
                }

                stlib.SetShownText(menu);
                int month = CurrentMonth;
                while (month == CurrentMonth)
                {
                    stlib.DisplayText();
                    if (SelectedDate != new DateTime())
                    {
                        break;
                    }
                    if (CurrentMonth != month)
                    {
                        CurrentMonth = Math.Clamp(CurrentMonth, startMonth, endMonth);
                        if (CurrentYear < startYear)
                        {
                            CurrentYear = startYear;
                            CurrentMonth = startMonth;
                            break;
                        }
                        else if (CurrentYear > endYear)
                        {
                            CurrentYear = endYear;
                            CurrentMonth = endMonth;
                        }
                        break;
                    }
                }
            }
            //Chache current selected date and reset it to be able to use the DatePicker again if needed
            var SelectedDateChache = SelectedDate;
            SelectedDate = new DateTime();
            return SelectedDateChache;
        }

        private List<BetterText> GetBetterTextForMonth(int month, int year)
        {
            //Get first day and the number of days in the month to be able to create the calander for the month
            DateTime firstDay = new DateTime(year, month, 1);
            int numberOfDays = DateTime.DaysInMonth(year, month);

            //Create the top part of the calander this includes the month and year centered and the days of the week
            var stringBuilder = new StringBuilder();

            string monthNameAndYear = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";
            stringBuilder.AppendLine(monthNameAndYear.PadLeft((20 + monthNameAndYear.Length) / 2).PadRight(20));
            stringBuilder.AppendLine("Su Mo Tu We Th Fr Sa");

            //Add top part of the calander to a list of BetterText objects so it can be displayed in the console
            List<BetterText> betterTextList = new List<BetterText>();

            foreach (var line in stringBuilder.ToString().Split('\n'))
            {
                BetterText betterText = new BetterText(line);
                betterTextList.Add(betterText);
            }


            //Create the first row of the calander
            //Calculate the offset of the first day of the month to know where to start adding the days in the calander
            int Offset = (int)firstDay.DayOfWeek % 7;
            int collum = 0;

            var firstline = new StringBuilder();

            for (int i = 0; i < Offset; i++)
            {

                firstline.Append("   ");
                collum++;
            }

            //Create the rest of the calander by adding the days to the first line and creating a new line when the collum reaches 7
            //Also add the appropriate functions to each day so when a day is selected the SelectedDate variable is updated to the selected date
            List<Action> functions = new List<Action>();

            for (int day = 1; day <= numberOfDays; day++)
            {
                firstline.Append($"[{day,2} ]");
                collum++;
                int tempday = day;
                Action action = () => SelectedDate = new DateTime(year, month, tempday);
                functions.Add(action);
                if (collum == 7)
                {
                    BetterText betterText = new BetterText(firstline.ToString(), functions);
                    betterTextList.Add(betterText);
                    firstline = new StringBuilder();
                    collum = 0;
                    functions = new List<Action> { };
                }
            }
            if (firstline.Length > 0)
            {
                BetterText betterText2 = new BetterText(firstline.ToString(), functions);
                betterTextList.Add(betterText2);
                firstline = new StringBuilder();
            }

            //Create the arrowkeys on the bottom of the calander and center them
            //These are used to change between different months
            string arrowkeys = " [<]     [>]";
            firstline.AppendLine(arrowkeys.PadLeft((20 + arrowkeys.Length) / 2).PadRight(20));

            //Handle the month changing when the left and right arrow keys are pressed
            Action left = () =>             {
                if (CurrentMonth == 1)
                {
                    CurrentMonth = 12;
                    CurrentYear--;
                }
                else
                {
                    CurrentMonth--;
                }
            };

            Action right = () =>             {
                if (CurrentMonth == 12)
                {
                    CurrentMonth = 1;
                    CurrentYear++;
                }
                else
                {
                    CurrentMonth++;
                }
            };

            //Add the arrow keys to the calander
            var betterText3 = new BetterText(firstline.ToString(), new List<Action> { left, right });
            betterTextList.Add(betterText3);

            //Return the list of BetterText objects for the given month
            return betterTextList;
        }
    }
}
