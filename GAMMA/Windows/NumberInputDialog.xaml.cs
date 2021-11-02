using System.Windows;

namespace GAMMA.Windows
{
    public partial class NumberInputDialog : Window
    {
        public NumberInputDialog(string title = "Number Input", string subtitle = "")
        {
            InitializeComponent();
            WindowTitle.Text = title;
            WindowSubtitle.Text = subtitle;
        }

        public int Number
        {
            get
            {
                bool success = int.TryParse(NumberInput.Text, out int result);
                return (success) ? result : 0;
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
