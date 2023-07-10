using BethanysPieShopHRM.BlazorWasm.Models;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Components;

public partial class EmployeeDetails
{
    [Parameter] public string? EmployeeId { get; set; }

    public Employee? Employee { get; set; } = new Employee();

    protected override Task OnInitializedAsync()
    {
        Employee = MockDataService.Employees
            .FirstOrDefault(
                employee => employee.EmployeeId == int.Parse(EmployeeId!));

        return base.OnInitializedAsync();
    }
}