using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniDecoderWpf.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UniDecoderWpf.Views
{
    public sealed partial class CharacterGrid : UserControl
    {
        public CharacterGrid()
        {
            this.InitializeComponent();
        }

        public List<BasicInfo> Characters { get; set; }

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
