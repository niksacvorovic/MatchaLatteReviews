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
using MatchaLatteReviews.WPF.ViewModel;

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for AddArtistForm.xaml
    /// </summary>
    public partial class AddArtistForm : Window
    {
        public AddArtistForm(string editorId)
        {
            InitializeComponent();
            DataContext = new AddArtistFormViewModel(this.Close, editorId);
        }
    }
}
