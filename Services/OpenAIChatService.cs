using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

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

        public async Task<string> SendMessageAsync(string message)
        {
            var payload = new
            {
                model = _options.Model,
                messages = new[]
                {
                    new { role = "system", content = "You are a philosophy professor grounded in postcolonial theory and indigenous knowledge systems. You challenge Eurocentric assumptions and explore relational, land-based epistemologies." },
                    new { role = "user", content = message }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _options.Endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var reply = doc.RootElement
                           .GetProperty("choices")[0]
                           .GetProperty("message")
                           .GetProperty("content")
                           .GetString();

            return reply ?? "[No response received]";
        }
    }
}
