using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnDurationChangeEventArgs : EventArgs
{
    public TimeSpan Duration { get; set; }
}