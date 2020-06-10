using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Extended
{
    public class BulImageRibbon : BulComponentBase
    {

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            // builder.OpenComponent<BulProgressBar>(0);
            // builder.AddMultipleAttributes(1, AdditionalAttributes);
            // builder.AddAttribute(2, "Value", Progress);
            // builder.AddAttribute(3, "Color", Color);
            // builder.AddAttribute(4, "Size", Size);
            
            builder.CloseComponent();
        }
    }
}