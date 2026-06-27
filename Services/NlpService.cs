using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBotGUI.Services;

public class NlpService
{
    private readonly Dictionary<string, string[]> _synonyms = new()
    {
        ["password"] = new[] { "password", "pass", "credentials", "login" },
        ["phishing"] = new[] { "phishing", "scam", "fraud", "fake email" },
        ["privacy"] = new[] { "privacy", "personal data", "private", "data protection" },
        ["malware"] = new[] { "malware", "virus", "trojan", "ransomware" },
        ["vpn"] = new[] { "vpn", "virtual private network", "secure network" },
        ["scam"] = new[] { "scam", "fraud", "con", "trick" },
        ["social engineering"] = new[] { "social engineering", "manipulation", "psychological" }
    };

    public string? DetectTopic(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var normalized = input.Trim().ToLowerInvariant();
        foreach (var topic in _synonyms)
        {
            if (topic.Value.Any(keyword => normalized.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                return topic.Key;
        }

        return null;
    }

    public bool IsGreeting(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var normalized = input.ToLowerInvariant();
        return normalized.Contains("hello") || normalized.Contains("hi") || normalized.Contains("hey");
    }

    public bool IsFeeling(string input, out string feeling)
    {
        feeling = string.Empty;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var normalized = input.ToLowerInvariant();
        if (normalized.Contains("worried") || normalized.Contains("anxious") || normalized.Contains("scared"))
        {
            feeling = "worried";
            return true;
        }

        if (normalized.Contains("confused") || normalized.Contains("lost") || normalized.Contains("uncertain"))
        {
            feeling = "confused";
            return true;
        }

        if (normalized.Contains("excited") || normalized.Contains("happy") || normalized.Contains("good"))
        {
            feeling = "excited";
            return true;
        }

        return false;
    }
}