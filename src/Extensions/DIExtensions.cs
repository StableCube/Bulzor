using System;
using StableCube.Bulzor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIExtensions
    {
        public static OptionsBuilder AddBulzer(this IServiceCollection services)
        {
            BulzorOptions ops = new BulzorOptions();
            return new OptionsBuilder(services, ops);
        }

        public static OptionsBuilder AddBulzer(
            this IServiceCollection services, Action<BulzorOptions> optionsAction)
        {
            BulzorOptions ops = new BulzorOptions();
            optionsAction.Invoke(ops);

            return new OptionsBuilder(services, ops);
        }
    }
}