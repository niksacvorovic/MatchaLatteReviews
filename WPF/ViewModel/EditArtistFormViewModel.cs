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
    public class EditArtistFormViewModel
    {
        private readonly ArtistService _artistService;
        private readonly CountryService _countryService;
        private readonly GenreService _genreService;
        private readonly Action _close;

        private readonly Artist _model;

        private readonly string _editorId;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        public EditArtistFormViewModel(Action close, string artistId, string editorId)
        {
            _artistService = Injector.CreateInstance<ArtistService>();
            _countryService = Injector.CreateInstance<CountryService>();
            _genreService = Injector.CreateInstance<GenreService>();
            _close = close;
            _model = _artistService.GetById(artistId) ?? throw new ArgumentNullException("Couldn not find chosen artist");
            _editorId = editorId;

            
            Countries = new ObservableCollection<SelectableCountry>(
                _countryService.GetAll().Select(c => new SelectableCountry(c))
            );
            Genres = new ObservableCollection<SelectableGenre>(
                _genreService.GetAll().Select(g => new SelectableGenre(g))
            );

            // prefill fields from model
            Title = _model.Title;
            Content = _model.Content;
            SelectedRating = _model.Rating;
            DebutText = _model.Debut.ToString();

            // mark selections
            foreach (var sc in Countries)
                sc.IsSelected = _model.CountryIds?.Contains(sc.Country.Id) == true;

            foreach (var sg in Genres)
                sg.IsSelected = _model.GenreIds?.Contains(sg.Genre.Id) == true;

            UpdateArtistCommand = new RelayCommand(_ => UpdateArtist());
        }

        public ICommand UpdateArtistCommand { get; }

        private string _title;
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }

        private string _content;
        public string Content { get => _content; set { _content = value; OnPropertyChanged(nameof(Content)); } }

        private int _selectedRating;
        public int SelectedRating { get => _selectedRating; set { _selectedRating = value; OnPropertyChanged(nameof(SelectedRating)); } }

        private string _debutText;
        public string DebutText { get => _debutText; set { _debutText = value; OnPropertyChanged(nameof(DebutText)); } }

        public ObservableCollection<SelectableCountry> Countries { get; }
        public ObservableCollection<SelectableGenre> Genres { get; }

        private void UpdateArtist()
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

            var selectedCountryIds = Countries.Where(c => c.IsSelected).Select(c => c.Country.Id).ToList();
            var selectedGenreIds = Genres.Where(g => g.IsSelected).Select(g => g.Genre.Id).ToList();

            try
            {
                
                _model.Title = Title;
                _model.Content = Content ?? string.Empty;
                _model.Rating = SelectedRating;
                _model.Debut = debutYear;
                _model.CountryIds = selectedCountryIds;
                _model.GenreIds = selectedGenreIds;

                // Validate who is changing
                // if (_model.EditorId != _editorId) { MessageHelper.ShowError("You cannot edit this artist."); return; }

                _artistService.Update(_model);

                MessageHelper.ShowInfo($"Artist '{_model.Title}' updated.");
                _close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Updating artist failed: {ex.Message}");
            }
        }
    }
}
