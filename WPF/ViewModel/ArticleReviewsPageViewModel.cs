using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class ReviewItemVM
    {
        public string ReviewId { get; set; }
        public int Rating { get; set; }             // 1..5
        public string RatingOutOf5 { get; set; }    // "4/5"
        public string StarsFilled { get; set; }     // "★"
        public string StarsEmpty { get; set; }     // "☆"
        public string Comment { get; set; }
        public Visibility CommentVisibility { get; set; }
        public string AuthorName { get; set; }
        public string Initials { get; set; }
        public string PublicBadge { get; set; }
        public string ApprovedBadge { get; set; }
        public Visibility PublicBadgeVisibility { get; set; }
        public Visibility ApprovedBadgeVisibility { get; set; }
    }

    public class ArticleReviewsPageViewModel : INotifyPropertyChanged
    {
        private readonly ReviewService _reviewService = Injector.CreateInstance<ReviewService>();
        private readonly RegisteredUserService _registeredUserService = Injector.CreateInstance<RegisteredUserService>();
        private readonly string _articleId;
        private readonly string _authorId;

        public ObservableCollection<ReviewItemVM> Reviews { get; } =
            new ObservableCollection<ReviewItemVM>();

        // Header
        private int _reviewsCount;
        public int ReviewsCount { get { return _reviewsCount; } private set { _reviewsCount = value; OnChanged(nameof(ReviewsCount)); } }

        private string _avgDisp;
        public string AverageRatingDisplay { get { return _avgDisp; } private set { _avgDisp = value; OnChanged(nameof(AverageRatingDisplay)); } }

        private string _avgStarsFilled;
        public string AverageStarsFilled { get { return _avgStarsFilled; } private set { _avgStarsFilled = value; OnChanged(nameof(AverageStarsFilled)); } }

        private string _avgStarsEmpty;
        public string AverageStarsEmpty { get { return _avgStarsEmpty; } private set { _avgStarsEmpty = value; OnChanged(nameof(AverageStarsEmpty)); } }

        // Add form
        private Visibility _addFormVisibility = Visibility.Collapsed;
        public Visibility AddFormVisibility { get { return _addFormVisibility; } private set { _addFormVisibility = value; OnChanged(nameof(AddFormVisibility)); } }
        public Visibility WriteButtonVisibility =>
        string.IsNullOrWhiteSpace(_authorId) ? Visibility.Collapsed : Visibility.Visible;


        public int NewRating { get { return _newRating; } set { _newRating = value; OnChanged(nameof(NewRating)); OnChanged(nameof(NewRatingOutOf5)); } }
        private int _newRating = 4;

        public string NewRatingOutOf5 { get { return _newRating + "/5"; } }
        public string NewComment { get { return _newComment; } set { _newComment = value; OnChanged(nameof(NewComment)); } }
        private string _newComment;

        public bool NewIsPublic { get { return _newIsPublic; } set { _newIsPublic = value; OnChanged(nameof(NewIsPublic)); } }
        private bool _newIsPublic = true;

        public bool NewIsApproved { get { return _newIsApproved; } set { _newIsApproved = value; OnChanged(nameof(NewIsApproved)); } }
        private bool _newIsApproved = true;

        public string NewError { get { return _newError; } private set { _newError = value; OnChanged(nameof(NewError)); } }
        private string _newError;

        public ArticleReviewsPageViewModel(string articleId, string authorId)
        {
            _articleId = articleId;
            _authorId = authorId;
            if (string.IsNullOrWhiteSpace(_authorId))
                AddFormVisibility = Visibility.Collapsed;
            Load();
        }

        public void ShowAddForm()
        {
            NewRating = 3; NewComment = null; NewIsPublic = true; NewIsApproved = true; NewError = null;
            AddFormVisibility = Visibility.Visible;
        }

        public void HideAddForm()
        {
            AddFormVisibility = Visibility.Collapsed;
            NewError = null;
        }

        public bool TrySaveNewReview()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_authorId))
                {
                    NewError = "Author not set.";
                    return false;
                }

                int rating = NewRating; // 1..5
                if (rating < 1 || rating > 5)
                {
                    NewError = "Rating must be between 1 and 5.";
                    return false;
                }

                var review = new Review(
                    rating: rating,
                    comment: string.IsNullOrWhiteSpace(NewComment) ? null : NewComment.Trim(),
                    isPublic: NewIsPublic,
                    isApproved: NewIsApproved,
                    authorId: _authorId,
                    articleId: _articleId
                );

                _reviewService.Add(review);

                // dodaj u kolekciju (bez full refresh)
                var added = MapReview(review);
                Reviews.Insert(0, added); // na vrh

                RecomputeHeader();
                return true;
            }
            catch (Exception ex)
            {
                NewError = ex.Message;
                return false;
            }
        }

        public void Reload() => Load();

        private void Load()
        {
            var list = _reviewService.GetArticleReviews(_articleId)
                                     .Where(r => r.IsPublic || r.AuthorId == _authorId)
                                     .ToList();

            Reviews.Clear();
            foreach (var r in list.OrderByDescending(x => x.ReviewId))
                Reviews.Add(MapReview(r));

            RecomputeHeader();
        }

        private void RecomputeHeader()
        {
            ReviewsCount = Reviews.Count;
            //if (ReviewsCount == 0)
            //{
            //    AverageRatingDisplay = "—";
            //    AverageStarsFilled = "";
            //    AverageStarsEmpty = new string('☆', 5);
            //    return;
            //}

            // double avg = Reviews.Average(x => x.Rating);
            double avg = _reviewService.GetArticleAverageRating(_articleId);
            if (avg == 0)
            {
                AverageRatingDisplay = "—";
                AverageStarsFilled = "";
                AverageStarsEmpty = new string('☆', 5);
                return;
            }
            AverageRatingDisplay = avg.ToString("0.0");
            int filled = Math.Max(0, Math.Min(5, (int)Math.Floor(avg)));
            AverageStarsFilled = new string('★', filled);
            AverageStarsEmpty = new string('☆', 5 - filled);
        }

        private ReviewItemVM MapReview(Review r)
        {
            
            int rf = r.Rating;

            var authorName = _registeredUserService.GetById(r.AuthorId).Username ?? r.AuthorId ?? "Anonymous";
            var initials = MakeInitials(authorName);

            return new ReviewItemVM
            {
                ReviewId = r.ReviewId,
                Rating = rf,
                RatingOutOf5 = rf.ToString() + "/5",
                StarsFilled = new string('★', rf),
                StarsEmpty = new string('☆', 5 - rf),
                Comment = string.IsNullOrWhiteSpace(r.Comment) ? null : r.Comment.Trim(),
                CommentVisibility = string.IsNullOrWhiteSpace(r.Comment) ? Visibility.Collapsed : Visibility.Visible,
                AuthorName = authorName,
                Initials = initials,
                PublicBadge = r.IsPublic ? "Public" : "",
                ApprovedBadge = r.IsApproved ? "Approved" : "",
                PublicBadgeVisibility = r.IsPublic ? Visibility.Visible : Visibility.Collapsed,
                ApprovedBadgeVisibility = r.IsApproved ? Visibility.Visible : Visibility.Collapsed
            };
        }

        private static string MakeInitials(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "?";
            var parts = s.Trim().Split(new[] { ' ', '.', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1) return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpperInvariant();
            return (parts[0][0].ToString() + parts[1][0].ToString()).ToUpperInvariant();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnChanged(string name) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(name)); }
    }
}
