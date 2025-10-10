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
    /// Interaction logic for AdministratorPanel.xaml
    /// </summary>
    public partial class AdministratorPanel : Window
    {
        public AdministratorPanel()
        {
            InitializeComponent();
        }

        private void RegisterEditor_Click(object sender, RoutedEventArgs e)
        {
            EditorRegisterPage editorRegisterPage = new EditorRegisterPage();
            editorRegisterPage.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
    }
}
