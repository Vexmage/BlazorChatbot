# BlazorChatbot

A Blazor Server chatbot application that uses the OpenAI API to generate responses in real time. Built as part of a developer workshop on integrating AI into .NET applications, this project showcases how modern .NET (Blazor Server) can be used alongside AI tools to create conversational, vibe-oriented web applications.

## 🧠 Features

- 🔌 .NET 8 / Blazor Server
- 🤖 Integration with OpenAI Chat Completions API (`gpt-3.5-turbo`)
- 💬 Live chat UI with user/AI message thread
- 🎨 Clean, responsive styling using basic CSS
- 🧪 Foundation for further exploration: personalities, memory, prompt tuning, etc.

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
