﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UniDecoderWpf.ViewModels
{
    public class ShowBlockPageViewModel : CharacterPageBaseViewModel
    {
        public ShowBlockPageViewModel()
        {
            BlockNames = this.unicodeSvc.GetUnicodeBlockNames();
            //Value = "Basic Latin";
        }

        /// <summary>
        /// Gets the block names.
        /// </summary>
        /// <value>
        /// The block names.
        /// </value>
        public List<string> BlockNames { get; }

        /// <summary>
        /// Creates the list.
        /// </summary>
        /// <returns></returns>
        protected override List<BasicInfo> CreateList()
        {
            return this.unicodeSvc.GetCharactersByBlock(Value);
        }

        //public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Value = ((ComboBox)sender).SelectedValue.ToString();
        //}

        public void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                IEnumerable<string> names = BlockNames.Where(x => true);
                foreach (var word in sender.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    names = names.Where(n => n.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                names = names.ToList();
                sender.ItemsSource = names.ToList();
                if (names.Count() == 1)
                {
                    sender.Text = /*Value =*/ names.Single();
                }
            }
        }


        public void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            sender.Text = /*Value =*/ args.SelectedItem.ToString();
        }


        public void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
                sender.Text = /*Value =*/ args.QueryText;
            }
            else
            {
                // Use args.QueryText to determine what to do.
                IEnumerable<string> names = BlockNames.Where(x => true);
                foreach (var word in sender.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    names = names.Where(n => n.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                names = names.ToList();
                sender.ItemsSource = names.ToList();
                if (names.Count() == 1)
                {
                    sender.Text = /*Value =*/ names.Single();
                }
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(ShowBlockPageViewModel)]?.ToString();
            }

            if (string.IsNullOrEmpty(Value))
            {
                Value = "Basic Latin";
            }

            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            //if (suspending)
            {
                suspensionState[nameof(ShowBlockPageViewModel)] = Value;
            }

            return Task.CompletedTask;
        }
    }
}
