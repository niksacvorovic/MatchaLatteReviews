using MatchaLatteReviews.Domain.Model;
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
    /// Interaction logic for ApproveMusicForm.xaml
    /// </summary>
    public partial class ApproveMusicForm : Window
    {
        public ApproveMusicForm(Music music)
        {
            InitializeComponent();
            DataContext = new ApproveMusicFormViewModel(this.Close, music);
        }
    }
}
