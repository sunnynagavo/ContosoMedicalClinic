using System.Net.Http.Json;
using System.Text.Json;

namespace ContosoMedicalClinic.Infrastructure.DataApi;

/// <summary>
/// Generic HTTP client for Data API Builder REST endpoints.
/// DAB exposes entities at /api/{EntityName} with OData-style filtering.
/// </summary>
public class DabHttpClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<T>> GetListAsync<T>(string entity, string? filter = null)
    {
        var url = $"/api/{entity}";
        if (!string.IsNullOrEmpty(filter))
            url += $"?$filter={Uri.EscapeDataString(filter)}";

        var response = await httpClient.GetFromJsonAsync<DabListResponse<T>>(url, JsonOptions);
        return response?.Value ?? [];
    }

    public async Task<T?> GetByIdAsync<T>(string entity, int id, string pkColumn = "Id")
    {
        var url = $"/api/{entity}/{pkColumn}/{id}";
        try
        {
            return await httpClient.GetFromJsonAsync<T>(url, JsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task<T> CreateAsync<T>(string entity, object data)
    {
        var response = await httpClient.PostAsJsonAsync($"/api/{entity}", data, JsonOptions);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions))!;
    }

    public async Task<T> UpdateAsync<T>(string entity, int id, object data, string pkColumn = "Id")
    {
        var response = await httpClient.PatchAsJsonAsync($"/api/{entity}/{pkColumn}/{id}", data, JsonOptions);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions))!;
    }

    public async Task DeleteAsync(string entity, int id, string pkColumn = "Id")
    {
        var response = await httpClient.DeleteAsync($"/api/{entity}/{pkColumn}/{id}");
        response.EnsureSuccessStatusCode();
    }

    private record DabListResponse<T>(List<T> Value);
}
