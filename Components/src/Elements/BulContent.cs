using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public class BulContent : BulComponentBase
{
    [Parameter]
    public BulSize? Size { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("content");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        ClassBuilder.Size = Size;

        MergeBuilderClassAttribute(ClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    }
}
