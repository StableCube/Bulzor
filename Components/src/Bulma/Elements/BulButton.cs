using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulButton : BulComponentBase
    {
        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulColor? TextColor { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Active { get; set; }

        [Parameter]
        public bool? Focused { get; set; }

        [Parameter]
        public bool? Hovered { get; set; }

        [Parameter]
        public bool? Outlined { get; set; }

        [Parameter]
        public bool? FullWidth { get; set; }

        [Parameter]
        public bool? Inverted { get; set; }

        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("button");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetIsLoading(Loading);
            ClassBuilder.SetSchemeColor(Color);
            ClassBuilder.SetTextColor(TextColor);
            ClassBuilder.SetSize(Size);
            ClassBuilder.SetIsActive(Active);
            ClassBuilder.SetIsFocused(Focused);
            ClassBuilder.SetIsHovered(Hovered);
            ClassBuilder.SetIsOutlined(Outlined);
            ClassBuilder.SetIsFullWidth(FullWidth);
            ClassBuilder.SetIsInverted(Inverted);
            ClassBuilder.SetIsRounded(Rounded);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "button");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
            builder.AddContent(4, ChildContent);
            builder.CloseElement();
        }
    }
}