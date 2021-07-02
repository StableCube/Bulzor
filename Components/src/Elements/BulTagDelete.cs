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

        protected override void BuildBulma()
        {
            ClassBuilder.Size = Size;
            ClassBuilder.SchemeColor = Color;
            ClassBuilder.IsLight = Light;
            ClassBuilder.IsRounded = Rounded;
            ClassBuilder.IsDelete = true;

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "a");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
            builder.CloseElement();
        }
    }
}