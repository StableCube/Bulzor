using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulBusySpinner : BulComponentBase
    {
        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("busy-spinner");


        protected override void BuildBulma()
        {
            ClassBuilder.Size = Size;
            ClassBuilder.SchemeColor = Color;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));
            builder.CloseElement();
        }
    }
}