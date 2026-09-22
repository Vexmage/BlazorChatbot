namespace BlazorChatbot.Models;

public enum PhilosophicalRelevance
{
    Relevant,
    Transformable,
    Uncertain,
    Irrelevant
}

public sealed record RelevanceAssessment(
    PhilosophicalRelevance Classification,
    double Confidence,
    IReadOnlyList<string> DetectedDomains,
    string Reason,
    string? SuggestedReframing = null
);
