using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components.Extended;

public class BulToggleSwitch : BulInputBase<bool>
{
    [Parameter]
    public bool Rounded { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected BulmaClassBuilder LabelClassBuilder = new("toggle-switch");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        LabelClassBuilder.SchemeColor = Color;
        LabelClassBuilder.Size = Size;
        LabelClassBuilder.IsRounded = Rounded;

        MergeBuilderClassAttribute(LabelClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "label");
        builder.AddAttribute(1, "class", LabelClassBuilder.ClassString);

        builder.OpenComponent<InputCheckbox>(2);
        builder.AddAttribute(3, "Value", Value);
        builder.AddAttribute(4, "ValueExpression", ValueExpression);
        builder.AddAttribute(5, "ValueChanged", ValueChanged);
        builder.CloseComponent();

        builder.OpenElement(6, "i");
        builder.CloseElement();

        builder.OpenElement(7, "span");
        builder.AddContent(8, ChildContent);
        builder.CloseElement();

        builder.CloseElement();
    }
}
