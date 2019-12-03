using System.Collections.Generic;

namespace StableCube.Bulzor
{
    public class SassOptions
    {
        public string Theme { get; set; }

        public IList<string> Imports { get; set; }

        public string OutputCssFilename { get; set; }

        public string OutputGzipFilename { get; set; }
    }
}