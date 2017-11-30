using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniDecoderWpf.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UniDecoderWpf.Views
{
    public sealed partial class CharacterGrid : UserControl
    {
        public CharacterGrid()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty CharactersProperty =
           DependencyProperty.Register(nameof(Characters), typeof(ObservableCollection<BasicInfo>),
              typeof(CharacterGrid), new PropertyMetadata(null));


        public ObservableCollection<BasicInfo> Characters
        {
            get { return (ObservableCollection<BasicInfo>)GetValue(CharactersProperty); }
            set { SetValue(CharactersProperty, value); }
        }

        public string Utf8String(BasicInfo character)
        {
            var enc = System.Text.Encoding.UTF8;
            var ba = enc.GetBytes(character.Character);

            return string.Join(" ", ba.Select(b => b.ToString("X2")));
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // TODO write character to clipboard
            var bi = (BasicInfo)e.ClickedItem;
        }
    }
}
