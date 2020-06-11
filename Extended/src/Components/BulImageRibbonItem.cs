using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Extended
{
    public class BulImageRibbonItem : BulComponentBase
    {
        [Parameter]
        public RibbonImage Image { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public bool Focused { get; set; }

        [Parameter]
        public BulRatio? Ratio { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("image-ribbon-item");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetIsActive(Active);
            InputClassBuilder.SetIsFocused(Focused);
            _elementClass = InputClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", _elementClass);

            builder.OpenComponent<BulImage>(3);
            builder.AddAttribute(4, "Ratio", Ratio);
            builder.AddAttribute(5, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenElement(3, "img");
                builder2.AddAttribute(2, "class", "image-ribbon-thumb");
                builder2.AddAttribute(4, "src", Image.Uri.AbsoluteUri);
                builder2.CloseElement();
            }));
            builder.CloseComponent();


            builder.CloseElement();
        }
    }
}