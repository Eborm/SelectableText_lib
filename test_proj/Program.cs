using SelectableText_lib_namespace;
using static System.Net.Mime.MediaTypeNames;
using SelectableText_lib_namespace.Classes;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace showcase_proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int? choice = null; //This is a example how a menu would be made without this library, this is just to show how much easier it is to make a menu with the library as you can see in the other functions
            while (choice == null)
            {
                Console.Clear();
                Console.WriteLine("1. Simple menu");
                Console.WriteLine("2. Keyword menu");
                Console.WriteLine("3. Advanced menu");
                Console.WriteLine("4. Update menu");
                Console.WriteLine("5. Calendar example");
                Console.WriteLine("6. Exit");
                string? input = Console.ReadLine();
                if (input == "1")
                {
                    choice = 1;
                }
                else if (input == "2")
                {
                    choice = 2;
                }
                else if (input == "3")
                {
                    choice = 3;
                }
                else if (input == "4")
                {
                    choice = 4;
                }
                else if (input == "5")
                {
                    choice = 5;
                }
                else if (input == "6")
                {
                    choice = 6;
                }
            }
            switch (choice)
            {
                case 1:
                    SimpleMenu();
                    break;
                case 2:
                    KeyWordMenu();
                    break;
                case 3:
                    AdvancedMenu();
                    break;
                case 4:
                    UpdateMenu();
                    break;
                case 5:
                    CallenderExample();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }            
        }

        static void KeyWordMenu()
        {
            var loop = true; //Setup loop variable that allows us to exit the menu when we want
            SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White); //First variable is used to chose if you want the intro animation
            //Second variable is the background color of the text when it is not selected, third variable is the text color of the text
            //These will be inverted when the text is selected, so the background color will become the text color and the text color will become the background color
            st.AddText("test", "Test [yes]", MyFunction); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("test2", "Test 2 [no]", MyFunction2); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("sometext", "some text"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.AddText("sometext2", "some text2"); //This is just text that will be displayed but not selectable as it has no function tied to it
            List<string> menu = new List<string> { "test", "empty", "test2", "sometext", "sometext2" };
            st.SetShownText(menu); //list of items that you want displayed in the menu, the order they are in is the order they will be displayed in, you can repeat items as many times as you want,
            //empty simply displays a nothing as this is a empty string preadded into the dictionary
            while (loop)  //Use loop variable to keep the menu running when the user presses escape it will be detected and the while loop will stop
            {
                st.DisplayText(ref loop); //creates the menu and handles all the input and output for you, it will call the functions you have tied to the text when the user selects them and presses enter
            }
        }

        static void SimpleMenu()
        {
            var loop = true; //Setup look variable that allows us to exit the menu when we want
            SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White); //First variable is used to chose if you want the intro animation
            //Second variable is the background color of the text when it is not selected, third variable is the text color of the text
            st.AddText("Test [yes]", MyFunction); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("Test 2 [no]", MyFunction2); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("some text"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.AddText("some text2"); //This is just text that will be displayed but not selectable as it has no function tied to it
            List<int> menu = new List<int> { 1, 0, 2, 3, 4 };
            st.SetShownText(menu);  //list of items that you want displayed in the menu, the order they are in is the order they will be displayed in, you can repeat items as many times as you want,
            //0 simply displays a nothing as this is a empty string preadded into the dictionary
            while (loop) //Use loop variable to keep the menu running when the user presses escape it will be detected and the while loop will stop
            {
                st.DisplayText(ref loop); //creates the menu and handles all the input and output for you, it will call the functions you have tied to the text when the user selects them and presses enter
            }
        }

        static void AdvancedMenu()
        {
            var loop = true; //Setup look variable that allows us to exit the menu when we want
            SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White); //First variable is used to chose if you want the intro animation
            //Second variable is the background color of the text when it is not selected, third variable is the text color of the text
            st.AddText("Test [yes] [no]", new List<Action> { MyFunction, MyFunction2 }); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("Test 2 [no] [yes]", new List<Action> { MyFunction2, MyFunction }); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("some text"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.AddText("some text2"); //This is just text that will be displayed but not selectable as it has no function tied to it
            List<int> menu = new List<int> { 1, 0, 2, 3, 4 };
            st.SetShownText(menu);  //list of items that you want displayed in the menu, the order they are in is the order they will be displayed in, you can repeat items as many times as you want,
            //0 simply displays a nothing as this is a empty string preadded into the dictionary
            while (loop) //Use loop variable to keep the menu running when the user presses escape it will be detected and the while loop will stop
            {
                st.DisplayText(ref loop); //creates the menu and handles all the input and output for you, it will call the functions you have tied to the text when the user selects them and presses enter
            }
        }

        static void UpdateMenu() //If you want to use keyword it would be the same as the keyword menu just add a second text after the first one and keep the rest the same
        {
            var loop = true; //Setup look variable that allows us to exit the menu when we want
            SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White); //First variable is used to chose if you want the intro animation
            //Second variable is the background color of the text when it is not selected, third variable is the text color of the text
            st.AddText("[O] Not Selected", MyFunction, "[X] Selected"); //Bracketed text is the text that will be highlighted when the user has that row selected, third parameter is the text that will be displayed when the user has that row selected instead of the normal text
            st.AddText("[O] Not Selected [O]", new List<Action> {MyFunction, MyFunction2}, "[X] Selected [X]");//Bracketed text is the text that will be highlighted when the user has that row selected, third parameter is the text that will be displayed when the user has that row selected instead of the normal text, this example also shows how you can have multiple functions tied to one text and it will call the functions in order of how they are in the list when the user presses enter
            st.AddText("[0] Not Selected [Run function]", new List<Action> { MyFunction, MyFunction2}, "[X] Selected [run]", 0); //Bracketed text is the text that will be highlighted when the user has that row selected, third parameter is the text that will be displayed when the user has that row selected instead of the normal text, this example also shows how you can have multiple functions tied to one text and it will call the functions in order of how they are in the list when the user presses enter, this also shows how you can have different text for when the item is selected and not selected
            List<int> menu = new List<int> { 1, 0, 2, 0, 3 };
            st.SetShownText(menu); //list of items that you want displayed in the menu, the order they are in is the order they will be displayed in, you can repeat items as many times as you want,
            //0 simply displays a nothing as this is a empty string preadded into the dictionary
            while (loop) //Use loop variable to keep the menu running when the user presses escape it will be detected and the while loop will stop
            {
                st.DisplayText(ref loop); //creates the menu and handles all the input and output for you, it will call the functions you have tied to the text when the user selects them and presses enter
            }
        }

        //Showcase function that clears the screen displays some text and waits for the user to press enter before returning to the menu
        public static void MyFunction()
        {
            Console.Clear();
            Console.WriteLine("Test function");
            Console.ReadLine();
        }

        //Showcase function that clears the screen displays some text and waits for the user to press enter before returning to the menu
        public static void MyFunction2()
        {
            Console.Clear();
            Console.WriteLine("Test function 2");
            Console.ReadLine();
        }
        static void CallenderExample()
        {
            //Create Datepicker object
            DatePicker dp = new DatePicker();

            //Create Datepicker and print the result, this will display a calendar that the user can navigate and select a date from, the parameters are the start date and end date of the calendar, the function will return the date that the user selected
            var date = dp.DatePickerSingleDate(new DateTime(2020, 1, 1), new DateTime(2024, 12, 31));
            Console.WriteLine(date);
        }
    }
}
