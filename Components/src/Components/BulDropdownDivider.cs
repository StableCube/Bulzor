﻿using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components
{
    public class BulDropdownDivider : BulComponentBase
    {
        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("dropdown-divider");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "hr");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));

            builder.CloseElement();
        }
    }
}