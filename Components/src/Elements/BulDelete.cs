using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulDelete : BulComponentBase
    {
        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("delete");

        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            ClassBuilder.SetSize(Size);

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