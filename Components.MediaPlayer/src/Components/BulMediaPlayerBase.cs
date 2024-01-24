using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public abstract class BulMediaPlayerBase : BulComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private MediaPlayerCommands Commands { get; set; }

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
    public EventCallback<BulMediaPlayerState> OnPlayChange { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnPlayingChange { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnPauseChange { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnVolumeChange { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnFullscreenChange { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnEnded { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnAbort { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnEmptied { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnError { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnRateChange { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnDurationChanged { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnProgress { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnTimeUpdated { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnSeeking { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnSeeked { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnCanPlay { get; set; }

    [Parameter]
    public EventCallback<BulMediaPlayerState> OnCanPlayThrough { get; set; }

    public BulMediaPlayerState PlayerState { get; set; } = new BulMediaPlayerState();

    protected string ElementId { get; } = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

    protected BulmaClassBuilder MediaPlayerClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player image");
    protected BulmaClassBuilder MediaRootClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-media-root");

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Commands = new MediaPlayerCommands(JSRuntime);

        PlayerState.Loop = Loop;
        PlayerState.Autoplay = Autoplay;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;
    
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
            "OnRateChangeEventHandler",
            "OnAbortEventHandler",
            "OnEmptiedEventHandler",
            "OnErrorEventHandler"
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

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        MergeBuilderClassAttribute(MediaRootClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
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
    public async Task OnPlayingEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.PlayState = BulMediaPlayState.Playing;
        await OnPlayingChange.InvokeAsync(PlayerState);
        StateHasChanged();

    }

    [JSInvokable]
    public async Task OnPlayEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.PlayState = BulMediaPlayState.Playing;
        await OnPlayChange.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnPauseEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.PlayState = BulMediaPlayState.Paused;
        await OnPauseChange.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnVolumeChangeEventHandler(string eventJson)
    {
        var eventObj = JsonSerializer.Deserialize<BulMediaPlayerVolumeChangeEvent>(eventJson);
        PlayerState.Volume = eventObj.Volume;
        PlayerState.Muted = eventObj.Muted;
        await OnVolumeChange.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnFullscreenChangeEventHandler(bool value)
    {
        PlayerState.Fullscreen = value;
        await OnFullscreenChange.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnEndedEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        await OnEnded.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnDurationChangeEventHandler(double value)
    {
        PlayerState.Duration = TimeSpan.FromSeconds(value);
        await OnDurationChanged.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnProgressEventHandler(string eventJson)
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

        await OnProgress.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnTimeUpdateEventHandler(double value)
    {
        PlayerState.CurrentTime = TimeSpan.FromSeconds(value);
        await OnTimeUpdated.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnSeekingEventHandler()
    {
        PlayerState.Seeking = true;
        await OnSeeking.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnSeekedEventHandler()
    {
        PlayerState.Seeking = false;

        await OnSeeked.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnCanPlayEventHandler(string json)
    {
        var eventObj = JsonSerializer.Deserialize<BulMediaCanPlayEvent>(json);
        PlayerState.CurrentSrc = new Uri(eventObj.CurrentSrc);
        PlayerState.Width = eventObj.Width;
        PlayerState.Height = eventObj.Height;
        PlayerState.CanPlay = true;

        await OnCanPlay.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnCanPlayThroughEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.CanPlayThrough = true;

        await OnCanPlayThrough.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnRateChangeEventHandler(double value)
    {
        PlayerState.Rate = value;
        await OnRateChange.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnAbortEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        await OnAbort.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnEmptiedEventHandler()
    {
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        PlayerState.CanPlay = false;
        await OnEmptied.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task OnErrorEventHandler(string currentSrc)
    {
        PlayerState.CurrentSrc = new Uri(currentSrc);
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        PlayerState.CanPlay = false;
        await OnError.InvokeAsync(PlayerState);
        StateHasChanged();
    }

    private async Task PlayPauseClickHandlerAsync(MouseEventArgs args)
    {
        if(PlayerState.PlayState == BulMediaPlayState.Playing)
        {
            await Commands.PauseAsync(ElementId);
        }
        else if(PlayerState.PlayState == BulMediaPlayState.Paused || PlayerState.PlayState == BulMediaPlayState.Stopped)
        {
            await Commands.PlayAsync(ElementId);
        }
    }

    private async Task FullscreenClickHandlerAsync(MouseEventArgs args)
    {
        await Commands.FullscreenToggleAsync(ElementId);
    }

    private async Task MuteClickHandlerAsync(MouseEventArgs args)
    {
        await Commands.SetMuteAsync(ElementId, !PlayerState.Muted);
    }

    private async Task VolumeChangeHandlerAsync(double volume)
    {
        await Commands.SetVolumeAsync(ElementId, volume);
    }

    private async Task ScrubberTimeChangeHandlerAsync(TimeSpan value)
    {
        await Commands.SetTimeAsync(ElementId, value);
    }

    private void OnLoopClickHandler(MouseEventArgs args)
    {
        PlayerState.Loop = !PlayerState.Loop;
    }

    private async Task PlaybackRateChangeHandlerAsync(double value)
    {
        await Commands.SetPlaybackRateAsync(ElementId, value);
    }
}
