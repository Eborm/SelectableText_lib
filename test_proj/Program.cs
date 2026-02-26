using SelectableText_lib_namespace;
using static System.Net.Mime.MediaTypeNames;

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
                string input = Console.ReadLine();
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
                SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White);
                st.add_text("test", "Test [yes]", myfunction);
                st.add_text("test2", "Test 2 [no]", myfunction2);
                st.add_text("sometext", "some text");
                st.add_text("sometext2","some text2");
                st.set_shown_text(new List<string> {"test", "empty", "test2", "empty", "sometext", "sometext2"});
                while (true)
                {
                    st.display_text();
            }
        }

        static void SimpleMenu()
        {
            SelectableText_lib st = new SelectableText_lib(true ,ConsoleColor.Black, ConsoleColor.White);
            st.add_text("test", "Test [yes]", myfunction);
            st.add_text("test2", "Test 2 [no]", myfunction2);
            st.add_text("some text");
            st.add_text("some text2");
            st.set_shown_text(new List<int> {1, 0 ,2, 0, 3, 4});
            while (true)
            {
                st.display_text();
            }

        }

        public static void myfunction()
        {
            Console.Clear();
            Console.WriteLine("Test function");
            Console.ReadLine();
        }

        public static void myfunction2()
        {
            Console.Clear();
            Console.WriteLine("Test function 2");
            Console.ReadLine();
        }
    }
}
