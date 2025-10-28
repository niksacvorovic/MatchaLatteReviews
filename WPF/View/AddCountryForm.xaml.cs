using MatchaLatteReviews.WPF.ViewModel;
using System.Windows;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for AddCountryForm.xaml
    /// </summary>
    public partial class AddCountryForm : Window
    {
        public AddCountryForm()
        {
            InitializeComponent();
            DataContext = new AddCountryFormViewModel(this.Close);
        }
    }
}
