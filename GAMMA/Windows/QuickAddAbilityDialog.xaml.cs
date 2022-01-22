using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GAMMA.Windows
{
    /// <summary>
    /// Interaction logic for QuickAddAbilityDialog.xaml
    /// </summary>
    public partial class QuickAddAbilityDialog : Window
    {
        public QuickAddAbilityDialog(bool includeSaveDc = false)
        {
            InitializeComponent();
            LBL_UtilizedStat.Text = (includeSaveDc) ? "Save Ability" : "Attack Ability";
            ROW_SaveDc.Visibility = (includeSaveDc) ? Visibility.Visible : Visibility.Collapsed;
            CBX_UtilizedStat.ItemsSource = new List<string>() { "Spellcasting Ability Modifier", "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
            CBX_DamageTypeA.ItemsSource = new List<string>(Configuration.DamageTypes);
            CBX_DamageTypeB.ItemsSource = new List<string>(Configuration.DamageTypes);
        }

        public string AbilityName
        {
            get
            {
                return (string.IsNullOrEmpty(TBX_AbilityName.Text) ? "New Ability" : TBX_AbilityName.Text);
            }
        }
        public string UtilizedStat
        {
            get
            {
                return string.IsNullOrEmpty(CBX_UtilizedStat.Text) ? "Strength" : CBX_UtilizedStat.Text;
            }
        }
        public int SaveDc
        {
            get
            {
                return int.TryParse(TBX_SaveDc.Text, out int dc) ? dc : 0;
            }
        }
        public int DamageDiceQuantityA
        {
            get
            {
                return int.TryParse(TBX_DamageDiceQtyA.Text, out int dice) ? dice : 0;
            }
        }
        public int DamageDiceQuantityB
        {
            get
            {
                return int.TryParse(TBX_DamageDiceQtyB.Text, out int dice) ? dice : 0;
            }
        }
        public int DamageDiceSidesA
        {
            get
            {
                return int.TryParse(TBX_DamageDiceSidesA.Text, out int dice) ? dice : 0;
            }
        }
        public int DamageDiceSidesB
        {
            get
            {
                return int.TryParse(TBX_DamageDiceSidesB.Text, out int dice) ? dice : 0;
            }
        }
        public string DamageTypeA
        {
            get
            {
                return string.IsNullOrEmpty(CBX_DamageTypeA.Text) ? "Slashing" : CBX_DamageTypeA.Text;
            }
        }
        public string DamageTypeB
        {
            get
            {
                return string.IsNullOrEmpty(CBX_DamageTypeB.Text) ? "Slashing" : CBX_DamageTypeB.Text;
            }
        }
        public bool IncludeHalfDamage
        {
            get
            {
                return (bool)CHK_IncludeHalfDamages.IsChecked;
            }
        }
        public bool AddModToDamageA
        {
            get
            {
                return (bool)CHK_IncludeDamageModA.IsChecked;
            }
        }
        public bool AddModToDamageB
        {
            get
            {
                return (bool)CHK_IncludeDamageModB.IsChecked;
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
