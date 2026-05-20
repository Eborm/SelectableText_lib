# SelectableText_lib

A C# library for enabling selectable text that runs functions upon selection.

## Overview

**SelectableText_lib** enables you to easily create interactive text menus. You define selectable (and optionally non-selectable) text segments, assign functions to them, and display them to the user—ideal for building menus and interactive console experiences.

## Example: Using the Library (`showcase_proj`)

The `showcase_proj` directory contains a sample console project showcasing different usage scenarios, such as simple menus, keyword-driven menus, advanced multi-action menus, and a calendar picker.

## Shorter menu's

Using the library you can make a 4 choice + exit menu which is around 53 lines (using a switch case to sort the input). To around 15 lines using the library. (The numbers where directly pulled from the `showcase_proj` comparing the main function to the SimpleMenu function).

### Sample Code: Creating a Keyword Menu

```csharp
using SelectableText_lib_namespace;

static void KeyWordMenu()
{
    // Initialize the menu with optional animation and color customization
    SelectableText_lib st = new SelectableText_lib(true, ConsoleColor.Black, ConsoleColor.White);

    // Add selectable and non-selectable text items
    st.AddText("test", "Test [yes]", myfunction);      // Selectable: runs myfunction
    st.AddText("test2", "Test 2 [no]", myfunction2);   // Selectable: runs myfunction2
    st.AddText("sometext", "some text");               // Non-selectable
    st.AddText("sometext2", "some text2");             // Non-selectable

    // Define the order in which menu items appear
    List<string> menu = new List<string> { "test", "empty", "test2", "sometext", "sometext2" };
    st.SetShownText(menu);

    // Display the menu and handle user interaction
    while (true)
    {
        st.DisplayText(); // Handles user input and selection
    }
}

// Example callback functions
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
```

#### Other Examples in `test_proj/Program.cs`:

- **SimpleMenu** – demonstrates basic menu creation.
- **AdvancedMenu** – shows multi-action items.
- **CallenderExample** – integrates a calendar/date picker UI.

To run these examples, open the solution in Visual Studio and set `showcase_proj` as the startup project.

## Installation

Clone and add the library project or its files to your solution:

```bash
git clone https://github.com/Eborm/SelectableText_lib.git
```

Add a project reference to `SelectableText_lib_namespace`.

## Summary

- `AddText(string key, string displayText, Action handler)` — Add selectable item.
- `AddText(string key, string displayText)` — Add non-selectable item.
- `SetShownText(List<string> keys)` — Specify menu order and which items to display.
- `DisplayText()` — Shows menu, handles navigation and selection.

(See library source for advanced features, such as multiple actions per item.)

## License

MIT

---

_For more advanced usage and additional scenarios, review the full example in [`test_proj/Program.cs`](https://github.com/Eborm/SelectableText_lib/blob/main/test_proj/Program.cs)._
