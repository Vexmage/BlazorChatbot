# 🦉 Philosobot

Philosobot is a Blazor Server chatbot that responds to your questions with philosophical nuance, distinct voice, and a touch of mystery. Built on top of OpenAI’s GPT API, it showcases how modern .NET and prompt engineering can work together to create conversational experiences.


## 🧠 Features

- ⚙️ Built with **Blazor Server (.NET 7)** for fast, interactive UI
- 🗣️ **Persona-based chat** powered by OpenAI's Chat Completions API
- 🎭 Multiple philosophical styles: analytic, continental, postcolonial, and more
- 🎨 Branded UI with custom owl + brain logo (aka the Philosobird™)
- 🧩 Fully extendable system for prompt tuning, theming, and future memory support


## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- An [OpenAI API key](https://platform.openai.com/account/api-keys)

### Setup

1. Clone the repo:

```bash
git clone https://github.com/YOUR_USERNAME/BlazorChatbot.git
cd BlazorChatbot

2. Add your OpenAI API key in appsettings.json:
"OpenAI": {
  "ApiKey": "sk-REPLACE_ME",
  "Endpoint": "https://api.openai.com/v1/chat/completions",
  "Model": "gpt-3.5-turbo"
}

3. Run the project:
dotnet run

Navigate to https://localhost:5001/chat to start chatting!

🧩 Folder Structure
BlazorChatbot/
│
├── Pages/                # Blazor page components (e.g., Chat.razor)
├── Services/             # OpenAIChatService lives here
├── wwwroot/css/site.css  # Styling for chat UI
├── appsettings.json      # Config for OpenAI API
└── Program.cs            # Service wiring and app startup

📸 Screenshots

(coming soon — add demo shots here later)
