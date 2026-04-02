using Microsoft.Extensions.DependencyInjection;
using StableCube.Bulzor.Components.MediaPlayer;

namespace StableCube.Bulzor.Components;

public class OptionsBuilder
{
    public OptionsBuilder(IServiceCollection services, BulzorConfig options)
    {
        services.AddSingleton(options);

        services.AddScoped<MediaPlayerCommands>();
        services.AddScoped<MediaPlayerEvents>();
    }
}
