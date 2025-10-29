using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MatchaLatteReviews.Application.Constants;
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class MusicListItemVM
    {
        public string Id { get; set; }
        public string Cover { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public string TypeLabel { get; set; }     // Album/EP/Single/Song/Performance...
        public int ReleaseYear { get; set; }
        public string LengthMinutes { get; set; } // "NN min"
        public string VersionsBadge { get; set; } // "n versions"
        public string Blurb { get; set; }

        public Music BackingModel { get; set; }   // za Edit
    }

    public class ManageMusicPageViewModel
    {
        private readonly MusicService _musicService;

        // isti pristup slici kao kod artists
        private readonly Func<string, string> _coverByImage = imageKey =>
            string.IsNullOrWhiteSpace(imageKey)
                ? "pack://application:,,,/WPF/img/logo.png"
                : Path.Combine(Constants.ProjectRoot, $"WPF/img/covers/{imageKey}.jpg");

        private readonly Action _openAdd;
        private readonly Action<Music> _openEdit;

        public ObservableCollection<MusicListItemVM> Music { get; }

        public ICommand AddNewMusicCommand { get; }
        public ICommand EditMusicCommand { get; }
        public ICommand DeleteMusicCommand { get; }
        public ICommand RefreshCommand { get; }

        public ManageMusicPageViewModel(Action openAdd, Action<Music> openEdit)
        {
            _musicService = Injector.CreateInstance<MusicService>();
            _openAdd = openAdd ?? (() => { });
            _openEdit = openEdit ?? (_ => { });

            Music = new ObservableCollection<MusicListItemVM>();

            AddNewMusicCommand = new RelayCommand(_ => _openAdd());
            EditMusicCommand = new RelayCommand(m => Edit((MusicListItemVM)m));
            DeleteMusicCommand = new RelayCommand(m => Delete((MusicListItemVM)m));
            RefreshCommand = new RelayCommand(_ => Refresh());

            Refresh();
        }

        public void Refresh()
        {
            Music.Clear();

            var all = _musicService.GetAll()
                                   .OrderBy(m => m.Title)
                                   .ToList();

            foreach (var m in all)
            {
                Music.Add(new MusicListItemVM
                {
                    Id = m.Id,
                    Title = m.Title,
                    Rating = m.Rating,
                    TypeLabel = m.Type.ToString(),
                    ReleaseYear = (m.ReleaseDate == default(DateTime) ? m.Date : m.ReleaseDate).Year,
                    LengthMinutes = m.Length > 0 ? $"{m.Length} min" : "—",
                    VersionsBadge = m.Versions != null ? $"{m.Versions.Count} version(s)" : "0 version(s)",
                    Blurb = Truncate(m.Content, 100),
                    Cover = _coverByImage(m.Image),
                    BackingModel = m
                });
            }
        }

        private void Edit(MusicListItemVM vm)
        {
            if (vm == null) return;
            _openEdit(vm.BackingModel);
        }

        private void Delete(MusicListItemVM vm)
        {
            if (vm == null) return;

            var res = MessageBox.Show($"Delete music '{vm.Title}'?",
                                      "Confirm delete",
                                      MessageBoxButton.YesNo,
                                      MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes) return;

            try
            {
                _musicService.Delete(vm.Id);
                MessageHelper.ShowInfo($"Music '{vm.Title}' deleted.");
                Refresh();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Delete failed: {ex.Message}");
            }
        }

        private static string Truncate(string s, int n)
            => string.IsNullOrEmpty(s) ? "" : (s.Length <= n ? s : s.Substring(0, n) + "…");
    }
}
