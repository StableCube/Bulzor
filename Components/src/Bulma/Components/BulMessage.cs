using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulMessage : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        [Parameter]
        public BulPrimaryColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("message");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetPrimaryColor(Color);
            ClassBuilder.SetSize(Size);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "article");
            builder.AddAttribute(1, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}