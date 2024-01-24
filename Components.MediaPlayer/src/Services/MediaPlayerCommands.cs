using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class MediaPlayerCommands
{
    private readonly IJSRuntime _jSRuntime;

    public MediaPlayerCommands(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task PlayAsync(string elementId, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerPlay",
            cancellationToken,
            elementId
        );
    }

    public async Task PauseAsync(string elementId, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerPause",
            cancellationToken,
            elementId
        );
    }

    public async Task FullscreenToggleAsync(string elementId, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerFullscreenToggle",
            cancellationToken,
            elementId
        );
    }

    public async Task SetMuteAsync(string elementId, bool value, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerSetMuted",
            cancellationToken,
            elementId,
            value
        );
    }

    public async Task SetVolumeAsync(string elementId, double volume, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerSetVolume",
            cancellationToken,
            elementId,
            volume
        );
    }

    public async Task SetTimeAsync(string elementId, TimeSpan value, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerSetCurrentTime",
            cancellationToken,
            elementId,
            value.TotalSeconds
        );
    }

    public async Task SetPlaybackRateAsync(string elementId, double value, CancellationToken cancellationToken = default)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bulMediaPlayerSetPlaybackRate",
            cancellationToken,
            elementId,
            value
        );
    }
}
