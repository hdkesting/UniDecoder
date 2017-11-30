using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UniDecoderWpf.ViewModels
{
    public class ShowBlockPageViewModel : CharacterPageBaseViewModel
    {
        public ShowBlockPageViewModel()
        {
            BlockNames = this.svc.GetUnicodeBlockNames();
            //Value = "Basic Latin";
        }

        public List<string> BlockNames { get; }

        protected override List<BasicInfo> CreateList()
        {
            return this.svc.GetCharactersByBlock(Value);
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Value = ((ComboBox)sender).SelectedValue.ToString();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }

            if (string.IsNullOrEmpty(Value))
            {
                Value = "Basic Latin";
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            //if (suspending)
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
