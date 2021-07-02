using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulInputDateTime : BulInputBase<DateTime>
    {
        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public Range YearRange { get; set; } = new Range (1900, DateTime.UtcNow.Year);

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

        [Parameter]
        public Range HourRange { get; set; } = new Range (0, 23);

        [Parameter]
        public bool ShowHours { get; set; } = false;

        [Parameter]
        public string HourLabel { get; set; } = "HH";

        [Parameter]
        public Range MinuteRange { get; set; } = new Range (0, 59);

        [Parameter]
        public bool ShowMinutes { get; set; } = false;

        [Parameter]
        public string MinuteLabel { get; set; } = "MN";

        [Parameter]
        public Range SecondRange { get; set; } = new Range (0, 59);

        [Parameter]
        public bool ShowSeconds { get; set; } = false;

        [Parameter]
        public string SecondLabel { get; set; } = "SS";

        [Parameter]
        public Range MillisecondRange { get; set; } = new Range (0, 999);

        [Parameter]
        public bool ShowMilliseconds { get; set; } = false;

        [Parameter]
        public string MillisecondLabel { get; set; } = "MS";

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("field bul-datetime-input");
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
                BuildSelectInput(builder, 2, Value.Month, MonthRange, MonthLabel, EventCallback.Factory.Create<int>(this, MonthValueChangedHandler));

            if(ShowDays)
                BuildSelectInput(builder, 3, Value.Day, DayRange, DayLabel, EventCallback.Factory.Create<int>(this, DayValueChangedHandler));

            if(ShowYears)
                BuildNumberInput(builder, 4, Value.Year, YearRange, YearLabel, EventCallback.Factory.Create<int>(this, YearValueChangedHandler));
            
            if(ShowHours)
                BuildSelectInput(builder, 5, Value.Hour, HourRange, HourLabel, EventCallback.Factory.Create<int>(this, HourValueChangedHandler));

            if(ShowMinutes)
                BuildSelectInput(builder, 6, Value.Minute, MinuteRange, MinuteLabel, EventCallback.Factory.Create<int>(this, MinuteValueChangedHandler));

            if(ShowSeconds)
                BuildSelectInput(builder, 7, Value.Second, SecondRange, SecondLabel, EventCallback.Factory.Create<int>(this, SecondValueChangedHandler));
            
            if(ShowMilliseconds)
                BuildNumberInput(builder, 8, Value.Millisecond, MillisecondRange, MillisecondLabel, EventCallback.Factory.Create<int>(this, MillisecondValueChangedHandler));

            builder.CloseElement();
        }

        private void BuildSelectInput(
            RenderTreeBuilder builder, 
            int index, 
            int value, 
            Range range,
            string label,
            EventCallback<int> callback
        )
        {
            value = Math.Clamp(value, range.Start.Value, range.End.Value);
            
            Expression<Func<int>> valueExpression = () => value;

            builder.OpenRegion(index);

            if(label != null)
            {
                builder.OpenElement(2, "p");
                builder.AddAttribute(3, "class", "control");
                builder.OpenElement(4, "p");
                builder.AddAttribute(5, "class", LabelClassBuilder.ClassString);
                builder.AddContent(6, label);
                builder.CloseElement();
                builder.CloseElement();
            }

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "control bul-datetime-select");

            builder.OpenComponent<BulInputSelect<int>>(2);
            builder.AddAttribute(3, "Value", value);
            builder.AddAttribute(4, "ValueExpression", valueExpression);
            builder.AddAttribute(5, "ValueChanged", callback);
            builder.AddAttribute(6, "AdditionalAttributes", AdditionalAttributes);
            builder.AddAttribute(7, "Size", Size);
            builder.AddAttribute(8, "Color", Color);
            builder.AddAttribute(9, "Rounded", Rounded);

            builder.AddAttribute(10, "ChildContent", (RenderFragment)((builder2) =>
            {
                builder2.OpenRegion(11);
                int i = 0;
                for (int rangeVal = range.Start.Value; rangeVal <= range.End.Value; rangeVal++)
                {
                    builder2.OpenElement(i, "option");
                    i++;
                    builder2.AddAttribute(i, "value", rangeVal.ToString());
                    i++;
                    builder2.AddContent(i, rangeVal.ToString());
                    builder2.CloseElement();
                    i++;
                }
                builder2.CloseRegion();
            }));

            builder.CloseComponent();
            builder.CloseElement();
            
            builder.CloseRegion();
        }

        private void BuildNumberInput(
            RenderTreeBuilder builder, 
            int index, 
            int value, 
            Range range,
            string label,
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
            builder.AddAttribute(6, "class", "control bul-datetime-number");

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
            Value = new DateTime(year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second, Value.Millisecond);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void MonthValueChangedHandler(int month)
        {
            month = Math.Clamp(month, MonthRange.Start.Value, MonthRange.End.Value);
            Value = new DateTime(Value.Year, month, Value.Day, Value.Hour, Value.Minute, Value.Second, Value.Millisecond);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void DayValueChangedHandler(int day)
        {
            day = Math.Clamp(day, DayRange.Start.Value, DayRange.End.Value);
            Value = new DateTime(Value.Year, Value.Month, day, Value.Hour, Value.Minute, Value.Second, Value.Millisecond);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void HourValueChangedHandler(int hour)
        {
            hour = Math.Clamp(hour, HourRange.Start.Value, HourRange.End.Value);
            Value = new DateTime(Value.Year, Value.Month, Value.Day, hour, Value.Minute, Value.Second, Value.Millisecond);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void MinuteValueChangedHandler(int minute)
        {
            minute = Math.Clamp(minute, MinuteRange.Start.Value, MinuteRange.End.Value);
            Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, minute, Value.Second, Value.Millisecond);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void SecondValueChangedHandler(int second)
        {
            second = Math.Clamp(second, SecondRange.Start.Value, SecondRange.End.Value);
            Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, second, Value.Millisecond);

            await ValueChanged.InvokeAsync(Value);
        }

        private async void MillisecondValueChangedHandler(int ms)
        {
            ms = Math.Clamp(ms, MillisecondRange.Start.Value, MillisecondRange.End.Value);
            Value = new DateTime(Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second, ms);

            await ValueChanged.InvokeAsync(Value);
        }
    }
}