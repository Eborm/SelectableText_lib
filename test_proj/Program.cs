using SelectableText_lib_namespace;
using static System.Net.Mime.MediaTypeNames;
using SelectableText_lib_namespace.Classes;

namespace test_proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int? choice = null;
            while (choice == null)
            {
                Console.Clear();
                Console.WriteLine("1. Simple menu");
                Console.WriteLine("2. Keyword menu");
                Console.WriteLine("3. Exit");
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
            }
            if (choice == 1)
            {
                SimpleMenu();
            }
            else if (choice == 2)
            {
                KeyWordMenu();
            }
        }

        static void KeyWordMenu()
        {
            SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White); //First variable is used to chose if you want the intro animation
            //Second variable is the background color of the text when it is not selected, third variable is the text color of the text
            //These will be inverted when the text is selected, so the background color will become the text color and the text color will become the background color
            st.AddText("test", "Test [yes]", myfunction); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("test2", "Test 2 [no]", myfunction2); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("sometext", "some text"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.AddText("sometext2","some text2"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.SetShownText(new List<string> {"test", "empty", "test2", "empty", "sometext", "sometext2"}); //list of items that you want displayed in the menu, the order they are in is the order they will be displayed in, you can repeat items as many times as you want,
            //empty simply displays a nothing as this is a empty string preadded into the dictionary
            while (true)
            {
                st.DisplayText(); //creates the menu and handles all the input and output for you, it will call the functions you have tied to the text when the user selects them and presses enter
            }
        }

        static void SimpleMenu()
        {
            SelectableText_lib st = new SelectableText_lib(true ,ConsoleColor.Black, ConsoleColor.White); //First variable is used to chose if you want the intro animation
            //Second variable is the background color of the text when it is not selected, third variable is the text color of the text
            st.AddText("test", "Test [yes]", myfunction); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("test2", "Test 2 [no]", myfunction2); //Bracketed text is the text that will be highlighted when the user has that row selected
            st.AddText("some text"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.AddText("some text2"); //This is just text that will be displayed but not selectable as it has no function tied to it
            st.SetShownText(new List<int> {1, 0 ,2, 0, 3, 4});  //list of items that you want displayed in the menu, the order they are in is the order they will be displayed in, you can repeat items as many times as you want,
            //0 simply displays a nothing as this is a empty string preadded into the dictionary
            while (true)
            {
                st.DisplayText(); //creates the menu and handles all the input and output for you, it will call the functions you have tied to the text when the user selects them and presses enter
            }

        }


        //Showcase function that clears the screen displays some text and waits for the user to press enter before returning to the menu
        public static void myfunction()
        {
            Console.Clear();
            Console.WriteLine("Test function");
            Console.ReadLine();
        }

        //Showcase function that clears the screen displays some text and waits for the user to press enter before returning to the menu
        public static void myfunction2()
        {
            Console.Clear();
            Console.WriteLine("Test function 2");
            Console.ReadLine();
        }
    }
}
