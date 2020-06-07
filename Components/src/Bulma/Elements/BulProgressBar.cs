using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulProgressBar : BulComponentBase
    {
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

        protected override void BuildBulma()
        {
            ClassBuilder.SetPrimaryColor(Color);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "progress");
            builder.AddAttribute(1, "class", MergeClassAttribute(_elementClass));
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