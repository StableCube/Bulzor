using System.Globalization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components.Extended;

public class BulSlider<TValue> : InputNumber<TValue>
{
    [Parameter]
    public TValue Min { get; set; }

    [Parameter]
    public TValue Max { get; set; }

    [Parameter]
    public TValue Step { get; set; }

    [Parameter]
    public BulSize? Size { get; set; }

    [Parameter]
    public BulSchemeColor? Color { get; set; }

    [Parameter]
    public bool FullWidth { get; set; }

    [Parameter]
    public bool Circle { get; set; }

    protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("slider");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected void BuildBulma()
    {
        InputClassBuilder.SchemeColor = Color;
        InputClassBuilder.Size = Size;
        InputClassBuilder.IsFullWidth = FullWidth;
        InputClassBuilder.IsCircle = Circle;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "range");
        builder.AddAttribute(3, "class", InputClassBuilder.ClassString);
        builder.AddAttribute(4, "min", Min);
        builder.AddAttribute(5, "max", Max);
        builder.AddAttribute(6, "step", Step);
        builder.AddAttribute(7, "value", Value);
        builder.AddAttribute(8, "oninput", EventCallback.Factory.CreateBinder<string>(
            this, 
            __value => CurrentValueAsString = __value, CurrentValueAsString
        ));
        builder.CloseElement();
    }

    protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
    {
        if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.InvariantCulture, out result))
        {
            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = string.Format(ParsingErrorMessage, FieldIdentifier.FieldName);
            return false;
        }
    }
}
