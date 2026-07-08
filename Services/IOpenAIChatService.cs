// Services/IOpenAIChatService.cs
using BlazorChatbot.Models;

namespace BlazorChatbot.Services;

public interface IOpenAIChatService
{
    Task<string> SendMessageAsync(IEnumerable<ChatMessage> messages);
}