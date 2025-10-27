using MatchaLatteReviews.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginPageViewModel(this.Close);
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel vm) 
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        private void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            RegisterPage register = new RegisterPage();
            register.Show();
            this.Close();
        }

        private void OnMainPageClicked(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void LoginPage_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is LoginPageViewModel vm) vm.LoginCommand.Execute(null);
        }
    }
}