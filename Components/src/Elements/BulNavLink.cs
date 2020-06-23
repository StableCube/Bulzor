using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;

namespace StableCube.Bulzor.Components
{
    public class BulNavLink : BulComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets a value representing the URL matching behavior.
        /// </summary>
        [Parameter]
        public NavLinkMatch Match { get; set; }

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<NavLink>(0);
            builder.AddAttribute(1, "ActiveClass", CssConfig.Prefix + "is-active");
            builder.AddAttribute(2, "Match", Match);
            builder.AddAttribute(3, "AdditionalAttributes", AdditionalAttributes);

            builder.AddAttribute(4, "ChildContent", (RenderFragment)((builder2) => {
                builder2.AddContent(5, ChildContent);
            }));

            builder.CloseComponent();
        }
    }
}