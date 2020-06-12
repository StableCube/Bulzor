using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Extended
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
        public BulRatio? Ratio { get; set; }

        [Parameter]
        public EventCallback<RibbonImage> OnClick { get; set; }

        protected BulmaClassBuilder InputClassBuilder = new BulmaClassBuilder("image-ribbon-item");
        protected string _elementClass = String.Empty;

        protected override void BuildBulma()
        {
            BulSize size = BulSize.Large;
            int fromCenter = 0;
            if(Image.Index < FocusedIndex)
                fromCenter = FocusedIndex - Image.Index;

            if(Image.Index > FocusedIndex)
                fromCenter = Image.Index - FocusedIndex;

            if(fromCenter == 1)
                size = BulSize.Medium;

            if(fromCenter > 1)
                size = BulSize.Small;

            InputClassBuilder.SetSize(size);
            InputClassBuilder.SetSchemeColor(Color);
            InputClassBuilder.SetIsActive(Active);
            InputClassBuilder.SetIsFocused((FocusedIndex == Image.Index));

            _elementClass = InputClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", _elementClass);
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickHandlerAsync));

            builder.OpenComponent<BulImage>(4);
            builder.AddAttribute(5, "Ratio", Ratio);
            builder.AddAttribute(6, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenElement(7, "div");
                builder2.AddContent(8, (RenderFragment)((builder3) => {
                    builder3.OpenElement(9, "img");
                    builder3.AddAttribute(10, "src", Image.Uri.AbsoluteUri);
                    builder3.CloseElement();
                }));
                builder2.CloseElement();
            }));
            builder.CloseComponent();

            builder.CloseElement();
        }

        private async Task OnClickHandlerAsync(MouseEventArgs args)
        {
            await OnClick.InvokeAsync(Image);
        }
    }
}