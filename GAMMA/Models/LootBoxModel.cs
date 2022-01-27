using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class LootBoxModel : BaseModel
    {
        // Constructors
        public LootBoxModel()
        {
            Name = "New Loot Box";
            Items = new ObservableCollection<ItemModel>();
            ItemLinks = new();
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region Items
        private ObservableCollection<ItemModel> _Items;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> Items
        {
            get => _Items;
            set => SetAndNotify(ref _Items, value);
        }
        #endregion
        #region ItemLinks
        private ObservableCollection<ItemLink> _ItemLinks;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemLink> ItemLinks
        {
            get => _ItemLinks;
            set => SetAndNotify(ref _ItemLinks, value);
        }
        #endregion
        #region CoinageMinimum
        private int _CoinageMinimum;
        [XmlSaveMode(XSME.Single)]
        public int CoinageMinimum
        {
            get => _CoinageMinimum;
            set
            {
                _CoinageMinimum = value;
                NotifyPropertyChanged();
                ProcessedMinimum = HelperMethods.GetDerivedCoinage(value);
            }
        }
        #endregion
        #region CoinageMaximum
        private int _CoinageMaximum;
        [XmlSaveMode(XSME.Single)]
        public int CoinageMaximum
        {
            get => _CoinageMaximum;
            set
            {
                _CoinageMaximum = value;
                NotifyPropertyChanged();
                ProcessedMaximum = HelperMethods.GetDerivedCoinage(value);
            }
        }
        #endregion
        #region ProcessedMinimum
        private string _ProcessedMinimum;
        public string ProcessedMinimum
        {
            get => _ProcessedMinimum;
            set => SetAndNotify(ref _ProcessedMinimum, value);
        }
        #endregion
        #region ProcessedMaximum
        private string _ProcessedMaximum;
        public string ProcessedMaximum
        {
            get => _ProcessedMaximum;
            set => SetAndNotify(ref _ProcessedMaximum, value);
        }
        #endregion

        // Commands
        #region AddItem
        public ICommand AddItem => new RelayCommand(param => DoAddItem());
        private void DoAddItem()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    ItemModel itemToAdd = HelperMethods.DeepClone(item);
                    foreach (ItemModel lbItem in Items)
                    {
                        if (item.Name == lbItem.Name)
                        {
                            lbItem.Quantity += item.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    Items.Add(itemToAdd);
                    itemToAdd.DropChance = 100;
                }
            }

        }
        #endregion
        #region AddItemLink
        public ICommand AddItemLink => new RelayCommand(param => DoAddItemLink());
        private void DoAddItemLink()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    foreach (ItemLink lbItem in ItemLinks)
                    {
                        if (item.Name == lbItem.Name)
                        {
                            lbItem.Quantity += item.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    ItemLinks.Add(new ItemLink { Name = item.Name, DropChance = 100, Quantity = item.Quantity, LinkedItem = item });
                }
            }

        }
        #endregion
        #region DeleteLootBox
        public ICommand DeleteLootBox => new RelayCommand(param => DoDeleteLootBox());
        private void DoDeleteLootBox()
        {
            Configuration.MainModelRef.ToolsView.LootBoxes.Remove(this);
        }
        #endregion
        #region RollLoot
        public ICommand RollLoot => new RelayCommand(param => DoRollLoot());
        private void DoRollLoot()
        {
            string message = "Loot found in " + Name + ":";
            int coinRoll = Configuration.RNG.Next(CoinageMinimum, CoinageMaximum + 1);
            message += "\nMoney: " + HelperMethods.GetDerivedCoinage(coinRoll);
            foreach (ItemLink link in ItemLinks)
            {
                int dropQty = 0;
                for (int i = 0; i < link.Quantity; i++)
                {
                    dropQty += (Configuration.RNG.Next(1, 101) <= link.DropChance) ? 1 : 0;
                }
                if (dropQty > 0) { message += string.Format("\n{0} x {1}", dropQty, link.Name); }
            }

            HelperMethods.NotifyUser(message);

        }
        #endregion

        // Public Methods
        public void UpdateToNewLootSystem()
        {
            if (ItemLinks.Count() > 0) { return; } // implies the conversion has already taken place
            foreach (ItemModel lootItem in Items)
            {
                ItemLinks.Add(new ItemLink { Name = lootItem.Name, DropChance = lootItem.DropChance, Quantity = lootItem.Quantity, LinkedItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == lootItem.Name) });
            }
            Items.Clear();
        }
        public void ConnectItemLinks()
        {
            foreach (ItemLink link in ItemLinks)
            {
                link.LinkedItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == link.Name);
            }
        }

    }
}
