using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Demo.Client
{
    public class ButtonOptions
    {
        public BulSchemeColor Color { get; set; } = BulSchemeColor.Default;
        public bool Loading { get; set; }
        public BulSize Size { get; set; } = BulSize.Default;
        public bool Focused { get; set; }
        public bool Active { get; set; }
        public bool Hovered { get; set; }
        public bool Outlined { get; set; }
        public bool FullWidth { get; set; }
        public bool Inverted { get; set; }
        public bool Rounded { get; set; }
        public bool Static { get; set; }
    }
}
