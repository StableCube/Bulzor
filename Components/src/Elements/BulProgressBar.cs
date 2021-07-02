﻿using System;
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
        public double Value { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("progress");

        protected override void BuildBulma()
        {
            ClassBuilder.SchemeColor = Color;
            ClassBuilder.Size = Size;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "progress");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));
            builder.AddAttribute(3, "max", "100");

            if(Indeterminate == false)
            {
                Value = Math.Clamp(Value, 0, 100);

                builder.AddAttribute(4, "value", Value);
            }

            builder.CloseElement();
        }
    }
}