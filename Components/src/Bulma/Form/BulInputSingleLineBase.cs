using System;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public abstract class BulInputSingleLineBase<TValue> : BulInputBase<TValue>
    {
        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public bool HasIconsLeft { get; set; }

        [Parameter]
        public bool HasIconsRight { get; set; }

        [Parameter]
        public bool? Static { get; set; }

        [Parameter]
        public RenderFragment BulIcons { get; set; }

        protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("control");
        protected BulmaClassBuilder InputClassBuilder { get; set; } = new BulmaClassBuilder("input");

        protected string _wrapperClass = String.Empty;

        protected string _inputClass = String.Empty;

        protected override void BuildBulma()
        {
            WrapperClassBuilder.SetSize(Size);
            WrapperClassBuilder.SetIsLoading(Loading);
            WrapperClassBuilder.SetIsStatic(Static);
            WrapperClassBuilder.SetHasIconsLeft(HasIconsLeft);
            WrapperClassBuilder.SetHasIconsRight(HasIconsRight);

            _wrapperClass = WrapperClassBuilder.ToString();

            InputClassBuilder.SetPrimaryColor(Color);
            InputClassBuilder.SetSize(Size);
            InputClassBuilder.SetIsRounded(Rounded);
            _inputClass = InputClassBuilder.ToString();

            if(AdditionalAttributes.ContainsKey("class"))
            {
                AdditionalAttributes["class"] = _inputClass;
            }
            else
            {
                AdditionalAttributes.Add("class", _inputClass);
            }
        }
    }
}