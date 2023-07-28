using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components
{
    public class BulInputTextArea : BulInputBase<string>
    {
        [Parameter]
        public bool? Expanded { get; set; }

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("control");

        protected BulmaClassBuilder InputClassBuilder { get; set; } = new BulmaClassBuilder("textarea");

        protected override void BuildBulma()
        {
            WrapperClassBuilder.Size = Size;
            WrapperClassBuilder.IsLoading = Loading;
            WrapperClassBuilder.IsExpanded = Expanded;

            InputClassBuilder.SchemeColor = Color;
            InputClassBuilder.Size = Size;

            MergeBuilderClassAttribute(InputClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", WrapperClassBuilder.ClassString);

            builder.OpenComponent<InputTextArea>(2);
            builder.AddAttribute(3, "Value", Value);
            builder.AddAttribute(4, "ValueExpression", ValueExpression);
            builder.AddAttribute(5, "ValueChanged", ValueChanged);
            builder.AddAttribute(6, "AdditionalAttributes", CombinedAdditionalAttributes);
            builder.CloseComponent();

            builder.CloseElement();
        }
    }
}