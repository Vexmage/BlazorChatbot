// Services/IPersonaService.cs
using BlazorChatbot.Models;

namespace BlazorChatbot.Services;

public interface IPersonaService
{
    IReadOnlyList<Persona> GetPersonas();
    Persona? GetDefaultPersona();
    Persona? GetPersonaByPrompt(string? systemPrompt);
}