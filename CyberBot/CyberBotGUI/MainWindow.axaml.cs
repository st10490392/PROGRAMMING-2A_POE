using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CyberBotGUI.Models;
using CyberBotGUI.Services;

namespace CyberBotGUI;

public partial class MainWindow : Window
{
    private readonly ChatbotService _bot = new();
    private readonly QuizService _quiz = new();
    private QuizQuestion? _currentQuestion;

    public MainWindow()
    {
        InitializeComponent();

        var sendButton = this.FindControl<Button>("SendButton");
        if (sendButton != null)
            sendButton.Click += SendButton_Click;

        var startQuizButton = this.FindControl<Button>("StartQuizButton");
        if (startQuizButton != null)
            startQuizButton.Click += StartQuiz_Click;

        var submitAnswerButton = this.FindControl<Button>("SubmitAnswerButton");
        if (submitAnswerButton != null)
            submitAnswerButton.Click += CheckAnswer_Click;

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

    private void StartQuiz_Click(object? sender, RoutedEventArgs e)
    {
        _quiz.ResetQuiz();
        _currentQuestion = _quiz.GetNextQuestion();
        var questionText = this.FindControl<TextBlock>("QuizQuestion");
        var scoreText = this.FindControl<TextBlock>("QuizScore");
        var answerBox = this.FindControl<TextBox>("QuizAnswer");

        if (_currentQuestion == null || questionText == null || scoreText == null || answerBox == null)
            return;

        questionText.Text = _currentQuestion.Question;
        scoreText.Text = _quiz.GetScoreText();
        answerBox.Text = string.Empty;
    }

    private void CheckAnswer_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentQuestion == null)
            return;

        var answerBox = this.FindControl<TextBox>("QuizAnswer");
        var questionText = this.FindControl<TextBlock>("QuizQuestion");
        var scoreText = this.FindControl<TextBlock>("QuizScore");
        var chatBox = this.FindControl<TextBox>("ChatBox");

        if (answerBox == null || questionText == null || scoreText == null || chatBox == null)
            return;

        var userAnswer = answerBox.Text?.Trim() ?? string.Empty;
        var correct = _quiz.CheckAnswer(_currentQuestion, userAnswer);

        chatBox.Text += $"Quiz question: {_currentQuestion.Question}\n";
        chatBox.Text += correct
            ? "CyberBot: Correct! Well done.\n\n"
            : $"CyberBot: Not quite. The answer is: {_currentQuestion.Answer}\n\n";

        _currentQuestion = _quiz.GetNextQuestion();
        if (_currentQuestion != null)
        {
            questionText.Text = _currentQuestion.Question;
        }
        else
        {
            questionText.Text = "Quiz complete! Click Start Quiz to play again.";
        }

        scoreText.Text = _quiz.GetScoreText();
        answerBox.Text = string.Empty;
    }
}