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
    /// Interaction logic for ManageMusicPage.xaml
    /// </summary>
    public partial class ManageMusicPage : Window
    {
        private readonly ManageMusicPageViewModel _vm;

        public ManageMusicPage()
        {
            InitializeComponent();
            _vm = new ManageMusicPageViewModel(
                openAdd: OpenAddDialog,
                openEdit: OpenEditDialog);
            DataContext = _vm;
        }

        private void OpenAddDialog()
        {
            string editorId = "Vuk";
            var win = new AddMusicForm(editorId); // pretpostavka da imaš ovaj form (analogno AddArtistForm)
            win.Owner = this;
            win.ShowDialog();
            _vm.Refresh();
        }

        private void OpenEditDialog(MatchaLatteReviews.Domain.Model.Music music)
        {
            var win = new EditMusicForm(music.Id);
            win.Owner = this;
            win.ShowDialog();
            _vm.Refresh();
        }
        
    }
}
