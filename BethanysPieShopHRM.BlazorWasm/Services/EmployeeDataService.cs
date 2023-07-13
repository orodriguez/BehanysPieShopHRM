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

    public Task<Employee> AddEmployee()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }
}