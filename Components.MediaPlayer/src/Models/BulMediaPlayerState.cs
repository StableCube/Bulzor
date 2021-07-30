using System;
using System.Collections.Generic;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public class BulMediaPlayerState
    {
        public Uri CurrentSrc { get; set; }
        public BulMediaPlayMediaType MediaType { get; set; }
        public BulMediaPlayState PlayState { get; set; } = BulMediaPlayState.Stopped;
        public double Volume { get; set; } = 1;
        public bool Muted { get; set; }
        public bool Fullscreen { get; set; }
        public TimeSpan CurrentTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Dictionary<int, BulMediaProgressItem> BufferProgress { get; set; } = new Dictionary<int, BulMediaProgressItem>();
        public bool Seeking { get; set; }
        public bool CanPlay { get; set; }
        public bool CanPlayThrough { get; set; }
        public bool Loop { get; set; }
        public bool Autoplay { get; set; }
        public double Rate { get; set; } = 1;
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
