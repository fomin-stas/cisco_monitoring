using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SetupConfiguration
{
    public static class PasswordBoxExtension
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled", typeof (bool?), typeof (PasswordBoxExtension),
            new PropertyMetadata(null, IsEnabledPropertyChangedCallback));

        private static void IsEnabledPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox) dependencyObject;

            var oldValue = e.OldValue as bool?;
            if (oldValue != null)
            {
                passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;
            }

            var newValue = e.NewValue as bool?;
            if (newValue != null)
            {
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
            }
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            var passwordBox = (PasswordBox) sender;
            SetPassword(passwordBox, passwordBox.Password);
        }

        public static void SetIsEnabled(DependencyObject element, bool? value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        public static bool? GetIsEnabled(DependencyObject element)
        {
            return (bool?) element.GetValue(IsEnabledProperty);
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password",
            typeof (string), typeof (PasswordBoxExtension),
            new PropertyMetadata(string.Empty, OnDependencyPropertyChanged));

        public static string GetPassword(PasswordBox paswordBox)
        {
            return (string) paswordBox.GetValue(PasswordProperty);
        }

        public static void SetPassword(PasswordBox paswordBox, string value)
        {
            paswordBox.SetValue(PasswordProperty, value);
        }

        private static void OnDependencyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox) source;

            if (e.Property == PasswordProperty)
            {
                var newValue = (string) e.NewValue;

                if(newValue != passwordBox.Password)
                    passwordBox.Password = newValue;
            }
        }

    }
}
