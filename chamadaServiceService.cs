using System.Net.Http.Json;
using System.Text.Json;

namespace App.WindowsService;

public class JokeService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private const string JokeApiUrl =
        "https://karljoke.herokuapp.com/jokes/programming/random";

    public JokeService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<string> GetJokeAsync()
    {
        try
        {
            // A API retorna uma matriz com uma única entrada.
            Joke[]? jokes = await _httpClient.GetFromJsonAsync<Joke[]>(
                JokeApiUrl, _options);

            Joke? joke = jokes?[0];

            return joke is not null
                ? $"{joke.Setup}{Environment.NewLine}{joke.Punchline}"
                : "Sem piadas aqui...";
        }
        catch (Exception ex)
        {
            return $"Isso não é engraçado!!! {ex}";
        }
    }
}

public record Joke(int Id, string Type, string Setup, string Punchline);