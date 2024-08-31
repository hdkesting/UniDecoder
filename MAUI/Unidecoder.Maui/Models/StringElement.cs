namespace Unidecoder.Maui.Models;

/// <summary>
/// A set of characters that belong together.
/// </summary>
public class StringElement
{
    public string Element { get; set; }

    public List<CodepointInfo> Codepoints { get; set; } = new List<CodepointInfo>();

    public StringElement(string element)
    {
        Element = element;
    }

    public override string ToString() => $"{Element} ({Codepoints.Count})";
}
