using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.WPF.ViewModel;
using MatchaLatteReviews.WPF.View;
using System.Windows;
using MatchaLatteReviews.DependencyInjection;   
using MatchaLatteReviews.Stores;

namespace MatchaLatteReviews.WPF.View
{
    public partial class ArticlePage : Window

    {
        private readonly UserStore _userStore;
        public ArticlePage(Article article)
        {
            InitializeComponent();
            _userStore = Injector.CreateInstance<UserStore>();
            DataContext = new ArticleViewModel(article);
        }

        private void OpenReviews_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ArticleViewModel vm)
            {
                var userId = _userStore.CurrentUser?.UserId ?? string.Empty;

                var win = new ArticleReviewsPage(vm.Id, userId);
                win.Owner = this;
                win.ShowDialog();
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}
