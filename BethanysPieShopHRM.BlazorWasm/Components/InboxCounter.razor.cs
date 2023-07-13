using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.BlazorWasm.Components;

public partial class InboxCounter
{
    public int MessageCount { get; set; }

    [Inject]
    public ApplicationState ApplicationState { get; set; }

    protected override void OnInitialized()
    {
        MessageCount = new Random().Next(10);
        ApplicationState.NumberOfMessages = MessageCount;
    }
}