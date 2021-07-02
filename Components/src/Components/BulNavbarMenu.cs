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
        public bool? IsActive { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-menu");

        protected override void BuildBulma()
        {
            ClassBuilder.IsActive = IsActive;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}