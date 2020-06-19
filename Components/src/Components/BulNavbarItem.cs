using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarItem : BulComponentBase
    {
        [Parameter]
        public bool Dropdown { get; set; }

        [Parameter]
        public bool DropdownUp { get; set; }

        [Parameter]
        public bool Hoverable { get; set; }

        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-item");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetHasDropdown(Dropdown);
            ClassBuilder.SetHasDropdownUp(DropdownUp);
            ClassBuilder.SetIsHoverable(Hoverable);
            ClassBuilder.SetIsActive(Active);
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}