namespace Unidecoder.Maui.Controls;

using Unidecoder.Maui.Models;

public class ElementTemplateSelector : DataTemplateSelector
{
    required public DataTemplate SingleTemplate { get; set; }

    required public DataTemplate FirstTemplate { get; set; }

    required public DataTemplate MiddleTemplate { get; set; }

    required public DataTemplate LastTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is CodepointAndPosition cap)
        {
            return cap.Position switch
            {
                Support.CodepointPosition.Single => SingleTemplate,
                Support.CodepointPosition.Middle => MiddleTemplate,
                Support.CodepointPosition.First => FirstTemplate,
                Support.CodepointPosition.Last => LastTemplate,
                _ => throw new NotSupportedException("Unknown Position value: " + cap.Position),
            };
        }

        throw new NotSupportedException("Unsupported template item type");
    }
}
