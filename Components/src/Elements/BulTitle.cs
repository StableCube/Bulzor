using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTitle : BulComponentBase
    {
        [Parameter]
        public BulTextSize? Size { get; set; }

        /// <summary>
        /// If supplied the root element will be a header tag of the given size.
        /// Otherwise it will be a paragraph tag.
        /// </summary>
        [Parameter]
        public int? HeaderSize { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("title");

        protected override void BuildBulma()
        {
            ClassBuilder.TextSize = Size;

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(HeaderSize.HasValue)
            {
                builder.OpenElement(0, $"h{HeaderSize}");
            }
            else
            {
                builder.OpenElement(0, "p");
            }

            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.AddContent(2, ChildContent);

            builder.CloseElement();
        }
    }
}