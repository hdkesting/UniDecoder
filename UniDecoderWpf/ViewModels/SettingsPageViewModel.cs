﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.SettingsService;
using Windows.UI.Xaml;

namespace UniDecoderWpf.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
    }

    public class SettingsPartViewModel : ViewModelBase
    {
        readonly Services.SettingsServices.SettingsService _settings;

        public SettingsPartViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                this._settings = Services.SettingsServices.SettingsService.Instance;
            }
        }

        public bool ShowHamburgerButton
        {
            get { return this._settings.ShowHamburgerButton; }
            set { this._settings.ShowHamburgerButton = value; base.RaisePropertyChanged(); }
        }

        public bool IsFullScreen
        {
            get { return this._settings.IsFullScreen; }
            set
            {
                this._settings.IsFullScreen = value;
                base.RaisePropertyChanged();
                if (value)
                {
                    ShowHamburgerButton = false;
                }
                else
                {
                    ShowHamburgerButton = true;
                }
            }
        }

        public bool UseShellBackButton
        {
            get { return this._settings.UseShellBackButton; }
            set { this._settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return this._settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { this._settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
        }

        public Version UnicodeVersion => System.Unicode.UnicodeInfo.UnicodeVersion;
    }

    public class AboutPartViewModel : ViewModelBase
    {
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        //public Uri RateMe => new Uri("http://aka.ms/template10");
    }
}
