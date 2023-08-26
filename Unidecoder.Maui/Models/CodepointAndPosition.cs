using Unidecoder.Maui.Support;

namespace Unidecoder.Maui.Models;

public class CodepointAndPosition :IEquatable<CodepointAndPosition>
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

    public bool Equals(CodepointAndPosition? other)
    {
        return this.Position == other?.Position && this.Codepoint.Equals(other?.Codepoint);
    }

    public override string ToString() => Position switch
    {
        CodepointPosition.Single => $"⦃{Codepoint.Character}⦄",
        CodepointPosition.First => $"⦃{Codepoint.Character}",
        CodepointPosition.Middle => $"{Codepoint.Character}",
        CodepointPosition.Last => $"{Codepoint.Character}⦄",
        _ => "?",
    };
}
