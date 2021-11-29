using GAMMA.Toolbox;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerBuildingBlock : BaseModel
    {
        // Constructors
        public PlayerBuildingBlock()
        {
            Features = new();
            EquipmentChoices = new ObservableCollection<ConvertibleValue>();
            SpellTableRows = new ObservableCollection<SpellTableRowModel>();
            MulticlassingSlotsOptions = new() { "1/1", "1/2", "1/3" };
            MulticlassingSlots = "1/1";
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
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode("Single")]
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode("Single")]
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

        #region EquipmentChoices
        private ObservableCollection<ConvertibleValue> _EquipmentChoices;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ConvertibleValue> EquipmentChoices
        {
            get
            {
                return _EquipmentChoices;
            }
            set
            {
                _EquipmentChoices = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasSpellcasting
        private bool _HasSpellcasting;
        [XmlSaveMode("Single")]
        public bool HasSpellcasting
        {
            get
            {
                return _HasSpellcasting;
            }
            set
            {
                if (value == false && SpellTableRows.Count() > 0)
                {
                    YesNoDialog question = new("Unmarking this will delete the spell table.\nProceed?");
                    question.ShowDialog();
                    if (question.Answer == false) { return; }
                    SpellTableRows = new ObservableCollection<SpellTableRowModel>();
                }
                _HasSpellcasting = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellcastingAbility
        private string _SpellcastingAbility;
        [XmlSaveMode("Single")]
        public string SpellcastingAbility
        {
            get
            {
                return _SpellcastingAbility;
            }
            set
            {
                _SpellcastingAbility = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellsKnownPerLevelType
        private string _SpellsKnownPerLevelType;
        [XmlSaveMode("Single")]
        public string SpellsKnownPerLevelType
        {
            get
            {
                return _SpellsKnownPerLevelType;
            }
            set
            {
                _SpellsKnownPerLevelType = value;
                NotifyPropertyChanged();
                ModifySpellTable();
            }
        }
        #endregion
        #region SpellTableRows
        private ObservableCollection<SpellTableRowModel> _SpellTableRows;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<SpellTableRowModel> SpellTableRows
        {
            get
            {
                return _SpellTableRows;
            }
            set
            {
                _SpellTableRows = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MulticlassingSlotsOptions
        private List<string> _MulticlassingSlotsOptions;
        public List<string> MulticlassingSlotsOptions
        {
            get
            {
                return _MulticlassingSlotsOptions;
            }
            set
            {
                _MulticlassingSlotsOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MulticlassingSlots
        private string _MulticlassingSlots;
        [XmlSaveMode("Single")]
        public string MulticlassingSlots
        {
            get
            {
                return _MulticlassingSlots;
            }
            set
            {
                _MulticlassingSlots = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CanCastRituals
        private bool _CanCastRituals;
        [XmlSaveMode("Single")]
        public bool CanCastRituals
        {
            get
            {
                return _CanCastRituals;
            }
            set
            {
                _CanCastRituals = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Features
        private ObservableCollection<FeatureModel> _Features;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<FeatureModel> Features
        {
            get
            {
                return _Features;
            }
            set
            {
                _Features = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddEquipmentChoice
        private RelayCommand _AddEquipmentChoice;
        public ICommand AddEquipmentChoice
        {
            get
            {
                if (_AddEquipmentChoice == null)
                {
                    _AddEquipmentChoice = new RelayCommand(param => DoAddEquipmentChoice());
                }
                return _AddEquipmentChoice;
            }
        }
        private void DoAddEquipmentChoice()
        {
            EquipmentChoices.Add(new ConvertibleValue("New Equipment Choice"));
        }
        #endregion
        #region AddFeature
        private RelayCommand _AddFeature;
        public ICommand AddFeature
        {
            get
            {
                if (_AddFeature == null)
                {
                    _AddFeature = new RelayCommand(param => DoAddFeature());
                }
                return _AddFeature;
            }
        }
        private void DoAddFeature()
        {
            Features.Add(new());
        }
        #endregion
        #region SortFeatures
        private RelayCommand _SortFeatures;
        public ICommand SortFeatures
        {
            get
            {
                if (_SortFeatures == null)
                {
                    _SortFeatures = new RelayCommand(param => DoSortFeatures());
                }
                return _SortFeatures;
            }
        }
        private void DoSortFeatures()
        {
            Features = new(Features.OrderBy(feature =>
            feature.Name == "Base Armor Proficiencies" ? 1 :
            feature.Name == "Base Weapon Proficiencies" ? 2 :
            feature.Name == "Base Tool Proficiencies" ? 3 :
            feature.Name == "Base Saving Throws" ? 4 :
            feature.Name == "Base Skill Proficiencies" ? 5 :
            feature.Name == "Ability Score Improvement" ? 6 : 7).ThenBy(feature => feature.LevelAvailable).ThenBy(feature => feature.Name));
        }
        #endregion

        // Private Methods
        private void ModifySpellTable()
        {
            if (Configuration.MainModelRef.SpellsKnownPerLevelOptions == null) { return; }
            if (Configuration.MainModelRef.SpellsKnownPerLevelOptions.Contains(SpellsKnownPerLevelType))
            {
                string mode = (SpellsKnownPerLevelType == "Set") ? "Set" : "Calculated";
                if (SpellTableRows.Count() == 0)
                {
                    for (int i = 0; i <= 20; i++)
                    {
                        SpellTableRows.Add(new SpellTableRowModel { ClassLevel = i + 1, SpellsKnownMode = mode });
                    }
                }
                else
                {
                    foreach (SpellTableRowModel spellTableRow in SpellTableRows)
                    {
                        if (spellTableRow.SpellsKnownMode == mode) { break; }
                        spellTableRow.SpellsKnownMode = mode;
                    }
                }
            }
        }

    }
}
