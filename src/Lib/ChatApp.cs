using Terminal.Gui;

namespace semantic_console.Lib.Chat;

public class ChatApp //: IChatApp
{
    public void Start()
    {
        try
        {
            Application.Run<ChatWindow>();
        }
        finally
        {
            Application.Shutdown();
        }
    }
}

// public interface IChatApp
// {
//     void Start();
// }

// internal class ChatWindow : Window
// {
//     public ChatWindow()
//     {
//         Title = "Chat Window";
//         var messageView = new TextView()
//         {
//             Text = "Messages",
//             ReadOnly = true,
//             Width = Dim.Fill(),
//             Height = Dim.Fill(),
//         };
//         var inputTextBox = new TextView()
//         {
//             Text = "Input",
//             Width = Dim.Fill(),
//             Height = Dim.Sized(4),
//             Y = Pos.Bottom(messageView),
//         };
//         Add(inputTextBox, messageView);
//     }
// }