using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components
{
    public class BulInputTextArea : BulInputBase<string>
    {
        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("control");

        protected BulmaClassBuilder InputClassBuilder { get; set; } = new BulmaClassBuilder("textarea");

        protected string _wrapperClass = String.Empty;
        protected string _inputClass = String.Empty;

        protected override void BuildBulma()
        {
            WrapperClassBuilder.SetSize(Size);
            WrapperClassBuilder.SetIsLoading(Loading);

            _wrapperClass = WrapperClassBuilder.ToString();

            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetSize(Size);
            _inputClass = InputClassBuilder.ToString();
            
            if(AdditionalAttributes.ContainsKey("class"))
            {
                AdditionalAttributes["class"] =  _inputClass;
            }
            else
            {
                AdditionalAttributes.Add("class", _inputClass);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", _wrapperClass);

            builder.OpenComponent<InputTextArea>(2);
            builder.AddAttribute(3, "Value", Value);
            builder.AddAttribute(4, "ValueExpression", ValueExpression);
            builder.AddAttribute(5, "ValueChanged", ValueChanged);
            builder.AddAttribute(6, "AdditionalAttributes", AdditionalAttributes);
            builder.CloseComponent();

            builder.CloseElement();
        }
    }
}