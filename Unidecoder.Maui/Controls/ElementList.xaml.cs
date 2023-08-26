namespace Unidecoder.Maui.Controls;

using System.Collections.ObjectModel;

using Microsoft.Maui.Controls;

using Unidecoder.Maui.Models;

public partial class ElementList : ContentView
{
	public static readonly BindableProperty ElementsProperty
		= BindableProperty.Create(nameof(Elements), typeof(IList<Models.StringElement>), typeof(ElementList),
			defaultValue: new List<Models.StringElement>(),
			propertyChanged: ElementsPropertyChanged);

    private const int InitialCount = 15;
    private const int AdditionalCount = 7;
    private int elementsConverted;

    /// <summary>
    /// Gets or sets the incoming list of elements to show.
    /// </summary>
    public IList<Models.StringElement> Elements
	{
		get => (IList<Models.StringElement>)GetValue(ElementsProperty);
		set => SetValue(ElementsProperty, value);
	}

    public ObservableCollection<Models.CodepointAndPosition> Codepoints { get; } = new();

    public ElementList()
	{
		InitializeComponent();
        //this.VM = MauiProgram.App.Services.GetService<ViewModels.ElementListVm>()
        //    ?? throw new InvalidOperationException("VM not found for DI: ElementListVm");
        // this.BindingContext = this;//  {.VM}; //<- do not do this, this needs the binding context of the surrounding page to bind correctly ???
    }

    private static void ElementsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        System.Diagnostics.Debug.WriteLine("ElementList - ElementsPropertyChanged");
		var this_ = (ElementList)bindable;
		this_.ElementsChanged();
		//this_.OnPropertyChanged(nameof(this_.VM));
	}

    internal void ElementsChanged()
    {
        // do NOT just clear Codepoints and start over: try and keep what is alreday correct, assuming most is just changing (or rather appending) a previous text

        int cpindex = 0;

        var prevElementsConverted = elementsConverted;
        elementsConverted = 0;
        System.Diagnostics.Debug.WriteLine($">> ElementsChanged: Starting new initial {InitialCount}, was {prevElementsConverted}");

        var initiallist = TranslateElements(Elements.Take(Math.Max(InitialCount, prevElementsConverted)));
        foreach (var element in initiallist)
        {
            if (cpindex >= Codepoints.Count)
            {
                System.Diagnostics.Debug.WriteLine($">> ElementsChanged: Add cp {element.Codepoint.Character} at index {cpindex}");
                Codepoints.Add(element);
            }
            else if (!Codepoints[cpindex].Equals(element))
            {
                System.Diagnostics.Debug.WriteLine($">> ElementsChanged: Replace index {cpindex} with cp {element.Codepoint.Character}");
                Codepoints[cpindex] = element;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($">> ElementsChanged: index {cpindex} already has {element.Codepoint.Character}");
            }

            cpindex++;
        }

        while (Codepoints.Count > cpindex)
        {
            System.Diagnostics.Debug.WriteLine($">> ElementsChanged: remove extra at end - total before {Codepoints.Count}");
            Codepoints.RemoveAt(Codepoints.Count-1);
        }

        this.OnPropertyChanged(nameof(Codepoints));

        System.Diagnostics.Debug.WriteLine($">> ElementsChanged: converted {elementsConverted} items into {Codepoints.Count} Codepoints out of {Elements.Count}");
    }

    internal void OnTresholdReached(object sender, EventArgs e)
    {
        if (Elements is null || elementsConverted >= Elements.Count)
        {
            System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: already processed all {Elements?.Count ?? -1}");
            return;
        }

        foreach (var elt in TranslateElements(Elements.Skip(elementsConverted).Take(AdditionalCount)))
        {
            Codepoints.Add(elt);
        }

        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: added {AdditionalCount} items to Codepoints (total {Codepoints.Count})");
        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: converted {elementsConverted} items into Codepoints out of {Elements.Count}");
    }

    private IList<Models.CodepointAndPosition> TranslateElements(IEnumerable<StringElement> elements)
    {
        var list = new List<CodepointAndPosition>();
        foreach (var element in elements)
        {
            elementsConverted++;
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