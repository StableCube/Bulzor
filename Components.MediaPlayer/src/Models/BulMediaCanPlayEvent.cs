using System.Collections.Generic;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public class BulMediaCanPlayEvent
    {
        public string CurrentSrc { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
