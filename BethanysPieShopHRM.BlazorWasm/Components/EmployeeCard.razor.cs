using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Components;

public partial class EmployeeCard
{
    [Parameter]
    public Employee? Employee { get; set; }
    
    [Parameter] 
    public EventCallback<Employee> EmployeeQuickViewClicked { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    private void NavigateToDetails(Employee employee)
    {
        NavigationManager.NavigateTo(
            $"/employeedetails/{employee.EmployeeId}");
    }
}