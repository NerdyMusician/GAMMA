using GAMMA.Models;
using GAMMA.Toolbox;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class EncounterMultiTargetDialog : Window
    {
        public EncounterMultiTargetDialog(List<CreatureModel> creatures)
        {
            InitializeComponent();

            ItemsControl_Creatures.ItemsSource = creatures.OrderBy(creature => creature.DisplayName);
            ComboBox_SaveAbility.ItemsSource = Configuration.AbilityTypes.ToList();
            CBX_PrimaryDamageType.ItemsSource = Configuration.DamageTypes.ToList();
            CBX_SecondaryDamageType.ItemsSource = Configuration.DamageTypes.ToList();
            ComboBox_EffectType.ItemsSource = new List<string> { "Attack", "Other" };
            ComboBox_Condition.ItemsSource = new List<string> { "None", "Special", "Blinded", "Charmed", "Deafened", "Exhaustion", "Frightened", "Grappled", "Paralyzed", "Petrified", "Poisoned", "Prone", "Restrained", "Stunned", "Unconscious", "Raise Exhaustion" };
            ComboBox_EffectType.SelectionChanged += ComboBox_EffectType_SelectionChanged;
            ComboBox_Condition.SelectionChanged += ComboBox_Condition_SelectionChanged;
            ComboBox_EffectType.SelectedItem = "Attack";
            ComboBox_Condition.SelectedItem = "None";
            TBX_PrimaryDamageAmount.Text = "0";
            TBX_SecondaryDamageAmount.Text = "0";
            IsTarget_Toggled(null, null);
        }

        private void ComboBox_Condition_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComboBox_Condition.SelectedItem.ToString() == "Special") { SpecialForm.Visibility = Visibility.Visible; } else { SpecialForm.Visibility = Visibility.Collapsed; }
        }

        private void ComboBox_EffectType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComboBox_EffectType.SelectedItem.ToString() == "Attack") { AttackForm.Visibility = Visibility.Visible; } else { AttackForm.Visibility = Visibility.Collapsed; }
        }

        public List<CreatureModel> SelectedCreatures
        {
            get
            {
                List<CreatureModel> selectedCreatures = new();
                foreach (object item in ItemsControl_Creatures.Items)
                {
                    CreatureModel creature = item as CreatureModel;
                    if (creature.IsTarget)
                    {
                        selectedCreatures.Add(creature);
                    }
                }
                return selectedCreatures;
            }
        }

        public string SaveAbility
        {
            get
            {
                return ComboBox_SaveAbility.Text;
            }
        }

        public int SaveDifficulty
        {
            get
            {
                bool check = int.TryParse(TextBox_SaveDifficulty.Text, out int result);
                return (check) ? result : -1;
            }
        }

        public int PrimaryDamageOnFail
        {
            get
            {
                bool check = int.TryParse(TBX_PrimaryDamageAmount.Text, out int result);
                return (check) ? result : -1;
            }
        }
        public int SecondaryDamageOnFail
        {
            get
            {
                bool check = int.TryParse(TBX_SecondaryDamageAmount.Text, out int result);
                return (check) ? result : -1;
            }
        }

        public string ConditionOnFail
        {
            get
            {
                return ComboBox_Condition.Text;
            }
        }

        public string SpecialCondition
        {
            get
            {
                return TextBox_SpecialCondition.Text;
            }
        }
        
        public bool HalfOnSave
        {
            get
            {
                return (bool)CheckBox_HalfOnSave.IsChecked;
            }
        }

        public string PrimaryDamageType
        {
            get
            {
                return CBX_PrimaryDamageType.Text;
            }
        }
        public string SecondaryDamageType
        {
            get
            {
                return CBX_SecondaryDamageType.Text;
            }
        }
        public bool IsMagicWeapon
        {
            get
            {
                return (bool)CheckBox_MagicWeapon.IsChecked;
            }
        }
        public bool IsAdamantineWeapon
        {
            get
            {
                return (bool)CheckBox_Adamantine.IsChecked;
            }
        }
        public bool IsSilveredWeapon
        {
            get
            {
                return (bool)CheckBox_Silvered.IsChecked;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (SaveAbility == null) { message += "\nSave Ability must be selected."; }
            if (SaveDifficulty <= 0) { message += "\nInvalid Save Difficulty value."; }
            if (ComboBox_EffectType.SelectedItem.ToString() == "Attack" && PrimaryDamageOnFail <= 0) { message += "\nInvalid primary damage value for attack."; }
            if (ComboBox_EffectType.SelectedItem.ToString() == "Attack" && SecondaryDamageOnFail < 0) { message += "\nInvalid secondary damage value for attack."; }
            if (ComboBox_EffectType.SelectedItem.ToString() == "Attack" && PrimaryDamageType == "") { message += "\nPrimary damage type must be selected."; }
            if (ComboBox_EffectType.SelectedItem.ToString() == "Attack" && SecondaryDamageType == "" && SecondaryDamageOnFail > 0) { message += "\nSecondary damage type must be selected."; }
            bool atLeastOneTarget = false;
            foreach (object item in ItemsControl_Creatures.Items)
            {
                if ((item as CreatureModel).IsTarget == true)
                {
                    atLeastOneTarget = true;
                    break;
                }
            }
            if (atLeastOneTarget == false) { message += "\nMust select at least 1 target."; }
            if (message != "")
            {
                HelperMethods.NotifyUser(message);
                return;
            }
                
            this.DialogResult = true;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (object item in ItemsControl_Creatures.Items)
            {
                (item as CreatureModel).IsTarget = false;
                (item as CreatureModel).HasGroupSaveAdvantage = false;
                (item as CreatureModel).HasGroupSaveDisadvantage = false;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void IsTarget_Toggled(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (object item in ItemsControl_Creatures.Items)
            {
                CreatureModel creature = item as CreatureModel;
                if (creature.IsTarget)
                {
                    count++;
                }
            }
            string output = count + " target" + ((count != 1) ? "s" : "") + " selected";
            SelectedTargetCount.Text = output;
        }
    }
}
