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

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetTextSize(Size);
            ClassBuilder.SetIsBoxed(Boxed);
            ClassBuilder.SetIsToggle(Toggle);
            ClassBuilder.SetIsToggleRounded(ToggleRounded);
            ClassBuilder.SetIsFullWidth(FullWidth);
            ClassBuilder.SetIsCentered(Centered);
            ClassBuilder.SetIsRight(Right);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", MergeClassAttribute(_elementClass));

            builder.OpenElement(2, "ul");
            builder.AddContent(3, ChildContent);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}