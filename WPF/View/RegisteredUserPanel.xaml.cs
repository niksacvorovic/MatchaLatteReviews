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
    public partial class RegisteredUserPanel : Window
    {
        public RegisteredUserPanel()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
        private void ChildScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is DependencyObject d)
            {
                var parent = FindParentScrollViewer(d);
                if (parent != null)
                {
                    e.Handled = true;
                    var ev = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                    {
                        RoutedEvent = UIElement.MouseWheelEvent,
                        Source = sender
                    };
                    parent.RaiseEvent(ev);
                }
            }
        }

        private static ScrollViewer FindParentScrollViewer(DependencyObject child)
        {
            DependencyObject current = child;
            while (current != null)
            {
                current = VisualTreeHelper.GetParent(current);
                if (current is ScrollViewer sv) return sv;
            }
            return null;
        }
    }
}
