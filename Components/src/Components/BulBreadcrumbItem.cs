using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public class BulBreadcrumbItem : BulComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

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
        builder.OpenElement(0, "li");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    }
}
