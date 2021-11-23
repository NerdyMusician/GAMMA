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
        public InventoryModel(CharacterModel character)
        {
            Owner = character;
            Name = "New Inventory";
            AllItems = new ObservableCollection<ItemModel>();
            FilteredItems = new ObservableCollection<ItemModel>();
            Filters = new ObservableCollection<BoolOption>();
            SearchText = "";

            AllItems.CollectionChanged += AllItems_CollectionChanged;
            SetFilterList();
        }

        public void AllItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Owner.UpdateInventoryStats();
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
        #region IsCarried
        private bool _IsCarried;
        [XmlSaveMode("Single")]
        public bool IsCarried
        {
            get
            {
                return _IsCarried;
            }
            set
            {
                _IsCarried = value;
                NotifyPropertyChanged();
                Owner.UpdateInventoryStats();
            }
        }
        #endregion
        #region Owner
        private CharacterModel _Owner;
        public CharacterModel Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsSelected
        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        //#region ShowPlatinum
        //private bool _ShowPlatinum;
        //public bool ShowPlatinum
        //{
        //    get
        //    {
        //        return _ShowPlatinum;
        //    }
        //    set
        //    {
        //        _ShowPlatinum = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //#endregion
        #region PlatinumPieces
        private int _PlatinumPieces;
        [XmlSaveMode("Single")]
        public int PlatinumPieces
        {
            get
            {
                return _PlatinumPieces;
            }
            set
            {
                _PlatinumPieces = value;
                NotifyPropertyChanged();
                Owner.UpdateInventoryStats();
            }
        }
        #endregion
        #region GoldPieces
        private int _GoldPieces;
        [XmlSaveMode("Single")]
        public int GoldPieces
        {
            get
            {
                return _GoldPieces;
            }
            set
            {
                _GoldPieces = value;
                NotifyPropertyChanged();
                Owner.UpdateInventoryStats();
            }
        }
        #endregion
        #region SilverPieces
        private int _SilverPieces;
        [XmlSaveMode("Single")]
        public int SilverPieces
        {
            get
            {
                return _SilverPieces;
            }
            set
            {
                _SilverPieces = value;
                NotifyPropertyChanged();
                Owner.UpdateInventoryStats();
            }
        }
        #endregion
        #region CopperPieces
        private int _CopperPieces;
        [XmlSaveMode("Single")]
        public int CopperPieces
        {
            get
            {
                return _CopperPieces;
            }
            set
            {
                _CopperPieces = value;
                NotifyPropertyChanged();
                Owner.UpdateInventoryStats();
            }
        }
        #endregion

        #region AllItems
        private ObservableCollection<ItemModel> _AllItems;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemModel> AllItems
        {
            get
            {
                return _AllItems;
            }
            set
            {
                _AllItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredItems
        private ObservableCollection<ItemModel> _FilteredItems;
        public ObservableCollection<ItemModel> FilteredItems
        {
            get
            {
                return _FilteredItems;
            }
            set
            {
                _FilteredItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveItem
        private ItemModel _ActiveItem;
        public ItemModel ActiveItem
        {
            get
            {
                return _ActiveItem;
            }
            set
            {
                _ActiveItem = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SearchText
        private string _SearchText;
        public string SearchText
        {
            get
            {
                return _SearchText;
            }
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
            get
            {
                return _Filters;
            }
            set
            {
                _Filters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Count_AllItems
        private int _Count_AllItems;
        public int Count_AllItems
        {
            get
            {
                return _Count_AllItems;
            }
            set
            {
                _Count_AllItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Count_FilteredItems
        private int _Count_FilteredItems;
        public int Count_FilteredItems
        {
            get
            {
                return _Count_FilteredItems;
            }
            set
            {
                _Count_FilteredItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowFilters
        private bool _ShowFilters;
        public bool ShowFilters
        {
            get
            {
                return _ShowFilters;
            }
            set
            {
                _ShowFilters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ShowShopList
        private bool _ShowShopList;
        public bool ShowShopList
        {
            get
            {
                return _ShowShopList;
            }
            set
            {
                _ShowShopList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ItemValue
        private string _ItemValue;
        public string ItemValue
        {
            get
            {
                return _ItemValue;
            }
            set
            {
                _ItemValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddItem
        private RelayCommand _AddItem;
        public ICommand AddItem
        {
            get
            {
                if (_AddItem == null)
                {
                    _AddItem = new RelayCommand(DoAddItem);
                }
                return _AddItem;
            }
        }
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
                MultiObjectSelectionDialog selectionDialog = new MultiObjectSelectionDialog(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
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

                Owner.UpdateInventoryStats();
                //Owner.UpdateTotals();

            }
            UpdateFilteredList();
        }
        #endregion
        #region SelectFilters
        private RelayCommand _SelectFilters;
        public ICommand SelectFilters
        {
            get
            {
                if (_SelectFilters == null)
                {
                    _SelectFilters = new RelayCommand(DoSelectFilters);
                }
                return _SelectFilters;
            }
        }
        private void DoSelectFilters(object filter)
        {
            foreach (BoolOption option in Filters)
            {
                option.Marked = (filter.ToString() == "All") ? true : false;
            }
        }
        #endregion
        #region ClearSearch
        private RelayCommand _ClearSearch;
        public ICommand ClearSearch
        {
            get
            {
                if (_ClearSearch == null)
                {
                    _ClearSearch = new RelayCommand(param => DoClearSearch());
                }
                return _ClearSearch;
            }
        }
        private void DoClearSearch()
        {
            SearchText = "";
            UpdateFilteredList();
        }
        #endregion
        #region ModifyCoinage
        private RelayCommand _ModifyCoinage;
        public ICommand ModifyCoinage
        {
            get
            {
                if (_ModifyCoinage == null)
                {
                    _ModifyCoinage = new RelayCommand(DoModifyCoinage);
                }
                return _ModifyCoinage;
            }
        }
        private void DoModifyCoinage(object param)
        {
            if (param.ToString() == "Add")
            {
                CurrencyDialog currencyDialog = new CurrencyDialog("Add to " + Name);
                if (currencyDialog.ShowDialog() == true)
                {
                    GoldPieces += currencyDialog.GP;
                    SilverPieces += currencyDialog.SP;
                    CopperPieces += currencyDialog.CP;
                }
            }
            if (param.ToString() == "Subtract")
            {
                CurrencyDialog currencyDialog = new CurrencyDialog("Subtract from " + Name);
                if (currencyDialog.ShowDialog() == true)
                {
                    if ((GoldPieces - currencyDialog.GP) < 0) { new NotificationDialog("Insufficient gold pieces.").ShowDialog(); return; }
                    if ((SilverPieces - currencyDialog.SP) < 0) { new NotificationDialog("Insufficient silver pieces.").ShowDialog(); return; }
                    if ((CopperPieces - currencyDialog.CP) < 0) { new NotificationDialog("Insufficient copper pieces.").ShowDialog(); return; }
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
                CurrencyDialog currencyDialog = new CurrencyDialog("Transfer from " + Name, inventoryNames);
                if (currencyDialog.ShowDialog() == true)
                {
                    if ((GoldPieces - currencyDialog.GP) < 0) { new NotificationDialog("Insufficient gold pieces.").ShowDialog(); return; }
                    if ((SilverPieces - currencyDialog.SP) < 0) { new NotificationDialog("Insufficient silver pieces.").ShowDialog(); return; }
                    if ((CopperPieces - currencyDialog.CP) < 0) { new NotificationDialog("Insufficient copper pieces.").ShowDialog(); return; }

                    InventoryModel targetInventory = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.FirstOrDefault(inv => inv.Name == currencyDialog.TransferTarget);
                    if (targetInventory == null) { new NotificationDialog("Invalid transfer target.").ShowDialog(); return; }

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
        private RelayCommand _RemoveInventory;
        public ICommand RemoveInventory
        {
            get
            {
                if (_RemoveInventory == null)
                {
                    _RemoveInventory = new RelayCommand(param => DoRemoveInventory());
                }
                return _RemoveInventory;
            }
        }
        private void DoRemoveInventory()
        {
            if (Owner.Inventories[0] == this) { new NotificationDialog("Cannot remove base inventory.").ShowDialog(); return; }
            YesNoDialog question = new YesNoDialog("This will remove all currency and items under this inventory.\nContinue?");
            if (question.ShowDialog() == true)
            {
                if (question.Answer == true)
                {
                    Owner.Inventories.Remove(this);
                }
            }
        }
        #endregion
        #region AddInventory
        private RelayCommand _AddInventory;
        public ICommand AddInventory
        {
            get
            {
                if (_AddInventory == null)
                {
                    _AddInventory = new RelayCommand(param => DoAddInventory());
                }
                return _AddInventory;
            }
        }
        private void DoAddInventory()
        {
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.Count() >= 6) { new NotificationDialog("Inventory tab limit is 6.").ShowDialog(); return; }
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories.Add(new InventoryModel(Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter));
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
            ObservableCollection<ItemModel> filteredItems = new ObservableCollection<ItemModel>();
            ObservableCollection<ItemModel> tools = new ObservableCollection<ItemModel>();
            int cp = 0;
            foreach (ItemModel item in AllItems)
            {
                cp += (item.RawValue * item.Quantity);
                if (item.Name.ToUpper().Contains(SearchText.ToUpper()) == false) { continue; }
                BoolOption filter = Filters.FirstOrDefault(filter => filter.Name == item.Type);
                if (filter == null) { continue; }

                if (Filters.First(filter => filter.Name == item.Type).Marked) { filteredItems.Add(item); }

            }
            FilteredItems = new ObservableCollection<ItemModel>(filteredItems.OrderBy(item => item.Name));

            ItemValue = HelperMethods.GetDerivedCoinage(cp) + " (" + HelperMethods.GetDerivedCoinage(Convert.ToInt32(cp * 0.6)) + ")";

            Count_AllItems = AllItems.Count();
            Count_FilteredItems = FilteredItems.Count();
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
            Owner.ItemCollectionChanged(null, null);
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
        
        
    }
}
