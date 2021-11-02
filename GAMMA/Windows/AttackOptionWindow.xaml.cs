using GAMMA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GAMMA.Windows
{
    public partial class AttackOptionWindow : Window
    {
        // Constructors
        public AttackOptionWindow(AttackModel attack)
        {
            InitializeComponent();
            AttackName.Text = attack.Name;
            string description = "";
            if (attack.HasAttackRoll) 
            { 
                description += string.Format("+{0} to hit, {1}d{2}+{3} {4} damage; ",
                    attack.AttackModifier,
                    attack.DamageDiceQuantity,
                    attack.DamageDiceQuality,
                    attack.DamageDiceModifier,
                    attack.DamageType);
            }
            AttackDescription.Text = description;
            foreach (AttackOptionModel option in attack.AttackOptions)
            {
                option.UpdateAutoText();
            }
            OptionList.ItemsSource = attack.AttackOptions.ToList();
        }

        // Properties
        public List<AttackOptionModel> Options
        {
            get
            {
                return (OptionList.ItemsSource as IEnumerable<AttackOptionModel>).Where(option => option.UseOption).ToList();
            }
        }

        // Methods
        private void Window_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
