using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulMediaPlayerVolumeChangeEvent
{
    public double Volume { get; set; }
    public bool Muted { get; set; }
}
