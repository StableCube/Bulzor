using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnErrorEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
}