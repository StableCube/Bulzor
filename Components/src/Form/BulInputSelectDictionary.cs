using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components
{
    public class BulInputSelectDictionary<TKey, TValue> : BulComponentBase
    {
        /// <summary>
        /// Options to be selected from
        /// </summary>
        [Parameter]
        public IDictionary<TKey, TValue> Options { get; set; } = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        [Parameter]
        public KeyValuePair<TKey, TValue> Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<KeyValuePair<TKey, TValue>> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter]
        public Expression<Func<KeyValuePair<TKey, TValue>>> ValueExpression { get; set; }

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

        [Parameter]
        public bool? Expanded { get; set; }

        [Parameter]
        public bool? FullWidth { get; set; }

        protected BulmaClassBuilder ControlClassBuilder { get; set; } = new BulmaClassBuilder("control");
        protected BulmaClassBuilder SelectClassBuilder { get; set; } = new BulmaClassBuilder("select");
        protected BulmaClassBuilder IconClassBuilder { get; set; } = new BulmaClassBuilder("icon");

        private Dictionary<string, string> _stringDic = new Dictionary<string, string>();
        private Dictionary<string, TKey> _dicMap = new Dictionary<string, TKey>();

        private string ValueInternal { get; set; }
        private Expression<Func<string>> ValueExpressionInternal { get; set; }
        private TKey InitialValue { get; set; } = default(TKey);
        
        protected override void BuildBulma()
        {
            SelectClassBuilder.Size = Size;
            SelectClassBuilder.IsLoading = Loading;
            SelectClassBuilder.IsRounded = Rounded;
            SelectClassBuilder.IsFullWidth = FullWidth;

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

            ControlClassBuilder.IsExpanded = Expanded;
            ControlClassBuilder.HasIconsLeft = !string.IsNullOrEmpty(IconClass);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            ValueExpressionInternal = () => ValueInternal;
            if(EqualityComparer<TKey>.Default.Equals(InitialValue, default(TKey)))
                InitialValue = Value.Key;

            _stringDic.Clear();
            _dicMap.Clear();
            foreach (var dicValPair in Options)
            {
                string stringKey = dicValPair.Key.ToString();
                _stringDic.Add(stringKey, dicValPair.Value.ToString());
                _dicMap.Add(stringKey, dicValPair.Key);
            }

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ControlClassBuilder.ClassString);
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", SelectClassBuilder.ClassString);

            BuildSelect(builder, 4);

            builder.CloseElement();

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
                    
                    if(!ReferenceEquals(InitialValue, null) && EqualityComparer<TKey>.Default.Equals(InitialValue, _dicMap[pair.Key]))
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
            builder.AddAttribute(1, "class", IconClassBuilder.ClassString);

            builder.OpenElement(2, "i");
            builder.AddAttribute(3, "class", IconClass);
            builder.CloseElement();

            builder.CloseElement();
            builder.CloseRegion();
        }

        private async void InputValueChangedInternalHandler(string value)
        {
            var sourceKey = (TKey)_dicMap[value];

            await ValueChanged.InvokeAsync(new KeyValuePair<TKey, TValue>(sourceKey, Options[sourceKey]));
        }
    }
}