using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class ReviewArticlesFormViewModel
    {
        //private readonly ArticleService _articleService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReviewArticlesFormViewModel(Action close)
        {
            //_articleService = Injector.CreateInstance<ArticleService>();
            ViewArticleCommand = new RelayCommand(_ => ViewArticle());
            ApproveArticleCommand = new RelayCommand(_ => ApproveArticle());
            DeclineArticleCommand = new RelayCommand(_ => DeclineArticle());
            _close = close;
        }

        private Action _close;
        public ICommand ViewArticleCommand { get; }
        public ICommand ApproveArticleCommand { get; }
        public ICommand DeclineArticleCommand { get; }

        public Article SelectedArticle { get; set; }
        public ObservableCollection<Article> ArticlesToReview { get; set; }

        public void ViewArticle()
        {
            MessageBox.Show("Article");
        }

        public void ApproveArticle()
        {
            MessageBox.Show("Article Approved!");
        }

        public void DeclineArticle()
        {
            var confirm = MessageBox.Show("Are you sure you want to decline this article?", "Confirm Declining", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                MessageBox.Show("Article Declined!");
            }
        }
    }
}