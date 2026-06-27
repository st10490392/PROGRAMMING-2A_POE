using System;
using System.Collections.Generic;

namespace CyberBotGUI.Services;

public class ChatbotService
{
    private readonly NlpService _nlp = new();
    private static readonly Random _random = new();

    private readonly Dictionary<string, string[]> _responses = new()
    {
        ["password"] = new[]
        {
            "Use a passphrase and a password manager to keep your passwords strong and unique.",
            "Avoid reusing passwords across multiple accounts.",
            "Enable multi-factor authentication whenever possible."
        },
        ["phishing"] = new[]
        {
            "Always verify the sender and never click links in suspicious emails.",
            "Phishing messages often ask you to act quickly or share sensitive data.",
            "If in doubt, go to the site directly instead of following email links."
        },
        ["privacy"] = new[]
        {
            "Review app permissions and limit what you share on social media.",
            "Use strong privacy settings on accounts and avoid oversharing.",
            "Be careful on public Wi-Fi and avoid sending sensitive data there."
        },
        ["malware"] = new[]
        {
            "Install updates and use antivirus software to reduce malware risk.",
            "Do not download attachments from unknown senders.",
            "Malware often comes from untrusted websites and fake installers."
        },
        ["vpn"] = new[]
        {
            "A VPN encrypts your traffic on public networks.",
            "Choose a reputable VPN provider with a clear privacy policy.",
            "Remember a VPN does not make you immune to phishing."
        },
        ["scam"] = new[]
        {
            "If something sounds too good to be true, it probably is.",
            "Never send money or credentials to people you don't know.",
            "Scammers often pretend to be tech support or banks."
        },
        ["social engineering"] = new[]
        {
            "Social engineering tricks you into revealing sensitive information.",
            "Always verify identity before acting on unusual requests.",
            "Treat unsolicited calls, texts, or emails with suspicion."
        }
    };

    private readonly string[] _defaultResponses = new[]
    {
        "I can help with passwords, phishing, privacy, malware, VPNs, scams, and social engineering.",
        "Ask me a cybersecurity question and I will give you a helpful tip.",
        "Stay safe online by using strong passwords and being cautious with links."
    };

    public string GetResponse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Please type a cybersecurity question.";

        if (_nlp.IsGreeting(input))
            return "Hello! Ask me about cybersecurity and I will help.";

        if (_nlp.TryDetectFeeling(input, out var feeling))
        {
            return feeling switch
            {
                "worried" => "It’s normal to feel worried. I can help you understand risks and stay safe.",
                "confused" => "I can explain cybersecurity topics step by step. What would you like to know?",
                "excited" => "Great! Let’s turn that energy into safer online habits.",
                _ => "I’m here to help you with cybersecurity advice."
            };
        }

        var topic = _nlp.DetectTopic(input);
        if (topic != null && _responses.TryGetValue(topic, out var options))
        {
            return options[_random.Next(options.Length)];
        }

        return _defaultResponses[_random.Next(_defaultResponses.Length)];
    }
}