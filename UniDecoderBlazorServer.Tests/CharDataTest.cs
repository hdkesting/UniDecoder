namespace UniDecoderBlazorServer.Tests;

using UniDecoderBlazorServer.Components.Shared;
using UniDecoderBlazorServer.Services;

[TestClass]
public class CharDataTest : BunitTestContext
{
    [TestMethod]
    public void CharData_ShowsCorrectDetails()
    {
        const string testValue = "a";
        var svc = new UnidecoderService();
        var testchar = svc.ListCharacters(testValue)[0];

        var cut = RenderComponent<CharData>(parameters => parameters
                    .Add(p => p.Character, testchar)
                    .Add(p => p.Position, CharData.ElementPosition.Single)
                    .Add(p => p.Element, testValue));

        // Assert
        cut.Find("div div").MarkupMatches($"<div class=\"latin sample\" onclick=\"cp({testchar.Codepoint}, '{testchar.Name}')\">{testchar.Character}</div>");

        var element = cut.Find("div div");
        Assert.AreEqual(testchar.Character, element.InnerHtml);
        Assert.AreEqual(testchar.Codepoint.ToString(), element.GetAttribute("onclick")?.Split('(')[1].Split(',')[0]);

        // TODO get actually the value of 2nd parameter in a less brittle way
        Assert.AreEqual(testchar.Name, element.GetAttribute("onclick")?.Split('(')[1].Split(',')[1].Trim('\'', ' ', ')'));
    }
}
