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

	/// <summary>
	/// The full element this is codepoint part of.
	/// </summary>
	/// <remarks>Only needed for a 'First' cp.</remarks>
    public StringElement? Element { get; init; }
}
