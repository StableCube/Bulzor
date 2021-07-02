using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public abstract class BulMediaPlayerBase : BulComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
    
        [Parameter]
        public Uri Src { get; set; }

        [Parameter]
        public Uri Poster { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public bool Loop { get; set; }

        [Parameter]
        public bool Autoplay { get; set; }

        [Parameter]
        public bool Muted { get; set; }

        [Parameter]
        public BulMediaPlayPreload? Preload { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<BulMediaPlayState> OnPlayStateChange { get; set; }

        [Parameter]
        public EventCallback<BulMediaPlayerVolumeChangeEvent> OnVolumeChange { get; set; }

        [Parameter]
        public EventCallback<bool> OnFullscreenChange { get; set; }

        protected BulMediaPlayerStateModel PlayerState { get; set; } = new BulMediaPlayerStateModel();

        protected string ElementId { get; } = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

        protected BulmaClassBuilder MediaPlayerClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player");
        protected BulmaClassBuilder MediaRootClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-media-root");

        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlayerState.Loop = Loop;
            PlayerState.Autoplay = Autoplay;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Media event triggers do not work in Dotnet 5 but should be fixed in 6 and many of these should then not be needed.
                await JSRuntime.InvokeVoidAsync(
                    "bulMediaPlayerEvents", 
                    DotNetObjectReference.Create(this),
                    ElementId.ToString(),
                    "OnPlayEventHandler",
                    "OnPlayingEventHandler",
                    "OnPauseEventHandler",
                    "OnVolumeChangeEventHandler",
                    "OnFullscreenChangeEventHandler",
                    "OnEndedEventHandler",
                    "OnDurationChangeEventHandler",
                    "OnProgressEventHandler",
                    "OnTimeUpdateEventHandler",
                    "OnSeekingEventHandler",
                    "OnSeekedEventHandler",
                    "OnCanPlayEventHandler",
                    "OnCanPlayThroughEventHandler",
                    "OnRateChangeEventHandler"
                );

                if(Muted != PlayerState.Muted)
                {
                    PlayerState.Muted = Muted;
                    await JSRuntime.InvokeVoidAsync(
                        "bulMediaPlayerSetMuted",
                        ElementId,
                        PlayerState.Muted
                    );
                }
            }
        }

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", ElementId);
            builder.AddAttribute(2, "class", MediaPlayerClassBuilder.ClassString);
            builder.AddContent(3, (RenderFragment)((builder2) => {

                BuildMediaTag(builder2, 4);
                BuildControls(builder2, 5);
            }));

            builder.CloseElement();
        }

        protected abstract void BuildMediaTag(RenderTreeBuilder builder, int index);

        private void BuildControls(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenComponent<BulMediaPlayerControls>(0);
            builder.AddAttribute(1, "PlayerState", PlayerState);
            builder.AddAttribute(2, "Size", Size);
            builder.AddAttribute(3, "Color", Color);
            builder.AddAttribute(4, "OnPlayPauseClick", EventCallback.Factory.Create<MouseEventArgs>(this, PlayPauseClickHandlerAsync));
            builder.AddAttribute(5, "OnVolumeChange", EventCallback.Factory.Create<double>(this, VolumeChangeHandlerAsync));
            builder.AddAttribute(6, "OnMuteClick", EventCallback.Factory.Create<MouseEventArgs>(this, MuteClickHandlerAsync));
            builder.AddAttribute(7, "OnFullscreenClick", EventCallback.Factory.Create<MouseEventArgs>(this, FullscreenClickHandlerAsync));
            builder.AddAttribute(8, "OnScrubberTimeChange", EventCallback.Factory.Create<TimeSpan>(this, ScrubberTimeChangeHandlerAsync));
            builder.AddAttribute(9, "OnLoopClick", EventCallback.Factory.Create<MouseEventArgs>(this, OnLoopClickHandler));
            builder.AddAttribute(10, "OnPlaybackRateChange", EventCallback.Factory.Create<double>(this, PlaybackRateChangeHandlerAsync));
            builder.CloseComponent();
            builder.CloseRegion();
        }

        [JSInvokable]
        public async Task OnPlayingEventHandler()
        {
            PlayerState.PlayState = BulMediaPlayState.Playing;
            await OnPlayStateChange.InvokeAsync(PlayerState.PlayState);
            StateHasChanged();

        }

        [JSInvokable]
        public async Task OnPlayEventHandler()
        {
            PlayerState.PlayState = BulMediaPlayState.Playing;
            await OnPlayStateChange.InvokeAsync(PlayerState.PlayState);
            StateHasChanged();
        }

        [JSInvokable]
        public async Task OnPauseEventHandler()
        {
            PlayerState.PlayState = BulMediaPlayState.Paused;
            await OnPlayStateChange.InvokeAsync(PlayerState.PlayState);
            StateHasChanged();
        }

        [JSInvokable]
        public async Task OnVolumeChangeEventHandler(string eventJson)
        {
            var eventObj = JsonSerializer.Deserialize<BulMediaPlayerVolumeChangeEvent>(eventJson);
            PlayerState.Volume = eventObj.Volume;
            PlayerState.Muted = eventObj.Muted;
            await OnVolumeChange.InvokeAsync(eventObj);
            StateHasChanged();
        }

        [JSInvokable]
        public async Task OnFullscreenChangeEventHandler(bool value)
        {
            PlayerState.Fullscreen = value;
            await OnFullscreenChange.InvokeAsync(value);
            StateHasChanged();
        }

        [JSInvokable]
        public async Task OnEndedEventHandler()
        {
            PlayerState.PlayState = BulMediaPlayState.Stopped;
            await OnPlayStateChange.InvokeAsync(PlayerState.PlayState);
            StateHasChanged();
        }

        [JSInvokable]
        public void OnDurationChangeEventHandler(double value)
        {
            PlayerState.Duration = TimeSpan.FromSeconds(value);
            StateHasChanged();
        }

        [JSInvokable]
        public void OnProgressEventHandler(string eventJson)
        {
            var eventObj = JsonSerializer.Deserialize<BulMediaProgressChangeEvent>(eventJson);
            PlayerState.BufferProgress.Clear();
            foreach (var progressItem in eventObj.Progress)
            {
                PlayerState.BufferProgress.Add(progressItem.Key, new BulMediaProgressItem()
                {
                    Index = progressItem.Value.Index,
                    Start = TimeSpan.FromSeconds(progressItem.Value.Start),
                    End = TimeSpan.FromSeconds(progressItem.Value.End),
                });
            }
            StateHasChanged();
        }

        [JSInvokable]
        public void OnTimeUpdateEventHandler(double value)
        {
            PlayerState.CurrentTime = TimeSpan.FromSeconds(value);
            StateHasChanged();
        }

        [JSInvokable]
        public void OnSeekingEventHandler()
        {
            PlayerState.Seeking = true;
            StateHasChanged();
        }

        [JSInvokable]
        public void OnSeekedEventHandler()
        {
            PlayerState.Seeking = false;
            StateHasChanged();
        }

        [JSInvokable]
        public void OnCanPlayEventHandler()
        {
            PlayerState.CanPlay = true;
            StateHasChanged();
        }

        [JSInvokable]
        public void OnCanPlayThroughEventHandler()
        {
            PlayerState.CanPlayThrough = true;
            StateHasChanged();
        }

        [JSInvokable]
        public void OnRateChangeEventHandler(double value)
        {
            PlayerState.Rate = value;
            StateHasChanged();
        }

        private async Task PlayPauseClickHandlerAsync(MouseEventArgs args)
        {
            if(PlayerState.PlayState == BulMediaPlayState.Playing)
            {
                await JSRuntime.InvokeVoidAsync(
                    "bulMediaPlayerPause",
                    ElementId
                );
            }
            else if(PlayerState.PlayState == BulMediaPlayState.Paused || PlayerState.PlayState == BulMediaPlayState.Stopped)
            {
                await JSRuntime.InvokeVoidAsync(
                    "bulMediaPlayerPlay",
                    ElementId
                );
            }
        }

        private async Task FullscreenClickHandlerAsync(MouseEventArgs args)
        {
            await JSRuntime.InvokeVoidAsync(
                "bulMediaPlayerFullscreenToggle",
                ElementId
            );
        }

        private async Task MuteClickHandlerAsync(MouseEventArgs args)
        {
            await JSRuntime.InvokeVoidAsync(
                "bulMediaPlayerSetMuted",
                ElementId,
                !PlayerState.Muted
            );
        }

        private async Task VolumeChangeHandlerAsync(double volume)
        {
            await JSRuntime.InvokeVoidAsync(
                "bulMediaPlayerSetVolume",
                ElementId,
                volume
            );
        }

        private async Task ScrubberTimeChangeHandlerAsync(TimeSpan value)
        {
            await JSRuntime.InvokeVoidAsync(
                "bulMediaPlayerSetCurrentTime",
                ElementId,
                value.TotalSeconds
            );
        }

        private void OnLoopClickHandler(MouseEventArgs args)
        {
            PlayerState.Loop = !PlayerState.Loop;
        }

        private async Task PlaybackRateChangeHandlerAsync(double value)
        {
            await JSRuntime.InvokeVoidAsync(
                "bulMediaPlayerSetPlaybackRate",
                ElementId,
                value
            );
        }
    }
}