using System.Text.Json;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.BlazorWasm.Services;

public class CountryDataService : ICountryDataService
{
    private readonly HttpClient _httpClient = default!;

    public CountryDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        var stream = await _httpClient.GetStreamAsync($"api/country");
        return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(
            stream, 
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<Country> GetCountryById(int countryId)
    {
        var stream = await _httpClient.GetStreamAsync($"api/country{countryId}");
        return await JsonSerializer.DeserializeAsync<Country>(
            stream, 
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}