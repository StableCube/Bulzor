﻿using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTag : BulComponentBase
    {
        [Parameter]
        public BulSchemeColor? Color { get; set; }

        /// <summary>
        /// Use light version of color
        /// </summary>
        [Parameter]
        public bool? Light { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("tag");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetSize(Size);
            ClassBuilder.SetSchemeColor(Color);
            ClassBuilder.SetIsLight(Light);
            ClassBuilder.SetIsRounded(Rounded);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "span");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}