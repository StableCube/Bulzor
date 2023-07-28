using System;
using System.Globalization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulSwitch : BulInputBase<bool>
    {
        [Parameter]
        public bool Thin { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool RightToLeft { get; set; }

        [Parameter]
        public string Label { get; set; } = String.Empty;

        protected BulmaClassBuilderExtended InputClassBuilder = new BulmaClassBuilderExtended("switch");

        protected override void BuildBulma()
        {
            InputClassBuilder.SchemeColor = Color;
            InputClassBuilder.Size = Size;
            InputClassBuilder.IsThin = Thin;
            InputClassBuilder.IsRounded = Rounded;
            InputClassBuilder.IsOutlined = Outlined;
            InputClassBuilder.IsRightToLeft = RightToLeft;

            MergeBuilderClassAttribute(InputClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            string id = String.Empty;
            if(AdditionalAttributes.ContainsKey("id"))
            {
                id = AdditionalAttributes["id"].ToString();
            }
            else
            {
                id = Guid.NewGuid().ToString();
                CombinedAdditionalAttributes.Add("id", id);
            }

            CombinedAdditionalAttributes.Add("type", "checkbox");

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "field");

            builder.OpenComponent<InputCheckbox>(1);
            //builder.AddAttribute(2, "class", InputClassBuilder.ClassString);
            //builder.AddAttribute(2, "type", "checkbox");
            builder.AddAttribute(3, "Value", Value);
            builder.AddAttribute(4, "ValueExpression", ValueExpression);
            builder.AddAttribute(5, "ValueChanged", ValueChanged);
            builder.AddAttribute(6, "AdditionalAttributes", CombinedAdditionalAttributes);
            builder.CloseComponent();

            builder.OpenElement(7, "label");
            builder.AddAttribute(8, "for", id);
            builder.AddContent(9, Label);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}