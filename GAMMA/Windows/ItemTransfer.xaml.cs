using GAMMA.CustomControls;
using GAMMA.Models;
using GAMMA.Toolbox;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class ItemTransfer : Window
    {
        public ItemTransfer(List<InventoryModel> inventories, string initialFrom)
        {
            InitializeComponent();
            UpdatedInventories = inventories;
            foreach (InventoryModel inventory in UpdatedInventories)
            {
                inventory.AllItems = new(inventory.AllItems.OrderBy(i => i.Name));
            }
            List<string> inventoryNames = new();
            foreach (InventoryModel inventory in UpdatedInventories)
            {
                inventoryNames.Add(inventory.Name);
            }
            CBX_ListOne.ItemsSource = inventoryNames;
            CBX_ListTwo.ItemsSource = inventoryNames;
            CBX_ListOne.Text = initialFrom;
            
        }

        public List<InventoryModel> UpdatedInventories;

        private void CBX_ListOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ITM_ListOne.ItemsSource = UpdatedInventories.FirstOrDefault(i => i.Name == CBX_ListOne.SelectedValue.ToString()).AllItems;
            if (CBX_ListTwo.SelectedValue == null) 
            { 
                CBX_ListTwo.Text = (CBX_ListTwo.ItemsSource as List<string>).First(i => i != CBX_ListTwo.Text); 
            }
            if (CBX_ListOne.SelectedValue.ToString() == CBX_ListTwo.SelectedValue.ToString())
            {
                CBX_ListTwo.Text = (CBX_ListTwo.ItemsSource as List<string>).First(i => i != CBX_ListTwo.Text);
            }
        }
        private void CBX_ListTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ITM_ListTwo.ItemsSource = UpdatedInventories.FirstOrDefault(i => i.Name == CBX_ListTwo.SelectedValue.ToString()).AllItems;
            if (CBX_ListOne.SelectedValue.ToString() == CBX_ListTwo.SelectedValue.ToString())
            {
                CBX_ListOne.Text = (CBX_ListOne.ItemsSource as List<string>).First(i => i != CBX_ListOne.Text);
            }
        }
        private void BTN_TransferToTwo_Click(object sender, RoutedEventArgs e)
        {
            bool existingItemFound = false;
            ItemModel thisItem = (sender as MiniButton).DataContext as ItemModel;
            foreach (ItemModel item in ITM_ListTwo.ItemsSource)
            {
                if (thisItem.Name == item.Name) 
                { 
                    item.Quantity++;
                    existingItemFound = true;
                    break;
                }
            }
            if (existingItemFound == false)
            {
                ItemModel itemToAdd = HelperMethods.DeepClone(thisItem);
                itemToAdd.Quantity = 1;
                (ITM_ListTwo.ItemsSource as ObservableCollection<ItemModel>).Add(itemToAdd);
            }
            thisItem.Quantity--;
            if (thisItem.Quantity <= 0)
            {
                (ITM_ListOne.ItemsSource as ObservableCollection<ItemModel>).Remove(thisItem);
            }
        }
        private void BTN_TransferToOne_Click(object sender, RoutedEventArgs e)
        {
            bool existingItemFound = false;
            ItemModel thisItem = (sender as MiniButton).DataContext as ItemModel;
            foreach (ItemModel item in ITM_ListOne.ItemsSource)
            {
                if (thisItem.Name == item.Name)
                {
                    item.Quantity++;
                    existingItemFound = true;
                    break;
                }
            }
            if (existingItemFound == false)
            {
                ItemModel itemToAdd = HelperMethods.DeepClone(thisItem);
                itemToAdd.Quantity = 1;
                (ITM_ListOne.ItemsSource as ObservableCollection<ItemModel>).Add(itemToAdd);
            }
            thisItem.Quantity--;
            if (thisItem.Quantity <= 0)
            {
                (ITM_ListTwo.ItemsSource as ObservableCollection<ItemModel>).Remove(thisItem);
            }
        }
        private void DoneButton_Click(object sender, RoutedEventArgs e)
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
