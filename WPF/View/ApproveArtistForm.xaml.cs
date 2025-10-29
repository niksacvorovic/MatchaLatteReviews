using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.WPF.ViewModel;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for ApproveArtistForm.xaml
    /// </summary>
    public partial class ApproveArtistForm : Window
    {
        public ApproveArtistForm(Artist artist)
        {
            InitializeComponent();
            DataContext = new ApproveArtistFormViewModel(this.Close,  artist);
        }
    }
}
