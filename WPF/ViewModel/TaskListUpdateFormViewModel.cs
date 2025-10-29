using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using MatchaLatteReviews.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class TaskListUpdateFormViewModel : INotifyPropertyChanged
    {
        private readonly EditorService _editorService;
        private readonly GenreService _genreService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TaskListUpdateFormViewModel(Action close)
        {
            _editorService = Injector.CreateInstance<EditorService>();
            _genreService = Injector.CreateInstance<GenreService>();
            AddToTaskListCommand = new RelayCommand(_ => AddToTaskList());
            AddGenreCommand = new RelayCommand(_ => AddGenre());
            AvailableAuthors = new List<string>();
            Genres = new ObservableCollection<Genre>();
            foreach(var genre in _genreService.GetAll()) Genres.Add(genre);
            _close = close;
        }

        private Action _close;
        public ICommand AddGenreCommand { get; }
        public ICommand AddToTaskListCommand { get; }

        public string Title { get; set; }
        public bool IsArtist { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public Genre SelectedGenre { get; set; }
        public ObservableCollection<Genre> SelectedGenres { get; set; } = new ObservableCollection<Genre>();
        public List<String> AvailableAuthors { get; set; }
        public string SelectedAuthorUsername { get; set; }

        private void AddGenre()
        {
            if (SelectedGenre == null)
            {
                MessageHelper.ShowInfo("Please select a genre to add.");
                return;
            }

            if (SelectedGenres.Any(g => g.Id == SelectedGenre.Id))
            {
                MessageHelper.ShowInfo("That genre is already added.");
                return;
            }

            SelectedGenres.Add(SelectedGenre);
            AvailableAuthors.AddRange(_editorService.GetEditorsForGenre(SelectedGenre));
            AvailableAuthors = AvailableAuthors.Distinct().ToList();
            OnPropertyChanged(nameof(SelectedGenres));
            OnPropertyChanged(nameof(AvailableAuthors));
        }


        public void AddToTaskList()
        {
            if (SelectedAuthorUsername is null)
            {
                MessageHelper.ShowInfo("Please select a suitable author");
            } else
            {
                Article newArticle;
                if (IsArtist)
                {
                    newArticle = new Artist(Title, "", 0, "", DateTime.MinValue, Domain.Enums.Status.Assigned, 0, null, new List<string>(), SelectedGenres.Select(g => g.Id).ToList(), 0, new List<string>());
                    _editorService.AddToTaskList(SelectedAuthorUsername, newArticle);
                }
                else
                {
                    newArticle = new Music(Title, "", 0, "", DateTime.MinValue, Domain.Enums.Status.Assigned, 0, null, new List<string>(), SelectedGenres.Select(g => g.Id).ToList(), Domain.Enums.Type.Album, "", 0, new List<Domain.Model.Version>(), DateTime.MinValue);
                    _editorService.AddToTaskList(SelectedAuthorUsername, newArticle);
                }
                MessageHelper.ShowInfo("Article successfully added to task list!");
            }
            _close();
        }
    }
}
