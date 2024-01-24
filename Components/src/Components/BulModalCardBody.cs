using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public class BulModalCardBody : BulComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("modal-card-body");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        MergeBuilderClassAttribute(ClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "section");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    }
}
