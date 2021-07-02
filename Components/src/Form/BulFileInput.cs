using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulFileInput : BulComponentBase
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file-input");

        protected override void BuildBulma()
        {
            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(2, CombinedAdditionalAttributes);
            builder.AddAttribute(3, "type", "file");

            builder.CloseElement();
        }
    }
}