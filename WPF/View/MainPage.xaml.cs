using MatchaLatteReviews.Application.Constants;
using System;
using System.IO;
using System.Windows;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window // Ensure this matches the base class in all partial declarations
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
    }
}
