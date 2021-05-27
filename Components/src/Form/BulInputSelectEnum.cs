using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulInputSelectEnum<TValue> : BulComponentBase
    {
        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        [Parameter]
        public TValue Value { get; set; }

        /// <summary>
        /// Optionally set formatted display names
        /// </summary>
        [Parameter]
        public IDictionary<TValue, string> DisplayNames { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        /// <summary>
        /// Add an icon with the supplied class. For instance "fa fa-globe fa-2x"
        /// </summary>
        [Parameter]
        public string IconClass { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public bool? Loading { get; set; }

        protected BulmaClassBuilder ControlClassBuilder { get; set; } = new BulmaClassBuilder("control");
        protected BulmaClassBuilder SelectClassBuilder { get; set; } = new BulmaClassBuilder("select");
        protected BulmaClassBuilder IconClassBuilder { get; set; } = new BulmaClassBuilder("icon");

        protected string _controlClass = String.Empty;
        protected string _selectClass = String.Empty;
        protected string _iconClass = String.Empty;

        private Dictionary<string, string> _stringDic = new Dictionary<string, string>();
        private Dictionary<string, TValue> _dicMap = new Dictionary<string, TValue>();

        private string ValueInternal { get; set; }
        private Expression<Func<string>> ValueExpressionInternal { get; set; }
        private TValue InitialValue { get; set; } = default(TValue);

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

            if(Value == null || !Value.GetType().IsEnum)
                throw new TypeLoadException("Value must be an Enum type");
            
            ValueExpressionInternal = () => ValueInternal;
            if(EqualityComparer<TValue>.Default.Equals(InitialValue, default(TValue)))
                InitialValue = Value;

            BuildDataMap();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", _controlClass);
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", _selectClass);

            BuildSelect(builder, 4);

            if(!string.IsNullOrEmpty(IconClass))
            {
                BuildIcon(builder, 5);
            }

            builder.CloseElement();
        }

        private void BuildSelect(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);

            builder.OpenComponent<InputSelect<string>>(0);
            builder.AddAttribute(1, "Value", ValueInternal);
            builder.AddAttribute(2, "ValueExpression", ValueExpressionInternal);
            builder.AddAttribute(3, "ValueChanged", EventCallback.Factory.Create<string>(this, InputValueChangedInternalHandler));
            builder.AddAttribute(4, "AdditionalAttributes", AdditionalAttributes);

            BuildOptions(builder, 5);

            builder.CloseComponent();
            builder.CloseElement();

            builder.CloseRegion();
        }

        private void BuildOptions(RenderTreeBuilder builder, int index)
        {
            builder.AddAttribute(index, "ChildContent", (RenderFragment)((builder2) => {
                int i = index;
                foreach (var pair in _stringDic)
                {
                    builder2.OpenRegion(i);

                    builder2.OpenElement(0, "option");
                    builder2.AddAttribute(1, "value", pair.Key);
                    
                    if(!ReferenceEquals(InitialValue, null) && EqualityComparer<TValue>.Default.Equals(InitialValue, _dicMap[pair.Key]))
                    {
                        builder2.AddAttribute(2, "selected");
                    }

                    builder2.AddContent(3, pair.Value);
                    builder2.CloseElement();

                    builder2.CloseRegion();
                    i++;
                }
            }));
        }

        private void BuildIcon(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);

            builder.OpenElement(0, "span");
            builder.AddAttribute(1, "class", _iconClass);

            builder.OpenElement(2, "i");
            builder.AddAttribute(3, "class", IconClass);
            builder.CloseElement();

            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildDataMap()
        {
            _stringDic.Clear();
            _dicMap.Clear();
            foreach (int enumI in Enum.GetValues(typeof(TValue)))
            {
                string stringKey = ((int)enumI).ToString();
                string stringVal = Enum.GetName(typeof(TValue), enumI);
                TValue enumVal = (TValue)Enum.ToObject(typeof(TValue) , enumI);

                if(DisplayNames != null && DisplayNames.ContainsKey(enumVal))
                {
                    _stringDic.Add(stringKey, DisplayNames[enumVal]);
                }
                else
                {
                    _stringDic.Add(stringKey, stringVal);
                }

                _dicMap.Add(stringKey, enumVal);
            }
        }

        private async void InputValueChangedInternalHandler(string value)
        {
            var sourceValue = (TValue)_dicMap[value];

            await ValueChanged.InvokeAsync(sourceValue);
        }
    }
}