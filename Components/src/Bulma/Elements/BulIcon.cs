using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public enum BulIconPosition
    {
        Left,
        Right
    }

    public class BulIcon : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter]
        public BulColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        /// <summary>
        /// Sets the is-left or is-right bulma property
        /// </summary>
        [Parameter]
        public BulIconPosition? Position { get; set; }

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("icon");

        protected string _wrapperClass = String.Empty;

        protected void BuildBulma()
        {
            WrapperClassBuilder.SetSize(Size);
            WrapperClassBuilder.SetTextColor(Color);

            if(Position.HasValue)
            {
                if(Position.Value == BulIconPosition.Left)
                {
                    WrapperClassBuilder.SetIsLeft(true);
                }
                else
                {
                    WrapperClassBuilder.SetIsRight(true);
                }
            }

            _wrapperClass = WrapperClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "span");
            builder.AddAttribute(1, "class", _wrapperClass);

            builder.OpenElement(2, "i");
            builder.AddMultipleAttributes(3, AdditionalAttributes);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}