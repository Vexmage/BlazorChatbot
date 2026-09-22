using BlazorChatbot.Models;

namespace BlazorChatbot.Services;

public interface IPhilosophicalRelevanceService
{
    RelevanceAssessment Assess(string prompt);
}
