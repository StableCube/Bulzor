using System;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

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

            string css = SassCompiler.Compile(ops);

            string wwwrootPath = Path.Combine(Path.GetTempPath(), "bulzorwwwroot");
            if(!Directory.Exists(wwwrootPath))
                Directory.CreateDirectory(wwwrootPath);

            string cssPath = Path.Combine(wwwrootPath, ops.OutputCssFilename);
            File.WriteAllText(cssPath, css);

            string gzipPath = Path.Combine(wwwrootPath, ops.OutputGzipFilename);
            if(ops.OutputGzipFilename != null)
                GzipCompress(cssPath, gzipPath);

            var staticOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(wwwrootPath),
                RequestPath = "/_content/StableCube.Bulzor/css-compiled",
            };

            if(ops.OutputGzipFilename != null)
            {
                staticOptions.OnPrepareResponse = context =>
                {
                    var headers = context.Context.Response.Headers;
                    string contentType = headers["Content-Type"];
                    if (contentType == "application/x-gzip")
                    {
                        if (context.File.Name.EndsWith("js.gz"))
                        {
                            contentType = "application/javascript";
                        }
                        else if (context.File.Name.EndsWith("css.gz"))
                        {
                            contentType = "text/css";
                        }
                        headers.Add("Content-Encoding", "gzip");
                        headers["Content-Type"] = contentType;
                    }
                };
            }

            builder.UseStaticFiles(staticOptions);

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