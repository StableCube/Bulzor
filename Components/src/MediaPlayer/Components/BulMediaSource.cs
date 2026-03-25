using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulMediaSource : BulComponentBase
{
    [Parameter]
    public Uri MediaUri { get; set; }

    [Parameter]
    public string MediaType { get; set; }

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "source");
        builder.AddAttribute(1, "src", MediaUri);
        builder.AddAttribute(2, "type", MediaType);
        builder.CloseElement();
    }
}
