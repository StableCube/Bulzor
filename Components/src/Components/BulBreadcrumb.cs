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

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "nav");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);

            builder.OpenElement(2, "ul");
            builder.AddContent(3, ChildContent);
            // builder.AddAttribute(3, "ChildContent", (RenderFragment)((builder2) => {
            //     builder2.AddContent(5, ChildContent);
            // }));
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}