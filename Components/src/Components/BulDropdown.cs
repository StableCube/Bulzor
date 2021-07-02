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
            DropdownClassBuilder.IsActive = Active;
            DropdownClassBuilder.IsRight = Right;
            DropdownClassBuilder.IsHoverable = Hoverable;
            DropdownClassBuilder.IsUp = Up;

            MergeBuilderClassAttribute(DropdownClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(Active)
            {
                builder.OpenRegion(0);
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "class", BackgroundClassBuilder.ClassString);
                builder.AddAttribute(3, "style", "position: fixed; top: 0px; left: 0px; width: 100%; height: 100%; opacity: 0;");
                builder.AddAttribute(4, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickOut));
                builder.CloseElement();
                builder.CloseRegion();
            }

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);

            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "dropdown-trigger");
            builder.AddContent(4, BulDropdownTrigger);
            builder.CloseElement();

            builder.OpenElement(5, "div");
            builder.AddAttribute(6, "class", "dropdown-menu");
            builder.OpenElement(7, "div");
            builder.AddAttribute(8, "class", "dropdown-content");
            
            builder.AddContent(9, BulDropdownContent);
            builder.CloseElement();
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}