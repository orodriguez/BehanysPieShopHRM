using BethanysPieShopHRM.BlazorWasm.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Pages;

public partial class EmployeeEdit
{
    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }
    
    [Inject]
    public ICountryDataService? CountryDataService { get; set; }

    [Inject]
    public IJobCategoryDataService? JobCategoryDataService { get; set; }
    
    [Parameter]
    public string? EmployeeId { get; set; }
    
    private Employee Employee { get; set; } = new Employee();
    
    private List<Country> Countries { get; set; } = new List<Country>();

    public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();
    
    protected override async Task OnInitializedAsync()
    {
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
}