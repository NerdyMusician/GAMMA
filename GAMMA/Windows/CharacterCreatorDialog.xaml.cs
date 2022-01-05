using GAMMA.Models;
using GAMMA.Toolbox;
using System.Windows;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class CharacterCreatorDialog : Window
    {
        public CharacterCreatorDialog(CharacterModel character)
        {
            InitializeComponent();
            DataContext = character;
            (DataContext as CharacterModel).Races = Configuration.MainModelRef.PlayerRaces;
            (DataContext as CharacterModel).Alignments = Configuration.Alignments;
            (DataContext as CharacterModel).Backgrounds = Configuration.MainModelRef.PlayerBackgrounds;
            (DataContext as CharacterModel).SetFeatChoices();
            foreach (PlayerClassLinkModel link in (DataContext as CharacterModel).PlayerClasses)
            {
                link.UpdateSubclassList();
            }
            (DataContext as CharacterModel).UpdateLanguageChoiceCounts(); // GC1
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CharacterModel).ValidateCharacterCreation(true) == true)
            {
                (DataContext as CharacterModel).ImplementCharacterCreationStats();
                DialogResult = true;
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
