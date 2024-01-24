using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace StableCube.Bulzor.Components;

public class BulCardFooterItemLink : BulComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Gets or sets a value representing the URL matching behavior.
    /// </summary>
    [Parameter]
    public NavLinkMatch Match { get; set; }

    [Parameter]
    public bool Active { get; set; }

    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("card-footer-item");

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
        builder.OpenComponent<NavLink>(0);
        builder.AddAttribute(1, "ActiveClass", CssConfig.Prefix + "is-active");
        builder.AddAttribute(2, "Match", Match);
        builder.AddMultipleAttributes(3, CombinedAdditionalAttributes);

        builder.AddAttribute(4, "ChildContent", (RenderFragment)((builder2) => {
            builder2.AddContent(5, ChildContent);
        }));

        builder.CloseComponent();
    }
}
