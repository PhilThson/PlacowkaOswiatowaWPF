using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlacowkaOswiatowa.Helpers
{
    public class ThemeWatcher
    {
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string RegistryValueName = "AppsUseLightTheme";
        private static WindowsThemeEnum windowsTheme;

        public WindowsThemeEnum WindowsTheme
        {
            get { return windowsTheme; }
            set { windowsTheme = value; }
        }

        public void StartThemeWatching()
        {
            var currentUser = WindowsIdentity.GetCurrent();
            string query = string.Format(
                CultureInfo.InvariantCulture,
                @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
                currentUser.User.Value,
                RegistryKeyPath.Replace(@"\", @"\\"),
                RegistryValueName);

            try
            {
                windowsTheme = GetWindowsTheme();
                MergeThemeDictionaries(windowsTheme);

                var watcher = new ManagementEventWatcher(query);
                watcher.EventArrived += Watcher_EventArrived;
                SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
                // Start listening for events
                watcher.Start();
            }
            catch (Exception ex)
            {
                // This can fail on Windows 7
                windowsTheme = WindowsThemeEnum.Default;

            }

        }

        private void MergeThemeDictionaries(WindowsThemeEnum windowsTheme)
        {
            string appTheme = "Light";
            switch (windowsTheme)
            {
                case WindowsThemeEnum.Light:
                    appTheme = "Light";
                    break;
                case WindowsThemeEnum.Dark:
                    appTheme = "Dark";
                    break;
                case WindowsThemeEnum.HighContrast:
                    appTheme = "HighContrast";
                    break;
            }

            App.Current.Resources.MergedDictionaries[0].Source = new Uri($"/Themes/{appTheme}.xaml", UriKind.Relative);

        }

        private void SystemParameters_StaticPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            windowsTheme = GetWindowsTheme();

            MergeThemeDictionaries(windowsTheme);

            ThemeChangedEventArgs themeChangedArgument = new ThemeChangedEventArgs();
            themeChangedArgument.WindowsTheme = windowsTheme;

            //App.WindowsThemeChanged?.Invoke(this, themeChangedArgument);

        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            windowsTheme = GetWindowsTheme();

            MergeThemeDictionaries(windowsTheme);

            ThemeChangedEventArgs themeChangedArgument = new ThemeChangedEventArgs();
            themeChangedArgument.WindowsTheme = windowsTheme;

            //App.WindowsThemeChanged?.Invoke(this, themeChangedArgument);
            
        }

        public WindowsThemeEnum GetWindowsTheme()
        {
            WindowsThemeEnum theme = WindowsThemeEnum.Light;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
                {
                    object registryValueObject = key?.GetValue(RegistryValueName);
                    if (registryValueObject == null)
                    {
                        return WindowsThemeEnum.Light;
                    }

                    int registryValue = (int)registryValueObject;

                    if (SystemParameters.HighContrast)
                        theme = WindowsThemeEnum.HighContrast;

                    theme = registryValue > 0 ? WindowsThemeEnum.Light : WindowsThemeEnum.Dark;
                }

                return theme;
            }
            catch (Exception ex)
            {
                return theme;
            }
        }
    }
}
