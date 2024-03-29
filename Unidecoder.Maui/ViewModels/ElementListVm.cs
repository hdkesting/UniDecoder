﻿namespace Unidecoder.Maui.ViewModels;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using Unidecoder.Maui.Models;

public partial class ElementListVm : ObservableObject
{
    [ObservableProperty]
    private IList<Models.CodepointAndPosition> _codepoints = new List<Models.CodepointAndPosition>();

    [ObservableProperty]
    private string _elementCount = "!?";

    internal void ElementsChanged(IList<StringElement> elements)
    {
        ElementCount = $"{elements.Count} -> {Codepoints.Count}";
        Codepoints = TranslateElements(elements);
    }

    private static IList<Models.CodepointAndPosition> TranslateElements(IList<StringElement> elements)
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
