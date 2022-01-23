using GAMMA.Toolbox;
using System.Windows;

namespace GAMMA.Windows
{
    public partial class NoteSearchDialog : Window
    {
        // Constructors
        public NoteSearchDialog()
        {
            InitializeComponent();
            CBX_LookInHeader.IsChecked = true;
            CBX_LookInContent.IsChecked = true;
        }

        // Private Methods
        private void Window_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            if (TBX_SearchText.Text == null || TBX_SearchText.Text == "")
            {
                HelperMethods.NotifyUser("Please enter search text.");
                return;
            }
            this.DialogResult = true;
        }

    }
}
