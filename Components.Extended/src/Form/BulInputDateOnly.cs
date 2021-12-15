using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulInputDateOnly : BulInputBase<DateOnly>
    {
        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public Range YearRange { get; set; } = new Range (1900, DateTimeOffset.UtcNow.Year);

        [Parameter]
        public bool ShowYears { get; set; } = true;

        [Parameter]
        public string YearLabel { get; set; } = "YYYY";

        [Parameter]
        public Range MonthRange { get; set; } = new Range (1, 12);

        [Parameter]
        public bool ShowMonths { get; set; } = true;

        [Parameter]
        public string MonthLabel { get; set; } = "MM";

        [Parameter]
        public Range DayRange { get; set; } = new Range (1, 31);

        [Parameter]
        public bool ShowDays { get; set; } = true;

        [Parameter]
        public string DayLabel { get; set; } = "DD";

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("field bul-dateonly-input");
        protected BulmaClassBuilder LabelClassBuilder { get; set; } = new BulmaClassBuilder("button is-static");
        protected BulmaClassBuilder SelectInputClassBuilder { get; set; } = new BulmaClassBuilder("select");
        protected BulmaClassBuilder NumberInputClassBuilder { get; set; } = new BulmaClassBuilder("input");

        protected string _selectInputClass = String.Empty;
        protected string _numberInputClass = String.Empty;

        protected override void BuildBulma()
        {
            WrapperClassBuilder.HasAddons = true;

            LabelClassBuilder.SchemeColor = Color;
            LabelClassBuilder.Size = Size;

            BuildPart(SelectInputClassBuilder, ref _selectInputClass);
            BuildPart(NumberInputClassBuilder, ref _numberInputClass);
        }

        private void BuildPart(BulmaClassBuilder classBuilder, ref string classString)
        {
            classBuilder.Size = Size;
            classBuilder.IsLoading = Loading;
            classBuilder.IsRounded = Rounded;

            if(Loading.HasValue == false || Loading.Value == false)
            {
                classBuilder.SchemeColor = Color;
            }
            else
            {
                classBuilder.SchemeColor = null;
            }

            classString = classBuilder.ClassString;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", WrapperClassBuilder.ClassString);

            if(ShowMonths)
                BuildNumberInput(builder, 2, Value.Month, MonthRange, MonthLabel, "month", EventCallback.Factory.Create<int>(this, MonthValueChangedHandler));

            if(ShowDays)
                BuildNumberInput(builder, 3, Value.Day, DayRange, DayLabel, "day", EventCallback.Factory.Create<int>(this, DayValueChangedHandler));

            if(ShowYears)
                BuildNumberInput(builder, 4, Value.Year, YearRange, YearLabel, "year", EventCallback.Factory.Create<int>(this, YearValueChangedHandler));
            
            builder.CloseElement();
        }

        private void BuildNumberInput(
            RenderTreeBuilder builder,
            int index,
            int value,
            Range range,
            string label,
            string type,
        EventCallback<int> callback
        )
        {
            value = Math.Clamp(value, range.Start.Value, range.End.Value);
            
            Expression<Func<int>> valueExpression = () => value;

            builder.OpenRegion(index);

            if (label != null)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "control");
                builder.OpenElement(2, "div");
                builder.AddAttribute(3, "class", LabelClassBuilder.ClassString);
                builder.AddContent(4, label);
                builder.CloseElement();
                builder.CloseElement();
            }

            builder.OpenElement(5, "div");
            builder.AddAttribute(6, "class", $"control bul-dateinput-{type}");

            builder.OpenComponent<BulInputNumber<int>>(7);
            builder.AddAttribute(8, "Value", value);
            builder.AddAttribute(9, "ValueExpression", valueExpression);
            builder.AddAttribute(10, "ValueChanged", callback);
            builder.AddAttribute(11, "AdditionalAttributes", AdditionalAttributes);
            builder.AddAttribute(12, "Size", Size);
            builder.AddAttribute(13, "Color", Color);
            builder.AddAttribute(14, "Rounded", Rounded);

            builder.CloseComponent();

            builder.CloseElement();
            
            builder.CloseRegion();
        }

        private async void YearValueChangedHandler(int year)
        {
            year = Math.Clamp(year, YearRange.Start.Value, YearRange.End.Value);
            Value = new DateOnly(year, Value.Month, Value.Day);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void MonthValueChangedHandler(int month)
        {
            month = Math.Clamp(month, MonthRange.Start.Value, MonthRange.End.Value);
            Value = new DateOnly(Value.Year, month, Value.Day);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void DayValueChangedHandler(int day)
        {
            day = Math.Clamp(day, DayRange.Start.Value, DayRange.End.Value);
            Value = new DateOnly(Value.Year, Value.Month, day);

            await ValueChanged.InvokeAsync(Value);
        }
    }
}