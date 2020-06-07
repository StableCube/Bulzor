using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTitle : BulComponentBase
    {
        [Parameter]
        public BulColor? Color { get; set; }

        [Parameter]
        public BulTextSize? Size { get; set; }

        [Parameter]
        public BulTextWeight? Weight { get; set; }

        [Parameter]
        public bool Spaced { get; set; }

        /// <summary>
        /// If supplied the root element will be a header tag of the given size.
        /// Otherwise it will be a paragraph tag.
        /// </summary>
        [Parameter]
        public int? HeaderSize { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("title");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetTextColor(Color);
            ClassBuilder.SetTextSize(Size);
            ClassBuilder.SetTextWeight(Weight);
            ClassBuilder.SetIsSpaced(Spaced);

            _elementClass = ClassBuilder.ToString();
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

            builder.AddAttribute(1, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(2, ChildContent);

            builder.CloseElement();
        }
    }
}