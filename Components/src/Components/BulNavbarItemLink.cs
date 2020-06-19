using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarItemLink : BulNavLink
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-item");

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