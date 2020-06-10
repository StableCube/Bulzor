using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulFileCtaIcon : BulComponentBase
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file-icon");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "span");
            builder.AddAttribute(1, "class", _elementClass);

            builder.OpenElement(2, "i");
            builder.AddMultipleAttributes(3, AdditionalAttributes);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}