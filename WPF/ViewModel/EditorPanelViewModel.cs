using System.Collections.ObjectModel;
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class EditorPanelViewModel
    {
        private Editor _loggedEditor;
        private readonly EditorService _editorService;

        public EditorPanelViewModel(User user)
        {
            _loggedEditor = (Editor)user;
            _editorService = Injector.CreateInstance<EditorService>();
            AuthoredArticles = new ObservableCollection<Article>();
            TaskList = new ObservableCollection<Article>();
            FirstName = _loggedEditor.FirstName;
            LastName = _loggedEditor.LastName;
            Username = _loggedEditor.Username;
        }

        public ObservableCollection<Article> AuthoredArticles { get; set; }
        public ObservableCollection<Article> TaskList { get; set; }
        public Article SelectedAuthoredArticle { get; set; }
        public Article SelectedTask { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public void Load()
        {
            AuthoredArticles.Clear();
            TaskList.Clear();
            _loggedEditor = _editorService.Get(_loggedEditor.UserId);
            foreach(var article in _loggedEditor.Articles) AuthoredArticles.Add(article);
            foreach(var article in _loggedEditor.TaskList) TaskList.Add(article);
        }
    }
}