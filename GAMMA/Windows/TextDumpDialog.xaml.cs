using System.Windows;

namespace GAMMA.Windows
{
    public partial class TextDumpDialog : Window
    {
        public TextDumpDialog(string headerMessage)
        {
            InitializeComponent();
            CustomMessage.Text = headerMessage;
        }

        public string DumpTextValue
        {
            get
            {
                return DumpText.Text;
            }
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
