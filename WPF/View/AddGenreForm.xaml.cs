using MatchaLatteReviews.WPF.ViewModel;
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

namespace MatchaLatteReviews.WPF.View
{
    /// <summary>
    /// Interaction logic for AddGenreForm.xaml
    /// </summary>
    public partial class AddGenreForm : Window
    {
        public AddGenreForm()
        {
            InitializeComponent();
            DataContext = new AddGenreFormViewModel(this.Close);
        }
    }
}
