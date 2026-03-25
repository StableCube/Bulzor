using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components;

public class BulDropdownDivider : BulComponentBase
{
    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("dropdown-divider");

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
        builder.OpenElement(0, "hr");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);

        builder.CloseElement();
    }
}
