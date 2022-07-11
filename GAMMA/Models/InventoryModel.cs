using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class InventoryModel : BaseModel
    {
        // Constructors
        public InventoryModel()
        {
            Name = "New Inventory";
            AllItems = new();
            FilteredItems = new();
            Filters = new();
            SearchText = string.Empty;

            AllItems.CollectionChanged += AllItems_CollectionChanged;
            SetFilterList();
        }

        public void AllItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
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
        #region IsCarried
        private bool _IsCarried;
        [XmlSaveMode(XSME.Single)]
        public bool IsCarried
        {
            get => _IsCarried;
            set
            {
                _IsCarried = value;
                NotifyPropertyChanged();
                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
            }
        }
        #endregion
        #region IsSelected
        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set => SetAndNotify(ref _IsSelected, value);
        }
        #endregion

        #region PlatinumPieces
        private int _PlatinumPieces;
        [XmlSaveMode(XSME.Single)]
        public int PlatinumPieces
        {
            get => _PlatinumPieces;
            set
            {
                _PlatinumPieces = value;
                NotifyPropertyChanged();
                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
            }
        }
        #endregion
        #region GoldPieces
        private int _GoldPieces;
        [XmlSaveMode(XSME.Single)]
        public int GoldPieces
        {
            get => _GoldPieces;
            set
            {
                _GoldPieces = value;
                NotifyPropertyChanged();
                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
            }
        }
        #endregion
        #region SilverPieces
        private int _SilverPieces;
        [XmlSaveMode(XSME.Single)]
        public int SilverPieces
        {
            get => _SilverPieces;
            set
            {
                _SilverPieces = value;
                NotifyPropertyChanged();
                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
            }
        }
        #endregion
        #region CopperPieces
        private int _CopperPieces;
        [XmlSaveMode(XSME.Single)]
        public int CopperPieces
        {
            get => _CopperPieces;
            set
            {
                _CopperPieces = value;
                NotifyPropertyChanged();
                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
            }
        }
        #endregion

        #region AllItems
        private ObservableCollection<ItemModel> _AllItems;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> AllItems
        {
            get => _AllItems;
            set => SetAndNotify(ref _AllItems, value);
        }
        #endregion
        #region FilteredItems
        private ObservableCollection<ItemModel> _FilteredItems;
        public ObservableCollection<ItemModel> FilteredItems
        {
            get => _FilteredItems;
            set => SetAndNotify(ref _FilteredItems, value);
        }
        #endregion
        #region ActiveItem
        private ItemModel _ActiveItem;
        public ItemModel ActiveItem
        {
            get => _ActiveItem;
            set => SetAndNotify(ref _ActiveItem, value);
        }
        #endregion

        #region SearchText
        private string _SearchText;
        public string SearchText
        {
            get => _SearchText;
            set
            {
                _SearchText = value;
                NotifyPropertyChanged();
                UpdateFilteredList();
            }
        }
        #endregion
        #region Filters
        private ObservableCollection<BoolOption> _Filters;
        public ObservableCollection<BoolOption> Filters
        {
            get => _Filters;
            set => SetAndNotify(ref _Filters, value);
        }
        #endregion
        #region Count_AllItems
        private int _Count_AllItems;
        public int Count_AllItems
        {
            get => _Count_AllItems;
            set => SetAndNotify(ref _Count_AllItems, value);
        }
        #endregion
        #region Count_FilteredItems
        private int _Count_FilteredItems;
        public int Count_FilteredItems
        {
            get => _Count_FilteredItems;
            set => SetAndNotify(ref _Count_FilteredItems, value);
        }
        #endregion
        #region ShowFilters
        private bool _ShowFilters;
        public bool ShowFilters
        {
            get => _ShowFilters;
            set => SetAndNotify(ref _ShowFilters, value);
        }
        #endregion

        #region ShowShopList
        private bool _ShowShopList;
        public bool ShowShopList
        {
            get => _ShowShopList;
            set => SetAndNotify(ref _ShowShopList, value);
        }
        #endregion
        #region ItemValue
        private string _ItemValue;
        public string ItemValue
        {
            get => _ItemValue;
            set => SetAndNotify(ref _ItemValue, value);
        }
        #endregion

        // Commands
        #region AddItem
        public ICommand AddItem => new RelayCommand(DoAddItem);
        private void DoAddItem(object type)
        {
            if (type.ToString() == "Custom")
            {
                AllItems.Add(new ItemModel { IsCustomItem = true });
                AllItems.Last().PropertyChanged += InventoryModel_PropertyChanged;
                FilteredItems.Add(AllItems.Last());
                ActiveItem = AllItems.Last();
            }
            if (type.ToString() == "Preset")
            {
                MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
                if (selectionDialog.ShowDialog() == true)
                {
                    foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                    {
                        bool existingFound = false;
                        ItemModel itemToAdd = HelperMethods.DeepClone(item);
                        foreach (ItemModel subItem in AllItems)
                        {
                            if (itemToAdd.Name == subItem.Name)
                            {
                                subItem.Quantity += itemToAdd.Quantity;
                                existingFound = true;
                                break;
                            }
                        }
                        if (existingFound) { continue; }
                        if (itemToAdd.Type == "Alcohol")
                        {
                            itemToAdd.CurrentVolume = itemToAdd.MaxVolume;
                        }
                        AllItems.Add(itemToAdd);
                        AllItems.Last().PropertyChanged += InventoryModel_PropertyChanged;
                        FilteredItems.Add(AllItems.Last());
                    }
                }

                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();

            }
            if (type.ToString() == "TextInput")
            {
                AddItemsFromTextInput();
            }
            UpdateFilteredList();
        }
        #endregion
        #region SelectFilters
        public ICommand SelectFilters => new RelayCommand(DoSelectFilters);
        private void DoSelectFilters(object filter)
        {
            foreach (BoolOption option in Filters)
            {
                option.Marked = (filter.ToString() == "All") ? true : false;
            }
        }
        #endregion
        #region ClearSearch
        public ICommand ClearSearch => new RelayCommand(DoClearSearch);
        private void DoClearSearch(object param)
        {
            SearchText = string.Empty;
            UpdateFilteredList();
        }
        #endregion
        #region ModifyCoinage
        public ICommand ModifyCoinage => new RelayCommand(DoModifyCoinage);
        private void DoModifyCoinage(object param)
        {
            if (param.ToString() == "Add")
            {
                CurrencyDialog currencyDialog = new("Add to " + Name);
                if (currencyDialog.ShowDialog() == true)
                {
                    GoldPieces += currencyDialog.GP;
                    SilverPieces += currencyDialog.SP;
                    CopperPieces += currencyDialog.CP;
                }
            }
            if (param.ToString() == "Subtract")
            {
                CurrencyDialog currencyDialog = new("Subtract from " + Name);
                if (currencyDialog.ShowDialog() == true)
                {
                    if ((GoldPieces - currencyDialog.GP) < 0) { HelperMethods.NotifyUser("Insufficient gold pieces."); return; }
                    if ((SilverPieces - currencyDialog.SP) < 0) { HelperMethods.NotifyUser("Insufficient silver pieces."); return; }
                    if ((CopperPieces - currencyDialog.CP) < 0) { HelperMethods.NotifyUser("Insufficient copper pieces."); return; }
                    GoldPieces -= currencyDialog.GP;
                    SilverPieces -= currencyDialog.SP;
                    CopperPieces -= currencyDialog.CP;
                }
            }
            if (param.ToString() == "Transfer")
            {
                List<string> inventoryNames = new();
                foreach (InventoryModel inventory in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories)
                {
                    if (inventory.Name == Name) { continue; }
                    inventoryNames.Add(inventory.Name);
                }
                CurrencyDialog currencyDialog = new("Transfer from " + Name, inventoryNames);
                if (currencyDialog.ShowDialog() == true)
                {
                    if ((GoldPieces - currencyDialog.GP) < 0) { HelperMethods.NotifyUser("Insufficient gold pieces."); return; }
                    if ((SilverPieces - currencyDialog.SP) < 0) { HelperMethods.NotifyUser("Insufficient silver pieces."); return; }
                    if ((CopperPieces - currencyDialog.CP) < 0) { HelperMethods.NotifyUser("Insufficient copper pieces."); return; }

                    InventoryModel targetInventory = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.FirstOrDefault(inv => inv.Name == currencyDialog.TransferTarget);
                    if (targetInventory == null) { HelperMethods.NotifyUser("Invalid transfer target."); return; }

                    GoldPieces -= currencyDialog.GP;
                    SilverPieces -= currencyDialog.SP;
                    CopperPieces -= currencyDialog.CP;
                    targetInventory.GoldPieces += currencyDialog.GP;
                    targetInventory.SilverPieces += currencyDialog.SP;
                    targetInventory.CopperPieces += currencyDialog.CP;

                }
            }
        }
        #endregion
        #region RemoveInventory
        public ICommand RemoveInventory => new RelayCommand(DoRemoveInventory);
        private void DoRemoveInventory(object param)
        {
            CharacterModel owner = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (owner == null) { HelperMethods.NotifyUser("Invalid ActiveCharacter"); return; }
            if (owner.Inventories[0] == this) { HelperMethods.NotifyUser("Cannot remove base inventory."); return; }
            YesNoDialog question = new("This will remove all currency and items under this inventory.\nContinue?");
            if (question.ShowDialog() == true)
            {
                if (question.Answer == true)
                {
                    owner.Inventories.Remove(this);
                }
            }
        }
        #endregion
        #region AddInventory
        public ICommand AddInventory => new RelayCommand(DoAddInventory);
        private void DoAddInventory(object param)
        {
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.Count >= 6) { HelperMethods.NotifyUser("Inventory tab limit is 6."); return; }
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.Add(new());
        }
        #endregion
        #region TransferItems
        public ICommand TransferItems => new RelayCommand(DoTransferItems);
        private void DoTransferItems(object param)
        {
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.Count < 2)
            {
                HelperMethods.NotifyUser("There is only a single inventory available for this character.");
                return;
            }
            ItemTransfer itemTransfer = new(Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.ToList(), Name);
            itemTransfer.ShowDialog();
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories = new(itemTransfer.UpdatedInventories);
            foreach (InventoryModel inventory in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories)
            {
                inventory.UpdateFilteredList();
            }
        }
        #endregion

        // Public Methods
        public void UpdateFilteredList()
        {
            ObservableCollection<ItemModel> filteredItems = new();
            ObservableCollection<ItemModel> tools = new();
            foreach (ItemModel item in AllItems)
            {
                if (item.Name.ToUpper().Contains(SearchText.ToUpper()) == false) { continue; }
                BoolOption filter = Filters.FirstOrDefault(filter => filter.Name == item.Type);
                if (filter == null) { continue; }

                if (Filters.First(filter => filter.Name == item.Type).Marked) { filteredItems.Add(item); }

            }
            FilteredItems = new(filteredItems.OrderBy(item => item.Name));

            UpdateInventoryItemValueTotal();

            Count_AllItems = AllItems.Count;
            Count_FilteredItems = FilteredItems.Count;
        }
        public void UpdateInventoryItemValueTotal()
        {
            int cp = 0;
            foreach (ItemModel item in AllItems)
            {
                cp += (item.RawValue * item.Quantity);
            }
            ItemValue = HelperMethods.GetDerivedCoinage(cp) + " (" + HelperMethods.GetDerivedCoinage(Convert.ToInt32(cp * 0.6)) + ")";
        }
        public void GetUpdatedItemData()
        {
            List<ItemModel> updatedItems = new();
            foreach (ItemModel item in AllItems)
            {
                if (item.IsCustomItem) { updatedItems.Add(HelperMethods.DeepClone(item)); continue; }
                ItemModel matchedItem = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == item.Name);
                if (matchedItem == null) { updatedItems.Add(HelperMethods.DeepClone(item)); continue; }
                matchedItem = HelperMethods.DeepClone(matchedItem);
                matchedItem.Quantity = item.Quantity;
                matchedItem.CurrentVolume = item.CurrentVolume;
                updatedItems.Add(matchedItem);
            }
            AllItems = new(updatedItems);
        }
        public void InventoryModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CharacterModel owner = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (owner == null) { HelperMethods.NotifyUser("Invalid ActiveCharacter"); return; }
            owner.ItemCollectionChanged(null, null);
        }

        // Private Methods
        private void SetFilterList()
        {
            foreach (string type in Configuration.ItemTypes)
            {
                Filters.Add(new BoolOption { Name = type, Marked = true });
                Filters.Last().PropertyChanged += new PropertyChangedEventHandler(Filter_PropertyChanged);
            }
        }
        private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFilteredList();
        }
        private void AddItemsFromTextInput()
        {
            CharacterModel owner = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (owner == null) { HelperMethods.NotifyUser("Invalid ActiveCharacter"); return; }
            TextDumpDialog textInput = new("Item Add Expected Format:\n{Quantity} x {Item Name}\nNote: 1 line per entry");
            if (textInput.ShowDialog() == true)
            {
                string message = "Added Items:\n";
                string invalidItems = string.Empty;
                string[] lines = textInput.DumpTextValue.Replace("\r","").Split('\n');
                Dictionary<string, int> itemsAndQuantities = new();
                foreach (string line in lines)
                {
                    int index = line.IndexOf("x");
                    if (index == -1) { continue; }
                    if (int.TryParse(line.Substring(0, index).Trim(), out int qty))
                    {
                        string itemName = line.Substring(index + 1).Trim();
                        if (itemsAndQuantities.ContainsKey(itemName))
                        {
                            itemsAndQuantities[itemName] += qty;
                        }
                        else
                        {
                            itemsAndQuantities.Add(itemName, qty);
                        }
                    }
                }
                foreach (KeyValuePair<string, int> itemAndQuantity in itemsAndQuantities)
                {
                    bool existingFound = false;
                    ItemModel matchedItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == itemAndQuantity.Key);
                    if (matchedItem == null || itemAndQuantity.Value < 1) 
                    {
                        invalidItems += itemAndQuantity.Value + " x " + itemAndQuantity.Key + "\n";
                        continue;
                    }
                    ItemModel itemToAdd = HelperMethods.DeepClone(matchedItem);
                    itemToAdd.Quantity = itemAndQuantity.Value;
                    foreach (ItemModel subItem in AllItems)
                    {
                        if (itemToAdd.Name == subItem.Name)
                        {
                            subItem.Quantity += itemToAdd.Quantity;
                            existingFound = true;
                            message += itemAndQuantity.Value + " x " + itemAndQuantity.Key + " (+)\n";
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    if (itemToAdd.Type == "Alcohol")
                    {
                        itemToAdd.CurrentVolume = itemToAdd.MaxVolume;
                    }
                    AllItems.Add(itemToAdd);
                    AllItems.Last().PropertyChanged += InventoryModel_PropertyChanged;
                    FilteredItems.Add(AllItems.Last());
                    message += itemAndQuantity.Value + " x " + itemAndQuantity.Key + "\n";
                }

                if (!string.IsNullOrEmpty(invalidItems))
                {
                    message += "\nSkipped Items:\n";
                    message += invalidItems;
                }
                HelperMethods.NotifyUser(message);
                owner.UpdateInventoryStats();

            }
        }
        
        
    }
}
