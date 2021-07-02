﻿using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulFileLabel : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("file-label");

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "label");
            builder.AddAttribute(1, "class", ClassBuilder.ClassString);
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.AddContent(3, ChildContent);

            builder.CloseElement();
        }
    }
}