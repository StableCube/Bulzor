using System.Collections.Generic;

namespace StableCube.Bulzor
{
    public class SassOptions
    {
        public string Theme { get; set; }

        public IList<string> Imports { get; set; }

        public string BulmaVersion { get; set; } = "0.8.0";

        public string OutputCssPath { get; set; }

        public string OutputGzipPath { get; set; }
    }
}