using GAMMA.Models;
using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class ObjectSelectionDialog : Window
    {
        // Constructors
        public ObjectSelectionDialog()
        {
            InitializeComponent();
        }
        public ObjectSelectionDialog(List<ItemModel> items, bool multiAdd = false)
        {
            InitializeComponent();
            DialogHeader.Text = "Select an Item";
            ObjectSelectDropdown.OriginalSource = items;
            ObjectSelectDropdown.ItemsSource = items;
            if (multiAdd)
            {
                QuantitySection.Visibility = Visibility.Visible;
            }
        }
        public ObjectSelectionDialog(List<CreatureModel> items, bool multiAdd = false)
        {
            InitializeComponent();
            DialogHeader.Text = "Select an Creature";
            ObjectSelectDropdown.OriginalSource = items;
            ObjectSelectDropdown.ItemsSource = items;
            if (multiAdd)
            {
                QuantitySection.Visibility = Visibility.Visible;
            }
        }
        public ObjectSelectionDialog(List<SpellModel> spells)
        {
            InitializeComponent();
            DialogHeader.Text = "Select a Spell";
            ObjectSelectDropdown.OriginalSource = spells;
            ObjectSelectDropdown.ItemsSource = spells;
        }
        public ObjectSelectionDialog(List<CreaturePackModel> packs)
        {
            InitializeComponent();
            DialogHeader.Text = "Select a Pack";
            ObjectSelectDropdown.OriginalSource = packs;
            ObjectSelectDropdown.ItemsSource = packs;
        }
        public ObjectSelectionDialog(List<InventoryModel> inventories, int quantity)
        {
            InitializeComponent();
            DialogHeader.Text = "Select an Inventory";
            ObjectSelectDropdown.OriginalSource = inventories;
            ObjectSelectDropdown.ItemsSource = inventories;
            QuantitySection.Visibility = Visibility.Visible;
            TextBox_Quantity.Text = quantity.ToString();
        }
        public ObjectSelectionDialog(List<ConvertibleValue> options, string header)
        {
            InitializeComponent();
            DialogHeader.Text = header;
            ObjectSelectDropdown.OriginalSource = options;
            ObjectSelectDropdown.ItemsSource = options;
        }
        public ObjectSelectionDialog(List<LabeledNumber> options, string header)
        {
            InitializeComponent();
            DialogHeader.Text = header;
            ObjectSelectDropdown.OriginalSource = options;
            ObjectSelectDropdown.ItemsSource = options;
        }

        // Properties
        public object SelectedObject
        {
            get
            {
                return ObjectSelectDropdown.SelectedItem;
            }
        }
        public int Quantity
        {
            get
            {
                if (int.TryParse(TextBox_Quantity.Text, out int quantity) == false)
                {
                    return 1;
                }
                return Convert.ToInt32(quantity);
            }
        }

        // Private Methods
        private void Window_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
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
