using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulFileInput : BulComponentBase
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file-input");

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "class", ClassBuilder.ClassString);
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.AddAttribute(3, "type", "file");

            builder.CloseElement();
        }
    }
}