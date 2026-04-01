using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class MediaPlayerCommands : IAsyncDisposable
{
    private IJSObjectReference CommandsInterop { get; set; }

    public MediaPlayerCommands()
    {
    }

    public async ValueTask DisposeAsync()
    {
        if (CommandsInterop != null)
        {
            try
            {
                await CommandsInterop.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }

        GC.SuppressFinalize(this);
    }

    public async Task InitInteropAsync(IJSObjectReference interrop, string instanceId)
    {
        CommandsInterop = await interrop.InvokeConstructorAsync("BulMediaPlayerCommands", instanceId);
    }

    public async Task PlayAsync(CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "Play",
            cancellationToken
        );
    }

    public async Task PauseAsync(CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "Pause",
            cancellationToken
        );
    }

    public async Task FullscreenToggleAsync(CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "FullscreenToggle",
            cancellationToken
        );
    }

    public async Task SetMuteAsync(bool value, CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "SetMuted",
            cancellationToken,
            value
        );
    }

    public async Task SetVolumeAsync(double volume, CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "SetVolume",
            cancellationToken,
            volume
        );
    }

    public async Task SetTimeAsync(TimeSpan value, CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "SetCurrentTime",
            cancellationToken,
            value.TotalSeconds
        );
    }

    public async Task SetPlaybackRateAsync(double value, CancellationToken cancellationToken = default)
    {
        await CommandsInterop.InvokeVoidAsync(
            "SetPlaybackRate",
            cancellationToken,
            value
        );
    }
}
