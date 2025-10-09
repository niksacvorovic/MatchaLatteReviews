using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domen.Enumeracije;
using MatchaLatteReviews.Stores;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    internal class LoginPageViewModel : INotifyPropertyChanged
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
                if (user.Lozinka != Password)
                {
                    MessageHelper.ShowError("Incorrect password.");
                    return;
                }
                _userStore.SetCurrentUser(user);
                RedirectBasedOnRole(user.Uloga);
                _closeWindow();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($"Login failed: {exception.Message}");
            }
        }
        private void RedirectBasedOnRole(Uloga role)
        {
            switch (role)
            {
                case Uloga.RegistrovaniKorisnik:
                    OpenRegisteredUserPanel();
                    break;
                case Uloga.Urednik:
                    OpenEditorPanel();
                    break;
                case Uloga.Administrator:
                    OpenAdministratorPanel();
                    break;
                default:
                    MessageHelper.ShowError("Unknown user role.");
                    break;
            }
        }
        private void OpenAdministratorPanel()
        {
            throw new NotImplementedException();
        }
        private void OpenEditorPanel()
        {
            throw new NotImplementedException();
        }
        private void OpenRegisteredUserPanel()
        {
            throw new NotImplementedException();
        }
    }
}