using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulTagDelete : BulComponentBase
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
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("tag");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetSize(Size);
            ClassBuilder.SetSchemeColor(Color);
            ClassBuilder.SetIsLight(Light);
            ClassBuilder.SetIsRounded(Rounded);
            ClassBuilder.SetIsDelete(true);

            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "a");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
            builder.CloseElement();
        }
    }
}