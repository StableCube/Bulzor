using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components;

public class BulNavbarLink : BulComponentBase
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

    [Parameter]
    public bool Arrowless { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-link");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        ClassBuilder.IsArrowless = Arrowless;
        ClassBuilder.IsActive = Active;

        MergeBuilderClassAttribute(ClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<NavLink>(0);
        builder.AddAttribute(1, "ActiveClass", CssConfig.Prefix + "is-active");
        builder.AddAttribute(2, "Match", Match);
        builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
        builder.AddMultipleAttributes(4, CombinedAdditionalAttributes);

        builder.AddAttribute(5, "ChildContent", (RenderFragment)((builder2) => {
            builder2.AddContent(6, ChildContent);
        }));

        builder.CloseComponent();
    }
}
