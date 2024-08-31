namespace Unidecoder.Maui.Support;

/// <summary>
/// The position of the codepoint within the display glyph.
/// </summary>
public enum CodepointPosition
{
    /// <summary>
    /// The glyph consists of a single codepoint.
    /// </summary>
    Single,

    /// <summary>
    /// This is the first codepoint in the glyph.
    /// </summary>
    First,

    /// <summary>
    /// This is a middle codepont in a glyph.
    /// </summary>
    Middle,

    /// <summary>
    /// This is the last codepoint in the glyph.
    /// </summary>
    Last,
}
