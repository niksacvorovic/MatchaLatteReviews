using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.WPF.View;
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
    public class AddGenreFormViewModel : INotifyPropertyChanged
    {
        private readonly GenreService _genreService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AddGenreFormViewModel(Action close)
        {
            _genreService = Injector.CreateInstance<GenreService>();
            AddGenreCommand = new RelayCommand(_ => AddGenre());
            AllGenres = new ObservableCollection<Genre>(_genreService.GetAll());
            _close = close;
        }

        private Action _close;
        public ICommand AddGenreCommand { get; }
        public ObservableCollection<Genre> AllGenres { get; set; }

        public string Name { get; set; }

        private void AddGenre()
        {
            Genre newGenre = new Genre(Name);
            try
            {
                _genreService.Add(newGenre);
                MessageHelper.ShowInfo($"{newGenre.Name} genre added!");
                _close();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($" failed: {exception.Message}");
            }
        }
    }
}
