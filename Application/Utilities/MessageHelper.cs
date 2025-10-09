using System.Windows;

namespace MatchaLatteReviews.Application.Utilities
{
    public static class MessageHelper
    {
        public static void ShowInfo(string message)
        {
            System.Windows.MessageBox.Show(message, "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public static void ShowError(string message)
        {
            System.Windows.MessageBox.Show(message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static bool ShowConfirm(string message)
        {
            return System.Windows.MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == System.Windows.MessageBoxResult.Yes;
        }
    }
}
