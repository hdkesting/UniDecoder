﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Template10.Mvvm;
using UniDecoderWpf.Models;
using Windows.UI.Xaml;

namespace UniDecoderWpf.ViewModels
{
    public abstract class CharacterPageBaseViewModel : ViewModelBase
    {
        protected readonly Services.UnicodeServices.UnicodeService unicodeSvc = new Services.UnicodeServices.UnicodeService();

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
            for (int i = 0; i < Math.Max(List.Count, newlist.Count); i++)
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
                    i--; // redo this index
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

        public void GotoSettings(object sender, RoutedEventArgs e) =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy(object sender, RoutedEventArgs e) =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout(object sender, RoutedEventArgs e) =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}
