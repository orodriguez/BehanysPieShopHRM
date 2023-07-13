using System.Text.Json;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.BlazorWasm.Services;

public class EmployeeDataService : IEmployeeDataService
{
    private HttpClient _httpClient;

    public EmployeeDataService(HttpClient httpClient) => 
        _httpClient = httpClient;

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        var result = await _httpClient.GetStreamAsync("api/employee");
        return (await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }))!;
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