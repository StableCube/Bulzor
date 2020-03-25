using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;

namespace StableCube.Bulzor
{
    public static class SassFileMiddleware
    {
        public static IApplicationBuilder UseBulzorSassCompiler(
            this IApplicationBuilder builder,
            Action<SassOptions> optionsAction
        )
        {
            SassOptions ops = new SassOptions();
            optionsAction.Invoke(ops);

            if(!Directory.Exists(ops.CompileOutputDir))
                Directory.CreateDirectory(ops.CompileOutputDir);

            if(!Directory.Exists(ops.TempDir))
                Directory.CreateDirectory(ops.TempDir);

            string css = SassCompiler.Compile(ops);

            if(!Directory.Exists(ops.CompileOutputDir))
                Directory.CreateDirectory(ops.CompileOutputDir);

            string cssPath = Path.Combine(ops.CompileOutputDir, ops.OutputCssFilename);
            File.WriteAllText(cssPath, css);

            string gzipPath = Path.Combine(ops.CompileOutputDir, ops.OutputGzipFilename);
            GzipCompress(cssPath, gzipPath);

            return builder;
        }

        private static void GzipCompress(string sourcePath, string outPath)
        {
            using (FileStream originalFileStream = File.OpenRead(sourcePath))
            {
                using (FileStream compressedFileStream = File.Create(outPath))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionLevel.Optimal))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }
    }
}