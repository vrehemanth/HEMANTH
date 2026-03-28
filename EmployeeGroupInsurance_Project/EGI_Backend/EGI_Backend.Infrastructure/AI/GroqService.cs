using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using EGI_Backend.Application.Interfaces;

namespace EGI_Backend.Infrastructure.AI
{
    public class GroqService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string? _apiKey;

        public GroqService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = configuration["Groq:ApiKey"]?.Trim();
        }

        public async Task<string> GenerateChatResponse(List<(string role, string content)> messages)
        {
            try
            {
                if (string.IsNullOrEmpty(_apiKey)) return "AI unavailable (Missing API Key).";

                var requestBody = new
                {
                    model = "llama-3.1-8b-instant",
                    messages = messages.Select(m => new { role = m.role, content = m.content }).ToArray(),
                    temperature = 0.2,
                    max_tokens = 1500
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) return $"Groq error ({response.StatusCode}): {responseBody}";

                using var doc = JsonDocument.Parse(responseBody);
                var root = doc.RootElement;
                if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                {
                    return choices[0].GetProperty("message").GetProperty("content").GetString() ?? "";
                }

                return "Empty response from Groq.";
            }
            catch (Exception ex) { return $"Groq Error: {ex.Message}"; }
        }

        public async Task<string> GenerateResponse(string prompt)
        {
            return await GenerateChatResponse(new List<(string role, string content)> { ("user", prompt) });
        }
    }
}
