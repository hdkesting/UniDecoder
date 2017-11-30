using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Controls;

namespace UniDecoderWpf.ViewModels
{
    public class ShowCharacterPageViewModel : CharacterPageBaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShowCharacterPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            else
            {
                Value = "1× 🍕 à €1,‒";
            }
        }

        protected override List<BasicInfo> CreateList()
        {
            return this.svc.ShowCharactersInString(Value);
        }

        public void StringValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Value = ((TextBox)sender).Text; // calls CreateList
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }


    }
}
