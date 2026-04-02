using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnPlayEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
}