using BethanysPieShopHRM.BlazorWasm.Models;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.BlazorWasm.Pages;

public partial class EmployeeOverview
{
    private Employee? _selectedEmployee;
    public List<Employee>? Employees { get; set; } = default!;

    public string Title { get; set; } = "Employee overview";

    protected override void OnInitialized()
    {
        Employees = MockDataService.Employees;
    }

    private void ShowQuickViewPopup(Employee selectedEmployee)
    {
        _selectedEmployee = selectedEmployee;
    }
}