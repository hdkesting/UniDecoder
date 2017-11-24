using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Navigation;

namespace UniDecoderWpf.ViewModels
{
    public class CharacterGridViewModel : ViewModelBase
    {
        private List<BasicInfo> characterList;

        public List<BasicInfo> CharacterList
        {
            get { return this.characterList; }
            set { Set(ref this.characterList, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                var svc = new UniDecoderWpf.Services.UnicodeServices.UnicodeService();

                if (suspensionState[nameof(CharacterList)] is List<int> list)
                {
                    CharacterList = svc.GetCharacters(list);
                }
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(CharacterList)] = CharacterList.Select(bi => bi.Codepoint).ToList();
            }
            await Task.CompletedTask;
        }

    }
}
