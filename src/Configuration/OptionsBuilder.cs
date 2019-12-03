using System;
using Microsoft.Extensions.DependencyInjection;

namespace StableCube.Bulzor
{
    public class OptionsBuilder
    {
        private IServiceCollection _services;
        private BulzorOptions _options;
        
        public OptionsBuilder(IServiceCollection services, BulzorOptions options)
        {
            _services = services;
            _options = options;


            services.AddSingleton(options);
        }
    }
}
