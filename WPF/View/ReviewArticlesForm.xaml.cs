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
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.WPF.ViewModel;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for ReviewArticlesForm.xaml
    /// </summary>
    public partial class ReviewArticlesForm : Window
    {
        public ReviewArticlesForm()
        {
            InitializeComponent();
            DataContext = new ReviewArticlesFormViewModel(this.Close);
        }

        private void ViewArticles_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ReviewArticlesFormViewModel vm) 
                if (vm.SelectedArticle != null)
                {
                    ArticlePage articlePage = new ArticlePage(vm.SelectedArticle);
                    articlePage.ShowDialog();
                }
                else
                {
                    MessageHelper.ShowInfo("Select an article to view!");
                }
        }
    }
}
