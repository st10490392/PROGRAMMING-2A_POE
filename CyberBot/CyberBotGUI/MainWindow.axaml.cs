using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CyberBotGUI.Services;

namespace CyberBotGUI;

public partial class MainWindow : Window
{
    private readonly ChatbotService _bot = new();

    public MainWindow()
    {
        InitializeComponent();
        this.FindControl<Button>("SendButton")?.Click += SendButton_Click;
        Opened += MainWindow_Opened;
    }

    private void MainWindow_Opened(object? sender, EventArgs e)
    {
        var chatBox = this.FindControl<TextBox>("ChatBox");
        if (chatBox != null)
        {
            chatBox.Text = "CyberBot: Hello! Type a question below and press Send.\n\n";
        }
    }

    private void SendButton_Click(object? sender, RoutedEventArgs e)
    {
        var userInput = this.FindControl<TextBox>("UserInput");
        var chatBox = this.FindControl<TextBox>("ChatBox");
        if (userInput == null || chatBox == null)
            return;

        var text = userInput.Text?.Trim();
        if (string.IsNullOrEmpty(text))
            return;

        chatBox.Text += $"You: {text}\n";
        chatBox.Text += $"CyberBot: {_bot.GetResponse(text)}\n\n";
        userInput.Text = string.Empty;
    }
}