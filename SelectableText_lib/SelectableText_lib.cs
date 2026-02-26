using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using SelectableText_lib_namespace.Classes;

namespace SelectableText_lib_namespace
{
    public class SelectableText_lib
    {
        //console colors
        private System.ConsoleColor BackgroundColor;
        private System.ConsoleColor ForegroundColor;

        //setting up detection and sleepingbeauty classes
        private Detection _detection = new Detection();
        private ShortTimeOut _sb = new ShortTimeOut();
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
        private static Action VoidFunct() { return () => { }; }
        private Dictionary<int, Tuple<string, Action>> textDictonary = new Dictionary<int, Tuple<string, Action>> {};
        private Dictionary<string, int> textKeyDictonary = new Dictionary<string, int> { { "empty", 0 } };
        private int selectedText = -1;
        private int textCount = -1;
        private List<int> writeThisText = new List<int> {0};
        private List<int> selectableText = new List<int> {};


        public SelectableText_lib(System.ConsoleColor BackgroundColor = ConsoleColor.Black, System.ConsoleColor ForegroundColor = ConsoleColor.White)
        {
            this.AddText("");
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
            textDictonary[0] = new Tuple<string, Action> ("", VoidFunct());
            Console.Clear();
        }

        public SelectableText_lib(bool disable_startup_animation, System.ConsoleColor BackgroundColor = ConsoleColor.Black, System.ConsoleColor ForegroundColor = ConsoleColor.White)
        {
            this.AddText("");
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
            textDictonary[0] = new Tuple<string, Action>("", VoidFunct());
        }
    
        public void AddText(string text)
        {
            Action? functionName = null;
            if (functionName == null) functionName = VoidFunct();
            textCount++;
            textDictonary.Add(textCount, new Tuple<string, Action>(text, functionName));
        }
        
        public void AddText(string text, Action functionName)
        {
            if (functionName == null) functionName = VoidFunct();
            textCount++;
            textDictonary.Add(textCount, new Tuple<string, Action>(text, functionName));
        }


        public void AddText(string keyword, string text, Action functionName)
        {
            if (functionName == null) functionName = VoidFunct();
            textCount++;
            textKeyDictonary.Add(keyword, textCount);
            textDictonary.Add(textCount, new Tuple<string, Action>(text, functionName));
        }

        public void AddText(string keyword, string text)
        {
            Action? functionName = null;
            if (functionName == null) functionName = VoidFunct();
            textCount++;
            textKeyDictonary.Add(keyword, textCount);
            textDictonary.Add(textCount, new Tuple<string, Action>(text, functionName));
        }

        public void SetShownText(List<int> selectedText)
        {
            selectableText = new List<int> {};
            writeThisText = selectedText;
            if (writeThisText.Count == 0) { writeThisText.Add(0); }
            foreach ( int key in writeThisText)
            {
                if (textDictonary[key].Item2 != VoidFunct())
                {
                    selectableText.Add(key);
                }
            }
        }

        public void SetShownText(List<string> textkeys)
        {
            selectableText = new List<int> {};
            foreach (string key in textkeys)
            {
                if (textKeyDictonary.ContainsKey(key))
                {
                    writeThisText.Add(textKeyDictonary[key]);
                }
                else
                {
                    //Add in error handeling for missing keys
                }
            }
            foreach (int key in writeThisText)
            {
                if (textDictonary[key].Item2 != VoidFunct())
                {
                    selectableText.Add(key);
                }
            }
            if (writeThisText.Count == 0) { writeThisText.Add(0); }
        }

        public void DisplayText()
        {
            selectedText = Math.Clamp(selectedText, writeThisText[0], writeThisText[writeThisText.Count - 1]);
            Console.Clear();
            foreach (int text_key in writeThisText)
            {
                if (text_key == selectedText)
                {
                    WriteSelectedText(textDictonary[text_key].Item1);
                }
                else
                {
                    WriteText(textDictonary[text_key].Item1);
                }
            }
            int input = _detection.DetectUpDown();

            switch (input)
            {
                case -1:
                    try { selectedText = selectableText[selectableText.FindIndex(a => a == selectedText) - 1]; }
                    catch
                    {
                        try { selectedText = selectableText[selectableText.Count - 1]; }
                        catch { }
                    }
                    break;
                case 1:
                    try { selectedText = selectableText[selectableText.FindIndex(a => a == selectedText) + 1]; }
                    catch { 
                        try { selectedText = selectableText[0]; }
                        catch { }
                    }
                    break;
                case 2:
                    if (textDictonary[selectedText].Item2 != textDictonary[0].Item2)
                    {
                        textDictonary[selectedText].Item2?.Invoke();
                    }
                    break;
                default:
                    break;
            }
        }

        public void WriteText(string text)
        {
            List<char> splitText = text.ToList();
            foreach (char c in splitText)
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

        public void WriteSelectedText(string text)
        {
            List<char> splitText = text.ToList();
            if (string.Join("", splitText).Contains("["))
            {
                foreach (char c in splitText)
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
