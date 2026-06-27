using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using CyberBotGUI.Models;

namespace CyberBotGUI.Services;

public class QuizService
{
    private readonly string _dbPath = Path.Combine(AppContext.BaseDirectory, "quiz.db");
    private readonly List<QuizQuestion> _questions = new();
    private int _currentIndex;
    private int _correctCount;
    private int _attemptCount;

    public QuizService()
    {
        EnsureDatabase();
        LoadQuestions();
    }

    private void EnsureDatabase()
    {
        var folder = Path.GetDirectoryName(_dbPath);
        if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS QuizQuestions (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Question TEXT NOT NULL,
                Answer TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS QuizResults (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Correct INTEGER NOT NULL,
                Attempted INTEGER NOT NULL,
                Timestamp TEXT NOT NULL
            );";
        command.ExecuteNonQuery();
    }

    private void LoadQuestions()
    {
        _questions.Clear();

        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Question, Answer FROM QuizQuestions;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            _questions.Add(new QuizQuestion
            {
                Id = reader.GetInt32(0),
                Question = reader.GetString(1),
                Answer = reader.GetString(2)
            });
        }

        if (_questions.Count == 0)
        {
            SeedQuestions();
            LoadQuestions();
        }
    }

    private void SeedQuestions()
    {
        var seedQuestions = new[]
        {
            new QuizQuestion
            {
                Question = "What should you do with a suspicious email link?",
                Answer = "Do not click it and verify the sender"
            },
            new QuizQuestion
            {
                Question = "What is the strongest way to protect multiple accounts?",
                Answer = "Use unique, strong passwords and a password manager"
            },
            new QuizQuestion
            {
                Question = "What does VPN stand for?",
                Answer = "Virtual private network"
            },
            new QuizQuestion
            {
                Question = "What is phishing?",
                Answer = "A scam that tricks you into giving personal information"
            }
        };

        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        connection.Open();

        foreach (var question in seedQuestions)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO QuizQuestions (Question, Answer) VALUES ($question, $answer);";
            command.Parameters.AddWithValue("$question", question.Question);
            command.Parameters.AddWithValue("$answer", question.Answer);
            command.ExecuteNonQuery();
        }
    }

    public QuizQuestion? GetNextQuestion()
    {
        if (_questions.Count == 0)
            return null;

        if (_currentIndex >= _questions.Count)
            _currentIndex = 0;

        return _questions[_currentIndex++];
    }

    public bool CheckAnswer(QuizQuestion question, string userAnswer)
    {
        _attemptCount++;
        var isCorrect = string.Equals(question.Answer.Trim(), userAnswer.Trim(), StringComparison.OrdinalIgnoreCase);
        SaveResult(isCorrect);
        if (isCorrect)
            _correctCount++;
        return isCorrect;
    }

    private void SaveResult(bool correct)
    {
        using var connection = new SqliteConnection($"Data Source={_dbPath}");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO QuizResults (Correct, Attempted, Timestamp)
            VALUES ($correct, $attempted, $timestamp);";
        command.Parameters.AddWithValue("$correct", correct ? 1 : 0);
        command.Parameters.AddWithValue("$attempted", 1);
        command.Parameters.AddWithValue("$timestamp", DateTime.UtcNow.ToString("o"));
        command.ExecuteNonQuery();
    }

    public string GetScoreText()
    {
        return $"Score: {_correctCount}/{_attemptCount}";
    }

    public void ResetQuiz()
    {
        _currentIndex = 0;
        _correctCount = 0;
        _attemptCount = 0;
    }
}