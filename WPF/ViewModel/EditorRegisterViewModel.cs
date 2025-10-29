using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    internal class EditorRegisterViewModel : INotifyPropertyChanged
    {
        private readonly EditorService _editorService;
        private readonly GenreService _genreService;
        private readonly Action _close;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditorRegisterViewModel(Action close)
        {
            _editorService = Injector.CreateInstance<EditorService>();
            _genreService = Injector.CreateInstance<GenreService>();
            _close = close;

            RegisterCommand = new RelayCommand(_ => RegisterEditor());
            AddGenreCommand = new RelayCommand(_ => AddGenre());

            LoadGenres();
        }
        
        private void LoadGenres()
        {
            var genres = _genreService.GetAll().ToList();
            Genres = new ObservableCollection<GenreSelectionViewModel>(
                genres.Select(g => new GenreSelectionViewModel(g))
            );
            OnPropertyChanged(nameof(Genres));
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ObservableCollection<GenreSelectionViewModel> Genres { get; set; }
        public GenreSelectionViewModel SelectedGenre { get; set; }
        public ObservableCollection<Genre> SelectedGenres { get; set; } = new ObservableCollection<Genre>();
        
        public ICommand RegisterCommand { get; }
        public ICommand AddGenreCommand { get; }

        private void AddGenre()
        {
            if (SelectedGenre == null)
            {
                MessageHelper.ShowInfo("Please select a genre to add.");
                return;
            }

            if (SelectedGenres.Any(g => g.Name == SelectedGenre.Genre.Name))
            {
                MessageHelper.ShowInfo("That genre is already added.");
                return;
            }

            SelectedGenres.Add(SelectedGenre.Genre);
            OnPropertyChanged(nameof(SelectedGenres));
        }

        private void RegisterEditor()
        {
            try
            {
                var selectedGenres = SelectedGenres.ToList();

                if (selectedGenres.Count == 0)
                {
                    MessageHelper.ShowInfo("Please add at least one genre.");
                    return;
                }

                var registeredEditor = new Editor(
                    UserName,
                    Password,
                    FirstName,
                    LastName,
                    true,
                    Domain.Enums.Role.Editor,
                    new System.Collections.Generic.List<string>(),
                    new System.Collections.Generic.List<string>(),
                    selectedGenres.Select(g => g.Id).ToList() 
                );

                _editorService.Register(registeredEditor);
                MessageHelper.ShowInfo("Registration successful!");
                _close();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($"Registration failed: {exception.Message}");
            }
        }
    }
    public class GenreSelectionViewModel : INotifyPropertyChanged
    {
        public Genre Genre { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public GenreSelectionViewModel(Genre genre)
        {
            Genre = genre;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
