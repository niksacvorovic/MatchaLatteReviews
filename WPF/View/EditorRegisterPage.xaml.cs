using MatchaLatteReviews.WPF.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for EditorRegisterPage.xaml
    /// </summary>
    public partial class EditorRegisterPage : Window
    {
        public EditorRegisterPage()
        {
            InitializeComponent();
            DataContext = new EditorRegisterViewModel(this.Close);
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditorRegisterViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
