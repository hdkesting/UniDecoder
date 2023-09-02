namespace Unidecoder.Maui.Controls;

using System.Collections.ObjectModel;

using Microsoft.Maui.Controls;

using Unidecoder.Maui.Models;

public partial class ElementList : ContentView
{
	public static readonly BindableProperty ElementsProperty
		= BindableProperty.Create(nameof(Elements), typeof(IEnumerable<Models.StringElement>), typeof(ElementList),
			defaultValue: new List<Models.StringElement>(),
			propertyChanged: ElementsPropertyChanged);

    private const int InitialCount = 15;
    private const int AdditionalCount = 7;
    private const int MaxCount = 50;
    private int elementsConverted;
    private bool allElementsProcessed;

    private IEnumerator<Models.StringElement>? elementsEnumerator;

    /// <summary>
    /// Gets or sets the incoming list of elements to show.
    /// </summary>
    public IEnumerable<Models.StringElement> Elements
	{
		get => (IEnumerable<Models.StringElement>)GetValue(ElementsProperty);
		set => SetValue(ElementsProperty, value);
	}

    public ObservableCollection<Models.CodepointAndPosition> Codepoints { get; } = new();

    public ElementList()
	{
		InitializeComponent();
        // this.BindingContext = this; //<- do not do this, this needs the binding context of the surrounding page to bind correctly ???
    }

    private static void ElementsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        System.Diagnostics.Debug.WriteLine("ElementList - ElementsPropertyChanged");
		var this_ = (ElementList)bindable;
		this_.ElementsChanged();
	}

    internal void ElementsChanged()
    {
        // do NOT just clear Codepoints and start over: try and keep what is already correct, assuming most is just changing (or rather appending) a previous text

        int cpindex = 0;

        // *copy* the current enumerator, so that the "Current" value is kept (will probably not survive the "GetValue" otherwise)
        elementsEnumerator = Elements.GetEnumerator();

        var prevElementsConverted = elementsConverted;
        elementsConverted = 0;
        allElementsProcessed = false;
        System.Diagnostics.Debug.WriteLine($">> ElementsChanged: Starting new initial {InitialCount}, was {prevElementsConverted}");

        bool wasScrolled = false;

        // get at least InitialCount, up to the current maximum, but with a maximum of MaxCount (as protection)
        var totake = Math.Min(Math.Max(InitialCount, prevElementsConverted), MaxCount);

        // Elements.Take(totake).ToList();
        List<Models.StringElement> firstPart = new();
        for(int i=0; i<totake; i++)
        {
            if (elementsEnumerator.MoveNext())
            {
                firstPart.Add(elementsEnumerator.Current);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($">> ElementsChanged: Got All at index {i}");
                allElementsProcessed = true;
                break;
            }
        }

        foreach (var element in TranslateElements(firstPart))
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
                if (!wasScrolled)
                {
                    TheElements.ScrollTo(cpindex, position: ScrollToPosition.Center);
                    wasScrolled = true;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($">> ElementsChanged: index {cpindex} already has {element.Codepoint.Character}");
            }

            cpindex++;
        }

        // clean up possible trailing part
        if (Codepoints.Count > cpindex)
        {
            while (Codepoints.Count > cpindex)
            {
                System.Diagnostics.Debug.WriteLine($">> ElementsChanged: remove extra at end - total before {Codepoints.Count}");
                Codepoints.RemoveAt(Codepoints.Count - 1);
            }

            if (!wasScrolled)
            {
                TheElements.ScrollTo(cpindex, position: ScrollToPosition.End);
            }
        }

        System.Diagnostics.Debug.WriteLine($">> ElementsChanged: converted {elementsConverted} items into {Codepoints.Count} Codepoints");
    }

    internal void OnTresholdReached(object sender, EventArgs e)
    {
        if (elementsEnumerator is null)
        {
            System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: Elements is null");
            return;
        }

        if (allElementsProcessed)
        {
            System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: already got all");
            return;
        }

        // var nextPart = Elements.Take(AdditionalCount).ToList();
        List<Models.StringElement> nextPart = new();
        for (int i = 0; i < AdditionalCount; i++)
        {
            if (elementsEnumerator.MoveNext())
            {
                nextPart.Add(elementsEnumerator.Current);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: got all at index {i}");
                allElementsProcessed = true;
                break;
            }
        }

        // treshold reached means that there is no existing filling for Codepoints, so always append
        foreach (var elt in TranslateElements(nextPart))
        {
            Codepoints.Add(elt);
        }

        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: added {AdditionalCount} items to Codepoints (total {Codepoints.Count})");
        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: converted {elementsConverted} items into Codepoints ");
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