using MatchaLatteReviews.WPF.ViewModel;
using System;
using System.Windows;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for TaskListUpdateForm.xaml
    /// </summary>
    public partial class TaskListUpdateForm : Window
    {
        public TaskListUpdateForm()
        {
            InitializeComponent();
            DataContext = new TaskListUpdateFormViewModel(this.Close);
        }
    }
}
