using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace StableCube.Bulzor
{
    public static class StaticFileMiddleware
    {
        public static IApplicationBuilder UseBulzorStaticFiles(
            this IApplicationBuilder builder,
            Action<BulzorStaticFileOptions> optionsAction
        )
        {
            BulzorStaticFileOptions ops = new BulzorStaticFileOptions();
            optionsAction.Invoke(ops);

            if(!Directory.Exists(ops.RootDir))
                Directory.CreateDirectory(ops.RootDir);

            var staticOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ops.RootDir),
                RequestPath = "/_content/StableCube.Bulzor/css-compiled",
            };

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

            builder.UseStaticFiles(staticOptions);

            return builder;
        }
    }
}