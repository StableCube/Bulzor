using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

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
        
        protected BulmaClassBuilder DropdownClassBuilder { get; set; } = new BulmaClassBuilder("dropdown");

        protected string _dropdownClass = String.Empty;

        protected override void BuildBulma()
        {
            DropdownClassBuilder.SetIsActive(Active);
            DropdownClassBuilder.SetIsRight(Right);
            DropdownClassBuilder.SetIsHoverable(Hoverable);
            DropdownClassBuilder.SetIsUp(Up);
            
            _dropdownClass = DropdownClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_dropdownClass));

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