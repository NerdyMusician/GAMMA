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
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode(XSME.Single)]
        public string Sourcebook
        {
            get => _Sourcebook;
            set => SetAndNotify(ref _Sourcebook, value);
        }
        #endregion

        #region EquipmentChoices
        private ObservableCollection<ConvertibleValue> _EquipmentChoices;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> EquipmentChoices
        {
            get => _EquipmentChoices;
            set => SetAndNotify(ref _EquipmentChoices, value);
        }
        #endregion

        #region HasSpellcasting
        private bool _HasSpellcasting;
        [XmlSaveMode(XSME.Single)]
        public bool HasSpellcasting
        {
            get => _HasSpellcasting;
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
        [XmlSaveMode(XSME.Single)]
        public string SpellcastingAbility
        {
            get => _SpellcastingAbility;
            set => SetAndNotify(ref _SpellcastingAbility, value);
        }
        #endregion
        #region SpellsKnownPerLevelType
        private string _SpellsKnownPerLevelType;
        [XmlSaveMode(XSME.Single)]
        public string SpellsKnownPerLevelType
        {
            get => _SpellsKnownPerLevelType;
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
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<SpellTableRowModel> SpellTableRows
        {
            get => _SpellTableRows;
            set => SetAndNotify(ref _SpellTableRows, value);
        }
        #endregion
        #region MulticlassingSlotsOptions
        private List<string> _MulticlassingSlotsOptions;
        public List<string> MulticlassingSlotsOptions
        {
            get => _MulticlassingSlotsOptions;
            set => SetAndNotify(ref _MulticlassingSlotsOptions, value);
        }
        #endregion
        #region MulticlassingSlots
        private string _MulticlassingSlots;
        [XmlSaveMode(XSME.Single)]
        public string MulticlassingSlots
        {
            get => _MulticlassingSlots;
            set => SetAndNotify(ref _MulticlassingSlots, value);
        }
        #endregion
        #region CanCastRituals
        private bool _CanCastRituals;
        [XmlSaveMode(XSME.Single)]
        public bool CanCastRituals
        {
            get => _CanCastRituals;
            set => SetAndNotify(ref _CanCastRituals, value);
        }
        #endregion

        #region Features
        private ObservableCollection<FeatureModel> _Features;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<FeatureModel> Features
        {
            get => _Features;
            set => SetAndNotify(ref _Features, value);
        }
        #endregion

        // Commands
        #region AddEquipmentChoice
        public ICommand AddEquipmentChoice => new RelayCommand(param => DoAddEquipmentChoice());
        private void DoAddEquipmentChoice()
        {
            EquipmentChoices.Add(new ConvertibleValue("New Equipment Choice"));
        }
        #endregion
        #region AddFeature
        public ICommand AddFeature => new RelayCommand(param => DoAddFeature());
        private void DoAddFeature()
        {
            Features.Add(new());
        }
        #endregion
        #region SortFeatures
        public ICommand SortFeatures => new RelayCommand(param => DoSortFeatures());
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
