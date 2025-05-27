using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using static BlazorChatbot.Pages.Chat;

namespace BlazorChatbot.Services
{
    public class OpenAIChatService
    {
        private readonly HttpClient _http;
        private readonly OpenAIOptions _options;

        public OpenAIChatService(HttpClient http, IOptions<OpenAIOptions> options)
        {
            _http = http;
            _options = options.Value;
        }

        public async Task<string> SendMessageAsync(IEnumerable<ChatMessage> messages)
        {
            var formattedMessages = messages.Select(m => new
            {
                role = m.Role.ToLower(),
                content = m.Content
            });

            var payload = new
            {
                model = _options.Model,
                messages = formattedMessages
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _options.Endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }

    }
}
