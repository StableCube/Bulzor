using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarLink : BulNavLink
    {
        [Parameter]
        public bool Arrowless { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-link");

        protected override void BuildBulma()
        {
            ClassBuilder.IsArrowless = Arrowless;
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
                AdditionalAttributes["class"] = ClassBuilder.ClassString;
            }
            
            base.BuildRenderTree(builder);
        }
    }
}