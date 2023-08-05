namespace Unidecoder.Maui.Controls;

using Unidecoder.Maui.Models;

public partial class SingleElement : ContentView
{
    public static readonly BindableProperty ElementProperty
        = BindableProperty.Create(nameof(Element), typeof(CodepointInfo), typeof(SingleElement));

    public SingleElement()
	{
		InitializeComponent();
	}

    public CodepointInfo Element
    {
        get => (CodepointInfo)GetValue(ElementProperty);
        set => SetValue(ElementProperty, value);
    }
}