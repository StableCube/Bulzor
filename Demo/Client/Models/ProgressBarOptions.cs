using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Demo.Client
{
    public class ProgressBarOptions
    {
        public int Progress { get; set; } = 25;

        public bool Indeterminate { get; set; }

        public BulPrimaryColor? Color { get; set; }
    }
}
