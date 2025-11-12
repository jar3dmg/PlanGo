using System.Net.Http.Json;
using TravelOrganizer.Client.Models;

namespace TravelOrganizer.Client.Services;

/// <summary>
/// Servicio encargado de comunicarse con la API para manejar los viajes.
/// </summary>
public class TripApiService
{
    private readonly HttpClient _http;

    public TripApiService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Obtiene la lista completa de viajes desde la API.
    /// </summary>
    public async Task<List<TripGetDto>> GetTripsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<TripGetDto>>("api/trips");
        return result ?? new List<TripGetDto>();
    }

    /// <summary>
    /// Obtiene un viaje específico por su ID.
    /// </summary>
    public async Task<TripGetDto?> GetTripByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/trips/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TripGetDto>();
    }

    /// <summary>
    /// Crea un nuevo viaje en la base de datos.
    /// </summary>
    public async Task CreateTripAsync(TripPostDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/trips", dto);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Actualiza un viaje existente.
    /// </summary>
    public async Task<bool> UpdateTripAsync(int id, TripPostDto dto)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"api/trips/{id}", dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Elimina un viaje por su ID.
    /// </summary>
    public async Task<bool> DeleteTripAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/trips/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
