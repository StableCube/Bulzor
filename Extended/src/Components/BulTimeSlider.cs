using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Extended
{
    public class BulTimeSlider : InputBase<TimeSpan>
    {
        [Parameter]
        public TimeSpan Min { get; set; } = TimeSpan.Zero;

        [Parameter]
        public TimeSpan Max { get; set; }

        [Parameter]
        public TimeSpan Step { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Circle { get; set; }

        public string ParsingErrorMessage { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("time-slider");
        protected string _elementClass = String.Empty;

        private long LongValue { get; set; }
        private Expression<Func<long>> LongValueExpression { get; set; }

        protected void BuildBulma()
        {
            _elementClass = InputClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            LongValueExpression = () => LongValue;

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", _elementClass);
            builder.AddAttribute(2, "AdditionalAttributes", AdditionalAttributes);

            builder.OpenComponent<BulSlider<long>>(3);
            builder.AddAttribute(4, "Value", (long)Value.TotalMilliseconds);
            builder.AddAttribute(5, "ValueExpression", LongValueExpression);
            builder.AddAttribute(6, "ValueChanged", EventCallback.Factory.Create<long>(this, ValueChangedHandlerAsync));
            builder.AddAttribute(7, "Min", (long)Min.TotalMilliseconds);
            builder.AddAttribute(8, "Max", (long)Max.TotalMilliseconds);
            builder.AddAttribute(9, "Step", (long)Step.TotalMilliseconds);
            builder.AddAttribute(10, "Size", Size);
            builder.AddAttribute(11, "Color", Color);
            builder.AddAttribute(12, "FullWidth", FullWidth);
            builder.AddAttribute(13, "Circle", Circle);
            builder.CloseComponent();

            builder.CloseElement();
        }

        private async Task ValueChangedHandlerAsync(long value)
        {
            Value = TimeSpan.FromMilliseconds(value);
            await ValueChanged.InvokeAsync(Value);
        }

        protected override bool TryParseValueFromString(string value, out TimeSpan result, out string validationErrorMessage)
        {
            if (TimeSpan.TryParse(value, out result))
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