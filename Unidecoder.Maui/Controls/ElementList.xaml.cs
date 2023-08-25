namespace Unidecoder.Maui.Controls;

using System.Collections.ObjectModel;

using Unidecoder.Maui.Models;

public partial class ElementList : ContentView
{
	public static readonly BindableProperty ElementsProperty
		= BindableProperty.Create(nameof(Elements), typeof(IList<Models.StringElement>), typeof(ElementList),
			defaultValue: new List<Models.StringElement>(),
			propertyChanged: ElementsPropertyChanged);

    private const int initialCount = 25;
    private const int additionalCount = 7;
    private int elementsConverted;

    /// <summary>
    /// Gets or sets the incoming list of elements to show.
    /// </summary>
    public IList<Models.StringElement> Elements
	{
		get => (IList<Models.StringElement>)GetValue(ElementsProperty);
		set => SetValue(ElementsProperty, value);
	}

    private string _elementCount = "!?";

    public ObservableCollection<Models.CodepointAndPosition> Codepoints { get; } = new();

    public string ElementCount { get => _elementCount; set => _elementCount = value; }

    //public ICommand TresholdReachedCommand { get; init; }

    public ElementList()
	{
		InitializeComponent();
        //this.VM = MauiProgram.App.Services.GetService<ViewModels.ElementListVm>()
        //    ?? throw new InvalidOperationException("VM not found for DI: ElementListVm");
        // this.BindingContext = this;//  {.VM}; //<- do not do this, this needs the binding context of the surrounding page to bind correctly ???
        //TresholdReachedCommand = new Command(OnTresholdReached);
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
        Codepoints.Clear();

        var initiallist = TranslateElements(Elements.Take(initialCount));
        foreach (var element in initiallist)
        {
            Codepoints.Add(element);
        }

        elementsConverted = initialCount;
        System.Diagnostics.Debug.WriteLine($">> ElementsChanged: converted {elementsConverted} items into {Codepoints.Count} Codepoints out of {Elements.Count}");

        ElementCount = $"{Elements.Count} -> {Codepoints.Count}";
    }

    internal void OnTresholdReached(object sender, EventArgs e)
    {
        if (Elements is null || elementsConverted >= Elements.Count)
        {
            System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: got all {Elements?.Count ?? -1}");
            return;
        }

        foreach (var elt in TranslateElements(Elements.Skip(elementsConverted).Take(additionalCount)))
        {
            Codepoints.Add(elt);
        }

        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: added {additionalCount} items to Codepoints (total {Codepoints.Count})");
        elementsConverted += additionalCount;
        System.Diagnostics.Debug.WriteLine($">> OnTresholdReached: converted {elementsConverted} items into Codepoints out of {Elements.Count}");
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