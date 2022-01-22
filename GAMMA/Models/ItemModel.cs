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
            get
            {
                return _Type;
            }
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode(XSME.Single)]
        public string Sourcebook
        {
            get
            {
                return _Sourcebook;
            }
            set
            {
                _Sourcebook = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Quantity
        private int _Quantity;
        [XmlSaveMode(XSME.Single)]
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
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
            get
            {
                return _Weight;
            }
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
            get
            {
                return _RawValue;
            }
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
            get
            {
                return _ProcessedValue;
            }
            set
            {
                _ProcessedValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Description
        private string _Description;
        [XmlSaveMode(XSME.Single)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Lore
        private string _Lore;
        [XmlSaveMode(XSME.Single)]
        public string Lore
        {
            get
            {
                return _Lore;
            }
            set
            {
                _Lore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InEditMode
        private bool _InEditMode;
        public bool InEditMode
        {
            get
            {
                return _InEditMode;
            }
            set
            {
                _InEditMode = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DropChance
        private int _DropChance;
        [XmlSaveMode(XSME.Single)]
        public int DropChance
        {
            get
            {
                return _DropChance;
            }
            set
            {
                _DropChance = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsCraftable
        private bool _IsCraftable;
        [XmlSaveMode(XSME.Single)]
        public bool IsCraftable
        {
            get
            {
                return _IsCraftable;
            }
            set
            {
                _IsCraftable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CanDismantle
        private bool _CanDismantle;
        [XmlSaveMode(XSME.Single)]
        public bool CanDismantle
        {
            get
            {
                return _CanDismantle;
            }
            set
            {
                _CanDismantle = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CreatableThroughEnchanting
        private bool _CreatableThroughEnchanting;
        [XmlSaveMode(XSME.Single)]
        public bool CreatableThroughEnchanting
        {
            get
            {
                return _CreatableThroughEnchanting;
            }
            set
            {
                _CreatableThroughEnchanting = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CraftingToolkit
        private string _CraftingToolkit;
        [XmlSaveMode(XSME.Single)]
        public string CraftingToolkit
        {
            get
            {
                return _CraftingToolkit;
            }
            set
            {
                _CraftingToolkit = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region EnchantingBaseItem
        private string _EnchantingBaseItem;
        [XmlSaveMode(XSME.Single)]
        public string EnchantingBaseItem
        {
            get
            {
                return _EnchantingBaseItem;
            }
            set
            {
                _EnchantingBaseItem = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CraftingComponents
        private ObservableCollection<ItemModel> _CraftingComponents;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> CraftingComponents
        {
            get
            {
                return _CraftingComponents;
            }
            set
            {
                _CraftingComponents = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AcquiredComponents
        private ObservableCollection<ItemModel> _AcquiredComponents;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> AcquiredComponents
        {
            get
            {
                return _AcquiredComponents;
            }
            set
            {
                _AcquiredComponents = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region EnchantingRunes
        private ObservableCollection<ItemModel> _EnchantingRunes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> EnchantingRunes
        {
            get
            {
                return _EnchantingRunes;
            }
            set
            {
                _EnchantingRunes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CraftingProgress
        private int _CraftingProgress;
        [XmlSaveMode(XSME.Single)]
        public int CraftingProgress
        {
            get
            {
                return _CraftingProgress;
            }
            set
            {
                _CraftingProgress = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsCraftingMenuOpen
        private bool _IsCraftingMenuOpen;
        public bool IsCraftingMenuOpen
        {
            get
            {
                return _IsCraftingMenuOpen;
            }
            set
            {
                _IsCraftingMenuOpen = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsEnchantingMenuOpen
        private bool _IsEnchantingMenuOpen;
        public bool IsEnchantingMenuOpen
        {
            get
            {
                return _IsEnchantingMenuOpen;
            }
            set
            {
                _IsEnchantingMenuOpen = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Environment
        private string _Environment;
        [XmlSaveMode(XSME.Single)]
        public string Environment
        {
            get
            {
                return _Environment;
            }
            set
            {
                _Environment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CraftingDifficulty
        private int _CraftingDifficulty;
        [XmlSaveMode(XSME.Single)]
        public int CraftingDifficulty
        {
            get
            {
                return _CraftingDifficulty;
            }
            set
            {
                _CraftingDifficulty = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region QuantityCrafted
        private int _QuantityCrafted;
        [XmlSaveMode(XSME.Single)]
        public int QuantityCrafted
        {
            get
            {
                return _QuantityCrafted;
            }
            set
            {
                _QuantityCrafted = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsCustomItem
        private bool _IsCustomItem;
        [XmlSaveMode(XSME.Single)]
        public bool IsCustomItem
        {
            get
            {
                return _IsCustomItem;
            }
            set
            {
                _IsCustomItem = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get
            {
                return _IsValidated;
            }
            set
            {
                _IsValidated = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowCraftingSections
        private bool _ShowCraftingSections;
        public bool ShowCraftingSections
        {
            get
            {
                return _ShowCraftingSections;
            }
            set
            {
                _ShowCraftingSections = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowVolume
        private bool _ShowVolume;
        public bool ShowVolume
        {
            get
            {
                return _ShowVolume;
            }
            set
            {
                _ShowVolume = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowDrinkButton
        private bool _ShowDrinkButton;
        public bool ShowDrinkButton
        {
            get
            {
                return _ShowDrinkButton;
            }
            set
            {
                _ShowDrinkButton = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowAlcoholPercentage
        private bool _ShowAlcoholPercentage;
        public bool ShowAlcoholPercentage
        {
            get
            {
                return _ShowAlcoholPercentage;
            }
            set
            {
                _ShowAlcoholPercentage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AlcoholPercentage
        private int _AlcoholPercentage;
        [XmlSaveMode(XSME.Single)]
        public int AlcoholPercentage
        {
            get
            {
                return _AlcoholPercentage;
            }
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
            get
            {
                return _AlcoholDc;
            }
            set
            {
                _AlcoholDc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region VolumeSize
        private string _VolumeSize;
        [XmlSaveMode(XSME.Single)]
        public string VolumeSize
        {
            get
            {
                return _VolumeSize;
            }
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
            get
            {
                return _CurrentVolume;
            }
            set
            {
                _CurrentVolume = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MaxVolume
        private int _MaxVolume;
        [XmlSaveMode(XSME.Single)]
        public int MaxVolume
        {
            get
            {
                return _MaxVolume;
            }
            set
            {
                _MaxVolume = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DrinkVolStr
        private string _DrinkVolStr;
        [XmlSaveMode(XSME.Single)]
        public string DrinkVolStr
        {
            get
            {
                return _DrinkVolStr;
            }
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
            get
            {
                return _DrinkVolume;
            }
            set
            {
                _DrinkVolume = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowFishing
        private bool _ShowFishing;
        public bool ShowFishing
        {
            get
            {
                return _ShowFishing;
            }
            set
            {
                _ShowFishing = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FishingEnvironment
        private string _FishingEnvironment;
        [XmlSaveMode(XSME.Single)]
        public string FishingEnvironment
        {
            get
            {
                return _FishingEnvironment;
            }
            set
            {
                _FishingEnvironment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Rarity
        private string _Rarity;
        [XmlSaveMode(XSME.Single)]
        public string Rarity
        {
            get
            {
                return _Rarity;
            }
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
            get
            {
                return _ShowEquipButton_Hand;
            }
            set
            {
                _ShowEquipButton_Hand = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowEquipButton_Armor
        private bool _ShowEquipButton_Armor;
        
        public bool ShowEquipButton_Armor
        {
            get
            {
                return _ShowEquipButton_Armor;
            }
            set
            {
                _ShowEquipButton_Armor = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowEquipButton_Accessory
        private bool _ShowEquipButton_Accessory;
        
        public bool ShowEquipButton_Accessory
        {
            get
            {
                return _ShowEquipButton_Accessory;
            }
            set
            {
                _ShowEquipButton_Accessory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Magic Information
        #region IsMagic
        private bool _IsMagic;
        [XmlSaveMode(XSME.Single)]
        public bool IsMagic
        {
            get
            {
                return _IsMagic;
            }
            set
            {
                _IsMagic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresAttunement
        private bool _RequiresAttunement;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresAttunement
        {
            get
            {
                return _RequiresAttunement;
            }
            set
            {
                _RequiresAttunement = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StatChanges
        private ObservableCollection<LabeledNumber> _StatChanges;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<LabeledNumber> StatChanges
        {
            get
            {
                return _StatChanges;
            }
            set
            {
                _StatChanges = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Armor Information
        #region ShowArmorInformation
        private bool _ShowArmorInformation;
        
        public bool ShowArmorInformation
        {
            get
            {
                return _ShowArmorInformation;
            }
            set
            {
                _ShowArmorInformation = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region BaseArmorClass
        private int _BaseArmorClass;
        [XmlSaveMode(XSME.Single)]
        public int BaseArmorClass
        {
            get
            {
                return _BaseArmorClass;
            }
            set
            {
                _BaseArmorClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StrengthRequirement
        private int _StrengthRequirement;
        [XmlSaveMode(XSME.Single)]
        public int StrengthRequirement
        {
            get
            {
                return _StrengthRequirement;
            }
            set
            {
                _StrengthRequirement = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region GivesStealthDisadvantage
        private bool _GivesStealthDisadvantage;
        [XmlSaveMode(XSME.Single)]
        public bool GivesStealthDisadvantage
        {
            get
            {
                return _GivesStealthDisadvantage;
            }
            set
            {
                _GivesStealthDisadvantage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Weapon Information
        #region ShowWeaponInformation
        private bool _ShowWeaponInformation;
        
        public bool ShowWeaponInformation
        {
            get
            {
                return _ShowWeaponInformation;
            }
            set
            {
                _ShowWeaponInformation = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageDiceQuantity
        private int _DamageDiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int DamageDiceQuantity
        {
            get
            {
                return _DamageDiceQuantity;
            }
            set
            {
                _DamageDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageDiceQuality
        private int _DamageDiceQuality;
        [XmlSaveMode(XSME.Single)]
        public int DamageDiceQuality
        {
            get
            {
                return _DamageDiceQuality;
            }
            set
            {
                _DamageDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageType
        private string _DamageType;
        [XmlSaveMode(XSME.Single)]
        public string DamageType
        {
            get
            {
                return _DamageType;
            }
            set
            {
                _DamageType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresAmmunition
        private bool _RequiresAmmunition;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresAmmunition
        {
            get
            {
                return _RequiresAmmunition;
            }
            set
            {
                _RequiresAmmunition = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsFinesse
        private bool _IsFinesse;
        [XmlSaveMode(XSME.Single)]
        public bool IsFinesse
        {
            get
            {
                return _IsFinesse;
            }
            set
            {
                _IsFinesse = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsHeavy
        private bool _IsHeavy;
        [XmlSaveMode(XSME.Single)]
        public bool IsHeavy
        {
            get
            {
                return _IsHeavy;
            }
            set
            {
                _IsHeavy = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsLight
        private bool _IsLight;
        [XmlSaveMode(XSME.Single)]
        public bool IsLight
        {
            get
            {
                return _IsLight;
            }
            set
            {
                _IsLight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsLoading
        private bool _IsLoading;
        [XmlSaveMode(XSME.Single)]
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasRange
        private bool _HasRange;
        [XmlSaveMode(XSME.Single)]
        public bool HasRange
        {
            get
            {
                return _HasRange;
            }
            set
            {
                _HasRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NormalRange
        private int _NormalRange;
        [XmlSaveMode(XSME.Single)]
        public int NormalRange
        {
            get
            {
                return _NormalRange;
            }
            set
            {
                _NormalRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LongRange
        private int _LongRange;
        [XmlSaveMode(XSME.Single)]
        public int LongRange
        {
            get
            {
                return _LongRange;
            }
            set
            {
                _LongRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasReach
        private bool _HasReach;
        [XmlSaveMode(XSME.Single)]
        public bool HasReach
        {
            get
            {
                return _HasReach;
            }
            set
            {
                _HasReach = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CanBeThrown
        private bool _CanBeThrown;
        [XmlSaveMode(XSME.Single)]
        public bool CanBeThrown
        {
            get
            {
                return _CanBeThrown;
            }
            set
            {
                _CanBeThrown = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NormalThrowRange
        private int _NormalThrowRange;
        [XmlSaveMode(XSME.Single)]
        public int NormalThrowRange
        {
            get
            {
                return _NormalThrowRange;
            }
            set
            {
                _NormalThrowRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LongThrowRange
        private int _LongThrowRange;
        [XmlSaveMode(XSME.Single)]
        public int LongThrowRange
        {
            get
            {
                return _LongThrowRange;
            }
            set
            {
                _LongThrowRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsTwoHanded
        private bool _IsTwoHanded;
        [XmlSaveMode(XSME.Single)]
        public bool IsTwoHanded
        {
            get
            {
                return _IsTwoHanded;
            }
            set
            {
                _IsTwoHanded = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsVersatile
        private bool _IsVersatile;
        [XmlSaveMode(XSME.Single)]
        public bool IsVersatile
        {
            get
            {
                return _IsVersatile;
            }
            set
            {
                _IsVersatile = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region VersatileDiceQuantity
        private int _VersatileDiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int VersatileDiceQuantity
        {
            get
            {
                return _VersatileDiceQuantity;
            }
            set
            {
                _VersatileDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region VersatileDiceQuality
        private int _VersatileDiceQuality;
        [XmlSaveMode(XSME.Single)]
        public int VersatileDiceQuality
        {
            get
            {
                return _VersatileDiceQuality;
            }
            set
            {
                _VersatileDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HitDamageBonus
        private int _HitDamageBonus;
        [XmlSaveMode(XSME.Single)]
        public int HitDamageBonus
        {
            get
            {
                return _HitDamageBonus;
            }
            set
            {
                _HitDamageBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region ItemTypes
        private List<string> _ItemTypes;
        public List<string> ItemTypes
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
        #region DuplicateItem
        public ICommand DuplicateItem
        {
            get
            {
                return new RelayCommand(param => DoDuplicateItem());
            }
        }
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
        public ICommand DeleteItem
        {
            get
            {
                return new RelayCommand(param => DoDeleteItem());
            }
        }
        private void DoDeleteItem()
        {
            ItemBuilderViewModel itemBuilder = Configuration.MainModelRef.ItemBuilderView;
            if (CheckItemDependencies() == false) { return; }
            itemBuilder.AllItems.Remove(this);
            itemBuilder.FilteredItems.Remove(this);
        }
        #endregion
        #region RemoveLootFromBox
        private RelayCommand _RemoveLootFromBox;
        public ICommand RemoveLootFromBox
        {
            get
            {
                if (_RemoveLootFromBox == null)
                {
                    _RemoveLootFromBox = new RelayCommand(param => DoRemoveLootFromBox());
                }
                return _RemoveLootFromBox;
            }
        }
        private void DoRemoveLootFromBox()
        {
            Configuration.MainModelRef.ToolsView.ActiveLootBox.Items.Remove(this);
        }
        #endregion
        #region RemoveItem
        private RelayCommand _RemoveItem;
        public ICommand RemoveItem
        {
            get
            {
                if (_RemoveItem == null)
                {
                    _RemoveItem = new RelayCommand(DoRemoveItem);
                }
                return _RemoveItem;
            }
        }
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
        private RelayCommand _AddCraftingComponent;
        public ICommand AddCraftingComponent
        {
            get
            {
                if (_AddCraftingComponent == null)
                {
                    _AddCraftingComponent = new RelayCommand(param => DoAddCraftingComponent());
                }
                return _AddCraftingComponent;
            }
        }
        private void DoAddCraftingComponent()
        {
            if (Configuration.ItemTypes.Contains(Type) == false)
            {
                new NotificationDialog("Please select a valid item type.").ShowDialog();
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
        private RelayCommand _AddAcquiredComponent;
        public ICommand AddAcquiredComponent
        {
            get
            {
                if (_AddAcquiredComponent == null)
                {
                    _AddAcquiredComponent = new RelayCommand(param => DoAddAcquiredComponent());
                }
                return _AddAcquiredComponent;
            }
        }
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
        private RelayCommand _AddEnchantingRune;
        public ICommand AddEnchantingRune
        {
            get
            {
                if (_AddEnchantingRune == null)
                {
                    _AddEnchantingRune = new RelayCommand(param => DoAddEnchantingRune());
                }
                return _AddEnchantingRune;
            }
        }
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
        private RelayCommand _SelectToolkit;
        public ICommand SelectToolkit
        {
            get
            {
                if (_SelectToolkit == null)
                {
                    _SelectToolkit = new RelayCommand(param => DoSelectToolkit());
                }
                return _SelectToolkit;
            }
        }
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
        private RelayCommand _SetEnchantingBaseItem;
        public ICommand SetEnchantingBaseItem
        {
            get
            {
                if (_SetEnchantingBaseItem == null)
                {
                    _SetEnchantingBaseItem = new RelayCommand(param => DoSetEnchantingBaseItem());
                }
                return _SetEnchantingBaseItem;
            }
        }
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
        private RelayCommand _RemoveCraftingComponent;
        public ICommand RemoveCraftingComponent
        {
            get
            {
                if (_RemoveCraftingComponent == null)
                {
                    _RemoveCraftingComponent = new RelayCommand(param => DoRemoveCraftingComponent());
                }
                return _RemoveCraftingComponent;
            }
        }
        private void DoRemoveCraftingComponent()
        {
            Configuration.MainModelRef.ItemBuilderView.ActiveItem.CraftingComponents.Remove(this);
        }
        #endregion
        #region RemoveAcquiredComponent
        private RelayCommand _RemoveAcquiredComponent;
        public ICommand RemoveAcquiredComponent
        {
            get
            {
                if (_RemoveAcquiredComponent == null)
                {
                    _RemoveAcquiredComponent = new RelayCommand(param => DoRemoveAcquiredComponent());
                }
                return _RemoveAcquiredComponent;
            }
        }
        private void DoRemoveAcquiredComponent()
        {
            Configuration.MainModelRef.ItemBuilderView.ActiveItem.AcquiredComponents.Remove(this);
        }
        #endregion
        #region RemoveEnchantingRune
        private RelayCommand _RemoveEnchantingRune;
        public ICommand RemoveEnchantingRune
        {
            get
            {
                if (_RemoveEnchantingRune == null)
                {
                    _RemoveEnchantingRune = new RelayCommand(param => DoRemoveEnchantingRune());
                }
                return _RemoveEnchantingRune;
            }
        }
        private void DoRemoveEnchantingRune()
        {
            Configuration.MainModelRef.ItemBuilderView.ActiveItem.EnchantingRunes.Remove(this);
        }
        #endregion
        #region RemoveConsumedMaterial
        private RelayCommand _RemoveConsumedMaterial;
        public ICommand RemoveConsumedMaterial
        {
            get
            {
                if (_RemoveConsumedMaterial == null)
                {
                    _RemoveConsumedMaterial = new RelayCommand(param => DoRemoveConsumedMaterial());
                }
                return _RemoveConsumedMaterial;
            }
        }
        private void DoRemoveConsumedMaterial()
        {
            Configuration.MainModelRef.SpellBuilderView.ActiveSpell.ConsumedMaterials.Remove(this);
        }
        #endregion
        #region WorkOnCrafting
        private RelayCommand _WorkOnCrafting;
        public ICommand WorkOnCrafting
        {
            get
            {
                if (_WorkOnCrafting == null)
                {
                    _WorkOnCrafting = new RelayCommand(DoWorkOnCrafting);
                }
                return _WorkOnCrafting;
            }
        }
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
                new NotificationDialog(Name + " has been completed and moved to backpack.").ShowDialog();
                return;
            }
            HelperMethods.AddToPlayerLog(message);

            IsCraftingMenuOpen = false;

        }
        #endregion
        #region WorkOnEnchanting
        private RelayCommand _WorkOnEnchanting;
        public ICommand WorkOnEnchanting
        {
            get
            {
                if (_WorkOnEnchanting == null)
                {
                    _WorkOnEnchanting = new RelayCommand(DoWorkOnEnchanting);
                }
                return _WorkOnEnchanting;
            }
        }
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
                new NotificationDialog("Missing Imbuing Lens").ShowDialog();
                return;
            }

            int spellLevel = Convert.ToInt32(param);
            if (HelperMethods.CheckForSpellSlots(ref character, spellLevel) == false)
            {
                new NotificationDialog("Insufficient spell slot.").ShowDialog();
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
                new NotificationDialog(Name + " has been completed and moved to backpack.").ShowDialog();
                return;
            }

            HelperMethods.AddToPlayerLog(message);

        }
        #endregion
        #region DismantleItem
        private RelayCommand _DismantleItem;
        public ICommand DismantleItem
        {
            get
            {
                if (_DismantleItem == null)
                {
                    _DismantleItem = new RelayCommand(DoDismantleItem);
                }
                return _DismantleItem;
            }
        }
        private void DoDismantleItem(object param)
        {
            InventoryModel sourceInventory = param as InventoryModel;
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;

            if (character.IsInCarriedItems(CraftingToolkit, 1) == false)
            {
                new NotificationDialog("Missing tool: " + CraftingToolkit).ShowDialog();
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
        private RelayCommand _OfferItemToSell;
        public ICommand OfferItemToSell
        {
            get
            {
                if (_OfferItemToSell == null)
                {
                    _OfferItemToSell = new RelayCommand(DoOfferItemToSell);
                }
                return _OfferItemToSell;
            }
        }
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
        private RelayCommand _RetractItemToSell;
        public ICommand RetractItemToSell
        {
            get
            {
                if (_RetractItemToSell == null)
                {
                    _RetractItemToSell = new RelayCommand(DoRetractItemToSell);
                }
                return _RetractItemToSell;
            }
        }
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
        private RelayCommand _OfferItemToBuy;
        public ICommand OfferItemToBuy
        {
            get
            {
                if (_OfferItemToBuy == null)
                {
                    _OfferItemToBuy = new RelayCommand(DoOfferItemToBuy);
                }
                return _OfferItemToBuy;
            }
        }
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
        private RelayCommand _RetractItemToBuy;
        public ICommand RetractItemToBuy
        {
            get
            {
                if (_RetractItemToBuy == null)
                {
                    _RetractItemToBuy = new RelayCommand(DoRetractItemToBuy);
                }
                return _RetractItemToBuy;
            }
        }
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
        private RelayCommand _ToolRoll;
        public ICommand ToolRoll
        {
            get
            {
                if (_ToolRoll == null)
                {
                    _ToolRoll = new RelayCommand(DoToolRoll);
                }
                return _ToolRoll;
            }
        }
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
        private RelayCommand _RemoveFromPlayerToolProf;
        public ICommand RemoveFromPlayerToolProf
        {
            get
            {
                if (_RemoveFromPlayerToolProf == null)
                {
                    _RemoveFromPlayerToolProf = new RelayCommand(param => DoRemoveFromPlayerToolProf());
                }
                return _RemoveFromPlayerToolProf;
            }
        }
        private void DoRemoveFromPlayerToolProf()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ToolProficiencies.Remove(this);
        }
        #endregion
        #region Drink
        private RelayCommand _Drink;
        public ICommand Drink
        {
            get
            {
                if (_Drink == null)
                {
                    _Drink = new RelayCommand(DoDrink);
                }
                return _Drink;
            }
        }
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
        private RelayCommand _ResetVolume;
        public ICommand ResetVolume
        {
            get
            {
                if (_ResetVolume == null)
                {
                    _ResetVolume = new RelayCommand(param => DoResetVolume());
                }
                return _ResetVolume;
            }
        }
        private void DoResetVolume()
        {
            CurrentVolume = MaxVolume;
        }
        #endregion
        #region Transfer
        private RelayCommand _Transfer;
        public ICommand Transfer
        {
            get
            {
                if (_Transfer == null)
                {
                    _Transfer = new RelayCommand(DoTransfer);
                }
                return _Transfer;
            }
        }
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
        private RelayCommand _RemoveFromInventory;
        public ICommand RemoveFromInventory
        {
            get
            {
                if (_RemoveFromInventory == null)
                {
                    _RemoveFromInventory = new RelayCommand(DoRemoveFromInventory);
                }
                return _RemoveFromInventory;
            }
        }
        private void DoRemoveFromInventory(object param)
        {
            if (param == null) { return; }
            InventoryModel inventory = param as InventoryModel;
            inventory.AllItems.Remove(this);
            inventory.UpdateFilteredList();
        }
        #endregion
        #region AdjustQuantity
        private RelayCommand _AdjustQuantity;
        public ICommand AdjustQuantity
        {
            get
            {
                if (_AdjustQuantity == null)
                {
                    _AdjustQuantity = new RelayCommand(DoAdjustQuantity);
                }
                return _AdjustQuantity;
            }
        }
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

        // Public Methods
        

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
