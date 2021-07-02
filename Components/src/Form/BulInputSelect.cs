using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulInputSelect<TValue> : BulInputBase<TValue>
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        /// <summary>
        /// Add an icon with the supplied class. For instance "fa fa-globe fa-2x"
        /// </summary>
        [Parameter]
        public string IconClass { get; set; }

        protected BulmaClassBuilder ControlClassBuilder { get; set; } = new BulmaClassBuilder("control");
        protected BulmaClassBuilder SelectClassBuilder { get; set; } = new BulmaClassBuilder("select");
        protected BulmaClassBuilder IconClassBuilder { get; set; } = new BulmaClassBuilder("icon");

        protected override void BuildBulma()
        {
            SelectClassBuilder.Size = Size;
            SelectClassBuilder.IsLoading = Loading;
            SelectClassBuilder.IsRounded = Rounded;

            if(Loading.HasValue == false || Loading.Value == false)
            {
                SelectClassBuilder.SchemeColor = Color;
            }
            else
            {
                SelectClassBuilder.SchemeColor = null;
            }

            if(!string.IsNullOrEmpty(IconClass))
            {
                IconClassBuilder.IsLeft = true;
                IconClassBuilder.Size = Size;
            }

            ControlClassBuilder.HasIconsLeft = !string.IsNullOrEmpty(IconClass);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ControlClassBuilder.ClassString);
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", SelectClassBuilder.ClassString);

            builder.OpenComponent<InputSelect<TValue>>(4);
            builder.AddAttribute(5, "Value", Value);
            builder.AddAttribute(6, "ValueExpression", ValueExpression);
            builder.AddAttribute(7, "ValueChanged", ValueChanged);
            builder.AddAttribute(8, "AdditionalAttributes", AdditionalAttributes);

            builder.AddAttribute(9, "ChildContent", (RenderFragment)((builder2) => {
                builder2.AddContent(10, ChildContent);
            }));

            builder.CloseComponent();
            builder.CloseElement();

            if(!string.IsNullOrEmpty(IconClass))
            {
                builder.OpenElement(12, "span");
                builder.AddAttribute(13, "class", IconClassBuilder.ClassString);

                builder.OpenElement(15, "i");
                builder.AddAttribute(16, "class", IconClass);
                builder.CloseElement();

                builder.CloseElement();
            }

            builder.CloseElement();
        }
    }
}