using System.Windows;

namespace GAMMA.Windows
{
    public partial class NotificationDialog : Window
    {
        public NotificationDialog(string notification, string format = "Notification")
        {
            InitializeComponent();
            CustomMessage.Text = notification;
            OkButton.Focus();

            switch (format)
            {
                case "Report":
                    this.Height = 480;
                    this.Width = 640;
                    CustomMessage.Height = 404;
                    CustomMessage.Width = 596;
                    CustomMessage.VerticalContentAlignment = VerticalAlignment.Top;
                    CustomMessage.HorizontalContentAlignment = HorizontalAlignment.Left;
                    OkButton.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case "Wait":
                    OkButton.Visibility = Visibility.Collapsed;
                    break;
                default: // Notification
                    break;
            }

        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
