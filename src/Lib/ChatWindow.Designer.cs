using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace semantic_console.Lib;

public partial class ChatWindow : Window
{
    // private TextField inputView;
    private TextView inputArea;
    private FrameView messageViewFrame;
    private TextView messages;

    public KeyEvent HandleEnterPressed { get; private set; }

    private void InitializeComponent()
    {
        Title = "Example App (Ctrl+Q to quit)";

        // Create input components and labels
        var inputFrame = new FrameView("InputArea")
        {
            Y = Pos.AnchorEnd() - 5,
            Width = Dim.Fill(),
            Height = 5
        };
        inputArea = new TextView()
        {
            // Text = "Input",
            // Position text field adjacent to the label

            // Fill remaining horizontal space
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };
        inputFrame.Add(inputArea);


        messageViewFrame = new FrameView("Messages")
        {
            Text = "Username:",
            Width = Dim.Fill(),
            Height = Dim.Percent(100) - (inputFrame.Height),
        };
        messages = new TextView()
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ReadOnly = true,
        };

        messageViewFrame.Add(messages);
        // Add the views to the Window
        Add(inputFrame, messageViewFrame);
    }

}