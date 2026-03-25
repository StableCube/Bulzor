using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components;

public class BulFileCtaIcon : BulComponentBase
{
    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file-icon");

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
        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", ClassBuilder.ClassString);

        builder.OpenElement(2, "i");
        builder.AddMultipleAttributes(3, AdditionalAttributes);
        builder.CloseElement();

        builder.CloseElement();
    }
}
