using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnAbortEventArgs : EventArgs
{
    public Uri CurrentSrc { get; set; }
}