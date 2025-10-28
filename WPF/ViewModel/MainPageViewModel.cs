using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MatchaLatteReviews.Application.Services;


using TypeEnum = MatchaLatteReviews.Domain.Enums.Type;
using VersionModel = MatchaLatteReviews.Domain.Model.Version;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class EditorsPickVM
    {
        public string ArticleId { get; set; }
        public string Cover { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Blurb { get; set; }
        public double Rating { get; set; }
    }

    public class ReleaseVM
    {
        public string ArticleId { get; set; }
        public string Cover { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Format { get; set; }
        public double AvgRating { get; set; }
        public string Kind { get; set; }    //  "Album" / "EP" / "Single" / "Song"
    }

    public class ArtistCardVM
    {
        public string ArticleId { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string BioOneLine { get; set; }
        public string TypeBadge { get; set; }
    }

    public class EventVM
    {
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string ParticipantsShort { get; set; }
        public bool HasRecording { get; set; }
        public bool CanStream { get; set; }
    }

    public class MainPageViewModel
    {
        private readonly IArticleRepository _articles;

        public ObservableCollection<EditorsPickVM> EditorsPicks { get; private set; }
        public ObservableCollection<ReleaseVM> NewReleases { get; private set; }
        public ObservableCollection<ArtistCardVM> FeaturedArtists { get; private set; }
        public ObservableCollection<ReleaseVM> Performances { get; private set; }


        public ICommand OpenDetailsCommand { get; private set; }

        public MainPageViewModel()
        {
            _articles = Injector.CreateInstance<IArticleRepository>();
            EditorsPicks = new ObservableCollection<EditorsPickVM>();
            NewReleases = new ObservableCollection<ReleaseVM>();
            FeaturedArtists = new ObservableCollection<ArtistCardVM>();
            Performances = new ObservableCollection<ReleaseVM>();

            OpenDetailsCommand = new RelayCommand(idObj =>
            {
                var id = idObj as string;
                if (string.IsNullOrWhiteSpace(id)) return;

                var article = _articles.GetAll().FirstOrDefault(a => a.Id == id);
                if (article == null) return;

                var win = new MatchaLatteReviews.WPF.View.ArticlePage(article);
                win.ShowDialog();
            });

            LoadFront();
        }

        private void LoadFront()
        {
            var all = _articles.GetAll();
            var allList = all != null ? all.ToList() : new List<Article>();

            var music = allList.OfType<Music>().ToList();

            NewReleases.Clear();

            var latest6 = music
                .Where(m => m.Type != TypeEnum.Performance)
                .Select(m =>
                {
                    var last = (m.Versions != null && m.Versions.Any())
                        ? m.Versions.OrderByDescending(v => v.ReleaseDate).First()
                        : null;

                    
                    var sortDate =
                        (last?.ReleaseDate != default ? last.ReleaseDate :
                        (m.ReleaseDate != default ? m.ReleaseDate : m.Date));

                    return new { Music = m, Last = last, SortDate = sortDate };
                })
                .OrderByDescending(x => x.SortDate)
                .ThenByDescending(x => x.Music.Rating) 
                .Take(6)
                .ToList();

            foreach (var x in latest6)
            {
                NewReleases.Add(MapRelease(x.Music, x.Last));
            }


            // Editors' choice
            EditorsPicks.Clear();

            var wanted = new (string title, TypeEnum type)[] {
                    ("eternal sunshine", TypeEnum.Album),
                    ("folklore",          TypeEnum.Album),
                    ("sweetener", TypeEnum.Album),
                    ("evermore",          TypeEnum.Album),
                    ("no tears left to cry",            TypeEnum.Single),
                    ("willow",            TypeEnum.Single),
                    ("cardigan",          TypeEnum.Single),
                };

            foreach (var w in wanted)
            {
                var m = music.FirstOrDefault(x =>
                    string.Equals(x.Title, w.title, StringComparison.OrdinalIgnoreCase) &&
                    x.Type == w.type);

                if (m == null) continue;

                var last = (m.Versions != null
                    ? m.Versions.OrderByDescending(v => v.ReleaseDate).FirstOrDefault()
                    : null);

                EditorsPicks.Add(new EditorsPickVM
                {
                    ArticleId = m.Id,
                    Title = m.Title,
                    Type = m.Type.ToString(),  
                    Genre = "",
                    Blurb = Truncate(m.Content, 120),
                    Rating = m.Rating,
                    Cover = CoverByImage(m.Image) 
                });
            }



            // Top artists
            var artistService = new ArtistService();
            var topArtists = artistService.GetTopRated(6);

            FeaturedArtists.Clear();
            foreach (var art in topArtists)
            {
                FeaturedArtists.Add(new ArtistCardVM
                {
                    ArticleId = art.Id,
                    Name = art.Title,
                    Genre = "",
                    Photo = CoverByImage(art.Image),
                    BioOneLine = Truncate(art.Content, 110),
                    TypeBadge = ""
                });
            }


            // Performances 
            var perfAll = music
                .Where(m => m.Type == TypeEnum.Performance)         
                .OrderByDescending(m => m.ReleaseDate)
                .ToList();

            Performances.Clear();
            foreach (var p in perfAll)
            {
                var last = (p.Versions != null
                    ? p.Versions.OrderByDescending(v => v.ReleaseDate).FirstOrDefault()
                    : null);

                Performances.Add(MapRelease(p, last));  
            }
        }

        private ReleaseVM MapRelease(Music m, VersionModel last)
        {
            return new ReleaseVM
            {
                ArticleId = m.Id,
                Title = m.Title,
                Artist = "",
                Year = (last != null ? last.ReleaseDate : m.Date).Year,
                Genre = "",
                Format = (last != null ? last.Format.ToString() : ""),
                AvgRating = m.Rating,
                Cover = CoverByImage(m.Image),
                Kind = m.Type.ToString()
            };
        }

        private static string CoverByImage(string imageKey)
        {
            if (string.IsNullOrWhiteSpace(imageKey))
                return "pack://application:,,,/WPF/img/default_artist.jpg";
            return $"pack://application:,,,/WPF/img/covers/{imageKey}.jpg";
        }

        private static string Truncate(string s, int n)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Length <= n ? s : s.Substring(0, n) + "…";
        }

        private void ChildScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is DependencyObject d)
            {
                var parent = FindParentScrollViewer(d);
                if (parent != null)
                {
                    e.Handled = true;
                    var ev = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                    {
                        RoutedEvent = UIElement.MouseWheelEvent,
                        Source = sender
                    };
                    parent.RaiseEvent(ev);
                }
            }
        }
        private static ScrollViewer FindParentScrollViewer(DependencyObject child)
        {
            DependencyObject current = child;
            while (current != null)
            {
                current = VisualTreeHelper.GetParent(current);
                if (current is ScrollViewer sv) return sv;
            }
            return null;
        }
    }
}
