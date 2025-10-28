using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.WPF.ViewModel;
using System.Windows;

namespace MatchaLatteReviews.WPF.View
{
    public partial class ArticlePage : Window
    {
        public ArticlePage(Article article)
        {
            InitializeComponent();
            DataContext = new ArticleViewModel(article);
        }

        private void OpenReviews_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ArticleViewModel vm)
            {
                var win = new LoginPage();
                //var win = new ReviewsWindow(vm.Id, vm.Title); // 
                win.Owner = this;
                win.ShowDialog();
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}
