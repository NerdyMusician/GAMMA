using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class ShopWindow : Window
    {
        public ShopWindow(string shopType, List<ItemModel> shopItems, List<ItemModel> characterItems, int characterCoinage)
        {
            this.Title = shopType;
            InitializeComponent();
            DataContext = new ShopViewModel(shopItems, characterItems, characterCoinage);
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopGreeting);
        }

        public List<ItemModel> ItemsForCharacter
        {
            get
            {
                List<ItemModel> items = new List<ItemModel>((DataContext as ShopViewModel).CharacterItems);
                foreach (ItemModel item in (DataContext as ShopViewModel).ShopOfferedItems)
                {
                    bool foundItem = false;
                    foreach (ItemModel xItem in items)
                    {
                        if (xItem.Name == item.Name)
                        {
                            foundItem = true;
                            xItem.Quantity += item.Quantity;
                        }
                    }
                    if (foundItem == false)
                    {
                        items.Add(HelperMethods.DeepClone(item));
                    }
                }
                return items;
            }
        }
        public int CharacterCurrency
        {
            get
            {
                return (DataContext as ShopViewModel).CharacterCoinage + (DataContext as ShopViewModel).TransactionValue;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as ShopViewModel).CharacterCoinage + (DataContext as ShopViewModel).TransactionValue < 0)
            {
                new NotificationDialog("Invalid funds.").ShowDialog();
                return;
            }
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopFarewell);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ShopViewModel).UndoCharacterOfferings();
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopFarewell);
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
