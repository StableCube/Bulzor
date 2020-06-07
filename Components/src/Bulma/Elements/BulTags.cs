using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTags : BulComponentBase
    {
        [Parameter]
        public BulPrimaryColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? HasAddons { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("tags");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetPrimaryColor(Color);
            ClassBuilder.SetSizeChild(Size);
            ClassBuilder.SetHasAddons(HasAddons);

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