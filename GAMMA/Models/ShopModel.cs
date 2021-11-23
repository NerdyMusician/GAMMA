using GAMMA.Toolbox;
using GAMMA.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class ShopModel : BaseModel
    {
        // Constructors
        public ShopModel()
        {
            Name = "New Shop";
            ShopIcons = new List<string>
            {
                "Icon_Anvil",
                "Icon_Book",
                "Icon_Crate",
                "Icon_Fish",
                "Icon_Food",
                "Icon_Holy",
                "Icon_MagicItem",
                "Icon_Musicnote",
                "Icon_Pack",
                "Icon_Poison",
                "Icon_Potion",
                "Icon_Ring",
                "Icon_Shirt",

            };
            ItemTypes = new();

        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode("Single")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Icon
        private string _Icon;
        [XmlSaveMode("Single")]
        public string Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShopIcons
        private List<string> _ShopIcons;
        public List<string> ShopIcons
        {
            get
            {
                return _ShopIcons;
            }
            set
            {
                _ShopIcons = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ItemTypes
        private ObservableCollection<BoolOption> _ItemTypes;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<BoolOption> ItemTypes
        {
            get
            {
                return _ItemTypes;
            }
            set
            {
                _ItemTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveShop
        private RelayCommand _RemoveShop;
        public ICommand RemoveShop
        {
            get
            {
                if (_RemoveShop == null)
                {
                    _RemoveShop = new RelayCommand(param => DoRemoveShop());
                }
                return _RemoveShop;
            }
        }
        private void DoRemoveShop()
        {
            Configuration.MainModelRef.ToolsView.Shops.Remove(this);
        }
        #endregion
        #region OpenShop
        private RelayCommand _OpenShop;
        public ICommand OpenShop
        {
            get
            {
                if (_OpenShop == null)
                {
                    _OpenShop = new RelayCommand(DoOpenShop);
                }
                return _OpenShop;
            }
        }
        private void DoOpenShop(object param)
        {
            InventoryModel sourceInventory = param as InventoryModel;
            List<string> shopItemTypes = new();
            foreach (BoolOption option in ItemTypes)
            {
                if (option.Marked) { shopItemTypes.Add(option.Name); }
            }
            List<ItemModel> characterItems = new List<ItemModel>(sourceInventory.AllItems.Where(item => shopItemTypes.Contains(item.Type)));
            List<ItemModel> shopItems = new List<ItemModel>(Configuration.ItemRepository.Where(item => shopItemTypes.Contains(item.Type)));
            int characterValue = (sourceInventory.PlatinumPieces * 1000) + (sourceInventory.GoldPieces * 100) + (sourceInventory.SilverPieces * 10) + sourceInventory.CopperPieces;

            if (shopItems == null) { return; }
            ShopWindow shopWindow = new ShopWindow(Name, shopItems, characterItems, characterValue);
            shopWindow.ShowDialog();
            if (shopWindow.DialogResult == true)
            {
                int platinum = (Configuration.MainModelRef.SettingsView.UsePlatinum) ? shopWindow.CharacterCurrency / 1000 : 0;
                int gold = (shopWindow.CharacterCurrency - platinum * 1000) / 100;
                int silver = (shopWindow.CharacterCurrency - (platinum * 1000) - (gold * 100)) / 10;
                int copper = shopWindow.CharacterCurrency - (platinum * 1000) - (gold * 100) - (silver * 10);
                sourceInventory.PlatinumPieces = platinum;
                sourceInventory.GoldPieces = gold;
                sourceInventory.SilverPieces = silver;
                sourceInventory.CopperPieces = copper;

                foreach (ItemModel shopItem in shopWindow.ItemsForCharacter)
                {
                    bool itemFound = false;
                    foreach (ItemModel invItem in sourceInventory.AllItems)
                    {
                        if (shopItem.Name == invItem.Name)
                        {
                            invItem.Quantity = shopItem.Quantity;
                            itemFound = true;
                            break;
                        }
                    }
                    if (itemFound == false)
                    {
                        sourceInventory.AllItems.Add(HelperMethods.DeepClone(shopItem));
                    }
                }

                sourceInventory.AllItems = new ObservableCollection<ItemModel>(sourceInventory.AllItems.Where(item => item.Quantity > 0).ToList());
                sourceInventory.FilteredItems = new ObservableCollection<ItemModel>(sourceInventory.AllItems);

            }
        }
        #endregion

        // Public Methods
        public void SetItemTypes()
        {
            List<BoolOption> options = new();
            foreach (string type in Configuration.ItemTypes)
            {
                options.Add(new BoolOption { Name = type });
                BoolOption existingOption = ItemTypes.FirstOrDefault(i => i.Name == type);
                if (existingOption == null) { continue; }
                options.Last().Marked = existingOption.Marked;
            }
            ItemTypes = new ObservableCollection<BoolOption>(options);

        }

    }
}
