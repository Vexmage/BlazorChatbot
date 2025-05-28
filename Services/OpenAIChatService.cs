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
        private Dictionary<string, BioEntry> _bios = new();

        public OpenAIChatService(HttpClient http, IOptions<OpenAIOptions> options)
        {
            _http = http;
            _options = options.Value;
        }

        private async Task LoadBiosAsync()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "bios.json");

            if (!File.Exists(path))
            {
                Console.WriteLine($"❌ bios.json not found at: {path}");
                return;
            }

            try
            {
                var json = await File.ReadAllTextAsync(path);
                _bios = JsonSerializer.Deserialize<Dictionary<string, BioEntry>>(json)
                        ?? new Dictionary<string, BioEntry>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"❌ Error parsing bios.json: {ex.Message}");
            }

            Console.WriteLine($"✅ Loaded {_bios.Count} bios.");
            foreach (var kv in _bios)
            {
                var name = kv.Value?.Name ?? "❌ NULL";
                Console.WriteLine($"– Key: {kv.Key}, Name: {name}");
            }
        }

        public class BioEntry
        {
            public string? Name { get; set; }
            public string? Tribe { get; set; }
            public string? Bio { get; set; }
            public List<string>? Aliases { get; set; }
        }

        private string? FindMatchingBio(string? userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("⚠️ No user input provided to bio matcher.");
                return null;
            }

            string normalizedInput = Normalize(userInput);

            foreach (var entry in _bios.Values)
            {
                if (string.IsNullOrWhiteSpace(entry.Name))
                    continue;

                var namesToCheck = new List<string> { entry.Name };
                if (entry.Aliases != null)
                {
                    namesToCheck.AddRange(entry.Aliases.Where(alias => !string.IsNullOrWhiteSpace(alias)));
                }

                foreach (var name in namesToCheck)
                {
                    if (normalizedInput.Contains(Normalize(name)))
                    {
                        Console.WriteLine($"✅ Matched alias or name: {name}");
                        return entry.Bio;
                    }
                }
            }

            Console.WriteLine("❌ No matching bio found.");
            return null;
        }

        private static string Normalize(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var sb = new StringBuilder();
            foreach (char c in input.ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public async Task<string> SendMessageAsync(IEnumerable<ChatMessage> messages)
        {
            if (_bios.Count == 0)
                await LoadBiosAsync();

            var userMessage = messages.LastOrDefault(m => m.Role.Equals("user", StringComparison.OrdinalIgnoreCase));
            string? bio = userMessage?.Content != null ? FindMatchingBio(userMessage.Content) : null;

            var allMessages = new List<object>();

            if (!string.IsNullOrWhiteSpace(bio))
            {
                allMessages.Add(new
                {
                    role = "system",
                    content = $"The user may ask about a known figure. Use the following trusted source if relevant:\n{bio}"
                });
            }

            allMessages.AddRange(messages.Select(m => new
            {
                role = m.Role.ToLower(),
                content = m.Content
            }));

            var payload = new
            {
                model = _options.Model,
                messages = allMessages
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
