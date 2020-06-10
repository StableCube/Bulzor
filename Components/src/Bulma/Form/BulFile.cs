using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulFile : BulComponentBase
    {
        [Parameter]
        public BulPrimaryColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Boxed { get; set; }

        [Parameter]
        public bool? HasName { get; set; }

        [Parameter]
        public bool? FullWidth { get; set; }

        [Parameter]
        public bool? Right { get; set; }

        [Parameter]
        public bool? Centered { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetPrimaryColor(Color);
            ClassBuilder.SetSize(Size);
            ClassBuilder.SetIsBoxed(Boxed);
            ClassBuilder.SetHasName(HasName);
            ClassBuilder.SetIsFullWidth(FullWidth);
            ClassBuilder.SetIsRight(Right);
            ClassBuilder.SetIsCentered(Centered);
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", _elementClass);
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}