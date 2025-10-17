using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class AddMusicFormViewModel : INotifyPropertyChanged
    {
        private string editorId;
        private readonly MusicService _musicService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AddMusicFormViewModel(Action close, string edId)
        {
            editorId = edId;
            _musicService = Injector.CreateInstance<MusicService>();

            AddMusicCommand = new RelayCommand(_ => AddMusic());
            BackCommand = new RelayCommand(_ => Back());

            _close = close;
        }

        private Action _close;
        public ICommand AddMusicCommand { get; }
        public ICommand BackCommand { get; }

        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        //public DateTime ReleaseDate { get; set; }
        private DateTime _releaseDate = DateTime.Now; // default na danas
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                _releaseDate = value;
                OnPropertyChanged(nameof(ReleaseDate));
            }
        }
        public string TypeString { get; set; }
        public MatchaLatteReviews.Domain.Enums.Type type;

        public string LengthString { get; set; }
        private int Length;

        public List<int> Ratings { get; } = new List<int> { 1, 2, 3, 4, 5 };
        public List<string> Types { get; } = new List<string> { "EP", "Single", "Album", "Performance", "Song" };

        private void AddMusic()
        {
            Title = this.Title;
            Content = this.Content;
            Rating = this.Rating;
            ReleaseDate = this.ReleaseDate;
            if(ReleaseDate == null)
            {
                ReleaseDate = DateTime.Now;
            }

            int parsedLength;
            if (!int.TryParse(LengthString, out parsedLength))
            {
                parsedLength = 0;
            }

            Length = parsedLength;

            MatchaLatteReviews.Domain.Enums.Type parsedType;
            if (!Enum.TryParse(TypeString, out parsedType))
            {
                parsedType = 0;
            }

            type = parsedType;

            //List<MatchaLatteReviews.Domain.Model.Version> versions = new List<MatchaLatteReviews.Domain.Model.Version>();
            //MessageBox.Show("Title: " + Title + "\nContent: " + Content + "\nRating: " + Rating + "\nRelease Date: " + ReleaseDate + "\nType: " + Type + "\nLength: " + Length);
            Music newMusic = new Music(Title, Rating, Content, ReleaseDate, Domain.Enums.Status.Approved, 0, editorId, new List<string>(), new List<string>(), type, Length, new List<MatchaLatteReviews.Domain.Model.Version>());

            try
            {
                _musicService.Add(newMusic);
                MessageHelper.ShowInfo($"{newMusic.Title} added!");
                _close();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($" failed: {exception.Message}");
            }
            _close?.Invoke();
        }

        private void Back()
        {
            _close();
        }
    }
}
