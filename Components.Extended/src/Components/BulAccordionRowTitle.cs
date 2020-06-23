using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulAccordionRowTitle : BulComponentBase
    {
        [CascadingParameter]
        public BulAccordionRow ParentRow { get; set; }

        [CascadingParameter]
        public Guid RowId { get; set; }

        [CascadingParameter]
        public BulSchemeColor? Color { get; set; }

        [CascadingParameter]
        public bool Active { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected BulmaClassBuilder HeaderClassBuilder { get; set; } = new BulmaClassBuilder("accordion-row-header");

        protected BulmaClassBuilder ArrowClassBuilder { get; set; } = new BulmaClassBuilder("accordion-arrow");

        protected BulmaClassBuilder TitleClassBuilder { get; set; } = new BulmaClassBuilder("accordion-row-title");

        protected string _headerElementClass = String.Empty;
        protected string _arrowElementClass = String.Empty;
        protected string _titleElementClass = String.Empty;

        protected override void BuildBulma()
        {
            HeaderClassBuilder.SetSchemeColor(Color);
            _headerElementClass = HeaderClassBuilder.ToString();

            ArrowClassBuilder.SetSchemeColor(Color);
            ArrowClassBuilder.SetIsActive(Active);
            _arrowElementClass = ArrowClassBuilder.ToString();

            TitleClassBuilder.SetSchemeColor(Color);
            _titleElementClass = TitleClassBuilder.ToString();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "a");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickHandlerAsync));

            builder.AddContent(3, (RenderFragment)((builder2) => {
                builder2.OpenElement(4, "div");
                builder2.AddAttribute(5, "class", _arrowElementClass);
                builder2.CloseElement();

                builder2.OpenComponent<BulCardHeader>(6);
                builder2.AddAttribute(7, "class", _headerElementClass);
                builder.AddAttribute(8, "ChildContent", (RenderFragment)((builder3) => {
                    builder3.OpenComponent<BulCardHeaderTitle>(7);
                    builder3.AddAttribute(9, "class", _titleElementClass);
                    builder3.AddAttribute(10, "ChildContent", ChildContent);
                    builder3.CloseComponent();
                }));
                builder.CloseComponent();
            }));

            builder.CloseElement();
        }

        private async Task OnClickHandlerAsync(MouseEventArgs args)
        {
            ParentRow.OnActiveToggle();
            await OnClick.InvokeAsync(args);
        }
    }
}