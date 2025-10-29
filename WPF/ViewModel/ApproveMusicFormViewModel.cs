using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class ApproveMusicFormViewModel
    {
        private readonly MusicService _musicService;
        private readonly CountryService _countryService;
        private readonly GenreService _genreService;
        private readonly EditorService _editorService;
        private readonly Action _close;

        private readonly Music _model;

        // releaseDate
        // type
        // lenght
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        public ApproveMusicFormViewModel(Action close, Music music)
        {
            _musicService = Injector.CreateInstance<MusicService>();
            _countryService = Injector.CreateInstance<CountryService>();
            _genreService = Injector.CreateInstance<GenreService>();
            _editorService = Injector.CreateInstance<EditorService>();
            _close = close;
            _model = music;

            Genres = new ObservableCollection<SelectableGenre>(
                _genreService.GetAll().Select(g => new SelectableGenre(g))
            );

            // prefill fields from model
            Title = _model.Title;
            Content = _model.Content;
            SelectedRating = 3; // default rating

            ReleaseDate = _model.ReleaseDate == default ? DateTime.Now : _model.ReleaseDate;
            LengthString = _model.Length.ToString();
            SelectedType = _model.Type;
            AvailableTypes = Enum.GetValues(typeof(Domain.Enums.Type)).Cast<Domain.Enums.Type>();

            foreach (var sg in Genres)
                sg.IsSelected = _model.GenreIds?.Contains(sg.Genre.Id) == true;

            ApproveMusicCommand = new RelayCommand(_ => ApproveMusic());
        }

        public ICommand ApproveMusicCommand { get; }

        private string _title;
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }

        private DateTime? _releaseDate;
        public DateTime? ReleaseDate
        {
            get => _releaseDate;
            set { _releaseDate = value; OnPropertyChanged(nameof(ReleaseDate)); }
        }

        private string _lengthString;
        public string LengthString
        {
            get => _lengthString;
            set { _lengthString = value; OnPropertyChanged(nameof(LengthString)); }
        }

        private Domain.Enums.Type _selectedType;
        public Domain.Enums.Type SelectedType
        {
            get => _selectedType;
            set { _selectedType = value; OnPropertyChanged(nameof(SelectedType)); }
        }

        public IEnumerable<Domain.Enums.Type> AvailableTypes { get; }

        private string _image;
        public string Image { get => _image; set { _image = value; OnPropertyChanged(nameof(Image)); } }

        private string _content;
        public string Content { get => _content; set { _content = value; OnPropertyChanged(nameof(Content)); } }

        private int _selectedRating;
        public int SelectedRating { get => _selectedRating; set { _selectedRating = value; OnPropertyChanged(nameof(SelectedRating)); } }

        private string _debutText;
        //public string DebutText { get => _debutText; set { _debutText = value; OnPropertyChanged(nameof(DebutText)); } }

        //public ObservableCollection<SelectableCountry> Countries { get; }
        public ObservableCollection<SelectableGenre> Genres { get; }

        private void ApproveMusic()
        {

            //if (!int.TryParse(DebutText, out var debutYear) || debutYear < 1800 || debutYear > DateTime.Now.Year + 1)
            //{
            //    MessageHelper.ShowError("Debut must be a valid year.");
            //    return;
            //}
            //var selectedCountryIds = Countries.Where(c => c.IsSelected).Select(c => c.Country.Id).ToList();
            var selectedGenreIds = Genres.Where(g => g.IsSelected).Select(g => g.Genre.Id).ToList();

            try
            {

                _model.Title = Title;
                _model.Image = Image;
                _model.Content = Content ?? string.Empty;
                _model.Rating = SelectedRating;


                _model.ReleaseDate = ReleaseDate ?? DateTime.Now;

                if (int.TryParse(LengthString, out int length))
                    _model.Length = length;
                else
                    _model.Length = 0; // ili možeš prikazati grešku ako nije validan unos

                _model.Type = SelectedType;

                _model.GenreIds = selectedGenreIds;
                _model.Status = Domain.Enums.Status.ForReview;

                // Validate who is changing
                // if (_model.EditorId != _editorId) { MessageHelper.ShowError("You cannot edit this artist."); return; }

                _musicService.Update(_model);
                _editorService.FinishTask(_model.EditorId, _model);
                MessageHelper.ShowInfo($"Music '{_model.Title}' approved.");
                _close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Approving music failed: {ex.Message}");
            }
        }
    }
}
