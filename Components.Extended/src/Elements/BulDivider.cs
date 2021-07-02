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

        protected override void BuildBulma()
        {
            InputClassBuilder.SchemeColor = Color;
            InputClassBuilder.IsVertical = Vertical;

            if(Position.HasValue)
            {
                if(Position.Value == BulHorizontalPosition.Left)
                {
                    InputClassBuilder.IsLeft = true;
                    InputClassBuilder.IsRight = null;
                }
                else if(Position.Value == BulHorizontalPosition.Right)
                {
                    InputClassBuilder.IsLeft = null;
                    InputClassBuilder.IsRight = true;
                }
                else
                {
                    InputClassBuilder.IsLeft = null;
                    InputClassBuilder.IsRight = null;
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(InputClassBuilder.ClassString));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}