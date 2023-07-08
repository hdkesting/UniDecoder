using Unidecoder.Maui.Models;
using Unidecoder.Maui.ViewModels;

namespace Unidecoder.Maui.Controls;

public partial class ElementList : ContentView
{
	public static readonly BindableProperty ElementsProperty
		= BindableProperty.Create(nameof(Elements), typeof(IList<Models.StringElement>), typeof(ElementList),
			defaultValue: new List<Models.StringElement>(),
			propertyChanged: ElementsPropertyChanged);

    public ElementList()
	{
		InitializeComponent();
		this.VM = new ElementListVm();
		// this.BindingContext = this; <- don't do this
	}

	public IList<Models.StringElement> Elements
	{
		get => (IList<Models.StringElement>)GetValue(ElementsProperty);
		set => SetValue(ElementsProperty, value);
	}

    public ElementListVm VM { get; }

    private static void ElementsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
		var this_ = (ElementList)bindable;
		var elements = (IList<Models.StringElement>)newValue;
		this_.VM.ElementsChanged(elements);
	}

}