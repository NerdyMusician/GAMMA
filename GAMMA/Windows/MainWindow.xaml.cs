using GAMMA.CustomControls;
using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.ViewModels;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace GAMMA.Windows
{
    public partial class MainWindow : Window
    {
        // Constructors
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                AutoClosePopups = true;

                Directory.CreateDirectory(Environment.CurrentDirectory + "/Audio/Music");
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Audio/Sfx");
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Images/Creatures");
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Images/Players");
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Images/Npcs");
                Directory.CreateDirectory(Environment.CurrentDirectory + "/NoteAttachments");
                this.DataContext = new MainViewModel();
                this.SizeChanged += Window_SizeChanged;
                AutosaveTimer = new Timer(300000); // 300000 = 5 minutes
                AutosaveTimer.Elapsed += AutosaveTimer_Elapsed;
                AutosaveTimer.Enabled = true;
                MusicPlayer.Play();
                SfxPlayer.Play();
                SystemAudioPlayer.Play();
                SetTooltipText();
            }
            catch (Exception e)
            {
                new NotificationDialog("Startup Failure\n" + e.Message).ShowDialog();
                HelperMethods.WriteToLogFile("Startup Failure: " + e.Message);
                this.Close();
            }
            
        }

        // Private Properties
        private readonly Timer AutosaveTimer;
        private readonly bool AutoClosePopups;

        // Private Methods
        #region Audio
        private void MusicPlayButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Play();
        }
        private void MusicPauseButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Pause();
        }
        private void MusicStopButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Stop();
        }
        private void SfxPlayButton_Click(object sender, RoutedEventArgs e)
        {
            SfxPlayer.Play();
        }
        private void SfxPauseButton_Click(object sender, RoutedEventArgs e)
        {
            SfxPlayer.Pause();
        }
        private void SfxStopButton_Click(object sender, RoutedEventArgs e)
        {
            SfxPlayer.Stop();
        }
        private void SystemPlayButton_Click(object sender, RoutedEventArgs e)
        {
            SystemAudioPlayer.Play();
        }
        private void SystemPauseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemAudioPlayer.Pause();
        }
        private void SystemStopButton_Click(object sender, RoutedEventArgs e)
        {
            SystemAudioPlayer.Stop();
        }
        private void MusicPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Position = TimeSpan.FromMilliseconds(1);
        }
        #endregion
        private void AutosaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Configuration.MainModelRef.SettingsView.EnableCharacterAutosave)
            {
                AutosaveCharacters();
            }
            if (Configuration.MainModelRef.SettingsView.EnableCampaignsAutosave)
            {
                AutosaveCampaigns();
            }
                
        }
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (AutoClosePopups == false) { return; }
            ClosePopups(sender as Window);            
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (AutoClosePopups == false) { return; }
            ClosePopups(sender as Window);            
        }

        private void ClosePopups(Window window)
        {
            foreach (MiniToggleButton toggleButton in FindVisualChildren<MiniToggleButton>(window))
            {
                if (toggleButton.CloseOnWindowFocusLoss == false) { continue; }
                toggleButton.IsChecked = false;
            }
            foreach (ImageToggleButton toggleButton in FindVisualChildren<ImageToggleButton>(window))
            {
                if (toggleButton.CloseOnWindowFocusLoss == false) { continue; }
                toggleButton.IsChecked = false;
            }
            foreach (GammaComboBox comboBox in FindVisualChildren<GammaComboBox>(window))
            {
                comboBox.IsDropDownOpen = false;
            }
            foreach (Popup popup in FindVisualChildren<Popup>(window))
            {
                if (popup.IsOpen == true)
                {
                    popup.IsOpen = false;
                    popup.IsOpen = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (ComboBox comboBox in FindVisualChildren<ComboBox>(sender as Window))
            {
                comboBox.GotFocus += ComboBox_GotFocus;
                comboBox.GotKeyboardFocus += ComboBox_GotFocus;
            }
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T t)
                    {
                        yield return t;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).IsDropDownOpen = true;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                CombatantsInitiativeList.Height = this.ActualHeight - 360;
                CombatantsNameList.Height = this.ActualHeight - 360;
                CombatantsNpcList.Height = this.ActualHeight - 360;
                CombatantsPlayerList.Height = this.ActualHeight - 360;
                TitleBar.Width = this.ActualWidth - 24;
            }
            catch { }
        }
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
        private void CopyTextblockText(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText((sender as MenuItem).CommandParameter.ToString());
        }
        private void SetTooltipText()
        {
            Tooltip_Blinded.Text = "- A blinded creature can’t see and automatically fails any ability check that requires sight.\n- Attack rolls against the creature have advantage, and the creature’s Attack rolls have disadvantage.";
            Tooltip_Charmed.Text = "- A charmed creature can’t Attack the charmer or target the charmer with harmful Abilities or magical Effects.\n- The charmer has advantage on any ability check to interact socially with the creature.";
            Tooltip_Deafened.Text = "- A deafened creature can’t hear and automatically fails any ability check that requires hearing.";
            Tooltip_Frightened.Text = "- A frightened creature has disadvantage on Ability Checks and Attack rolls while the source of its fear is within line of sight.\n- The creature can’t willingly move closer to the source of its fear.";
            Tooltip_Grappled.Text = "- A grappled creature’s speed becomes 0, and it can’t benefit from any bonus to its speed.\n- The condition ends if the Grappler is incapacitated.\n- The condition also ends if an Effect removes the grappled creature from the reach of the Grappler or Grappling Effect, such as when a creature is hurled away by the Thunderwave spell.";
            Tooltip_Incapacitated.Text = "- An incapacitated creature can’t take Actions or Reactions.";
            Tooltip_Invisible.Text = "- An invisible creature is impossible to see without the aid of magic or a Special sense. For the purpose of Hiding, the creature is heavily obscured. The creature’s location can be detected by any noise it makes or any tracks it leaves.\n- Attack rolls against the creature have disadvantage, and the creature’s Attack rolls have advantage.";
            Tooltip_Paralyzed.Text = "- A paralyzed creature is incapacitated (see the condition) and can’t move or speak.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- Attack rolls against the creature have advantage.\n- Any Attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
            Tooltip_Petrified.Text = "- A petrified creature is transformed, along with any nonmagical object it is wearing or carrying, into a solid inanimate substance (usually stone). Its weight increases by a factor of ten, and it ceases aging.\n- The creature is incapacitated(see the condition), can’t move or speak, and is unaware of its surroundings.\n- Attack rolls against the creature have advantage.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- The creature has Resistance to all damage.\n- The creature is immune to poison and disease, although a poison or disease already in its system is suspended, not neutralized.";
            Tooltip_Poisoned.Text = "- A poisoned creature has disadvantage on Attack rolls and Ability Checks.";
            Tooltip_Prone.Text = "- A prone creature’s only Movement option is to crawl, unless it stands up and thereby ends the condition.\n- The creature has disadvantage on Attack rolls.\n- An Attack roll against the creature has advantage if the attacker is within 5 feet of the creature. Otherwise, the Attack roll has disadvantage.";
            Tooltip_Restrained.Text = "- A restrained creature’s speed becomes 0, and it can’t benefit from any bonus to its speed.\n- Attack rolls against the creature have advantage, and the creature’s Attack rolls have disadvantage.\n- The creature has disadvantage on Dexterity Saving Throws.";
            Tooltip_Stunned.Text = "- A stunned creature is incapacitated (see the condition), can’t move, and can speak only falteringly.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- Attack rolls against the creature have advantage.";
            Tooltip_Unconscious.Text = "- An unconscious creature is incapacitated (see the condition), can’t move or speak, and is unaware of its surroundings.\n- The creature drops whatever it’s holding and falls prone.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- Attack rolls against the creature have advantage.\n- Any Attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
            Tooltip_Exhaustion.Text = "Some Special Abilities and environmental Hazards, such as starvation and the long-­term Effects of freezing or scorching temperatures, can lead to a Special condition called exhaustion. Exhaustion is measured in six levels. An Effect can give a creature one or more levels of exhaustion, as specified in the effect’s description." +
                "\n" +
                "\n1. Disadvantage on Ability Checks" +
                "\n2. Speed halved" + 
                "\n3. Disadvantage on Attack rolls and Saving Throws" +
                "\n4. Hit point maximum halved" +
                "\n5. Speed reduced to zero" +
                "\n6. Death" +
                "\n" +
                "\n- If an already exhausted creature suffers another Effect that causes exhaustion, its current level of exhaustion increases by the amount specified in the effect’s description." +
                "\n- A creature suffers the Effect of its current level of exhaustion as well as all lower levels. For example, a creature suffering level 2 exhaustion has its speed halved and has disadvantage on Ability Checks." +
                "\n- An Effect that removes exhaustion reduces its level as specified in the effect’s description, with all exhaustion Effects ending if a creature’s exhaustion level is reduced below 1." +
                "\n- Finishing a Long Rest reduces a creature’s exhaustion level by 1, provided that the creature has also ingested some food and drink.";
            Tooltip_Intoxication.Text = "A creature that consumes an alcoholic beverage runs the risk of becoming intoxicated, which can affect their abilities and eventually lead to death." +
                "\n" +
                "\n1. Disadvantage on Dexterity ability and skill checks" + 
                "\n2. Disadvantage on Charisma ability and skill checks" +
                "\n3. Disadvantage on Attack Rolls and Saving Throws" + 
                "\n4. Unable to get up from prone or crawl position" +
                "\n5. Falls to zero hit points and must make Death Saving Throws" + 
                "\n6. Death by alcohol poisoning" +
                "\n" + 
                "\nA creature that has intoxication levels can reduce that level by 1 for every two hours without consuming an alcoholic beverage.";
            Tooltip_Intoxication2.Text = Tooltip_Intoxication.Text;
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object selectedItem = e.NewValue ?? e.OldValue;
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveNote = selectedItem as NoteModel;
        }
        private void CampaignNotesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object selectedItem = e.NewValue ?? e.OldValue;
            if (Configuration.MainModelRef.CampaignView.ActiveCampaign == null) { return; }
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNote = selectedItem as NoteModel;
        }
        private void AutosaveCampaigns()
        {
            Configuration.MainModelRef.CampaignView.LastSave = DateTime.Now.ToString();
            if (Configuration.MainModelRef.CampaignView.Campaigns.Count() == 0)
            {
                // Prevents zero character save crash
                XDocument blankDoc = new();
                blankDoc.Add(new XElement("GameCampaignSet"));
                blankDoc.Save(Configuration.CampaignDataFilePath);
                return;
            }
            XDocument itemDocument = new();
            itemDocument.Add(XmlMethods.ListToXml(Configuration.MainModelRef.CampaignView.Campaigns.ToList()));
            itemDocument.Save(Configuration.CampaignDataFilePath);
            return;
        }
        private void AutosaveCharacters()
        {
            if (Configuration.MainModelRef.CharacterBuilderView.Characters.Count() == 0)
            {
                XDocument blankDoc = new();
                blankDoc.Add(new XElement("CharacterModelSet"));
                blankDoc.Save("Data/Characters.xml");
                return;
            }

            XDocument playerDocument = new();
            playerDocument.Add(XmlMethods.ListToXml(Configuration.MainModelRef.CharacterBuilderView.Characters.ToList()));
            playerDocument.Save(Directory.GetCurrentDirectory() + "/Data/Characters.xml");
            HelperMethods.WriteToLogFile("Characters Autosaved");

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if ((DataContext as MainViewModel) == null) { return; } // Startup crash
                (DataContext as MainViewModel).SettingsView.SaveSettings();
                ChromeDriver driver = ((DataContext as MainViewModel).WebDriver) as ChromeDriver;
                if (driver != null)
                {
                    if (driver.SessionId != null) { driver.Quit(); }
                }
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                HelperMethods.WriteToLogFile(ex.Message, true);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            bool saveCharacters = false;
            if (Configuration.MainModelRef.SettingsView.EnableCharacterExitsave)
            {
                if (Configuration.MainModelRef.SettingsView.PromptCharacterExitsave && Configuration.HasUsedCharacterBuilder)
                {
                    YesNoDialog question = new("Save characters before closing?");
                    if (question.ShowDialog() == true)
                    {
                        saveCharacters = question.Answer;
                    }
                }
                if (saveCharacters)
                {
                    AutosaveCharacters();
                }
            }
            this.Close();
        }
        private void ToggleMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TBX_R20Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return; }
            (DataContext as MainViewModel).OpenWebDriver.Execute(null);
        }
    }
}
