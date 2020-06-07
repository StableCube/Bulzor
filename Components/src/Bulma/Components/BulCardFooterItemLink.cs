using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulCardFooterItemLink : BulNavLink
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("card-footer-item");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(!AdditionalAttributes.ContainsKey("class"))
            {
                AdditionalAttributes.Add("class", MergeClassAttribute(_elementClass));
            }
            else
            {
                AdditionalAttributes["class"] = MergeClassAttribute(_elementClass);
            }
            
            base.BuildRenderTree(builder);
        }
    }
}