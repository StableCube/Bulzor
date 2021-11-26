using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace StableCube.Bulzor.Demo.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()           
                .UseSerilog((context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                    if(IsInContainer() && !IsRemoteDev())
                        configuration.WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter());
                });

            return builder;
        }

        private static bool IsInContainer()
        {
            string envVal = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
            if(String.IsNullOrEmpty(envVal))
                return false;

            return Convert.ToBoolean(envVal);
        }

        private static bool IsRemoteDev()
        {
            string envVal = Environment.GetEnvironmentVariable("REMOTE_DEV_MODE");
            if(String.IsNullOrEmpty(envVal))
                return false;

            if(!Int32.TryParse(envVal.AsSpan(), out int isRemote))
                return false;

            return isRemote == 1 ? true : false;
        }
    }
}