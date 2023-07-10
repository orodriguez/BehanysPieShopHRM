using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Components;

public partial class ProfilePicture
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}