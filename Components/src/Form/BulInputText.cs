using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components
{
    public class BulInputText : BulInputSingleLineBase<string>
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", WrapperClassBuilder.ClassString);

            builder.OpenComponent<InputText>(2);
            builder.AddAttribute(3, "Value", Value);
            builder.AddAttribute(4, "ValueExpression", ValueExpression);
            builder.AddAttribute(5, "ValueChanged", ValueChanged);
            builder.AddAttribute(6, "AdditionalAttributes", CombinedAdditionalAttributes);
            builder.CloseComponent();

            if(BulIcons != null)
                builder.AddContent(7, BulIcons);

            builder.CloseElement();
        }
    }
}