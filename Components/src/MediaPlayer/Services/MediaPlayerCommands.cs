using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class MediaPlayerCommands
{
    private IJSObjectReference MediaPlayerJsInterop { get; set; }

    public MediaPlayerCommands(IJSObjectReference interop)
    {
        MediaPlayerJsInterop = interop;
    }

    public async Task PlayAsync(string elementId, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerPlay",
            cancellationToken,
            elementId
        );
    }

    public async Task PauseAsync(string elementId, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerPause",
            cancellationToken,
            elementId
        );
    }

    public async Task FullscreenToggleAsync(string elementId, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerFullscreenToggle",
            cancellationToken,
            elementId
        );
    }

    public async Task SetMuteAsync(string elementId, bool value, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerSetMuted",
            cancellationToken,
            elementId,
            value
        );
    }

    public async Task SetVolumeAsync(string elementId, double volume, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerSetVolume",
            cancellationToken,
            elementId,
            volume
        );
    }

    public async Task SetTimeAsync(string elementId, TimeSpan value, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerSetCurrentTime",
            cancellationToken,
            elementId,
            value.TotalSeconds
        );
    }

    public async Task SetPlaybackRateAsync(string elementId, double value, CancellationToken cancellationToken = default)
    {
        await MediaPlayerJsInterop.InvokeVoidAsync(
            "BulMediaPlayerSetPlaybackRate",
            cancellationToken,
            elementId,
            value
        );
    }
}
