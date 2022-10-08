using Unidecoder.Maui.Support;

namespace Unidecoder.Maui.Models;

public class CodepointAndPosition
{
	public CodepointAndPosition(CodepointInfo codepoint, CodepointPosition position)
	{
		Codepoint = codepoint;
		Position = position;
	}

	public CodepointInfo Codepoint { get; }

	public CodepointPosition Position { get; }
}
