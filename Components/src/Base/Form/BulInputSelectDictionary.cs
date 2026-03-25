using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Linq;
using System;

namespace StableCube.Bulzor.Components;

public class BulInputSelectDictionary<TValue> : BulInputSelectBase<KeyValuePair<TValue, string>>
{
    /// <summary>
    /// Options to be selected from. 
    /// Dictionary Key is the option display name and Value is the associated data.
    /// </summary>
    [Parameter, EditorRequired]
    public IImmutableDictionary<TValue, string> Options { get; set; }

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

        BuildSelect(builder, 4);

        builder.CloseElement();

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
        builder.CloseRegion();
    }

    private void BuildOptions(RenderTreeBuilder builder, int index)
    {
        int i = index;
        foreach (var pair in Options)
        {
            builder.OpenRegion(i);

            builder.OpenElement(0, "option");
            builder.AddAttribute(1, "value", pair.Value);
            
            builder.AddContent(2, pair.Value);
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

    protected override string FormatValueAsString(KeyValuePair<TValue, string> value)
    {
        return value.Value;
    }

    protected override bool TryParseValueFromString(string value, out KeyValuePair<TValue, string> result, out string validationErrorMessage)
    {
        KeyValuePair<TValue, string> option = Options.Where(o => o.Value == value).SingleOrDefault();
        if(!option.Equals(default(KeyValuePair<TValue, string>)))
        {
            result = option;
            validationErrorMessage = null;
            return true;
        }

        result = default;
        validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
        return false;
    }
}
