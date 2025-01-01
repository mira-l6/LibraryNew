using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class GeminiService
{
    private readonly HttpClient _httpClient;

    public GeminiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GenerateResponse(string prompt, string apiKey)
    {
        var requestPayload = new
        {
            model = "models/gemini-2.0",
            prompt = prompt,
            temperature = 0.7,
            maxOutputTokens = 256
        };

        var requestContent = new StringContent(
            JsonSerializer.Serialize(requestPayload),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(
            $"https://generativelanguage.googleapis.com/v1beta2/models/gemini:generateText?key={apiKey}",
            requestContent
        );

        //response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<JsonElement>(responseContent);

        return responseJson.GetProperty("text").GetString() ?? "No response generated.";
    }
}
