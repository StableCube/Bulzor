using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended;

public class BulAccordionRowFooter : BulComponentBase
{
    [CascadingParameter]
    public bool Active { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("accordion-row-footer");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        ClassBuilder.IsActive = Active;

        MergeBuilderClassAttribute(ClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<BulCardFooter>(0);
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "ChildContent", ChildContent);
        builder.CloseComponent();
    }
}
