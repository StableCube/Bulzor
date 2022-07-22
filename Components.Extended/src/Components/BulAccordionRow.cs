using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulAccordionRow : BulComponentBase
    {
        [CascadingParameter]
        public BulAccordion ParentRoot { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public EventCallback<Guid> OnExpand { get; set; }

        [Parameter]
        public EventCallback<Guid> OnCollapse { get; set; }

        public Guid RowId { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("accordion-row");

        private bool _cachedActiveValue;

        public BulAccordionRow()
        {
            RowId = Guid.NewGuid();

            _cachedActiveValue = Active;
        }

        protected override void BuildBulma()
        {
            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenComponent<CascadingValue<BulAccordionRow>>(0);
            builder.AddAttribute(1, "Value", this);
            builder.AddAttribute(2, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenComponent<CascadingValue<Guid>>(3);
                builder2.AddAttribute(4, "Value", RowId);
                builder2.AddAttribute(5, "ChildContent", (RenderFragment)((builder3) => {
                    builder3.OpenComponent<CascadingValue<bool>>(6);
                    builder3.AddAttribute(7, "Value", Active);
                    builder3.AddAttribute(8, "ChildContent", (RenderFragment)((builder4) => {
                        builder4.OpenComponent<BulCard>(9);
                        builder4.AddMultipleAttributes(10, CombinedAdditionalAttributes);
                        builder4.AddAttribute(11, "ChildContent", ChildContent);
                        builder4.CloseComponent();
                    }));
                    builder3.CloseComponent();
                }));
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
        }

        protected override void OnInitialized()
        {
            if(Active && ParentRoot.ActiveRow.Equals(Guid.Empty))
                ParentRoot.OnRowActivated(RowId);
        }

        protected override async Task OnParametersSetAsync()
        {
            if(Active && !ParentRoot.AllowMultiple && !ParentRoot.ActiveRow.Equals(RowId))
            {
                Active = false;
                StateHasChanged();
            }

            if(_cachedActiveValue && !Active)
                await OnCollapse.InvokeAsync(RowId);

            if(!_cachedActiveValue && Active)
                await OnExpand.InvokeAsync(RowId);

            _cachedActiveValue = Active;
        }

        public async void OnActiveToggle()
        {
            if(!Active)
                ParentRoot.OnRowActivated(RowId);

            if(ParentRoot.AllowMultiple || ParentRoot.ActiveRow.Equals(RowId))
            {
                Active = !Active;
                StateHasChanged();
            }

            if(_cachedActiveValue && !Active)
                await OnCollapse.InvokeAsync(RowId);

            if(!_cachedActiveValue && Active)
                await OnExpand.InvokeAsync(RowId);

            _cachedActiveValue = Active;
        }
    }
}