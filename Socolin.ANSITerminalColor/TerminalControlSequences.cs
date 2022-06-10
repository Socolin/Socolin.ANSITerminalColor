using JetBrains.Annotations;

namespace Socolin.ANSITerminalColor;

[PublicAPI]
[Flags]
public enum TerminalControlSequences
{
	None = -1,
	Reset = 0,
	Bold = 1,
	Faint = 2,
	Italic = 3,
	Underline = 4,
	SlowBlink = 5,
	RapidBlink = 6,
	Invert = 7,
	Hide = 8,
	Strike = 9,
	PrimaryFont = 10,
	AlternativeFont1 = 11,
	AlternativeFont2 = 12,
	AlternativeFont3 = 13,
	AlternativeFont4 = 14,
	AlternativeFont5 = 15,
	AlternativeFont6 = 16,
	AlternativeFont7 = 17,
	AlternativeFont8 = 18,
	AlternativeFont9 = 19,
	Fraktur = 20,
	DoublyUnderlined = 21,
	NormalIntensity = 22,
	NeitherItalicNorBlackletter = 23,
	NotUnderline = 24,
	NotBlinking = 25,
	NotReverse = 27,
	Reveal = 28,
	NotCrossedOut = 29,
	SetForegroundColor = 38,
	DefaultForegroundColor = 39,
	SetBackgroundColor = 48,
	DefaultBackgroundColor = 49,
}