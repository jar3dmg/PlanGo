using System.Net.Http.Json;
using TravelOrganizer.Client.Models;

namespace TravelOrganizer.Client.Services;

public class ActivityApiService
{
    private readonly HttpClient _http;

    public ActivityApiService(HttpClient http)
    {
        _http = http;
    }

    // EXISTENTE:
    public async Task<List<ActivityGetDto>> GetByTripIdAsync(int tripId)
    {
        try
        {
            var response = await _http.GetAsync($"api/activities/trip/{tripId}");
            if (!response.IsSuccessStatusCode)
                return new List<ActivityGetDto>();

            var data = await response.Content.ReadFromJsonAsync<List<ActivityGetDto>>();
            return data ?? new List<ActivityGetDto>();
        }
        catch
        {
            return new List<ActivityGetDto>();
        }
    }

    // NUEVOS MÉTODOS:

    public async Task<ActivityGetDto?> GetByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/activities/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<ActivityGetDto>();
    }

    public async Task<bool> CreateAsync(ActivityPostDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/activities", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, ActivityPostDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/activities/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/activities/{id}");
        return response.IsSuccessStatusCode;
    }
}
