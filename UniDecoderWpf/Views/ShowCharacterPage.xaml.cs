using System;
using UniDecoderWpf.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace UniDecoderWpf.Views
{
    public sealed partial class ShowCharacterPage : Page
    {
        public ShowCharacterPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void SomeClickMethod(object sender, RoutedEventArgs e)
        {

        }

        //private void StringValue_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        //{
        //    var svc = new Services.UnicodeServices.UnicodeService();

        //    var list = svc.ShowCharactersInString(sender.Text);

        //    //this.CharGrid.Characters = list;
        //    this.CharGrid.DataContext = new ViewModels.CharacterGridViewModel() { CharacterList = list };

        //    ((ShowCharacterPageViewModel)this.DataContext).List = list;
        //}
    }
}