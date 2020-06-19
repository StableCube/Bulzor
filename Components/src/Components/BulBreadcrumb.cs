using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulBreadcrumb : BulComponentBase
    {
        /// <summary>
        /// Sets the is-centered Bulma class
        /// </summary>
        [Parameter]
        public bool? Centered { get; set; }

        /// <summary>
        /// Sets the separator Bulma class
        /// </summary>
        [Parameter]
        public BulSeparator? Separator { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("breadcrumb");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetIsCentered(Centered);
            ClassBuilder.SetSize(Size);
            ClassBuilder.SetSeparator(Separator);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "nav");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}