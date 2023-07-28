using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarMenu : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        /// <summary>
        /// The navbar-menu is hidden on touch devices < 1024px . You need to add the modifier class is-active to display it. 
        /// </summary>
        [Parameter]
        public bool? Active { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-menu");

        protected override void BuildBulma()
        {
            ClassBuilder.IsActive = Active;

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}