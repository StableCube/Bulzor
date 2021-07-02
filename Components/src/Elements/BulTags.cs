﻿using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulTags : BulComponentBase
    {
        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? HasAddons { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("tags");

        protected override void BuildBulma()
        {
            ClassBuilder.SchemeColor = Color;
            ClassBuilder.SizeChild = Size;
            ClassBuilder.HasAddons = HasAddons;
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