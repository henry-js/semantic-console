using Terminal.Gui;

namespace semantic_console.Lib.Chat;

public static class ChatApp //: IChatApp
{
    public static void Start()
    {
        Application.Init();

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

internal class ChatWindow : Window
{
}