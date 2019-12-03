using System;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Builder;
using SharpScss;

namespace StableCube.Bulzor
{
    public static class SassFileMiddleware
    {
        public static IApplicationBuilder UseBulzorSass(
            this IApplicationBuilder builder,
            Action<SassOptions> optionsAction
        )
        {
            SassOptions ops = new SassOptions();
            optionsAction.Invoke(ops);

            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if(!Directory.Exists(buildDir))
                throw new DirectoryNotFoundException($"Directory not found: {buildDir}");

            if(!File.Exists(ops.Theme))
                throw new FileNotFoundException($"File not found: {ops.Theme}");

            string bulzorRootPath = Path.Combine(buildDir, "bulzorsass/Sass/bulzor.scss");
            if(!File.Exists(bulzorRootPath))
                throw new FileNotFoundException($"File not found: {bulzorRootPath}");

            string bulmaFunctionsPath = Path.Combine(buildDir, $"bulzorsass/bulma-{ops.BulmaVersion}/sass/utilities/functions.sass");
            if(!File.Exists(bulmaFunctionsPath))
                throw new FileNotFoundException($"File not found: {bulmaFunctionsPath}");

            string bulmaRootPath = Path.Combine(buildDir, $"bulzorsass/bulma-{ops.BulmaVersion}/bulma.sass");
            if(!File.Exists(bulmaRootPath))
                throw new FileNotFoundException($"File not found: {bulmaRootPath}");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"@import \"{bulmaFunctionsPath}\";");
            sb.AppendLine($"@import \"{ops.Theme}\";");
            sb.AppendLine($"@import \"{bulmaRootPath}\";");
            sb.AppendLine($"@import \"{bulzorRootPath}\";");

            foreach (var import in ops.Imports)
            {
                if(!File.Exists(import))
                    throw new FileNotFoundException($"File not found: {import}");

                sb.AppendLine($"@import \"{import}\";");
            }

            string css = String.Empty;
            try
            {
                var result = Scss.ConvertToCss(sb.ToString());
                css = result.Css;
            }
            catch (System.Exception e)
            {
                throw new SassCompileException("Sass failed to compile. There is probably an error in a sass document.", e);
            }

            File.WriteAllText(ops.OutputCssPath, css);

            if(ops.OutputGzipPath != null)
                GzipCompress(ops.OutputCssPath, ops.OutputGzipPath);

            return builder;
        }

        public static void GzipCompress(string sourcePath, string outPath)
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