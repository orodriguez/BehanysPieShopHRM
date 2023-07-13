using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.BlazorWasm.Services;

public interface IEmployeeDataService
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> GetEmployeeDetails(int employeeId);
    Task<Employee> AddEmployee();
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(int employeeId);
}