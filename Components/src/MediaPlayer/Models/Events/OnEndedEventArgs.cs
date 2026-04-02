using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnEndedEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
}