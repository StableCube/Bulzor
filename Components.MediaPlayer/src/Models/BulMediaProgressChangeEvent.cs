using System.Collections.Generic;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public class BulMediaProgressChangeEvent
    {
        public Dictionary<int, BulMediaProgressEventItem> Progress { get; set; }
    }
}
