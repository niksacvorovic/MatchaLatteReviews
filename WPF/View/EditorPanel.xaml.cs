using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Stores;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for EditorPanel.xaml
    /// </summary>
    public partial class EditorPanel : Window
    {       
        private UserStore _userStore;
        public EditorPanel(UserStore e)
        {
            _userStore = e;
            InitializeComponent(); // mora biti prvo
            DataContext = e.GetCurrentUser();
        }

        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var user = DataContext as Editor; // ili Editor ako koristiš Editor
            if (user != null)
            {
                MessageBox.Show($"Ime: {user.FirstName}\nPrezime: {user.LastName}\nUsername: {user.Username}");
            }
            else
            {
                MessageBox.Show("DataContext je null ili nije tipa User.");
            }
        }

        public void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        public void AddMusicButton_Click(object sender, RoutedEventArgs e)
        {
            AddMusicForm addArticle = new AddMusicForm(_userStore.GetCurrentUser().UserId);
            addArticle.Show();
        }

        public void AddArtistButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ova funkcionalnost još nije implementirana.");
        }

        public void AddPerformanceButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ova funkcionalnost još nije implementirana.");
        }

    }
}
