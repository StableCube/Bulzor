using Microsoft.Extensions.DependencyInjection;

namespace StableCube.Bulzor.Components
{
    public class BulzorOptionsBuilder
    {
        private IServiceCollection _services;

        public BulzorOptionsBuilder(IServiceCollection services, BulzorConfig config)
        {
            _services = services;

            _services.AddSingleton(config);

            CssConfig.Prefix = config.CssPrefix;
        }
    }
}