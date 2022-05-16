namespace UniDecoderBlazorServer.Models;

public class StringElement
{
    public string Element { get; set; }

    public List<CodepointInfo> Codepoints { get; set; } = new List<CodepointInfo>();

    public StringElement(string element)
    {
        Element = element;
    }
}
