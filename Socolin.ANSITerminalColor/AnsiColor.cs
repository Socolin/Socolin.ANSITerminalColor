using System.Text;
using JetBrains.Annotations;

namespace Socolin.ANSITerminalColor;

[PublicAPI]
public readonly struct AnsiColor
{
	private const char EscapeCode = '\x1b';
	public AnsiColor[]? Codes { get; } = null;

	public static AnsiColor Reset = new(TerminalControlSequences.Reset);
	public static AnsiColor Bold = new(TerminalControlSequences.Bold);
	public static AnsiColor Italic = new(TerminalControlSequences.Italic);
	public static AnsiColor Underline = new(TerminalControlSequences.Underline);
	public static AnsiColor Strike = new(TerminalControlSequences.Strike);

	public static AnsiColor Foreground(Terminal256ColorCodes color) => new(TerminalControlSequences.SetForegroundColor, color);
	public static AnsiColor Foreground(TerminalRgbColor color) => new(TerminalControlSequences.SetForegroundColor, color);
	public static AnsiColor Foreground(int r, int g, int b) => new(TerminalControlSequences.SetForegroundColor, new TerminalRgbColor(r, g, b));
	public static AnsiColor Background(Terminal256ColorCodes color) => new(TerminalControlSequences.SetBackgroundColor, color);
	public static AnsiColor Background(TerminalRgbColor color) => new(TerminalControlSequences.SetBackgroundColor, color);
	public static AnsiColor Background(int r, int g, int b) => new(TerminalControlSequences.SetBackgroundColor, new TerminalRgbColor(r, g, b));
	public static AnsiColor Composite(params AnsiColor[] codes) => new(codes);

#if NET3_0_OR_GREATER
	[return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("text")]
#endif
	public static string? ColorizeText(string? text, AnsiColor code)
	{
		var sb = new StringBuilder();
		code.ToEscapeSequence(sb);
		sb.Append(text);
		Reset.ToEscapeSequence(sb);
		return sb.ToString();
	}

	public readonly TerminalControlSequences ControlSequence = TerminalControlSequences.None;
	public readonly Terminal256ColorCodes ColorCode256 = Terminal256ColorCodes.None;
	public readonly TerminalRgbColor RgbColorCode = TerminalRgbColor.None;

	public AnsiColor(TerminalControlSequences controlSequence)
	{
		ControlSequence = controlSequence;
		switch (ControlSequence)
		{
			case TerminalControlSequences.SetForegroundColor:
			case TerminalControlSequences.SetBackgroundColor:
				throw new InvalidColorException($"{ControlSequence} requires a color parameter");
		}
	}

	public AnsiColor(TerminalControlSequences controlSequence, Terminal256ColorCodes colorCode256)
	{
		ControlSequence = controlSequence;
		ColorCode256 = colorCode256;
	}

	public AnsiColor(TerminalControlSequences controlSequence, TerminalRgbColor rgbColor)
	{
		ControlSequence = controlSequence;
		RgbColorCode = rgbColor;
	}

	public AnsiColor(params AnsiColor[] codes)
	{
		Codes = codes;
	}

	public string Colorize(string? text)
	{
		var sb = new StringBuilder();
		ToEscapeSequence(sb);
		sb.Append(text);
		Reset.ToEscapeSequence(sb);
		return sb.ToString();
	}

	public override string ToString()
	{
		return ToEscapeSequence();
	}

	public void ToString(StringBuilder sb)
	{
		ToEscapeSequence(sb);
	}

	public string ToEscapeSequence()
	{
		var sb = new StringBuilder();
		ToEscapeSequence(sb);
		return sb.ToString();
	}

	public void ToEscapeSequence(StringBuilder sb)
	{
		sb.Append(EscapeCode);
		sb.Append('[');
		if (Codes != null && Codes.Length > 0)
		{
			foreach (var colorCode in Codes)
			{
				colorCode.ToEscapeParameters(sb);
				sb.Append(';');
			}

			sb.Length--;
			sb.Append('m');
			return;
		}

		ToEscapeParameters(sb);
		sb.Append('m');
	}

	public void ToEscapeParameters(StringBuilder sb)
	{
		switch (ControlSequence)
		{
			case TerminalControlSequences.SetForegroundColor:
			case TerminalControlSequences.SetBackgroundColor:
				sb.Append((int)ControlSequence);
				sb.Append(';');
				if (ColorCode256 != Terminal256ColorCodes.None)
					sb.Append(ColorCode256.ToColorParameter());
				else if (RgbColorCode != TerminalRgbColor.None)
					sb.Append(RgbColorCode.ToColorParameter());
				else
					throw new InvalidColorException($"{ControlSequence} requires a color parameter");
				break;
			default:
				sb.Append((int)ControlSequence);
				break;
		}
	}
}