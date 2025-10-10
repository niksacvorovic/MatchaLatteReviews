using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.WPF.View;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class RegisterViewModel: INotifyPropertyChanged
    {
        private readonly RegisteredUserService _registeredUserService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RegisterViewModel(Action close)
        {
            _registeredUserService = Injector.CreateInstance<RegisteredUserService>();
            RegisterCommand = new RelayCommand(_ => RegisterUser());
            _close = close;
        }
        public ICommand RegisterCommand { get; }

        private Action _close;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Public { get; set; }


        private void RegisterUser()
        {
            RegisteredUser registeredUser = new RegisteredUser(UserName, Password, FirstName, LastName, Public, Domain.Enums.Role.RegisteredUser, false, new System.Collections.Generic.List<string>(), new System.Collections.Generic.List<string>());
            try
            {
                _registeredUserService.Register(registeredUser);
                MessageHelper.ShowInfo("Registration successful!");
                LoginPage loginPage = new LoginPage();
                loginPage.Show();
                _close();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($"Registration failed: {exception.Message}");
            }
        }
    }
}
