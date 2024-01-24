using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components.Extended;

public class BulAccordionRowTitle : BulComponentBase
{
    [CascadingParameter]
    public BulAccordionRow ParentRow { get; set; }

    [CascadingParameter]
    public Guid RowId { get; set; }

    [CascadingParameter]
    public BulSchemeColor? Color { get; set; }

    [CascadingParameter]
    public bool Active { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    protected BulmaClassBuilder HeaderClassBuilder { get; set; } = new BulmaClassBuilder("accordion-row-header");

    protected BulmaClassBuilder ArrowClassBuilder { get; set; } = new BulmaClassBuilder("accordion-arrow");

    protected BulmaClassBuilder TitleClassBuilder { get; set; } = new BulmaClassBuilder("accordion-row-title");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        HeaderClassBuilder.SchemeColor = Color;

        ArrowClassBuilder.SchemeColor = Color;
        ArrowClassBuilder.IsActive = Active;

        TitleClassBuilder.SchemeColor = Color;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "a");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickHandlerAsync));

        builder.AddContent(3, (RenderFragment)((builder2) => {
            builder2.OpenElement(4, "div");
            builder2.AddAttribute(5, "class", ArrowClassBuilder.ClassString);
            builder2.CloseElement();

            builder2.OpenComponent<BulCardHeader>(6);
            builder2.AddAttribute(7, "class", HeaderClassBuilder.ClassString);
            builder.AddAttribute(8, "ChildContent", (RenderFragment)((builder3) => {
                builder3.OpenComponent<BulCardHeaderTitle>(7);
                builder3.AddAttribute(9, "class", TitleClassBuilder.ClassString);
                builder3.AddAttribute(10, "ChildContent", ChildContent);
                builder3.CloseComponent();
            }));
            builder.CloseComponent();
        }));

        builder.CloseElement();
    }

    private async Task OnClickHandlerAsync(MouseEventArgs args)
    {
        ParentRow.OnActiveToggle();
        await OnClick.InvokeAsync(args);
    }
}
