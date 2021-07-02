using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulDropdown : BulComponentBase
    {
        [Parameter]
        public bool Active { get; set; }

        /// <summary>
        /// Right align menu
        /// </summary>
        [Parameter]
        public bool Right { get; set; }

        /// <summary>
        /// Act as a dropup menu
        /// </summary>
        [Parameter]
        public bool Up { get; set; }

        /// <summary>
        /// Menu activates on hover
        /// </summary>
        [Parameter]
        public bool Hoverable { get; set; }

        [Parameter]
        public RenderFragment BulDropdownTrigger { get; set; }

        [Parameter]
        public RenderFragment BulDropdownContent { get; set; }
        
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickOut { get; set; }

        protected BulmaClassBuilder BackgroundClassBuilder { get; set; } = new BulmaClassBuilder("dropdown-bg");
        protected BulmaClassBuilder DropdownClassBuilder { get; set; } = new BulmaClassBuilder("dropdown");

        protected override void BuildBulma()
        {
            BackgroundClassBuilder.IsHidden = true;

            DropdownClassBuilder.IsActive = Active;
            DropdownClassBuilder.IsRight = Right;
            DropdownClassBuilder.IsHoverable = Hoverable;
            DropdownClassBuilder.IsUp = Up;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(Active)
            {
                builder.OpenRegion(0);
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "class", BackgroundClassBuilder.ClassString);
                builder.AddAttribute(3, "style", "position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;");
                builder.AddAttribute(4, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickOut));
                builder.CloseElement();
                builder.CloseRegion();
            }

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(DropdownClassBuilder.ClassString));

            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", "dropdown-trigger");
            builder.AddContent(5, BulDropdownTrigger);
            builder.CloseElement();

            builder.OpenElement(6, "div");
            builder.AddAttribute(7, "class", "dropdown-menu");
            builder.OpenElement(8, "div");
            builder.AddAttribute(9, "class", "dropdown-content");
            
            builder.AddContent(10, BulDropdownContent);
            builder.CloseElement();
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}