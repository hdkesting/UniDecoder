using Unidecoder.Maui.Models;

namespace Unidecoder.Maui.Controls;

public partial class ElementList : ContentView
{
	public static readonly BindableProperty ElementsProperty
		= BindableProperty.Create(nameof(Elements), typeof(IList<Models.StringElement>), typeof(ElementList), defaultValue: new List<Models.StringElement>());

	public ElementList()
	{
		InitializeComponent();
		this.Codepoints = new List<Models.CodepointAndPosition>();
		this.BindingContext = this;
	}

	// TODO recalculate the list to single codepoints, with a Single/First/Middle/Last enum value

	public IList<Models.StringElement> Elements
	{
		get => (IList<Models.StringElement>)GetValue(ElementsProperty);
		set
		{
			SetValue(ElementsProperty, value);
			this.Codepoints = TranslateElements(value);
		}
	}

	public IList<Models.CodepointAndPosition> Codepoints { get; set; }

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
				list.Add(new CodepointAndPosition(element.Codepoints[0], Support.CodepointPosition.First));
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