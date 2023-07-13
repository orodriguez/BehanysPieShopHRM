using BethanysPieShopHRM.BlazorWasm.Models;
using BethanysPieShopHRM.BlazorWasm.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Components;

public partial class EmployeeDetails
{
    [Inject] public IEmployeeDataService? EmployeeDataService { get; set; }
    [Parameter] public string? EmployeeId { get; set; }

    public Employee? Employee { get; set; } = new Employee();

    protected override async Task OnInitializedAsync() => 
        Employee = await EmployeeDataService!
            .GetEmployeeDetails(int.Parse(EmployeeId));
}