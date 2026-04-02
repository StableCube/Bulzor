using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnPauseEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
}