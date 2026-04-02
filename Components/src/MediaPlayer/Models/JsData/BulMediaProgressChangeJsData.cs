using System.Collections.Generic;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulMediaProgressChangeJsData
{
    public Dictionary<int, BulMediaProgressItemJsData> Progress { get; set; }
}
