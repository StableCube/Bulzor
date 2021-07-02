using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulCardFooterItemLink : BulNavLink
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("card-footer-item");

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(!AdditionalAttributes.ContainsKey("class"))
            {
                AdditionalAttributes.Add("class", MergeClassAttribute(ClassBuilder.ClassString));
            }
            else
            {
                AdditionalAttributes["class"] = MergeClassAttribute(ClassBuilder.ClassString);
            }
            
            base.BuildRenderTree(builder);
        }
    }
}