using System.Collections.Generic;

namespace StableCube.Bulzor
{
    public class SassOptions
    {
        /// <summary>
        /// Path to Sass template file
        /// </summary>
        public string ThemeTemplatePath { get; set; }

        /// <summary>
        /// List of Sass file paths to import
        /// </summary>
        public IList<string> Imports { get; set; } = new List<string>();

        /// <summary>
        /// Directory to store file while compiling
        /// </summary>
        public string TempDir { get; set; }

        /// <summary>
        /// Directory to compile output to
        /// </summary>
        public string CompileOutputDir { get; set; }

        /// <summary>
        /// Filename to compile to
        /// </summary>
        public string OutputCssFilename { get; set; }

        /// <summary>
        /// Filename to compress to
        /// </summary>
        public string OutputGzipFilename { get; set; }
    }
}