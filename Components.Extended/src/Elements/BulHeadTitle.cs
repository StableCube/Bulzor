using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.Extended;

/// <summary>
/// Change the title in the document head
/// </summary>
public class BulHeadTitle : ComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public string Title { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("bulSetPageTitle", Title);
        }
    }
}
