using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public abstract class BulMediaPlayerBase : BulComponentBase, IDisposable
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    private MediaPlayerCommands PlayerCommands { get; set; }

    [Inject]
    private MediaPlayerEvents PlayerEvents { get; set; }

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

    protected string InstanceId { get; } = "p" + Guid.NewGuid().ToString().Split('-')[0];

    protected BulmaClassBuilder MediaPlayerClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player image");
    protected BulmaClassBuilder MediaRootClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-media-root");

    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlayerState.Loop = Loop;
        PlayerState.Autoplay = Autoplay;

        PlayerEvents.OnAbortEvent += OnAbortEvent;
        PlayerEvents.OnCanPlayEvent += OnCanPlayEvent;
        PlayerEvents.OnCanPlayThroughEvent += OnCanPlayThroughEvent;
        PlayerEvents.OnDurationChangeEvent += OnDurationChangeEvent;
        PlayerEvents.OnEmptiedEvent += OnEmptiedEvent;
        PlayerEvents.OnEndedEvent += OnEndedEvent;
        PlayerEvents.OnErrorEvent += OnErrorEvent;
        PlayerEvents.OnFullscreenChangeEvent += OnFullscreenChangeEvent;
        PlayerEvents.OnPauseEvent += OnPauseEvent;
        PlayerEvents.OnPlayEvent += OnPlayEvent;
        PlayerEvents.OnPlayingEvent += OnPlayingEvent;
        PlayerEvents.OnProgressEvent += OnProgressEvent;
        PlayerEvents.OnRateChangeEvent += OnRateChangeEvent;
        PlayerEvents.OnSeekedEvent += OnSeekedEvent;
        PlayerEvents.OnSeekingEvent += OnSeekingEvent;
        PlayerEvents.OnTimeUpdateEvent += OnTimeUpdateEvent;
        PlayerEvents.OnVolumeChangeEvent += OnVolumeChangeEvent;

        BuildBulma();
    }

    public void Dispose()
    {
        PlayerEvents.OnAbortEvent -= OnAbortEvent;
        PlayerEvents.OnCanPlayEvent -= OnCanPlayEvent;
        PlayerEvents.OnCanPlayThroughEvent -= OnCanPlayThroughEvent;
        PlayerEvents.OnDurationChangeEvent -= OnDurationChangeEvent;
        PlayerEvents.OnEmptiedEvent -= OnEmptiedEvent;
        PlayerEvents.OnEndedEvent -= OnEndedEvent;
        PlayerEvents.OnErrorEvent -= OnErrorEvent;
        PlayerEvents.OnFullscreenChangeEvent -= OnFullscreenChangeEvent;
        PlayerEvents.OnPauseEvent -= OnPauseEvent;
        PlayerEvents.OnPlayEvent -= OnPlayEvent;
        PlayerEvents.OnPlayingEvent -= OnPlayingEvent;
        PlayerEvents.OnProgressEvent -= OnProgressEvent;
        PlayerEvents.OnRateChangeEvent -= OnRateChangeEvent;
        PlayerEvents.OnSeekedEvent -= OnSeekedEvent;
        PlayerEvents.OnSeekingEvent -= OnSeekingEvent;
        PlayerEvents.OnTimeUpdateEvent -= OnTimeUpdateEvent;
        PlayerEvents.OnVolumeChangeEvent -= OnVolumeChangeEvent;

        GC.SuppressFinalize(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        await PlayerCommands.InitInteropAsync(InstanceId);
        await PlayerEvents.InitInteropAsync(InstanceId);

        if(Muted != PlayerState.Muted)
        {
            PlayerState.Muted = Muted;
            await PlayerCommands.SetMuteAsync(PlayerState.Muted);
        }

        await InvokeAsync(StateHasChanged);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BuildBulma();
    }

    protected override void BuildBulma()
    {
        MergeBuilderClassAttribute(MediaRootClassBuilder);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", MediaPlayerClassBuilder.ClassString);
        builder.AddContent(2, (builder2) =>
        {
            BuildMediaTag(builder2, 3);
            BuildControls(builder2, 4);
        });

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

    private async void OnVolumeChangeEvent(object sender, OnVolumeChangeEventArgs e)
    {
        PlayerState.Volume = e.Volume;
        PlayerState.Muted = e.Muted;
        await OnVolumeChange.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnTimeUpdateEvent(object sender, OnTimeUpdateEventArgs e)
    {
        PlayerState.CurrentTime = e.CurrentTime;
        await OnTimeUpdated.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnSeekingEvent(object sender, OnSeekingEventArgs e)
    {
        PlayerState.Seeking = true;
        await OnSeeking.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnSeekedEvent(object sender, OnSeekedEventArgs e)
    {
        PlayerState.Seeking = false;
        await OnSeeked.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnRateChangeEvent(object sender, OnRateChangeEventArgs e)
    {
        PlayerState.Rate = e.Rate;
        await OnRateChange.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnProgressEvent(object sender, OnProgressEventArgs e)
    {
        PlayerState.BufferProgress.Clear();
        foreach (var progressItem in e.Progress)
        {
            PlayerState.BufferProgress.Add(progressItem.Key, new BulMediaProgressItem()
            {
                Index = progressItem.Value.Index,
                Start = TimeSpan.FromSeconds(progressItem.Value.Start),
                End = TimeSpan.FromSeconds(progressItem.Value.End),
            });
        }

        await OnProgress.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnPlayEvent(object sender, OnPlayEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.PlayState = BulMediaPlayState.Playing;

        await OnPlayChange.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnPlayingEvent(object sender, OnPlayingEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.Width = e.Width;
        PlayerState.Height = e.Height;
        PlayerState.Duration = e.Duration;
        PlayerState.PlayState = BulMediaPlayState.Playing;

        await OnPlayingChange.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnPauseEvent(object sender, OnPauseEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.PlayState = BulMediaPlayState.Paused;

        await OnPauseChange.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnFullscreenChangeEvent(object sender, OnFullscreenChangeEventArgs e)
    {
        PlayerState.Fullscreen = e.IsFullscreen;
        await OnFullscreenChange.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnErrorEvent(object sender, OnErrorEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        PlayerState.CanPlay = false;
        await OnError.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnEndedEvent(object sender, OnEndedEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        await OnEnded.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnEmptiedEvent(object sender, OnEmptiedEventArgs e)
    {
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        PlayerState.CanPlay = false;
        await OnEmptied.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnDurationChangeEvent(object sender, OnDurationChangeEventArgs e)
    {
        PlayerState.Duration = e.Duration;
        await OnDurationChanged.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnCanPlayThroughEvent(object sender, OnCanPlayThroughEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.Width = e.Width;
        PlayerState.Height = e.Height;
        PlayerState.Duration = e.Duration;
        PlayerState.CanPlayThrough = true;

        await OnCanPlayThrough.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnCanPlayEvent(object sender, OnCanPlayEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.Width = e.Width;
        PlayerState.Height = e.Height;
        PlayerState.CanPlay = true;
        PlayerState.Duration = e.Duration;

        await OnCanPlay.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnAbortEvent(object sender, OnAbortEventArgs e)
    {
        PlayerState.CurrentSrc = e.CurrentSrc;
        PlayerState.PlayState = BulMediaPlayState.Stopped;
        await OnAbort.InvokeAsync(PlayerState);
        await InvokeAsync(StateHasChanged);
    }

    private async Task PlayPauseClickHandlerAsync(MouseEventArgs args)
    {
        if(!RendererInfo.IsInteractive)
            return;

        if(PlayerState.PlayState == BulMediaPlayState.Playing)
        {
            await PlayerCommands.PauseAsync();
        }
        else if(PlayerState.PlayState == BulMediaPlayState.Paused || PlayerState.PlayState == BulMediaPlayState.Stopped)
        {
            await PlayerCommands.PlayAsync();
        }
    }

    private async Task FullscreenClickHandlerAsync(MouseEventArgs args)
    {
        if(!RendererInfo.IsInteractive)
            return;

        await PlayerCommands.FullscreenToggleAsync();
    }

    private async Task MuteClickHandlerAsync(MouseEventArgs args)
    {
        if(!RendererInfo.IsInteractive)
            return;
        
        await PlayerCommands.SetMuteAsync(!PlayerState.Muted);
    }

    private async Task VolumeChangeHandlerAsync(double volume)
    {
        if(!RendererInfo.IsInteractive)
            return;

        await PlayerCommands.SetVolumeAsync(volume);
    }

    private async Task ScrubberTimeChangeHandlerAsync(TimeSpan value)
    {
        if(!RendererInfo.IsInteractive)
            return;

        await PlayerCommands.SetTimeAsync(value);
    }

    private void OnLoopClickHandler(MouseEventArgs args)
    {
        PlayerState.Loop = !PlayerState.Loop;
    }

    private async Task PlaybackRateChangeHandlerAsync(double value)
    {
        if(!RendererInfo.IsInteractive)
            return;

        await PlayerCommands.SetPlaybackRateAsync(value);
    }
}
