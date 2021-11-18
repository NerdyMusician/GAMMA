using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using System.Windows;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class ImprovCreatureDialog : Window
    {
        public ImprovCreatureDialog()
        {
            InitializeComponent();
            CBX_DamageType.ItemsSource = Configuration.DamageTypes;
            TBX_Name.Text = "Improvised Creature";
            TBX_Strength.Text = "10";
            TBX_Dexterity.Text = "10";
            TBX_Constitution.Text = "10";
            TBX_Intelligence.Text = "10";
            TBX_Wisdom.Text = "10";
            TBX_Charisma.Text = "10";
            TBX_HitPoints.Text = "20";
            TBX_ArmorClass.Text = "10";
            TBX_Speed.Text = "30";
            TBX_AttackBonus.Text = "3";
            TBX_DamageDiceQuantity.Text = "1";
            TBX_DamageDiceSides.Text = "6";
            TBX_DamageDiceBonus.Text = "1";
            CBX_DamageType.Text = "Bludgeoning";
            TBX_Quantity.Text = "1";
        }

        public string CreatureName
        {
            get
            {
                if (TBX_Name.Text == null || TBX_Name.Text == "") { return "Improvised Creature"; }
                else { return TBX_Name.Text; }
            }
        }
        public int StrengthScore
        {
            get
            {
                int.TryParse(TBX_Strength.Text, out int str);
                return str;
            }
        }
        public int DexterityScore
        {
            get
            {
                int.TryParse(TBX_Dexterity.Text, out int dex);
                return dex;
            }
        }
        public int ConstitutionScore
        {
            get
            {
                int.TryParse(TBX_Constitution.Text, out int con);
                return con;
            }
        }
        public int IntelligenceScore
        {
            get
            {
                int.TryParse(TBX_Strength.Text, out int intel);
                return intel;
            }
        }
        public int WisdomScore
        {
            get
            {
                int.TryParse(TBX_Wisdom.Text, out int wis);
                return wis;
            }
        }
        public int CharismaScore
        {
            get
            {
                int.TryParse(TBX_Charisma.Text, out int cha);
                return cha;
            }
        }
        public int HitPoints
        {
            get
            {
                int.TryParse(TBX_HitPoints.Text, out int hp);
                return hp;
            }
        }
        public int ArmorClass
        {
            get
            {
                int.TryParse(TBX_ArmorClass.Text, out int ac);
                return ac;
            }
        }
        public int Speed
        {
            get
            {
                int.TryParse(TBX_Speed.Text, out int spd);
                return spd;
            }
        }
        public CustomAbility Attack
        {
            get
            {
                string dmgType = "Bludgeoning";
                int.TryParse(TBX_AttackBonus.Text, out int atkBns);
                int.TryParse(TBX_DamageDiceQuantity.Text, out int diceQty);
                int.TryParse(TBX_DamageDiceSides.Text, out int diceSides);
                int.TryParse(TBX_DamageDiceBonus.Text, out int dmgBns);
                if(CBX_DamageType.Text != null && CBX_DamageType.Text != "") { dmgType = CBX_DamageType.Text; }
                return new CustomAbility(atkBns, diceQty, diceSides, dmgBns, dmgType);
            }
        }

        public int Quantity
        {
            get
            {
                int.TryParse(TBX_Quantity.Text, out int qty);
                return qty;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

    }
}
