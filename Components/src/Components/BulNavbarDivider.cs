using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarDivider : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-divider");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "hr");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}