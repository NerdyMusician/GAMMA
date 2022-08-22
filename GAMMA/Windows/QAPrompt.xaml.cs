using GAMMA.CustomControls;
using GAMMA.Toolbox;
using System.Collections.Generic;
using System.Windows;

namespace GAMMA.Windows
{
    public partial class QAPrompt : Window
    {
        public QAPrompt(string question, List<ConvertibleValue> answers)
        {
            InitializeComponent();
            Question.Text = question;
            AnswerList.ItemsSource = answers;
        }

        public string Answer = string.Empty;

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            Answer = ((sender as ImageButton).DataContext as ConvertibleValue).Value;
            this.DialogResult = true;
        }
    }
}
