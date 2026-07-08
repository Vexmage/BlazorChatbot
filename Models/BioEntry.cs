// Models/BioEntry.cs
namespace BlazorChatbot.Models;

public class BioEntry
{
    public string? Name { get; set; }
    public string? Tribe { get; set; }
    public string? Bio { get; set; }
    public List<string>? Aliases { get; set; }
}