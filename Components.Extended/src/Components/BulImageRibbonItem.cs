using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulImageRibbonItem : BulComponentBase
    {
        [CascadingParameter]
        public int FocusedIndex { get; set; }

        [Parameter]
        public RibbonImage Image { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public BulRatio Ratio { get; set; } = BulRatio.R1by1;

        [Parameter]
        public EventCallback<BulImageRibbonItemClickEventArgs> OnClick { get; set; }

        protected BulmaClassBuilder RootClassBuilder = new BulmaClassBuilder("image-ribbon-item");
        protected BulmaClassBuilder WrapClassBuilder = new BulmaClassBuilder("image");
        protected BulmaClassBuilder ImageClassBuilder = new BulmaClassBuilder();

        protected override void BuildBulma()
        {
            RootClassBuilder.Size = GetSize();
            RootClassBuilder.SchemeColor = Color;
            if(Active)
            {
                RootClassBuilder.IsActive = true;
                RootClassBuilder.IsHidden = false;
            }
            else
            {
                RootClassBuilder.IsActive = false;
                RootClassBuilder.IsHidden = true;
            }
            
            RootClassBuilder.IsFocused = (FocusedIndex == Image.Index);

            WrapClassBuilder.Ratio = Ratio;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", RootClassBuilder.ClassString);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickHandlerAsync));

            builder.AddContent(3, (RenderFragment)((builder2) => {
                builder2.OpenElement(4, "figure");
                builder2.AddAttribute(5, "class", WrapClassBuilder.ClassString);
                builder2.AddContent(6, (RenderFragment)((builder3) => {
                    builder3.OpenElement(7, "img");
                    builder3.AddAttribute(8, "class", ImageClassBuilder.ClassString);
                    builder3.AddAttribute(9, "src", Image.Uri.AbsoluteUri);
                    builder3.CloseElement();
                }));
                builder2.CloseElement();
            }));

            builder.CloseElement();
        }

        private async Task OnClickHandlerAsync(MouseEventArgs args)
        {
            await OnClick.InvokeAsync(new BulImageRibbonItemClickEventArgs(args, Image));
        }

        /// <summary>
        /// Get the  number of elements away from focused index this item is
        /// </summary>
        private int DistanceFromCenter()
        {
            int fromCenter = 0;
            if(Image.Index < FocusedIndex)
                fromCenter = FocusedIndex - Image.Index;

            if(Image.Index > FocusedIndex)
                fromCenter = Image.Index - FocusedIndex;

            return fromCenter;
        }

        /// <summary>
        /// Uses the position in the ribbon to find this items size
        /// </summary>
        private BulSize GetSize()
        {
            BulSize size = BulSize.Large;
            int fromCenter = DistanceFromCenter();

            if(fromCenter == 1)
                size = BulSize.Medium;

            if(fromCenter == 2)
                size = BulSize.Normal;

            if(fromCenter > 2)
                size = BulSize.Small;
            
            return size;
        }
    }
}