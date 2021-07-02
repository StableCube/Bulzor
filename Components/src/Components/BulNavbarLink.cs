using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace StableCube.Bulzor.Components
{
    public class BulNavbarLink : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets a value representing the URL matching behavior.
        /// </summary>
        [Parameter]
        public NavLinkMatch Match { get; set; }

        [Parameter]
        public bool Active { get; set; }

        [Parameter]
        public bool Arrowless { get; set; }

        protected BulmaClassBuilder ClassBuilder { get; set; } = new BulmaClassBuilder("navbar-link");

        protected override void BuildBulma()
        {
            ClassBuilder.IsArrowless = Arrowless;
            ClassBuilder.IsActive = Active;

            MergeBuilderClassAttribute(ClassBuilder);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenComponent<NavLink>(0);
            builder.AddAttribute(1, "ActiveClass", CssConfig.Prefix + "is-active");
            builder.AddAttribute(2, "Match", Match);
            builder.AddMultipleAttributes(3, CombinedAdditionalAttributes);

            builder.AddAttribute(4, "ChildContent", (RenderFragment)((builder2) => {
                builder2.AddContent(5, ChildContent);
            }));

            builder.CloseComponent();
        }
    }
}