using BethanysPieShopHRM.BlazorWasm.Models;
using BethanysPieShopHRM.BlazorWasm.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Pages;

public partial class EmployeeOverview
{
    [Inject] public IEmployeeDataService? EmployeeDataService { get; set; }
    
    private Employee? _selectedEmployee;
    public List<Employee>? Employees { get; set; } = default!;

    public string Title { get; set; } = "Employee overview";

    protected async override Task OnInitializedAsync()
    {
        Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
    }

    private void ShowQuickViewPopup(Employee selectedEmployee)
    {
        _selectedEmployee = selectedEmployee;
    }
}