using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class MediaPlayerEvents : IAsyncDisposable, IDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private readonly BulzorConfig _config;
    private IJSObjectReference _jsModule;
    private IJSObjectReference _jsClass;
    private DotNetObjectReference<MediaPlayerEvents> _jsRef;

    public event EventHandler<OnPlayingEventArgs> OnPlayingEvent;
    public event EventHandler<OnPlayEventArgs> OnPlayEvent;
    public event EventHandler<OnPauseEventArgs> OnPauseEvent;
    public event EventHandler<OnVolumeChangeEventArgs> OnVolumeChangeEvent;
    public event EventHandler<OnFullscreenChangeEventArgs> OnFullscreenChangeEvent;
    public event EventHandler<OnEndedEventArgs> OnEndedEvent;
    public event EventHandler<OnDurationChangeEventArgs> OnDurationChangeEvent;
    public event EventHandler<OnProgressEventArgs> OnProgressEvent;
    public event EventHandler<OnTimeUpdateEventArgs> OnTimeUpdateEvent;
    public event EventHandler<OnSeekingEventArgs> OnSeekingEvent;
    public event EventHandler<OnSeekedEventArgs> OnSeekedEvent;
    public event EventHandler<OnCanPlayEventArgs> OnCanPlayEvent;
    public event EventHandler<OnCanPlayThroughEventArgs> OnCanPlayThroughEvent;
    public event EventHandler<OnRateChangeEventArgs> OnRateChangeEvent;
    public event EventHandler<OnAbortEventArgs> OnAbortEvent;
    public event EventHandler<OnEmptiedEventArgs> OnEmptiedEvent;
    public event EventHandler<OnErrorEventArgs> OnErrorEvent;

    public MediaPlayerEvents(IJSRuntime jsRuntime, BulzorConfig config)
    {
        _jsRuntime = jsRuntime;
        _config = config;
    }

    public async ValueTask DisposeAsync()
    {
        if (_jsClass != null)
        {
            try
            {
                await _jsClass.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }

        if (_jsModule != null)
        {
            try
            {
                await _jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }

        _jsRef?.Dispose();

        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        _jsRef?.Dispose();

        GC.SuppressFinalize(this);
    }

    public async Task InitInteropAsync(string instanceId)
    {
        _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", _config.MediaPlayerJsPath);
        _jsRef = DotNetObjectReference.Create(this);
        _jsClass = await _jsModule.InvokeConstructorAsync("BulMediaPlayerEventListener", instanceId, _jsRef);
    }

    [JSInvokable]
    public void OnPlayingEventHandler(string eventJson)
    {
        if(string.IsNullOrEmpty(eventJson))
            return;

        var eventObj = JsonSerializer.Deserialize<BulMediaPlayingJsData>(eventJson);

        OnPlayingEvent?.Invoke(this, new OnPlayingEventArgs()
        {
            CurrentSrc = new Uri(eventObj.CurrentSrc),
            Duration = TimeSpan.FromSeconds(eventObj.Duration),
            Width = eventObj.Width,
            Height = eventObj.Height,
        });
    }

    [JSInvokable]
    public void OnPlayEventHandler(string currentSrc)
    {
        if(string.IsNullOrEmpty(currentSrc))
            return;

        OnPlayEvent?.Invoke(this, new OnPlayEventArgs()
        {
            CurrentSrc = new Uri(currentSrc)
        });
    }

    [JSInvokable]
    public void OnPauseEventHandler(string currentSrc)
    {
        if(string.IsNullOrEmpty(currentSrc))
            return;
        
        OnPauseEvent?.Invoke(this, new OnPauseEventArgs()
        {
            CurrentSrc = new Uri(currentSrc)
        });
    }

    [JSInvokable]
    public async Task OnVolumeChangeEventHandler(string eventJson)
    {
        if(string.IsNullOrEmpty(eventJson))
            return;

        var eventObj = JsonSerializer.Deserialize<BulMediaPlayerVolumeChangeJsData>(eventJson);
        OnVolumeChangeEvent?.Invoke(this, new OnVolumeChangeEventArgs()
        {
            Volume = eventObj.Volume,
            Muted = eventObj.Muted,
        });
    }

    [JSInvokable]
    public async Task OnFullscreenChangeEventHandler(bool value)
    {
        OnFullscreenChangeEvent?.Invoke(this, new OnFullscreenChangeEventArgs()
        {
            IsFullscreen = value
        });
    }

    [JSInvokable]
    public async Task OnEndedEventHandler(string currentSrc)
    {
        if(string.IsNullOrEmpty(currentSrc))
            return;

        OnEndedEvent.Invoke(this, new OnEndedEventArgs()
        {
            CurrentSrc = new Uri(currentSrc)
        });
    }

    [JSInvokable]
    public async Task OnDurationChangeEventHandler(double value)
    {
        OnDurationChangeEvent?.Invoke(this, new OnDurationChangeEventArgs()
        {
            Duration = TimeSpan.FromSeconds(value)
        });
    }

    [JSInvokable]
    public async Task OnProgressEventHandler(string eventJson)
    {
        if(string.IsNullOrEmpty(eventJson))
            return;

        var eventObj = JsonSerializer.Deserialize<BulMediaProgressChangeJsData>(eventJson);

        OnProgressEvent?.Invoke(this, new OnProgressEventArgs()
        {
            Progress = eventObj.Progress
        });
    }

    [JSInvokable]
    public async Task OnTimeUpdateEventHandler(double value)
    {
        OnTimeUpdateEvent?.Invoke(this, new OnTimeUpdateEventArgs()
        {
            CurrentTime = TimeSpan.FromSeconds(value)
        });
    }

    [JSInvokable]
    public async Task OnSeekingEventHandler()
    {
        OnSeekingEvent?.Invoke(this, new OnSeekingEventArgs());
    }

    [JSInvokable]
    public async Task OnSeekedEventHandler()
    {
        OnSeekedEvent?.Invoke(this, new OnSeekedEventArgs());
    }

    [JSInvokable]
    public async Task OnCanPlayEventHandler(string json)
    {
        if(string.IsNullOrEmpty(json))
            return;

        var eventObj = JsonSerializer.Deserialize<BulMediaCanPlayJsData>(json);
        
        OnCanPlayEvent?.Invoke(this, new OnCanPlayEventArgs()
        {
            CurrentSrc = new Uri(eventObj.CurrentSrc),
            Duration = TimeSpan.FromSeconds(eventObj.Duration),
            Width = eventObj.Width,
            Height = eventObj.Height,
        });
    }

    [JSInvokable]
    public async Task OnCanPlayThroughEventHandler(string json)
    {
        if(string.IsNullOrEmpty(json))
            return;

        var eventObj = JsonSerializer.Deserialize<BulMediaCanPlayJsData>(json);

        OnCanPlayThroughEvent?.Invoke(this, new OnCanPlayThroughEventArgs()
        {
            CurrentSrc = new Uri(eventObj.CurrentSrc),
            Duration = TimeSpan.FromSeconds(eventObj.Duration),
            Width = eventObj.Width,
            Height = eventObj.Height,
        });
    }

    [JSInvokable]
    public async Task OnRateChangeEventHandler(double value)
    {
        OnRateChangeEvent?.Invoke(this, new OnRateChangeEventArgs()
        {
            Rate = value
        });
    }

    [JSInvokable]
    public async Task OnAbortEventHandler(string currentSrc)
    {
        if(string.IsNullOrEmpty(currentSrc))
            return;

        OnAbortEvent?.Invoke(this, new OnAbortEventArgs()
        {
            CurrentSrc = new Uri(currentSrc)
        });
    }

    [JSInvokable]
    public async Task OnEmptiedEventHandler()
    {
        OnEmptiedEvent?.Invoke(this, new OnEmptiedEventArgs());
    }

    [JSInvokable]
    public async Task OnErrorEventHandler(string currentSrc)
    {
        if(string.IsNullOrEmpty(currentSrc))
            return;

        OnErrorEvent?.Invoke(this, new OnErrorEventArgs()
        {
            CurrentSrc = new Uri(currentSrc)
        });
    }
}
