using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulDivider : BulComponentBase
    {
        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public bool? IsLeft { get; set; }

        [Parameter]
        public bool? IsRight { get; set; }

        [Parameter]
        public bool? IsVertical { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("divider");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetIsLeft(IsLeft);
            InputClassBuilder.SetIsRight(IsRight);
            InputClassBuilder.SetIsVertical(IsVertical);

            _elementClass = InputClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}