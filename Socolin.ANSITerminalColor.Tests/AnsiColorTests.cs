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
	[TestCaseSource(nameof(ColorizeResetColorCodeTestCases))]
	public void ColorizeAppendExpectedReset(AnsiColor code, string expected)
	{
		var actual = code.Colorize("Test");
		Console.WriteLine(actual);
		Console.WriteLine(string.Join("ㅤ", actual.ToCharArray()));
		Console.WriteLine(string.Join("ㅤ", expected.ToCharArray()));
		Assert.That(actual.ToCharArray(), Is.EqualTo(expected.ToCharArray()));
	}

	private static readonly IEnumerable<TestCaseData> ColorizeResetColorCodeTestCases = new[]
	{
		new TestCaseData(new AnsiColor(TerminalControlSequences.Bold), EscapeCode + "[1mTest" + EscapeCode + "[2m") {TestName = "Bold"},
		new TestCaseData(AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C122), EscapeCode + "[38;5;122mTest" + EscapeCode + "[39m") {TestName = "Foreground"},
		new TestCaseData(AnsiColor.Background(Terminal256ColorCodes.Aquamarine1C122), EscapeCode + "[48;5;122mTest" + EscapeCode + "[49m") {TestName = "Background"},
		new TestCaseData(AnsiColor.Composite(AnsiColor.Bold, AnsiColor.Underline, AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C86)), EscapeCode + "[1;4;38;5;86mTest" + EscapeCode + "[2;24;39m") {TestName = "Composite"},
	};


	[Test]
	[TestCaseSource(nameof(ResetColorCodes))]
	public void ColorizeAppendExpectedReset(AnsiColor code, TerminalControlSequences expected)
	{
		Assert.That(code.ResetSequence, Is.EqualTo(expected));
	}

	private static readonly IEnumerable<TestCaseData> ResetColorCodes = new[]
	{
		new TestCaseData(AnsiColor.Bold, TerminalControlSequences.Faint),
		new TestCaseData(AnsiColor.Strike, TerminalControlSequences.NotCrossedOut),
		new TestCaseData(new AnsiColor(TerminalControlSequences.RapidBlink), TerminalControlSequences.NotBlinking),
		new TestCaseData(new AnsiColor(TerminalControlSequences.SlowBlink), TerminalControlSequences.NotBlinking),
		new TestCaseData(new AnsiColor(TerminalControlSequences.Underline), TerminalControlSequences.NotUnderline),
		new TestCaseData(new AnsiColor(TerminalControlSequences.Italic), TerminalControlSequences.NeitherItalicNorBlackletter),
		new TestCaseData(new AnsiColor(TerminalControlSequences.Hide), TerminalControlSequences.Reveal),
		new TestCaseData(AnsiColor.Foreground(Terminal256ColorCodes.Aquamarine1C122), TerminalControlSequences.DefaultForegroundColor),
		new TestCaseData(AnsiColor.Background(Terminal256ColorCodes.Aquamarine1C122), TerminalControlSequences.DefaultBackgroundColor),
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


		var boldBlue = AnsiColor.Composite(
			AnsiColor.Foreground(Terminal256ColorCodes.CadetBlueC73),
			AnsiColor.Bold
		);
		var orangeBackgroundUnderline = AnsiColor.Composite(
			AnsiColor.Background(Terminal256ColorCodes.Orange4C58),
			AnsiColor.Underline
		);
		Console.WriteLine(boldBlue.Colorize("Bold Blue " + orangeBackgroundUnderline.Colorize("Orange Underlined") + " Bold Blue"));
		Console.WriteLine(orangeBackgroundUnderline.Colorize("Orange Underlined " + boldBlue.Colorize("Bold Blue") + " Orange Underlined"));
	}
}