using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulBreadcrumb : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        /// Sets the is-centered Bulma class
        /// </summary>
        [Parameter]
        public bool? Centered { get; set; }

        /// <summary>
        /// Sets the separator Bulma class
        /// </summary>
        [Parameter]
        public BulSeparator? Separator { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("breadcrumb");

        protected string _elementClass = String.Empty;

        protected void BuildBulma()
        {
            ClassBuilder.SetIsCentered(Centered);
            ClassBuilder.SetSize(Size);
            ClassBuilder.SetSeparator(Separator);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "nav");
            builder.AddAttribute(1, "class", _elementClass);
            builder.AddContent(2, ChildContent);

            builder.CloseElement();
        }
    }
}