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

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.OpenElement(2, "ul");
            builder.AddContent(3, ChildContent);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}