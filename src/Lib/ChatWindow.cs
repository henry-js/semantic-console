using Terminal.Gui;

namespace semantic_console.Lib;

public partial class ChatWindow
{
    public ChatWindow()
    {
        InitializeComponent();

        inputArea.KeyDown += HandleInput;
    }

    private void HandleInput(KeyEventEventArgs args)
    {
        if (args.KeyEvent.Key != Key.Enter)
        {
            args.Handled = false;
            return;
        }
        args.Handled = true;
        var message = inputArea.Text;
        var time = TimeOnly.FromDateTime(DateTime.Now);

        messages.Text += $"{Environment.NewLine}You @ ({time}): {message}";
        inputArea.Text = "";
    }
}
