using GAMMA.Toolbox;
using System.Windows;

namespace GAMMA.Windows
{
    public partial class ValuePrompt : Window
    {
        public ValuePrompt(string promptMessage)
        {
            InitializeComponent();
            CustomMessage.Text = promptMessage;
        }

        public int Value = 0;
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueField.Text, out int Value) == false)
            {
                HelperMethods.NotifyUser("Must be an integer value.");
                return;
            }
            this.DialogResult = true;
        }

    }
}
