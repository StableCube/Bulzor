using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTag : BulComponentBase
    {
        [Parameter]
        public BulSchemeColor? Color { get; set; }

        /// <summary>
        /// Use light version of color
        /// </summary>
        [Parameter]
        public bool? Light { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("tag");

        protected override void BuildBulma()
        {
            ClassBuilder.Size = Size;
            ClassBuilder.SchemeColor = Color;
            ClassBuilder.IsLight = Light;
            ClassBuilder.IsRounded = Rounded;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "span");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}