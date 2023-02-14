﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PlacowkaOswiatowa.Helpers.CustomUI.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _isPasswordChanging;
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), 
                typeof(BindablePasswordBox), new FrameworkPropertyMetadata(string.Empty, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false,
                    UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        //Zbindowanie PasswordBoxa z widoku do właściwości ViewModelu
        public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }
        //Odświeżenie widoku przy zmianie zbindowanej wartości w ViewModelu
        private void UpdatePassword()
        {
            if(!_isPasswordChanging)
                passwordBox.Password = Password;
        }
    }
}
