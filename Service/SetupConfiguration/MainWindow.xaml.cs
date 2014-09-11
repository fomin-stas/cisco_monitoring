using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SetupConfiguration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginTextBox.Focus();
        }

        private void WizzardTabitem_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newTab = FindFirstParent<TabItem>(sender as FrameworkElement);
            if (!Equals(newTab, WizzardTabControl.SelectedItem))
            {
                e.Handled = true;
            }
        }

        private static T FindFirstParent<T>(FrameworkElement control) where T : FrameworkElement
        {
            if (control == null)
                return null;

            if (control is T)
                return (T)control;

            return FindFirstParent<T>(control.Parent as FrameworkElement);
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            switch (WizzardTabControl.SelectedIndex)
            {
                case 0:
                {
                    var command = NextButton.Command;
                    if (command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                    break;
                }
                case 1:
                {
                    var command = NextButton.Command;
                    if (command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                    break;
                }
                case 2:
                {
                    var command = CompleteButton.Command;
                    if (command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                    break;
                }
            }
        }

    }
}
