using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components
{
    public class BulInputCheckbox : BulInputBase<bool>
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("checkbox");

        protected string _wrapperClass = String.Empty;

        protected void BuildBulma()
        {
            _wrapperClass = WrapperClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "label");
            builder.AddAttribute(1, "class", _wrapperClass);

            builder.OpenComponent<InputCheckbox>(2);
            builder.AddAttribute(3, "Value", Value);
            builder.AddAttribute(4, "ValueExpression", ValueExpression);
            builder.AddAttribute(5, "ValueChanged", ValueChanged);
            builder.AddAttribute(6, "AdditionalAttributes", AdditionalAttributes);
            builder.CloseComponent();

            builder.AddContent(7, ChildContent);

            builder.CloseElement();
        }
    }
}