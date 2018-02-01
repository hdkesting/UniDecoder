using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace UniDecoderWpf.Support
{
    public static class TextBlockExtensions
    {
        // based on https://stackoverflow.com/a/47599775/121309
        // https://docs.microsoft.com/en-us/windows/uwp/xaml-platform/custom-attached-properties

        public static IEnumerable<TextRange> GetBindableTextRanges(DependencyObject obj)
        {
            return (IEnumerable<TextRange>)obj.GetValue(BindableTextRangesProperty);
        }

        public static void SetBindableTextRanges(DependencyObject obj, IEnumerable<TextRange> value)
        {
            obj.SetValue(BindableTextRangesProperty, value);
        }

        public static readonly DependencyProperty BindableTextRangesProperty =
            DependencyProperty.RegisterAttached("BindableTextRanges", typeof(IEnumerable<TextRange>),
                typeof(TextBlockExtensions), new PropertyMetadata(null, OnBindableTextRangesChanged));

        private static void OnBindableTextRangesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBlock Target)
            {
                if (!Target.TextHighlighters.Any())
                {
                    Target.TextHighlighters.Add(new TextHighlighter { Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Yellow) });
                }

                var hl = Target.TextHighlighters.First();
                hl.Ranges.Clear();
                foreach (TextRange range in (System.Collections.IEnumerable)e.NewValue)
                {
                    hl.Ranges.Add(range);
                }
            }
        }
    }
}
