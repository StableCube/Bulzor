using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnPlayingEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
}