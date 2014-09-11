using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows;
using SetupConfiguration.Models;

namespace SetupConfiguration.ViewModels
{
    public class MainViewModel: BaseModel
    {
        public MainViewModel()
        {
            NextCommand = new DelegateCommand(HandleNextCommand);
            PrevCommand = new DelegateCommand(HandlePrevCommand);
            CompleteCommand = new DelegateCommand(HandleCompleteCommand, () => _canExecute);
        }

        #region Commands

        public DelegateCommand NextCommand { get; private set; }
        public DelegateCommand PrevCommand { get; private set; }
        public DelegateCommand CompleteCommand { get; private set; }

        private void HandleCompleteCommand()
        {
            _canExecute = false;
            CompleteCommand.RaiseCanExecuteChanged();

            if (_jdsuPort == null)
                _jdsuPort = string.Empty;

            if (_community == null)
                _community = string.Empty;

            var asyncAction = new Action(() =>
            {

                var repository = new Repository();
                repository.AddAdmin(_login, _firstPassword);
                repository.UpdateServicePort(Int32.Parse(_port));
                repository.UpdateJdsu(_jdsuPort, _community);


                using (var serviceController = new ServiceController("WaterGate Service"))
                {
                    if (serviceController.CanStop)
                    {
                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                    }

                    serviceController.Start();
                }


                Environment.Exit(0);
            });

            asyncAction.BeginInvoke(null, null);
        }

        private void HandleNextCommand()
        {
            switch (_selectedCategory)
            {
                case 0:
                {
                    if (string.IsNullOrEmpty(_login))
                    {
                        MessageBox.Show("Для продолжения заполните поле логина.", "Логин не может быть пустым.", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(_firstPassword) || string.IsNullOrEmpty(_secondPassword))
                    {
                        MessageBox.Show("Для продолжения заполните оба поля пароля.", "Пароль не может быть пустым.", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!_firstPassword.Equals(_secondPassword, StringComparison.Ordinal))
                    {
                        MessageBox.Show("Введенные пароли не совпадают.", "Пароли не совпадают", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    break;
                }

                case 1:
                {
                    try
                    {
                        Int32.Parse(_port);
                    }
                    catch
                    {
                        MessageBox.Show("Порт введен неверно. Порт должен состоять исключительно из цифр.", "Неверный порт", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    break;
                }
            }

            if(SelectedCategory < 2)
                SelectedCategory++;
        }

        private void HandlePrevCommand()
        {
            if (SelectedCategory > 0)
                SelectedCategory--;
        }

        #endregion

        #region Models

        private bool _canExecute = true;

        private int _selectedCategory;
        private string _login;
        private string _firstPassword;
        private string _secondPassword;

        private string _port;

        private string _jdsuPort;
        private string _community;


        public string Login
        {
            get { return _login; }
            set
            {
                _login = value == null ? null : value.Trim();
                OnPropertyChanged("Login");
            }
        }

        public string FirstPassword
        {
            get { return _firstPassword; }
            set
            {
                _firstPassword = value == null ? null : value.Trim();
                OnPropertyChanged("FirstPassword");
            }
        }

        public string SecondPassword
        {
            get { return _secondPassword; }
            set
            {
                _secondPassword = value == null ? null : value.Trim();
                OnPropertyChanged("SecondPassword");
            }
        }

        public string Port
        {
            get { return _port; }
            set
            {
                _port = value == null ? null : value.Trim();
                OnPropertyChanged("Port");
            }
        }

        public string JdsuPort
        {
            get { return _jdsuPort; }
            set
            {
                _jdsuPort = value == null ? null : value.Trim();
                OnPropertyChanged("JdsuPort");
            }
        }

        public string Community
        {
            get { return _community; }
            set
            {
                _community = value;
                OnPropertyChanged("Community");
            }
        }

        public int SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        #endregion
    }
}
