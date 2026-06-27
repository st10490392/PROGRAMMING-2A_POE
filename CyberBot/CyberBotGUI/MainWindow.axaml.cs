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

    private TextBox? _chatBox;
    private TextBox? _userInput;
    private TextBox? _quizAnswer;
    private TextBlock? _quizQuestion;
    private TextBlock? _quizScore;

    public MainWindow()
    {
        InitializeComponent();
        Opened += MainWindow_Opened;
    }

    private void MainWindow_Opened(object? sender, EventArgs e)
    {
        _chatBox = this.FindControl<TextBox>("ChatBox");
        _userInput = this.FindControl<TextBox>("UserInput");
        _quizAnswer = this.FindControl<TextBox>("QuizAnswer");
        _quizQuestion = this.FindControl<TextBlock>("QuizQuestion");
        _quizScore = this.FindControl<TextBlock>("QuizScore");

        var sendButton = this.FindControl<Button>("SendButton");
        if (sendButton != null)
            sendButton.Click += SendButton_Click;

        var startQuizButton = this.FindControl<Button>("StartQuizButton");
        if (startQuizButton != null)
            startQuizButton.Click += StartQuiz_Click;

        var submitAnswerButton = this.FindControl<Button>("SubmitAnswerButton");
        if (submitAnswerButton != null)
            submitAnswerButton.Click += CheckAnswer_Click;

        if (_chatBox != null)
            _chatBox.Text = "CyberBot: Hello! Type a question and press Send.\n\n";
    }

    private void SendButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_userInput == null || _chatBox == null)
            return;

        var text = _userInput.Text?.Trim();
        if (string.IsNullOrEmpty(text))
            return;

        _chatBox.Text += $"You: {text}\n";
        _chatBox.Text += $"CyberBot: {_bot.GetResponse(text)}\n\n";
        _userInput.Text = string.Empty;
    }

    private void StartQuiz_Click(object? sender, RoutedEventArgs e)
    {
        _quiz.ResetQuiz();
        _currentQuestion = _quiz.GetNextQuestion();

        if (_quizQuestion != null && _quizScore != null && _quizAnswer != null)
        {
            if (_currentQuestion != null)
            {
                _quizQuestion.Text = _currentQuestion.Question;
                _quizScore.Text = _quiz.GetScoreText();
                _quizAnswer.Text = string.Empty;
            }
        }
    }

    private void CheckAnswer_Click(object? sender, RoutedEventArgs e)
    {
        if (_currentQuestion == null || _quizAnswer == null || _quizQuestion == null || _quizScore == null || _chatBox == null)
            return;

        var userAnswer = _quizAnswer.Text?.Trim() ?? string.Empty;
        var correct = _quiz.CheckAnswer(_currentQuestion, userAnswer);

        _chatBox.Text += $"Quiz: {_currentQuestion.Question}\n";
        _chatBox.Text += correct
            ? "CyberBot: Correct! Well done.\n\n"
            : $"CyberBot: Not quite. The answer is: {_currentQuestion.Answer}\n\n";

        _currentQuestion = _quiz.GetNextQuestion();
        _quizQuestion.Text = _currentQuestion?.Question ?? "Quiz complete! Click Start Quiz to play again.";
        _quizScore.Text = _quiz.GetScoreText();
        _quizAnswer.Text = string.Empty;
    }
}