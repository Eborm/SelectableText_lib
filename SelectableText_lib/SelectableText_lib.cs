using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace SelectableText_lib_namespace
{
    public class SelectableText_lib
    {
        //console colors
        private System.ConsoleColor BackgroundColor;
        private System.ConsoleColor ForegroundColor;

        //setting up detection and sleepingbeauty classes
        private detection _detection = new detection();
        private sleepingbeauty _sb = new sleepingbeauty();
        private double x = 0.002;

        //startup animation
        private static string startup =
@"          _____                _____                    _____            _____                    _____          
         /\    \              /\    \                  /\    \          /\    \                  /\    \         
        /::\    \            /::\    \                /::\____\        /::\    \                /::\    \        
       /::::\    \           \:::\    \              /:::/    /        \:::\    \              /::::\    \       
      /::::::\    \           \:::\    \            /:::/    /          \:::\    \            /::::::\    \      
     /:::/\:::\    \           \:::\    \          /:::/    /            \:::\    \          /:::/\:::\    \     
    /:::/__\:::\    \           \:::\    \        /:::/    /              \:::\    \        /:::/__\:::\    \    
    \:::\   \:::\    \          /::::\    \      /:::/    /               /::::\    \      /::::\   \:::\    \   
  ___\:::\   \:::\    \        /::::::\    \    /:::/    /       ____    /::::::\    \    /::::::\   \:::\    \  
 /\   \:::\   \:::\    \      /:::/\:::\    \  /:::/    /       /\   \  /:::/\:::\    \  /:::/\:::\   \:::\ ___\ 
/::\   \:::\   \:::\____\    /:::/  \:::\____\/:::/____/       /::\   \/:::/  \:::\____\/:::/__\:::\   \:::|    |
\:::\   \:::\   \::/    /   /:::/    \::/    /\:::\    \       \:::\  /:::/    \::/    /\:::\   \:::\  /:::|____|
 \:::\   \:::\   \/____/   /:::/    / \/____/  \:::\    \       \:::\/:::/    / \/____/  \:::\   \:::\/:::/    / 
  \:::\   \:::\    \      /:::/    /            \:::\    \       \::::::/    /            \:::\   \::::::/    /  
   \:::\   \:::\____\    /:::/    /              \:::\    \       \::::/____/              \:::\   \::::/    /   
    \:::\  /:::/    /    \::/    /                \:::\    \       \:::\    \               \:::\  /:::/    /    
     \:::\/:::/    /      \/____/                  \:::\    \       \:::\    \               \:::\/:::/    /     
      \::::::/    /                                 \:::\    \       \:::\    \               \::::::/    /      
       \::::/    /                                   \:::\____\       \:::\____\               \::::/    /       
        \::/    /                                     \::/    /        \::/    /                \::/    /        
         \/____/                                       \/____/          \/____/                  \/____/              
                                                                                                                 ";
        private List<char> bstlibtext = startup.ToList();

        //setting up the text functions
        private static Action voidfunct() { return () => { }; }
        private Dictionary<int, Tuple<string, Action>> _TextDictonary = new Dictionary<int, Tuple<string, Action>> {};
        private Dictionary<string, int> _TextKeyDictonary = new Dictionary<string, int> { { "empty", 0 } };
        private int selectedtext = -1;
        private int textcount = -1;
        private List<int> write_this_text = new List<int> {0};
        private List<int> selectable_text = new List<int> {};


        public SelectableText_lib(System.ConsoleColor BackgroundColor = ConsoleColor.Black, System.ConsoleColor ForegroundColor = ConsoleColor.White)
        {
            this.add_text("");
            this.BackgroundColor = BackgroundColor;
            this.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.SetWindowSize(10, 10);
            for (int i = 0; i < bstlibtext.Count; i++)
            {
                Console.Write(bstlibtext[i]);
                _sb.NOP(x);
            }
            _sb.NOP(0.5);
            _TextDictonary[0] = new Tuple<string, Action> ("", voidfunct());
            Console.Clear();
        }

        public SelectableText_lib(bool disable_startup_animation, System.ConsoleColor BackgroundColor = ConsoleColor.Black, System.ConsoleColor ForegroundColor = ConsoleColor.White)
        {
            this.add_text("");
            this.BackgroundColor = BackgroundColor;
            this.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            if (disable_startup_animation == false)
            {
                for (int i = 0; i < bstlibtext.Count; i++)
                {
                    Console.Write(bstlibtext[i]);
                    _sb.NOP(x);
                }
                _sb.NOP(0.5);
                Console.Clear();
            }
            _TextDictonary[0] = new Tuple<string, Action>("", voidfunct());
        }
    
        public void add_text(string text)
        {
            Action functionname = null;
            if (functionname == null) functionname = voidfunct();
            textcount++;
            _TextDictonary.Add(textcount, new Tuple<string, Action>(text, functionname));
        }
        
        public void add_text(string text, Action functionname)
        {
            if (functionname == null) functionname = voidfunct();
            textcount++;
            _TextDictonary.Add(textcount, new Tuple<string, Action>(text, functionname));
        }


        public void add_text(string keyword, string text, Action functionname)
        {
            if (functionname == null) functionname = voidfunct();
            textcount++;
            _TextKeyDictonary.Add(keyword, textcount);
            _TextDictonary.Add(textcount, new Tuple<string, Action>(text, functionname));
        }

        public void add_text(string keyword, string text)
        {
            Action functionname = null;
            if (functionname == null) functionname = voidfunct();
            textcount++;
            _TextKeyDictonary.Add(keyword, textcount);
            _TextDictonary.Add(textcount, new Tuple<string, Action>(text, functionname));
        }

        public void set_shown_text(List<int> textindices)
        {
            selectable_text = new List<int> {};
            write_this_text = textindices;
            if (write_this_text.Count == 0) { write_this_text.Add(0); }
            foreach ( int key in write_this_text)
            {
                if (_TextDictonary[key].Item2 != voidfunct())
                {
                    selectable_text.Add(key);
                }
            }
        }

        public void set_shown_text(List<string> textkeys)
        {
            selectable_text = new List<int> {};
            foreach (string key in textkeys)
            {
                if (_TextKeyDictonary.ContainsKey(key))
                {
                    write_this_text.Add(_TextKeyDictonary[key]);
                }
            }
            foreach (int key in write_this_text)
            {
                if (_TextDictonary[key] != null)
                {
                    selectable_text.Add(key);
                }
            }
            if (write_this_text.Count == 0) { write_this_text.Add(0); }
        }

        public void display_text()
        {
            selectedtext = Math.Clamp(selectedtext, write_this_text[0], write_this_text[write_this_text.Count - 1]);
            Console.Clear();
            foreach (int text_key in write_this_text)
            {
                if (text_key == selectedtext)
                {
                    write_selected_text(_TextDictonary[text_key].Item1);
                }
                else
                {
                    write_text(_TextDictonary[text_key].Item1);
                }
            }
            int input = _detection.detect_up_down();

            switch (input)
            {
                case -1:
                    try { selectedtext = selectable_text[selectable_text.FindIndex(a => a == selectedtext) - 1]; }
                    catch
                    {
                        try { selectedtext = selectable_text[selectable_text.Count - 1]; }
                        catch { }
                    }
                    break;
                case 1:
                    try { selectedtext = selectable_text[selectable_text.FindIndex(a => a == selectedtext) + 1]; }
                    catch { 
                        try { selectedtext = selectable_text[0]; }
                        catch { }
                    }
                    break;
                        }
                case 2:
                    if (_TextDictonary[selectedtext].Item2 != _TextDictonary[0].Item2)
                    {
                        _TextDictonary[selectedtext].Item2?.Invoke();
                    }
                    break;
                default:
                    break;
            }
        }

        public void write_text(string text)
        {
            List<char> splittext = text.ToList();
            foreach (char c in splittext)
            {
                if (c == '[')
                {
                    continue;
                }
                else if (c == ']') { continue; }
                else { Console.Write(c); }
            }
            Console.WriteLine();
        }

        public void write_selected_text(string text)
        {
            List<char> splittext = text.ToList();
            if (string.Join("", splittext).Contains("["))
            {
                foreach (char c in splittext)
                {
                    if (c == '[')
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (c == ']') { Console.ResetColor(); }
                    else
                    { Console.Write(c); }
                }
                Console.WriteLine();
            }
            else { Console.WriteLine(text);}
        }
    }
}
