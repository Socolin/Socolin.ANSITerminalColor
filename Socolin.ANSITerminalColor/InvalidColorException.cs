namespace Socolin.ANSITerminalColor;

public class InvalidColorException : Exception
{
	public InvalidColorException(string? message)
		: base(message)
	{
	}
}