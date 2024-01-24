using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulMediaProgressItem
{
    public int Index { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
}
