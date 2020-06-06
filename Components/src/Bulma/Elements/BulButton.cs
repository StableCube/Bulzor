using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulButton : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public BulPrimaryColor? Color { get; set; }

        [Parameter]
        public BulColor? TextColor { get; set; }

        [Parameter]
        public BulTextSize? TextSize { get; set; }

        [Parameter]
        public BulTextWeight? TextWeight { get; set; }

        [Parameter]
        public bool? Selected { get; set; }

        [Parameter]
        public bool? Active { get; set; }

        [Parameter]
        public bool? Focused { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("button");

        protected string _buttonClass = String.Empty;

        protected void BuildBulma()
        {
            ClassBuilder.SetIsLoading(Loading);
            ClassBuilder.SetPrimaryColor(Color);
            ClassBuilder.SetTextColor(TextColor);
            ClassBuilder.SetTextSize(TextSize);
            ClassBuilder.SetTextWeight(TextWeight);
            ClassBuilder.SetIsSelected(Selected);
            ClassBuilder.SetIsActive(Active);
            ClassBuilder.SetIsFocused(Focused);

            _buttonClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "button");
            builder.AddMultipleAttributes(1, AdditionalAttributes);

            builder.AddAttribute(2, "class", _buttonClass);
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(
              this, 
              OnClick
            ));
            
            builder.AddContent(4, ChildContent);

            builder.CloseElement();
        }
    }
}