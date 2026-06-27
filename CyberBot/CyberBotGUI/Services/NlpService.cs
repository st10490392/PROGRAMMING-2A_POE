using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBotGUI.Services;

public class NlpService
{
    private readonly Dictionary<string, string[]> _topicKeywords = new()
    {
        ["password"] = new[] { "password", "pass", "credentials", "login", "account" },
        ["phishing"] = new[] { "phishing", "scam", "fraud", "fake email", "spoof" },
        ["privacy"] = new[] { "privacy", "personal data", "private", "data protection", "exposure" },
        ["malware"] = new[] { "malware", "virus", "trojan", "ransomware", "worm" },
        ["vpn"] = new[] { "vpn", "virtual private network", "secure network", "private network" },
        ["scam"] = new[] { "scam", "fraud", "con", "trick", "deceive" },
        ["social engineering"] = new[] { "social engineering", "manipulation", "psychological", "persuasion" }
    };

    private readonly Dictionary<string, string[]> _feelingKeywords = new()
    {
        ["worried"] = new[] { "worried", "anxious", "scared", "afraid", "nervous" },
        ["confused"] = new[] { "confused", "lost", "uncertain", "puzzled", "unsure" },
        ["excited"] = new[] { "excited", "happy", "good", "great", "awesome" }
    };

    public string? DetectTopic(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var normalized = input.Trim().ToLowerInvariant();
        foreach (var topic in _topicKeywords)
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

    public bool TryDetectFeeling(string input, out string feeling)
    {
        feeling = string.Empty;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var normalized = input.ToLowerInvariant();
        foreach (var pair in _feelingKeywords)
        {
            if (pair.Value.Any(keyword => normalized.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
            {
                feeling = pair.Key;
                return true;
            }
        }

        return false;
    }
}