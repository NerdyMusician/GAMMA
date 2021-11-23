using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class ItemBuilderViewModel : BaseModel
    {
        // Constructors
        public ItemBuilderViewModel()
        {
            // Initialization
            XmlMethods.XmlToList(Configuration.ItemDataFilePath, out List<ItemModel> items);
            AllItems = new ObservableCollection<ItemModel>(items);
            FilteredItems = new ObservableCollection<ItemModel>(AllItems.ToList());
            ItemTypeFilters = new ObservableCollection<BoolOption>();
            SetFilterLists();
            ItemSearchText = "";
            Configuration.ItemRepository = AllItems.ToList();
        }

        // Databound Properties
        #region AllItems
        private ObservableCollection<ItemModel> _AllItems;
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

        #region ShowTools
        private bool _ShowTools;
        public bool ShowTools
        {
            get
            {
                return _ShowTools;
            }
            set
            {
                _ShowTools = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ItemSearchText
        private string _ItemSearchText;
        public string ItemSearchText
        {
            get
            {
                return _ItemSearchText;
            }
            set
            {
                _ItemSearchText = value;
                NotifyPropertyChanged();
                UpdateItemFilter();
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
        #region ItemTypeFilters
        private ObservableCollection<BoolOption> _ItemTypeFilters;
        public ObservableCollection<BoolOption> ItemTypeFilters
        {
            get
            {
                return _ItemTypeFilters;
            }
            set
            {
                _ItemTypeFilters = value;
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

        // Commands
        #region AddItem
        private RelayCommand _AddItem;
        public ICommand AddItem
        {
            get
            {
                if (_AddItem == null)
                {
                    _AddItem = new RelayCommand(param => DoAddItem());
                }
                return _AddItem;
            }
        }
        private void DoAddItem()
        {
            AllItems.Add(new ItemModel());
            FilteredItems.Add(AllItems.Last());
            ActiveItem = FilteredItems.Last();
        }
        #endregion
        #region SaveItems
        private RelayCommand _SaveItems;
        public ICommand SaveItems
        {
            get
            {
                if (_SaveItems == null)
                {
                    _SaveItems = new RelayCommand(param => DoSaveItems());
                }
                return _SaveItems;
            }
        }
        public bool DoSaveItems(bool notifyUser = true)
        {
            if (CheckItemReferences() == false) { return false; }
            if (CheckItemRestrictions() == false) { return false; }
            if (AllItems.Count() == 0)
            {
                // Prevents zero item save crash
                XDocument blankDoc = new XDocument();
                blankDoc.Add(new XElement("ItemModelSet"));
                blankDoc.Save("Data/Items.xml");
                return true;
            }
            List<string> duplicateItems = new();
            foreach (ItemModel item in AllItems)
            {
                if (AllItems.Where(aItem => aItem.Name == item.Name).Count() > 1)
                {
                    if (duplicateItems.Contains(item.Name) == false) { duplicateItems.Add(item.Name); }
                }
            }
            if (duplicateItems.Count() > 0)
            {
                string message = "Duplicate items found:\n";
                foreach (string item in duplicateItems)
                {
                    message += item + "\n";
                }
                new NotificationDialog(message).ShowDialog();
                return false;
            }
            XDocument itemDocument = new XDocument();
            itemDocument.Add(XmlMethods.ListToXml(AllItems.ToList()));
            itemDocument.Save("Data/Items.xml");
            Configuration.ItemRepository = AllItems.ToList();
            HelperMethods.WriteToLogFile("Items Saved.", notifyUser);

            return true;
        }
        #endregion
        #region SortItems
        private RelayCommand _SortItems;
        public ICommand SortItems
        {
            get
            {
                if (_SortItems == null)
                {
                    _SortItems = new RelayCommand(param => DoSortItems());
                }
                return _SortItems;
            }
        }
        private void DoSortItems()
        {
            AllItems = new ObservableCollection<ItemModel>(AllItems.OrderBy(item => item.Name));
            FilteredItems = new ObservableCollection<ItemModel>(FilteredItems.OrderBy(item => item.Name));
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
            foreach (BoolOption option in ItemTypeFilters)
            {
                option.Marked = (filter.ToString() == "All") ? true : false;
            }
        }
        #endregion
        #region ImportItems
        public ICommand ImportItems => new RelayCommand(param => DoImportItems());
        private void DoImportItems()
        {
            OpenFileDialog openWindow = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new YesNoDialog("Prior to import, the current item list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            if (DoSaveItems() == false) { return; }

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Items(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion
        #region ClearItemSearch
        private RelayCommand _ClearItemSearch;
        public ICommand ClearItemSearch
        {
            get
            {
                if (_ClearItemSearch == null)
                {
                    _ClearItemSearch = new RelayCommand(param => DoClearItemSearch());
                }
                return _ClearItemSearch;
            }
        }
        private void DoClearItemSearch()
        {
            ItemSearchText = "";
        }
        #endregion

        // Public Methods
        public void UpdateItemFilter()
        {
            ObservableCollection<ItemModel> filteredItems = new ObservableCollection<ItemModel>();
            foreach (ItemModel item in AllItems)
            {
                if (item.Name.ToUpper().Contains(ItemSearchText.ToUpper()) == false) { continue; }

                // 1.22: null check, crashed when an item type not in config was set to an item
                BoolOption filter = ItemTypeFilters.FirstOrDefault(filter => filter.Name == item.Type);
                if (filter == null) { continue; }

                if (filter.Marked) { filteredItems.Add(item); }
            }
            FilteredItems = new ObservableCollection<ItemModel>(filteredItems.OrderBy(item => item.Name));

            Count_FilteredItems = FilteredItems.Count();
            Count_AllItems = AllItems.Count();

        }

        // Private Methods
        private void SetFilterLists()
        {
            foreach (string type in Configuration.ItemTypes)
            {
                ItemTypeFilters.Add(new BoolOption { Name = type, Marked = true });
                ItemTypeFilters.Last().PropertyChanged += new PropertyChangedEventHandler(ItemTypeFilter_PropertyChanged);
            }
        }
        private void ItemTypeFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateItemFilter();
        }
        private bool CheckItemReferences()
        {
            List<string> allItemNames = new List<string> { null, "" };
            string message = "Missing References:";
            bool missingReference = false;
            CreatureBuilderViewModel creatureBuilder = Configuration.MainModelRef.CreatureBuilderView;
            CharacterBuilderViewModel characterBuilder = Configuration.MainModelRef.CharacterBuilderView;
            SpellBuilderViewModel spellBuilder = Configuration.MainModelRef.SpellBuilderView;
            foreach (ItemModel item in AllItems)
            {
                allItemNames.Add(item.Name);
            }
            foreach (ItemModel item in AllItems)
            {
                foreach (ItemModel component in item.CraftingComponents)
                {
                    if (allItemNames.Contains(component.Name) == false) { message += "\n" + item.Name + " crafting component: " + component.Name; missingReference = true; }
                }
                foreach (ItemModel component in item.AcquiredComponents)
                {
                    if (allItemNames.Contains(component.Name) == false) { message += "\n" + item.Name + " acquired component: " + component.Name; missingReference = true; }
                }
                if (allItemNames.Contains(item.EnchantingBaseItem) == false) { message += "\n" + item.Name + " enchanting base item: " + item.EnchantingBaseItem; missingReference = true; }
                foreach (ItemModel rune in item.EnchantingRunes)
                {
                    if (allItemNames.Contains(rune.Name) == false) { message += "\n" + item.Name + " enchanting rune: " + rune.Name; missingReference = true; }
                }
            }
            foreach (CharacterModel character in characterBuilder.Characters)
            {
                foreach (InventoryModel inventory in character.Inventories)
                {
                    foreach (ItemModel item in inventory.AllItems)
                    {
                        if (item.IsCustomItem) { continue; }
                        if (allItemNames.Contains(item.Name) == false) { message += "\n" + character.Name + " " + inventory.Name + " item: " + item.Name; missingReference = true; }
                    }
                }
            }
            foreach (SpellModel spell in spellBuilder.AllSpells)
            {
                foreach (ItemModel item in spell.ConsumedMaterials)
                {
                    if (allItemNames.Contains(item.Name) == false) { message += "\n" + spell.Name + " consumed material: " + item.Name; missingReference = true; }
                }
            }
            foreach (LootBoxModel lootBox in Configuration.MainModelRef.ToolsView.LootBoxes)
            {
                foreach (ItemModel item in lootBox.Items)
                {
                    if (allItemNames.Contains(item.Name) == false) { message += "\n" + lootBox.Name + " loot: " + item.Name; missingReference = true; }
                }
            }
            if (missingReference) { new NotificationDialog(message).ShowDialog(); return false; }
            return true;
        }
        private bool CheckItemRestrictions()
        {
            string message = "Item validation errors:";
            bool issueFound = false;
            List<string> uncraftables = new List<string> { "Rune", "Resource", "Ingredient" };
            foreach (ItemModel item in AllItems)
            {
                if (uncraftables.Contains(item.Type) && (item.IsCraftable || item.CanDismantle || item.CreatableThroughEnchanting) && (item.CraftingComponents.Count() > 0 || item.AcquiredComponents.Count() > 0 || item.EnchantingRunes.Count() > 0)) 
                { 
                    message += "\n" + item.Name + " is not allowed to be crafted, dismantled, or enchanted.";
                    issueFound = true;
                }
            }
            if (issueFound)
            {
                new NotificationDialog(message).ShowDialog();
                return false;
            }
            return true;
        }

    }
}
