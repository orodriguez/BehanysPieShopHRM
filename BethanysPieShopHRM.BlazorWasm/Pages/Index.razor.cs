using BethanysPieShopHRM.BlazorWasm.Components.Widgets;

namespace BethanysPieShopHRM.BlazorWasm.Pages;

public partial class Index
{
    public List<Type> Widgets { get; set; } = 
        new() { typeof(EmployeeCountWidget), typeof(InboxWidget) };
}