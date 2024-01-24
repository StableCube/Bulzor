using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components;

public class BulButton : BulComponentBase
{
    [Parameter]
    public bool Loading { get; set; }

    [Parameter]
    public BulSchemeColor? Color { get; set; }

    [Parameter]
    public BulSize? Size { get; set; }

    [Parameter]
    public bool? Active { get; set; }

    [Parameter]
    public bool? Focused { get; set; }

    [Parameter]
    public bool? Hovered { get; set; }

    [Parameter]
    public bool? Outlined { get; set; }

    [Parameter]
    public bool? FullWidth { get; set; }

    [Parameter]
    public bool? Inverted { get; set; }

    [Parameter]
    public bool? Rounded { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("button");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        ClassBuilder.IsLoading = Loading;
        ClassBuilder.SchemeColor = Color;
        ClassBuilder.Size = Size;
        ClassBuilder.IsActive = Active;
        ClassBuilder.IsFocused = Focused;
        ClassBuilder.IsHovered = Hovered;
        ClassBuilder.IsOutlined = Outlined;
        ClassBuilder.IsFullWidth = FullWidth;
        ClassBuilder.IsInverted = Inverted;
        ClassBuilder.IsRounded = Rounded;

        MergeBuilderClassAttribute(ClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "button");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
        builder.AddContent(3, ChildContent);
        builder.CloseElement();
    }
}
