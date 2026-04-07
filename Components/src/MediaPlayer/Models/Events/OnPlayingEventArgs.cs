using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnPlayingEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
    public TimeSpan Duration { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}