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
        public BulHorizontalPosition? Position { get; set; }

        [Parameter]
        public bool? Vertical { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("divider");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetIsVertical(Vertical);

            if(Position.HasValue)
            {
                if(Position.Value == BulHorizontalPosition.Left)
                {
                    InputClassBuilder.SetIsLeft(true);
                    InputClassBuilder.SetIsRight(null);
                }
                else if(Position.Value == BulHorizontalPosition.Right)
                {
                    InputClassBuilder.SetIsLeft(null);
                    InputClassBuilder.SetIsRight(true);
                }
                else
                {
                    InputClassBuilder.SetIsLeft(null);
                    InputClassBuilder.SetIsRight(null);
                }
            }

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