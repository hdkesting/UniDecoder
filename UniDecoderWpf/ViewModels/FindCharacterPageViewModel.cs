using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
        }

        protected override List<BasicInfo> CreateList()
        {
            return this.unicodeSvc.FindCharactersByName(Value);
        }

        public void StringValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Value = ((TextBox)sender).Text; // calls CreateList
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(FindCharacterPageViewModel)]?.ToString();
            }

            if (String.IsNullOrEmpty(Value))
            {
                Value = "pizza";
            }

            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            //if (suspending)
            {
                suspensionState[nameof(FindCharacterPageViewModel)] = Value;
            }

            return Task.CompletedTask;
        }
    }
}
