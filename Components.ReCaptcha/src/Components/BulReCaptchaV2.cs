using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.ReCaptcha
{
    public class BulReCaptchaV2 : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public string SiteKey { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public BulRecaptchaTheme Theme { get; set; } = BulRecaptchaTheme.Light;

        [Parameter]
        public BulRecaptchaSize Size { get; set; } = BulRecaptchaSize.Normal;

        [Parameter]
        public EventCallback<string> OnExpired { get; set; }

        [Parameter]
        public EventCallback<string> OnComplete { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string ElementId { get; set; } = Guid.NewGuid().ToString();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;
            
            await JSRuntime.InvokeVoidAsync(
                "bulReCaptchaRenderV2", 
                DotNetObjectReference.Create(this),
                ElementId, 
                SiteKey, 
                Theme.ToString().ToLower(),
                Size.ToString().ToLower(),
                "OnCompleteHandler",
                "OnExpiredHandler"
            );
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", ElementId);
            builder.AddAttribute(2, "class", "g-recaptcha");
            builder.AddAttribute(3, "data-sitekey", SiteKey);
            builder.CloseElement();
        }

        [JSInvokable]
        public async Task OnCompleteHandler(string responseToken)
        {
            Value = responseToken;
            await OnComplete.InvokeAsync(Value);
            await ValueChanged.InvokeAsync(Value);
        }
        
        [JSInvokable]
        public async Task OnExpiredHandler()
        {
            Value = null;
            await OnExpired.InvokeAsync(ElementId);
            await ValueChanged.InvokeAsync(Value);
        }

        public async Task ResetAsync()
        {
            await JSRuntime.InvokeVoidAsync("bulReCaptchaResetV2");
        }
    }
}