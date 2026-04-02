using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnVolumeChangeEventArgs : EventArgs
{
    public double Volume { get; set; }
    public bool Muted { get; set; }
}