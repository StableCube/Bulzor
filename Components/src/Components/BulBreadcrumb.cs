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

        protected override void BuildBulma()
        {
            ClassBuilder.IsCentered = Centered;
            ClassBuilder.Size = Size;
            ClassBuilder.Separator = Separator;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "nav");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}