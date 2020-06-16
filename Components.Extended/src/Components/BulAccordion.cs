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

        [Parameter]
        public bool AllowMultiple { get; set; }

        public Guid ActiveRow { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("bul-accordion");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
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
                    builder3.AddMultipleAttributes(7, AdditionalAttributes);
                    builder3.AddAttribute(8, "class", MergeClassAttribute(_elementClass));
                    builder3.AddContent(9, ChildContent);
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