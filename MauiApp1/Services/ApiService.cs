using MauiApp1.Models;
using System.Text;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Client>> GetClientsAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error fetching clients: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Client>>(jsonResponse);
    }

    public async Task<HttpResponseMessage> UpdateClientAsync(string url, Client client)
    {
        var jsonPayload = JsonSerializer.Serialize(client);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        return await _httpClient.PutAsync(url, content);
    }

    public async Task<HttpResponseMessage> DeleteClientAsync(string url)
    {
        return await _httpClient.DeleteAsync(url);
    }
    public async Task<HttpResponseMessage> AddClientAsync(string url, Client client)
    {
        var jsonPayload = JsonSerializer.Serialize(client);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        return await _httpClient.PostAsync(url, content);
    }

}
