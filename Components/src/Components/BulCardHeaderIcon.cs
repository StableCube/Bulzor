﻿using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulCardHeaderIcon : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("card-header-icon");

        protected override void BuildBulma()
        {
            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "span");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}