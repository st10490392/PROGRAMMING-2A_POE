 using System;
using System.Collections.Generic;

namespace CyberBotGUI.Services;

public class ChatbotService
{
    private Random random = new Random();

    private string lastTopic = "";

    private string favouriteTopic = "";

    private Dictionary<string, List<string>> responses =
        new Dictionary<string, List<string>>()
    {
        {
            "password",
            new List<string>()
            {
                "Use strong unique passwords.",
                "Avoid using personal details in passwords.",
                "Use a password manager."
            }
        },

        {
            "phishing",
            new List<string>()
            {
                "Never click suspicious links.",
                "Check sender email addresses carefully.",
                "Phishing scams often create urgency."
            }
        },

        {
            "privacy",
            new List<string>()
            {
                "Review privacy settings regularly.",
                "Avoid oversharing online.",
                "Enable two-factor authentication."
            }
        },

        {
            "malware",
            new List<string>()
            {
                "Keep your software updated to prevent malware infections.",
                "Don't open attachments from unknown senders.",
                "Use reputable antivirus tools and scan regularly."
            }
        },

        {
            "vpn",
            new List<string>()
            {
                "VPNs help protect your online privacy.",
                "Avoid free VPNs you do not trust.",
                "VPNs encrypt your internet traffic."
            }
        },

        {
            "scams",
            new List<string>()
            {
                "If something sounds too good to be true, it probably is.",
                "Verify requests before sharing sensitive information.",
                "Scammers often create a false sense of urgency."
            }
        },

        {
            "social engineering",
            new List<string>()
            {
                "Social engineers use psychology to trick people.",
                "Always verify the identity of anyone requesting access.",
                "Be cautious when someone asks for confidential data."
            }
        }
    };

    public string GetResponse(string input)
    {
        input = input.ToLower();

        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please enter a message.";
        }

        // Sentiment detection
        if (input.Contains("worried"))
        {
            return "It's understandable to feel worried. Scammers can be convincing.";
        }

        if (input.Contains("frustrated"))
        {
            return "Cybersecurity can feel overwhelming, but small steps help.";
        }

        if (input.Contains("curious"))
        {
            return "That's great! Learning cybersecurity is important.";
        }

        if (input.Contains("scared"))
        {
            return "I understand feeling scared — focus on one small security habit at a time.";
        }

        if (input.Contains("confused"))
        {
            return "If you're confused, take a deep breath and ask for a simpler explanation.";
        }

        if (input.Contains("excited"))
        {
            return "That's awesome! Cybersecurity is an exciting skill to learn.";
        }

        // Memory
        if (input.Contains("i like"))
        {
            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    favouriteTopic = keyword;
                    return $"Great! I'll remember that you're interested in {keyword}.";
                }
            }
        }

        if (input.Contains("i like privacy"))
        {
            favouriteTopic = "privacy";
            return "Great! I'll remember that you're interested in privacy.";
        }

        // Conversation flow
        if (input.Contains("tell me more") ||
            input.Contains("another tip"))
        {
            if (lastTopic != "")
            {
                return GetRandomResponse(lastTopic);
            }

            return "Tell me which topic you'd like to hear more about first.";
        }

        // Keyword recognition
        foreach (var keyword in responses.Keys)
        {
            if (input.Contains(keyword))
            {
                lastTopic = keyword;
                return GetRandomResponse(keyword);
            }
        }

        if (favouriteTopic != "")
        {
            return $"Since you're interested in {favouriteTopic}, remember to stay safe online.";
        }

        return "I didn't quite understand that. Could you rephrase?";
    }

    private string GetRandomResponse(string topic)
    {
        var list = responses[topic];

        int index = random.Next(list.Count);

        return list[index];
    }
}