using System.Collections.Generic;
using System.Windows;

namespace GAMMA.Windows
{
    public partial class CurrencyDialog : Window
    {
        public CurrencyDialog(string title, List<string> transferOptions = null)
        {
            InitializeComponent();
            WindowTitle.Text = title;
            if (transferOptions == null)
            {
                TransferTo.Visibility = Visibility.Collapsed;
            }
            else
            {
                TransferTo.ItemsSource = transferOptions;
            }
        }

        public int GP
        {
            get
            {
                bool success = int.TryParse(GoldPieces.Text, out int result);
                return (success) ? result : 0;
            }
        }
        public int SP
        {
            get
            {
                bool success = int.TryParse(SilverPieces.Text, out int result);
                return (success) ? result : 0;
            }
        }
        public int CP
        {
            get
            {
                bool success = int.TryParse(CopperPieces.Text, out int result);
                return (success) ? result : 0;
            }
        }
        public string TransferTarget
        {
            get
            {
                return TransferTo.Text;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
