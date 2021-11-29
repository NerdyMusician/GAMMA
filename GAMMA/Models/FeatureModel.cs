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
    public class FeatureModel : BaseModel
    {
        // Constructors
        public FeatureModel()
        {
            Name = "New Feature";
            LevelAvailable = 1;
            Names = new()
            {
                "Ability Score Improvement",
                "Base Armor Proficiencies",
                "Base Weapon Proficiencies",
                "Base Tool Proficiencies",
                "Base Saving Throws",
                "Base Skill Proficiencies",
                "Multiclass Ability Prerequisites"
            };
            FeatureTypes = new();
            foreach (FeatureForm form in FeatureForms)
            {
                FeatureTypes.Add(form.Name);
            }
            Choices = new();
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
        #region Names
        private List<string> _Names;
        public List<string> Names
        {
            get
            {
                return _Names;
            }
            set
            {
                _Names = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FeatureType
        private string _FeatureType;
        [XmlSaveMode("Single")]
        public string FeatureType
        {
            get
            {
                return _FeatureType;
            }
            set
            {
                if (value != null || FeatureType != null)
                {
                    if (value != FeatureType && Choices.Count() > 0)
                    {
                        YesNoDialog question = new("Changing the feature type will remove the current choices, continue?");
                        question.ShowDialog();
                        if (question.Answer == false) { return; }
                        Choices.Clear();
                    }
                }
                _FeatureType = value;
                NotifyPropertyChanged();
                if (value == null) { return; }
                FeatureForm ff = FeatureForms.FirstOrDefault(f => f.Name == value);
                if (ff == null) { new NotificationDialog("Missing Feature Form \"" + value + "\"."); return; }
                ShowDetailsField = ff.ShowDetailsField;
                ShowChoiceNumField = ff.ShowChoiceNumField;
                ShowChoiceList = ff.ShowChoiceList;
            }
        }
        #endregion
        #region FeatureTypes
        private List<string> _FeatureTypes;
        public List<string> FeatureTypes
        {
            get
            {
                return _FeatureTypes;
            }
            set
            {
                _FeatureTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LevelAvailable
        private int _LevelAvailable;
        [XmlSaveMode("Single")]
        public int LevelAvailable
        {
            get
            {
                return _LevelAvailable;
            }
            set
            {
                _LevelAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowDetailsField
        private bool _ShowDetailsField;
        public bool ShowDetailsField
        {
            get
            {
                return _ShowDetailsField;
            }
            set
            {
                _ShowDetailsField = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowChoiceNumField
        private bool _ShowChoiceNumField;
        public bool ShowChoiceNumField
        {
            get
            {
                return _ShowChoiceNumField;
            }
            set
            {
                _ShowChoiceNumField = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowChoiceList
        private bool _ShowChoiceList;
        public bool ShowChoiceList
        {
            get
            {
                return _ShowChoiceList;
            }
            set
            {
                _ShowChoiceList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Details
        private string _Details;
        [XmlSaveMode("Single")]
        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ChoicesAllowed
        private int _ChoicesAllowed;
        [XmlSaveMode("Single")]
        public int ChoicesAllowed
        {
            get
            {
                return _ChoicesAllowed;
            }
            set
            {
                _ChoicesAllowed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Choices
        private ObservableCollection<FeatureData> _Choices;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<FeatureData> Choices
        {
            get
            {
                return _Choices;
            }
            set
            {
                _Choices = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Private Properties
        private readonly List<FeatureForm> FeatureForms = new()
        {
            new() { Name = "Ability Score Improvement", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = false },
            new() { Name = "Additional Feat", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = false },
            new() { Name = "Additional Known Cantrips", ShowChoiceList = true, FormNumber = 2 },
            new() { Name = "Armor Proficiencies - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Armor Proficiencies - Set", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = true },
            new() { Name = "Eldritch Invocations Known", ShowChoiceNumField = true },
            new() { Name = "Expanded Spell List (Class)", ShowChoiceList = true },
            new() { Name = "Extra Known Spells - Set", ShowChoiceList = true },
            new() { Name = "Extra Spells Known - Choice (Any)", ShowChoiceNumField = true },
            new() { Name = "Extra Spells Known - Choice (Class)", ShowChoiceNumField = true },
            new() { Name = "Language Proficiencies - Choice", ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Language Proficiencies - Set", ShowChoiceList = true },
            new() { Name = "Multiclass Ability Prerequisite - And", ShowChoiceList = true, FormNumber = 2 },
            new() { Name = "Multiclass Ability Prerequisite - Or", ShowChoiceList = true, FormNumber = 2 },
            new() { Name = "Multiclass Armor Proficiencies - Choice", ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Multiclass Armor Proficiencies - Set", ShowChoiceList = true },
            new() { Name = "Multiclass Language Proficiencies - Choice", ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Multiclass Language Proficiencies - Set", ShowChoiceList = true },
            new() { Name = "Multiclass Skill Proficiencies - Choice", ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Multiclass Skill Proficiencies - Set", ShowChoiceList = true },
            new() { Name = "Multiclass Tool Proficiencies - Choice", ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Multiclass Tool Proficiencies - Set", ShowChoiceList = true },
            new() { Name = "Multiclass Weapon Proficiencies - Choice", ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Multiclass Weapon Proficiencies - Set", ShowChoiceList = true },
            new() { Name = "Saving Throws - Set", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = true },
            new() { Name = "Skill Expertise - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = false },
            new() { Name = "Skill Proficiencies - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Skill Proficiencies - Set", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = true },
            new() { Name = "Stat Bonuses - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = true, FormNumber = 2 },
            new() { Name = "Stat Bonuses - Set", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = true, FormNumber = 2 },
            new() { Name = "Tool Proficiencies - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Tool Proficiencies - Set", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = true },
            new() { Name = "Trait", ShowDetailsField = true, ShowChoiceNumField = false, ShowChoiceList = false },
            new() { Name = "Traits - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = true, FormNumber = 1 },
            new() { Name = "Weapon Proficiencies - Choice", ShowDetailsField = false, ShowChoiceNumField = true, ShowChoiceList = true },
            new() { Name = "Weapon Proficiencies - Set", ShowDetailsField = false, ShowChoiceNumField = false, ShowChoiceList = true },
        };
        private readonly List<string> ArmorProficiencyTypes = new() { "Light Armor", "Medium Armor", "Heavy Armor", "Shields" };
        private readonly List<string> AttributeTypes = new() { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
        private readonly List<string> WeaponProficiencyTypes = new() { "[Simple Weapons]", "[Martial Weapons]" };
        private readonly List<string> SkillProficiencyTypes = new() { "Acrobatics", "Animal Handling", "Arcana", "Athletics", "Deception", "History", "Insight", "Intimidation", "Investigation", "Medicine", "Nature", "Perception", "Performance", "Persuasion", "Religion", "Sleight of Hand", "Stealth", "Survival" };
        private readonly List<string> ToolProficiencyTypes = new() { "Land Vehicles", "Water Vehicles", "[Other Tools]", "[Artisan Tools]", "[Musical Instruments]", "[Gaming Sets]" };
        private readonly List<string> LanguageProficiencyTypes = new() { "[Standard Languages]", "[Exotic Languages]" };
        private readonly List<string> OtherStats = new() { "Armor Class", "Movement Speed", "Hit Point Maximum per Level", "Attribute Choice" };
        

        // Commands
        #region AddChoice
        private RelayCommand _AddChoice;
        public ICommand AddChoice
        {
            get
            {
                if (_AddChoice == null)
                {
                    _AddChoice = new RelayCommand(param => DoAddChoice());
                }
                return _AddChoice;
            }
        }
        private void DoAddChoice()
        {
            if (FeatureTypes.Contains(FeatureType) == false) { new NotificationDialog("Invalid feature type.").ShowDialog(); return; }
            List<ConvertibleValue> options = new();
            switch (FeatureType)
            {
                case "Additional Known Cantrips":
                    foreach (string sc in Configuration.MainModelRef.SpellcastingClasses)
                    {
                        options.Add(new(sc));
                    }
                    break;
                case "Armor Proficiencies - Choice":
                case "Armor Proficiencies - Set":
                case "Multiclass Armor Proficiencies - Set":
                    foreach (string apt in ArmorProficiencyTypes)
                    {
                        options.Add(new(apt));
                    }
                    break;
                case "Extra Known Spells - Set":
                case "Expanded Spell List (Class)":
                    foreach (SpellModel spell in Configuration.SpellRepository.Where(s => s.IsValidated))
                    {
                        options.Add(new(spell.Name));
                    }
                    break;
                case "Language Proficiencies - Choice":
                case "Language Proficiencies - Set":
                    foreach (string lpt in LanguageProficiencyTypes)
                    {
                        options.Add(new(lpt));
                    }
                    foreach (LanguageModel language in Configuration.MainModelRef.ToolsView.Languages)
                    {
                        if (language.IsValidated == false) { continue; }
                        options.Add(new(language.Name));
                    }
                    break;
                case "Requisite Choice":
                    Choices.Add(new() { Name = "NewTrait", Form = FeatureForms.First(f => f.Name == FeatureType).FormNumber });
                    break;
                case "Saving Throws - Choice":
                case "Saving Throws - Set":
                case "Multiclass Ability Prerequisite - And":
                case "Multiclass Ability Prerequisite - Or":
                    foreach (string att in AttributeTypes)
                    {
                        options.Add(new(att));
                    }
                    break;
                case "Skill Proficiencies - Choice":
                case "Skill Proficiencies - Set":
                case "Multiclass Skill Proficiencies - Choice":
                    foreach (string spt in SkillProficiencyTypes)
                    {
                        options.Add(new(spt));
                    }
                    break;
                case "Stat Bonuses - Choice":
                case "Stat Bonuses - Set":
                    foreach (string oth in OtherStats)
                    {
                        options.Add(new(oth));
                    }
                    foreach (string att in AttributeTypes)
                    {
                        options.Add(new(att));
                    }
                    foreach (string spt in SkillProficiencyTypes)
                    {
                        options.Add(new(spt));
                    }
                    break;
                case "Tool Proficiencies - Choice":
                case "Tool Proficiencies - Set":
                case "Multiclass Tool Proficiencies - Choice":
                case "Multiclass Tool Proficiencies - Set":
                    foreach (string tpt in ToolProficiencyTypes)
                    {
                        options.Add(new(tpt));
                    }
                    foreach (ItemModel item in Configuration.ItemRepository)
                    {
                        if (item.IsValidated == false) { continue; }
                        List<string> itemTypes = new() { "Artisan Tool", "Tool", "Gaming Set", "Instrument" };
                        if (itemTypes.Contains(item.Type) == false) { continue; }
                        options.Add(new(item.Name));
                    }
                    break;
                case "Traits - Choice":
                case "Traits - Set":
                    Choices.Add(new() { Name = "NewTrait", Description = "TraitDescription", Form = FeatureForms.First(f => f.Name == FeatureType).FormNumber });
                    return;
                case "Weapon Proficiencies - Choice":
                case "Weapon Proficiencies - Set":
                case "Multiclass Weapon Proficiencies - Set":
                    foreach (string wpt in WeaponProficiencyTypes)
                    {
                        options.Add(new(wpt));
                    }
                    foreach (ItemModel item in Configuration.ItemRepository)
                    {
                        if (item.IsValidated == false) { continue; }
                        if (item.Type.Contains("Melee Weapon") == false && item.Type.Contains("Ranged Weapon") == false) { continue; }
                        options.Add(new(item.Name));
                    }
                    break;
                default:
                    new NotificationDialog("Unhandled feature type \"" + FeatureType + "\"").ShowDialog();
                    return;
            }
            MultiObjectSelectionDialog selectionDialog = new(options, FeatureType.Split('-')[0]);
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ConvertibleValue cv in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCVs)
                {
                    bool existingFound = false;
                    foreach (FeatureData opt in Choices)
                    {
                        if (opt.Name == cv.Value)
                        {
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    Choices.Add(new() { Name = cv.Value, Form = FeatureForms.First(f => f.Name == FeatureType).FormNumber });
                }
                Choices = new(Choices.OrderBy(item => item.Name));
            }
        }
        #endregion
        #region RemoveFeature
        private RelayCommand _RemoveFeature;
        public ICommand RemoveFeature
        {
            get
            {
                if (_RemoveFeature == null)
                {
                    _RemoveFeature = new RelayCommand(DoRemoveFeature);
                }
                return _RemoveFeature;
            }
        }
        private void DoRemoveFeature(object param)
        {
            if (param == null) { new NotificationDialog("No parameter passed for DoRemoveFeature.").ShowDialog(); return; }
            switch (param.ToString())
            {
                case "Active Player Class":
                    Configuration.MainModelRef.ToolsView.ActivePlayerClass.Features.Remove(this);
                    break;
                case "Active Player Subclass":
                    Configuration.MainModelRef.ToolsView.ActivePlayerSubclass.Features.Remove(this);
                    break;
                case "Active Player Race":
                    Configuration.MainModelRef.ToolsView.ActivePlayerRace.Features.Remove(this);
                    break;
                case "Active Player Subrace":
                    Configuration.MainModelRef.ToolsView.ActivePlayerSubrace.Features.Remove(this);
                    break;
                case "Active Player Background":
                    Configuration.MainModelRef.ToolsView.ActivePlayerBackground.Features.Remove(this);
                    break;
                case "Active Player Feat":
                    Configuration.MainModelRef.ToolsView.ActivePlayerFeat.Features.Remove(this);
                    break;
                default:
                    new NotificationDialog("Unhandled parameter \"" + param.ToString() + "\" in DoRemoveFeature.").ShowDialog();
                    return;
            }

        }
        #endregion

        // Private Methods

    }

    [Serializable]
    public class FeatureData : BaseModel
    {
        // Constructors
        public FeatureData()
        {

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
        #region Description
        private string _Description;
        [XmlSaveMode("Single")]
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
        #region Quantity
        private int _Quantity;
        [XmlSaveMode("Single")]
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
            }
        }
        #endregion
        #region Form
        private int _Form;
        [XmlSaveMode("Single")]
        public int Form
        {
            get
            {
                return _Form;
            }
            set
            {
                _Form = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveData
        private RelayCommand _RemoveData;
        public ICommand RemoveData
        {
            get
            {
                if (_RemoveData == null)
                {
                    _RemoveData = new RelayCommand(DoRemoveData);
                }
                return _RemoveData;
            }
        }
        private void DoRemoveData(object param)
        {
            if (param == null) { new NotificationDialog("No parameter passed for DoRemoveData.").ShowDialog(); return; }
            if (param.GetType() == typeof(FeatureModel))
            {
                (param as FeatureModel).Choices.Remove(this);
            }
        }
        #endregion

    }

    [Serializable]
    public class FeatureForm
    {
        // Constructors
        public FeatureForm()
        {

        }

        // Public Methods
        public string Name;
        // Form 0: wrap panel tiles fit to size
        // Form 1: Empty Name and Description field (see Traits - Choice)
        // Form 2: Quantity field and set value (see Stat Bonuses)
        // Form 3: Just editible name field (see Requisite Choice)
        public int FormNumber;
        public bool ShowDetailsField;
        public bool ShowChoiceNumField;
        public bool ShowChoiceList;

    }

    public class Prerequisite : BaseModel
    {
        // Constructors
        public Prerequisite()
        {
            Types = new()
            {
                "Level",
                "Strength Score",
                "Dexterity Score",
                "Constitution Score",
                "Intelligence Score",
                "Wisdom Score",
                "Charisma Score",
                "Other",
            };
        }

        // Databound Properties
        #region Type
        private string _Type;
        [XmlSaveMode("Single")]
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
            }
        }
        #endregion
        #region Types
        private List<string> _Types;
        public List<string> Types
        {
            get
            {
                return _Types;
            }
            set
            {
                _Types = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }

}
