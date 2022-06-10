namespace Socolin.ANSITerminalColor.Tests;

public class AnsiColorTests
{
	private const char EscapeCode = '\x1b';

	[Test]
	[TestCaseSource(nameof(ColorCodeTestCases))]
	public void GenerateExpectedCode(AnsiColor code, string expected)
	{
		Console.WriteLine(AnsiColor.ColorizeText("Test text", code));
		Console.WriteLine(string.Join("ㅤ", code.ToString().ToCharArray()));
		Console.WriteLine(string.Join("ㅤ", expected.ToCharArray()));
		Assert.That(code.ToString().ToCharArray(), Is.EqualTo(expected.ToCharArray()));
	}

	private static readonly IEnumerable<TestCaseData> ColorCodeTestCases = new[]
	{
		new TestCaseData(new AnsiColor(TerminalControlSequences.Bold), EscapeCode + "[1m"),
		new TestCaseData(AnsiColor.Bold, EscapeCode + "[1m"),
		new TestCaseData(AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C122), EscapeCode + "[38;5;122m"),
		new TestCaseData(AnsiColor.Background(Terminal256ColorCodes.Aquamarine1C122), EscapeCode + "[48;5;122m"),
		new TestCaseData(AnsiColor.Background(TerminalRgbColor.From(94, 6, 0)), EscapeCode + "[48;2;94;6;0m"),
		new TestCaseData(AnsiColor.Foreground(255, 16, 240), EscapeCode + "[38;2;255;16;240m"),
		new TestCaseData(AnsiColor.Composite(AnsiColor.Bold, AnsiColor.Underline, AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C86)), EscapeCode + "[1;4;38;5;86m"),
	};

	[Test]
	public void VisualTest()
	{
		Console.WriteLine(AnsiColor.ColorizeText(
			"some-text",
			AnsiColor.Composite(
				AnsiColor.Bold,
				AnsiColor.Underline,
				AnsiColor.Foreground(Terminal256ColorCodes.Gold3C178)
			)));
		Console.WriteLine(AnsiColor.Bold + "Bold" + AnsiColor.Reset);
		Console.WriteLine(AnsiColor.Underline + "Underline" + AnsiColor.Reset);
		Console.WriteLine(AnsiColor.Italic + "Italic" + AnsiColor.Reset);
		var compositeCode = AnsiColor.Composite(AnsiColor.Bold, AnsiColor.Underline, AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C86));
		Console.WriteLine(compositeCode + "Test" + AnsiColor.Reset);
		Console.WriteLine(AnsiColor.ColorizeText("Colored Text", compositeCode) + " non colored text");


		Console.WriteLine(AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C122) + "Test" + AnsiColor.Reset);
		Console.WriteLine(AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C122) + "Test" + AnsiColor.Reset);
	}
}