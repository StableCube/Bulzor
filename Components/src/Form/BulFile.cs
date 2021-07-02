using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulFile : BulComponentBase
    {
        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Boxed { get; set; }

        [Parameter]
        public bool? HasName { get; set; }

        [Parameter]
        public bool? FullWidth { get; set; }

        [Parameter]
        public bool? Right { get; set; }

        [Parameter]
        public bool? Centered { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file");

        protected override void BuildBulma()
        {
            ClassBuilder.SchemeColor = Color;
            ClassBuilder.Size = Size;
            ClassBuilder.IsBoxed = Boxed;
            ClassBuilder.HasName = HasName;
            ClassBuilder.IsFullWidth = FullWidth;
            ClassBuilder.IsRight = Right;
            ClassBuilder.IsCentered = Centered;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", ClassBuilder.ClassString);
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}