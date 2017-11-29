using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using UniDecoderWpf.Models;

namespace UniDecoderWpf.ViewModels
{
    public abstract class CharacterPageBaseViewModel : ViewModelBase
    {
        protected readonly Services.UnicodeServices.UnicodeService svc = new Services.UnicodeServices.UnicodeService();

        private string value;
        private ObservableCollection<BasicInfo> list = new ObservableCollection<BasicInfo>();

        public string Value
        {
            get { return this.value; }
            set
            {
                Set(ref this.value, value);
                UpdateList();
            }
        }

        public ObservableCollection<BasicInfo> List
        {
            get { return this.list; }
            set { Set(ref this.list, value); }
        }

        protected void UpdateList()
        {
            var newlist = CreateList();
            MergeList(newlist);
        }

        protected abstract List<BasicInfo> CreateList();

        protected void MergeList(List<BasicInfo> newlist)
        {
            var max = Math.Max(List.Count, newlist.Count);
            for (int i = 0; i < max; i++)
            {
                var oldc = i < List.Count ? List[i] : null;
                var newc = i < newlist.Count ? newlist[i] : null;
                if (oldc == null)
                {
                    // moved past end of List, so add it
                    List.Add(newc);
                }
                else if (newc == null)
                {
                    // moved past end of newlist, so remove from List
                    List.RemoveAt(i);
                }
                else // neither are null
                {
                    if (oldc.Codepoint != newc.Codepoint)
                    {
                        // different, so replace
                        List[i] = newc;
                    }
                }
            }

        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}
