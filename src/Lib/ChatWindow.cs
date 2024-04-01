using System.Diagnostics;
using Terminal.Gui;

namespace semantic_console.Lib;

public partial class ChatWindow
{
    public ChatWindow()
    {
        InitializeComponent();
        inputArea.TextChanged += HandleTextChanged;
    }

    private void HandleTextChanged()
    {
        Debug.WriteLine(inputArea.Text);
    }
}
