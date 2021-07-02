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

        protected override void BuildBulma()
        {
            ClassBuilder.HasDropdown = Dropdown;
            ClassBuilder.HasDropdownUp = DropdownUp;
            ClassBuilder.IsHoverable = Hoverable;
            ClassBuilder.IsActive = Active;
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