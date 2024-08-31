namespace Unidecoder.Maui.Controls;

using Unidecoder.Maui.ViewModels;

public partial class ElementList : ContentView
{
	public static readonly BindableProperty ElementsProperty
		= BindableProperty.Create(nameof(Elements), typeof(IList<Models.StringElement>), typeof(ElementList),
			defaultValue: new List<Models.StringElement>(),
			propertyChanged: ElementsPropertyChanged);

    public ElementList()
	{
		InitializeComponent();
        this.VM = MauiProgram.App.Services.GetService<ViewModels.ElementListVm>()
            ?? throw new InvalidOperationException("VM not found for DI: ElementListVm");
        // this.BindingContext = this; <- do not do this, this needs the binding context of the surrounding page to bind correctly
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
		this_.OnPropertyChanged(nameof(this_.VM));
	}
}