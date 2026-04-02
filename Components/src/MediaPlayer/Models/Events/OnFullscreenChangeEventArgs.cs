using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnFullscreenChangeEventArgs : EventArgs
{
    public bool IsFullscreen { get; set; }
}