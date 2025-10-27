using MatchaLatteReviews.Application.Utilities;
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


using TypeEnum = MatchaLatteReviews.Domain.Enums.Type;
using VersionModel = MatchaLatteReviews.Domain.Model.Version;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class EditorsPickVM
    {
        public string Cover { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Blurb { get; set; }
        public double Rating { get; set; }
    }

    public class ReleaseVM
    {
        public string Cover { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }  
        public int Year { get; set; }
        public string Genre { get; set; }  
        public string Format { get; set; }  
        public double AvgRating { get; set; }
        public string Kind { get; set; }    // NEW: "Album" / "EP" / "Single" / "Song"
    }

    public class ArtistCardVM
    {
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string BioOneLine { get; set; }
        public string TypeBadge { get; set; }
    }

    public class EventVM
    {
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
        public ObservableCollection<EventVM> Events { get; private set; }

        public ICommand OpenDetailsCommand { get; private set; }

        public MainPageViewModel()
        {
            _articles = Injector.CreateInstance<IArticleRepository>();
            EditorsPicks = new ObservableCollection<EditorsPickVM>();
            NewReleases = new ObservableCollection<ReleaseVM>();
            FeaturedArtists = new ObservableCollection<ArtistCardVM>();
            Events = new ObservableCollection<EventVM>();

            OpenDetailsCommand = new RelayCommand(_ => MessageHelper.ShowInfo("Detalji (stub): biće modal/stranica za par primera."));
            LoadFront();
        }

        private void LoadFront()
        {
            var all = _articles.GetAll();
            var allList = all != null ? all.ToList() : new List<Article>();

            // 1) Novi albumi/EP/singlovi 
            var music = allList.OfType<Music>().ToList();
            var releases = music
                .Where(m => m.Type == TypeEnum.Album || m.Type == TypeEnum.EP || m.Type == TypeEnum.Single)
                .Select(m => new
                {
                    Music = m,
                    LastRelease = (m.Versions != null ? m.Versions.OrderByDescending(v => v.ReleaseDate).FirstOrDefault() : null)
                })
                .OrderByDescending(x => x.LastRelease != null ? x.LastRelease.ReleaseDate : DateFallback(x.Music))
                .Take(8)
                .ToList();

            foreach (var r in releases)
                NewReleases.Add(MapRelease(r.Music, r.LastRelease));

            // 2) Urednicki izbor 
            var pinnedImages = new[] { "alb-mbf", "alb-showgirl", "sng-ophelia" };

            foreach (var a in allList.Where(a => !string.IsNullOrWhiteSpace(a.Image) && pinnedImages.Contains(a.Image)))
            {
                if (a is Music m)
                {
                    var last = (m.Versions != null ? m.Versions.OrderByDescending(v => v.ReleaseDate).FirstOrDefault() : null);
                    EditorsPicks.Add(MapPick(m, m.Type.ToString(), last));
                }
                else
                {
                    EditorsPicks.Add(new EditorsPickVM
                    {
                        Title = a.Title,
                        Type = "Article",
                        Genre = "",
                        Blurb = Truncate(a.Content, 120),
                        Rating = a.Rating,
                        Cover = CoverByImage(a.Image)
                    });
                }
            }

            // 3) Izdvojeni izvodjaci
            var artistList = allList.OfType<Artist>().Take(8).ToList();
            foreach (var art in artistList)
            {
                FeaturedArtists.Add(new ArtistCardVM
                {
                    Name = art.Title,
                    Genre = "",
                    Photo = CoverByImage(art.Image),
                    BioOneLine = Truncate(art.Content, 110),
                    TypeBadge = "" 
                });
            }

            // 4) Izvodjenja/Dogadjaji 
            var perf = music
                .Where(m => m.Type == TypeEnum.Performance)
                .OrderByDescending(m => (m.Versions != null && m.Versions.Any()) ? m.Versions.Max(v => v.ReleaseDate) : m.Date)
                .Take(4)
                .ToList();

            foreach (var p in perf)
            {
                var last = (p.Versions != null ? p.Versions.OrderByDescending(v => v.ReleaseDate).FirstOrDefault() : null);
                Events.Add(new EventVM
                {
                    Title = p.Title,
                    Date = last != null ? last.ReleaseDate : p.Date,
                    Location = "—",
                    ParticipantsShort = "",
                    HasRecording = true,
                    CanStream = false
                });
            }
        }

        private ReleaseVM MapRelease(Music m, VersionModel last)
        {
            return new ReleaseVM
            {
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

        private EditorsPickVM MapPick(Music m, string type, VersionModel last)
        {
            return new EditorsPickVM
            {
                Title = m.Title,
                Type = type,
                Genre = "",
                Blurb = Truncate(m.Content, 120),
                Rating = m.Rating,
                Cover = CoverByImage(m.Image)
            };
        }

        private static DateTime DateFallback(Article a)
        {
            return a != null ? a.Date : DateTime.MinValue;
        }

        private static string CoverByImage(string imageKey)
        {
            if (string.IsNullOrWhiteSpace(imageKey))
                return "pack://application:,,,/WPF/img/logo.png";
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
