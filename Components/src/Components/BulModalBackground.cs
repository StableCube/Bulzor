using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulModalBackground : BulComponentBase
    {
        [Parameter]
        public bool ShowBackground { get; set; } = true;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("modal-background");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = ClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(_elementClass));
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
            builder.CloseElement();
        }
    }
}