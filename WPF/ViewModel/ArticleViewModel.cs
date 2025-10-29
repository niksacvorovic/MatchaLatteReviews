using MatchaLatteReviews.Application.Constants;
using MatchaLatteReviews.Application.Services;          
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Enums;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;   
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TypeEnum = MatchaLatteReviews.Domain.Enums.Type;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class ArticleViewModel
    {
        private readonly IArticleRepository _articles;
        private readonly CountryService _countryService;   
        private readonly GenreService _genreService;       

        // Common
        public string Id { get; }
        public string Title { get; }
        public string Cover { get; }
        public string Content { get; }
        public int Rating { get; }

        // Artist fields
        public bool IsArtist { get; }
        public int Debut { get; }
        public List<string> CountryNames { get; }
        public string CountryNamesJoined => (CountryNames != null && CountryNames.Any())
            ? string.Join(", ", CountryNames)
            : "—";

        // Music fields 
        public bool IsMusic { get; }
        public string MusicType { get; }
        public string MusicName { get; }
        public DateTime? ReleaseDate { get; }
        public int? MusicLength { get; }
        public int VersionsCount { get; }

        public List<string> MusicGenres { get; private set; } = new List<string>(); 
        public string MusicGenresJoined => (MusicGenres != null && MusicGenres.Any())
            ? string.Join(", ", MusicGenres)
            : "—";

        // Artist's releases
        public List<ReleaseRow> Albums { get; }
        public List<ReleaseRow> EPs { get; }
        public List<ReleaseRow> Singles { get; }
        public List<ReleaseRow> Songs { get; }
        public List<ReleaseRow> Performances { get; }

        public ArticleViewModel(Article article)
        {
            _articles = Injector.CreateInstance<IArticleRepository>();
            _countryService = new CountryService();  
            _genreService = new GenreService();    

            Id = article.Id;
            Title = article.Title;
            Rating = article.Rating;
            Content = article.Content ?? string.Empty;
            Cover = CoverByImage(article.Image);

            if (article is Artist ar)
            {
                IsArtist = true;
                Debut = ar.Debut;

                var ids = ar.CountryIds ?? new List<string>();
                var allCountries = _countryService.GetAll().ToList();
                CountryNames = allCountries
                    .Where(c => ids.Contains(c.Id))
                    .Select(c => c.Name)
                    .Distinct()
                    .ToList();

                var ms = _articles.GetAll()
                                  .OfType<Music>()
                                  .Where(m => string.Equals(m.Name, ar.Title, StringComparison.OrdinalIgnoreCase))
                                  .OrderByDescending(m => m.ReleaseDate)
                                  .ToList();

                Albums = ToRows(ms.Where(m => m.Type == TypeEnum.Album));
                EPs = ToRows(ms.Where(m => m.Type == TypeEnum.EP));
                Singles = ToRows(ms.Where(m => m.Type == TypeEnum.Single));
                Songs = ToRows(ms.Where(m => m.Type == TypeEnum.Song));
                Performances = ToRows(ms.Where(m => m.Type == TypeEnum.Performance));
            }
            else if (article is Music m)
            {
                IsMusic = true;
                MusicType = m.Type.ToString();
                MusicName = m.Name;
                ReleaseDate = m.ReleaseDate;
                MusicLength = m.Length;
                VersionsCount = m.Versions?.Count ?? 0;

                var genreIds = m.GenreIds ?? new List<string>();
                var allGenres = _genreService.GetAll().ToList();

                MusicGenres = allGenres
                    .Where(g => genreIds.Contains(g.Id))
                    .Select(g => g.Name)
                    .Distinct()
                    .OrderBy(n => n)
                    .ToList();
            }
        }

        private static List<ReleaseRow> ToRows(IEnumerable<Music> items) =>
            items.Select(m => new ReleaseRow
            {
                Title = m.Title,
                Date = m.ReleaseDate == default ? (DateTime?)null : m.ReleaseDate
            })
            .OrderByDescending(r => r.Date ?? DateTime.MinValue)
            .ToList();

        private static string CoverByImage(string imageKey)
        {
            if (string.IsNullOrWhiteSpace(imageKey))
                return "pack://application:,,,/WPF/img/logo.png";
            return Path.Combine(Constants.ProjectRoot, $"WPF/img/covers/{imageKey}.jpg");
        }
    }

    public class ReleaseRow
    {
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string TitleAndDate => Date.HasValue
            ? $"{Title} — {Date:yyyy-MM-dd}"
            : Title;
    }
}
