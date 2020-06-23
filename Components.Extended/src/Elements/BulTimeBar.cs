using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulTimeBar : BulComponentBase
    {
        [Parameter]
        public TimeSpan Current { get; set; } = TimeSpan.Zero;

        [Parameter]
        public TimeSpan Min { get; set; } = TimeSpan.Zero;

        [Parameter]
        public TimeSpan Max { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("time-bar");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetSize(Size);
            _elementClass = InputClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", _elementClass);
            builder.AddAttribute(2, "AdditionalAttributes", AdditionalAttributes);

            builder.AddContent(3, (RenderFragment)((builder2) => {
                builder2.OpenElement(4, "div");
                builder2.AddAttribute(5, "class", $"{CssConfig.Prefix}time-bar-landmark {CssConfig.Prefix}time-bar-landmark-start");
                builder2.AddContent(6, GetTimeString(Min));
                builder2.CloseElement();

                builder2.OpenElement(7, "div");
                builder2.AddAttribute(8, "class", $"{CssConfig.Prefix}time-bar-landmark");
                builder2.AddContent(9, GetTimeString(Current));
                builder2.CloseElement();

                builder2.OpenElement(10, "div");
                builder2.AddAttribute(11, "class", $"{CssConfig.Prefix}time-bar-landmark {CssConfig.Prefix}time-bar-landmark-end");
                builder2.AddContent(12, GetTimeString(Max));
                builder2.CloseElement();
            }));

            builder.CloseElement();
        }

        private string GetTimeString(TimeSpan time)
        {
            if(time.TotalMinutes < 1)
                return time.ToString(@"ss\.ff");

            if(time.TotalHours < 1)
                return time.ToString(@"mm\:ss\.ff");

            return time.ToString(@"hh\:mm\:ss\.ff");
        }
    }
}