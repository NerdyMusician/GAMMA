using System.Windows;

namespace GAMMA.Windows
{
    public partial class YesNoDialog : Window
    {
        public YesNoDialog(string question)
        {
            InitializeComponent();
            CustomMessage.Text = question;
        }

        public bool Answer = false;

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Answer = true;
            this.DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
