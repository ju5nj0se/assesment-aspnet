using JuanJoseHernandez.DTOs;

namespace JuanJoseHernandez.Services.Implementations;

public class OpenAIService
{
    private readonly HttpClient _client;
    
    public OpenAIService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("OpenAI");
    }

    public async Task<string> EnviarPromptAsync(string prompt)
    {
        var body = new
        {
            model = "gpt-4.1-mini",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var response = await _client.PostAsJsonAsync("chat/completions", body);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OpenAIChatResponse>();

        return result?.Choices[0].Message.Content ?? string.Empty;
    }   
}