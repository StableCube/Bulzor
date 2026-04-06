using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class MediaPlayerCommands : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private readonly BulzorConfig _config;
    private IJSObjectReference _jsModule;
    private IJSObjectReference _jsClass;

    public MediaPlayerCommands(IJSRuntime jsRuntime, BulzorConfig config)
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

        GC.SuppressFinalize(this);
    }

    public async Task InitInteropAsync(string instanceId)
    {
        _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", _config.MediaPlayerJsPath);
        _jsClass = await _jsModule.InvokeConstructorAsync("BulMediaPlayerCommands", instanceId);
    }

    public async Task PlayAsync(CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
        
        await _jsClass.InvokeVoidAsync(
            "Play",
            cancellationToken
        );
    }

    public async Task PauseAsync(CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
        
        await _jsClass.InvokeVoidAsync(
            "Pause",
            cancellationToken
        );
    }

    public async Task FullscreenToggleAsync(CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
        
        await _jsClass.InvokeVoidAsync(
            "FullscreenToggle",
            cancellationToken
        );
    }

    public async Task SetMuteAsync(bool value, CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
        
        await _jsClass.InvokeVoidAsync(
            "SetMuted",
            cancellationToken,
            value
        );
    }

    public async Task SetVolumeAsync(double volume, CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
        
        await _jsClass.InvokeVoidAsync(
            "SetVolume",
            cancellationToken,
            volume
        );
    }

    public async Task SetTimeAsync(TimeSpan value, CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
        
        await _jsClass.InvokeVoidAsync(
            "SetCurrentTime",
            cancellationToken,
            value.TotalSeconds
        );
    }

    public async Task SetPlaybackRateAsync(double value, CancellationToken cancellationToken = default)
    {
        if(_jsClass == null)
            return;
    
        await _jsClass.InvokeVoidAsync(
            "SetPlaybackRate",
            cancellationToken,
            value
        );
    }
}
