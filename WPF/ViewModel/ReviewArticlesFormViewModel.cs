using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MatchaLatteReviews.Domain.Enums;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class ReviewArticlesFormViewModel
    {
        private readonly ArtistService _artistService;
        private readonly MusicService _musicService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReviewArticlesFormViewModel(Action close)
        {
            _artistService = Injector.CreateInstance<ArtistService>();
            _musicService = Injector.CreateInstance<MusicService>();
            ApproveArticleCommand = new RelayCommand(_ => ApproveArticle());
            DeclineArticleCommand = new RelayCommand(_ => DeclineArticle());
            ArticlesToReview = new ObservableCollection<Article>();
            LoadArticlesForReview();
            _close = close;
        }

        private Action _close;
        public ICommand ApproveArticleCommand { get; }
        public ICommand DeclineArticleCommand { get; }
        public Article SelectedArticle { get; set; }
        public ObservableCollection<Article> ArticlesToReview { get; set; }

        private void LoadArticlesForReview()
        {
            ArticlesToReview.Clear();
            foreach (var artist in _artistService.GetAll().Where(a => a.Status == Status.ForReview))
            {
                ArticlesToReview.Add(artist);
            }

            foreach (var music in _musicService.GetAll().Where(a => a.Status == Status.ForReview))
            {
                ArticlesToReview.Add(music);
            }
        }

        private void UpdateArticleStatus(Article article, Status status)
        {
            if (article != null)
            {
                article.Status = status;
                if (article is Artist)
                {
                    _artistService.Update((Artist) article);
                }
                else if (article is Music) 
                {
                    _musicService.Update((Music) article);
                }
                LoadArticlesForReview();
            }
        }

        public void ApproveArticle()
        {
            if (SelectedArticle != null)
            {
                MessageHelper.ShowInfo("Article Approved!");
                UpdateArticleStatus(SelectedArticle, Status.Approved);
            }
            else
            {
                MessageHelper.ShowInfo("Select an article to approve!");
            }
        }

        public void DeclineArticle()
        {
            if (SelectedArticle != null)
            {
                var confirm = MessageHelper.ShowConfirm("Are you sure you want to decline this article?");
                if (confirm)
                {
                    MessageHelper.ShowInfo("Article Declined!");
                    UpdateArticleStatus(SelectedArticle, Status.Declined);
                }
            }
            else
            {
                MessageHelper.ShowInfo("Select an article to decline!");
            }
        }
    }
}