This is a copy of [Martin Ender's Retina](https://github.com/m-ender/retina) to be built with .NET Core.

# Retina

Retina is a regex-based recreational programming language. Its main feature is taking some text via standard input and repeatedly applying regex operations to it (e.g. matching, splitting, and most of all replacing). Under the hood, it uses .NET's regex engine, which means that both the .NET flavour and the ECMAScript flavour are available.

Retina was mainly developed for [Code golf](https://en.wikipedia.org/wiki/Code_golf) which may explain its very terse configuration syntax and some weird design decisions.

## Building Retina
You will need .NET Core. 
Run `cd Retina/Retina` to get to the main Retina project. From here you can build the project using `dotnet publish -o <OUTPUT_DIR> -r <RID>`, where `<OUTPUT_DIR>` is (obviously) the directory you want the executable to go, and `<RID>` is one of `linux-x64`, `win-x64`, `win-x86`, or `osx-x64` depending on your platform.
This will produce an executable named either `Retina` or `Retina.exe` as well as a bunch of `.dll` files.

For development, you can of course run `dotnet run` or `dotnet build` instead of `dotnet publish` if you know what you're doing.

## How does it work?

Full documentation of the language **[can be found in the Wiki](https://github.com/m-ender/retina/wiki/The-Language)**. It might also be worth having a look at the **[changelog](https://github.com/m-ender/retina/blob/master/CHANGELOG.md)**.
