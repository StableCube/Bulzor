# About

Bulzor is a component system for Blazor built on top of the Bulma css framework. One of the main goals of Bulzor is to only use JavaScript where absolutely necessary. Since Bulma is a purely CSS framework it makes it a great pair for Blazor.

Some things are not implemented such as the layout elements. These do not benefit from Blazor components and can be used 
in the usual way laid out in the Bulma docs.

## Live Demo

A live demo as well as additional documentation can be found here [bulzor.stablecube.com](https://bulzor.stablecube.com)


## Sample Project

A sample client/server project can be found in the [Demo directory](https://github.com/StableCube/Bulzor/tree/master/Demo)

This sample project is the source code for the live demo and should give you a good understanding of how everything works.

## Installation

Install the [Nuget package](https://www.nuget.org/packages/StableCube.Bulzor.Components/) into your project.

## Grabbing Sass and javascript With NPM

Pull in the Bulma sass and extended components sass/javascript however you would like but using the NPM packages in a convenient option. 

The base Bulzor components currently do not use any non Bulma Sass or javascript and do not need to pull in any additional files.

#### package.json
```json
{
  "license": "MIT",
  "dependencies": {},
  "devDependencies": {
    "bulma": "^1.0.*",
    "bulzor": "^1.0.*"
  }
}
```

## Compiling sass to css

You can compile the sass to css in many different ways but the demo project shows how you can do it in an automatic way with component css isolation.
