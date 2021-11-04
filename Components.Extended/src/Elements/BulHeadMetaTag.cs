using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.Extended
{
    /// <summary>
    /// Change add or update a meta tag in document head
    /// </summary>
    public class BulHeadMetaTag : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public string Name { get; set; }
        
        [Parameter]
        public string Content { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("bulSetMetaTag", Name, Content);
            }
        }
    }
}