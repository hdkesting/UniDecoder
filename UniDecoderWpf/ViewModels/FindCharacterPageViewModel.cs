using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Controls;

namespace UniDecoderWpf.ViewModels
{
    public class FindCharacterPageViewModel : CharacterPageBaseViewModel
    {

        public FindCharacterPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            else
            {
                Value = "pizza";
            }
        }

        protected override List<BasicInfo> CreateList()
        {
            return this.svc.FindCharacters(Value);
        }

        public void StringValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Value = ((TextBox)sender).Text; // calls CreateList
        }

    }
}
