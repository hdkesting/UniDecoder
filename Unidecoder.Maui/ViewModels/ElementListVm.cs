namespace Unidecoder.Maui.ViewModels;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using Unidecoder.Maui.Models;

[Obsolete("Code is now directly in control")]
public partial class ElementListVm : ObservableObject
{
    const int initialCount = 15;
    const int additionalCount = 7;
    private int elementsConverted;

    private IList<StringElement>? elements;

    [ObservableProperty]
    private ObservableCollection<Models.CodepointAndPosition> _codepoints = new ();

    [ObservableProperty]
    private string _elementCount = "!?";

    public ICommand TresholdReached { get; init; }

    public ElementListVm()
    {
        TresholdReached = new Command(OnTresholdReached);
    }

    internal void ElementsChanged(IList<StringElement> elements)
    {
        Codepoints.Clear();
        this.elements = elements;

        var initiallist = TranslateElements(elements.Take(initialCount));
        foreach (var element in initiallist)
        {
            Codepoints.Add(element);
        }

        elementsConverted = initialCount;
        System.Diagnostics.Debug.WriteLine($">> ElementsChanged: converted {elementsConverted} items into Codepoints out of {elements.Count}");

        ElementCount = $"{elements.Count} -> {Codepoints.Count}";
    }

    public void OnTresholdReached()
    {
        if (elements is null || elementsConverted >= elements.Count)
        {
            System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: got all {elements?.Count ?? -1}");
            return;
        }

        foreach (var elt in TranslateElements(elements.Skip(elementsConverted).Take(additionalCount)))
        {
            Codepoints.Add(elt);
        }

        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: added {additionalCount} items to Codepoints (total {Codepoints.Count})");
        elementsConverted += additionalCount;
        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: converted {elementsConverted} items into Codepoints out of {elements.Count}");
    }

    private static IList<Models.CodepointAndPosition> TranslateElements(IEnumerable<StringElement> elements)
    {
        var list = new List<CodepointAndPosition>();
        foreach (var element in elements)
        {
            if (element.Codepoints.Count == 1)
            {
                list.Add(new CodepointAndPosition(element.Codepoints[0], Support.CodepointPosition.Single));
            }
            else
            {
                list.Add(new CodepointAndPosition(element.Codepoints[0], Support.CodepointPosition.First) { Element = element });
                foreach (var sub in element.Codepoints.Skip(1).Take(element.Codepoints.Count - 2))
                {
                    list.Add(new CodepointAndPosition(sub, Support.CodepointPosition.Middle));
                }

                list.Add(new CodepointAndPosition(element.Codepoints.Last(), Support.CodepointPosition.Last));
            }
        }

        return list;
    }
}
