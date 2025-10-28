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
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.WPF.ViewModel;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for EditArtistForm.xaml
    /// </summary>
    public partial class EditArtistForm : Window
    {
        public EditArtistForm(string artistId)
        {
            InitializeComponent();
            DataContext = new EditArtistFormViewModel(this.Close, artistId);
        }

        
    }
}
