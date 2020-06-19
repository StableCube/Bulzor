using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulModal : BulComponentBase
    {
        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("modal");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetIsActive(Active);

            _elementClass = ClassBuilder.ToString();
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