using System.Text;
using System.Text.Json;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;

namespace BethanysPieShopHRM.BlazorWasm.Services;

public class EmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public EmployeeDataService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees(
        bool refreshRequired = false)
    {
        if (!refreshRequired)
        {
            bool employeeExpirationExists = await _localStorageService
                .ContainKeyAsync(
                    LocalStorageConstants.EmployeeListExpirationKey);

            if (employeeExpirationExists)
            {
                var employeeListExpiration = await _localStorageService
                    .GetItemAsync<DateTime>(LocalStorageConstants.EmployeeListExpirationKey);
                if (employeeListExpiration > DateTime.Now)
                {
                    if (await _localStorageService.ContainKeyAsync(
                            LocalStorageConstants.EmployeesList))
                    {
                        return await _localStorageService
                            .GetItemAsync<List<Employee>>(
                                LocalStorageConstants.EmployeesList);
                    }
                }
            }
        }

        var result = await _httpClient.GetAsync("api/employee");
        var stream = await result.Content.ReadAsStreamAsync();
        var employees = await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        await _localStorageService
            .SetItemAsync(LocalStorageConstants.EmployeesList, employees);

        await _localStorageService
            .SetItemAsync(LocalStorageConstants.EmployeeListExpirationKey, DateTime.Now.AddMinutes(1));

        return employees;
    }

    public async Task<Employee> GetEmployeeDetails(int employeeId)
    {
        var result = await _httpClient.GetStreamAsync($"api/employee/{employeeId}");
        return (await JsonSerializer.DeserializeAsync<Employee>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }))!;
    }

    public async Task<Employee?> AddEmployee(Employee employee)
    {
        var employeeJson = new StringContent(
            JsonSerializer.Serialize(employee), 
            Encoding.UTF8, 
            "application/json");

        var response = await _httpClient.PostAsync("api/employee", employeeJson);

        if (!response.IsSuccessStatusCode) return null;
        
        var content = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<Employee>(content);
    }

    public async Task UpdateEmployee(Employee employee)
    {
        var employeeJson =
            new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

        await _httpClient.PutAsync("api/employee", employeeJson);
    }

    public async Task DeleteEmployee(int employeeId)
    {
        await _httpClient.DeleteAsync($"api/employee/{employeeId}");
    }
}