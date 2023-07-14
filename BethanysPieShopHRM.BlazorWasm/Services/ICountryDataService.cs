using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.BlazorWasm.Services;

public interface ICountryDataService
{
    Task<IEnumerable<Country>> GetAllCountries();

    Task<Country> GetCountryById(int countryId);
}