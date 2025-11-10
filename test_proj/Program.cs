using SelectableText_lib_namespace;

namespace test_proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SelectableText_lib st = new SelectableText_lib(false ,ConsoleColor.Black, ConsoleColor.White);
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
