using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class SelectableCountry : INotifyPropertyChanged
    {
        public Country Country { get; }
        private bool _isSelected;
        public bool IsSelected { get => _isSelected; set { _isSelected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected))); } }
        public event PropertyChangedEventHandler PropertyChanged;
        public SelectableCountry(Country c) => Country = c;
    }

    public class SelectableGenre : INotifyPropertyChanged
    {
        public Genre Genre { get; }
        private bool _isSelected;
        public bool IsSelected { get => _isSelected; set { _isSelected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected))); } }
        public event PropertyChangedEventHandler PropertyChanged;
        public SelectableGenre(Genre g) => Genre = g;
    }

    public class AddArtistFormViewModel : INotifyPropertyChanged
    {
        private readonly ArtistService _artistService;
        private readonly CountryService _countryService;
        private readonly GenreService _genreService;
        private readonly Action _close;
        private readonly string _editorId;



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        public AddArtistFormViewModel(Action close, string editorId)
        {
            _artistService = Injector.CreateInstance<ArtistService>();
            _countryService = Injector.CreateInstance<CountryService>();
            _genreService = Injector.CreateInstance<GenreService>();
            _close = close;
            _editorId = editorId;

            Countries = new ObservableCollection<SelectableCountry>(
                _countryService.GetAll().Select(c => new SelectableCountry(c))
            );

            Genres = new ObservableCollection<SelectableGenre>(
                _genreService.GetAll().Select(g => new SelectableGenre(g))
            );

            AddArtistCommand = new RelayCommand(_ => AddArtist());
        }

        public ICommand AddArtistCommand { get; }

        // -------- Article fields --------
        private string _title;
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }

        private string _content;
        public string Content { get => _content; set { _content = value; OnPropertyChanged(nameof(Content)); } }

        private int _selectedRating = 3; // default is 3
        public int SelectedRating
        {
            get => _selectedRating;
            set { _selectedRating = value; OnPropertyChanged(nameof(SelectedRating)); }
        }


        // -------- Artist specific --------
        private string _debutText;
        public string DebutText { get => _debutText; set { _debutText = value; OnPropertyChanged(nameof(DebutText)); } }


        // -------- Selections --------
        public ObservableCollection<SelectableCountry> Countries { get; }
        public ObservableCollection<SelectableGenre> Genres { get; }

        private void AddArtist()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageHelper.ShowError("Title is required.");
                return;
            }


            if (!int.TryParse(DebutText, out var debutYear) || debutYear < 1800 || debutYear > DateTime.Now.Year + 1)
            {
                MessageHelper.ShowError("Debut must be a valid year.");
                return;
            }

            var finalDate = DateTime.Now;

            // Selections
            List<string> selectedCountryIds = Countries.Where(c => c.IsSelected).Select(c => c.Country.Id).ToList();
            List<string> selectedGenreIds = Genres.Where(g => g.IsSelected).Select(g => g.Genre.Id).ToList();

            // ReviewIds empty when creating
            var reviewIds = new List<string>();

            var artist = new Artist(
                title: Title,
                image: "",
                rating: SelectedRating,
                content: Content ?? string.Empty,
                date: finalDate,
                status: Domain.Enums.Status.Approved,
                views: 0,
                editorId: _editorId,
                reviewIds: reviewIds,
                genreIds: selectedGenreIds,
                debut: debutYear,
                countryIds: selectedCountryIds
            );

            try
            {
                _artistService.Add(artist);
                MessageHelper.ShowInfo($"Artist '{Title}' added!");
                _close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Adding artist failed: {ex.Message}");
            }
        }
    }
}
