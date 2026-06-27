# CyberBot

## Project Overview

This repository includes two connected cybersecurity chatbot implementations:

- **Part 1:** `CyberBot` console chatbot
- **Part 2:** `CyberBotGUI` Avalonia desktop chatbot

The console version is a legacy POE Part 1 implementation with voice greeting and ASCII art, while the GUI version is a newer desktop app with richer topic handling.

## Part 1 — Console Chatbot

### Features

- Voice greeting support (WAV)
- ASCII art startup display
- User name interaction
- Cybersecurity keyword responses
- Input handling with exit support

### Relevant files

- `CyberBot.csproj`
- `Chatbot.cs`

### Run the console bot

1. Ensure .NET 10 SDK is installed.
2. Restore packages:

```bash
dotnet restore CyberBot.csproj
```

3. Build the project:

```bash
dotnet build CyberBot.csproj
```

4. Run the app:

```bash
dotnet run --project CyberBot.csproj
```

## Part 2 — Avalonia GUI Chatbot

### Features

- Cross-platform Avalonia desktop UI
- Topic recognition for cybersecurity subjects
- Random topic-specific responses
- Memory for favorite topics
- Sentiment-aware responses for worried, frustrated, scared, confused, and excited messages
- Follow-up flow: "tell me more" / "another tip"

### Supported topics

- Passwords
- Phishing
- Privacy
- Malware
- VPNs
- Scams
- Social engineering

### Relevant files

- `CyberBotGUI.csproj`
- `App.axaml`
- `App.axaml.cs`
- `MainWindow.axaml`
- `MainWindow.axaml.cs`
- `Services/ChatbotService.cs`

### Run the GUI bot

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

## Part 3 — GUI Enhancements and Submission Notes

### Added enhancements

- Improved tabbed UI layout for Chat Bot, Task Manager, Cyber Quiz, and Activity Log
- Styled controls with a dark cybersecurity theme
- Added a read-only chat history box and user input/message send flow
- Included task management UI for adding and deleting tasks
- Added quiz question/answer flow with score display
- Added activity log area for future event tracking

### Notes

- The current project structure supports the full GUI application workflow.
- The GUI app is launched from `CyberBot/CyberBotGUI`.
- Further wiring of the chat service and final bug fixes are pending.

### Run instructions

```bash
cd /home/gingercodephantom/PROGRAMMING-2A_POE/CyberBot/CyberBotGUI
dotnet clean
dotnet run
```

## Technologies Used

- .NET 8 / .NET 10
- Avalonia UI 11
- C#

Part3 commit marker 1 - 2026-06-27T21:14:35+02:00

Part3 commit marker 2 - 2026-06-27T21:14:35+02:00

Part3 commit marker 3 - 2026-06-27T21:14:35+02:00

Part3 commit marker 4 - 2026-06-27T21:14:35+02:00
