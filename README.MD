# About

Bulzor is a component system for Blazor built on top of the Bulma css framework. One of the main goals of Bulzor is to only use JavaScript where absolutely necessary. Since Bulma is a purely CSS framework it makes it a great pair for Blazor.

## Live Demo

A live demo as well as additional documentation can be found here [bulzor.stablecube.com](https://bulzor.stablecube.com)


## Sample Project

A sample client/server project can be found in the [Demo directory](https://github.com/StableCube/Bulzor/tree/master/Demo)

## Installation

Install the [Nuget package](https://www.nuget.org/packages/StableCube.Bulzor.Components/) into your project.

```bash
dotnet add package StableCube.Bulzor.Components --version 0.2.8
```

Or install the extended [Nuget package](https://www.nuget.org/packages/StableCube.Bulzor.Components.Extended/) for the base components as well as additional tools not included in base Bulma.

```bash
dotnet add package StableCube.Bulzor.Components.Extended --version 0.2.8
```

## Building CSS With NPM

Bulma uses SASS to build their CSS. There are a variety of ways to build the SASS into usable CSS but NPM is one of the easier options. Below is an example of the packages needed to compile the SASS as well as adding a prefix to the class names to avoid conflicts.

#### package.json
```json
{
  "license": "MIT",
  "dependencies": {},
  "devDependencies": {
    "bulma": "^0.8.2",
    "bulzor-components-extended": "^0.2.3",
    "node-sass": "^4.13.1",
    "postcss": "^7.0.30",
    "postcss-cli": "^7.1.2",
    "postcss-prefixer": "^2.1.2"
  },
  "scripts": {
    "build": "npm run build-sass && npm run build-prefix",
    "build-sass": "node-sass --output-style expanded --source-map true _StyleRoot.scss wwwroot/css/bulzor.base.css",
    "build-prefix": "postcss --no-map --output wwwroot/css/bulzor.css wwwroot/css/bulzor.base.css --verbose"
  }
}
```

#### postcss.config.js
```js
module.exports = (ctx) => ({
    map: ctx.options.map,
    plugins: [
        require('postcss-prefixer')({ 
            prefix: 'bul-' 
        })
    ]
});
```

Currently the extended components requires a small amount of interop code which can be installed with the **bulzor-components-extended** package

```bash
npm i bulzor-components-extended
```


## Configure CSS Prefix

Register the CSS prefix you chose when you compiled the SASS

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