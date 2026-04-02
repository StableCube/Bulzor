using System;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnRateChangeEventArgs : EventArgs
{
    public double Rate { get; set; }
}