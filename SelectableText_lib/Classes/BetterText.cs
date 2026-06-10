using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SelectableText_lib_namespace.Classes
{
    public class BetterText
    {
        private string text { get; set; }
        private string? altText { get; set; }
        private List<Action>? functions { get; set; }
        public bool isExecutable = false;
        public bool hasMultipleFunctions = false;
        // Dictionary that maps the selection index to the function index, this is used to determine which function to execute when a user selects a certain part of the text, the key is the selection index and the value is the function index
        // First intiger is the index of the starting characther of the selection, second intiger is the index of the function in the functions list that should be executed when that selection is selected and the last intiger is the the selection, this is used to determine how much of the text should be highlighted when that selection is selected
        public Dictionary<int, Tuple<int, int>>? selectionIndex { get; set; }

        //Most simpelst initialsation of the better text being just text
        public BetterText(string _text)
        {
            text = _text;
            functions = new List<Action>();

            //Initialize the selection index for each function
            selectionIndex = CalculateSelectionIndexes(_text);
        }

        //Better text with single function implementation
        public BetterText(string _text, Action _function)
        {
            text = _text;
            functions = new List<Action> { _function };
            isExecutable = true;

            //Initialize the selection index for each function
            selectionIndex = CalculateSelectionIndexes(_text);
        }

        //Better text with multiple functions implementation
        public BetterText(string _text, List<Action> _functions)
        {
            text = _text;
            functions = _functions;
            isExecutable = true;
            hasMultipleFunctions = true;

            //Initialize the selection index for each function
            selectionIndex = CalculateSelectionIndexes(_text);
        }

        //Better text that is updateable with single function implementation
        public BetterText(string _text, string _alttext, Action _function)
        {
            //See if the alternative text has the same amount of selectable parts as the original text, if not throw an exception as this would cause problems when trying to update the text
            if (CalculateSelectionIndexes(_text).Count != CalculateSelectionIndexes(_alttext).Count)
            {
                throw new ArgumentException("Both text and alttext must have the same number of selection indices.");
            }   

            text = _text;
            altText = _alttext;
            Action function = () =>
            {
                var temp = text;
                UpdateText(altText);
                altText = temp;
                _function.Invoke();
            };
            functions = new List<Action> { function };
            isExecutable = true;
            //Initialize the selection index for each function
            selectionIndex = CalculateSelectionIndexes(_text);
        }

        //Better text that is updateable with multiple functions implementation where any trigger updates the text
        public BetterText(string _text, string _alttext, List<Action> _functions)
        {
            //See if the alternative text has the same amount of selectable parts as the original text, if not throw an exception as this would cause problems when trying to update the text
            if (CalculateSelectionIndexes(_text).Count != CalculateSelectionIndexes(_alttext).Count)
            {
                throw new ArgumentException("Both text and alttext must have the same number of selection indices.");
            }
            text = _text;
            altText = _alttext;
            List<Action> functionsWithUpdate = new List<Action>();
            for (int i = 0; i < _functions.Count; i++)
            {
                int index = i; // Capture the current value of i for use in the lambda expression
                Action function = () =>
                {
                    var temp = text;
                    UpdateText(altText);
                    altText = temp;
                    _functions[index].Invoke();
                };
                functionsWithUpdate.Add(function);
            }
            functions = functionsWithUpdate;
            isExecutable = true;
            hasMultipleFunctions = true;
            //Initialize the selection index for each function
            selectionIndex = CalculateSelectionIndexes(_text);
        }

        //Better text that is updateable with multiple functions implementation where only the trigger updates the text
        public BetterText(string _text, string _alttext, List<Action> _functions, int UpdateFunction)
        {
            //See if the alternative text has the same amount of selectable parts as the original text, if not throw an exception as this would cause problems when trying to update the text
            if (CalculateSelectionIndexes(_text).Count != CalculateSelectionIndexes(_alttext).Count)
            {
                throw new ArgumentException("Both text and alttext must have the same number of selection indices.");
            }
            text = _text;
            altText = _alttext;
            List<Action> functionsWithUpdate = new List<Action>();
            for (int i = 0; i < _functions.Count; i++)
            {
                int index = i; // Capture the current value of i for use in the lambda expression
                Action function = () =>
                {
                    if (index == UpdateFunction)
                    {
                        var temp = text;
                        UpdateText(altText);
                        altText = temp;
                    }
                    _functions[index].Invoke();
                };
                functionsWithUpdate.Add(function);
            }
            functions = functionsWithUpdate;
            isExecutable = true;
            hasMultipleFunctions = true;
            //Initialize the selection index for each function
            selectionIndex = CalculateSelectionIndexes(_text);
        }

        //Execute the single function if it exists
        public void Execute()
        {
            if (functions != null)
            {
                functions[0]?.Invoke();
            }
            else //Should never happen since SelectableText shoud only call this if there is a function assigned
            {
                throw new InvalidOperationException("No function assigned to execute.");
            }
        }

        //Execute the function at the specified index if it exists
        public void Execute(int index)
        {
            if (functions != null && index >= 0 && index < functions.Count)
            {
                functions[index].Invoke();
            }
            else
            {
                throw new IndexOutOfRangeException("Function index is out of range.");
            }
        }

        public void ExecuteOnSelectionIndex(int index)
        {
            if (selectionIndex != null && selectionIndex.ContainsKey(index))
            {
                int functionIndex = selectionIndex[index].Item1;
                Execute(functionIndex);
            }
            else
            {
                throw new IndexOutOfRangeException("Selection index is out of range.");
            }
        }

        public void UpdateText(string newText)
        {
            //Validation for the text indicies is valadated upon creation of the object and does not need to be handeld by the function itself
            if (newText == null)
            {
                throw new ArgumentNullException(nameof(newText), "New text cannot be null.");
            }
            if (hasMultipleFunctions)
            {
                selectionIndex = CalculateSelectionIndexes(newText);
                text = newText;
            }
            else if (!hasMultipleFunctions)
            {
                text = newText;
            }
            else
            {
                throw new InvalidOperationException("Cannot update text for this BetterText instance.");
            }
        }

        public Dictionary<int, Tuple<int, int>> CalculateSelectionIndexes(string text)
        {
            //Local variable to store the selection index mapping
            Dictionary<int, Tuple<int, int>> _selectionIndex = new Dictionary<int, Tuple<int, int>>();
            //Initialize the selection index for each function
            List<char> splitText = text.ToList();
            int j = 0;
            bool foundStart = false;
            int startIndex = 0;
            int lenght = 0;
            foreach (char c in splitText)
            {
                if (c == '[' && !foundStart)
                {
                    foundStart = true;
                    startIndex = j;
                }
                else if (c == ']' && foundStart)
                {
                    lenght = j - startIndex;
                    _selectionIndex.Add(_selectionIndex.Count, new Tuple<int, int> (startIndex, lenght));
                    foundStart = false;
                }
                j++;
            }
            return _selectionIndex;

        }

        public string GetText()
        {
            return text;
        }
    }
}