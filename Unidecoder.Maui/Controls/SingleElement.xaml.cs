namespace Unidecoder.Maui.Controls;

using System;

using Unidecoder.Maui.Models;

public partial class SingleElement : ContentView
{
    public static readonly BindableProperty ElementProperty
        = BindableProperty.Create(nameof(Element), typeof(CodepointInfo), typeof(SingleElement), propertyChanged: ElementChanged);

    private static void ElementChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // NOP
    }

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