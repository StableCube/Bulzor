using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulNavbar : BulComponentBase
    {
        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public bool Transparent { get; set; }

        [Parameter]
        public bool FixedTop { get; set; }

        [Parameter]
        public bool FixedBottom { get; set; }

        [Parameter]
        public bool Spaced { get; set; }

        [Parameter]
        public bool Shadowed { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SchemeColor = Color;
            ClassBuilder.IsTransparent = Transparent;
            ClassBuilder.IsFixedTop = FixedTop;
            ClassBuilder.IsFixedBottom = FixedBottom;
            ClassBuilder.IsSpaced = Spaced;
            ClassBuilder.HasShadow = Shadowed;

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "nav");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}