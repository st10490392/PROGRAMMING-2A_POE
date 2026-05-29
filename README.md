# CyberBotGUI

## Project Overview

CyberBotGUI is a cross-platform desktop cybersecurity awareness chatbot built with Avalonia UI on .NET 8. It provides interactive guidance on common security topics and responds to user sentiment, memory, and follow-up requests.

## Features

- Keyword recognition for cybersecurity topics
- Random topic-specific responses
- Conversation flow with "tell me more" and "another tip"
- Memory support for favorite topics
- Sentiment detection for worried, frustrated, scared, confused, and excited messages
- Enhanced Linux-compatible GUI using Avalonia

## Supported Topics

- Passwords
- Phishing
- Privacy
- Malware
- VPNs
- Scams
- Social engineering

## How to Run

1. Ensure .NET 8 SDK is installed.
2. Restore packages:

```bash
dotnet restore CyberBotGUI.csproj
```

3. Build the project:

```bash
dotnet build CyberBotGUI.csproj
```

4. Run the app:

```bash
dotnet run --project CyberBotGUI.csproj
```

## Technologies Used

- .NET 8
- Avalonia UI 11
- C#
