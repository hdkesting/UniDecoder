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
    /// Finds the characters by their name, returning the first <see cref="MaxResults"/> matches.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <param name="capped">When <see langword="true"/> (default), then limit the result amount.</param>
    /// <param name="cancellationToken">A cancellation token, to stop longer queries on arrival of new input.</param>
    /// <returns>A list of <see cref="StringElement"/>.</returns>
    public List<StringElement> FindByName(string searchText, bool capped = true, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return new List<StringElement>();
        }

        searchText = searchText.Trim();

        var list = Enumerable.Range(LowestPossibleCodepoint, HighestPossibleCodepoint - LowestPossibleCodepoint)
            .Where(CodepointExists)
            .Select(UnicodeInfo.GetCharInfo)
            .Where(x => NameMatches(searchText, x.Name))
            .Select(info => new CodepointInfo(UnicodeInfo.GetCharInfo(info.CodePoint)))
            .Select(cp => new StringElement(cp.Character) { Codepoints = { cp } })
            .Select(cp => { cancellationToken.ThrowIfCancellationRequested(); return cp; });

        if (capped)
        {
            list = list.Take(MaxResultCount);
        }

        return list.ToList();
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
            .Select(it => new CodepointInfo(it))
            .ToList();

        return list;
    }

    /// <summary>
    /// Gets all the characters of a named block.
    /// </summary>
    /// <param name="blockName">Name of the block.</param>
    /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
    public List<CodepointInfo> GetCharactersOfBlock(string blockName)
    {
        var block = UnicodeInfo.GetBlocks().FirstOrDefault(b => b.Name.Equals(blockName, StringComparison.OrdinalIgnoreCase));

        if (block.Name is null)
        {
            return new List<CodepointInfo>();
        }

        var list = block.CodePointRange
            .Select(UnicodeInfo.GetCharInfo)
            .Select(it => new CodepointInfo(it))
            .ToList();

        return list;
    }

    /// <summary>
    /// Gets all the characters in the specified category.
    /// </summary>
    /// <param name="categoryName">Name of the category.</param>
    /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
    public List<CodepointInfo> GetCharactersOfCategory(string categoryName)
    {
        categoryName = categoryName.Replace(" ", string.Empty); // remove all spaces

        if (Enum.TryParse(categoryName, true, out System.Globalization.UnicodeCategory cat))
        {
            var list = Enumerable.Range(LowestPossibleCodepoint, HighestPossibleCodepoint - LowestPossibleCodepoint)
                .Where(CodepointExists)
                .Where(cp => UnicodeInfo.GetCategory(cp) == cat)
                .Select(UnicodeInfo.GetCharInfo)
                .Select(it => new CodepointInfo(it))
                //.Take(200)
                .ToList();

            return list;
        }

        return new List<CodepointInfo>();
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
