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
    /// Interaction logic for ManageArtistsPage.xaml
    /// </summary>
    public partial class ManageArtistsPage : Window
    {
        private readonly ManageArtistsViewModel _vm;
        public ManageArtistsPage()
        {
            InitializeComponent();
            _vm = new ManageArtistsViewModel(
                openAdd: OpenAddDialog,
                openEdit: OpenEditDialog);
            DataContext = _vm;
        }

        private void OpenAddDialog()
        {
            string editorId = "Vuk";
            var win = new AddArtistForm(editorId);
            win.Owner = this;
            win.ShowDialog();
            _vm.Refresh();
        }

        private void OpenEditDialog(MatchaLatteReviews.Domain.Model.Artist artist)
        {
            var win = new EditArtistForm(artist.Id);
            win.Owner = this;
            win.ShowDialog();
            _vm.Refresh();
        }
    }
}
