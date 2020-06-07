using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulMessageBody : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("message-body");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}