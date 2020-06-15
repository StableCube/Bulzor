using System;
using System.Globalization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Components.Extended
{
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
        protected string _elementClass = String.Empty;

        protected void BuildBulma()
        {
            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetSize(Size);
            InputClassBuilder.SetIsFullWidth(FullWidth);
            InputClassBuilder.SetIsCircle(Circle);
            _elementClass = InputClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "range");
            builder.AddAttribute(3, "class", _elementClass);
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
}