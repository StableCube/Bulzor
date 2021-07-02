using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTabs : BulComponentBase
    {
        [Parameter]
        public BulTextSize? Size { get; set; }

        [Parameter]
        public bool Boxed { get; set; }

        [Parameter]
        public bool Toggle { get; set; }

        [Parameter]
        public bool ToggleRounded { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Centered { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("tabs");

        protected override void BuildBulma()
        {
            ClassBuilder.TextSize = Size;
            ClassBuilder.IsBoxed = Boxed;
            ClassBuilder.IsToggle = Toggle;
            ClassBuilder.IsToggleRounded = ToggleRounded;
            ClassBuilder.IsFullWidth = FullWidth;
            ClassBuilder.IsCentered = Centered;
            ClassBuilder.IsRight = Right;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));

            builder.OpenElement(3, "ul");
            builder.AddContent(4, ChildContent);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}