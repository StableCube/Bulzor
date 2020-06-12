using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Extended
{
    public class BulImageRibbon : BulComponentBase
    {
        [Parameter]
        public Uri[] Images { get; set; }

        [Parameter]
        public int DisplayCount { get; set; } = 5;

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }
        
        [Parameter]
        public BulRatio? Ratio { get; set; }

        private RibbonImage[] RibbonImages { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("image-ribbon");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = InputClassBuilder.ToString();
        }

        protected override void OnParametersSet()
        {
            if (Images == null || Images.Length == 0)
                throw new IndexOutOfRangeException("Image count must be greater than zero");

            Value = Math.Clamp(Value, 0, Images.Length - 1);
            DisplayCount = Math.Clamp(DisplayCount, 1, Images.Length);

            if(RibbonImages == null || RibbonImages.Length != Images.Length)
                RibbonImages = new RibbonImage[Images.Length];
            
            for (int i = 0; i < Images.Length; i++)
                RibbonImages[i] = new RibbonImage(i, Images[i]);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", _elementClass);

            builder.OpenComponent<CascadingValue<int>>(3);
            builder.AddAttribute(4, "Value", Value);
            builder.AddAttribute(5, "ChildContent", (RenderFragment)((builder2) => {
                foreach (var image in RibbonImages)
                {
                    builder2.OpenRegion(6);

                    builder2.OpenComponent<BulImageRibbonItem>(0);
                    builder2.AddAttribute(1, "Image", image);
                    builder2.AddAttribute(2, "Color", Color);
                    builder2.AddAttribute(3, "Active", IsVisible(image.Index));
                    builder2.AddAttribute(4, "Ratio", Ratio);
                    builder2.AddAttribute<RibbonImage>(5, "OnClick", EventCallback.Factory.Create<RibbonImage>(this, OnClickHandlerAsync));
                    builder2.CloseComponent();

                    builder2.CloseRegion();
                }
            }));
            builder.CloseComponent();

            builder.CloseElement();
        }

        private bool IsVisible(int index)
        {
            int halfDisp = (int)Math.Ceiling((double)(DisplayCount - 1) / 2);
            int overCount = Images.Length - Value;
            int beginIdx = Value - halfDisp;
            if(Value < halfDisp)
                beginIdx = 0;
            
            if(overCount <= halfDisp)
                beginIdx = Images.Length - DisplayCount;

            return (index >= beginIdx && index <= beginIdx + DisplayCount - 1);
        }

        private async Task OnClickHandlerAsync(RibbonImage args)
        {
            Value = args.Index;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}