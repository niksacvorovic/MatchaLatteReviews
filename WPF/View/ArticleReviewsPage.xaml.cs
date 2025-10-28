using System;
using System.Collections.Generic;
using System.Globalization;
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
using MatchaLatteReviews.WPF.ViewModel;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for ArticleReviewsPage.xaml
    /// </summary>
    public partial class ArticleReviewsPage : Window
    {
        private readonly string _articleId;
        private readonly string _authorId;
        private readonly ArticleReviewsPageViewModel _vm;
        public ArticleReviewsPage(string articleId, string userId)
        {
            InitializeComponent();
            _articleId = articleId;
            _authorId = userId;
            //_articleId = "55b6a3ac-5b1b-4e75-bd24-a4f78d42e707";
            //_authorId = "5475143f-bb3f-4a57-8a7d-d4f2c81e2c9b";
            //_authorId = "d954a489-c60f-42d7-8725-c0af1ad75d52";
            _vm = new ArticleReviewsPageViewModel(_articleId, _authorId);
            DataContext = _vm;

        }

        private void WriteReview_Click(object sender, RoutedEventArgs e) => _vm.ShowAddForm();

        private void CancelAdd_Click(object sender, RoutedEventArgs e) => _vm.HideAddForm();

        private void SaveAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.TrySaveNewReview())
                _vm.HideAddForm();
        }
    }

    
}
