using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulAccordion : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        /// <summary>
        /// If true multiple rows can be open at once
        /// </summary>
        [Parameter]
        public bool AllowMultiple { get; set; }

        public Guid ActiveRow { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("accordion");

        protected override void BuildBulma()
        {
            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenComponent<CascadingValue<BulAccordion>>(0);
            builder.AddAttribute(1, "Value", this);
            builder.AddAttribute(2, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenComponent<CascadingValue<BulSchemeColor?>>(3);
                builder2.AddAttribute(4, "Value", Color);
                builder2.AddAttribute(5, "ChildContent", (RenderFragment)((builder3) => {
                    builder3.OpenElement(6, "div");
                    builder3.AddMultipleAttributes(7, CombinedAdditionalAttributes);
                    builder3.AddContent(8, ChildContent);
                    builder3.CloseElement();
                }));
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
        }

        public void OnRowActivated(Guid rowId)
        {
            if(!ActiveRow.Equals(rowId))
            {
                ActiveRow = rowId;
                StateHasChanged();
            }
        }
    }
}