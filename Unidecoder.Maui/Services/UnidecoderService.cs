using System.Linq;
using System.Unicode;

using Unidecoder.Maui.Models;
using Unidecoder.Maui.Support;

namespace Unidecoder.Maui.Services;

public class UnidecoderService
{
    private const int MaxResultCount = 40;
    private const int LowestPossibleCodepoint = 0x0000;
    private const int HighestPossibleCodepoint = 0x10FFFF;

    private int? totalCharacterCount;

    public int MaxResults => MaxResultCount;

    /// <summary>
    /// Lists the characters in the supplied <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
    public List<CodepointInfo> ListCharacters(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new List<CodepointInfo>();
        }

        var result = input.AsPermissiveCodePointEnumerable().Select(cp => new CodepointInfo(UnicodeInfo.GetCharInfo(cp))).ToList();
        return result;
    }

    public List<StringElement> ListElements(string input)
    {
        System.Globalization.TextElementEnumerator charEnum = System.Globalization.StringInfo.GetTextElementEnumerator(input);
        var res = new List<StringElement>();
        while (charEnum.MoveNext())
        {
            var se = new StringElement(charEnum.GetTextElement());
            se.Codepoints.AddRange(se.Element.AsPermissiveCodePointEnumerable().Select(cp => new CodepointInfo(UnicodeInfo.GetCharInfo(cp))));
            res.Add(se);
        }

        return res;
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>A dictionary combining the id and the name.</returns>
    public Dictionary<int, string> GetAllCategories()
    {
        return Enum.GetValues(typeof(System.Globalization.UnicodeCategory))
            .Cast<System.Globalization.UnicodeCategory>()
            .Where(uc => uc != System.Globalization.UnicodeCategory.PrivateUse)
            .Where(uc => uc != System.Globalization.UnicodeCategory.Surrogate)
            .ToDictionary(c => (int)c, c => c.ToString().ToSeparateWords());
    }

    /// <summary>
    /// Gets all blocks.
    /// </summary>
    /// <returns>A dictionary combining the internal id and the name.</returns>
    public Dictionary<int, string> GetAllBlocks()
    {
        return UnicodeInfo.GetBlocks()
            .Select((b, i) => new { Block = b, Index = i })
            .ToDictionary(b => b.Index, b => b.Block.Name);
    }

    /// <summary>
    /// Gets the count of all known characters.
    /// </summary>
    /// <returns>The count.</returns>
    public int GetTotalCharacterCount()
    {
        // the count does not change, so cache it
        return totalCharacterCount ??= Enumerable.Range(LowestPossibleCodepoint, HighestPossibleCodepoint - LowestPossibleCodepoint)
            .Count(CodepointExists);
    }

    /// <summary>
    /// Gets the supported unicode version.
    /// </summary>
    /// <returns>The version.</returns>
    public Version GetUnicodeVersion()
    {
        return UnicodeInfo.UnicodeVersion;
    }

    /// <summary>
    /// Finds the characters by their name, <em>lazily</em> returning the matches.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <returns>A sequence of <see cref="StringElement"/>s.</returns>
    public IEnumerable<StringElement> FindByName(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            yield break;
        }

        searchText = searchText.Trim();

        int count = 0;

        for (int cp = LowestPossibleCodepoint; cp <= HighestPossibleCodepoint; cp++)
        {
            if (CodepointExists(cp))
            {
                var charinfo = UnicodeInfo.GetCharInfo(cp);
                if (NameMatches(searchText, charinfo.Name))
                {
                    var cpinfo = new CodepointInfo(charinfo);
                    var se = new StringElement(cpinfo.Character) { Codepoints = { cpinfo } };
                    count++;
                    System.Diagnostics.Debug.WriteLine($"UnidecoderService/FindByName - found {count}th value: {cpinfo.Name}.");
                    yield return se;
                }
            }
        }
    }

    /// <summary>
    /// Finds characters the around the supplied codepoint value.
    /// </summary>
    /// <param name="codepoint">The codepoint.</param>
    /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
    public List<CodepointInfo> FindAroundValue(int codepoint)
    {
        const int halfRange = 8;

        var list = Enumerable.Range(codepoint - halfRange, halfRange * 2)
            .Where(n => n >= LowestPossibleCodepoint)
            .Where(n => n <= HighestPossibleCodepoint)
            .Where(CodepointExists)
            .Select(UnicodeInfo.GetCharInfo)
            .Where(it => !string.IsNullOrWhiteSpace(it.Name))
            .Select(it => new CodepointInfo(it))
            .ToList();

        return list;
    }

    /// <summary>
    /// Gets all the characters of a named block.
    /// </summary>
    /// <param name="blockName">Name of the block.</param>
    /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
    public IEnumerable<StringElement> GetCharactersOfBlock(string blockName)
    {
        var block = UnicodeInfo.GetBlocks().FirstOrDefault(b => b.Name.Equals(blockName, StringComparison.OrdinalIgnoreCase));

        if (block.Name is null)
        {
            yield break;
        }

        foreach (var cpi in block.CodePointRange
            .Select(UnicodeInfo.GetCharInfo)
            .Where(it => !string.IsNullOrWhiteSpace(it.Name))
            .Select(it => new CodepointInfo(it)))
        {
            yield return new StringElement(cpi.Character) { Codepoints = { cpi } }; 
        }
    }

    /// <summary>
    /// Gets all the characters in the specified category.
    /// </summary>
    /// <param name="categoryName">Name of the category.</param>
    /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
    public IEnumerable<StringElement> GetCharactersOfCategory(string categoryName)
    {
        categoryName = categoryName.Replace(" ", string.Empty); // remove all spaces

        if (Enum.TryParse(categoryName, true, out System.Globalization.UnicodeCategory cat))
        {
            for (var cp = LowestPossibleCodepoint; cp <= HighestPossibleCodepoint; cp++)
            {
                if (CodepointExists(cp) && UnicodeInfo.GetCategory(cp) == cat)
                {
                    var charinfo = UnicodeInfo.GetCharInfo(cp);
                    if (string.IsNullOrEmpty(charinfo.Name))
                    {
                        continue;
                    }

                    var cpinfo = new CodepointInfo(charinfo);
                    var se = new StringElement(cpinfo.Character) { Codepoints = { cpinfo } };
                    yield return se;
                }
            }
        }
    }

    private bool CodepointExists(int codepoint)
    {
        var cat = UnicodeInfo.GetCategory(codepoint);
        return cat != System.Globalization.UnicodeCategory.OtherNotAssigned;
        //// unfortunately I don't see a more direct/less brittle way
    }

    private bool NameMatches(string match, string source)
    {
        if (source == null)
        {
            return false;
        }

        var searchwords = match.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var sourcewords = source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in searchwords)
        {
            if (word.StartsWith("-"))
            {
                var notword = word.Substring(1);
                if (sourcewords.Any(w => w.StartsWith(notword, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }
            }
            else if (sourcewords.All(w => !w.StartsWith(word, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // no match for this search word
            }
        }

        return true;
    }
}
