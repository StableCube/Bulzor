using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarBurger : BulComponentBase
    {
        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-burger");

        protected override void BuildBulma()
        {
            ClassBuilder.IsActive = Active;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "a");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", MergeClassAttribute(ClassBuilder.ClassString));
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));

            builder.AddContent(4, (RenderFragment)((builder2) => {
                builder2.OpenElement(5, "span");
                builder2.CloseElement();

                builder2.OpenElement(6, "span");
                builder2.CloseElement();
                                
                builder2.OpenElement(7, "span");
                builder2.CloseElement();
            }));

            builder.CloseElement();
        }
    }
}