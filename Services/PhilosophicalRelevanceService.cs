using BlazorChatbot.Models;

namespace BlazorChatbot.Services;

public class PhilosophicalRelevanceService
    : IPhilosophicalRelevanceService
{
    private static readonly string[] PhilosophicalTerms =
    {
        "ethics",
        "ethical",
        "morality",
        "moral",
        "justice",
        "knowledge",
        "truth",
        "consciousness",
        "meaning",
        "existence",
        "free will",
        "mind",
        "reality",
        "virtue",
        "cogito",
        "philosophy",
        "philosophical",
        "descartes",
        "plato",
        "aristotle",
        "kant",
        "nietzsche"
    };

    private static readonly string[] PhilosophicalPatterns =
    {
        "ought we",
        "should we",
        "can we know",
        "what does it mean",
        "is it moral",
        "is it ethical",
        "is it just",
        "what is the nature of",
        "what makes something"
    };

    public RelevanceAssessment Assess(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            return new RelevanceAssessment(
                PhilosophicalRelevance.Irrelevant,
                1.0,
                Array.Empty<string>(),
                "The prompt is empty.");
        }

        var normalized = prompt.Trim().ToLowerInvariant();

        var matchedTerms = PhilosophicalTerms
            .Where(normalized.Contains)
            .ToArray();

        var matchedPatterns = PhilosophicalPatterns
            .Where(normalized.Contains)
            .ToArray();

        if (matchedTerms.Length > 0 || matchedPatterns.Length > 0)
        {
            return new RelevanceAssessment(
                PhilosophicalRelevance.Relevant,
                0.8,
                matchedTerms,
                "The prompt contains philosophical concepts or question patterns.");
        }

        return new RelevanceAssessment(
            PhilosophicalRelevance.Uncertain,
            0.5,
            Array.Empty<string>(),
            "No clearly philosophical intent was detected.");
    }
}
