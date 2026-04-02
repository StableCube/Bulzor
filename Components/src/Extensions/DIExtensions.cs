using System;
using StableCube.Bulzor.Components;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static OptionsBuilder AddBulzor(this IServiceCollection services, Action<BulzorConfig> optionsAction)
    {
        BulzorConfig ops = new();
        optionsAction.Invoke(ops);

        return new OptionsBuilder(services, ops);
    }

    public static OptionsBuilder AddBulzor(this IServiceCollection services)
    {
        BulzorConfig ops = new();

        return new OptionsBuilder(services, ops);
    }
}
