using System;
using System.Collections.Generic;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class OnProgressEventArgs : EventArgs
{
    public Dictionary<int, BulMediaProgressItemJsData> Progress { get; set; }
}