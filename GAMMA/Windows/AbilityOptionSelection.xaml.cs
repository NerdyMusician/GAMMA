using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GAMMA.Windows
{
    public partial class AbilityOptionSelection : Window
    {
        // Constructors
        public AbilityOptionSelection(string abilityName, List<CAVariable> variables)
        {
            InitializeComponent();
            List<BoolOption> options = new();
            foreach (CAVariable v in variables)
            {
                if (v.Type == "Toggled Option")
                {
                    options.Add(new() { Name = v.Name });
                    bool.TryParse(v.Value, out bool result);
                    options.Last().Marked = result;
                }
            }
            OptionList.ItemsSource = options;
            AbilityName.Text = "Use " + abilityName + " with...";
        }

        // Properties
        public List<BoolOption> Options
        {
            get
            {
                return (OptionList.ItemsSource as IEnumerable<BoolOption>).ToList();
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
