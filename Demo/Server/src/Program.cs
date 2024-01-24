using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using StableCube.Bulzor.Demo.Server;
using StableCube.Bulzor.Demo.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSystemd();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddRazorPages()
.AddRazorPagesOptions(options => 
{
    options.RootDirectory = "/src/Pages";
});

builder.Services.Configure<RouteOptions>(option =>
{
    option.LowercaseUrls = true;
    option.LowercaseQueryStrings = true;
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSassCompiler(options =>
    {
        options.DirectoriesToWatch.Add("../Client/src/Pages");
        options.FilesToWatch.Add("wwwroot/css/base.scss");
    });
}

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(StableCube.Bulzor.Demo.Client._Imports).Assembly);

app.Run();