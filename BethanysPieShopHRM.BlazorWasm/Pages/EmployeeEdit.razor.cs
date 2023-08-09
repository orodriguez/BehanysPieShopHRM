using System.Runtime.InteropServices;
using BethanysPieShopHRM.BlazorWasm.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShopHRM.BlazorWasm.Pages;

public partial class EmployeeEdit
{
    [Inject] public IEmployeeDataService? EmployeeDataService { get; set; }
    [Inject] public ICountryDataService? CountryDataService { get; set; }
    [Inject] public IJobCategoryDataService? JobCategoryDataService { get; set; }
    [Inject] public NavigationManager? NavigationManager { get; set; }
    [Parameter] public string? EmployeeId { get; set; }
    private Employee Employee { get; set; } = new Employee();
    private List<Country> Countries { get; set; } = new List<Country>();
    private List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();
    private string Message { get; set; } = string.Empty;
    private string StatusClass { get; set; } = string.Empty;
    private bool Saved { get; set; } = false;

    private IBrowserFile? _selectedFile;

    protected override async Task OnInitializedAsync()
    {
        Saved = false;

        Countries = (await CountryDataService!.GetAllCountries()).ToList();

        JobCategories = (await JobCategoryDataService!.GetAllJobCategories()).ToList();

        int.TryParse(EmployeeId, out var employeeId);

        if (employeeId == 0)
        {
            Employee = new Employee
            {
                CountryId = 1,
                JobCategoryId = 1,
                BirthDate = DateTime.Now,
                JoinedDate = DateTime.Now
            };
        }
        else
        {
            Employee = await EmployeeDataService!
                .GetEmployeeDetails(employeeId);
        }
    }

    private async Task HandleValidSubmit()
    {
        Saved = false;

        if (Employee.EmployeeId == 0)
        {
            if (_selectedFile != null)
            {
                var file = _selectedFile;
                var stream = file.OpenReadStream();
                var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                Employee.ImageName = file.Name;
                Employee.ImageContent = ms.ToArray();
            }

            var addedEmployee = await EmployeeDataService!.AddEmployee(Employee);
            if (addedEmployee == null)
            {
                Saved = false;
                StatusClass = "alert-danger";
                Message = "New employee added successfully";
            }

            Saved = true;
            StatusClass = "alert-success";
            Message = "New employee added successfully";
            return;
        }

        await EmployeeDataService!.UpdateEmployee(Employee);
        StatusClass = "alert-success";
        Message = "Employee updated successfully";
        Saved = true;
    }

    private void HandleInvalidSubmit()
    {
        StatusClass = "alert-danger";
        Message = "There are some validation errors. Please try again.";
    }

    private async Task DeleteEmployee()
    {
        await EmployeeDataService!.DeleteEmployee(Employee.EmployeeId);

        StatusClass = "alert-success";
        Message = "Delete successfully";
        Saved = true;
    }

    private void NavigateToOverview()
    {
        NavigationManager!.NavigateTo("/employeeoverview");
    }

    private void OnInputFileChange(InputFileChangeEventArgs e)
    {
        _selectedFile = e.File;
        StateHasChanged();
    }
}