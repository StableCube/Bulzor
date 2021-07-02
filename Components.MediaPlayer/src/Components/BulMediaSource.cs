using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public class BulMediaSource : ComponentBase
    {
        [Parameter]
        public Uri MediaUri { get; set; }

        [Parameter]
        public string MediaType { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "source");
            builder.AddAttribute(1, "src", MediaUri);
            builder.AddAttribute(2, "type", MediaType);
            builder.CloseElement();
        }
    }
}