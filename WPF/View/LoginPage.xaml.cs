using MatchaLatteReviews.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
            //RegisterStudentView registerStudentView = new RegisterStudentView();
            //registerStudentView.Show();
            this.Close();
        }
    }
}