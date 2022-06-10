## ANSITerminalColor

[![Nuget](https://img.shields.io/nuget/v/Socolin.ANSITerminalColor)](https://www.nuget.org/packages/Socolin.ANSITerminalColor)
![License:MIT](https://img.shields.io/badge/license-MIT-green)

Helpers to generate [ANSI escape codes](https://en.wikipedia.org/wiki/ANSI_escape_code)

## License

[MIT](Socolin.ANSITerminalColor/LICENSES.md)

## Example

Colorize a text before printing it to the console

```csharp
Console.WriteLine(AnsiColor.ColorizeText("Colored Text", AnsiColor.Bold))
Console.WriteLine(AnsiColor.ColorizeText("Colored Text", AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C86)))
```

Composite multiple modifiers

```csharp
Console.WriteLine(AnsiColor.ColorizeText(
    "some-text",
    AnsiColor.Composite(
        AnsiColor.Bold,
        AnsiColor.Underline,
        AnsiColor.Foreground(Terminal256ColorCodes.Gold3C178)
    )));
```

Using RGB Colors

```csharp
Console.WriteLine(AnsiColor.ColorizeText(
    "some-text",
    AnsiColor.Foreground(255, 16, 240)
);
```

More escape sequence can be used when using `new AnsiColor(...)`