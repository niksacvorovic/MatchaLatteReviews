using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Enums;
using MatchaLatteReviews.Stores;
using MatchaLatteReviews.WPF.View;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Action _closeWindow;
        private UserStore _userStore;
        private UserService _userService;

        public ICommand LoginCommand { get; }

        public LoginPageViewModel(Action closeWindow)
        {
            _closeWindow = closeWindow;
            LoginCommand = new RelayCommand(_ => Login());
            _userStore = Injector.CreateInstance<UserStore>();
            _userService = Injector.CreateInstance<UserService>();
        }
        public string Username { get; set; }
        public string Password { get; set; }

        private void Login()
        {
            try
            {
                var user = _userService.GetByUsername(Username);
                if (user == null)
                {
                    MessageHelper.ShowError("User with this username does not exist.");
                    return;
                }
                if (user.Password != Password)
                {
                    MessageHelper.ShowError("Incorrect password.");
                    return;
                }
                _userStore.SetCurrentUser(user);
                RedirectBasedOnRole(user.Role);
                _closeWindow();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($"Login failed: {exception.Message}");
            }
        }
        private void RedirectBasedOnRole(Role role)
        {
            switch (role)
            {
                case Role.RegisteredUser:
                    OpenRegisteredUserPanel();
                    break;
                case Role.Editor:
                    OpenEditorPanel();
                    break;
                case Role.Administrator:
                    OpenAdministratorPanel();
                    break;
                default:
                    MessageHelper.ShowError("Unknown user role.");
                    break;
            }
        }
        private void OpenAdministratorPanel()
        {
            var adminPanel = new AdministratorPanel();
            adminPanel.Show();
            _closeWindow();
        }
        private void OpenEditorPanel()
        {
            //MessageBox.Show(_userStore.GetCurrentUser().FirstName + " " + _userStore.GetCurrentUser().LastName + " logged in successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            var editorPanel = new EditorPanel(_userStore);
            editorPanel.Show();
            //_closeWindow();
        }
        private void OpenRegisteredUserPanel()
        {
            var userPanel = new RegisteredUserPanel();
            userPanel.Show();
            _closeWindow();
        }
    }
}