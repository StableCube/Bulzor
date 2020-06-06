using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulProgressBar : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// Progress as a percentage from 0 - 100
        /// </summary>
        [Parameter]
        public double Progress { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public BulPrimaryColor? Color { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("progress");

        protected string _elementClass = String.Empty;

        protected void BuildBulma()
        {
            ClassBuilder.SetPrimaryColor(Color);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "progress");
            builder.AddAttribute(1, "class", _elementClass);
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.AddAttribute(3, "max", "100");

            if(Indeterminate == false)
            {
                Progress = Math.Clamp(Progress, 0, 100);

                builder.AddAttribute(4, "value", Progress);
            }

            builder.CloseElement();
        }
    }
}