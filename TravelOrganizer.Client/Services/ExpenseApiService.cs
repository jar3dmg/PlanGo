using System.Net.Http.Json;
using TravelOrganizer.Client.Models;

namespace TravelOrganizer.Client.Services;

public class ExpenseApiService
{
    private readonly HttpClient _http;

    public ExpenseApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ExpenseGetDto>> GetByTripIdAsync(int tripId)
    {
        try
        {
            var response = await _http.GetAsync($"api/expenses/trip/{tripId}");
            if (!response.IsSuccessStatusCode)
                return new List<ExpenseGetDto>();

            var data = await response.Content.ReadFromJsonAsync<List<ExpenseGetDto>>();
            return data ?? new List<ExpenseGetDto>();
        }
        catch
        {
            return new List<ExpenseGetDto>();
        }
    }

    public async Task<ExpenseGetDto?> GetByIdAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<ExpenseGetDto>($"api/expenses/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateAsync(ExpensePostDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/expenses", dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, ExpensePostDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/expenses/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/expenses/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
