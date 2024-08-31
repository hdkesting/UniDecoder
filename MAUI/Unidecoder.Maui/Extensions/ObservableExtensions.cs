namespace Unidecoder.Maui.Extensions;

using System.Collections.ObjectModel;

public static class ObservableExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> target, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            target.Add(item);
        }
    }
}
