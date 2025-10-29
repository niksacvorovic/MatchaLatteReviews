using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using MatchaLatteReviews.Repositories;
using MatchaLatteReviews.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.WPF.ViewModel;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for EditorPanel.xaml
    /// </summary>
    public partial class EditorPanel : Window
    {       
        private readonly UserStore _userStore;

        public EditorPanel()
        {
            _userStore = Injector.CreateInstance<UserStore>();
            
            InitializeComponent();
            DataContext = new EditorPanelViewModel(_userStore.CurrentUser);
            RefreshPage();
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
        
        private void RefreshPage()
        {
            if (DataContext is EditorPanelViewModel vm) vm.Load(); 
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
            //AddArtistForm addArtist = new AddArtistForm(_userStore.GetCurrentUser().UserId);
            //addArtist.Show();
            
            var win = new AddArtistForm(_userStore.GetCurrentUser().UserId);
            win.Owner = this;
            win.ShowDialog();
            
        }

        private void ViewArticle_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditorPanelViewModel vm)
                if (vm.SelectedAuthoredArticle != null)
                {
                    ArticlePage articlePage = new ArticlePage(vm.SelectedAuthoredArticle);
                    articlePage.ShowDialog();
                }
                else
                {
                    MessageHelper.ShowInfo("Select an authored article to display it!");
                }
        }

        private void WriteArticle_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditorPanelViewModel vm)
                if (vm.SelectedTask != null)
                {
                    if (vm.SelectedTask is Artist artist)
                    {
                        ApproveArtistForm form = new ApproveArtistForm(artist);
                        form.ShowDialog();
                    }

                    if (vm.SelectedTask is Music music)
                    {
                        ApproveMusicForm form = new ApproveMusicForm(music);
                        form.ShowDialog();
                    }
                    RefreshPage();
                }
                else
                {
                    MessageHelper.ShowInfo("Select an article from the task list!");
                }
        }
    }
}
