using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnTimeUpdateEventArgs : EventArgs
{
    public TimeSpan CurrentTime { get; set; }
}