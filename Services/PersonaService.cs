// Services/PersonaService.cs
using System.Text.Json;
using BlazorChatbot.Models;

namespace BlazorChatbot.Services;

public class PersonaService : IPersonaService
{
    private readonly List<Persona> _personas = new();

    public PersonaService()
    {
        LoadPersonas();
    }

    private void LoadPersonas()
    {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "App_Data",
            "personas.json");

        if (!File.Exists(path))
        {
            Console.WriteLine($"personas.json not found at: {path}");
            return;
        }

        var json = File.ReadAllText(path);

        var personas = JsonSerializer.Deserialize<List<Persona>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (personas is not null)
        {
            _personas.AddRange(personas);
        }
    }

    public IReadOnlyList<Persona> GetPersonas() => _personas;

    public Persona? GetDefaultPersona() => _personas.FirstOrDefault();

    public Persona? GetPersonaByPrompt(string? systemPrompt)
    {
        if (string.IsNullOrWhiteSpace(systemPrompt))
            return null;

        return _personas.FirstOrDefault(p => p.SystemPrompt == systemPrompt);
    }
}