using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace StableCube.Bulzor.Components;

public class BulInputSelectEnum<TValue> : BulInputSelectBase<TValue>
{
    /// <summary>
    /// Optionally set formatted display names
    /// </summary>
    [Parameter]
    public IImmutableDictionary<TValue, string> DisplayNames { get; set; }

    /// <summary>
    /// Optionally exclude some enum values
    /// </summary>
    [Parameter]
    public IList<TValue> Exclude { get; set; }

    [Parameter]
    public EventCallback<TValue> OnValueChanged { get; set; }

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
        if(Value == null || !Value.GetType().IsEnum)
            throw new TypeLoadException("Value must be an Enum type");

        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", ControlClassBuilder.ClassString);
        builder.OpenElement(2, "div");
        builder.AddAttribute(3, "class", SelectClassBuilder.ClassString);

        BuildSelect(builder, 4);

        if(!string.IsNullOrEmpty(IconClass))
        {
            BuildIcon(builder, 5);
        }

        builder.CloseElement();
    }

    private void BuildSelect(RenderTreeBuilder builder, int index)
    {
        builder.OpenRegion(index);

        builder.OpenElement(0, "select");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "class", CssClass);
        builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValueAsString));
        builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, value => CurrentValueAsString = value, CurrentValueAsString, null));

        BuildOptions(builder, 5);

        builder.CloseComponent();
        builder.CloseElement();

        builder.CloseRegion();
    }

    private void BuildOptions(RenderTreeBuilder builder, int index)
    {
        int i = index;
        foreach (var enumVal in Enum.GetValues(typeof(TValue)))
        {
            builder.OpenRegion(i);

            builder.OpenElement(0, "option");
            builder.AddAttribute(1, "value", enumVal);
            
            builder.AddContent(2, GetDisplayName(enumVal));
            builder.CloseElement();

            builder.CloseRegion();
            i++;
        }
    }

    private void BuildIcon(RenderTreeBuilder builder, int index)
    {
        builder.OpenRegion(index);

        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", IconClassBuilder.ClassString);

        builder.OpenElement(2, "i");
        builder.AddAttribute(3, "class", IconClass);
        builder.CloseElement();

        builder.CloseElement();
        builder.CloseRegion();
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

    private string GetDisplayName(object value)
    {
        if(DisplayNames != null && DisplayNames.TryGetValue((TValue)value, out string displayName))
        {
            return displayName;
        }

        return value.ToString();
    }
}
