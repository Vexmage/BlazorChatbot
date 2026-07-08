// Models/ChatMessage.cs
namespace BlazorChatbot.Models;

public class ChatMessage
{
    public string Role { get; set; }
    public string Content { get; set; }
    public string? PersonaImagePath { get; set; }
    public string? PersonaName { get; set; }

    public ChatMessage(string role, string content, string? imagePath = null, string? personaName = null)
    {
        Role = role;
        Content = content;
        PersonaImagePath = imagePath;
        PersonaName = personaName;
    }
}