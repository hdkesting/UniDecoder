using System.Runtime.CompilerServices;

namespace Unidecoder.Maui.ViewModels;

public abstract class ViewModelBase : BindableObject
{
    /// <summary>
    /// Sets the specified field. Fires "PropertyChanged" event when the value was changed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        if (comparer.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
