// Models/Persona.cs
namespace BlazorChatbot.Models;

public class Persona
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string SystemPrompt { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
}