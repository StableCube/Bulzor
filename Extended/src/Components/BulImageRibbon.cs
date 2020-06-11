using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        public int Focus { get; set; }

        [Parameter]
        public BulRatio? Ratio { get; set; }

        private RibbonImage[] ImageObjs { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("image-ribbon");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            _elementClass = InputClassBuilder.ToString();
        }

        protected override void OnParametersSet()
        {
            if (DisplayCount % 2 == 0)
                throw new IndexOutOfRangeException("DisplayCount must be an odd number");

            if (Images == null || Images.Length == 0)
                throw new IndexOutOfRangeException("Image count must be greater than zero");

            Math.Clamp(Focus, 0, Images.Length - 1);

            if(ImageObjs == null || ImageObjs.Length != Images.Length)
                ImageObjs = new RibbonImage[Images.Length];
            
            for (int i = 0; i < Images.Length; i++)
                ImageObjs[i] = new RibbonImage(i, Images[i]);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", _elementClass);

            foreach (var image in ImageObjs)
            {
                builder.OpenRegion(3);

                builder.OpenComponent<BulImageRibbonItem>(0);
                builder.AddAttribute(1, "Image", image);
                builder.AddAttribute(2, "Color", Color);
                builder.AddAttribute(3, "Focused", (Focus == image.Index));
                builder.AddAttribute(4, "Active", IsVisible(image.Index));
                builder.AddAttribute(5, "Ratio", Ratio);
                builder.CloseComponent();

                builder.CloseRegion();
            }

            builder.CloseElement();
        }

        private bool IsVisible(int index)
        {
            int halfDisp = (int)Math.Ceiling((double)(DisplayCount - 1) / 2);
            int overCount = Images.Length - Focus;
            int beginIdx = Focus - halfDisp;
            if(Focus < halfDisp)
                beginIdx = 0;
            
            if(overCount <= halfDisp)
                beginIdx = Images.Length - DisplayCount;

            return (index >= beginIdx && index <= beginIdx + DisplayCount - 1);
        }
    }
}