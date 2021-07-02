using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulDropdownItemLink : BulNavLink
    {
        [Parameter]
        public bool Active { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("dropdown-item");

        protected override void BuildBulma()
        {
            ClassBuilder.IsActive = Active;
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