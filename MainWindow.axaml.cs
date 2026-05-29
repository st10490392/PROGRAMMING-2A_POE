 using Avalonia.Controls;
using Avalonia.Interactivity;
using CyberBotGUI.Services;

namespace CyberBotGUI;

public partial class MainWindow : Window
{
    private ChatbotService chatbot = new ChatbotService();

    public MainWindow()
    {
        InitializeComponent();

        ChatBox.Text =
@"========================================
🛡️  CYBERSECURITY AWARENESS BOT
========================================

Welcome to the Cybersecurity Bot!

Topics:
- Passwords
- Privacy
- Phishing
- Malware
- VPNs
- Scams
- Social engineering

Type a topic above to get started.
";
    }

    private void SendButton_Click(object? sender, RoutedEventArgs e)
    {
        string userMessage = UserInput.Text ?? "";

        ChatBox.Text += $"You: {userMessage}\n";

        string response = chatbot.GetResponse(userMessage);

        ChatBox.Text += $"Bot: {response}\n\n";

        UserInput.Text = "";
    }
}