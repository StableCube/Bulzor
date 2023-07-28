using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components
{
    public class BulPagination : BulComponentBase
    {
        [Parameter]
        public int PageCount { get; set; }

        [Parameter]
        public int CurrentPage { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool? Centered { get; set; }

        [Parameter]
        public bool? Rounded { get; set; }

        [Parameter]
        public string PreviousLinkText { get; set; } = "Previous";

        [Parameter]
        public string NextLinkText { get; set; } = "Next";

        [Parameter]
        public int PagePadLinks { get; set; } = 4;

        [Parameter]
        public EventCallback<BulPageClickEventArgs> OnPageSelected { get; set; }

        protected BulmaClassBuilder NavClassBuilder { get; set; } = new BulmaClassBuilder("pagination");

        List<int> _pages = new List<int>();

        protected override void BuildBulma()
        {
            NavClassBuilder.IsRounded = Rounded;
            NavClassBuilder.Size = Size;
            NavClassBuilder.IsCentered = Centered;

            MergeBuilderClassAttribute(NavClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "nav");
            builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
            builder.AddAttribute(2, "role", "navigation");

            BuildPreviousLink(builder, 3);
            BuildNextLink(builder, 4);
            BuildLinkList(builder, 5);

            builder.CloseElement();
        }

        private void BuildPreviousLink(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "class", "pagination-previous");
            if(CurrentPage <= 1)
                builder.AddAttribute(2, "disabled");
            
            builder.AddAttribute(3, "onclick", 
                EventCallback.Factory.Create<MouseEventArgs>(this, async (args) => await PageClickHandlerAsync(args, CurrentPage - 1)));
            builder.AddContent(4, PreviousLinkText);
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildNextLink(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "class", "pagination-next");
            if(CurrentPage >= PageCount)
                builder.AddAttribute(2, "disabled");

            builder.AddAttribute(3, "onclick", 
                EventCallback.Factory.Create<MouseEventArgs>(this, async (args) => await PageClickHandlerAsync(args, CurrentPage + 1)));
            builder.AddContent(4, NextLinkText);
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildLinkList(RenderTreeBuilder builder, int index)
        {
            _pages.Clear();

            for (int i = CurrentPage - PagePadLinks; i < CurrentPage; i++)
            {
                if(i < 1)
                    continue;
                
                _pages.Add(i);
            }

            _pages.Add(CurrentPage);

            for (int i = CurrentPage + 1; i <= CurrentPage + PagePadLinks; i++)
            {
                if(i < 2 || i > PageCount)
                    continue;

                _pages.Add(i);
            }

            var showFirstPage = false;
            if(!_pages.Contains(1))
                showFirstPage = true;
            
            var showLastPage = false;
            if(!_pages.Contains(PageCount) && PageCount > PagePadLinks + 1)
                showLastPage = true;

            builder.OpenRegion(index);
            builder.OpenElement(0, "ul");
            builder.AddAttribute(1, "class", "pagination-list");

            if(showFirstPage)
                BuildFirstPageLink(builder, 2);

            int el = 3;
            foreach (var page in _pages)
            {
                BuildPageLink(builder, el, page);

                el++;
            }

            if(showLastPage)
                BuildLastPageLink(builder, el);

            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildFirstPageLink(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);

            BuildPageLink(builder, 0, 1);

            builder.OpenElement(1, "li");
            builder.OpenElement(2, "span");
            builder.AddAttribute(3, "class", "pagination-ellipsis");
            builder.AddContent(4, "...");
            builder.CloseElement();
            builder.CloseElement();

            builder.CloseRegion();
        }

        private void BuildLastPageLink(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);

            builder.OpenElement(0, "li");
            builder.OpenElement(1, "span");
            builder.AddAttribute(2, "class", "pagination-ellipsis");
            builder.AddContent(3, "...");
            builder.CloseElement();
            builder.CloseElement();

            BuildPageLink(builder, 4, PageCount);

            builder.CloseRegion();
        }

        private void BuildPageLink(RenderTreeBuilder builder, int index, int page)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "li");
            builder.OpenElement(1, "a");

            var loopP = page;
            builder.AddAttribute(2, "onclick", 
                EventCallback.Factory.Create<MouseEventArgs>(this, async (args) => await PageClickHandlerAsync(args, loopP)));

            builder.AddAttribute(3, "class", "pagination-link" + ((page == CurrentPage) ? " is-current" : String.Empty));
            builder.AddContent(4, page.ToString());

            builder.CloseElement();
            builder.CloseElement();
            builder.CloseRegion();
        }

        public async Task PageClickHandlerAsync(MouseEventArgs args, int pageNum)
        {
            if(pageNum < 1 || pageNum > PageCount || pageNum == CurrentPage)
                return;

            CurrentPage = pageNum;

            await OnPageSelected.InvokeAsync(new BulPageClickEventArgs(args, pageNum));
        }
    }
}