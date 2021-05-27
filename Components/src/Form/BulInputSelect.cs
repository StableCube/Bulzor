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

        protected string _controlClass = String.Empty;
        protected string _selectClass = String.Empty;
        protected string _iconClass = String.Empty;

        protected override void BuildBulma()
        {
            SelectClassBuilder.SetSize(Size);
            SelectClassBuilder.SetIsLoading(Loading);
            SelectClassBuilder.SetIsRounded(Rounded);

            if(Loading.HasValue == false || Loading.Value == false)
            {
                SelectClassBuilder.SetSchemeColor(Color);
            }
            else
            {
                SelectClassBuilder.SetSchemeColor(null);
            }

            if(!string.IsNullOrEmpty(IconClass))
            {
                IconClassBuilder.SetIsLeft(true);
                IconClassBuilder.SetSize(Size);
            }

            ControlClassBuilder.SetHasIconsLeft(!string.IsNullOrEmpty(IconClass));

            _controlClass = ControlClassBuilder.ToString();
            _selectClass = SelectClassBuilder.ToString();
            _iconClass = IconClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", _controlClass);
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", _selectClass);

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
                builder.AddAttribute(13, "class", _iconClass);

                builder.OpenElement(15, "i");
                builder.AddAttribute(16, "class", IconClass);
                builder.CloseElement();

                builder.CloseElement();
            }

            builder.CloseElement();
        }
    }
}