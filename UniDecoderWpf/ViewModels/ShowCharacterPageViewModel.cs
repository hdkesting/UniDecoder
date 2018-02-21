using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Documents;

namespace UniDecoderWpf.ViewModels
{
    public class ShowCharacterPageViewModel : CharacterPageBaseViewModel
    {
        private IEnumerable<TextRange> fragments;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShowCharacterPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        public IEnumerable<TextRange> Fragments
        {
            get
            {
                var text = Value ?? string.Empty;
                var ranges = this.unicodeSvc.SplitInRanges(text).ToList();
                //Set(ref this.fragments, ranges);
                this.fragments = ranges;
                return this.fragments;
            }
            set
            {
                Set(ref this.fragments, value);
            }
        }

        protected override List<BasicInfo> CreateList()
        {
            return this.unicodeSvc.ShowCharactersInString(Value);
        }

        public void StringValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox)sender).Text;
            Value = text; // calls CreateList

            var ranges = this.unicodeSvc.SplitInRanges(text).ToList();

            Fragments = ranges;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(ShowCharacterPageViewModel)]?.ToString();
            }

            if (string.IsNullOrEmpty(Value))
            {
                Value = "1× 🍕 à €1,‒";
            }

            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            //if (suspending)
            {
                suspensionState[nameof(ShowCharacterPageViewModel)] = Value;
            }

            return Task.CompletedTask;
        }
    }
}
