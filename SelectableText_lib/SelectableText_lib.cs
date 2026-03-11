using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Dictionary<int, BetterText> textDictonary = new Dictionary<int, BetterText> { };
        private Dictionary<string, int> textKeyDictonary = new Dictionary<string, int> { { "empty", 0 } };
        private Tuple<int, int> selectedText = new Tuple <int, int> (-1, -1);
        private int textCount = -1;
        private List<int> writeThisText = new List<int> { 0 };
        private int writeThisTextIndex = 0;
        private Dictionary<int, List<int>> selectableText = new Dictionary<int, List<int>> { };

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
            textDictonary[0] = new BetterText("");
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
            textDictonary[0] = new BetterText("");
        }

        public void AddText(string text)
        {
            textCount++;
            textDictonary.Add(textCount, new BetterText(text));
        }

        public void AddText(string text, Action function)
        {
            textCount++;
            textDictonary.Add(textCount, new BetterText(text, function));
        }

        public void AddText(string text, List<Action> functions)
        {
            textCount++;
            textDictonary.Add(textCount, new BetterText(text, functions));

        }

        public void AddText(string keyword, string text)
        {
            textCount++;
            textDictonary.Add(textCount, new BetterText(text));
            textKeyDictonary.Add(keyword, textCount);
        }

        public void AddText(string keyword, string text, Action function)
        {
            textCount++;
            textDictonary.Add(textCount, new BetterText(text, function));
            textKeyDictonary.Add(keyword, textCount);
        }

        public void AddText(string keyword, string text, List<Action> functions)
        {
            textCount++;
            textDictonary.Add(textCount, new BetterText(text, functions));
            textKeyDictonary.Add(keyword, textCount);
        }

        public void SetShownText(List<int> _selectedText)
        {
            selectableText = new Dictionary<int, List<int>> { };
            writeThisText = _selectedText;

            if (writeThisText.Count == 0) { writeThisText.Add(0); }
            foreach (int key in writeThisText)
            {
                if (textDictonary[key].isExecutable)
                {
                    List<int> temp = new List<int> { };
                    foreach (var selection in textDictonary[key].selectionIndex)
                    {
                        temp.Add(selection.Key);
                    }

                    selectableText.Add(key, temp);
                }
            }
            writeThisTextIndex = Math.Clamp(writeThisTextIndex - 1, 0, (writeThisText.Count - 1));
            selectedText = new Tuple<int, int>(selectedText.Item1, Math.Clamp(selectedText.Item2 - 1, 0, textDictonary[writeThisText[writeThisTextIndex]].selectionIndex.Count));
        }

        public void SetShownText(List<string> textkeys)
        {
            writeThisText = new List<int>{ };
            selectableText = new Dictionary<int, List<int>> { };
            foreach (string key in textkeys)
            {
                if (textKeyDictonary.ContainsKey(key))
                {
                    writeThisText.Add(textKeyDictonary[key]);
                }
                else
                {
                    throw new KeyNotFoundException($"The key '{key}' was not found in the textKeyDictonary.");
                }
            }
            foreach (int key in writeThisText)
            {
                if (textDictonary[key].isExecutable)
                {
                    List<int> temp = new List<int> { };
                    foreach (var selection in textDictonary[key].selectionIndex)
                    {
                        temp.Add(selection.Key);
                    }

                    selectableText.Add(key, temp);
                }
            }
            if (writeThisText.Count == 0) { writeThisText.Add(0); }
            writeThisTextIndex = Math.Clamp(writeThisTextIndex - 1, 0, (writeThisText.Count - 1));
            selectedText = new Tuple<int, int>(writeThisText[writeThisTextIndex], Math.Clamp(selectedText.Item2 - 1, 0, textDictonary[writeThisText[writeThisTextIndex]].selectionIndex.Count));
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

        public void WriteSelectedText(BetterText text)
        {
            List<char> splitText = text.GetText().ToList();
            if (string.Join("", splitText).Contains("["))
            {
                foreach (char c in splitText)
                {
                    if (c == '[')
                    {
                        Console.BackgroundColor = ForegroundColor;
                        Console.ForegroundColor = BackgroundColor;
                    }
                    else if (c == ']') { Console.ResetColor(); }
                    else
                    { Console.Write(c); }
                }
                Console.WriteLine();
            }
            else { Console.WriteLine(text); }
        }

        public void WriteSelectedText(BetterText text, int selectionIndex)
        {
            List<char> splitText = text.GetText().ToList();
            if (string.Join("", splitText).Contains("[") && text.selectionIndex != null && text.selectionIndex.ContainsKey(selectionIndex))
            {
                for (int i = 0; i < splitText.Count; i++)
                {
                    if (splitText[i] == '[' && text.selectionIndex[selectionIndex].Item1 == i)
                    {
                        Console.BackgroundColor = ForegroundColor;
                        Console.ForegroundColor = BackgroundColor;
                    }
                    else if (splitText[i] == ']' && text.selectionIndex[selectionIndex].Item2 == i - text.selectionIndex[selectionIndex].Item1)
                    {
                        Console.ResetColor();
                    }
                    else
                    {
                        var c = splitText[i].ToString();
                        if (c == "[" || c == "]")
                        {
                            continue;
                        }
                        Console.Write(splitText[i]);
                    }
                }
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
            else { Console.WriteLine(text); }
        }

        public void DisplayText()
        {
            Console.Clear();
            foreach (int text_key in writeThisText)
            {
                if (text_key == selectedText.Item1)
                {
                    if (textDictonary[text_key].hasMultipleFunctions)
                    {
                        WriteSelectedText(textDictonary[text_key], selectedText.Item2);
                    }
                    else
                    {
                        WriteSelectedText(textDictonary[text_key]);
                    }
                }
                else
                {
                    WriteText(textDictonary[text_key].GetText());
                }
            }
            var input = _detection.AdvancedDetection();

            switch (input)
            {
                case -1:
                    if (writeThisTextIndex == 0)
                    {
                        writeThisTextIndex = writeThisText.Count-1;
                    }
                    else
                    {
                        writeThisTextIndex = Math.Clamp(writeThisTextIndex - 1, 0, (writeThisText.Count - 1));
                    }
                    while (!selectableText.ContainsKey(writeThisText[writeThisTextIndex]))
                    {
                        if (writeThisTextIndex == 0)
                        {
                            writeThisTextIndex = writeThisText.Count-1;
                        }
                        else
                        {
                            writeThisTextIndex = Math.Clamp(writeThisTextIndex - 1, 0, (writeThisText.Count - 1));
                        }
                    }
                    selectedText = new Tuple<int, int>(writeThisText[writeThisTextIndex], Math.Clamp(selectedText.Item2 - 1, 0, textDictonary[writeThisText[writeThisTextIndex]].selectionIndex.Count));
                    break;
                case 1:
                    if (writeThisTextIndex == writeThisText.Count-1)
                    {
                        writeThisTextIndex = 0;
                    }
                    else
                    {
                        writeThisTextIndex = Math.Clamp(writeThisTextIndex + 1, 0, (writeThisText.Count - 1));
                    }
                    while (!selectableText.ContainsKey(writeThisText[writeThisTextIndex]))
                    {
                        if (writeThisText[writeThisTextIndex] == writeThisText[writeThisText.Count-1])
                        {
                            writeThisTextIndex = 0;
                        }
                        else
                        {
                            writeThisTextIndex = Math.Clamp(writeThisTextIndex + 1, 0, (writeThisText.Count - 1));
                        }
                    }
                    selectedText = new Tuple<int, int>(writeThisText[writeThisTextIndex], Math.Clamp(selectedText.Item2 - 1, 0, textDictonary[writeThisText[writeThisTextIndex]].selectionIndex.Count));
                    break;
                case -2:
                    selectedText = new Tuple<int, int>(selectedText.Item1, Math.Clamp(selectedText.Item2-1, 0, textDictonary[writeThisText[writeThisTextIndex]].selectionIndex.Count - 1));
                    break;
                case 2:
                    selectedText = new Tuple<int, int>(selectedText.Item1, Math.Clamp(selectedText.Item2+1, 0, textDictonary[writeThisText[writeThisTextIndex]].selectionIndex.Count - 1));
                    break;
                case 3:
                    if (selectedText.Item1 == -1 || selectedText.Item2 == -1)
                    {
                        break;
                    }
                    if (textDictonary[selectedText.Item1].isExecutable && !textDictonary[selectedText.Item1].hasMultipleFunctions)
                    {
                        Console.WriteLine(selectedText.Item2);
                        textDictonary[selectedText.Item1].Execute(0);
                    }
                    else if (textDictonary[selectedText.Item1].isExecutable && textDictonary[selectedText.Item1].hasMultipleFunctions)
                    {
                        textDictonary[selectedText.Item1].Execute(selectedText.Item2);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
