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
        public int Multiple { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("select");

        private readonly Type _nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));
        protected string _wrapperClass = String.Empty;

        protected override void BuildBulma()
        {
            WrapperClassBuilder.SetSize(Size);
            WrapperClassBuilder.SetIsLoading(Loading);
            WrapperClassBuilder.SetPrimaryColor(Color);
            WrapperClassBuilder.SetSize(Size);
            WrapperClassBuilder.SetIsRounded(Rounded);

            if(Multiple > 1)
                WrapperClassBuilder.SetIsMultiple(true);

            _wrapperClass = WrapperClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(Multiple > 1)
            {
                if(!AdditionalAttributes.ContainsKey("multiple"))
                    AdditionalAttributes.Add("multiple", String.Empty);

                if(!AdditionalAttributes.ContainsKey("size"))
                    AdditionalAttributes.Add("size", Multiple);  
            }

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "control");
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", _wrapperClass);

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
            builder.CloseElement();
        }
    }
}