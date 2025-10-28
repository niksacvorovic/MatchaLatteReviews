using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class ArtistListItemVM
    {
        public string Id { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Debut { get; set; }
        public string Blurb { get; set; }

        public Artist BackingModel { get; set; }  // for Edit
    }

    public class ManageArtistsViewModel
    {
        private readonly ArtistService _artistService;
        private readonly Func<string, string> _coverByImage = imageKey =>
            string.IsNullOrWhiteSpace(imageKey)
                ? "pack://application:,,,/WPF/img/default_artist.jpg"
                : $"pack://application:,,,/WPF/img/covers/{imageKey}.jpg";

        private readonly Action _openAdd;
        private readonly Action<Artist> _openEdit;

        public ObservableCollection<ArtistListItemVM> Artists { get; }

        public ICommand AddNewArtistCommand { get; }
        public ICommand EditArtistCommand { get; }
        public ICommand DeleteArtistCommand { get; }
        public ICommand RefreshCommand { get; }

        public ManageArtistsViewModel(Action openAdd, Action<Artist> openEdit)
        {
            _artistService = Injector.CreateInstance<ArtistService>();
            _openAdd = openAdd ?? (() => { });
            _openEdit = openEdit ?? (_ => { });

            Artists = new ObservableCollection<ArtistListItemVM>();

            AddNewArtistCommand = new RelayCommand(_ => _openAdd());
            EditArtistCommand = new RelayCommand(a => Edit((ArtistListItemVM)a));
            DeleteArtistCommand = new RelayCommand(a => Delete((ArtistListItemVM)a));
            RefreshCommand = new RelayCommand(_ => Refresh());

            Refresh();
        }

        public void Refresh()
        {
            Artists.Clear();

            var all = _artistService.GetAll()
                                    .OrderBy(a => a.Title)
                                    .ToList();

            foreach (var a in all)
            {
                Artists.Add(new ArtistListItemVM
                {
                    Id = a.Id,
                    Name = a.Title,
                    Rating = a.Rating,
                    Debut = a.Debut,
                    Blurb = Truncate(a.Content, 100),
                    Photo = _coverByImage(a.Image),
                    BackingModel = a
                });
            }
        }

        private void Edit(ArtistListItemVM vm)
        {
            if (vm == null) return;
            _openEdit(vm.BackingModel);
        }

        private void Delete(ArtistListItemVM vm)
        {
            if (vm == null) return;

            var res = MessageBox.Show($"Delete artist '{vm.Name}'?",
                                      "Confirm delete",
                                      MessageBoxButton.YesNo,
                                      MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes) return;

            try
            {
                _artistService.Delete(vm.Id);
                MessageHelper.ShowInfo($"Artist '{vm.Name}' deleted.");
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
