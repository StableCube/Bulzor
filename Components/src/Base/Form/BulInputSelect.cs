using System;
using System.Globalization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public class BulInputSelect<TValue> : BulInputSelectBase<TValue>
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
        SelectClassBuilder.Size = Size;
        SelectClassBuilder.IsLoading = Loading;
        SelectClassBuilder.IsRounded = Rounded;
        SelectClassBuilder.IsFullWidth = FullWidth;

        if(Loading.HasValue == false || Loading.Value == false)
        {
            SelectClassBuilder.SchemeColor = Color;
        }
        else
        {
            SelectClassBuilder.SchemeColor = null;
        }

        if(!string.IsNullOrEmpty(IconClass))
        {
            IconClassBuilder.IsLeft = true;
            IconClassBuilder.Size = Size;
        }

        ControlClassBuilder.IsExpanded = Expanded;
        ControlClassBuilder.HasIconsLeft = !string.IsNullOrEmpty(IconClass);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", ControlClassBuilder.ClassString);
        builder.OpenElement(2, "div");
        builder.AddAttribute(3, "class", SelectClassBuilder.ClassString);

        builder.OpenComponent<InputSelect<TValue>>(4);
        builder.AddAttribute(5, "Value", Value);
        builder.AddAttribute(6, "ValueExpression", ValueExpression);
        builder.AddAttribute(7, "ValueChanged", ValueChanged);
        builder.AddAttribute(8, "AdditionalAttributes", AdditionalAttributes);

        builder.AddAttribute(9, "ChildContent", (RenderFragment)((builder2) => {
            builder2.AddContent(10, ChildContent);
        }));

        builder.CloseComponent();
        builder.CloseElement();

        if(!string.IsNullOrEmpty(IconClass))
        {
            builder.OpenElement(12, "span");
            builder.AddAttribute(13, "class", IconClassBuilder.ClassString);

            builder.OpenElement(15, "i");
            builder.AddAttribute(16, "class", IconClass);
            builder.CloseElement();

            builder.CloseElement();
        }

        builder.CloseElement();
    }

    protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
    {
        if (BindConverter.TryConvertTo(value, CultureInfo.CurrentCulture, out TValue parsedValue))
        {
            result = parsedValue;
            validationErrorMessage = null;
            return true;
        }

        // Map null/empty value to null if the bound object is nullable
        if (string.IsNullOrEmpty(value))
        {
            var nullableType = Nullable.GetUnderlyingType(typeof(TValue));
            if (nullableType != null)
            {
                result = default;
                validationErrorMessage = null;
                return true;
            }
        }

        // The value is invalid => set the error message
        result = default;
        validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
        return false;
    }
}
