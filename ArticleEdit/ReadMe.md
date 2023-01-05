# Type Script Dev Environment- Setup

[TypescriptOrg]: https://www.typescriptlang.org/
[Nuget]: https://www.nuget.org/
[RequireJS]: https://requirejs.org/
[RequireJS-Sample]: http://mk-prg-net.de/Woc/Woc?wocId=js.modules.require-js.html&colmax=1
[JQuery]: https://jquery.com/
[JQueryTSModule]: https://www.nuget.org/packages/jquery.TypeScript.DefinitelyTyped/

The goal of this project is to find out an Setup for a [TypeScript][TypeScriptOrg] Dev- Environment

## Folder Hirarchy

```
mkit
 +-Tools
 |   +- Microsoft.TypeScript.Compiler.3.1.5
 |       +- TSC.exe
 |
 +-TS-Test
     +- index.html
     +- init-ts.bat  // extends the PATH- Environment- Var with Path of TSC.exe Compiler
     +- Nuget.Config // Defines all package sources for Nuget.exe
     +- nuget.exe    // Newest Nuget CLI- Tool 
     +- ReadMe.md
     +- tsconfig.json
     +- Content
     |   +- bootstrap*.css 
     |
     + Scripts
        +- bootstrap*.js
        +- jquery*.js
        +- require.js       // AMD- Loader for Browser
        +- ATMO.DFC.Naming  // All TypeScript- Sources
        +- ATMO.DFC.Naming.JS // All JavaScript files resulting from TSC.exe Compilations  
```

## Installiere das Microsoft.TypeScript.MSBuild Nuget Pkg in der Webanwendung

Konfiguriere anschließend in den Projeteinstellungen die Typescript- Sektion

## Install TypeScript Compiler via Nuget.exe

```
nuget install microsoft.typescript.compiler -Version 3.1.5
```
## Install [RequireJS][RequireJS] as AMD Loader via Nuget.exe

Because ES6 Loading of Modules works not properly in **Chrome** browsers, I switched back to use [RequireJS][RequireJS] for this.

```cmd
nuget install RequireJS -Version 2.3.6
```

## Install JQuery TypeScript Declaration Files

To import [JQuery][JQuery] as a Module, this [TypeScript Module declaration][JQueryTSModule] is needed.

```cmd
nuget install jquery.TypeScript.DefinitelyTyped
```

## Install IIS Express Extension to Visual Studio Code

Extension ID: `warren-buckley.iis-express`

This Extension enables convinet hosting of Websites in IIS- Express like in Visual Studio for Web- Projects. Hosting can be easily started and stoped from VS- Code. Makes debugging of Web Projects much easier. 


