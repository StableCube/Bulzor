using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulModalCardBody : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("modal-card-body");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "section");
            builder.AddAttribute(1, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}