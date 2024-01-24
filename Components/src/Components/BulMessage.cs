using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public class BulMessage : BulComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    [Parameter]
    public BulSchemeColor? Color { get; set; }

    [Parameter]
    public BulSize? Size { get; set; }

    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("message");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        ClassBuilder.SchemeColor = Color;
        ClassBuilder.Size = Size;

        MergeBuilderClassAttribute(ClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "article");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    }
}
