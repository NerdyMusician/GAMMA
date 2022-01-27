using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class ItemModel : BaseModel
    {
        // Constructors
        public ItemModel()
        {
            Type = "Adventuring Gear";
            Name = "New Item";
            ItemTypes = Configuration.ItemTypes.ToList();
            CraftingComponents = new ObservableCollection<ItemModel>();
            AcquiredComponents = new ObservableCollection<ItemModel>();
            EnchantingRunes = new ObservableCollection<ItemModel>();
            StatChanges = new();
        }

        // Databound Properties
        #region Type
        private string _Type;
        [XmlSaveMode(XSME.Single)]
        public string Type
        {
            get => _Type;
            set
            {
                _Type = value;
                NotifyPropertyChanged();
                if (value != "Rune" && value != "Resource" && value != "Ingredient") { ShowCraftingSections = true; } else { ShowCraftingSections = false; }
                if (value == "Alcohol") 
                { 
                    ShowVolume = true; 
                    ShowDrinkButton = true;
                    ShowAlcoholPercentage = true;
                } 
                else 
                { 
                    ShowVolume = false; 
                    ShowDrinkButton = false;
                    ShowAlcoholPercentage = false;
                }
                ShowWeaponInformation = Configuration.WeaponItemTypes.Contains(value);
                ShowArmorInformation = Configuration.ArmorItemTypes.Contains(value);
                ShowFishing = value == "Fish";
                UpdateRawValue();
                UpdateEquipButtons();
            }
        }
        #endregion
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode(XSME.Single)]
        public string Sourcebook
        {
            get => _Sourcebook;
            set => SetAndNotify(ref _Sourcebook, value);
        }
        #endregion
        #region Quantity
        private int _Quantity;
        [XmlSaveMode(XSME.Single)]
        public int Quantity
        {
            get => _Quantity;
            set
            {
                _Quantity = value;
                NotifyPropertyChanged();
                HelperMethods.CheckAndCall_UpdateActiveCharacterInventoryStats();
            }
        }
        #endregion
        #region Weight
        private decimal _Weight;
        [XmlSaveMode(XSME.Single)]
        public decimal Weight
        {
            get => _Weight;
            set
            {
                _Weight = value;
                NotifyPropertyChanged();
                UpdateRawValue();
            }
        }
        #endregion
        #region RawValue
        private int _RawValue;
        [XmlSaveMode(XSME.Single)]
        public int RawValue
        {
            get => _RawValue;
            set
            {
                _RawValue = value;
                NotifyPropertyChanged();
                UpdateProcessedValue();
            }
        }
        #endregion
        #region ProcessedValue
        private string _ProcessedValue;
        public string ProcessedValue
        {
            get => _ProcessedValue;
            set => SetAndNotify(ref _ProcessedValue, value);
        }
        #endregion
        #region Description
        private string _Description;
        [XmlSaveMode(XSME.Single)]
        public string Description
        {
            get => _Description;
            set => SetAndNotify(ref _Description, value);
        }
        #endregion
        #region Lore
        private string _Lore;
        [XmlSaveMode(XSME.Single)]
        public string Lore
        {
            get => _Lore;
            set => SetAndNotify(ref _Lore, value);
        }
        #endregion
        #region InEditMode
        private bool _InEditMode;
        public bool InEditMode
        {
            get => _InEditMode;
            set => SetAndNotify(ref _InEditMode, value);
        }
        #endregion
        #region DropChance
        private int _DropChance;
        [XmlSaveMode(XSME.Single)]
        public int DropChance
        {
            get => _DropChance;
            set => SetAndNotify(ref _DropChance, value);
        }
        #endregion
        #region IsCraftable
        private bool _IsCraftable;
        [XmlSaveMode(XSME.Single)]
        public bool IsCraftable
        {
            get => _IsCraftable;
            set => SetAndNotify(ref _IsCraftable, value);
        }
        #endregion
        #region CanDismantle
        private bool _CanDismantle;
        [XmlSaveMode(XSME.Single)]
        public bool CanDismantle
        {
            get => _CanDismantle;
            set => SetAndNotify(ref _CanDismantle, value);
        }
        #endregion
        #region CreatableThroughEnchanting
        private bool _CreatableThroughEnchanting;
        [XmlSaveMode(XSME.Single)]
        public bool CreatableThroughEnchanting
        {
            get => _CreatableThroughEnchanting;
            set => SetAndNotify(ref _CreatableThroughEnchanting, value);
        }
        #endregion
        #region CraftingToolkit
        private string _CraftingToolkit;
        [XmlSaveMode(XSME.Single)]
        public string CraftingToolkit
        {
            get => _CraftingToolkit;
            set => SetAndNotify(ref _CraftingToolkit, value);
        }
        #endregion
        #region EnchantingBaseItem
        private string _EnchantingBaseItem;
        [XmlSaveMode(XSME.Single)]
        public string EnchantingBaseItem
        {
            get => _EnchantingBaseItem;
            set => SetAndNotify(ref _EnchantingBaseItem, value);
        }
        #endregion
        #region CraftingComponents
        private ObservableCollection<ItemModel> _CraftingComponents;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> CraftingComponents
        {
            get => _CraftingComponents;
            set => SetAndNotify(ref _CraftingComponents, value);
        }
        #endregion
        #region AcquiredComponents
        private ObservableCollection<ItemModel> _AcquiredComponents;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> AcquiredComponents
        {
            get => _AcquiredComponents;
            set => SetAndNotify(ref _AcquiredComponents, value);
        }
        #endregion
        #region EnchantingRunes
        private ObservableCollection<ItemModel> _EnchantingRunes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> EnchantingRunes
        {
            get => _EnchantingRunes;
            set => SetAndNotify(ref _EnchantingRunes, value);
        }
        #endregion
        #region CraftingProgress
        private int _CraftingProgress;
        [XmlSaveMode(XSME.Single)]
        public int CraftingProgress
        {
            get => _CraftingProgress;
            set => SetAndNotify(ref _CraftingProgress, value);
        }
        #endregion
        #region IsCraftingMenuOpen
        private bool _IsCraftingMenuOpen;
        public bool IsCraftingMenuOpen
        {
            get => _IsCraftingMenuOpen;
            set => SetAndNotify(ref _IsCraftingMenuOpen, value);
        }
        #endregion
        #region IsEnchantingMenuOpen
        private bool _IsEnchantingMenuOpen;
        public bool IsEnchantingMenuOpen
        {
            get => _IsEnchantingMenuOpen;
            set => SetAndNotify(ref _IsEnchantingMenuOpen, value);
        }
        #endregion
        #region Environment
        private string _Environment;
        [XmlSaveMode(XSME.Single)]
        public string Environment
        {
            get => _Environment;
            set => SetAndNotify(ref _Environment, value);
        }
        #endregion
        #region CraftingDifficulty
        private int _CraftingDifficulty;
        [XmlSaveMode(XSME.Single)]
        public int CraftingDifficulty
        {
            get => _CraftingDifficulty;
            set => SetAndNotify(ref _CraftingDifficulty, value);
        }
        #endregion
        #region QuantityCrafted
        private int _QuantityCrafted;
        [XmlSaveMode(XSME.Single)]
        public int QuantityCrafted
        {
            get => _QuantityCrafted;
            set => SetAndNotify(ref _QuantityCrafted, value);
        }
        #endregion
        #region IsCustomItem
        private bool _IsCustomItem;
        [XmlSaveMode(XSME.Single)]
        public bool IsCustomItem
        {
            get => _IsCustomItem;
            set => SetAndNotify(ref _IsCustomItem, value);
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get => _IsValidated;
            set => SetAndNotify(ref _IsValidated, value);
        }
        #endregion
        #region ShowCraftingSections
        private bool _ShowCraftingSections;
        public bool ShowCraftingSections
        {
            get => _ShowCraftingSections;
            set => SetAndNotify(ref _ShowCraftingSections, value);
        }
        #endregion
        #region ShowVolume
        private bool _ShowVolume;
        public bool ShowVolume
        {
            get => _ShowVolume;
            set => SetAndNotify(ref _ShowVolume, value);
        }
        #endregion
        #region ShowDrinkButton
        private bool _ShowDrinkButton;
        public bool ShowDrinkButton
        {
            get => _ShowDrinkButton;
            set => SetAndNotify(ref _ShowDrinkButton, value);
        }
        #endregion
        #region ShowAlcoholPercentage
        private bool _ShowAlcoholPercentage;
        public bool ShowAlcoholPercentage
        {
            get => _ShowAlcoholPercentage;
            set => SetAndNotify(ref _ShowAlcoholPercentage, value);
        }
        #endregion
        #region AlcoholPercentage
        private int _AlcoholPercentage;
        [XmlSaveMode(XSME.Single)]
        public int AlcoholPercentage
        {
            get => _AlcoholPercentage;
            set
            {
                _AlcoholPercentage = value;
                NotifyPropertyChanged();
                AlcoholDc = GetDcFromAlcoholPercentage();
            }
        }
        #endregion
        #region AlcoholDc
        private int _AlcoholDc;
        public int AlcoholDc
        {
            get => _AlcoholDc;
            set => SetAndNotify(ref _AlcoholDc, value);
        }
        #endregion
        #region VolumeSize
        private string _VolumeSize;
        [XmlSaveMode(XSME.Single)]
        public string VolumeSize
        {
            get => _VolumeSize;
            set
            {
                _VolumeSize = value;
                NotifyPropertyChanged();
                MaxVolume = value switch
                {

                    "Barrel (40 gal)" => 1280,
                    "Keg (5 gal)" => 160,
                    "Pitcher (1 gal)" => 32,
                    "Bottle (1.5 pint)" => 6,
                    "Glass or Flask (1 pint)" => 4,
                    "Gill (1/4 pint)" => 1,
                    _ => 0
                };
                Weight = value switch
                {
                    "Barrel (40 gal)" => 400m,
                    "Keg (5 gal)" => 60m,
                    "Pitcher (1 gal)" => 10m,
                    "Bottle (1.5 pint)" => 4m,
                    "Glass or Flask (1 pint)" => 2m,
                    "Gill (1/4 pint)" => 1m,
                    _ => Weight
                };
            }
        }
        #endregion
        #region CurrentVolume
        private int _CurrentVolume;
        [XmlSaveMode(XSME.Single)]
        public int CurrentVolume
        {
            get => _CurrentVolume;
            set => SetAndNotify(ref _CurrentVolume, value);
        }
        #endregion
        #region MaxVolume
        private int _MaxVolume;
        [XmlSaveMode(XSME.Single)]
        public int MaxVolume
        {
            get => _MaxVolume;
            set => SetAndNotify(ref _MaxVolume, value);
        }
        #endregion
        #region DrinkVolStr
        private string _DrinkVolStr;
        [XmlSaveMode(XSME.Single)]
        public string DrinkVolStr
        {
            get => _DrinkVolStr;
            set
            {
                _DrinkVolStr = value;
                NotifyPropertyChanged();
                DrinkVolume = value switch
                {
                    "Glass or Flask (1 pint)" => 4,
                    "Gill (1/4 pint)" => 1,
                    _ => 0
                };
            }
        }
        #endregion
        #region DrinkVolume
        private int _DrinkVolume;
        public int DrinkVolume
        {
            get => _DrinkVolume;
            set => SetAndNotify(ref _DrinkVolume, value);
        }
        #endregion
        #region ShowFishing
        private bool _ShowFishing;
        public bool ShowFishing
        {
            get => _ShowFishing;
            set => SetAndNotify(ref _ShowFishing, value);
        }
        #endregion
        #region FishingEnvironment
        private string _FishingEnvironment;
        [XmlSaveMode(XSME.Single)]
        public string FishingEnvironment
        {
            get => _FishingEnvironment;
            set => SetAndNotify(ref _FishingEnvironment, value);
        }
        #endregion
        #region Rarity
        private string _Rarity;
        [XmlSaveMode(XSME.Single)]
        public string Rarity
        {
            get => _Rarity;
            set
            {
                _Rarity = value;
                NotifyPropertyChanged();
                UpdateRawValue();
            }
        }
        #endregion
        

        #region ShowEquipButton_Hand
        private bool _ShowEquipButton_Hand;
        public bool ShowEquipButton_Hand
        {
            get => _ShowEquipButton_Hand;
            set => SetAndNotify(ref _ShowEquipButton_Hand, value);
        }
        #endregion
        #region ShowEquipButton_Armor
        private bool _ShowEquipButton_Armor;
        public bool ShowEquipButton_Armor
        {
            get => _ShowEquipButton_Armor;
            set => SetAndNotify(ref _ShowEquipButton_Armor, value);
        }
        #endregion
        #region ShowEquipButton_Accessory
        private bool _ShowEquipButton_Accessory;
        public bool ShowEquipButton_Accessory
        {
            get => _ShowEquipButton_Accessory;
            set => SetAndNotify(ref _ShowEquipButton_Accessory, value);
        }
        #endregion

        // Databound Properties - Magic Information
        #region IsMagic
        private bool _IsMagic;
        [XmlSaveMode(XSME.Single)]
        public bool IsMagic
        {
            get => _IsMagic;
            set => SetAndNotify(ref _IsMagic, value);
        }
        #endregion
        #region RequiresAttunement
        private bool _RequiresAttunement;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresAttunement
        {
            get => _RequiresAttunement;
            set => SetAndNotify(ref _RequiresAttunement, value);
        }
        #endregion
        #region StatChanges
        private ObservableCollection<LabeledNumber> _StatChanges;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<LabeledNumber> StatChanges
        {
            get => _StatChanges;
            set => SetAndNotify(ref _StatChanges, value);
        }
        #endregion

        // Databound Properties - Armor Information
        #region ShowArmorInformation
        private bool _ShowArmorInformation;
        public bool ShowArmorInformation
        {
            get => _ShowArmorInformation;
            set => SetAndNotify(ref _ShowArmorInformation, value);
        }
        #endregion
        #region BaseArmorClass
        private int _BaseArmorClass;
        [XmlSaveMode(XSME.Single)]
        public int BaseArmorClass
        {
            get => _BaseArmorClass;
            set => SetAndNotify(ref _BaseArmorClass, value);
        }
        #endregion
        #region StrengthRequirement
        private int _StrengthRequirement;
        [XmlSaveMode(XSME.Single)]
        public int StrengthRequirement
        {
            get => _StrengthRequirement;
            set => SetAndNotify(ref _StrengthRequirement, value);
        }
        #endregion
        #region GivesStealthDisadvantage
        private bool _GivesStealthDisadvantage;
        [XmlSaveMode(XSME.Single)]
        public bool GivesStealthDisadvantage
        {
            get => _GivesStealthDisadvantage;
            set => SetAndNotify(ref _GivesStealthDisadvantage, value);
        }
        #endregion

        // Databound Properties - Weapon Information
        #region ShowWeaponInformation
        private bool _ShowWeaponInformation;
        public bool ShowWeaponInformation
        {
            get => _ShowWeaponInformation;
            set => SetAndNotify(ref _ShowWeaponInformation, value);
        }
        #endregion
        #region DamageDiceQuantity
        private int _DamageDiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int DamageDiceQuantity
        {
            get => _DamageDiceQuantity;
            set => SetAndNotify(ref _DamageDiceQuantity, value);
        }
        #endregion
        #region DamageDiceQuality
        private int _DamageDiceQuality;
        [XmlSaveMode(XSME.Single)]
        public int DamageDiceQuality
        {
            get => _DamageDiceQuality;
            set => SetAndNotify(ref _DamageDiceQuality, value);
        }
        #endregion
        #region DamageType
        private string _DamageType;
        [XmlSaveMode(XSME.Single)]
        public string DamageType
        {
            get => _DamageType;
            set => SetAndNotify(ref _DamageType, value);
        }
        #endregion
        #region RequiresAmmunition
        private bool _RequiresAmmunition;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresAmmunition
        {
            get => _RequiresAmmunition;
            set => SetAndNotify(ref _RequiresAmmunition, value);
        }
        #endregion
        #region IsFinesse
        private bool _IsFinesse;
        [XmlSaveMode(XSME.Single)]
        public bool IsFinesse
        {
            get => _IsFinesse;
            set => SetAndNotify(ref _IsFinesse, value);
        }
        #endregion
        #region IsHeavy
        private bool _IsHeavy;
        [XmlSaveMode(XSME.Single)]
        public bool IsHeavy
        {
            get => _IsHeavy;
            set => SetAndNotify(ref _IsHeavy, value);
        }
        #endregion
        #region IsLight
        private bool _IsLight;
        [XmlSaveMode(XSME.Single)]
        public bool IsLight
        {
            get => _IsLight;
            set => SetAndNotify(ref _IsLight, value);
        }
        #endregion
        #region IsLoading
        private bool _IsLoading;
        [XmlSaveMode(XSME.Single)]
        public bool IsLoading
        {
            get => _IsLoading;
            set => SetAndNotify(ref _IsLoading, value);
        }
        #endregion
        #region HasRange
        private bool _HasRange;
        [XmlSaveMode(XSME.Single)]
        public bool HasRange
        {
            get => _HasRange;
            set => SetAndNotify(ref _HasRange, value);
        }
        #endregion
        #region NormalRange
        private int _NormalRange;
        [XmlSaveMode(XSME.Single)]
        public int NormalRange
        {
            get => _NormalRange;
            set => SetAndNotify(ref _NormalRange, value);
        }
        #endregion
        #region LongRange
        private int _LongRange;
        [XmlSaveMode(XSME.Single)]
        public int LongRange
        {
            get => _LongRange;
            set => SetAndNotify(ref _LongRange, value);
        }
        #endregion
        #region HasReach
        private bool _HasReach;
        [XmlSaveMode(XSME.Single)]
        public bool HasReach
        {
            get => _HasReach;
            set => SetAndNotify(ref _HasReach, value);
        }
        #endregion
        #region CanBeThrown
        private bool _CanBeThrown;
        [XmlSaveMode(XSME.Single)]
        public bool CanBeThrown
        {
            get => _CanBeThrown;
            set => SetAndNotify(ref _CanBeThrown, value);
        }
        #endregion
        #region NormalThrowRange
        private int _NormalThrowRange;
        [XmlSaveMode(XSME.Single)]
        public int NormalThrowRange
        {
            get => _NormalThrowRange;
            set => SetAndNotify(ref _NormalThrowRange, value);
        }
        #endregion
        #region LongThrowRange
        private int _LongThrowRange;
        [XmlSaveMode(XSME.Single)]
        public int LongThrowRange
        {
            get => _LongThrowRange;
            set => SetAndNotify(ref _LongThrowRange, value);
        }
        #endregion
        #region IsTwoHanded
        private bool _IsTwoHanded;
        [XmlSaveMode(XSME.Single)]
        public bool IsTwoHanded
        {
            get => _IsTwoHanded;
            set => SetAndNotify(ref _IsTwoHanded, value);
        }
        #endregion
        #region IsVersatile
        private bool _IsVersatile;
        [XmlSaveMode(XSME.Single)]
        public bool IsVersatile
        {
            get => _IsVersatile;
            set => SetAndNotify(ref _IsVersatile, value);
        }
        #endregion
        #region VersatileDiceQuantity
        private int _VersatileDiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int VersatileDiceQuantity
        {
            get => _VersatileDiceQuantity;
            set => SetAndNotify(ref _VersatileDiceQuantity, value);
        }
        #endregion
        #region VersatileDiceQuality
        private int _VersatileDiceQuality;
        [XmlSaveMode(XSME.Single)]
        public int VersatileDiceQuality
        {
            get => _VersatileDiceQuality;
            set => SetAndNotify(ref _VersatileDiceQuality, value);
        }
        #endregion
        #region HitDamageBonus
        private int _HitDamageBonus;
        [XmlSaveMode(XSME.Single)]
        public int HitDamageBonus
        {
            get => _HitDamageBonus;
            set => SetAndNotify(ref _HitDamageBonus, value);
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region ItemTypes
        private List<string> _ItemTypes;
        public List<string> ItemTypes
        {
            get => _ItemTypes;
            set => SetAndNotify(ref _ItemTypes, value);
        }
        #endregion

        // Commands
        #region DuplicateItem
        public ICommand DuplicateItem => new RelayCommand(param => DoDuplicateItem());
        private void DoDuplicateItem()
        {
            ItemModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ItemBuilderView.AllItems.Insert(Configuration.MainModelRef.ItemBuilderView.AllItems.IndexOf(this), duplicate);
            Configuration.MainModelRef.ItemBuilderView.FilteredItems.Insert(Configuration.MainModelRef.ItemBuilderView.FilteredItems.IndexOf(this), duplicate);
            Configuration.MainModelRef.ItemBuilderView.ActiveItem = duplicate;
        }
        #endregion
        #region DeleteItem
        public ICommand DeleteItem => new RelayCommand(param => DoDeleteItem());
        private void DoDeleteItem()
        {
            ItemBuilderViewModel itemBuilder = Configuration.MainModelRef.ItemBuilderView;
            if (CheckItemDependencies() == false) { return; }
            itemBuilder.AllItems.Remove(this);
            itemBuilder.FilteredItems.Remove(this);
        }
        #endregion
        #region RemoveLootFromBox
        public ICommand RemoveLootFromBox => new RelayCommand(param => DoRemoveLootFromBox());
        private void DoRemoveLootFromBox()
        {
            Configuration.MainModelRef.ToolsView.ActiveLootBox.Items.Remove(this);
        }
        #endregion
        #region RemoveItem
        public ICommand RemoveItem => new RelayCommand(DoRemoveItem);
        private void DoRemoveItem(object param)
        {
            if (param == null) { return; }
            string removeFrom = param.ToString();
            switch (removeFrom)
            {
                case "ActivePlayerBackground.MandatoryEquipment":
                    Configuration.MainModelRef.ToolsView.ActivePlayerBackground.MandatoryEquipment.Remove(this);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region AddCraftingComponent
        public ICommand AddCraftingComponent => new RelayCommand(param => DoAddCraftingComponent());
        private void DoAddCraftingComponent()
        {
            if (Configuration.ItemTypes.Contains(Type) == false)
            {
                HelperMethods.NotifyUser("Please select a valid item type.");
                return;
            }

            MultiObjectSelectionDialog selectionDialog = Type switch
            {
                "Potion" => new MultiObjectSelectionDialog(Configuration.ItemRepository.Where(item => item.IsValidated && item.Type == "Ingredient").ToList()),
                _ => new MultiObjectSelectionDialog(Configuration.ItemRepository.Where(item => item.IsValidated && item.Type == "Resource").ToList())
            };

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    ItemModel itemToAdd = HelperMethods.DeepClone(item);
                    foreach (ItemModel component in CraftingComponents)
                    {
                        if (component.Name == itemToAdd.Name)
                        {
                            component.Quantity += itemToAdd.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    CraftingComponents.Add(itemToAdd);
                }
            }

        }
        #endregion
        #region AddAcquiredComponent
        public ICommand AddAcquiredComponent => new RelayCommand(param => DoAddAcquiredComponent());
        private void DoAddAcquiredComponent()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated && item.Type == "Resource").ToList());

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    ItemModel itemToAdd = HelperMethods.DeepClone(item);
                    foreach (ItemModel component in AcquiredComponents)
                    {
                        if (component.Name == itemToAdd.Name)
                        {
                            component.Quantity += itemToAdd.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    AcquiredComponents.Add(itemToAdd);
                }
            }

        }
        #endregion
        #region AddEnchantingRune
        public ICommand AddEnchantingRune => new RelayCommand(param => DoAddEnchantingRune());
        private void DoAddEnchantingRune()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated && item.Type == "Rune").ToList());

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    ItemModel itemToAdd = HelperMethods.DeepClone(item);
                    foreach (ItemModel rune in EnchantingRunes)
                    {
                        if (rune.Name == itemToAdd.Name)
                        {
                            rune.Quantity += itemToAdd.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    EnchantingRunes.Add(itemToAdd);
                }
            }

        }
        #endregion
        #region SelectToolkit
        public ICommand SelectToolkit => new RelayCommand(param => DoSelectToolkit());
        private void DoSelectToolkit()
        {
            ObjectSelectionDialog itemSelect = new(Configuration.ItemRepository.Where(item => item.Type == "Tool" && item.IsValidated).ToList());
            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                CraftingToolkit = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel).Name;

            }
        }
        #endregion
        #region SetEnchantingBaseItem
        public ICommand SetEnchantingBaseItem => new RelayCommand(param => DoSetEnchantingBaseItem());
        private void DoSetEnchantingBaseItem()
        {
            ObjectSelectionDialog itemSelect = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                EnchantingBaseItem = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel).Name;

            }
        }
        #endregion
        #region RemoveCraftingComponent
        public ICommand RemoveCraftingComponent => new RelayCommand(param => DoRemoveCraftingComponent());
        private void DoRemoveCraftingComponent()
        {
            Configuration.MainModelRef.ItemBuilderView.ActiveItem.CraftingComponents.Remove(this);
        }
        #endregion
        #region RemoveAcquiredComponent
        public ICommand RemoveAcquiredComponent => new RelayCommand(param => DoRemoveAcquiredComponent());
        private void DoRemoveAcquiredComponent()
        {
            Configuration.MainModelRef.ItemBuilderView.ActiveItem.AcquiredComponents.Remove(this);
        }
        #endregion
        #region RemoveEnchantingRune
        public ICommand RemoveEnchantingRune => new RelayCommand(param => DoRemoveEnchantingRune());
        private void DoRemoveEnchantingRune()
        {
            Configuration.MainModelRef.ItemBuilderView.ActiveItem.EnchantingRunes.Remove(this);
        }
        #endregion
        #region RemoveConsumedMaterial
        public ICommand RemoveConsumedMaterial => new RelayCommand(param => DoRemoveConsumedMaterial());
        private void DoRemoveConsumedMaterial()
        {
            Configuration.MainModelRef.SpellBuilderView.ActiveSpell.ConsumedMaterials.Remove(this);
        }
        #endregion
        #region WorkOnCrafting
        public ICommand WorkOnCrafting => new RelayCommand(DoWorkOnCrafting);
        private void DoWorkOnCrafting(object timePeriod)
        {
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            bool hasCraftingTool = false;

            foreach (ItemModel packItem in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories[0].AllItems)
            {
                if (packItem.Name == CraftingToolkit) { hasCraftingTool = true; }
            }

            if (hasCraftingTool == false)
            {
                HelperMethods.NotifyUser("Missing crafting tool: " + CraftingToolkit);
                return;
            }

            int.TryParse(timePeriod.ToString(), out int intTime);
            int mod = (character.IntelligenceModifier > character.WisdomModifier) ? character.IntelligenceModifier : character.WisdomModifier;
            int profBonus = (character.ToolProficiencies.FirstOrDefault(item => item.Name == CraftingToolkit) != null) ? character.ProficiencyBonus : 0;
            int total = 0;
            for (int i = 0; i < intTime; i++)
            {
                total += ((HelperMethods.RollD20() + mod + profBonus) * 15);
                int roll = HelperMethods.RollD20();
            }
            CraftingProgress += total;

            string message = character.Name + " worked on " + Name + " for " + timePeriod + ((intTime > 1) ? " hours." : " hour.");
            message += "\nResult: +" + total + "cp";
            message += "\nProgress: " + CraftingProgress + "/" + RawValue;
            if (CraftingProgress > RawValue) 
            {
                ItemModel itemToAdd = HelperMethods.DeepClone(this);
                itemToAdd.Quantity = QuantityCrafted;
                character.Inventories[0].AllItems.Add(this);
                character.Inventories[0].FilteredItems.Add(this);
                character.CraftingBench.Remove(this);
                HelperMethods.NotifyUser(Name + " has been completed and moved to backpack.");
                return;
            }
            HelperMethods.AddToPlayerLog(message);

            IsCraftingMenuOpen = false;

        }
        #endregion
        #region WorkOnEnchanting
        public ICommand WorkOnEnchanting => new RelayCommand(DoWorkOnEnchanting);
        private void DoWorkOnEnchanting(object param)
        {
            bool hasEnchantingTool = false;
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;

            foreach (ItemModel packItem in character.Inventories[0].AllItems)
            {
                if (packItem.Name == "Imbuing Lens") { hasEnchantingTool = true; }
            }

            if (hasEnchantingTool == false)
            {
                HelperMethods.NotifyUser("Missing Imbuing Lens");
                return;
            }

            int spellLevel = Convert.ToInt32(param);
            if (HelperMethods.CheckForSpellSlots(ref character, spellLevel) == false)
            {
                HelperMethods.NotifyUser("Insufficient spell slot.");
                return;
            }

            int total = ((HelperMethods.RollD20() + character.SpellAbilityModifier) * (spellLevel * 40));
            CraftingProgress += total;

            string message = character.Name + " imbued " + Name + " with level " + spellLevel + " spell slot.";
            message += "\nResult: +" + total + "cp";
            message += "\nProgress: " + CraftingProgress + "/" + RawValue;

            if (CraftingProgress > RawValue)
            {
                ItemModel itemToAdd = HelperMethods.DeepClone(this);
                itemToAdd.Quantity = QuantityCrafted;
                character.Inventories[0].AllItems.Add(itemToAdd);
                character.Inventories[0].FilteredItems.Add(itemToAdd);
                character.EnchantingTable.Remove(this);
                HelperMethods.NotifyUser(Name + " has been completed and moved to backpack.");
                return;
            }

            HelperMethods.AddToPlayerLog(message);

        }
        #endregion
        #region DismantleItem
        public ICommand DismantleItem => new RelayCommand(DoDismantleItem);
        private void DoDismantleItem(object param)
        {
            InventoryModel sourceInventory = param as InventoryModel;
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;

            if (character.IsInCarriedItems(CraftingToolkit, 1) == false)
            {
                HelperMethods.NotifyUser("Missing tool: " + CraftingToolkit);
                return;
            }

            foreach (ItemModel item in AcquiredComponents)
            {
                bool existingFound = false;
                ItemModel itemToAdd = HelperMethods.DeepClone(item);
                foreach (ItemModel bpItem in sourceInventory.AllItems)
                {
                    if (itemToAdd.Name == bpItem.Name)
                    {
                        bpItem.Quantity += itemToAdd.Quantity;
                        existingFound = true;
                        break;
                    }
                }

                if (existingFound) { continue; }
                sourceInventory.AllItems.Add(itemToAdd);
                sourceInventory.AllItems.Last().PropertyChanged += character.DndPlayerModel_PropertyChanged;
                sourceInventory.FilteredItems.Add(sourceInventory.AllItems.Last());

            }

            Quantity--;
            if (Quantity == 0)
            {
                sourceInventory.AllItems.Remove(this);
                sourceInventory.FilteredItems.Remove(this);
            }
            sourceInventory.UpdateFilteredList();

        }
        #endregion
        #region OfferItemToSell
        public ICommand OfferItemToSell => new RelayCommand(DoOfferItemToSell);
        private void DoOfferItemToSell(object quantity)
        {
            int qty = Convert.ToInt32(quantity);
            if (Quantity <= 0) { return; }
            if ((Quantity - qty) < 0) { qty = Quantity; }
            Quantity -= qty;

            bool itemFound = false;

            foreach (ItemModel item in Configuration.ShopRef.CharacterOfferedItems)
            {
                if (Name == item.Name) 
                {
                    item.Quantity += qty;
                    itemFound = true;
                    break; 
                }
            }

            if (itemFound == false)
            {
                Configuration.ShopRef.CharacterOfferedItems.Add(HelperMethods.DeepClone(this));
                Configuration.ShopRef.CharacterOfferedItems.Last().Quantity = qty;
            }

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopItemMove);
            Configuration.ShopRef.UpdateTransactionValue();

        }
        #endregion
        #region RetractItemToSell
        public ICommand RetractItemToSell => new RelayCommand(DoRetractItemToSell);
        public void DoRetractItemToSell(object quantity)
        {
            int qty = Convert.ToInt32(quantity);
            if ((Quantity - qty) < 0) { qty = Quantity; }

            Quantity -= qty;

            foreach (ItemModel item in Configuration.ShopRef.CharacterItems)
            {
                if (Name == item.Name)
                {
                    item.Quantity += qty;
                    break;
                }
            }

            if (Quantity == 0)
            {
                Configuration.ShopRef.CharacterOfferedItems.Remove(this);
            }

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopItemMove);
            Configuration.ShopRef.UpdateTransactionValue();

        }
        #endregion
        #region OfferItemToBuy
        public ICommand OfferItemToBuy => new RelayCommand(DoOfferItemToBuy);
        private void DoOfferItemToBuy(object quantity)
        {
            int qty = Convert.ToInt32(quantity);

            bool itemFound = false;

            foreach (ItemModel item in Configuration.ShopRef.ShopOfferedItems)
            {
                if (Name == item.Name)
                {
                    item.Quantity += qty;
                    itemFound = true;
                    break;
                }
            }

            if (itemFound == false)
            {
                Configuration.ShopRef.ShopOfferedItems.Add(HelperMethods.DeepClone(this));
                Configuration.ShopRef.ShopOfferedItems.Last().Quantity = qty;
            }

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopItemMove);
            Configuration.ShopRef.UpdateTransactionValue();

        }
        #endregion
        #region RetractItemToBuy
        public ICommand RetractItemToBuy => new RelayCommand(DoRetractItemToBuy);
        private void DoRetractItemToBuy(object quantity)
        {
            int qty = Convert.ToInt32(quantity);
            if ((Quantity - qty) < 0) { qty = Quantity; }

            Quantity -= qty;

            foreach (ItemModel item in Configuration.ShopRef.ShopItems)
            {
                if (Name == item.Name)
                {
                    item.Quantity += qty;
                    break;
                }
            }

            if (Quantity == 0)
            {
                Configuration.ShopRef.ShopOfferedItems.Remove(this);
            }

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_ShopItemMove);
            Configuration.ShopRef.UpdateTransactionValue();

        }
        #endregion
        #region ToolRoll
        public ICommand ToolRoll => new RelayCommand(DoToolRoll);
        private void DoToolRoll(object param)
        {
            int toolRoll;
            int toolTotal;
            int toolMod;
            bool hasProf;
            CharacterModel refChar;
            string message = "";

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            toolRoll = HelperMethods.RollD20((param.ToString() == "Advantage"), (param.ToString() == "Disadvantage"));
            refChar = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            hasProf = false;
            foreach (ItemModel tool in refChar.ToolProficiencies)
            {
                if (Name == tool.Name)
                {
                    hasProf = true;
                    break;
                }
            }
            toolMod = (hasProf) ? refChar.ProficiencyBonus : 0;
            toolTotal = toolRoll + toolMod;
            message += refChar.Name + " used " + Name + ((param.ToString() == "Advantage") ? " with advantage." : ((param.ToString() == "Disadvantage") ? " with disadvantage." : "."));
            message += "\nResult: " + toolTotal;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nRoll: [" + toolRoll + "] + " + toolMod; }
            HelperMethods.AddToPlayerLog(message, "Default", true);
            
        }
        #endregion
        #region RemoveFromPlayerToolProf
        public ICommand RemoveFromPlayerToolProf => new RelayCommand(param => DoRemoveFromPlayerToolProf());
        private void DoRemoveFromPlayerToolProf()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ToolProficiencies.Remove(this);
        }
        #endregion
        #region Drink
        public ICommand Drink => new RelayCommand(DoDrink);
        private void DoDrink(object param)
        {
            if (param == null) { return; }
            if (param.ToString() != "ShopBypass")
            {
                AdjustVolume(DrinkVolume);
            }

            CharacterModel player = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (param.ToString() != "PourOneOutForTheHomies")
            {
                
                int roll = HelperMethods.RollD20();
                int result = roll + player.ConstitutionModifier;
                string message = player.Name + " took a drink of " + Name + ".";
                if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                {
                    message += "\n" + "ABV " + AlcoholPercentage + "%, DC " + AlcoholDc;
                    message += "\nRoll: [" + roll + "] + " + player.ConstitutionModifier;
                }
                if (result < AlcoholDc)
                {
                    player.IntoxicationLevel++;
                    string intoxicationStatus = GetIntoxicationStatusFromLevel(player.IntoxicationLevel);
                    message += "\n" + player.Name + " is now " + intoxicationStatus + ".";
                }
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }
            else
            {
                HelperMethods.AddToPlayerLog(player.Name + " poured a drink of " + Name + ".", "Default", true);
            }

            if (param.ToString() == "ShopBypass") { return; }
            if (Quantity == 0 && CurrentVolume == 0)
            {
                InventoryModel inventory = param as InventoryModel;
                inventory.AllItems.Remove(this);
                inventory.UpdateFilteredList();
            }
        }
        #endregion
        #region ResetVolume
        public ICommand ResetVolume => new RelayCommand(param => DoResetVolume());
        private void DoResetVolume()
        {
            CurrentVolume = MaxVolume;
        }
        #endregion
        #region Transfer
        public ICommand Transfer => new RelayCommand(DoTransfer);
        private void DoTransfer(object param)
        {
            InventoryModel sourceInventory = param as InventoryModel;
            List<InventoryModel> inventories = new();
            foreach (InventoryModel inventory in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Inventories)
            {
                if (inventory != sourceInventory)
                {
                    inventories.Add(inventory);
                }
            }
            ObjectSelectionDialog transferTargetSelection = new(inventories, Quantity);
            if (transferTargetSelection.ShowDialog() == true)
            {
                if (transferTargetSelection.SelectedObject == null) { return; }
                int qty = (Quantity < transferTargetSelection.Quantity) ? Quantity : transferTargetSelection.Quantity;
                InventoryModel targetInventory = transferTargetSelection.SelectedObject as InventoryModel;
                bool matchFound = false;
                foreach (ItemModel item in targetInventory.AllItems)
                {
                    if (item.Name == Name)
                    {
                        item.Quantity += qty;
                        matchFound = true;
                        break;
                    }
                }
                if (matchFound == false)
                {
                    ItemModel itemCopy = HelperMethods.DeepClone(this);
                    itemCopy.Quantity = qty;
                    targetInventory.AllItems.Add(itemCopy);
                }
                Quantity -= qty;
                if (Quantity == 0)
                {
                    sourceInventory.AllItems.Remove(this);
                    sourceInventory.FilteredItems.Remove(this);
                }
            }
        }
        #endregion
        #region RemoveFromInventory
        public ICommand RemoveFromInventory => new RelayCommand(DoRemoveFromInventory);
        private void DoRemoveFromInventory(object param)
        {
            if (param == null) { return; }
            InventoryModel inventory = param as InventoryModel;
            inventory.AllItems.Remove(this);
            inventory.UpdateFilteredList();
        }
        #endregion
        #region AdjustQuantity
        public ICommand AdjustQuantity => new RelayCommand(DoAdjustQuantity);
        private void DoAdjustQuantity(object param)
        {
            if (param == null) { return; }
            bool validQty = int.TryParse(param.ToString(), out int qty);
            if (validQty == false) { return; }
            Quantity += qty;
            if (Quantity < 0) { Quantity = 0; }
        }
        #endregion
        #region EquipToCharacter
        public ICommand EquipToCharacter => new RelayCommand(DoEquipToCharacter);
        private void DoEquipToCharacter(object param)
        {
            if (param == null) { return; }
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (param.ToString() == "MainHand")
            {
                character.MainHandItem = Name;
                HelperMethods.NotifyUser(Name + " equipped to main hand.");
            }
            if (param.ToString() == "OffHand")
            {
                character.OffHandItem = Name;
                HelperMethods.NotifyUser(Name + " equipped to off hand.");
            }
            if (param.ToString() == "Armor")
            {
                character.ArmorItem = Name;
                HelperMethods.NotifyUser(Name + " equipped as armor.");
            }
            if (param.ToString() == "Accessory")
            {
                bool duplicate = false;
                foreach (ItemLink link in character.EquippedAccessories)
                {
                    if (link.Name == Name)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate == false)
                {
                    character.EquippedAccessories.Add(new() { Name = Name, LinkedItem = this });
                    HelperMethods.NotifyUser(Name + " equipped as an accessory.");
                }
                else
                {
                    HelperMethods.NotifyUser("This accessory is already equipped.");
                }
            }
        }
        #endregion
        #region AttuneToCharacter
        public ICommand AttuneToCharacter => new RelayCommand(DoAttuneToCharacter);
        private void DoAttuneToCharacter(object param)
        {
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (character.AttunedLinkedItemA == null) { character.AttunedItemA = Name; HelperMethods.NotifyUser(Name + " attuned to slot A."); return; }
            if (character.AttunedLinkedItemB == null) { character.AttunedItemB = Name; HelperMethods.NotifyUser(Name + " attuned to slot B."); return; }
            if (character.AttunedLinkedItemC == null) { character.AttunedItemC = Name; HelperMethods.NotifyUser(Name + " attuned to slot C."); return; }
            HelperMethods.NotifyUser("No available attunement slots.");
        }
        #endregion
        #region AddStatChange
        public ICommand AddStatChange => new RelayCommand(DoAddStatChange);
        private void DoAddStatChange(object param)
        {
            StatChanges.Add(new(Configuration.AlterantStats));
        }
        #endregion

        // Private Methods
        private void UpdateProcessedValue()
        {
            ProcessedValue = HelperMethods.GetDerivedCoinage(RawValue);
        }
        private bool CheckItemDependencies()
        {
            ItemBuilderViewModel itemBuilder = Configuration.MainModelRef.ItemBuilderView;
            CreatureBuilderViewModel creatureBuilder = Configuration.MainModelRef.CreatureBuilderView;
            CharacterBuilderViewModel characterBuilder = Configuration.MainModelRef.CharacterBuilderView;
            SpellBuilderViewModel spellBuilder = Configuration.MainModelRef.SpellBuilderView;
            List<string> foundDependencies = new();
            foreach (ItemModel item in itemBuilder.AllItems)
            {
                foreach (ItemModel component in item.CraftingComponents)
                {
                    if (Name == component.Name) { foundDependencies.Add("Crafting component for " + item.Name); }
                }
                foreach (ItemModel component in item.AcquiredComponents)
                {
                    if (Name == component.Name) { foundDependencies.Add("Acquired component from " + item.Name); }
                }
                if (item.EnchantingBaseItem == Name) { foundDependencies.Add("Enchanting base item for " + item.Name); }
                foreach (ItemModel component in item.EnchantingRunes)
                {
                    if (Name == component.Name) { foundDependencies.Add("Enchanting rune for " + item.Name); }
                }
            }
            foreach (CharacterModel character in characterBuilder.Characters)
            {
                foreach (InventoryModel inventory in character.Inventories)
                {
                    foreach (ItemModel item in inventory.AllItems)
                    {
                        if (Name == item.Name) { foundDependencies.Add(inventory.Name + " inventory of " + character.Name); }
                    }
                }
            }
            foreach (SpellModel spell in spellBuilder.AllSpells)
            {
                foreach (ItemModel item in spell.ConsumedMaterials)
                {
                    if (Name == item.Name) { foundDependencies.Add("Consumed material for " + spell.Name); }
                }
            }
            foreach (LootBoxModel lootBox in Configuration.MainModelRef.ToolsView.LootBoxes)
            {
                foreach (ItemModel item in lootBox.Items)
                {
                    if (Name == item.Name) { foundDependencies.Add("Loot item for " + lootBox.Name); }
                }
            }
            foreach (CreatureModel creature in creatureBuilder.AllCreatures)
            {
                foreach (ItemLink item in creature.ItemLinks)
                {
                    if (Name == item.Name) { foundDependencies.Add("Loot item for " + creature.Name); }
                }
            }
            if (foundDependencies.Count > 0) 
            {
                string message = "Unable to delete " + Name + ":\n" + HelperMethods.GetStringFromList(foundDependencies, "\n");
                HelperMethods.NotifyUser(message); 
                return false; 
            }
            return true;
        }
        private void AdjustVolume(int vol)
        {
            int nextVol = CurrentVolume - vol;
            if (nextVol <= 0)
            {
                Quantity--;
                if (Quantity >= 1)
                {
                    CurrentVolume = MaxVolume + nextVol;
                }
                else
                {
                    CurrentVolume = 0;
                }
            }
            else
            {
                CurrentVolume = nextVol;
            }
        }
        private int GetDcFromAlcoholPercentage()
        {
            return AlcoholPercentage switch
            {
                int n when (n >= 100) => 30,
                int n when (n <= 99 && n >= 90) => 28,
                int n when (n <= 89 && n >= 80) => 26,
                int n when (n <= 79 && n >= 70) => 24,
                int n when (n <= 69 && n >= 60) => 22,
                int n when (n <= 59 && n >= 50) => 20,
                int n when (n <= 49 && n >= 40) => 18,
                int n when (n <= 39 && n >= 30) => 16,
                int n when (n <= 29 && n >= 20) => 14,
                int n when (n <= 19 && n >= 10) => 12,
                int n when (n <= 9 && n >= 5) => 10,
                _ => 8
            };
        }
        private static string GetIntoxicationStatusFromLevel(int toxLevel)
        {
            return toxLevel switch
            {
                0 => "Sober",
                1 => "Buzzed",
                2 => "Lightly Intoxicated",
                3 => "Moderately Intoxicated",
                4 => "Heavily Intoxicated",
                5 => "Dangerously Intoxicated",
                _ => "Dead"
            };
        }
        private void UpdateRawValue()
        {
            if (Type == "Fish")
            {
                RawValue = Rarity switch
                {
                    "Common" => Convert.ToInt32(Weight * 2),
                    "Uncommon" => Convert.ToInt32(Weight * 4),
                    "Rare" => Convert.ToInt32(Weight * 8),
                    "Very Rare" => Convert.ToInt32(Weight * 15),
                    "Legendary" => Convert.ToInt32(Weight * 30),
                    _ => 0
                };
            }
        }
        private void UpdateEquipButtons()
        {
            List<string> handEquipTypes = new() { "Arcane Focus", "Firearms Ranged Weapon", "Instrument", "Shield", "Magic Weapon", "Simple Melee Weapon", "Simple Ranged Weapon", "Martial Melee Weapon", "Martial Ranged Weapon", "Wondrous Item" };
            List<string> armorEquipTypes = new() { "Clothing", "Light Armor", "Medium Armor", "Heavy Armor", "Wondrous Item" };
            List<string> accessoryEquipTypes = new() { "Clothing", "Holy Symbol", "Jewelry", "Wondrous Item" };

            ShowEquipButton_Hand = handEquipTypes.Contains(Type);
            ShowEquipButton_Armor = armorEquipTypes.Contains(Type);
            ShowEquipButton_Accessory = accessoryEquipTypes.Contains(Type);

        }
        

    }
}
