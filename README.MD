# About

Bulzor is a component system for Blazor built on top of the Bulma css framework. One of the main goals of Bulzor is to only use JavaScript where absolutely necessary. Since Bulma is a purely CSS framework it makes it a great pair for Blazor.

Some things are not implemented such as the layout elements. These do not benefit from Blazor components and can be used 
in the usual way laid out in the Bulma docs.

## Live Demo

A live demo as well as additional documentation can be found here [bulzor.stablecube.com](https://bulzor.stablecube.com)


## Sample Project

A sample client/server project can be found in the [Demo directory](https://github.com/StableCube/Bulzor/tree/master/Demo)

## Installation

Install the [Nuget package](https://www.nuget.org/packages/StableCube.Bulzor.Components/) into your project.

Or install the extended [Nuget package](https://www.nuget.org/packages/StableCube.Bulzor.Components.Extended/) for the base components as well as additional tools not included in base Bulma.

## Building CSS With NPM

Pull in the Bulma and extended source Sass however you would like. NPM in a convenient option.

#### package.json
```json
{
  "license": "MIT",
  "dependencies": {},
  "devDependencies": {
    "bulma": "^0.8.2",
    "bulzor-components-extended": "^0.2.3"
  }
}
```

## Configure CSS Prefix

If you build your sass with a namespace prefix you can set this in Bulzor.

```C#
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        new BulzorOptionsBuilder(builder.Services, new BulzorConfig()
        {
            CssPrefix = "bul-"
        });
    }
}
```

## Register Components in Client

#### _Imports.razor
```
@using StableCube.Bulzor.Components
@using StableCube.Bulzor.Components.Extended