using ExtensionMethods;
using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.Models
{
    [Serializable]
    public class CharacterModel : EntityModel
    {
        // Constructors
        public CharacterModel()
        {
            Name = "New Character";
            Subraces = new();
            ActionHistory = new();
            Messages = new();
            PlayerClasses = new();
            Abilities = new();
            Alterants = new();
            ActiveEffectAbilities = new();
            SpellLinks = new();
            Counters = new();
            Traits = new();
            CraftingBench = new();
            EnchantingTable = new();
            CreaturePen = new();
            Notes = new();
            Minions = new();
            CustomDiceSets = new();
            _DeathSaves = 0;
            _DeathFails = 0;

            Inventories = new();

            ToolProficiencies = new();
            ToolsInInventory = new();
            HitDiceSets = new();
            EquippedAccessories = new();

            ShowActionHistory = true;

            // Character Creation
            CCAttributeSets = new();
            SetLanguageProfs = new();
            SetSkillProfs = new();
            SetTraits = new();
            SetToolProfs = new();
            SkillChoiceSegments = new();
            ExpertiseChoiceSegments = new();
            AttributeFeatChoices = new();
            LanguageChoiceSegments = new();
            ToolChoiceSegments = new();
            WeaponChoiceSegments = new();
            ArmorChoiceSegments = new();
            FeatChoices = new();
            StartingEquipmentChoiceSets = new();
            ChosenEquipment = new();
            GrantedEquipment = new();
            TraitChoiceSegments = new();
            StatBonusChoiceSegments = new();
            SpellChoiceSegments = new();
            StrengthBaseScore = 8;
            DexterityBaseScore = 8;
            ConstitutionBaseScore = 8;
            IntelligenceBaseScore = 8;
            WisdomBaseScore = 8;
            CharismaBaseScore = 8;
            BaseAttributePoints = 27;

        }

        // Databound Properties - Base Info
        #region DisplayCharacterCreationWarning
        private bool _DisplayCharacterCreationWarning;
        public bool DisplayCharacterCreationWarning
        {
            get => _DisplayCharacterCreationWarning;
            set => SetAndNotify(ref _DisplayCharacterCreationWarning, value);
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
        #region Icon
        private string _Icon;
        [XmlSaveMode(XSME.Single)]
        public string Icon
        {
            get => _Icon;
            set => SetAndNotify(ref _Icon, value);
        }
        #endregion
        #region Race
        private string _Race;
        [XmlSaveMode(XSME.Single)]
        public string Race
        {
            get => _Race;
            set
            {
                _Race = value;
                NotifyPropertyChanged();
                LinkedRace = Configuration.MainModelRef.ToolsView.PlayerRaces.FirstOrDefault(race => race.Name == value);
                UpdateSubraceList();
                if (Subraces.Contains(Subrace) == false) { Subrace = string.Empty; }
                UpdateDisplayRace();
            }
        }
        #endregion
        #region Alignment
        private string _Alignment;
        [XmlSaveMode(XSME.Single)]
        public string Alignment
        {
            get => _Alignment;
            set => SetAndNotify(ref _Alignment, value);
        }
        #endregion
        #region Background
        private string _Background;
        [XmlSaveMode(XSME.Single)]
        public string Background
        {
            get => _Background;
            set
            {
                _Background = value;
                NotifyPropertyChanged();
                LinkedBackground = Configuration.MainModelRef.ToolsView.PlayerBackgrounds.FirstOrDefault(bg => bg.Name == value);
            }
        }
        #endregion
        #region Personality
        private string _Personality;
        [XmlSaveMode(XSME.Single)]
        public string Personality
        {
            get => _Personality;
            set => SetAndNotify(ref _Personality, value);
        }
        #endregion
        #region Ideals
        private string _Ideals;
        [XmlSaveMode(XSME.Single)]
        public string Ideals
        {
            get => _Ideals;
            set => SetAndNotify(ref _Ideals, value);
        }
        #endregion
        #region Bonds
        private string _Bonds;
        [XmlSaveMode(XSME.Single)]
        public string Bonds
        {
            get => _Bonds;
            set => SetAndNotify(ref _Bonds, value);
        }
        #endregion
        #region Flaws
        private string _Flaws;
        [XmlSaveMode(XSME.Single)]
        public string Flaws
        {
            get => _Flaws;
            set => SetAndNotify(ref _Flaws, value);
        }
        #endregion
        #region Backstory
        private string _Backstory;
        [XmlSaveMode(XSME.Single)]
        public string Backstory
        {
            get => _Backstory;
            set => SetAndNotify(ref _Backstory, value);
        }
        #endregion
        #region Languages
        private string _Languages;
        [XmlSaveMode(XSME.Single)]
        public string Languages
        {
            get => _Languages;
            set => SetAndNotify(ref _Languages, value);
        }
        #endregion
        #region OtherProficiencies
        private string _OtherProficiencies;
        [XmlSaveMode(XSME.Single)]
        public string OtherProficiencies
        {
            get => _OtherProficiencies;
            set => SetAndNotify(ref _OtherProficiencies, value);
        }
        #endregion
        #region ToolProficiencies
        private ObservableCollection<ItemModel> _ToolProficiencies;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> ToolProficiencies
        {
            get => _ToolProficiencies;
            set => SetAndNotify(ref _ToolProficiencies, value);
        }
        #endregion
        #region RawWeight
        private int _RawWeight;
        [XmlSaveMode(XSME.Single)]
        public int RawWeight
        {
            get => _RawWeight;
            set => SetAndNotify(ref _RawWeight, value);
        }
        #endregion
        #region RawHeight
        private int _RawHeight;
        [XmlSaveMode(XSME.Single)]
        public int RawHeight
        {
            get => _RawHeight;
            set
            {
                _RawHeight = value;
                NotifyPropertyChanged();
                SetProcessedHeight();
            }
        }
        #endregion
        #region ProcessedHeight
        private string _ProcessedHeight;
        public string ProcessedHeight
        {
            get => _ProcessedHeight;
            set => SetAndNotify(ref _ProcessedHeight, value);
        }
        #endregion
        #region PlayerClasses
        private ObservableCollection<PlayerClassLinkModel> _PlayerClasses;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<PlayerClassLinkModel> PlayerClasses
        {
            get => _PlayerClasses;
            set => SetAndNotify(ref _PlayerClasses, value);
        }
        #endregion
        #region ClassAutoText
        private string _ClassAutoText;
        public string ClassAutoText
        {
            get => _ClassAutoText;
            set => SetAndNotify(ref _ClassAutoText, value);
        }
        #endregion
        #region SubclassAutoText
        private string _SubclassAutoText;
        public string SubclassAutoText
        {
            get => _SubclassAutoText;
            set => SetAndNotify(ref _SubclassAutoText, value);
        }
        #endregion
        #region TotalLevel
        private int _TotalLevel;
        public int TotalLevel
        {
            get => _TotalLevel;
            set
            {
                _TotalLevel = value;
                NotifyPropertyChanged();
                ProficiencyBonus = HelperMethods.GetProfBonusFromCr(TotalLevel.ToString());
                UpdateXpToNext();
                SetAttributeFeatChoices();
            }
        }
        #endregion
        #region Subraces
        private List<string> _Subraces;
        public List<string> Subraces
        {
            get => _Subraces;
            set => SetAndNotify(ref _Subraces, value);
        }
        #endregion
        #region Subrace
        private string _Subrace;
        [XmlSaveMode(XSME.Single)]
        public string Subrace
        {
            get => _Subrace;
            set
            {
                _Subrace = value;
                NotifyPropertyChanged();
                UpdateDisplayRace();
                LinkedSubrace = Configuration.MainModelRef.ToolsView.PlayerSubraces.FirstOrDefault(race => race.Name == value);
            }
        }
        #endregion
        #region ExperiencePoints
        private int _ExperiencePoints;
        [XmlSaveMode(XSME.Single)]
        public int ExperiencePoints
        {
            get => _ExperiencePoints;
            set
            {
                _ExperiencePoints = value;
                NotifyPropertyChanged();
                UpdateXpToNext();
            }
        }
        #endregion
        #region XpToNext
        private int _XpToNext;
        public int XpToNext
        {
            get => _XpToNext;
            set => SetAndNotify(ref _XpToNext, value);
        }
        #endregion
        #region DisplayRace
        private string _DisplayRace;
        public string DisplayRace
        {
            get => _DisplayRace;
            set => SetAndNotify(ref _DisplayRace, value);
        }
        #endregion

        #region ActionHistory
        private ObservableCollection<string> _ActionHistory;
        
        public ObservableCollection<string> ActionHistory
        {
            get => _ActionHistory;
            set => SetAndNotify(ref _ActionHistory, value);
        }
        #endregion
        #region OutputLinkedToRoll20
        private bool _OutputLinkedToRoll20;
        public bool OutputLinkedToRoll20
        {
            get => _OutputLinkedToRoll20;
            set => SetAndNotify(ref _OutputLinkedToRoll20, value);
        }
        #endregion
        #region Messages
        private ObservableCollection<GameMessage> _Messages;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<GameMessage> Messages
        {
            get => _Messages;
            set => SetAndNotify(ref _Messages, value);
        }
        #endregion

        #region FinalArmorClass
        private int _FinalArmorClass;
        public int FinalArmorClass
        {
            get => _FinalArmorClass;
            set => SetAndNotify(ref _FinalArmorClass, value);
        }
        #endregion
        #region FinalSpeed
        private int _FinalSpeed;
        public int FinalSpeed
        {
            get => _FinalSpeed;
            set => SetAndNotify(ref _FinalSpeed, value);
        }
        #endregion

        // Databound Properties - Character Creation
        #region HasCompletedCharacterCreation
        private bool _HasCompletedCharacterCreation;
        [XmlSaveMode(XSME.Single)]
        public bool HasCompletedCharacterCreation
        {
            get => _HasCompletedCharacterCreation;
            set => SetAndNotify(ref _HasCompletedCharacterCreation, value);
        }
        #endregion

        #region BaseAttributePoints
        private int _BaseAttributePoints;
        [XmlSaveMode(XSME.Single)]
        public int BaseAttributePoints
        {
            get => _BaseAttributePoints;
            set => SetAndNotify(ref _BaseAttributePoints, value);
        }
        #endregion
        #region StrengthBaseScore
        private int _StrengthBaseScore;
        [XmlSaveMode(XSME.Single)]
        public int StrengthBaseScore
        {
            get => _StrengthBaseScore;
            set
            {
                _StrengthBaseScore = value;
                NotifyPropertyChanged();
                CalculateFinalScores();
            }
        }
        #endregion
        #region DexterityBaseScore
        private int _DexterityBaseScore;
        [XmlSaveMode(XSME.Single)]
        public int DexterityBaseScore
        {
            get => _DexterityBaseScore;
            set
            {
                _DexterityBaseScore = value;
                NotifyPropertyChanged();
                CalculateFinalScores();
            }
        }
        #endregion
        #region ConstitutionBaseScore
        private int _ConstitutionBaseScore;
        [XmlSaveMode(XSME.Single)]
        public int ConstitutionBaseScore
        {
            get => _ConstitutionBaseScore;
            set
            {
                _ConstitutionBaseScore = value;
                NotifyPropertyChanged();
                CalculateFinalScores();
            }
        }
        #endregion
        #region IntelligenceBaseScore
        private int _IntelligenceBaseScore;
        [XmlSaveMode(XSME.Single)]
        public int IntelligenceBaseScore
        {
            get => _IntelligenceBaseScore;
            set
            {
                _IntelligenceBaseScore = value;
                NotifyPropertyChanged();
                CalculateFinalScores();
            }
        }
        #endregion
        #region WisdomBaseScore
        private int _WisdomBaseScore;
        [XmlSaveMode(XSME.Single)]
        public int WisdomBaseScore
        {
            get => _WisdomBaseScore;
            set
            {
                _WisdomBaseScore = value;
                NotifyPropertyChanged();
                CalculateFinalScores();
            }
        }
        #endregion
        #region CharismaBaseScore
        private int _CharismaBaseScore;
        [XmlSaveMode(XSME.Single)]
        public int CharismaBaseScore
        {
            get => _CharismaBaseScore;
            set
            {
                _CharismaBaseScore = value;
                NotifyPropertyChanged();
                CalculateFinalScores();
            }
        }
        #endregion

        #region StrengthFinalScore
        private int _StrengthFinalScore;
        public int StrengthFinalScore
        {
            get => _StrengthFinalScore;
            set => SetAndNotify(ref _StrengthFinalScore, value);
        }
        #endregion
        #region DexterityFinalScore
        private int _DexterityFinalScore;
        public int DexterityFinalScore
        {
            get => _DexterityFinalScore;
            set => SetAndNotify(ref _DexterityFinalScore, value);
        }
        #endregion
        #region ConstitutionFinalScore
        private int _ConstitutionFinalScore;
        public int ConstitutionFinalScore
        {
            get => _ConstitutionFinalScore;
            set => SetAndNotify(ref _ConstitutionFinalScore, value);
        }
        #endregion
        #region IntelligenceFinalScore
        private int _IntelligenceFinalScore;
        public int IntelligenceFinalScore
        {
            get => _IntelligenceFinalScore;
            set => SetAndNotify(ref _IntelligenceFinalScore, value);
        }
        #endregion
        #region WisdomFinalScore
        private int _WisdomFinalScore;
        public int WisdomFinalScore
        {
            get => _WisdomFinalScore;
            set => SetAndNotify(ref _WisdomFinalScore, value);
        }
        #endregion
        #region CharismaFinalScore
        private int _CharismaFinalScore;
        public int CharismaFinalScore
        {
            get => _CharismaFinalScore;
            set => SetAndNotify(ref _CharismaFinalScore, value);
        }
        #endregion

        #region StrengthFinalModifier
        private int _StrengthFinalModifier;
        public int StrengthFinalModifier
        {
            get => _StrengthFinalModifier;
            set => SetAndNotify(ref _StrengthFinalModifier, value);
        }
        #endregion
        #region DexterityFinalModifier
        private int _DexterityFinalModifier;
        public int DexterityFinalModifier
        {
            get => _DexterityFinalModifier;
            set => SetAndNotify(ref _DexterityFinalModifier, value);
        }
        #endregion
        #region ConstitutionFinalModifier
        private int _ConstitutionFinalModifier;
        public int ConstitutionFinalModifier
        {
            get => _ConstitutionFinalModifier;
            set => SetAndNotify(ref _ConstitutionFinalModifier, value);
        }
        #endregion
        #region IntelligenceFinalModifier
        private int _IntelligenceFinalModifier;
        public int IntelligenceFinalModifier
        {
            get => _IntelligenceFinalModifier;
            set => SetAndNotify(ref _IntelligenceFinalModifier, value);
        }
        #endregion
        #region WisdomFinalModifier
        private int _WisdomFinalModifier;
        public int WisdomFinalModifier
        {
            get => _WisdomFinalModifier;
            set => SetAndNotify(ref _WisdomFinalModifier, value);
        }
        #endregion
        #region CharismaFinalModifier
        private int _CharismaFinalModifier;
        public int CharismaFinalModifier
        {
            get => _CharismaFinalModifier;
            set => SetAndNotify(ref _CharismaFinalModifier, value);
        }
        #endregion
        
        #region FeatPoints
        private int _FeatPoints;
        public int FeatPoints
        {
            get => _FeatPoints;
            set => SetAndNotify(ref _FeatPoints, value);
        }
        #endregion

        #region Races
        private List<string> _Races;
        public List<string> Races
        {
            get => _Races;
            set => SetAndNotify(ref _Races, value);
        }
        #endregion
        #region LinkedRace
        private PlayerRaceModel _LinkedRace;
        public PlayerRaceModel LinkedRace
        {
            get => _LinkedRace;
            set
            {
                _LinkedRace = value;
                NotifyPropertyChanged();
                if (value == null) { return; }
                UpdateCharacterSheet();
            }
        }
        #endregion
        #region LinkedSubrace
        private PlayerSubraceModel _LinkedSubrace;
        public PlayerSubraceModel LinkedSubrace
        {
            get => _LinkedSubrace;
            set
            {
                _LinkedSubrace = value;
                NotifyPropertyChanged();
                if (value == null) { return; }
                UpdateCharacterSheet();
            }
        }
        #endregion

        #region Backgrounds
        private List<string> _Backgrounds;
        public List<string> Backgrounds
        {
            get => _Backgrounds;
            set => SetAndNotify(ref _Backgrounds, value);
        }
        #endregion
        #region LinkedBackground
        private PlayerBackgroundModel _LinkedBackground;
        public PlayerBackgroundModel LinkedBackground
        {
            get => _LinkedBackground;
            set
            {
                _LinkedBackground = value;
                NotifyPropertyChanged();
                if (value == null) { return; }
                UpdateCharacterSheet();
            }
        }
        #endregion

        #region Alignments
        private List<string> _Alignments;
        public List<string> Alignments
        {
            get => _Alignments;
            set => SetAndNotify(ref _Alignments, value);
        }
        #endregion

        #region SetLanguageProfs
        private List<string> _SetLanguageProfs;
        public List<string> SetLanguageProfs
        {
            get => _SetLanguageProfs;
            set => SetAndNotify(ref _SetLanguageProfs, value);
        }
        #endregion
        #region LanguageChoiceSegments
        private ObservableCollection<ChoiceSet> _LanguageChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> LanguageChoiceSegments
        {
            get => _LanguageChoiceSegments;
            set => SetAndNotify(ref _LanguageChoiceSegments, value);
        }
        #endregion

        #region SetToolProfs
        private List<string> _SetToolProfs;
        public List<string> SetToolProfs
        {
            get => _SetToolProfs;
            set => SetAndNotify(ref _SetToolProfs, value);
        }
        #endregion
        #region ToolChoiceSegments
        private ObservableCollection<ChoiceSet> _ToolChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> ToolChoiceSegments
        {
            get => _ToolChoiceSegments;
            set => SetAndNotify(ref _ToolChoiceSegments, value);
        }
        #endregion

        #region SetSkillProfs
        private List<string> _SetSkillProfs;
        public List<string> SetSkillProfs
        {
            get => _SetSkillProfs;
            set => SetAndNotify(ref _SetSkillProfs, value);
        }
        #endregion
        #region SkillChoiceSegments
        private ObservableCollection<ChoiceSet> _SkillChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> SkillChoiceSegments
        {
            get => _SkillChoiceSegments;
            set => SetAndNotify(ref _SkillChoiceSegments, value);
        }
        #endregion
        #region ExpertiseChoiceSegments
        private ObservableCollection<ChoiceSet> _ExpertiseChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> ExpertiseChoiceSegments
        {
            get => _ExpertiseChoiceSegments;
            set => SetAndNotify(ref _ExpertiseChoiceSegments, value);
        }
        #endregion

        #region SetWeaponProfs
        private List<string> _SetWeaponProfs;
        public List<string> SetWeaponProfs
        {
            get => _SetWeaponProfs;
            set => SetAndNotify(ref _SetWeaponProfs, value);
        }
        #endregion
        #region WeaponChoiceSegments
        private ObservableCollection<ChoiceSet> _WeaponChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> WeaponChoiceSegments
        {
            get => _WeaponChoiceSegments;
            set => SetAndNotify(ref _WeaponChoiceSegments, value);
        }
        #endregion

        #region SetArmorProfs
        private List<string> _SetArmorProfs;
        public List<string> SetArmorProfs
        {
            get => _SetArmorProfs;
            set => SetAndNotify(ref _SetArmorProfs, value);
        }
        #endregion
        #region ArmorChoiceSegments
        private ObservableCollection<ChoiceSet> _ArmorChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> ArmorChoiceSegments
        {
            get => _ArmorChoiceSegments;
            set => SetAndNotify(ref _ArmorChoiceSegments, value);
        }
        #endregion

        #region AttributeFeatChoices
        private ObservableCollection<ChoiceSet> _AttributeFeatChoices;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> AttributeFeatChoices
        {
            get => _AttributeFeatChoices;
            set => SetAndNotify(ref _AttributeFeatChoices, value);
        }
        #endregion
        #region FeatChoices
        private ObservableCollection<BoolOption> _FeatChoices;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<BoolOption> FeatChoices
        {
            get => _FeatChoices;
            set => SetAndNotify(ref _FeatChoices, value);
        }
        #endregion

        #region CCAttributeSets
        private ObservableCollection<CharacterAttributeSet> _CCAttributeSets;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CharacterAttributeSet> CCAttributeSets
        {
            get => _CCAttributeSets;
            set => SetAndNotify(ref _CCAttributeSets, value);
        }
        #endregion

        #region StartingGold
        private int _StartingGold;
        public int StartingGold
        {
            get => _StartingGold;
            set => SetAndNotify(ref _StartingGold, value);
        }
        #endregion
        #region StartingEquipmentChoiceSets
        private ObservableCollection<ConvertibleValueSet> _StartingEquipmentChoiceSets;
        public ObservableCollection<ConvertibleValueSet> StartingEquipmentChoiceSets
        {
            get => _StartingEquipmentChoiceSets;
            set => SetAndNotify(ref _StartingEquipmentChoiceSets, value);
        }
        #endregion
        #region ChosenEquipment
        private ObservableCollection<ItemLink> _ChosenEquipment;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemLink> ChosenEquipment
        {
            get => _ChosenEquipment;
            set => SetAndNotify(ref _ChosenEquipment, value);
        }
        #endregion
        #region GrantedEquipment
        private ObservableCollection<ItemLink> _GrantedEquipment;
        public ObservableCollection<ItemLink> GrantedEquipment
        {
            get => _GrantedEquipment;
            set => SetAndNotify(ref _GrantedEquipment, value);
        }
        #endregion

        #region TraitChoiceSegments
        private ObservableCollection<ChoiceSet> _TraitChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> TraitChoiceSegments
        {
            get => _TraitChoiceSegments;
            set => SetAndNotify(ref _TraitChoiceSegments, value);
        }
        #endregion
        #region SetTraits
        private ObservableCollection<TraitModel> _SetTraits;
        public ObservableCollection<TraitModel> SetTraits
        {
            get => _SetTraits;
            set => SetAndNotify(ref _SetTraits, value);
        }
        #endregion

        #region StatBonusChoiceSegments
        private ObservableCollection<ChoiceSet> _StatBonusChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> StatBonusChoiceSegments
        {
            get => _StatBonusChoiceSegments;
            set => SetAndNotify(ref _StatBonusChoiceSegments, value);
        }
        #endregion
        #region MaxHpCalc
        private int _MaxHpCalc;
        public int MaxHpCalc
        {
            get => _MaxHpCalc;
            set => SetAndNotify(ref _MaxHpCalc, value);
        }
        #endregion

        #region SpellChoiceSegments
        private ObservableCollection<ChoiceSet> _SpellChoiceSegments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ChoiceSet> SpellChoiceSegments
        {
            get => _SpellChoiceSegments;
            set => SetAndNotify(ref _SpellChoiceSegments, value);
        }
        #endregion

        // Databound Properties - Attributes and Skills
        #region StrengthScore
        private int _StrengthScore;
        [XmlSaveMode(XSME.Single)]
        public int StrengthScore
        {
            get => _StrengthScore;
            set
            {
                _StrengthScore = value;
                NotifyPropertyChanged();
                UpdateModifiers();
                UpdateEncumbrance();
            }
        }
        #endregion
        #region DexterityScore
        private int _DexterityScore;
        [XmlSaveMode(XSME.Single)]
        public int DexterityScore
        {
            get => _DexterityScore;
            set
            {
                _DexterityScore = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region ConstitutionScore
        private int _ConstitutionScore;
        [XmlSaveMode(XSME.Single)]
        public int ConstitutionScore
        {
            get => _ConstitutionScore;
            set
            {
                _ConstitutionScore = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IntelligenceScore
        private int _IntelligenceScore;
        [XmlSaveMode(XSME.Single)]
        public int IntelligenceScore
        {
            get => _IntelligenceScore;
            set
            {
                _IntelligenceScore = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region WisdomScore
        private int _WisdomScore;
        [XmlSaveMode(XSME.Single)]
        public int WisdomScore
        {
            get => _WisdomScore;
            set
            {
                _WisdomScore = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region CharismaScore
        private int _CharismaScore;
        [XmlSaveMode(XSME.Single)]
        public int CharismaScore
        {
            get => _CharismaScore;
            set
            {
                _CharismaScore = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion

        #region StrengthBonus
        private int _StrengthBonus;
        [XmlSaveMode(XSME.Single)]
        public int StrengthBonus
        {
            get => _StrengthBonus;
            set => SetAndNotify(ref _StrengthBonus, value);
        }
        #endregion
        #region DexterityBonus
        private int _DexterityBonus;
        [XmlSaveMode(XSME.Single)]
        public int DexterityBonus
        {
            get => _DexterityBonus;
            set => SetAndNotify(ref _DexterityBonus, value);
        }
        #endregion
        #region ConstitutionBonus
        private int _ConstitutionBonus;
        [XmlSaveMode(XSME.Single)]
        public int ConstitutionBonus
        {
            get => _ConstitutionBonus;
            set => SetAndNotify(ref _ConstitutionBonus, value);
        }
        #endregion
        #region IntelligenceBonus
        private int _IntelligenceBonus;
        [XmlSaveMode(XSME.Single)]
        public int IntelligenceBonus
        {
            get => _IntelligenceBonus;
            set => SetAndNotify(ref _IntelligenceBonus, value);
        }
        #endregion
        #region WisdomBonus
        private int _WisdomBonus;
        [XmlSaveMode(XSME.Single)]
        public int WisdomBonus
        {
            get => _WisdomBonus;
            set => SetAndNotify(ref _WisdomBonus, value);
        }
        #endregion
        #region CharismaBonus
        private int _CharismaBonus;
        [XmlSaveMode(XSME.Single)]
        public int CharismaBonus
        {
            get => _CharismaBonus;
            set => SetAndNotify(ref _CharismaBonus, value);
        }
        #endregion

        #region StrengthModifier
        private int _StrengthModifier;
        public int StrengthModifier
        {
            get => _StrengthModifier;
            set => SetAndNotify(ref _StrengthModifier, value);
        }
        #endregion
        #region DexterityModifier
        private int _DexterityModifier;
        public int DexterityModifier
        {
            get => _DexterityModifier;
            set => SetAndNotify(ref _DexterityModifier, value);
        }
        #endregion
        #region ConstitutionModifier
        private int _ConstitutionModifier;
        public int ConstitutionModifier
        {
            get => _ConstitutionModifier;
            set => SetAndNotify(ref _ConstitutionModifier, value);
        }
        #endregion
        #region IntelligenceModifier
        private int _IntelligenceModifier;
        public int IntelligenceModifier
        {
            get => _IntelligenceModifier;
            set => SetAndNotify(ref _IntelligenceModifier, value);
        }
        #endregion
        #region WisdomModifier
        private int _WisdomModifier;
        public int WisdomModifier
        {
            get => _WisdomModifier;
            set => SetAndNotify(ref _WisdomModifier, value);
        }
        #endregion
        #region CharismaModifier
        private int _CharismaModifier;
        public int CharismaModifier
        {
            get => _CharismaModifier;
            set => SetAndNotify(ref _CharismaModifier, value);
        }
        #endregion

        #region HasSave_Strength
        private bool _HasSave_Strength;
        [XmlSaveMode(XSME.Single)]
        public bool HasSave_Strength
        {
            get => _HasSave_Strength;
            set
            {
                _HasSave_Strength = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region HasSave_Dexterity
        private bool _HasSave_Dexterity;
        [XmlSaveMode(XSME.Single)]
        public bool HasSave_Dexterity
        {
            get => _HasSave_Dexterity;
            set
            {
                _HasSave_Dexterity = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region HasSave_Constitution
        private bool _HasSave_Constitution;
        [XmlSaveMode(XSME.Single)]
        public bool HasSave_Constitution
        {
            get => _HasSave_Constitution;
            set
            {
                _HasSave_Constitution = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region HasSave_Intelligence
        private bool _HasSave_Intelligence;
        [XmlSaveMode(XSME.Single)]
        public bool HasSave_Intelligence
        {
            get => _HasSave_Intelligence;
            set
            {
                _HasSave_Intelligence = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region HasSave_Wisdom
        private bool _HasSave_Wisdom;
        [XmlSaveMode(XSME.Single)]
        public bool HasSave_Wisdom
        {
            get => _HasSave_Wisdom;
            set
            {
                _HasSave_Wisdom = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region HasSave_Charisma
        private bool _HasSave_Charisma;
        [XmlSaveMode(XSME.Single)]
        public bool HasSave_Charisma
        {
            get => _HasSave_Charisma;
            set
            {
                _HasSave_Charisma = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion

        #region StrengthSave
        private int _StrengthSave;
        public int StrengthSave
        {
            get => _StrengthSave;
            set => SetAndNotify(ref _StrengthSave, value);
        }
        #endregion
        #region DexteritySave
        private int _DexteritySave;
        public int DexteritySave
        {
            get => _DexteritySave;
            set => SetAndNotify(ref _DexteritySave, value);
        }
        #endregion
        #region ConstitutionSave
        private int _ConstitutionSave;
        public int ConstitutionSave
        {
            get => _ConstitutionSave;
            set => SetAndNotify(ref _ConstitutionSave, value);
        }
        #endregion
        #region IntelligenceSave
        private int _IntelligenceSave;
        public int IntelligenceSave
        {
            get => _IntelligenceSave;
            set => SetAndNotify(ref _IntelligenceSave, value);
        }
        #endregion
        #region WisdomSave
        private int _WisdomSave;
        public int WisdomSave
        {
            get => _WisdomSave;
            set => SetAndNotify(ref _WisdomSave, value);
        }
        #endregion
        #region CharismaSave
        private int _CharismaSave;
        public int CharismaSave
        {
            get => _CharismaSave;
            set => SetAndNotify(ref _CharismaSave, value);
        }
        #endregion

        #region IsProf_Acrobatics
        private bool _IsProf_Acrobatics;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Acrobatics
        {
            get => _IsProf_Acrobatics;
            set
            {
                _IsProf_Acrobatics = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_AnimalHandling
        private bool _IsProf_AnimalHandling;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_AnimalHandling
        {
            get => _IsProf_AnimalHandling;
            set
            {
                _IsProf_AnimalHandling = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Arcana
        private bool _IsProf_Arcana;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Arcana
        {
            get => _IsProf_Arcana;
            set
            {
                _IsProf_Arcana = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Athletics
        private bool _IsProf_Athletics;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Athletics
        {
            get => _IsProf_Athletics;
            set
            {
                _IsProf_Athletics = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Deception
        private bool _IsProf_Deception;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Deception
        {
            get => _IsProf_Deception;
            set
            {
                _IsProf_Deception = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_History
        private bool _IsProf_History;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_History
        {
            get => _IsProf_History;
            set
            {
                _IsProf_History = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Insight
        private bool _IsProf_Insight;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Insight
        {
            get => _IsProf_Insight;
            set
            {
                _IsProf_Insight = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Intimidation
        private bool _IsProf_Intimidation;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Intimidation
        {
            get => _IsProf_Intimidation;
            set
            {
                _IsProf_Intimidation = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Investigation
        private bool _IsProf_Investigation;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Investigation
        {
            get => _IsProf_Investigation;
            set
            {
                _IsProf_Investigation = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Medicine
        private bool _IsProf_Medicine;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Medicine
        {
            get => _IsProf_Medicine;
            set
            {
                _IsProf_Medicine = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Nature
        private bool _IsProf_Nature;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Nature
        {
            get => _IsProf_Nature;
            set
            {
                _IsProf_Nature = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Perception
        private bool _IsProf_Perception;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Perception
        {
            get => _IsProf_Perception;
            set
            {
                _IsProf_Perception = value;
                NotifyPropertyChanged();
                SetPassivePerception();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Performance
        private bool _IsProf_Performance;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Performance
        {
            get => _IsProf_Performance;
            set
            {
                _IsProf_Performance = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Persuasion
        private bool _IsProf_Persuasion;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Persuasion
        {
            get => _IsProf_Persuasion;
            set
            {
                _IsProf_Persuasion = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Religion
        private bool _IsProf_Religion;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Religion
        {
            get => _IsProf_Religion;
            set
            {
                _IsProf_Religion = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_SleightOfHand
        private bool _IsProf_SleightOfHand;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_SleightOfHand
        {
            get => _IsProf_SleightOfHand;
            set
            {
                _IsProf_SleightOfHand = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Stealth
        private bool _IsProf_Stealth;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Stealth
        {
            get => _IsProf_Stealth;
            set
            {
                _IsProf_Stealth = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsProf_Survival
        private bool _IsProf_Survival;
        [XmlSaveMode(XSME.Single)]
        public bool IsProf_Survival
        {
            get => _IsProf_Survival;
            set
            {
                _IsProf_Survival = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion

        #region IsExpert_Acrobatics
        private bool _IsExpert_Acrobatics;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Acrobatics
        {
            get => _IsExpert_Acrobatics;
            set
            {
                _IsExpert_Acrobatics = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_AnimalHandling
        private bool _IsExpert_AnimalHandling;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_AnimalHandling
        {
            get => _IsExpert_AnimalHandling;
            set
            {
                _IsExpert_AnimalHandling = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Arcana
        private bool _IsExpert_Arcana;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Arcana
        {
            get => _IsExpert_Arcana;
            set
            {
                _IsExpert_Arcana = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Athletics
        private bool _IsExpert_Athletics;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Athletics
        {
            get => _IsExpert_Athletics;
            set
            {
                _IsExpert_Athletics = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Deception
        private bool _IsExpert_Deception;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Deception
        {
            get => _IsExpert_Deception;
            set
            {
                _IsExpert_Deception = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_History
        private bool _IsExpert_History;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_History
        {
            get => _IsExpert_History;
            set
            {
                _IsExpert_History = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Insight
        private bool _IsExpert_Insight;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Insight
        {
            get => _IsExpert_Insight;
            set
            {
                _IsExpert_Insight = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Intimidation
        private bool _IsExpert_Intimidation;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Intimidation
        {
            get => _IsExpert_Intimidation;
            set
            {
                _IsExpert_Intimidation = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Investigation
        private bool _IsExpert_Investigation;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Investigation
        {
            get => _IsExpert_Investigation;
            set
            {
                _IsExpert_Investigation = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Medicine
        private bool _IsExpert_Medicine;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Medicine
        {
            get => _IsExpert_Medicine;
            set
            {
                _IsExpert_Medicine = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Nature
        private bool _IsExpert_Nature;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Nature
        {
            get => _IsExpert_Nature;
            set
            {
                _IsExpert_Nature = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Perception
        private bool _IsExpert_Perception;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Perception
        {
            get => _IsExpert_Perception;
            set
            {
                _IsExpert_Perception = value;
                NotifyPropertyChanged();
                SetPassivePerception();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Performance
        private bool _IsExpert_Performance;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Performance
        {
            get => _IsExpert_Performance;
            set
            {
                _IsExpert_Performance = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Persuasion
        private bool _IsExpert_Persuasion;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Persuasion
        {
            get => _IsExpert_Persuasion;
            set
            {
                _IsExpert_Persuasion = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Religion
        private bool _IsExpert_Religion;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Religion
        {
            get => _IsExpert_Religion;
            set
            {
                _IsExpert_Religion = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_SleightOfHand
        private bool _IsExpert_SleightOfHand;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_SleightOfHand
        {
            get => _IsExpert_SleightOfHand;
            set
            {
                _IsExpert_SleightOfHand = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Stealth
        private bool _IsExpert_Stealth;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Stealth
        {
            get => _IsExpert_Stealth;
            set
            {
                _IsExpert_Stealth = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region IsExpert_Survival
        private bool _IsExpert_Survival;
        [XmlSaveMode(XSME.Single)]
        public bool IsExpert_Survival
        {
            get => _IsExpert_Survival;
            set
            {
                _IsExpert_Survival = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion

        #region AcrobaticsBonus
        private int _AcrobaticsBonus;
        [XmlSaveMode(XSME.Single)]
        public int AcrobaticsBonus
        {
            get => _AcrobaticsBonus;
            set => SetAndNotify(ref _AcrobaticsBonus, value);
        }
        #endregion
        #region AnimalHandlingBonus
        private int _AnimalHandlingBonus;
        [XmlSaveMode(XSME.Single)]
        public int AnimalHandlingBonus
        {
            get => _AnimalHandlingBonus;
            set => SetAndNotify(ref _AnimalHandlingBonus, value);
        }
        #endregion
        #region ArcanaBonus
        private int _ArcanaBonus;
        [XmlSaveMode(XSME.Single)]
        public int ArcanaBonus
        {
            get => _ArcanaBonus;
            set => SetAndNotify(ref _ArcanaBonus, value);
        }
        #endregion
        #region AthleticsBonus
        private int _AthleticsBonus;
        [XmlSaveMode(XSME.Single)]
        public int AthleticsBonus
        {
            get => _AthleticsBonus;
            set => SetAndNotify(ref _AthleticsBonus, value);
        }
        #endregion
        #region DeceptionBonus
        private int _DeceptionBonus;
        [XmlSaveMode(XSME.Single)]
        public int DeceptionBonus
        {
            get => _DeceptionBonus;
            set => SetAndNotify(ref _DeceptionBonus, value);
        }
        #endregion
        #region HistoryBonus
        private int _HistoryBonus;
        [XmlSaveMode(XSME.Single)]
        public int HistoryBonus
        {
            get => _HistoryBonus;
            set => SetAndNotify(ref _HistoryBonus, value);
        }
        #endregion
        #region InsightBonus
        private int _InsightBonus;
        [XmlSaveMode(XSME.Single)]
        public int InsightBonus
        {
            get => _InsightBonus;
            set => SetAndNotify(ref _InsightBonus, value);
        }
        #endregion
        #region IntimidationBonus
        private int _IntimidationBonus;
        [XmlSaveMode(XSME.Single)]
        public int IntimidationBonus
        {
            get => _IntimidationBonus;
            set => SetAndNotify(ref _IntimidationBonus, value);
        }
        #endregion
        #region InvestigationBonus
        private int _InvestigationBonus;
        [XmlSaveMode(XSME.Single)]
        public int InvestigationBonus
        {
            get => _InvestigationBonus;
            set => SetAndNotify(ref _InvestigationBonus, value);
        }
        #endregion
        #region MedicineBonus
        private int _MedicineBonus;
        [XmlSaveMode(XSME.Single)]
        public int MedicineBonus
        {
            get => _MedicineBonus;
            set => SetAndNotify(ref _MedicineBonus, value);
        }
        #endregion
        #region NatureBonus
        private int _NatureBonus;
        [XmlSaveMode(XSME.Single)]
        public int NatureBonus
        {
            get => _NatureBonus;
            set => SetAndNotify(ref _NatureBonus, value);
        }
        #endregion
        #region PerceptionBonus
        private int _PerceptionBonus;
        [XmlSaveMode(XSME.Single)]
        public int PerceptionBonus
        {
            get => _PerceptionBonus;
            set => SetAndNotify(ref _PerceptionBonus, value);
        }
        #endregion
        #region PerformanceBonus
        private int _PerformanceBonus;
        [XmlSaveMode(XSME.Single)]
        public int PerformanceBonus
        {
            get => _PerformanceBonus;
            set => SetAndNotify(ref _PerformanceBonus, value);
        }
        #endregion
        #region PersuasionBonus
        private int _PersuasionBonus;
        [XmlSaveMode(XSME.Single)]
        public int PersuasionBonus
        {
            get => _PersuasionBonus;
            set => SetAndNotify(ref _PersuasionBonus, value);
        }
        #endregion
        #region ReligionBonus
        private int _ReligionBonus;
        [XmlSaveMode(XSME.Single)]
        public int ReligionBonus
        {
            get => _ReligionBonus;
            set => SetAndNotify(ref _ReligionBonus, value);
        }
        #endregion
        #region SleightOfHandBonus
        private int _SleightOfHandBonus;
        [XmlSaveMode(XSME.Single)]
        public int SleightOfHandBonus
        {
            get => _SleightOfHandBonus;
            set => SetAndNotify(ref _SleightOfHandBonus, value);
        }
        #endregion
        #region StealthBonus
        private int _StealthBonus;
        [XmlSaveMode(XSME.Single)]
        public int StealthBonus
        {
            get => _StealthBonus;
            set => SetAndNotify(ref _StealthBonus, value);
        }
        #endregion
        #region SurvivalBonus
        private int _SurvivalBonus;
        [XmlSaveMode(XSME.Single)]
        public int SurvivalBonus
        {
            get => _SurvivalBonus;
            set => SetAndNotify(ref _SurvivalBonus, value);
        }
        #endregion

        #region AcrobaticsModifier
        private int _AcrobaticsModifier;
        public int AcrobaticsModifier
        {
            get => _AcrobaticsModifier;
            set => SetAndNotify(ref _AcrobaticsModifier, value);
        }
        #endregion
        #region AnimalHandlingModifier
        private int _AnimalHandlingModifier;
        public int AnimalHandlingModifier
        {
            get => _AnimalHandlingModifier;
            set => SetAndNotify(ref _AnimalHandlingModifier, value);
        }
        #endregion
        #region ArcanaModifier
        private int _ArcanaModifier;
        public int ArcanaModifier
        {
            get => _ArcanaModifier;
            set => SetAndNotify(ref _ArcanaModifier, value);
        }
        #endregion
        #region AthleticsModifier
        private int _AthleticsModifier;
        public int AthleticsModifier
        {
            get => _AthleticsModifier;
            set => SetAndNotify(ref _AthleticsModifier, value);
        }
        #endregion
        #region DeceptionModifier
        private int _DeceptionModifier;
        public int DeceptionModifier
        {
            get => _DeceptionModifier;
            set => SetAndNotify(ref _DeceptionModifier, value);
        }
        #endregion
        #region HistoryModifier
        private int _HistoryModifier;
        public int HistoryModifier
        {
            get => _HistoryModifier;
            set => SetAndNotify(ref _HistoryModifier, value);
        }
        #endregion
        #region InsightModifier
        private int _InsightModifier;
        public int InsightModifier
        {
            get => _InsightModifier;
            set => SetAndNotify(ref _InsightModifier, value);
        }
        #endregion
        #region IntimidationModifier
        private int _IntimidationModifier;
        public int IntimidationModifier
        {
            get => _IntimidationModifier;
            set => SetAndNotify(ref _IntimidationModifier, value);
        }
        #endregion
        #region InvestigationModifier
        private int _InvestigationModifier;
        [XmlSaveMode(XSME.Single)]
        public int InvestigationModifier
        {
            get => _InvestigationModifier;
            set => SetAndNotify(ref _InvestigationModifier, value);
        }
        #endregion
        #region MedicineModifier
        private int _MedicineModifier;
        public int MedicineModifier
        {
            get => _MedicineModifier;
            set => SetAndNotify(ref _MedicineModifier, value);
        }
        #endregion
        #region NatureModifier
        private int _NatureModifier;
        public int NatureModifier
        {
            get => _NatureModifier;
            set => SetAndNotify(ref _NatureModifier, value);
        }
        #endregion
        #region PerceptionModifier
        private int _PerceptionModifier;
        public int PerceptionModifier
        {
            get => _PerceptionModifier;
            set => SetAndNotify(ref _PerceptionModifier, value);
        }
        #endregion
        #region PerformanceModifier
        private int _PerformanceModifier;
        public int PerformanceModifier
        {
            get => _PerformanceModifier;
            set => SetAndNotify(ref _PerformanceModifier, value);
        }
        #endregion
        #region PersuasionModifier
        private int _PersuasionModifier;
        public int PersuasionModifier
        {
            get => _PersuasionModifier;
            set => SetAndNotify(ref _PersuasionModifier, value);
        }
        #endregion
        #region ReligionModifier
        private int _ReligionModifier;
        public int ReligionModifier
        {
            get => _ReligionModifier;
            set => SetAndNotify(ref _ReligionModifier, value);
        }
        #endregion
        #region SleightOfHandModifier
        private int _SleightOfHandModifier;
        public int SleightOfHandModifier
        {
            get => _SleightOfHandModifier;
            set => SetAndNotify(ref _SleightOfHandModifier, value);
        }
        #endregion
        #region StealthModifier
        private int _StealthModifier;
        public int StealthModifier
        {
            get => _StealthModifier;
            set => SetAndNotify(ref _StealthModifier, value);
        }
        #endregion
        #region SurvivalModifier
        private int _SurvivalModifier;
        public int SurvivalModifier
        {
            get => _SurvivalModifier;
            set => SetAndNotify(ref _SurvivalModifier, value);
        }
        #endregion

        #region AllSkillBonus
        private int _AllSkillBonus;
        [XmlSaveMode(XSME.Single)]
        public int AllSkillBonus
        {
            get => _AllSkillBonus;
            set
            {
                _AllSkillBonus = value;
                NotifyPropertyChanged();
                UpdateModifiers();
            }
        }
        #endregion
        #region Darkvision
        private int _Darkvision;
        [XmlSaveMode(XSME.Single)]
        public int Darkvision
        {
            get => _Darkvision;
            set => SetAndNotify(ref _Darkvision, value);
        }
        #endregion
        #region HasPowerfulBuild
        private bool _HasPowerfulBuild;
        [XmlSaveMode(XSME.Single)]
        public bool HasPowerfulBuild
        {
            get => _HasPowerfulBuild;
            set
            {
                _HasPowerfulBuild = value;
                NotifyPropertyChanged();
                UpdateEncumbrance();
            }
        }
        #endregion
        #region HitPointMaxBonus
        private int _HitPointMaxBonus;
        [XmlSaveMode(XSME.Single)]
        public int HitPointMaxBonus
        {
            get => _HitPointMaxBonus;
            set
            {
                _HitPointMaxBonus = value;
                NotifyPropertyChanged();
                MaxHealth = GetCalculatedMaxHitPoints(ConstitutionFinalModifier);
            }
        }
        #endregion

        // Databound Properties - Combat
        #region ProficiencyBonus
        private int _ProficiencyBonus;
        public int ProficiencyBonus
        {
            get => _ProficiencyBonus;
            set => SetAndNotify(ref _ProficiencyBonus, value);
        }
        #endregion
        #region Speed
        private int _Speed;
        [XmlSaveMode(XSME.Single)]
        public int Speed
        {
            get => _Speed;
            set => SetAndNotify(ref _Speed, value);
        }
        #endregion
        #region ArmorClass
        private int _ArmorClass;
        [XmlSaveMode(XSME.Single)]
        public int ArmorClass
        {
            get => _ArmorClass;
            set
            {
                _ArmorClass = value;
                NotifyPropertyChanged();
                UpdateArmorClass();
            }
        }
        #endregion
        #region AltArmorClass
        private int _AltArmorClass;
        [XmlSaveMode(XSME.Single)]
        public int AltArmorClass
        {
            get => _AltArmorClass;
            set => SetAndNotify(ref _AltArmorClass, value);
        }
        #endregion
        #region PassivePerception
        private int _PassivePerception;
        [XmlSaveMode(XSME.Single)]
        public int PassivePerception
        {
            get => _PassivePerception;
            set => SetAndNotify(ref _PassivePerception, value);
        }
        #endregion
        #region Initiative
        private int _Initiative;
        [XmlSaveMode(XSME.Single)]
        public int Initiative
        {
            get => _Initiative;
            set => SetAndNotify(ref _Initiative, value);
        }
        #endregion

        #region CurrentHealth
        private int _CurrentHealth;
        [XmlSaveMode(XSME.Single)]
        public int CurrentHealth
        {
            get => _CurrentHealth;
            set
            {
                _CurrentHealth = value;
                NotifyPropertyChanged();
                UpdateStatus();
            }
        }
        #endregion
        #region MaxHealth
        private int _MaxHealth;
        [XmlSaveMode(XSME.Single)]
        public int MaxHealth
        {
            get => _MaxHealth;
            set
            {
                _MaxHealth = value;
                NotifyPropertyChanged();
                UpdateStatus();
            }
        }
        #endregion
        #region Status
        private string _Status;
        public string Status
        {
            get => _Status;
            set => SetAndNotify(ref _Status, value);
        }
        #endregion
        #region TempHealth
        private int _TempHealth;
        [XmlSaveMode(XSME.Single)]
        public int TempHealth
        {
            get => _TempHealth;
            set => SetAndNotify(ref _TempHealth, value);
        }
        #endregion
        #region HitDiceSets
        private ObservableCollection<HitDiceSet> _HitDiceSets;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<HitDiceSet> HitDiceSets
        {
            get => _HitDiceSets;
            set => SetAndNotify(ref _HitDiceSets, value);
        }
        #endregion

        #region DisplayPopup_Skills
        private bool _DisplayPopup_Skills;
        public bool DisplayPopup_Skills
        {
            get => _DisplayPopup_Skills;
            set
            {
                _DisplayPopup_Skills = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Skills"); }
            }
        }
        #endregion
        #region DisplayPopup_Saves
        private bool _DisplayPopup_Saves;
        public bool DisplayPopup_Saves
        {
            get => _DisplayPopup_Saves;
            set
            {
                _DisplayPopup_Saves = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Saves"); }
            }
        }
        #endregion
        #region DisplayPopup_Checks
        private bool _DisplayPopup_Checks;
        public bool DisplayPopup_Checks
        {
            get => _DisplayPopup_Checks;
            set
            {
                _DisplayPopup_Checks = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Checks"); }
            }
        }
        #endregion
        #region DisplayPopup_Dice
        private bool _DisplayPopup_Dice;
        public bool DisplayPopup_Dice
        {
            get => _DisplayPopup_Dice;
            set
            {
                _DisplayPopup_Dice = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Dice"); }
            }
        }
        #endregion
        #region DisplayPopup_Health
        private bool _DisplayPopup_Health;
        public bool DisplayPopup_Health
        {
            get => _DisplayPopup_Health;
            set
            {
                _DisplayPopup_Health = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Health"); }
            }
        }
        #endregion
        #region DisplayPopup_Tools
        private bool _DisplayPopup_Tools;
        public bool DisplayPopup_Tools
        {
            get => _DisplayPopup_Tools;
            set
            {
                _DisplayPopup_Tools = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Tools"); }
            }
        }
        #endregion
        #region DisplayPopup_Rest
        private bool _DisplayPopup_Rest;
        [XmlSaveMode(XSME.Single)]
        public bool DisplayPopup_Rest
        {
            get => _DisplayPopup_Rest;
            set
            {
                _DisplayPopup_Rest = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Rest"); }
            }
        }
        #endregion
        #region DisplayPopup_Tables
        private bool _DisplayPopup_Tables;
        public bool DisplayPopup_Tables
        {
            get => _DisplayPopup_Tables;
            set
            {
                _DisplayPopup_Tables = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("Tables"); }
            }
        }
        #endregion
        #region DisplayPopup_StandardActions
        private bool _DisplayPopup_StandardActions;
        public bool DisplayPopup_StandardActions
        {
            get => _DisplayPopup_StandardActions;
            set
            {
                _DisplayPopup_StandardActions = value;
                NotifyPropertyChanged();
                if (value) { ClosePopupsOtherThan("StandardActions"); }
            }
        }
        #endregion

        #region CustomDiceQuantity
        private int _CustomDiceQuantity;
        public int CustomDiceQuantity
        {
            get => _CustomDiceQuantity;
            set => SetAndNotify(ref _CustomDiceQuantity, value);
        }
        #endregion
        #region CustomDiceSides
        private int _CustomDiceSides;
        public int CustomDiceSides
        {
            get => _CustomDiceSides;
            set => SetAndNotify(ref _CustomDiceSides, value);
        }
        #endregion
        #region CustomDiceMod
        private int _CustomDiceMod;
        public int CustomDiceMod
        {
            get => _CustomDiceMod;
            set => SetAndNotify(ref _CustomDiceMod, value);
        }
        #endregion

        #region ToolsInInventory
        private ObservableCollection<ItemModel> _ToolsInInventory;
        public ObservableCollection<ItemModel> ToolsInInventory
        {
            get => _ToolsInInventory;
            set => SetAndNotify(ref _ToolsInInventory, value);
        }
        #endregion

        #region Minions
        private ObservableCollection<CreatureModel> _Minions;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CreatureModel> Minions
        {
            get => _Minions;
            set => SetAndNotify(ref _Minions, value);
        }
        #endregion

        #region Counters
        private ObservableCollection<CounterModel> _Counters;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CounterModel> Counters
        {
            get => _Counters;
            set => SetAndNotify(ref _Counters, value);
        }
        #endregion

        #region Abilities
        private ObservableCollection<CustomAbility> _Abilities;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CustomAbility> Abilities
        {
            get => _Abilities;
            set => SetAndNotify(ref _Abilities, value);
        }
        #endregion
        #region Alterants
        private ObservableCollection<CharacterAlterant> _Alterants;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CharacterAlterant> Alterants
        {
            get => _Alterants;
            set => SetAndNotify(ref _Alterants, value);
        }
        #endregion

        #region CombatNotes
        private string _CombatNotes;
        [XmlSaveMode(XSME.Single)]
        public string CombatNotes
        {
            get => _CombatNotes;
            set => SetAndNotify(ref _CombatNotes, value);
        }
        #endregion

        #region CustomDiceSets
        private ObservableCollection<CustomDiceModel> _CustomDiceSets;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CustomDiceModel> CustomDiceSets
        {
            get => _CustomDiceSets;
            set => SetAndNotify(ref _CustomDiceSets, value);
        }
        #endregion

        #region RollTables
        private List<RollTableModel> _RollTables;
        public List<RollTableModel> RollTables
        {
            get => _RollTables;
            set => SetAndNotify(ref _RollTables, value);
        }
        #endregion

        #region DeathSave1
        private bool _DeathSave1;
        public bool DeathSave1
        {
            get => _DeathSave1;
            set => SetAndNotify(ref _DeathSave1, value);
        }
        #endregion
        #region DeathSave2
        private bool _DeathSave2;
        public bool DeathSave2
        {
            get => _DeathSave2;
            set => SetAndNotify(ref _DeathSave2, value);
        }
        #endregion
        #region DeathSave3
        private bool _DeathSave3;
        public bool DeathSave3
        {
            get => _DeathSave3;
            set => SetAndNotify(ref _DeathSave3, value);
        }
        #endregion
        #region DeathFail1
        private bool _DeathFail1;
        public bool DeathFail1
        {
            get => _DeathFail1;
            set => SetAndNotify(ref _DeathFail1, value);
        }
        #endregion
        #region DeathFail2
        private bool _DeathFail2;
        public bool DeathFail2
        {
            get => _DeathFail2;
            set => SetAndNotify(ref _DeathFail2, value);
        }
        #endregion
        #region DeathFail3
        private bool _DeathFail3;
        public bool DeathFail3
        {
            get => _DeathFail3;
            set => SetAndNotify(ref _DeathFail3, value);
        }
        #endregion

        #region IsBlinded
        private bool _IsBlinded;
        [XmlSaveMode(XSME.Single)]
        public bool IsBlinded
        {
            get => _IsBlinded;
            set => SetAndNotify(ref _IsBlinded, value);
        }
        #endregion
        #region IsCharmed
        private bool _IsCharmed;
        [XmlSaveMode(XSME.Single)]
        public bool IsCharmed
        {
            get => _IsCharmed;
            set => SetAndNotify(ref _IsCharmed, value);
        }
        #endregion
        #region IsDeafened
        private bool _IsDeafened;
        [XmlSaveMode(XSME.Single)]
        public bool IsDeafened
        {
            get => _IsDeafened;
            set => SetAndNotify(ref _IsDeafened, value);
        }
        #endregion
        #region IsFrightened
        private bool _IsFrightened;
        [XmlSaveMode(XSME.Single)]
        public bool IsFrightened
        {
            get => _IsFrightened;
            set => SetAndNotify(ref _IsFrightened, value);
        }
        #endregion
        #region IsGrappled
        private bool _IsGrappled;
        [XmlSaveMode(XSME.Single)]
        public bool IsGrappled
        {
            get => _IsGrappled;
            set => SetAndNotify(ref _IsGrappled, value);
        }
        #endregion
        #region IsIncapacitated
        private bool _IsIncapacitated;
        [XmlSaveMode(XSME.Single)]
        public bool IsIncapacitated
        {
            get => _IsIncapacitated;
            set => SetAndNotify(ref _IsIncapacitated, value);
        }
        #endregion
        #region IsInvisible
        private bool _IsInvisible;
        [XmlSaveMode(XSME.Single)]
        public bool IsInvisible
        {
            get => _IsInvisible;
            set => SetAndNotify(ref _IsInvisible, value);
        }
        #endregion
        #region IsParalyzed
        private bool _IsParalyzed;
        [XmlSaveMode(XSME.Single)]
        public bool IsParalyzed
        {
            get => _IsParalyzed;
            set
            {
                _IsParalyzed = value;
                NotifyPropertyChanged();
                if (value == true) { IsIncapacitated = true; }
            }
        }
        #endregion
        #region IsPetrified
        private bool _IsPetrified;
        [XmlSaveMode(XSME.Single)]
        public bool IsPetrified
        {
            get => _IsPetrified;
            set
            {
                _IsPetrified = value;
                NotifyPropertyChanged();
                if (value == true) { IsIncapacitated = true; }
            }
        }
        #endregion
        #region IsPoisoned
        private bool _IsPoisoned;
        [XmlSaveMode(XSME.Single)]
        public bool IsPoisoned
        {
            get => _IsPoisoned;
            set => SetAndNotify(ref _IsPoisoned, value);
        }
        #endregion
        #region IsProne
        private bool _IsProne;
        [XmlSaveMode(XSME.Single)]
        public bool IsProne
        {
            get => _IsProne;
            set => SetAndNotify(ref _IsProne, value);
        }
        #endregion
        #region IsRestrained
        private bool _IsRestrained;
        [XmlSaveMode(XSME.Single)]
        public bool IsRestrained
        {
            get => _IsRestrained;
            set => SetAndNotify(ref _IsRestrained, value);
        }
        #endregion
        #region IsStunned
        private bool _IsStunned;
        [XmlSaveMode(XSME.Single)]
        public bool IsStunned
        {
            get => _IsStunned;
            set
            {
                _IsStunned = value;
                NotifyPropertyChanged();
                if (value == true) { IsIncapacitated = true; }
            }
        }
        #endregion
        #region IsUnconscious
        private bool _IsUnconscious;
        [XmlSaveMode(XSME.Single)]
        public bool IsUnconscious
        {
            get => _IsUnconscious;
            set
            {
                _IsUnconscious = value;
                NotifyPropertyChanged();
                if (value == true) { IsIncapacitated = true; }
            }
        }
        #endregion
        #region ExhaustionLevel
        private int _ExhaustionLevel;
        [XmlSaveMode(XSME.Single)]
        public int ExhaustionLevel
        {
            get => _ExhaustionLevel;
            set => SetAndNotify(ref _ExhaustionLevel, value);
        }
        #endregion
        #region IntoxicationLevel
        private int _IntoxicationLevel;
        [XmlSaveMode(XSME.Single)]
        public int IntoxicationLevel
        {
            get => _IntoxicationLevel;
            set => SetAndNotify(ref _IntoxicationLevel, value);
        }
        #endregion

        // Databound Properties - Traits
        #region TraitEditModeEnabled
        private bool _TraitEditModeEnabled;
        public bool TraitEditModeEnabled
        {
            get => _TraitEditModeEnabled;
            set => SetAndNotify(ref _TraitEditModeEnabled, value);
        }
        #endregion
        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<TraitModel> Traits
        {
            get => _Traits;
            set => SetAndNotify(ref _Traits, value);
        }
        #endregion

        // Databound Properties - Spellcasting
        #region IsConcentrating
        private bool _IsConcentrating;
        [XmlSaveMode(XSME.Single)]
        public bool IsConcentrating
        {
            get => _IsConcentrating;
            set
            {
                bool brokeConcentration = (_IsConcentrating == true && value == false);
                _IsConcentrating = value;
                NotifyPropertyChanged();
                if (brokeConcentration)
                {
                    HelperMethods.AddToGameplayLog("Concentration has been broken.");
                }
            }
        }
        #endregion
        #region SpellcastingAbility
        private string _SpellcastingAbility;
        [XmlSaveMode(XSME.Single)]
        public string SpellcastingAbility
        {
            get => _SpellcastingAbility;
            set
            {
                _SpellcastingAbility = value;
                NotifyPropertyChanged();
                SetSpellcastingStats();
            }
        }
        #endregion
        #region SpellSaveDc
        private int _SpellSaveDc;
        [XmlSaveMode(XSME.Single)]
        public int SpellSaveDc
        {
            get => _SpellSaveDc;
            set => SetAndNotify(ref _SpellSaveDc, value);
        }
        #endregion
        #region SpellAttackBonus
        private int _SpellAttackBonus;
        [XmlSaveMode(XSME.Single)]
        public int SpellAttackBonus
        {
            get => _SpellAttackBonus;
            set => SetAndNotify(ref _SpellAttackBonus, value);
        }
        #endregion
        #region SpellAbilityModifier
        private int _SpellAbilityModifier;
        [XmlSaveMode(XSME.Single)]
        public int SpellAbilityModifier
        {
            get => _SpellAbilityModifier;
            set => SetAndNotify(ref _SpellAbilityModifier, value);
        }
        #endregion

        #region SpellPreparedCount
        private int _SpellPreparedCount;
        public int SpellPreparedCount
        {
            get => _SpellPreparedCount;
            set => SetAndNotify(ref _SpellPreparedCount, value);
        }
        #endregion
        #region SpellLinks
        private ObservableCollection<SpellLink> _SpellLinks;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<SpellLink> SpellLinks
        {
            get => _SpellLinks;
            set => SetAndNotify(ref _SpellLinks, value);
        }
        #endregion

        #region ActiveEffectAbilities
        private ObservableCollection<CustomAbility> _ActiveEffectAbilities;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CustomAbility> ActiveEffectAbilities
        {
            get => _ActiveEffectAbilities;
            set => SetAndNotify(ref _ActiveEffectAbilities, value);
        }
        #endregion

        // Databound Properties - Inventory - Equipment
        #region MainHandItem
        private string _MainHandItem;
        [XmlSaveMode(XSME.Single)]
        public string MainHandItem
        {
            get => _MainHandItem;
            set
            {
                _MainHandItem = value;
                NotifyPropertyChanged();
                MainHandLinkedItem = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == value);
            }
        }
        #endregion
        #region MainHandLinkedItem
        private ItemModel _MainHandLinkedItem;
        
        public ItemModel MainHandLinkedItem
        {
            get => _MainHandLinkedItem;
            set
            {
                _MainHandLinkedItem = value;
                NotifyPropertyChanged();
                UpdateArmorClass();
            }
        }
        #endregion
        #region OffHandItem
        private string _OffHandItem;
        [XmlSaveMode(XSME.Single)]
        public string OffHandItem
        {
            get => _OffHandItem;
            set
            {
                _OffHandItem = value;
                NotifyPropertyChanged();
                OffHandLinkedItem = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == value);
            }
        }
        #endregion
        #region OffHandLinkedItem
        private ItemModel _OffHandLinkedItem;
        
        public ItemModel OffHandLinkedItem
        {
            get => _OffHandLinkedItem;
            set
            {
                _OffHandLinkedItem = value;
                NotifyPropertyChanged();
                UpdateArmorClass();
            }
        }
        #endregion
        #region ArmorItem
        private string _ArmorItem;
        [XmlSaveMode(XSME.Single)]
        public string ArmorItem
        {
            get => _ArmorItem;
            set
            {
                _ArmorItem = value;
                NotifyPropertyChanged();
                ArmorLinkedItem = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == value);
            }
        }
        #endregion
        #region ArmorLinkedItem
        private ItemModel _ArmorLinkedItem;
        
        public ItemModel ArmorLinkedItem
        {
            get => _ArmorLinkedItem;
            set
            {
                _ArmorLinkedItem = value;
                NotifyPropertyChanged();
                UpdateArmorClass();
            }
        }
        #endregion
        #region AttunedItemA
        private string _AttunedItemA;
        [XmlSaveMode(XSME.Single)]
        public string AttunedItemA
        {
            get => _AttunedItemA;
            set
            {
                _AttunedItemA = value;
                NotifyPropertyChanged();
                AttunedLinkedItemA = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == value);
            }
        }
        #endregion
        #region AttunedItemB
        private string _AttunedItemB;
        [XmlSaveMode(XSME.Single)]
        public string AttunedItemB
        {
            get => _AttunedItemB;
            set
            {
                _AttunedItemB = value;
                NotifyPropertyChanged();
                AttunedLinkedItemB = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == value);
            }
        }
        #endregion
        #region AttunedItemC
        private string _AttunedItemC;
        [XmlSaveMode(XSME.Single)]
        public string AttunedItemC
        {
            get => _AttunedItemC;
            set
            {
                _AttunedItemC = value;
                NotifyPropertyChanged();
                AttunedLinkedItemC = Configuration.MainModelRef.ItemBuilderView.AllItems.FirstOrDefault(i => i.Name == value);
            }
        }
        #endregion
        #region AttunedLinkedItemA
        private ItemModel _AttunedLinkedItemA;
        public ItemModel AttunedLinkedItemA
        {
            get => _AttunedLinkedItemA;
            set => SetAndNotify(ref _AttunedLinkedItemA, value);
        }
        #endregion
        #region AttunedLinkedItemB
        private ItemModel _AttunedLinkedItemB;
        public ItemModel AttunedLinkedItemB
        {
            get => _AttunedLinkedItemB;
            set => SetAndNotify(ref _AttunedLinkedItemB, value);
        }
        #endregion
        #region AttunedLinkedItemC
        private ItemModel _AttunedLinkedItemC;
        public ItemModel AttunedLinkedItemC
        {
            get => _AttunedLinkedItemC;
            set => SetAndNotify(ref _AttunedLinkedItemC, value);
        }
        #endregion
        #region EquippedAccessories
        private ObservableCollection<ItemLink> _EquippedAccessories;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemLink> EquippedAccessories
        {
            get => _EquippedAccessories;
            set => SetAndNotify(ref _EquippedAccessories, value);
        }
        #endregion

        // Databound Properties - Inventory - Base
        #region CarryingCapacity
        private decimal _CarryingCapacity;
        public decimal CarryingCapacity
        {
            get => _CarryingCapacity;
            set => SetAndNotify(ref _CarryingCapacity, value);
        }
        #endregion
        #region EncumbranceStatus
        private string _EncumbranceStatus;
        public string EncumbranceStatus
        {
            get => _EncumbranceStatus;
            set => SetAndNotify(ref _EncumbranceStatus, value);
        }
        #endregion
        #region TotalWeight
        private decimal _TotalWeight;
        public decimal TotalWeight
        {
            get => _TotalWeight;
            set => SetAndNotify(ref _TotalWeight, value);
        }
        #endregion
        #region TotalValue
        private string _TotalValue;
        public string TotalValue
        {
            get => _TotalValue;
            set => SetAndNotify(ref _TotalValue, value);
        }
        #endregion

        // Databound Properties - Inventory - 1.23.00
        #region CarriedWeight
        private decimal _CarriedWeight;
        public decimal CarriedWeight
        {
            get => _CarriedWeight;
            set => SetAndNotify(ref _CarriedWeight, value);
        }
        #endregion
        #region CarriedCurrency
        private string _CarriedCurrency;
        public string CarriedCurrency
        {
            get => _CarriedCurrency;
            set => SetAndNotify(ref _CarriedCurrency, value);
        }
        #endregion
        #region Inventories
        private ObservableCollection<InventoryModel> _Inventories;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<InventoryModel> Inventories
        {
            get => _Inventories;
            set => SetAndNotify(ref _Inventories, value);
        }
        #endregion

        // Databound Properties - Inventory - Crafting
        #region CraftingBench
        private ObservableCollection<ItemModel> _CraftingBench;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> CraftingBench
        {
            get => _CraftingBench;
            set => SetAndNotify(ref _CraftingBench, value);
        }
        #endregion

        // Databound Properties - Inventory - Enchanting
        #region EnchantingTable
        private ObservableCollection<ItemModel> _EnchantingTable;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> EnchantingTable
        {
            get => _EnchantingTable;
            set => SetAndNotify(ref _EnchantingTable, value);
        }
        #endregion

        // Databound Properties - Inventory - Animal Taming
        #region CreaturePen
        private ObservableCollection<CreatureModel> _CreaturePen;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CreatureModel> CreaturePen
        {
            get => _CreaturePen;
            set => SetAndNotify(ref _CreaturePen, value);
        }
        #endregion

        // Databound Properties - Inventory - Alchemy
        #region HerbalismEnvironment
        private string _HerbalismEnvironment;
        public string HerbalismEnvironment
        {
            get => _HerbalismEnvironment;
            set => SetAndNotify(ref _HerbalismEnvironment, value);
        }
        #endregion
        #region HerbalismResultText
        private string _HerbalismResultText;
        public string HerbalismResultText
        {
            get => _HerbalismResultText;
            set => SetAndNotify(ref _HerbalismResultText, value);
        }
        #endregion

        // Databound Properties - Inventory - Fishing
        #region FishingEnvironment
        private string _FishingEnvironment;
        [XmlSaveMode(XSME.Single)]
        public string FishingEnvironment
        {
            get => _FishingEnvironment;
            set => SetAndNotify(ref _FishingEnvironment, value);
        }
        #endregion
        #region FishingBonus
        private int _FishingBonus;
        [XmlSaveMode(XSME.Single)]
        public int FishingBonus
        {
            get => _FishingBonus;
            set => SetAndNotify(ref _FishingBonus, value);
        }
        #endregion

        // Databound Properties - Notes
        #region Notes
        private ObservableCollection<NoteModel> _Notes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<NoteModel> Notes
        {
            get => _Notes;
            set => SetAndNotify(ref _Notes, value);
        }
        #endregion
        #region ActiveNote
        private NoteModel _ActiveNote;
        public NoteModel ActiveNote
        {
            get => _ActiveNote;
            set => SetAndNotify(ref _ActiveNote, value);
        }
        #endregion

        // Databound Properties - Misc
        #region OnInfoTab
        private bool _OnInfoTab;
        public bool OnInfoTab
        {
            get => _OnInfoTab;
            set => SetAndNotify(ref _OnInfoTab, value);
        }
        #endregion
        #region OnSkillsTab
        private bool _OnSkillsTab;
        public bool OnSkillsTab
        {
            get => _OnSkillsTab;
            set => SetAndNotify(ref _OnSkillsTab, value);
        }
        #endregion
        #region OnCombatTab
        private bool _OnCombatTab;
        public bool OnCombatTab
        {
            get => _OnCombatTab;
            set => SetAndNotify(ref _OnCombatTab, value);
        }
        #endregion
        #region OnMinionsTab
        private bool _OnMinionsTab;
        public bool OnMinionsTab
        {
            get => _OnMinionsTab;
            set => SetAndNotify(ref _OnMinionsTab, value);
        }
        #endregion
        #region OnTraitsTab
        private bool _OnTraitsTab;
        public bool OnTraitsTab
        {
            get => _OnTraitsTab;
            set => SetAndNotify(ref _OnTraitsTab, value);
        }
        #endregion
        #region OnSpellcastingTab
        private bool _OnSpellcastingTab;
        public bool OnSpellcastingTab
        {
            get => _OnSpellcastingTab;
            set => SetAndNotify(ref _OnSpellcastingTab, value);
        }
        #endregion
        #region OnInventoryTab
        private bool _OnInventoryTab;
        public bool OnInventoryTab
        {
            get => _OnInventoryTab;
            set => SetAndNotify(ref _OnInventoryTab, value);
        }
        #endregion
        #region OnNotesTab
        private bool _OnNotesTab;
        public bool OnNotesTab
        {
            get => _OnNotesTab;
            set => SetAndNotify(ref _OnNotesTab, value);
        }
        #endregion
        #region ShowActionHistory
        private bool _ShowActionHistory;
        public bool ShowActionHistory
        {
            get => _ShowActionHistory;
            set => SetAndNotify(ref _ShowActionHistory, value);
        }
        #endregion

        // Private Properties
        private int _DeathSaves;
        private int _DeathFails;

        // Commands
        #region PlayerRoll
        public ICommand PlayerRoll => new RelayCommand(DoPlayerRoll);
        private void DoPlayerRoll(object parameter)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            string[] parts = parameter.ToString().Split(',');
            string message;
            int modifier;
            int finalRoll;
            int total;
            string rollType = parts[0];
            bool useAdvantage = false;
            bool useDisadvantage = false;
            if (parts.Length > 1)
            {
                string adv = parts[1].ToString();
                if (adv == "Advantage") { useAdvantage = true; }
                if (adv == "Disadvantage") { useDisadvantage = true; }
            }

            switch (rollType)
            {
                case "StrengthCheck":
                    modifier = StrengthModifier;
                    message = "strength check";
                    break;
                case "DexterityCheck":
                    modifier = DexterityModifier;
                    message = "dexterity check";
                    break;
                case "ConstitutionCheck":
                    modifier = ConstitutionModifier;
                    message = "constitution check";
                    break;
                case "IntelligenceCheck":
                    modifier = IntelligenceModifier;
                    message = "intelligence check";
                    break;
                case "WisdomCheck":
                    modifier = WisdomModifier;
                    message = "wisdom check";
                    break;
                case "CharismaCheck":
                    modifier = CharismaModifier;
                    message = "charisma check";
                    break;
                case "StrengthSave":
                    modifier = StrengthSave;
                    message = "strength save";
                    break;
                case "DexteritySave":
                    modifier = DexteritySave;
                    message = "dexterity save";
                    break;
                case "ConstitutionSave":
                    modifier = ConstitutionSave;
                    message = "constitution save";
                    break;
                case "IntelligenceSave":
                    modifier = IntelligenceSave;
                    message = "intelligence save";
                    break;
                case "WisdomSave":
                    modifier = WisdomSave;
                    message = "wisdom save";
                    break;
                case "CharismaSave":
                    modifier = CharismaSave;
                    message = "charisma save";
                    break;
                case "AcrobaticsCheck":
                    modifier = AcrobaticsModifier;
                    message = "acrobatics check";
                    break;
                case "AnimalHandlingCheck":
                    modifier = AnimalHandlingModifier;
                    message = "animal handling check";
                    break;
                case "ArcanaCheck":
                    modifier = ArcanaModifier;
                    message = "arcana check";
                    break;
                case "AthleticsCheck":
                    modifier = AthleticsModifier;
                    message = "athletics check";
                    break;
                case "DeceptionCheck":
                    modifier = DeceptionModifier;
                    message = "deception check";
                    break;
                case "HistoryCheck":
                    modifier = HistoryModifier;
                    message = "history check";
                    break;
                case "InsightCheck":
                    modifier = InsightModifier;
                    message = "insight check";
                    break;
                case "IntimidationCheck":
                    modifier = IntimidationModifier;
                    message = "intimidation check";
                    break;
                case "InvestigationCheck":
                    modifier = InvestigationModifier;
                    message = "investigation check";
                    break;
                case "MedicineCheck":
                    modifier = MedicineModifier;
                    message = "medicine check";
                    break;
                case "NatureCheck":
                    modifier = NatureModifier;
                    message = "nature check";
                    break;
                case "PerceptionCheck":
                    modifier = PerceptionModifier;
                    message = "perception check";
                    break;
                case "PerformanceCheck":
                    modifier = PerformanceModifier;
                    message = "performance check";
                    break;
                case "PersuasionCheck":
                    modifier = PersuasionModifier;
                    message = "persuasion check";
                    break;
                case "ReligionCheck":
                    modifier = ReligionModifier;
                    message = "religion check";
                    break;
                case "SleightOfHandCheck":
                    modifier = SleightOfHandModifier;
                    message = "sleight of hand check";
                    break;
                case "StealthCheck":
                    modifier = StealthModifier;
                    message = "stealth check";
                    break;
                case "SurvivalCheck":
                    modifier = SurvivalModifier;
                    message = "survival check";
                    break;
                case "SpellAttack":
                    modifier = SpellAttackBonus;
                    message = "spell attack";
                    break;
                case "CustomRoll":
                    string msg = Name + " made a custom roll (" + CustomDiceQuantity + "d" + CustomDiceSides + "+" + CustomDiceMod + ").";
                    string dr = string.Empty;
                    int rslt = 0;
                    for (int i = 0; i < CustomDiceQuantity; i++)
                    {
                        int roll = Configuration.RNG.Next(1, CustomDiceSides + 1);
                        if (i > 0) { dr += " + "; }
                        dr += roll;
                        rslt += roll;
                    }
                    rslt += CustomDiceMod;
                    msg += "\nResult: " + rslt;
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { msg += "\nRoll: [" + dr + "] + " + CustomDiceMod; }
                    HelperMethods.AddToGameplayLog(msg, "Default", true);
                    return;
                default:
                    modifier = 0;
                    message = "ERROR";
                    break;
            }

            finalRoll = HelperMethods.RollD20(useAdvantage, useDisadvantage);

            total = finalRoll + modifier;

            string output = string.Format("{0} made {1} {2}{3}{4}.",
                Name,
                HelperMethods.DoesStartWithVowel(message) ? "an" : "a",
                message,
                (useAdvantage) ? " with advantage" : "",
                (useDisadvantage) ? " with disadvantage" : "");
            output += "\nResult: " + total;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { output += "\nRoll: [" + finalRoll + "] + " + modifier; }

            HelperMethods.AddToGameplayLog(output, "Default", true);

            DisplayPopup_Skills = false;
            DisplayPopup_Checks = false;
            DisplayPopup_Saves = false;

        }
        #endregion
        #region RollInitiative
        public ICommand RollInitiative => new RelayCommand(DoRollInitiative);
        private void DoRollInitiative(object param)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int diceRoll1 = Configuration.RNG.Next(1, 21);
            int diceRoll2 = Configuration.RNG.Next(1, 21);
            int finalRoll = diceRoll1;
            bool useAdvantage = false;
            bool useDisadvantage = false;

            if (param != null)
            {
                if (param.ToString() == "Advantage")
                {
                    useAdvantage = true;
                    finalRoll = (diceRoll1 >= diceRoll2) ? diceRoll1 : diceRoll2;
                }
                if (param.ToString() == "Disadvantage")
                {
                    useDisadvantage = true;
                    finalRoll = (diceRoll1 <= diceRoll2) ? diceRoll1 : diceRoll2;
                }
            }

            int total = finalRoll + DexterityModifier;
            Initiative = total;

            HelperMethods.AddToGameplayLog(string.Format("{0} rolled for initiative{3}{4}.\nResult: {1}{2}",
                Name,
                total,
                (Configuration.MainModelRef.SettingsView.ShowDiceRolls) ? "\nRoll: [" + finalRoll + "] + " + DexterityModifier : "",
                (useAdvantage) ? " with advantage" : "",
                (useDisadvantage) ? " with disadvantage" : ""), "Default", true);

        }
        #endregion
        #region RollDice
        public ICommand RollDice => new RelayCommand(DoRollDice);
        private void DoRollDice(object diceSides)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int result = Configuration.RNG.Next(1, Convert.ToInt32(diceSides) + 1);
            HelperMethods.AddToGameplayLog(Name + " rolls 1d" + diceSides + " and gets " + result + ".", "Default", true);
        }
        #endregion
        #region FlipCoin
        public ICommand FlipCoin => new RelayCommand(param => DoFlipCoin());
        private void DoFlipCoin()
        {
            int result = Configuration.RNG.Next(1, 3);
            HelperMethods.AddToGameplayLog(string.Format("{0} flips a coin and gets {1}.", Name, (result == 1) ? "heads" : "tails"), "Default", true);
        }
        #endregion
        #region ShortRest
        public ICommand ShortRest => new RelayCommand(DoShortRest);
        private void DoShortRest(object param)
        {
            YesNoDialog question = new("Perform a short rest?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            foreach (CounterModel counter in Counters)
            {
                if (counter.ResetOnShortRest) { counter.CurrentValue = counter.MaxValue; }
            }

            IsConcentrating = false;
            HelperMethods.AddToGameplayLog(Name + " took a short rest.");

        }
        #endregion
        #region LongRest
        public ICommand LongRest => new RelayCommand(param => DoLongRest());
        private void DoLongRest()
        {
            YesNoDialog question = new("Perform a long rest?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            CurrentHealth = MaxHealth;
            foreach (HitDiceSet hitDiceSet in HitDiceSets)
            {
                hitDiceSet.CurrentHitDice += hitDiceSet.MaxHitDice / 2;
                if (hitDiceSet.CurrentHitDice > hitDiceSet.MaxHitDice) { hitDiceSet.CurrentHitDice = hitDiceSet.MaxHitDice; }
            }
            L1SpellsAvailable = L1SpellsMax;
            L2SpellsAvailable = L2SpellsMax;
            L3SpellsAvailable = L3SpellsMax;
            L4SpellsAvailable = L4SpellsMax;
            L5SpellsAvailable = L5SpellsMax;
            L6SpellsAvailable = L6SpellsMax;
            L7SpellsAvailable = L7SpellsMax;
            L8SpellsAvailable = L8SpellsMax;
            L9SpellsAvailable = L9SpellsMax;
            foreach (CounterModel counter in Counters)
            {
                if (counter.ResetOnRest) { counter.CurrentValue = counter.MaxValue; }
            }
            foreach (CreatureModel minion in Minions)
            {
                minion.CurrentHitPoints = minion.MaxHitPoints;
            }
            IsConcentrating = false;
            HelperMethods.AddToGameplayLog(Name + " took a long rest and recovered health and spell slots.", "Rest", true);
        }
        #endregion
        #region RollDeathSave
        public ICommand RollDeathSave => new RelayCommand(param => DoRollDeathSave());
        private void DoRollDeathSave()
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int roll = Configuration.RNG.Next(1, 21);
            string message = string.Empty;

            if (roll == 1) { _DeathFails += 2; message += Name + " makes a death save (" + roll + ") and gets two failures."; }
            if (roll >= 2 && roll <= 9) { _DeathFails++; message += Name + " makes a death save (" + roll + ") and gets one failure."; }
            if (roll >= 10 && roll <= 19) { _DeathSaves++; message += Name + " makes a death save (" + roll + ") and gets one success."; }
            if (roll == 20) { _DeathSaves = 0; _DeathFails = 0; CurrentHealth = 1; message += Name + " has recovered."; }

            message += " [" + _DeathSaves + "p/" + _DeathFails + "f]";

            if (_DeathSaves >= 1) { DeathSave1 = true; }
            if (_DeathSaves >= 2) { DeathSave2 = true; }
            if (_DeathSaves >= 3) { DeathSave3 = true; message += "\n" + Name + " has stabilized."; }

            if (_DeathFails >= 1) { DeathFail1 = true; }
            if (_DeathFails >= 2) { DeathFail2 = true; }
            if (_DeathFails >= 3) { DeathFail3 = true; message += "\n" + Name + " has died."; }

            HelperMethods.AddToGameplayLog(message, "Default", true);

        }
        #endregion
        #region RefreshDeathSaves
        public ICommand RefreshDeathSaves => new RelayCommand(param => DoRefreshDeathSaves());
        private void DoRefreshDeathSaves()
        {
            _DeathSaves = 0;
            _DeathFails = 0;
            DeathSave1 = false;
            DeathSave2 = false;
            DeathSave3 = false;
            DeathFail1 = false;
            DeathFail2 = false;
            DeathFail3 = false;
        }
        #endregion
        #region AddMinion
        public ICommand AddMinion => new RelayCommand(DoAddMinion);
        private void DoAddMinion(object creatureType)
        {
            MultiObjectSelectionDialog minionSelect = new(Configuration.CreatureRepository.Where(creature => creature.IsPlayer == false && creature.IsValidated).ToList(), "Creatures");
            if (minionSelect.ShowDialog() == true)
            {
                foreach (CreatureModel selectedCreature in (minionSelect.DataContext as MultiObjectSelectionViewModel).SelectedCreatures)
                {
                    for (int i = 0; i < selectedCreature.QuantityToAdd; i++)
                    {
                        AddCharacterMinion(selectedCreature);
                    }
                }
            }


        }
        #endregion
        #region SortMinions
        public ICommand SortMinions => new RelayCommand(DoSortMinions);
        private void DoSortMinions(object param)
        {
            if (Minions.Count == 0) { return; }
            Minions = new(Minions.OrderBy(m => m.DisplayName));
        }
        #endregion
        #region AddCreatureForTaming
        public ICommand AddCreatureForTaming => new RelayCommand(param => DoAddCreatureForTaming());
        private void DoAddCreatureForTaming()
        {
            ObjectSelectionDialog itemSelect;

            itemSelect = new ObjectSelectionDialog(Configuration.CreatureRepository.Where(creature => 
                creature.IsPlayer == false && 
                creature.IsValidated &&
                creature.CreatureCategory == "Beast" &&
                creature.Attr_Intelligence >= 2 &&
                creature.Alignment.Contains("Evil") == false).ToList());

            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }

                for (int i = 0; i < itemSelect.Quantity; i++)
                {
                    CreatureModel newCreature = HelperMethods.DeepClone(itemSelect.SelectedObject as CreatureModel);
                    newCreature.RollHitPoints(Configuration.MainModelRef.SettingsView.UseAveragedHitPoints);
                    newCreature.DisplayName = newCreature.Name;
                    newCreature.TamingProgress = Convert.ToInt32(newCreature.ExperienceValue * 0.45);
                    CreaturePen.Add(newCreature);
                }

            }
        }
        #endregion
        #region AddAbility
        public ICommand AddAbility => new RelayCommand(DoAddAbility);
        private void DoAddAbility(object param)
        {
            if (param == null) { return; }
            if (param.ToString() == "Blank")
            {
                Abilities.Add(new CustomAbility());
            }
            if (param.ToString() == "Quick Attack")
            {
                Abilities.Add(new());
                Abilities.Last().PopulateFromQuickForm();
            }
            if (param.ToString() == "Quick Save")
            {
                Abilities.Add(new());
                Abilities.Last().PopulateFromQuickForm(true);
            }
        }
        #endregion
        #region PasteAbility
        public ICommand PasteAbility => new RelayCommand(DoPasteAbility);
        private void DoPasteAbility(object param)
        {
            if (Configuration.CopiedAbility == null) { return; }
            Abilities.Add(HelperMethods.DeepClone(Configuration.CopiedAbility));
        }
        #endregion
        #region AddSpellLink
        public ICommand AddSpellLink => new RelayCommand(param => DoAddSpellLink());
        private void DoAddSpellLink()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.SpellRepository.Where(spell => spell.IsValidated).ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (SpellModel spell in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedSpells)
                {
                    bool existingFound = false;
                    foreach (SpellLink sl in SpellLinks)
                    {
                        if (sl.Name == spell.Name)
                        {
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    SpellLinks.Add(new SpellLink { Name = spell.Name, LinkedSpell = spell });
                }
            }
        }
        #endregion
        #region AddCounterToPlayer
        public ICommand AddCounterToPlayer => new RelayCommand(param => DoAddCounterToPlayer());
        private void DoAddCounterToPlayer()
        {
            Counters.Add(new CounterModel());
        }
        #endregion
        #region AddTraitToPlayer
        public ICommand AddTraitToPlayer => new RelayCommand(param => DoAddTraitToPlayer());
        private void DoAddTraitToPlayer()
        {
            Traits.Add(new TraitModel());
            Traits.Last().InEditMode = true;
        }
        #endregion
        #region CraftNewItem
        public ICommand CraftNewItem => new RelayCommand(param => DoCraftNewItem());
        private void DoCraftNewItem()
        {
            ObjectSelectionDialog itemSelect;
            bool hasCraftingTool = false;

            itemSelect = new ObjectSelectionDialog(Configuration.ItemRepository.Where(item => item.IsCraftable == true && item.Type != "Potion" && item.IsValidated).ToList());
            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                ItemModel itemToAdd = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel);

                foreach (ItemModel packItem in Inventories[0].AllItems)
                {
                    if (packItem.Name == itemToAdd.CraftingToolkit) { hasCraftingTool = true; }
                }

                if (hasCraftingTool == false) 
                {
                    HelperMethods.NotifyUser("Missing crafting tool: " + itemToAdd.CraftingToolkit);
                    return;
                }

                string missingItems = string.Empty;

                foreach (ItemModel component in itemToAdd.CraftingComponents)
                {
                    bool haveItem = false;
                    foreach (ItemModel backpackItem in Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == component.Name)
                        {
                            if (backpackItem.Quantity < component.Quantity)
                            {
                                missingItems += "\n" + backpackItem.Quantity + "/" + component.Quantity + " " + component.Name;
                                haveItem = true;
                            }
                            else
                            {
                                haveItem = true;
                            }
                        }
                    }
                    if (haveItem == false)
                    {
                        missingItems += "\n0/" + component.Quantity + " " + component.Name;
                    }
                }

                if (missingItems != "")
                {
                    HelperMethods.NotifyUser(missingItems.Insert(0, "Insufficient crafting materials:"));
                    return;
                }

                foreach (ItemModel component in itemToAdd.CraftingComponents)
                {
                    foreach (ItemModel backpackItem in Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == component.Name)
                        {
                            backpackItem.Quantity -= component.Quantity;
                        }
                    }
                }

                CraftingBench.Add(itemToAdd);

            }
        }
        #endregion
        #region EnchantItem
        public ICommand EnchantItem => new RelayCommand(param => DoEnchantItem());
        private void DoEnchantItem()
        {
            ObjectSelectionDialog itemSelect;
            bool hasCraftingTool = false;
            bool hasBaseItem = false;

            itemSelect = new ObjectSelectionDialog(Configuration.ItemRepository.Where(item => item.CreatableThroughEnchanting == true && item.IsValidated).ToList());
            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                ItemModel itemToAdd = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel);

                foreach (ItemModel packItem in Inventories[0].AllItems)
                {
                    if (packItem.Name == "Imbuing Lens") { hasCraftingTool = true; }
                    if (packItem.Name == itemToAdd.EnchantingBaseItem) { hasBaseItem = true; }
                }

                if (hasCraftingTool == false)
                {
                    HelperMethods.NotifyUser("Missing Imbuing Lens.");
                    return;
                }
                if (hasBaseItem == false)
                {
                    HelperMethods.NotifyUser("Missing base item: " + itemToAdd.EnchantingBaseItem);
                    return;
                }

                string missingRunes = string.Empty;
                foreach (ItemModel rune in itemToAdd.EnchantingRunes)
                {
                    bool haveItem = false;
                    foreach (ItemModel backpackItem in Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == rune.Name)
                        {
                            if (backpackItem.Quantity < rune.Quantity)
                            {
                                missingRunes += "\n" + backpackItem.Quantity + "/" + rune.Quantity + " " + rune.Name;
                                haveItem = true;
                            }
                            else
                            {
                                haveItem = true;
                            }
                        }
                    }
                    if (haveItem == false)
                    {
                        missingRunes += "\n0/" + rune.Quantity + " " + rune.Name;
                    }
                }

                if (missingRunes != "")
                {
                    HelperMethods.NotifyUser(missingRunes.Insert(0, "Insufficient enchanting runes:"));
                    return;
                }

                foreach (ItemModel rune in itemToAdd.EnchantingRunes)
                {
                    foreach (ItemModel backpackItem in Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == rune.Name)
                        {
                            backpackItem.Quantity -= rune.Quantity;
                        }
                    }
                }

                EnchantingTable.Add(itemToAdd);

            }
        }
        #endregion
        #region ClearHistory
        public ICommand ClearHistory => new RelayCommand(param => DoClearHistory());
        private void DoClearHistory()
        {
            ActionHistory = new ObservableCollection<string>();
        }
        #endregion
        #region DeleteCharacter
        public ICommand DeleteCharacter
        {
            get
            {
                return new RelayCommand(param => DoDeleteCharacter());
            }
        }
        private void DoDeleteCharacter()
        {
            Configuration.MainModelRef.CharacterBuilderView.Characters.Remove(this);
        }
        #endregion
        #region FindIngredients
        public ICommand FindIngredients => new RelayCommand(param => DoFindIngredients());
        private void DoFindIngredients()
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            if (Configuration.Environments.Contains(HerbalismEnvironment) == false) { return; }
            HerbalismResultText = string.Empty;
            int ingredientsRoll = Configuration.RNG.Next(1, 5);
            int profBonus = (ToolProficiencies.FirstOrDefault(tool => tool.Name == "Herbalism Kit") != null) ? ProficiencyBonus : 0;
            int numberOfIngredientsFound = ingredientsRoll + profBonus;
            List<ItemModel> environmentIngredients = Configuration.MainModelRef.ItemBuilderView.AllItems.Where(item => item.Type == "Ingredient" && item.Environment == HerbalismEnvironment && item.IsValidated).ToList();
            List<ItemModel> ingredientsFound = new();
            for (int i = 0; i < numberOfIngredientsFound; i++)
            {
                ItemModel newIngredient = HelperMethods.DeepClone(environmentIngredients[Configuration.RNG.Next(1, environmentIngredients.Count)]);
                bool duplicateFound = false;
                foreach (ItemModel ingredient in ingredientsFound)
                {
                    if (ingredient.Name == newIngredient.Name)
                    {
                        duplicateFound = true;
                        ingredient.Quantity++;
                    }
                }
                if (duplicateFound == false)
                {
                    newIngredient.Quantity = 1;
                    ingredientsFound.Add(newIngredient);
                }
            }
            HerbalismResultText = "Found " + numberOfIngredientsFound + " (" + ingredientsRoll + " + " + profBonus + ") ingredients:\n";
            foreach (ItemModel ingredient in ingredientsFound)
            {
                HerbalismResultText += ingredient.Quantity + " x " + ingredient.Name + "\n";
            }

            List<ItemModel> itemsToAdd = new();
            foreach (ItemModel ingredient in ingredientsFound)
            {
                bool duplicateFound = false;
                foreach (ItemModel backpackIngredient in Inventories[0].AllItems)
                {
                    if (ingredient.Name == backpackIngredient.Name)
                    {
                        duplicateFound = true;
                        backpackIngredient.Quantity += ingredient.Quantity;
                    }
                }
                if (duplicateFound == false)
                {
                    itemsToAdd.Add(ingredient);
                }
            }

            foreach (ItemModel ingredient in itemsToAdd)
            {
                Inventories[0].AllItems.Add(ingredient);
                Inventories[0].AllItems.Last().PropertyChanged += DndPlayerModel_PropertyChanged;
                Inventories[0].FilteredItems.Add(Inventories[0].AllItems.Last());

            }

            HerbalismResultText += "Ingredients have been moved to backpack.";

        }
        #endregion
        #region BrewPotion
        public ICommand BrewPotion => new RelayCommand(param => DoBrewPotion());
        private void DoBrewPotion()
        {
            ObjectSelectionDialog itemSelect;

            itemSelect = new ObjectSelectionDialog(Configuration.ItemRepository.Where(item => item.IsCraftable == true && item.Type == "Potion" && item.IsValidated).ToList());
            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                ItemModel potion = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel);
                potion.Quantity = 1;

                foreach (ItemModel component in potion.CraftingComponents)
                {
                    bool haveIngredients = false;
                    foreach (ItemModel item in Inventories[0].AllItems)
                    {
                        if (item.Name == component.Name && component.Quantity <= item.Quantity)
                        {
                            haveIngredients = true;
                        }
                    }
                    if (haveIngredients == false)
                    {
                        HelperMethods.NotifyUser("Insufficient ingredients to craft this potion.");
                        return;
                    }
                }

                string message = string.Empty;
                int roll = Configuration.RNG.Next(1, 21);
                int mod = (IntelligenceModifier > WisdomModifier) ? IntelligenceModifier : WisdomModifier;
                int prof = (ToolProficiencies.FirstOrDefault(tool => tool.Name == "Alchemist's Supplies") != null) ? ProficiencyBonus : 0;
                int total = roll + mod + prof;

                if (total >= potion.CraftingDifficulty)
                {
                    bool duplicateFound = false;
                    foreach (ItemModel item in Inventories[0].AllItems)
                    {
                        if (potion.Name == item.Name)
                        {
                            duplicateFound = true;
                            item.Quantity++;
                            break;
                        }
                    }
                    if (duplicateFound == false)
                    {
                        Inventories[0].AllItems.Add(potion);
                        Inventories[0].AllItems.Last().PropertyChanged += DndPlayerModel_PropertyChanged;
                        Inventories[0].FilteredItems.Add(Inventories[0].AllItems.Last());
                    }
                    message += potion.Name + " crafted successfully and added to backpack.\n";
                }
                else
                {
                    message += potion.Name + " crafting failed.\n";
                }

                message += "Supplies used:\n";

                foreach (ItemModel component in potion.CraftingComponents)
                {
                    message += component.Quantity + " x " + component.Name + "\n";
                    foreach (ItemModel backpackItem in Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == component.Name)
                        {
                            backpackItem.Quantity -= component.Quantity;
                        }
                    }
                }

                HelperMethods.NotifyUser(message);

            }
        }
        #endregion
        #region AddNote
        public ICommand AddNote => new RelayCommand(param => DoAddNote());
        private void DoAddNote()
        {
            Notes.Add(new NoteModel());
            ActiveNote = Notes.Last();
        }
        #endregion
        #region SortNotes
        public ICommand SortNotes => new RelayCommand(param => DoSortNotes());
        private void DoSortNotes()
        {
            Notes = new(SortNoteList(Notes.ToList()));
        }
        #endregion
        #region SortSpellLinks
        public ICommand SortSpellLinks => new RelayCommand(param => DoSortSpellLinks());
        private void DoSortSpellLinks()
        {
            SpellLinks = new(SpellLinks.OrderBy(sl => sl.LinkedSpell.SpellLevel).ThenBy(sl => sl.Name));
        }
        #endregion
        #region AdjustHitPoints
        public ICommand AdjustHitPoints => new RelayCommand(DoAdjustHitPoints);
        private void DoAdjustHitPoints(object amount)
        {
            int hpAmt = Convert.ToInt32(amount);
            if (hpAmt < 0)
            {
                TempHealth += hpAmt;
                if (TempHealth < 0)
                {
                    hpAmt = TempHealth;
                    TempHealth = 0;
                }
                else { return; }
            }
            int newHealth = CurrentHealth + hpAmt;
            if (newHealth < 0) { newHealth = 0; }
            if (newHealth > MaxHealth) { newHealth = MaxHealth; }
            CurrentHealth = newHealth;
        }
        #endregion
        #region PasteNote
        public ICommand PasteNote => new RelayCommand(param => DoPasteNote());
        private void DoPasteNote()
        {
            if (Configuration.CopiedNote == null) { return; }
            Notes.Add(HelperMethods.DeepClone(Configuration.CopiedNote));
        }
        #endregion
        #region SortTraits
        public ICommand SortTraits => new RelayCommand(param => DoSortTraits());
        private void DoSortTraits()
        {
            Traits = new ObservableCollection<TraitModel>(Traits.OrderBy(trait => trait.Name));
        }
        #endregion
        #region AddToolProficiencies
        public ICommand AddToolProficiencies => new RelayCommand(DoAddToolProficiencies);
        private void DoAddToolProficiencies(object param)
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated && item.Type == "Tool").ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    ItemModel itemToAdd = HelperMethods.DeepClone(item);
                    foreach (ItemModel tool in ToolProficiencies)
                    {
                        if (itemToAdd.Name == tool.Name)
                        {
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    ToolProficiencies.Add(itemToAdd);
                }
            }
        }
        #endregion
        #region AddHitDiceSet
        public ICommand AddHitDiceSet => new RelayCommand(param => DoAddHitDiceSet());
        private void DoAddHitDiceSet()
        {
            HitDiceSets.Add(new HitDiceSet());
        }
        #endregion
        #region ExportCharacter
        public ICommand ExportCharacter => new RelayCommand(param => DoExportCharacter());
        private void DoExportCharacter()
        {
            SaveFileDialog saveWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml",
                FileName = this.Name + ".xml",
                InitialDirectory = Environment.CurrentDirectory
            };
            if (saveWindow.ShowDialog() == true)
            {
                XDocument itemDocument = new();
                itemDocument.Add(XmlMethods.ListToXml(new List<CharacterModel> { this }));
                itemDocument.Save(saveWindow.FileName);
                YesNoDialog question = new("Character \"" + this.Name + "\" Exported\nOpen file explorer to file?");
                if (question.ShowDialog() == true)
                {
                    if (question.Answer == false) { return; }
                    string argument = "/select, \"" + saveWindow.FileName + "\"";
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
            }
        }
        #endregion
        #region SearchNotes
        public ICommand SearchNotes => new RelayCommand(param => DoSearchNotes());
        private void DoSearchNotes()
        {
            NoteSearchDialog searchDialog = new();
            if (searchDialog.ShowDialog() == true)
            {
                HelperMethods.CheckNoteSearch(Notes, searchDialog.TBX_SearchText.Text, searchDialog.CBX_UseCaseMatch.IsChecked, searchDialog.CBX_LookInHeader.IsChecked, searchDialog.CBX_LookInContent.IsChecked, out _);
            }
        }
        #endregion
        #region ClearSearchMatches
        public ICommand ClearSearchMatches => new RelayCommand(param => DoClearSearchMatches());
        private void DoClearSearchMatches()
        {
            HelperMethods.ClearNoteSearch(Notes);
        }
        #endregion
        #region AddCustomDice
        public ICommand AddCustomDice => new RelayCommand(param => DoAddCustomDice());
        private void DoAddCustomDice()
        {
            CustomDiceSets.Add(new CustomDiceModel());
        }
        #endregion
        #region PerformStandardAction
        public ICommand PerformStandardAction => new RelayCommand(DoPerformStandardAction);
        private void DoPerformStandardAction(object param)
        {
            string action = param.ToString();
            string msg = string.Empty;
            bool playSound = false;
            switch (action)
            {
                case "Dash":
                    msg = Name + " performs a Dash and gains another " + Speed + " feet of movement.";
                    break;
                case "Disengage":
                    msg = Name + " Disengages. Their movement doesn't provoke opportunity attacks for the rest of the turn.";
                    break;
                case "Dodge":
                    msg = Name + " performs a Dodge. Any attack rolls against them are done at disadvantage and they have advantage on Dexterity saving throws.";
                    break;
                case "Escape Grapple":
                    msg = Name + " attempts to escape a grapple.";
                    int escapeRoll = HelperMethods.RollD20();
                    int mod = (AthleticsModifier > AcrobaticsModifier) ? AthleticsModifier : AcrobaticsModifier;
                    string skill = (AthleticsModifier > AcrobaticsModifier) ? "Athletics" : "Acrobatics";
                    msg += "\n" + skill + " Result: " + (escapeRoll + mod);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\n" + skill + " Roll: [" + escapeRoll + "] + " + mod;
                    }
                    playSound = true;
                    break;
                case "Grapple":
                    msg = Name + " attempts to grapple a creature.";
                    int grappleRoll = HelperMethods.RollD20();
                    msg += "\nAthletics Result: " + (grappleRoll + AthleticsModifier);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\nAthletics Roll: [" + grappleRoll + "] + " + AthleticsModifier;
                    }
                    playSound = true;
                    break;
                case "Help Combat":
                    msg = Name + " helps combat by distracting a target within 5 feet of them. The first ally attack against this target before my next turn has advantage.";
                    break;
                case "Help Task":
                    msg = Name + " helps a creature with a task. The helped creature makes their next ability check for that task with advantage before my next turn.";
                    break;
                case "Hide":
                    msg = Name + " attempts to hide.";
                    int stealthRoll = HelperMethods.RollD20();
                    msg += "\nStealth Result: " + (stealthRoll + StealthModifier);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\nStealth Roll: [" + stealthRoll + "] + " + StealthModifier;
                    }
                    playSound = true;
                    break;
                case "Ready":
                    msg = Name + " readies an action.";
                    break;
                case "Search":
                    msg = Name + " searches for something.";
                    int percepRoll = HelperMethods.RollD20();
                    int investRoll = HelperMethods.RollD20();
                    msg += "\nPerception Result: " + (percepRoll + PerceptionModifier);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\nPerception Roll: [" + percepRoll + "] + " + PerceptionModifier;
                    }
                    msg += "\nInvestigation Result: " + (investRoll + InvestigationModifier);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\nInvestigation Roll: [" + investRoll + "] + " + InvestigationModifier;
                    }
                    playSound = true;
                    break;
                case "Shove Attack":
                    msg = Name + " attempts to shove a creature.";
                    int shoveRoll = HelperMethods.RollD20();
                    msg += "\nAthletics Result: " + (shoveRoll + AthleticsModifier);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\nAthletics Roll: [" + shoveRoll + "] + " + AthleticsModifier;
                    }
                    playSound = true;
                    break;
                case "Shove Resist":
                    msg = Name + " attempts to resist a shove.";
                    int resistRoll = HelperMethods.RollD20();
                    int rmod = (AthleticsModifier > AcrobaticsModifier) ? AthleticsModifier : AcrobaticsModifier;
                    string rskill = (AthleticsModifier > AcrobaticsModifier) ? "Athletics" : "Acrobatics";
                    msg += "\n" + rskill + " Result: " + (resistRoll + rmod);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        msg += "\n" + rskill + " Roll: [" + resistRoll + "] + " + rmod;
                    }
                    playSound = true;
                    break;
                case "Long Jump":
                    msg = Name + " makes a long jump.";
                    msg += "\nRunning Start: " + StrengthScore + " feet";
                    msg += "\nStanding Start: " + (StrengthScore / 2) + " feet";
                    break;
                case "High Jump":
                    msg = Name + " makes a high jump.";
                    msg += "\nRunning Start: " + (3 + StrengthModifier + ((RawHeight * 1.5) / 12)) + " feet";
                    msg += "\nStanding Start: " + (((3 + StrengthModifier) / 2) + ((RawHeight * 1.5) / 12)) + " feet";
                    break;
                default:
                    break;
            }

            if (msg != "")
            {
                if (playSound) { HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll); }
                HelperMethods.AddToGameplayLog(msg, "Default", true);
            }

            DisplayPopup_StandardActions = false;

        }
        #endregion
        #region AddClassLink
        public ICommand AddClassLink => new RelayCommand(param => DoAddClassLink());
        private void DoAddClassLink()
        {
            PlayerClasses.Add(new PlayerClassLinkModel());
        }
        #endregion
        #region GenerateHitDiceFromClasses
        public ICommand GenerateHitDiceFromClasses => new RelayCommand(param => DoGenerateHitDiceFromClasses());
        private void DoGenerateHitDiceFromClasses()
        {
            if (HitDiceSets.Count > 0)
            {
                YesNoDialog question = new("This will overwrite existing hit dice.\nContinue?");
                question.ShowDialog();
                if (question.Answer == false) { return; }
            }

            HitDiceSets.Clear();
            foreach (PlayerClassLinkModel playerClass in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(c => c.Name == playerClass.ClassName);
                if (pc == null) { continue; }
                HitDiceSets.Add(new HitDiceSet { MaxHitDice = playerClass.ClassLevel, CurrentHitDice = playerClass.ClassLevel, HitDiceQuality = pc.HitDice });
            }

        }
        #endregion
        #region GenerateHitPoints
        public ICommand GenerateHitPoints => new RelayCommand(param => DoGenerateHitPoints());
        private void DoGenerateHitPoints()
        {
            if (MaxHealth > 0)
            {
                YesNoDialog question = new("This will overwrite your existing current and maximum hit points.\nContinue?");
                question.ShowDialog();
                if (question.Answer == false) { return; }
            }
            MaxHealth = 0;
            foreach (PlayerClassLinkModel playerClass in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(c => c.Name == playerClass.ClassName);
                if (pc == null) { continue; }

                // Max Hit Points from Hit Dice for First Character Level
                // Hit Dice / 2 + 1 for remaining class levels for primary and secondary classes
                // + Constitution modifier for each class level
                if (PlayerClasses.First() == playerClass)
                {
                    MaxHealth += (((pc.HitDice * (playerClass.ClassLevel - 1)) / 2) + (playerClass.ClassLevel - 1) + (ConstitutionModifier * playerClass.ClassLevel) + pc.HitDice);
                }
                else
                {
                    MaxHealth += (((pc.HitDice * (playerClass.ClassLevel)) / 2) + (playerClass.ClassLevel) + (ConstitutionModifier * playerClass.ClassLevel));
                }
            }
            CurrentHealth = MaxHealth;
        }
        #endregion
        #region GoFishing
        public ICommand GoFishing => new RelayCommand(DoGoFishing);
        private void DoGoFishing(object param)
        {
            if (Inventories[0].AllItems.FirstOrDefault(item => item.Name == "Fishing Tackle") == null)
            {
                YesNoDialog question = new("No fishing tackle available in player backpack, fish anyway?\n(This implies you are borrowing someone else's gear)");
                question.ShowDialog();
                if (question.Answer == false) { return; }
            }
            if (Configuration.FishingEnvironments.Contains(FishingEnvironment) == false)
            {
                HelperMethods.NotifyUser("Please select a valid fishing environment.");
                return;
            }
            if (param == null) { param = string.Empty; }
            bool useAdv = param.ToString() == "Advantage";
            bool useDis = param.ToString() == "Disadvantage";
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int profBonus = (ToolProficiencies.FirstOrDefault(tool => tool.Name == "Fishing Tackle") != null) ? ProficiencyBonus : 0;
            int roll = HelperMethods.RollD20(param.ToString() == "Advantage", param.ToString() == "Disadvantage");
            int result = (roll + profBonus + FishingBonus);
            string message = Name + " used their Fishing Tackle in the " + FishingEnvironment + ((useAdv) ? " with advantage." : (useDis) ? " with disadvantage." : ".");
            message += "\nResult: " + result;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
            {
                message += "\nRoll: [" + roll + "] + " + profBonus + " + " + FishingBonus;
            }
            if (roll == 1) { HelperMethods.AddToGameplayLog(Name + " found nothing while fishing, tough luck!", "Default", true); return; }
            List<ItemModel> fish = new();
            fish.AddRange(Configuration.ItemRepository.Where(item => item.Type == "Adventuring Gear" && item.RawValue <= 10));
            if (result >= 5) { fish.AddRange(Configuration.ItemRepository.Where(item => item.Type == "Fish" && item.FishingEnvironment == FishingEnvironment)); }
            if (result >= 30) { fish.AddRange(Configuration.ItemRepository.Where(item => item.Type == "Wonderous Item" && item.RawValue >= 10000 && item.RawValue <= 50000)); }
            
            int attempts = result switch
            {
                int n when (n >= 10 && n <= 15) => 2,
                int n when (n >= 16 && n <= 20) => 3,
                int n when (n >= 21 && n <= 24) => 4,
                int n when (n >= 25) => 5,
                _ => 1
            };
            if (fish.Count == 0) { fish = Configuration.ItemRepository.Where(item => item.Type == "Fish").ToList(); }
            if (fish.Count == 0) { HelperMethods.NotifyUser("Tell your DM to put fish into the data repository."); return; }
            int fishRoll = 0;
            for (int i = 0; i < attempts; i++) 
            {
                int attempt = Configuration.RNG.Next(0, fish.Count);
                if (fish[attempt].RawValue > fish[fishRoll].RawValue) { fishRoll = attempt; }
            } 
            ItemModel caughtFish = HelperMethods.DeepClone(fish[fishRoll]);
            int catchDc = caughtFish.Weight switch
            {
                decimal n when (n >= 40 && n < 50) => 8,
                decimal n when (n >= 50 && n < 60) => 10,
                decimal n when (n >= 60 && n < 80) => 12,
                decimal n when (n >= 80 && n < 100) => 14,
                decimal n when (n >= 100 && n < 140) => 16,
                decimal n when (n >= 140 && n < 180) => 18,
                decimal n when (n >= 180 && n < 240) => 20,
                decimal n when (n >= 240 && n < 280) => 22,
                decimal n when (n >= 280 && n < 360) => 24,
                decimal n when (n >= 360 && n < 420) => 26,
                decimal n when (n >= 420 && n < 500) => 28,
                decimal n when (n >= 500) => 30,
                _ => 0
            };
            if (catchDc > 0)
            {
                message += "\nSomething heavy is on the hook!";
                int catchRoll = HelperMethods.RollD20(useAdv, useDis);
                int catchResult = catchRoll + StrengthSave;
                if (catchResult < catchDc)
                {
                    message += "\nAlmost caught " + (HelperMethods.DoesStartWithVowel(caughtFish.Name) ? "an" : "a") + " " + caughtFish.Name + " but it got away!";
                    message += "\nCatch DC: " + catchDc;
                    message += "\nCatch Result: " + (catchRoll + StrengthSave);
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        message += "\nCatch Roll: [" + catchRoll + "] + " + StrengthSave;
                    }
                    HelperMethods.AddToGameplayLog(message, "Default", true);
                    return;
                }
            }
            message += "\nCaught " + (HelperMethods.DoesStartWithVowel(caughtFish.Name) ? "an" : "a") + " " + caughtFish.Name + ".";
            bool matchFound = false;
            foreach (ItemModel item in Inventories[0].AllItems)
            {
                if (item.Name == caughtFish.Name)
                {
                    item.Quantity++;
                    matchFound = true;
                    break;
                }
            }
            if (matchFound == false)
            {
                caughtFish.Quantity = 1;
                Inventories[0].AllItems.Add(caughtFish);
                Inventories[0].AllItems.Last().PropertyChanged += DndPlayerModel_PropertyChanged;
                Inventories[0].FilteredItems.Add(caughtFish);
            }
            HelperMethods.AddToGameplayLog(message, "Default", true);

        }
        #endregion
        #region ProcessGroupSave
        public ICommand ProcessGroupSave => new RelayCommand(param => DoProcessGroupSave());
        private void DoProcessGroupSave()
        {
            if (Minions.Count <= 0) { HelperMethods.NotifyUser("You have no minions."); return; }
            EncounterMultiTargetDialog targetDialog = new(Minions.ToList());
            if (targetDialog.ShowDialog() == true)
            {
                if (targetDialog.SelectedCreatures.Count <= 0) { return; }
                string message = "Multiple creatures made a saving throw.";
                message += "\nSave Ability: " + targetDialog.SaveAbility;
                message += "\nSave Difficulty: " + targetDialog.SaveDifficulty;
                if (targetDialog.ComboBox_EffectType.SelectedItem.ToString() == "Attack")
                {
                    message += "\nDamage on Fail: " + targetDialog.PrimaryDamageOnFail + " " + targetDialog.PrimaryDamageType;
                    if (targetDialog.SecondaryDamageOnFail > 0) { message += ", " + targetDialog.SecondaryDamageOnFail + " " + targetDialog.SecondaryDamageType + " damage"; } else { message += " damage"; }
                    message += (targetDialog.HalfOnSave) ? "\nDamage on Save: " + (targetDialog.PrimaryDamageOnFail / 2) + " " + targetDialog.PrimaryDamageType : "";
                    if (targetDialog.SecondaryDamageOnFail > 0 && targetDialog.HalfOnSave) { message += ", " + (targetDialog.SecondaryDamageOnFail / 2) + " " + targetDialog.SecondaryDamageType + " damage"; }
                    else if (targetDialog.SecondaryDamageOnFail == 0 && targetDialog.HalfOnSave) { message += " damage"; }
                }
                foreach (CreatureModel creature in targetDialog.SelectedCreatures)
                {
                    int firstThrow = Configuration.RNG.Next(1, 21);
                    int secondThrow = Configuration.RNG.Next(1, 21);
                    int savingThrow = 0;

                    if (creature.HasGroupSaveAdvantage)
                    {
                        savingThrow = (firstThrow > secondThrow) ? firstThrow : secondThrow;
                    }
                    else if (creature.HasGroupSaveDisadvantage)
                    {
                        savingThrow = (firstThrow < secondThrow) ? firstThrow : secondThrow;
                    }
                    else
                    {
                        savingThrow = firstThrow;
                    }

                    savingThrow += targetDialog.SaveAbility switch
                    {
                        "Strength" => creature.StrengthSave,
                        "Dexterity" => creature.DexteritySave,
                        "Constitution" => creature.ConstitutionSave,
                        "Wisdom" => creature.WisdomSave,
                        "Intelligence" => creature.IntelligenceSave,
                        "Charisma" => creature.CharismaSave,
                        _ => 0
                    };

                    bool passedThrow = (savingThrow >= targetDialog.SaveDifficulty);
                    if (passedThrow)
                    {
                        message += "\n" + creature.DisplayName + ((creature.HasGroupSaveAdvantage) ? " (adv)" : "") + ((creature.HasGroupSaveDisadvantage) ? " (dis)" : "") + ": Pass";
                        creature.CurrentHitPoints -= HelperMethods.CalculateDamage(true, targetDialog, creature);
                    }
                    else
                    {
                        message += "\n" + creature.DisplayName + ((creature.HasGroupSaveAdvantage) ? " (adv)" : "") + ((creature.HasGroupSaveDisadvantage) ? " (dis)" : "") + ": Fail";
                        creature.CurrentHitPoints -= HelperMethods.CalculateDamage(false, targetDialog, creature);
                    }
                    if (creature.CurrentHitPoints <= 0)
                    {
                        creature.CurrentHitPoints = 0;
                        message += "\n" + creature.DisplayName + " has died.";
                        continue;
                    }
                    if (passedThrow) { continue; } // Skip condition check
                    switch (targetDialog.ConditionOnFail)
                    {
                        case "Special":
                            if (creature.Notes.Length > 0) { creature.Notes += "\n"; }
                            creature.Notes += targetDialog.SpecialCondition;
                            break;
                        case "Blinded":
                            creature.IsBlinded = (!creature.IsImmune_Blinded);
                            if (creature.IsBlinded) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Charmed":
                            creature.IsCharmed = (!creature.IsImmune_Charmed);
                            if (creature.IsCharmed) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Deafened":
                            creature.IsDeafened = (!creature.IsImmune_Deafened);
                            if (creature.IsDeafened) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Frightened":
                            creature.IsFrightened = (!creature.IsImmune_Frightened);
                            if (creature.IsFrightened) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Grappled":
                            creature.IsGrappled = (!creature.IsImmune_Grappled);
                            if (creature.IsGrappled) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Paralyzed":
                            creature.IsParalyzed = (!creature.IsImmune_Paralyzed);
                            if (creature.IsParalyzed) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Petrified":
                            creature.IsPetrified = (!creature.IsImmune_Petrified);
                            if (creature.IsPetrified) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Poisoned":
                            creature.IsPoisoned = (!creature.IsImmune_Poisoned);
                            if (creature.IsPoisoned) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Prone":
                            creature.IsProne = (!creature.IsImmune_Prone);
                            if (creature.IsProne) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Restrained":
                            creature.IsRestrained = (!creature.IsImmune_Restrained);
                            if (creature.IsRestrained) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Stunned":
                            creature.IsStunned = (!creature.IsImmune_Stunned);
                            if (creature.IsStunned) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Unconscious":
                            creature.IsUnconscious = (!creature.IsImmune_Unconscious);
                            if (creature.IsUnconscious) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Raise Exhaustion":
                            if (!creature.IsImmune_Exhaustion)
                            {
                                creature.ExhaustionLevel++;
                                message += "\n" + creature.DisplayName + " has raised to level " + creature.ExhaustionLevel + " exhaustion.";
                            }
                            break;
                        default:
                            break;
                    }
                }
                HelperMethods.AddToGameplayLog(message, "Save", true);
            }
        }
        #endregion
        #region AddInventory
        public ICommand AddInventory => new RelayCommand(param => DoAddInventory());
        private void DoAddInventory()
        {
            if (Inventories.Count >= 6) { HelperMethods.NotifyUser("Inventory tab limit is 6."); return; }
            Inventories.Add(new());
        }
        #endregion
        #region ToggleRoll20Link
        public ICommand ToggleRoll20Link => new RelayCommand(DoToggleRoll20Link);
        private void DoToggleRoll20Link(object param)
        {
            bool desiredState = !OutputLinkedToRoll20;
            if (Configuration.MainModelRef.WebDriver == null && desiredState == true)
            {
                HelperMethods.NotifyUser("The WebDriver is not started or not available.\nGo to Settings > WebDriver > Start WebDriver to start it.");
                return;
            }
            else
            {
                
                if (HelperMethods.SwitchbackActiveCharacter() && desiredState == true) { HelperMethods.AddToGameplayLog(Name + " will now output to web", "Default", true); }
                OutputLinkedToRoll20 = true;
            }
            if (desiredState == false)
            {
                HelperMethods.AddToGameplayLog(Name + " will no longer output to web.");
                OutputLinkedToRoll20 = false;
            }

        }
        #endregion
        #region AddAlterant
        public ICommand AddAlterant => new RelayCommand(DoAddAlterant);
        private void DoAddAlterant(object param)
        {
            Alterants.Add(new());
        }
        #endregion
        #region UnequipItem
        public ICommand UnequipItem => new RelayCommand(DoUnequipItem);
        private void DoUnequipItem(object param)
        {
            if (param == null) { return; }
            switch (param.ToString())
            {
                case "MainHand":
                    MainHandItem = string.Empty;
                    break;
                case "OffHand":
                    OffHandItem = string.Empty;
                    break;
                case "Armor":
                    ArmorItem = string.Empty;
                    break;
                case "AttunedSlotA":
                    AttunedItemA = string.Empty;
                    break;
                case "AttunedSlotB":
                    AttunedItemB = string.Empty;
                    break;
                case "AttunedSlotC":
                    AttunedItemC = string.Empty;
                    break;
                default: break;
            }
        }
        #endregion
        #region ClearMessages
        public ICommand ClearMessages => new RelayCommand(DoClearMessages);
        private void DoClearMessages(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("No parameter passed for CharacterModel.DoClearMessages.", true); return; }
            switch (param.ToString())
            {
                case "All":
                    YesNoDialog question = new("Clear message history?");
                    question.ShowDialog();
                    if (question.Answer == false) { return; }
                    Messages.Clear();
                    break;
                case "After10":
                    Messages = new(Messages.Take(10));
                    break;
                case "After50":
                    Messages = new(Messages.Take(50));
                    break;
                default:
                    HelperMethods.WriteToLogFile("Invalid parameter " + param.ToString() + " passed to CharacterModel.DoClearMessages.", true);
                    return;
            }

        }
        #endregion
        #region GenerateLootList
        public ICommand GenerateLootList => new RelayCommand(DoGenerateLootList);
        private void DoGenerateLootList(object param)
        {
            string message = "Character's Carried Loot:";
            int totalGoldDrop = 0;
            int totalSilverDrop = 0;
            int totalCopperDrop = 0;
            Dictionary<string, int> lootedItems = new();

            foreach (InventoryModel inventory in Inventories)
            {
                if (!inventory.IsCarried) { continue; }
                totalCopperDrop += inventory.CopperPieces;
                totalSilverDrop += inventory.SilverPieces;
                totalGoldDrop += inventory.GoldPieces;
                foreach (ItemModel item in inventory.AllItems)
                {
                    if (lootedItems.ContainsKey(item.Name)) { lootedItems[item.Name] += item.Quantity; }
                    else { lootedItems.Add(item.Name, item.Quantity); }
                }
            }

            message += string.Format("\nMoney: {0}{1}{2}{3}{4}",
                (totalGoldDrop > 0) ? totalGoldDrop + "gp" : "",
                (totalGoldDrop > 0 && totalSilverDrop > 0) ? " " : "",
                (totalSilverDrop > 0) ? totalSilverDrop + "sp" : "",
                (totalSilverDrop > 0 && totalCopperDrop > 0) ? " " : "",
                (totalCopperDrop > 0) ? totalCopperDrop + "cp" : "");
            foreach (KeyValuePair<string, int> item in lootedItems)
            {
                message += "\n" + item.Value + " x " + item.Key;
            }

            if (totalGoldDrop == 0 && totalSilverDrop == 0 && totalCopperDrop == 0 && lootedItems.Count == 0) { message = "No loot found."; }

            HelperMethods.AddToGameplayLog(message, "Loot");

        }
        #endregion

        public ICommand SearchTraits => new RelayCommand(DoSearchTraits);
        private void DoSearchTraits(object param)
        {
            NoteSearchDialog noteSearchDialog = new();
            if (noteSearchDialog.ShowDialog() == true)
            {
                string output = string.Empty;
                bool searchHeader = (bool)noteSearchDialog.CBX_LookInHeader.IsChecked;
                bool searchContent = (bool)noteSearchDialog.CBX_LookInContent.IsChecked;
                bool useCaseMatch = (bool)noteSearchDialog.CBX_UseCaseMatch.IsChecked;
                string text = noteSearchDialog.TBX_SearchText.Text;

                foreach (TraitModel trait in Traits)
                {
                    bool isMatch = false;
                    if (searchHeader == true)
                    {
                        if (useCaseMatch == true)
                        {
                            if (trait.Name.Contains(text)) { isMatch = true; }
                        }
                        else
                        {
                            if (trait.Name.ToUpper().Contains(text.ToUpper())) { isMatch = true; }
                        }
                    }
                    if (searchContent == true)
                    {
                        if (useCaseMatch == true)
                        {
                            if (trait.Description.Contains(text)) { isMatch = true; }
                        }
                        else
                        {
                            if (trait.Description.ToUpper().Contains(text.ToUpper())) { isMatch = true; }
                        }
                    }

                    if (isMatch == true) { output += $"{trait.Name}\n{trait.Description}\n\n"; }

                }

                if (output.Length > 0) { HelperMethods.NotifyUser(output, HelperMethods.UserNotificationType.Report); }
                else { HelperMethods.NotifyUser("No matches found."); }

            }
        }

        // Commands - Character Creator
        #region ChangeBaseAttribute
        public ICommand ChangeBaseAttribute => new RelayCommand(DoChangeBaseAttribute);
        private void DoChangeBaseAttribute(object param)
        {
            if (param == null)
            {
                HelperMethods.WriteToLogFile("No parameter given for CharacterModel.DoChangeBaseAttribute.", true);
                return;
            }
            if (param.ToString().Contains(',') == false)
            {
                HelperMethods.WriteToLogFile("Invalid parameter " + param.ToString() + " given for CharacterModelDoChangeBaseAttribute.\nFormat Expected: \"Attribute,ChangeType\"", true);
                return;
            }
            string attribute = param.ToString().Split(',')[0];
            string changeType = param.ToString().Split(',')[1];
            int nextStr = StrengthBaseScore;
            int nextDex = DexterityBaseScore;
            int nextCon = ConstitutionBaseScore;
            int nextInt = IntelligenceBaseScore;
            int nextWis = WisdomBaseScore;
            int nextCha = CharismaBaseScore;
            if (changeType != "Increment" && changeType != "Decrement")
            {
                HelperMethods.WriteToLogFile("Invalid change type " + changeType + " given for CharacterModelDoChangeBaseAttribute.", true);
                return;
            }
            switch (attribute)
            {
                case "Strength":
                    _ = (changeType == "Increment") ? nextStr++ : nextStr--;
                    break;
                case "Dexterity":
                    _ = (changeType == "Increment") ? nextDex++ : nextDex--;
                    break;
                case "Constitution":
                    _ = (changeType == "Increment") ? nextCon++ : nextCon--;
                    break;
                case "Intelligence":
                    _ = (changeType == "Increment") ? nextInt++ : nextInt--;
                    break;
                case "Wisdom":
                    _ = (changeType == "Increment") ? nextWis++ : nextWis--;
                    break;
                case "Charisma":
                    _ = (changeType == "Increment") ? nextCha++ : nextCha--;
                    break;
                default:
                    HelperMethods.WriteToLogFile("Invalid attribute " + attribute + " given for CharacterModelDoChangeBaseAttribute.", true);
                    return;
            }
            int pointsUsed = 0;
            pointsUsed += HelperMethods.GetPointCostFromScore(nextStr);
            pointsUsed += HelperMethods.GetPointCostFromScore(nextDex);
            pointsUsed += HelperMethods.GetPointCostFromScore(nextCon);
            pointsUsed += HelperMethods.GetPointCostFromScore(nextInt);
            pointsUsed += HelperMethods.GetPointCostFromScore(nextWis);
            pointsUsed += HelperMethods.GetPointCostFromScore(nextCha);
            if (pointsUsed > 27)
            {
                return;
            }
            else
            {
                StrengthBaseScore = nextStr;
                DexterityBaseScore = nextDex;
                ConstitutionBaseScore = nextCon;
                IntelligenceBaseScore = nextInt;
                WisdomBaseScore = nextWis;
                CharismaBaseScore = nextCha;
                UpdateAttributePointsRemaining();
            }
        }
        #endregion
        #region ResetAttributes
        public ICommand ResetAttributes => new RelayCommand(param => DoResetAttributes());
        private void DoResetAttributes()
        {
            YesNoDialog question = new("Reset base attributes to their starting values?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            StrengthBaseScore = 8;
            DexterityBaseScore = 8;
            ConstitutionBaseScore = 8;
            IntelligenceBaseScore = 8;
            WisdomBaseScore = 8;
            CharismaBaseScore = 8;
            BaseAttributePoints = 27;

            LinkedRace.NotifyPropertyChanged();
            LinkedSubrace.NotifyPropertyChanged();

        }
        #endregion

        private void UpdateAttributePointsRemaining()
        {
            int pointsUsed = 0;
            pointsUsed += HelperMethods.GetPointCostFromScore(StrengthBaseScore);
            pointsUsed += HelperMethods.GetPointCostFromScore(DexterityBaseScore);
            pointsUsed += HelperMethods.GetPointCostFromScore(ConstitutionBaseScore);
            pointsUsed += HelperMethods.GetPointCostFromScore(IntelligenceBaseScore);
            pointsUsed += HelperMethods.GetPointCostFromScore(WisdomBaseScore);
            pointsUsed += HelperMethods.GetPointCostFromScore(CharismaBaseScore);
            BaseAttributePoints = 27 - pointsUsed;
        }

        // Public Methods
        public void ConnectItemLinks()
        {
            foreach (ItemLink link in EquippedAccessories)
            {
                link.LinkedItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == link.Name);
            }
        }
        public void AddCharacterMinion(CreatureModel selectedCreature)
        {
            CreatureModel newCreature = HelperMethods.DeepClone(selectedCreature);
            int existingCreatureCount = Minions.Where(creature => creature.Name == newCreature.Name).Count();
            if (existingCreatureCount > 25) { return; }
            newCreature.TrackerIndicator = Configuration.AlphaArray[existingCreatureCount];
            newCreature.RollHitPoints(Configuration.MainModelRef.SettingsView.UseAveragedHitPoints);
            newCreature.DisplayName = newCreature.Name + " " + newCreature.TrackerIndicator;
            newCreature.SetFormattedTexts();
            newCreature.RefreshSpellSlots();
            Minions.Add(newCreature);
        }
        public void DndPlayerModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ItemCollectionChanged(null, null);
        }
        public void SetPropertyChanged()
        {
            foreach (InventoryModel inventory in Inventories)
            {
                foreach (ItemModel item in inventory.AllItems)
                {
                    item.PropertyChanged += DndPlayerModel_PropertyChanged;
                }
            }
        }
        public void UpdateInventoryStats()
        {
            decimal carriedWeight = 0m;
            int carriedCurrency = 0;
            ObservableCollection<ItemModel> tools = new();

            foreach (InventoryModel inventory in Inventories)
            {
                if (inventory.IsCarried == false) { continue; }
                foreach (ItemModel item in inventory.AllItems)
                {
                    carriedWeight += item.Weight * item.Quantity;
                    if (item.Type == "Tool") { tools.Add(item); }
                }

                if (Configuration.MainModelRef.SettingsView.UseCoinWeight) { carriedWeight += ((inventory.PlatinumPieces + inventory.GoldPieces + inventory.SilverPieces + inventory.CopperPieces) * 0.02m); }
                carriedCurrency += (inventory.PlatinumPieces * 1000) + (inventory.GoldPieces * 100) + (inventory.SilverPieces * 10) + inventory.CopperPieces;
                inventory.UpdateInventoryItemValueTotal();

            }

            CarriedWeight = carriedWeight;
            CarriedCurrency = HelperMethods.GetDerivedCoinage(carriedCurrency);
            UpdateEncumbrance();
            UpdateToolsCarried();

        }
        public void UpdateEncumbrance()
        {
            CarryingCapacity = StrengthScore * 15;
            if (HasPowerfulBuild) { CarryingCapacity *= 2; }
            if (Configuration.MainModelRef.SettingsView.UseVariantEncumbrance)
            {
                EncumbranceStatus = (CarriedWeight / CarryingCapacity) switch
                {
                    decimal n when (n >= 0.66m) => "(Heavily Encumbered)",
                    decimal n when (n >= 0.33m) => "(Heavily Encumbered)",
                    _ => "(Unencumbered)"
                };
            }
            else
            {
                EncumbranceStatus = (CarriedWeight >= CarryingCapacity) ? "(Encumbered)" : "(Unencumbered)";
            }
        }
        public void UpdateToolsCarried()
        {
            List<ItemModel> tools = new();
            foreach (InventoryModel inventory in Inventories)
            {
                if (inventory.IsCarried == false) { continue; }
                foreach (ItemModel item in inventory.AllItems)
                {
                    if (item.Type != "Tool") { continue; }
                    tools.Add(item);
                }
            }
            ToolsInInventory = new ObservableCollection<ItemModel>(tools);
        }
        public void UpdateStatus()
        {
            if (MaxHealth == 0) { return; }
            Status = (Convert.ToDouble(CurrentHealth) / Convert.ToDouble(MaxHealth)) switch
            {
                double n when (n >= 0.76) => "Fine",
                double n when (n >= 0.51) => "Bruised",
                double n when (n >= 0.26) => "Bloodied",
                double n when (n >= 0.01) => "Wounded",
                _ => "Dead"
            };
        }
        public void UpdateClassTotals()
        {
            SetTotalLevel();
            UpdateCharacterSheet();
            SetClassAutoText();
            SetSubclassAutoText();
        }
        public void SetTotalLevel()
        {
            int level = 0;
            foreach (PlayerClassLinkModel playerClass in PlayerClasses)
            {
                level += playerClass.ClassLevel;
            }
            TotalLevel = level;
        }
        public void SetClassAutoText()
        {
            ClassAutoText = string.Empty;
            if (PlayerClasses.Count <= 0) { return; }
            if (PlayerClasses.Count == 1)
            {
                ClassAutoText = "Level " + TotalLevel + " " + PlayerClasses.First().ClassName;
                return;
            }
            ClassAutoText = "Level " + TotalLevel + " [";
            foreach (PlayerClassLinkModel playerClass in PlayerClasses)
            {
                ClassAutoText += playerClass.ClassLevel + " " + playerClass.ClassName;
                if (playerClass != PlayerClasses.Last()) { ClassAutoText += ", "; }
            }
            ClassAutoText += "]";
        }
        public void SetSubclassAutoText()
        {
            string text = "(";
            foreach (PlayerClassLinkModel playerClass in PlayerClasses)
            {
                if (playerClass.SubClassName != null && playerClass.SubClassName != "")
                {
                    if (text.Length > 1) { text += ", "; }
                    text += playerClass.SubClassName;
                }
            }
            text += ")";
            if (text.Length > 2) { SubclassAutoText = text; }
        }
        public void UpdatePreparedSpellCount()
        {
            int spellPreparedCount = 0;
            foreach (SpellLink link in SpellLinks)
            {
                if (link.IsPrepared && link.LinkedSpell != null)
                {
                    if (link.LinkedSpell.SpellLevel > 0) { spellPreparedCount++; }
                }
            }
            SpellPreparedCount = spellPreparedCount;
        }
        public void UpdateSubclassLists()
        {
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                pclm.UpdateSubclassList();
            }
        }
        public void ItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateInventoryStats();
        }
        public void ConnectSpellLinks()
        {
            foreach (SpellLink link in SpellLinks)
            {
                link.LinkedSpell = Configuration.SpellRepository.FirstOrDefault(s => s.Name == link.Name);
            }
        }
        public void ReinitializeEventHandlers()
        {
            foreach (InventoryModel inventory in Inventories)
            {
                inventory.AllItems.CollectionChanged += inventory.AllItems_CollectionChanged;
                foreach (ItemModel item in inventory.AllItems)
                {
                    item.PropertyChanged += inventory.InventoryModel_PropertyChanged;
                }
            }
        }
        public bool IsInCarriedItems(string itemName, int qtyRequired)
        {
            int qtyFound = 0;
            foreach (InventoryModel inventory in Inventories)
            {
                if (inventory.IsCarried == false) { continue; }
                foreach (ItemModel item in inventory.AllItems)
                {
                    if (item.Name == itemName)
                    {
                        qtyFound += item.Quantity;
                    }
                }
            }
            return (qtyFound >= qtyRequired);
        }
        public void SetFeatChoices()
        {
            List<BoolOption> feats = new();
            foreach (PlayerFeatModel feat in Configuration.MainModelRef.ToolsView.PlayerFeats)
            {
                feats.Add(new BoolOption { Name = feat.Name, Description = feat.Description });
            }
            foreach (BoolOption option in FeatChoices)
            {
                BoolOption match = feats.FirstOrDefault(f => f.Name == option.Name);
                if (match == null)
                {
                    feats.Add(option);
                }
                else
                {
                    match.Marked = option.Marked;
                }
            }
            foreach (BoolOption feat in feats)
            {
                feat.PropertyChanged += Feat_PropertyChanged;
            }
            FeatChoices = new(feats);
        }
        public bool ValidateCharacterCreation(bool displayNotification = false)
        {
            List<string> errors = new();

            if (Name == "") { errors.Add("Name is blank."); }
            if (TotalLevel == 0) { errors.Add("No character levels selected."); }
            if (TotalLevel >= 20) { errors.Add("Total level exceeds 20."); }
            if (LinkedRace == null) { errors.Add("No Race selected."); }
            if (LinkedSubrace == null) { errors.Add("No Subrace selected."); }
            if (LinkedBackground == null) { errors.Add("No Background selected."); }

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                if (pclm.ClassName == "") { errors.Add("Missing character class"); }
                if (pclm.SubClassName == "") { errors.Add("Missing character subclass"); }
                if (pclm.ClassLevel <= 0 || pclm.ClassLevel >= 20) { errors.Add("Invalid class level."); }
                if ((pclm == PlayerClasses.First()) == false)
                {
                    PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(p => p.Name == pclm.ClassName);
                    if (pc == null) { break; }
                    foreach (FeatureModel feature in pc.Features)
                    {
                        if (feature.FeatureType == "Multiclass Ability Prerequisite - And")
                        {
                            foreach (FeatureData featureData in feature.Choices)
                            {
                                if (featureData.Name == "Strength") 
                                { 
                                    if (featureData.Quantity > StrengthFinalScore) { errors.Add("Insufficient Strength to multiclass into " + pclm.ClassName + "."); }
                                }
                                if (featureData.Name == "Dexterity")
                                {
                                    if (featureData.Quantity > DexterityFinalScore) { errors.Add("Insufficient Dexterity to multiclass into " + pclm.ClassName + "."); }
                                }
                                if (featureData.Name == "Constitution")
                                {
                                    if (featureData.Quantity > ConstitutionFinalScore) { errors.Add("Insufficient Constitution to multiclass into " + pclm.ClassName + "."); }
                                }
                                if (featureData.Name == "Intelligence")
                                {
                                    if (featureData.Quantity > IntelligenceFinalScore) { errors.Add("Insufficient Intelligence to multiclass into " + pclm.ClassName + "."); }
                                }
                                if (featureData.Name == "Wisdom")
                                {
                                    if (featureData.Quantity > WisdomFinalScore) { errors.Add("Insufficient Wisdom to multiclass into " + pclm.ClassName + "."); }
                                }
                                if (featureData.Name == "Charisma")
                                {
                                    if (featureData.Quantity > CharismaFinalScore) { errors.Add("Insufficient Charisma to multiclass into " + pclm.ClassName + "."); }
                                }
                            }
                        }
                        if (feature.FeatureType == "Multiclass Ability Prerequisite - Or")
                        {
                            bool foundMatch = false;
                            string stat = string.Empty;
                            foreach (FeatureData featureData in feature.Choices)
                            {
                                if (featureData.Name == "Strength")
                                {
                                    if (featureData.Quantity <= StrengthFinalScore) { foundMatch = true; } else { stat += "Strength "; }
                                }
                                if (featureData.Name == "Dexterity")
                                {
                                    if (featureData.Quantity <= DexterityFinalScore) { foundMatch = true; } else { stat += "Dexterity "; }
                                }
                                if (featureData.Name == "Constitution")
                                {
                                    if (featureData.Quantity <= ConstitutionFinalScore) { foundMatch = true; } else { stat += "Constitution "; }
                                }
                                if (featureData.Name == "Intelligence")
                                {
                                    if (featureData.Quantity <= IntelligenceFinalScore) { foundMatch = true; } else { stat += "Intelligence "; }
                                }
                                if (featureData.Name == "Wisdom")
                                {
                                    if (featureData.Quantity <= WisdomFinalScore) { foundMatch = true; } else { stat += "Wisdom "; }
                                }
                                if (featureData.Name == "Charisma")
                                {
                                    if (featureData.Quantity <= CharismaFinalScore) { foundMatch = true; } else { stat += "Charisma "; }
                                }
                            }
                            if (foundMatch == false) { errors.Add("Insufficient " + stat + "to multiclass into " + pclm.ClassName); }
                        }
                    }
                }
            }

            if (BaseAttributePoints > 0) { errors.Add("Base attribute points remaining."); }
            if (FeatPoints > 0) { errors.Add("Feat points remaining."); }

            foreach (CharacterAttributeSet characterAttributeSet in CCAttributeSets)
            {
                if (characterAttributeSet.PointsRemaining > 0)
                {
                    errors.Add(characterAttributeSet.Name + " has " + characterAttributeSet.PointsRemaining + " points remaining.");
                }
            }

            if (LinkedRace == null) { errors.Add("No race selected."); }
            if (Subraces.Count > 0 && LinkedSubrace == null) { errors.Add("No subrace selected."); }
            if (LinkedBackground == null) { errors.Add("No background selected."); }

            errors.AddRange(Validate_ChoiceSetCollection(SkillChoiceSegments));
            errors.AddRange(Validate_ChoiceSetCollection(ExpertiseChoiceSegments));
            errors.AddRange(Validate_ChoiceSetCollection(TraitChoiceSegments));
            errors.AddRange(Validate_ChoiceSetCollection(SpellChoiceSegments));
            errors.AddRange(Validate_ChoiceSetCollection(ToolChoiceSegments));
            errors.AddRange(Validate_ChoiceSetCollection(ArmorChoiceSegments));
            errors.AddRange(Validate_ChoiceSetCollection(WeaponChoiceSegments));

            foreach (ChoiceSet choiceSet in AttributeFeatChoices)
            {
                bool isMarked = false;
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked) { isMarked = true; }
                }
                if (isMarked == false)
                {
                    errors.Add(choiceSet.Source + " attribute/feat choice remaining.");
                }
            }

            if (errors.Count > 0 && displayNotification)
            {
                string output = "ERRORS:";
                foreach (string error in errors)
                {
                    output += "\n" + error;
                }
                DisplayCharacterCreationWarning = true;
                HelperMethods.NotifyUser(output, HelperMethods.UserNotificationType.Report);
                YesNoDialog question = new("Potential issues found, complete character creation anyway?\n(a warning icon will be displayed in character screen)");
                question.ShowDialog();
                return question.Answer;
            }

            DisplayCharacterCreationWarning = false;
            return true;

        }
        private static List<string> Validate_ChoiceSetCollection(ObservableCollection<ChoiceSet> choiceSetCollection)
        {
            List<string> messages = new();
            foreach (ChoiceSet choiceSet in choiceSetCollection)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    messages.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }
            return messages;
        }
        public void ResetSpellSlots()
        {
            L1SpellsAvailable = L1SpellsMax;
            L2SpellsAvailable = L2SpellsMax;
            L3SpellsAvailable = L3SpellsMax;
            L4SpellsAvailable = L4SpellsMax;
            L5SpellsAvailable = L5SpellsMax;
            L6SpellsAvailable = L6SpellsMax;
            L7SpellsAvailable = L7SpellsMax;
            L8SpellsAvailable = L8SpellsMax;
            L9SpellsAvailable = L9SpellsMax;
        }
        public bool ImplementCharacterCreationStats()
        {
            List<TraitModel> newTraits = new();
            List<SpellLink> newSpells = new();
            List<string> newTools = new();

            StrengthScore = StrengthFinalScore;
            DexterityScore = DexterityFinalScore;
            ConstitutionScore = ConstitutionFinalScore;
            IntelligenceScore = IntelligenceFinalScore;
            WisdomScore = WisdomFinalScore;
            CharismaScore = CharismaFinalScore;

            MaxHealth = MaxHpCalc;
            if (CurrentHealth == 0) { CurrentHealth = MaxHpCalc; }
            if (Speed == 0) { Speed = (LinkedRace == null) ? 0 : LinkedRace.BaseSpeed; }
            if (ArmorClass == 0) { ArmorClass = 10 + DexterityModifier; }
            if (Darkvision == 0) { Darkvision = (LinkedRace == null) ? 0 : LinkedRace.Darkvision; }

            // SET HIT DICE
            List<HitDiceSet> newHitDice = new();
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(c => c.Name == pclm.ClassName);
                if (pc == null) { continue; }
                bool existingFound = false;
                foreach (HitDiceSet hds in newHitDice)
                {
                    if (hds.HitDiceQuality == pc.HitDice) 
                    { 
                        existingFound = true;
                        hds.MaxHitDice += pclm.ClassLevel;
                        hds.CurrentHitDice += pclm.ClassLevel;
                        break;
                    }
                }
                if (existingFound) { continue; }
                newHitDice.Add(new HitDiceSet { MaxHitDice = pclm.ClassLevel, CurrentHitDice = pclm.ClassLevel, HitDiceQuality = pc.HitDice });
            }
            foreach (HitDiceSet existingHds in HitDiceSets)
            {
                foreach (HitDiceSet newHds in newHitDice)
                {
                    if (newHds.HitDiceQuality == existingHds.HitDiceQuality)
                    {
                        newHds.CurrentHitDice -= (existingHds.MaxHitDice - existingHds.CurrentHitDice);
                    }
                }
            }
            HitDiceSets = new(newHitDice);

            // RESET SKILL PROFS AND EXPS
            IsProf_Acrobatics = false;
            IsProf_AnimalHandling = false;
            IsProf_Arcana = false;
            IsProf_Athletics = false;
            IsProf_Deception = false;
            IsProf_History = false;
            IsProf_Insight = false;
            IsProf_Intimidation = false;
            IsProf_Investigation = false;
            IsProf_Medicine = false;
            IsProf_Nature = false;
            IsProf_Perception = false;
            IsProf_Performance = false;
            IsProf_Persuasion = false;
            IsProf_Religion = false;
            IsProf_SleightOfHand = false;
            IsProf_Stealth = false;
            IsProf_Survival = false;

            IsExpert_Acrobatics = false;
            IsExpert_AnimalHandling = false;
            IsExpert_Arcana = false;
            IsExpert_Athletics = false;
            IsExpert_Deception = false;
            IsExpert_History = false;
            IsExpert_Insight = false;
            IsExpert_Intimidation = false;
            IsExpert_Investigation = false;
            IsExpert_Medicine = false;
            IsExpert_Nature = false;
            IsExpert_Perception = false;
            IsExpert_Performance = false;
            IsExpert_Persuasion = false;
            IsExpert_Religion = false;
            IsExpert_SleightOfHand = false;
            IsExpert_Stealth = false;
            IsExpert_Survival = false;

            // MARK SAVING THROWS AND SKILL PROFICIENCIES
            if (PlayerClasses.Count > 0)
            {
                PlayerClassModel baseClass = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(c => c.Name == PlayerClasses.FirstOrDefault().ClassName);
                if (baseClass != null)
                {
                    foreach (FeatureModel feature in baseClass.Features)
                    {
                        if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                        if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
                    }
                }
            }
            foreach (PlayerClassLinkModel pClass in PlayerClasses)
            {
                PlayerSubclassModel subClass = Configuration.MainModelRef.ToolsView.PlayerSubclasses.FirstOrDefault(sc => sc.Name == pClass.SubClassName);
                if (subClass == null) { continue; }
                foreach (FeatureModel feature in subClass.Features)
                {
                    if (pClass.ClassLevel >= feature.LevelAvailable)
                    {
                        if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                        if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
                    }
                }
            }
            if (LinkedRace != null)
            {
                foreach (FeatureModel feature in LinkedRace.Features)
                {
                    if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                    if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
                }
            }

            if (LinkedSubrace != null)
            {
                foreach (FeatureModel feature in LinkedSubrace.Features)
                {
                    if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                    if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
                }
            }

            if (LinkedBackground != null)
            {
                foreach (FeatureModel feature in LinkedBackground.Features)
                {
                    if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                    if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
                }
            }

            // SKILL PROFICIENCIES - CHOICE
            foreach (ChoiceSet choiceSet in SkillChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked)
                    {
                        switch (choice.Name)
                        {
                            case "Acrobatics": IsProf_Acrobatics = true; break;
                            case "Animal Handling": IsProf_AnimalHandling = true; break;
                            case "Arcana": IsProf_Arcana = true; break;
                            case "Athletics": IsProf_Athletics = true; break;
                            case "Deception": IsProf_Deception = true; break;
                            case "History": IsProf_History = true; break;
                            case "Insight": IsProf_Insight = true; break;
                            case "Intimidation": IsProf_Intimidation = true; break;
                            case "Investigation": IsProf_Investigation = true; break;
                            case "Medicine": IsProf_Medicine = true; break;
                            case "Nature": IsProf_Nature = true; break;
                            case "Perception": IsProf_Perception = true; break;
                            case "Performance": IsProf_Performance = true; break;
                            case "Persuasion": IsProf_Persuasion = true; break;
                            case "Religion": IsProf_Religion = true; break;
                            case "Sleight of Hand": IsProf_SleightOfHand = true; break;
                            case "Stealth": IsProf_Stealth = true; break;
                            case "Survival": IsProf_Survival = true; break;
                            default: HelperMethods.WriteToLogFile("Unhandled skill in ImplementCharacterCreationStats() " + choice.Name, true); return false;
                        }
                    }
                }
            }

            // SKILL Expertise - CHOICE
            foreach (ChoiceSet choiceSet in ExpertiseChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked)
                    {
                        switch (choice.Name)
                        {
                            case "Acrobatics": IsExpert_Acrobatics = true; break;
                            case "Animal Handling": IsExpert_AnimalHandling = true; break;
                            case "Arcana": IsExpert_Arcana = true; break;
                            case "Athletics": IsExpert_Athletics = true; break;
                            case "Deception": IsExpert_Deception = true; break;
                            case "History": IsExpert_History = true; break;
                            case "Insight": IsExpert_Insight = true; break;
                            case "Intimidation": IsExpert_Intimidation = true; break;
                            case "Investigation": IsExpert_Investigation = true; break;
                            case "Medicine": IsExpert_Medicine = true; break;
                            case "Nature": IsExpert_Nature = true; break;
                            case "Perception": IsExpert_Perception = true; break;
                            case "Performance": IsExpert_Performance = true; break;
                            case "Persuasion": IsExpert_Persuasion = true; break;
                            case "Religion": IsExpert_Religion = true; break;
                            case "Sleight of Hand": IsExpert_SleightOfHand = true; break;
                            case "Stealth": IsExpert_Stealth = true; break;
                            case "Survival": IsExpert_Survival = true; break;
                            default: HelperMethods.WriteToLogFile("Unhandled expertise in ImplementCharacterCreationStats() " + choice.Name, true); return false;
                        }
                    }
                }
            }

            // TRAITS
            foreach (ChoiceSet choiceSet in TraitChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked == false) { continue; }
                    newTraits.Add(new() { Name = choiceSet.Source + " " + choice.Name, Description = choice.Description });
                }
            }
            foreach (TraitModel trait in SetTraits)
            {
                newTraits.Add(HelperMethods.DeepClone(trait));
            }
            foreach (TraitModel trait in Traits)
            {
                if (newTraits.FirstOrDefault(t => t.Name == trait.Name) == null) { newTraits.Add(HelperMethods.DeepClone(trait)); }
            }
            Traits = new(newTraits.OrderBy(t => t.Name));

            // SPELLCASTING
            int spellcasterClassesFound = 0;
            int spellcasterLevel = 0;
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel spc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(s => s.Name == pclm.ClassName);
                if (spc == null) { HelperMethods.WriteToLogFile("Invalid class " + pclm.ClassName + " in ImplementCharacterCreationStats", true); return false; }
                if (spc.HasSpellcasting) 
                { 
                    if (spellcasterClassesFound == 0) { SpellcastingAbility = spc.SpellcastingAbility; }
                    spellcasterClassesFound++;
                    spellcasterLevel += spc.MulticlassingSlots switch
                    {
                        "1/3" => pclm.ClassLevel / 3,
                        "1/2" => pclm.ClassLevel / 2,
                        _ => pclm.ClassLevel
                    };
                }
            }
            SpellTableRowModel spellRow = null;
            if (spellcasterClassesFound > 1) // Is a multiclass caster
            {
                spellRow = Configuration.MulticlassSpellSlotTable.First(sr => sr.ClassLevel == spellcasterLevel);
            }
            else if (spellcasterClassesFound == 1)
            {
                foreach (PlayerClassLinkModel pclm in PlayerClasses)
                {
                    PlayerClassModel spc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(s => s.Name == pclm.ClassName);
                    if (spc == null) { HelperMethods.WriteToLogFile("Invalid class " + pclm.ClassName + " in ImplementCharacterCreationStats", true); return false; }
                    if (spc.HasSpellcasting)
                    {
                        spellRow = spc.SpellTableRows.FirstOrDefault(s => s.ClassLevel == pclm.ClassLevel);
                    }
                }
            }
            if (spellRow != null)
            {
                L1SpellsMax = spellRow.SpellSlots_1st;
                L2SpellsMax = spellRow.SpellSlots_2nd;
                L3SpellsMax = spellRow.SpellSlots_3rd;
                L4SpellsMax = spellRow.SpellSlots_4th;
                L5SpellsMax = spellRow.SpellSlots_5th;
                L6SpellsMax = spellRow.SpellSlots_6th;
                L7SpellsMax = spellRow.SpellSlots_7th;
                L8SpellsMax = spellRow.SpellSlots_8th;
                L9SpellsMax = spellRow.SpellSlots_9th;
            }
            foreach (ChoiceSet choiceSet in SpellChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked)
                    {
                        newSpells.Add(new() { Name = choice.Name });
                    }
                }
            }
            foreach (SpellLink spellLink in SpellLinks)
            {
                SpellLink existingSpell = newSpells.FirstOrDefault(s => s.Name == spellLink.Name);
                if (existingSpell == null)
                {
                    newSpells.Add(HelperMethods.DeepClone(spellLink));
                }
                else
                {
                    existingSpell.IsPrepared = spellLink.IsPrepared;
                }
            }
            foreach (SpellLink spellLink in newSpells)
            {
                spellLink.LinkedSpell = Configuration.MainModelRef.SpellBuilderView.AllSpells.FirstOrDefault(s => s.Name == spellLink.Name);
            }
            SpellLinks = new(newSpells.OrderBy(ns => ns.LinkedSpell.SpellLevel).ThenBy(ns => ns.Name));

            // OTHER
            CCI_Tools();
            CCI_Other();
            CCI_Languages();

            // EQUIPMENT
            if (HasCompletedCharacterCreation == false && DisplayCharacterCreationWarning == false)
            {
                Inventories[0].GoldPieces = LinkedBackground.GoldPieces;
                List<ItemModel> items = new();
                foreach (ItemLink item in ChosenEquipment)
                {
                    ItemModel matchedItem = items.FirstOrDefault(i => i.Name == item.Name);
                    if (matchedItem == null)
                    {
                        ItemModel foundItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == item.Name);
                        if (foundItem == null) { HelperMethods.WriteToLogFile("Unable to find item + " + item.Name + " for CC Chosen Equipment.", true); continue; }
                        items.Add(HelperMethods.DeepClone(foundItem));
                        items.Last().Quantity = item.Quantity;
                    }
                    else
                    {
                        matchedItem.Quantity += item.Quantity;
                    }
                }
                foreach (ItemLink item in GrantedEquipment)
                {
                    ItemModel matchedItem = items.FirstOrDefault(i => i.Name == item.Name);
                    if (matchedItem == null)
                    {
                        ItemModel foundItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == item.Name);
                        if (foundItem == null) { HelperMethods.WriteToLogFile("Unable to find item + " + item.Name + " for CC Granted Equipment.", true); continue; }
                        items.Add(HelperMethods.DeepClone(foundItem));
                        items.Last().Quantity = item.Quantity;
                    }
                    else
                    {
                        matchedItem.Quantity += item.Quantity;
                    }
                }
                
                foreach (ItemModel item in items)
                {
                    ItemModel matchedItem = Inventories[0].AllItems.FirstOrDefault(i => i.Name == item.Name);
                    if (matchedItem == null)
                    {
                        Inventories[0].AllItems.Add(HelperMethods.DeepClone(Configuration.ItemRepository.First(i => i.Name == item.Name)));
                        Inventories[0].AllItems.Last().Quantity = item.Quantity;
                    }
                    else
                    {
                        matchedItem.Quantity += item.Quantity;
                    }
                }
                Inventories[0].IsCarried = true;
                Inventories[0].SearchText = string.Empty;

            }

            HasCompletedCharacterCreation = true;
            return true;

        }
        public int GetCalculatedMaxHitPoints(int conMod)
        {
            int hp = 0;
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(c => c.Name == pclm.ClassName);
                if (pc == null) { continue; }
                for (int i = 0; i < pclm.ClassLevel; i++)
                {
                    if (i == 0 && pc.Name == PlayerClasses.First().ClassName) { hp += pc.HitDice; }
                    else { hp += (pc.HitDice / 2) + 1; }
                }
            }
            for (int i = 0; i < TotalLevel; i++)
            {
                hp += conMod;
            }
            if (LinkedSubrace != null)
            {
                foreach (FeatureModel feature in LinkedSubrace.Features)
                {
                    if (feature.FeatureType == "Stat Bonuses - Set")
                    {
                        foreach (FeatureData featureData in feature.Choices)
                        {
                            if (featureData.Name == "Hit Point Maximum per Level")
                            {
                                for (int i = 0; i < TotalLevel; i++)
                                {
                                    hp += featureData.Quantity;
                                }
                            }
                        }
                    }
                }
            }
            foreach (BoolOption choice in FeatChoices)
            {
                if (choice.Marked)
                {
                    PlayerFeatModel matchedFeat = Configuration.MainModelRef.ToolsView.PlayerFeats.FirstOrDefault(f => f.Name == choice.Name);
                    if (matchedFeat == null) { continue; }
                    foreach (FeatureModel feature in matchedFeat.Features)
                    {
                        if (feature.FeatureType == "Stat Bonuses - Set")
                        {
                            foreach (FeatureData featureData in feature.Choices)
                            {
                                if (featureData.Name == "Hit Point Maximum per Level")
                                {
                                    for (int i = 0; i < TotalLevel; i++)
                                    {
                                        hp += featureData.Quantity;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            hp += HitPointMaxBonus;
            return hp;
        }
        public void UpdateModifiers()
        {
            // Get Values Values from Alterants
            UpdateStatBonusesFromAlterants();

            // Update Base Attribute Modifiers
            StrengthModifier = HelperMethods.GetAttributeModifier(StrengthScore + StrengthBonus);
            DexterityModifier = HelperMethods.GetAttributeModifier(DexterityScore + DexterityBonus);
            ConstitutionModifier = HelperMethods.GetAttributeModifier(ConstitutionScore + ConstitutionBonus);
            IntelligenceModifier = HelperMethods.GetAttributeModifier(IntelligenceScore + IntelligenceBonus);
            WisdomModifier = HelperMethods.GetAttributeModifier(WisdomScore + WisdomBonus);
            CharismaModifier = HelperMethods.GetAttributeModifier(CharismaScore + CharismaBonus);

            // Update Save Modifiers
            StrengthSave = StrengthModifier;
            DexteritySave = DexterityModifier;
            ConstitutionSave = ConstitutionModifier;
            IntelligenceSave = IntelligenceModifier;
            WisdomSave = WisdomModifier;
            CharismaSave = CharismaModifier;
            if (HasSave_Strength) { StrengthSave += ProficiencyBonus; }
            if (HasSave_Dexterity) { DexteritySave += ProficiencyBonus; }
            if (HasSave_Constitution) { ConstitutionSave += ProficiencyBonus; }
            if (HasSave_Intelligence) { IntelligenceSave += ProficiencyBonus; }
            if (HasSave_Wisdom) { WisdomSave += ProficiencyBonus; }
            if (HasSave_Charisma) { CharismaSave += ProficiencyBonus; }

            // Update Skill Modifiers
            AcrobaticsModifier = DexterityModifier + AcrobaticsBonus + AllSkillBonus;
            AnimalHandlingModifier = WisdomModifier + AnimalHandlingBonus + AllSkillBonus;
            ArcanaModifier = IntelligenceModifier + ArcanaBonus + AllSkillBonus;
            AthleticsModifier = StrengthModifier + AthleticsBonus + AllSkillBonus;
            DeceptionModifier = CharismaModifier + DeceptionBonus + AllSkillBonus;
            HistoryModifier = IntelligenceModifier + HistoryBonus + AllSkillBonus;
            InsightModifier = WisdomModifier + InsightBonus + AllSkillBonus;
            IntimidationModifier = CharismaModifier + IntimidationBonus + AllSkillBonus;
            InvestigationModifier = IntelligenceModifier + InvestigationBonus + AllSkillBonus;
            MedicineModifier = WisdomModifier + MedicineBonus + AllSkillBonus;
            NatureModifier = IntelligenceModifier + NatureBonus + AllSkillBonus;
            PerceptionModifier = WisdomModifier + PerceptionBonus + AllSkillBonus;
            PerformanceModifier = CharismaModifier + PerformanceBonus + AllSkillBonus;
            PersuasionModifier = CharismaModifier + PersuasionBonus + AllSkillBonus;
            ReligionModifier = IntelligenceModifier + ReligionBonus + AllSkillBonus;
            SleightOfHandModifier = DexterityModifier + SleightOfHandBonus + AllSkillBonus;
            StealthModifier = DexterityModifier + StealthBonus + AllSkillBonus;
            SurvivalModifier = WisdomModifier + SurvivalBonus + AllSkillBonus;
            if (IsProf_Acrobatics) { AcrobaticsModifier += ProficiencyBonus; }
            if (IsProf_AnimalHandling) { AnimalHandlingModifier += ProficiencyBonus; }
            if (IsProf_Arcana) { ArcanaModifier += ProficiencyBonus; }
            if (IsProf_Athletics) { AthleticsModifier += ProficiencyBonus; }
            if (IsProf_Deception) { DeceptionModifier += ProficiencyBonus; }
            if (IsProf_History) { HistoryModifier += ProficiencyBonus; }
            if (IsProf_Insight) { InsightModifier += ProficiencyBonus; }
            if (IsProf_Intimidation) { IntimidationModifier += ProficiencyBonus; }
            if (IsProf_Investigation) { InvestigationModifier += ProficiencyBonus; }
            if (IsProf_Medicine) { MedicineModifier += ProficiencyBonus; }
            if (IsProf_Nature) { NatureModifier += ProficiencyBonus; }
            if (IsProf_Perception) { PerceptionModifier += ProficiencyBonus; }
            if (IsProf_Performance) { PerformanceModifier += ProficiencyBonus; }
            if (IsProf_Persuasion) { PersuasionModifier += ProficiencyBonus; }
            if (IsProf_Religion) { ReligionModifier += ProficiencyBonus; }
            if (IsProf_SleightOfHand) { SleightOfHandModifier += ProficiencyBonus; }
            if (IsProf_Stealth) { StealthModifier += ProficiencyBonus; }
            if (IsProf_Survival) { SurvivalModifier += ProficiencyBonus; }
            if (IsExpert_Acrobatics) { AcrobaticsModifier += ProficiencyBonus; }
            if (IsExpert_AnimalHandling) { AnimalHandlingModifier += ProficiencyBonus; }
            if (IsExpert_Arcana) { ArcanaModifier += ProficiencyBonus; }
            if (IsExpert_Athletics) { AthleticsModifier += ProficiencyBonus; }
            if (IsExpert_Deception) { DeceptionModifier += ProficiencyBonus; }
            if (IsExpert_History) { HistoryModifier += ProficiencyBonus; }
            if (IsExpert_Insight) { InsightModifier += ProficiencyBonus; }
            if (IsExpert_Intimidation) { IntimidationModifier += ProficiencyBonus; }
            if (IsExpert_Investigation) { InvestigationModifier += ProficiencyBonus; }
            if (IsExpert_Medicine) { MedicineModifier += ProficiencyBonus; }
            if (IsExpert_Nature) { NatureModifier += ProficiencyBonus; }
            if (IsExpert_Perception) { PerceptionModifier += ProficiencyBonus; }
            if (IsExpert_Performance) { PerformanceModifier += ProficiencyBonus; }
            if (IsExpert_Persuasion) { PersuasionModifier += ProficiencyBonus; }
            if (IsExpert_Religion) { ReligionModifier += ProficiencyBonus; }
            if (IsExpert_SleightOfHand) { SleightOfHandModifier += ProficiencyBonus; }
            if (IsExpert_Stealth) { StealthModifier += ProficiencyBonus; }
            if (IsExpert_Survival) { SurvivalModifier += ProficiencyBonus; }

            // Other
            SetPassivePerception();
            SetSpellcastingStats();
            UpdateEncumbrance();
            UpdateArmorClass();
            UpdateSpeed();

        }
        public void UpdateAbilityDropdowns()
        {
            foreach (CustomAbility ability in Abilities)
            {
                List<string> variables = new();
                List<string> conditions = new();
                foreach (CAVariable variable in ability.Variables)
                {
                    variables.Add(variable.Name);
                    conditions.Add(variable.Name);
                }
                foreach (CharacterAlterant alterant in Alterants)
                {
                    conditions.Add(alterant.Name);
                }
                foreach (CAPreAction preAction in ability.PreActions) 
                { 
                    preAction.UpdateTargetList(variables); 
                    foreach (CACondition condition in preAction.Conditions)
                    {
                        condition.UpdateVariableList(conditions);
                    }
                }
                foreach (CAPostAction postAction in ability.PostActions)
                {
                    foreach (CACondition condition in postAction.Conditions)
                    {
                        condition.UpdateVariableList(conditions);
                    }
                }
            }
        }
        public void UpdateLanguageChoiceCounts()
        {
            foreach (ChoiceSet langChoiceSeg in LanguageChoiceSegments)
            {
                int selectedLangCount = 0;
                foreach (BoolOption langChoice in langChoiceSeg.Choices)
                {
                    if (langChoice.Marked) { selectedLangCount++; }
                }
                langChoiceSeg.ChoicesRemaining = langChoiceSeg.MaxChoices - selectedLangCount;
            }
        }
        public void UpdateInventoryItemCategories()
        {
            foreach (InventoryModel inventory in Inventories)
            {
                foreach (ItemModel item in inventory.AllItems)
                {
                    if (Configuration.ItemTypes.Contains(item.Type) == false)
                    {
                        ItemModel matchedSourceItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == item.Name);
                        if (matchedSourceItem == null) { item.Type = "Other"; }
                        else { item.Type = matchedSourceItem.Type; }
                    }
                }
            }
        }
        public void SetAlterantSuggestedValues()
        {
            foreach (CharacterAlterant alterant in Alterants)
            {
                foreach (LabeledNumber stat in alterant.StatChanges)
                {
                    stat.NameSuggestions = Configuration.AlterantStats;
                }
            }
        }

        // Private Methods - Character Creation Implementation
        private void CCI_SavingThrows_Set(List<FeatureData> choices)
        {
            foreach (FeatureData featureData in choices)
            {
                if (featureData.Name == "Strength") { HasSave_Strength = true; }
                if (featureData.Name == "Dexterity") { HasSave_Dexterity = true; }
                if (featureData.Name == "Constitution") { HasSave_Constitution = true; }
                if (featureData.Name == "Intelligence") { HasSave_Intelligence = true; }
                if (featureData.Name == "Wisdom") { HasSave_Wisdom = true; }
                if (featureData.Name == "Charisma") { HasSave_Charisma = true; }
            }
        }
        private void CCI_SkillProficiencies_Set(List<FeatureData> choices)
        {
            foreach (FeatureData featureData in choices)
            {
                if (featureData.Name == "Acrobatics") { IsProf_Acrobatics = true; }
                if (featureData.Name == "Animal Handling") { IsProf_AnimalHandling = true; }
                if (featureData.Name == "Arcana") { IsProf_Arcana = true; }
                if (featureData.Name == "Athletics") { IsProf_Athletics = true; }
                if (featureData.Name == "Deception") { IsProf_Deception = true; }
                if (featureData.Name == "History") { IsProf_History = true; }
                if (featureData.Name == "Insight") { IsProf_Insight = true; }
                if (featureData.Name == "Intimidation") { IsProf_Intimidation = true; }
                if (featureData.Name == "Investigation") { IsProf_Investigation = true; }
                if (featureData.Name == "Medicine") { IsProf_Medicine = true; }
                if (featureData.Name == "Nature") { IsProf_Nature = true; }
                if (featureData.Name == "Perception") { IsProf_Perception = true; }
                if (featureData.Name == "Performance") { IsProf_Performance = true; }
                if (featureData.Name == "Persuasion") { IsProf_Persuasion = true; }
                if (featureData.Name == "Religion") { IsProf_Religion = true; }
                if (featureData.Name == "Sleight of Hand") { IsProf_SleightOfHand = true; }
                if (featureData.Name == "Stealth") { IsProf_Stealth = true; }
                if (featureData.Name == "Survival") { IsProf_Survival = true; }
            }
        }
        private void CCI_Tools()
        {
            List<string> tools = new();
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(p => p.Name == pclm.ClassName);
                tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", pc.Features.ToList(), tools);
            }

            if (LinkedRace != null) { tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", LinkedRace.Features.ToList(), tools); }
            if (LinkedSubrace != null) { tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", LinkedSubrace.Features.ToList(), tools); }
            if (LinkedBackground != null) { tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", LinkedBackground.Features.ToList(), tools); }

            foreach (ChoiceSet choiceSet in ToolChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked && tools.Contains(choice.Name) == false) { tools.Add(choice.Name); }
                }
            }

            List<ItemModel> toolProfs = new();
            foreach (string tool in tools)
            {
                toolProfs.Add(new() { Name = tool });
            }

            ToolProficiencies = new(toolProfs);

        }
        private void CCI_Other()
        {
            List<string> otherProfs = new();
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(p => p.Name == pclm.ClassName);
                if (pclm == PlayerClasses.First())
                {
                    otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", pc.Features.ToList(), otherProfs);
                    otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", pc.Features.ToList(), otherProfs);
                }
                else
                {
                    otherProfs = AddOntoStringListFromFeatures("Multiclass Armor Proficiencies - Set", pc.Features.ToList(), otherProfs);
                    otherProfs = AddOntoStringListFromFeatures("Multiclass Weapon Proficiencies - Set", pc.Features.ToList(), otherProfs);
                }
            }

            if (LinkedRace != null)
            {
                otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", LinkedRace.Features.ToList(), otherProfs);
                otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", LinkedRace.Features.ToList(), otherProfs);
            }

            if (LinkedSubrace != null)
            {
                otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", LinkedSubrace.Features.ToList(), otherProfs);
                otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", LinkedSubrace.Features.ToList(), otherProfs);
            }

            if (LinkedBackground != null)
            {
                otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", LinkedBackground.Features.ToList(), otherProfs);
                otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", LinkedBackground.Features.ToList(), otherProfs);
            }

            foreach (ChoiceSet choiceSet in ArmorChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked && otherProfs.Contains(choice.Name) == false) { otherProfs.Add(choice.Name); }
                }
            }
            foreach (ChoiceSet choiceSet in WeaponChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked && otherProfs.Contains(choice.Name) == false) { otherProfs.Add(choice.Name); }
                }
            }

            otherProfs.Sort();
            string output = string.Empty;
            for (int i = 0; i < otherProfs.Count; i++)
            {
                if (i > 0) { output += ", "; }
                output += otherProfs[i];
            }
            output = output.Replace("[", "").Replace("]", "");

            OtherProficiencies = output;

        }
        private void CCI_Languages()
        {
            List<string> languages = new();
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(p => p.Name == pclm.ClassName);
                languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", pc.Features.ToList(), languages);
            }

            if (LinkedRace != null)
            {
                languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", LinkedRace.Features.ToList(), languages);
            }
            if (LinkedSubrace != null)
            {
                languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", LinkedSubrace.Features.ToList(), languages);
            }
            if (LinkedBackground != null)
            {
                languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", LinkedBackground.Features.ToList(), languages);
            }

            foreach (ChoiceSet choiceSet in LanguageChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked && languages.Contains(choice.Name) == false) { languages.Add(choice.Name); }
                }
            }

            languages.Sort();
            string output = string.Empty;
            for (int i = 0; i < languages.Count; i++)
            {
                if (i > 0) { output += ", "; }
                output += languages[i];
            }
            output = output.Replace("[", "").Replace("]", "");

            Languages = output;

        }
        private static List<string> AddOntoStringListFromFeatures(string featureType, List<FeatureModel> features, List<string> endList)
        {
            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    foreach (FeatureData fd in feature.Choices)
                    {
                        if (endList.Contains(fd.Name) == false) { endList.Add(fd.Name); }
                    }
                }
            }
            return endList;
        }

        // Private Methods
        private void UpdateArmorClass()
        {
            int ac = ArmorClass;
            if (ArmorLinkedItem != null)
            {
                if (ArmorLinkedItem.BaseArmorClass > 0)
                {
                    ac = ArmorLinkedItem.BaseArmorClass;
                    switch (ArmorLinkedItem.Type)
                    {
                        case "Light Armor":
                            ac += DexterityModifier;
                            break;
                        case "Medium Armor":
                            ac += (DexterityModifier > 2) ? 2 : DexterityModifier;
                            break;
                        default: break;
                    }
                    //ac += (ArmorLinkedItem.DexterityAcLimit <= DexterityModifier) ? ArmorLinkedItem.DexterityAcLimit : DexterityModifier;
                }
                foreach (LabeledNumber labeledNumber in ArmorLinkedItem.StatChanges)
                {
                    if (labeledNumber.Name == "Armor Class") { ac += labeledNumber.Value; }
                }
            }
            if (MainHandLinkedItem != null)
            {
                ac += MainHandLinkedItem.BaseArmorClass;
                foreach (LabeledNumber labeledNumber in MainHandLinkedItem.StatChanges)
                {
                    if (labeledNumber.Name == "Armor Class") { ac += labeledNumber.Value; }
                }
            }
            if (OffHandLinkedItem != null)
            {
                ac += OffHandLinkedItem.BaseArmorClass;
                foreach (LabeledNumber labeledNumber in OffHandLinkedItem.StatChanges)
                {
                    if (labeledNumber.Name == "Armor Class") { ac += labeledNumber.Value; }
                }
            }
            foreach (ItemLink linkedAccessory in EquippedAccessories)
            {
                if (linkedAccessory.LinkedItem == null) { continue; }
                foreach (LabeledNumber labeledNumber in linkedAccessory.LinkedItem.StatChanges)
                {
                    if (labeledNumber.Name == "Armor Class") { ac += labeledNumber.Value; }
                }
            }
            foreach (CharacterAlterant alterant in Alterants)
            {
                if (alterant.IsActive == false) { continue; }
                foreach (LabeledNumber number in alterant.StatChanges)
                {
                    if (number.Name == "Armor Class") { ac += number.Value; }
                }
            }
            FinalArmorClass = ac;
        }
        private void UpdateSpeed()
        {
            int speed = Speed;
            foreach (CharacterAlterant alterant in Alterants)
            {
                if (alterant.IsActive == false) { continue; }
                foreach (LabeledNumber number in alterant.StatChanges)
                {
                    if (number.Name == "Speed") { speed += number.Value; }
                }
            }
            FinalSpeed = speed;
        }
        private void UpdateStatBonusesFromAlterants()
        {
            StrengthBonus = 0;
            DexterityBonus = 0;
            ConstitutionBonus = 0;
            IntelligenceBonus = 0;
            WisdomBonus = 0;
            CharismaBonus = 0;

            AcrobaticsBonus = 0;
            AnimalHandlingBonus = 0;
            ArcanaBonus = 0;
            AthleticsBonus = 0;
            DeceptionBonus = 0;
            HistoryBonus = 0;
            InsightBonus = 0;
            IntimidationBonus = 0;
            InvestigationBonus = 0;
            MedicineBonus = 0;
            NatureBonus = 0;
            PerceptionBonus = 0;
            PerformanceBonus = 0;
            PersuasionBonus = 0;
            ReligionBonus = 0;
            SleightOfHandBonus = 0;
            StealthBonus = 0;
            SurvivalBonus = 0;

            foreach (CharacterAlterant alterant in Alterants)
            {
                if (alterant.IsActive == false) { continue; }
                foreach (LabeledNumber number in alterant.StatChanges)
                {
                    switch (number.Name)
                    {
                        case "Strength": StrengthBonus += number.Value; break;
                        case "Dexterity": DexterityBonus += number.Value; break;
                        case "Constitution": ConstitutionBonus += number.Value; break;
                        case "Intelligence": IntelligenceBonus += number.Value; break;
                        case "Wisdom": WisdomBonus += number.Value; break;
                        case "Charisma": CharismaBonus += number.Value; break;

                        case "Acrobatics": AcrobaticsBonus += number.Value; break;
                        case "Animal Handling": AnimalHandlingBonus += number.Value; break;
                        case "Arcana": ArcanaBonus += number.Value; break;
                        case "Athletics": AthleticsBonus += number.Value; break;
                        case "Deception": DeceptionBonus += number.Value; break;
                        case "History": HistoryBonus += number.Value; break;
                        case "Insight": InsightBonus += number.Value; break;
                        case "Intimidation": IntimidationBonus += number.Value; break;
                        case "Investigation": InvestigationBonus += number.Value; break;
                        case "Medicine": MedicineBonus += number.Value; break;
                        case "Nature": NatureBonus += number.Value; break;
                        case "Perception": PerceptionBonus += number.Value; break;
                        case "Performance": PerformanceBonus += number.Value; break;
                        case "Persuasion": PersuasionBonus += number.Value; break;
                        case "Religion": ReligionBonus += number.Value; break;
                        case "Sleight of Hand": SleightOfHandBonus += number.Value; break;
                        case "Stealth": StealthBonus += number.Value; break;
                        case "Survival": SurvivalBonus += number.Value; break;

                        default: break;

                    }
                }
            }

        }
        private List<NoteModel> SortNoteList(List<NoteModel> notes)
        {
            List<NoteModel> sortedNotes = notes.OrderBy(n => n.Category).ThenBy(n => n.Header).ToList();
            foreach (NoteModel note in sortedNotes)
            {
                NoteType nt = Configuration.MainModelRef.ToolsView.NoteTypes.FirstOrDefault(n => n.Name == note.Category);
                if (nt == null)
                {
                    note.SubNotes = new(SortNoteList(note.SubNotes.ToList()));
                }
                else if (nt.SortSubNotes)
                {
                    note.SubNotes = new(SortNoteList(note.SubNotes.ToList()));
                }
            }
            return sortedNotes;
        }
        private void SetPassivePerception()
        {
            PassivePerception = 10 + HelperMethods.GetAttributeModifier(WisdomScore) + ((IsProf_Perception) ? ProficiencyBonus : 0) + ((IsExpert_Perception) ? ProficiencyBonus : 0);
        }
        private void SetSpellcastingStats()
        {
            switch (SpellcastingAbility)
            {
                case "Strength":
                    SpellAttackBonus = StrengthModifier + ProficiencyBonus;
                    SpellAbilityModifier = StrengthModifier;
                    SpellSaveDc = 8 + SpellAttackBonus;
                    break;
                case "Dexterity":
                    SpellAttackBonus = DexterityModifier + ProficiencyBonus;
                    SpellAbilityModifier = DexterityModifier;
                    SpellSaveDc = 8 + SpellAttackBonus;
                    break;
                case "Constitution":
                    SpellAttackBonus = ConstitutionModifier + ProficiencyBonus;
                    SpellAbilityModifier = ConstitutionModifier;
                    SpellSaveDc = 8 + SpellAttackBonus;
                    break;
                case "Intelligence":
                    SpellAttackBonus = IntelligenceModifier + ProficiencyBonus;
                    SpellAbilityModifier = IntelligenceModifier;
                    SpellSaveDc = 8 + SpellAttackBonus;
                    break;
                case "Wisdom":
                    SpellAttackBonus = WisdomModifier + ProficiencyBonus;
                    SpellAbilityModifier = WisdomModifier;
                    SpellSaveDc = 8 + SpellAttackBonus;
                    break;
                case "Charisma":
                    SpellAttackBonus = CharismaModifier + ProficiencyBonus;
                    SpellAbilityModifier = CharismaModifier;
                    SpellSaveDc = 8 + SpellAttackBonus;
                    break;
                default:
                    break;
            }
        }
        private void SetProcessedHeight()
        {
            int feet = RawHeight / 12;
            int inches = RawHeight - (feet * 12);
            ProcessedHeight = "" + feet + "\' " + inches + "\"";
        }
        private void ClosePopupsOtherThan(string popup)
        {
            if (popup != "Checks") { DisplayPopup_Checks = false; }
            if (popup != "Skills") { DisplayPopup_Skills = false; }
            if (popup != "Dice") { DisplayPopup_Dice = false; }
            if (popup != "Health") { DisplayPopup_Health = false; }
            if (popup != "Saves") { DisplayPopup_Saves = false; }
            if (popup != "Tools") { DisplayPopup_Tools = false; }
            if (popup != "Tables") { DisplayPopup_Tables = false; }
            if (popup != "Rest") { DisplayPopup_Rest = false; }
            if (popup != "StandardActions") { DisplayPopup_StandardActions = false; }
        }
        private void UpdateSubraceList()
        {
            List<string> srs = new();
            foreach (PlayerSubraceModel subrace in Configuration.MainModelRef.ToolsView.PlayerSubraces)
            {
                if (subrace.SubraceOf == Race && subrace.IsValidated)
                {
                    srs.Add(subrace.Name);
                }
            }
            Subraces = new List<string>(srs);
        }
        private void UpdateXpToNext()
        {
            int nextLevel = TotalLevel + 1;
            if (nextLevel > 20) { XpToNext = 0; return; }
            XpToNext = HelperMethods.GetXpFromLevel(nextLevel) - ExperiencePoints;
        }
        private void UpdateDisplayRace()
        {
            DisplayRace = (Subrace != "") ? Subrace : Race;
        }
        private void CalculateFinalScores()
        {
            //if (Configuration.LoadComplete == false) { return; }
            int strScore = 0;
            int dexScore = 0;
            int conScore = 0;
            int intScore = 0;
            int wisScore = 0;
            int chaScore = 0;
            strScore += StrengthBaseScore;
            dexScore += DexterityBaseScore;
            conScore += ConstitutionBaseScore;
            intScore += IntelligenceBaseScore;
            wisScore += WisdomBaseScore;
            chaScore += CharismaBaseScore;
            foreach (CharacterAttributeSet attributeSet in CCAttributeSets)
            {
                strScore += attributeSet.Strength;
                dexScore += attributeSet.Dexterity;
                conScore += attributeSet.Constitution;
                intScore += attributeSet.Intelligence;
                wisScore += attributeSet.Wisdom;
                chaScore += attributeSet.Charisma;
            }
            StrengthFinalScore = strScore;
            DexterityFinalScore = dexScore;
            ConstitutionFinalScore = conScore;
            IntelligenceFinalScore = intScore;
            WisdomFinalScore = wisScore;
            CharismaFinalScore = chaScore;

            StrengthFinalModifier = HelperMethods.GetAttributeModifier(StrengthFinalScore);
            DexterityFinalModifier = HelperMethods.GetAttributeModifier(DexterityFinalScore);
            ConstitutionFinalModifier = HelperMethods.GetAttributeModifier(ConstitutionFinalScore);
            IntelligenceFinalModifier = HelperMethods.GetAttributeModifier(IntelligenceFinalScore);
            WisdomFinalModifier = HelperMethods.GetAttributeModifier(WisdomFinalScore);
            CharismaFinalModifier = HelperMethods.GetAttributeModifier(CharismaFinalScore);

            MaxHpCalc = GetCalculatedMaxHitPoints(ConstitutionFinalModifier);
            
        }
        private int GetMaxFeatPoints()
        {
            int feats = 0;
            foreach (ChoiceSet cs in AttributeFeatChoices)
            {
                if (cs.Choices.First(c => c.Name == "Feat").Marked)
                {
                    feats++;
                }
            }
            if (LinkedRace != null)
            {
                foreach (FeatureModel feature in LinkedRace.Features)
                {
                    if (feature.FeatureType == "Additional Feat")
                    {
                        feats++;
                    }
                }
            }
            return feats;

        }
        public void UpdateCharacterSheet()
        {
            if (Configuration.LoadComplete == false) { return; }
            foreach (ChoiceSet choiceSet in AttributeFeatChoices)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    choice.PropertyChanged += CharacterAttributeFeatChoiceSelection_PropertyChanged;
                }
            }
            UpdateAttributePointsRemaining();
            UpdateOtherStatChoices();
            UpdateAttributeSets();
            UpdateFeatUsage();
            UpdateLanguageSets();
            UpdateToolSets();
            UpdateArmorSets();
            UpdateWeaponSets();
            UpdateSkillSets();
            UpdateStartingEquipment();
            UpdateTraits();
            UpdateSpellSets();
            CalculateFinalScores();
        }
        private void SetAttributeFeatChoices()
        {
            List<ChoiceSet> attrFeatChoices = new();
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }
                foreach (FeatureModel feature in pc.Features.Where(feature => feature.FeatureType == "Ability Score Improvement"))
                {
                    if (pclm.ClassLevel >= feature.LevelAvailable)
                    {
                        attrFeatChoices.Add(new ChoiceSet { Source = pclm.ClassName + " Level " + feature.LevelAvailable, MaxChoices = 1, ChoicesRemaining = 1 });
                    }
                }
                foreach (ChoiceSet choiceSet in attrFeatChoices)
                {
                    choiceSet.Choices = new() { new BoolOption { ChoiceSet = choiceSet.Source, Name = "Feat" }, new BoolOption { ChoiceSet = choiceSet.Source, Name = "Attribute Points" } };
                    foreach (BoolOption choice in choiceSet.Choices)
                    {
                        choice.PropertyChanged += CharacterAttributeFeatChoiceSelection_PropertyChanged;
                    }
                }
                foreach (ChoiceSet choiceSet in AttributeFeatChoices)
                {
                    foreach (ChoiceSet choiceSetB in attrFeatChoices)
                    {
                        if (choiceSet.Source == choiceSetB.Source)
                        {
                            foreach (BoolOption optA in choiceSet.Choices)
                            {
                                foreach (BoolOption optB in choiceSetB.Choices)
                                {
                                    if (optA.Name == optB.Name)
                                    {
                                        optB.Marked = optA.Marked;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            AttributeFeatChoices = new ObservableCollection<ChoiceSet>(attrFeatChoices);
        }
        private void UpdateAttributeSets()
        {
            List<CharacterAttributeSet> attrSets = new();
            if (LinkedRace != null)
            {
                attrSets.AddRange(GetCharacterAttributeSetsFromFeatureList(LinkedRace.Features.ToList(), LinkedRace.Name));
            }
            if (LinkedSubrace != null)
            {
                attrSets.AddRange(GetCharacterAttributeSetsFromFeatureList(LinkedSubrace.Features.ToList(), LinkedSubrace.Name));
            }
            foreach (ChoiceSet cs in AttributeFeatChoices)
            {
                if (cs.Choices.First(c => c.Name == "Attribute Points").Marked)
                {
                    attrSets.Add(new() { Name = cs.Source, MaxPoints = 2, PointsRemaining = 2 });
                    attrSets.Last().PropertyChanged += CharacterAttributeSet_PropertyChanged;
                }
            }
            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel playerClass = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(pc => pc.Name == pclm.ClassName);
                if (playerClass == null) { continue; }
                foreach (FeatureModel feature in playerClass.Features)
                {
                    if (feature.FeatureType == "Stat Bonuses - Set" && pclm.ClassLevel >= feature.LevelAvailable)
                    {
                        attrSets.Add(new() { Name = playerClass.Name + " " + feature.Name });
                        attrSets.Last().MaxPoints = GetQuantityFromFeatureData(feature.Choices.ToList(), "Attribute Choice");
                        attrSets.Last().Strength = GetQuantityFromFeatureData(feature.Choices.ToList(), "Strength");
                        attrSets.Last().Dexterity = GetQuantityFromFeatureData(feature.Choices.ToList(), "Dexterity");
                        attrSets.Last().Constitution = GetQuantityFromFeatureData(feature.Choices.ToList(), "Constitution");
                        attrSets.Last().Intelligence = GetQuantityFromFeatureData(feature.Choices.ToList(), "Intelligence");
                        attrSets.Last().Wisdom = GetQuantityFromFeatureData(feature.Choices.ToList(), "Wisdom");
                        attrSets.Last().Charisma = GetQuantityFromFeatureData(feature.Choices.ToList(), "Charisma");
                        attrSets.Last().PropertyChanged += CharacterAttributeSet_PropertyChanged;
                    }
                }
            }
            
            foreach (CharacterAttributeSet cas in CCAttributeSets)
            {
                foreach (CharacterAttributeSet casB in attrSets)
                {
                    if (cas.Name == casB.Name)
                    {
                        casB.Strength = cas.Strength;
                        casB.Dexterity = cas.Dexterity;
                        casB.Constitution = cas.Constitution;
                        casB.Intelligence = cas.Intelligence;
                        casB.Wisdom = cas.Wisdom;
                        casB.Charisma = cas.Charisma;
                        casB.PointsRemaining = cas.PointsRemaining;
                        casB.MaxPoints = cas.MaxPoints;
                    }
                }
            }

            foreach (ChoiceSet choiceSet in StatBonusChoiceSegments)
            {
                bool isAttributeSet = false;
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (Configuration.AbilityTypes.Contains(choice.Name.Split(' ')[1])) { isAttributeSet = true; break; }
                }
                if (isAttributeSet)
                {
                    attrSets.Add(new() { Name = choiceSet.Source });
                    foreach (BoolOption choice in choiceSet.Choices)
                    {
                        string ability = choice.Name.Split(' ')[1];
                        int score = Convert.ToInt32(choice.Name.Split(' ')[0].Substring(1));
                        if (ability == "Strength" && choice.Marked) { attrSets.Last().Strength = score; }
                        if (ability == "Dexterity" && choice.Marked) { attrSets.Last().Dexterity = score; }
                        if (ability == "Constitution" && choice.Marked) { attrSets.Last().Constitution = score; }
                        if (ability == "Intelligence" && choice.Marked) { attrSets.Last().Intelligence = score; }
                        if (ability == "Wisdom" && choice.Marked) { attrSets.Last().Wisdom = score; }
                        if (ability == "Charisma" && choice.Marked) { attrSets.Last().Charisma = score; }
                    }
                }
            }

            CCAttributeSets = new(attrSets);
            CalculateFinalScores();
            
        }
        private void UpdateExpertiseSets()
        {
            List<ChoiceSet> expertiseChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                foreach (FeatureModel feature in pc.Features)
                {
                    if (feature.FeatureType == "Skill Expertise - Choice" && pclm.ClassLevel >= feature.LevelAvailable)
                    {
                        List<string> skillList = new();

                        PlayerClassModel baseClass = Configuration.MainModelRef.ToolsView.PlayerClasses.First(c => c.Name == PlayerClasses.First().ClassName);
                        foreach (FeatureModel subFeature in baseClass.Features)
                        {
                            foreach (FeatureData featureData in subFeature.Choices)
                            {
                                if (subFeature.FeatureType != "Skill Proficiencies - Set") { continue; }
                                if (skillList.Contains(featureData.Name) == false) { skillList.Add(featureData.Name); }
                            }
                        }
                        if (LinkedRace != null)
                        {
                            foreach (FeatureModel subFeature in LinkedRace.Features)
                            {
                                foreach (FeatureData featureData in subFeature.Choices)
                                {
                                    if (subFeature.FeatureType != "Skill Proficiencies - Set") { continue; }
                                    if (skillList.Contains(featureData.Name) == false) { skillList.Add(featureData.Name); }
                                }
                            }
                        }
                        if (LinkedSubrace != null)
                        {
                            foreach (FeatureModel subFeature in LinkedSubrace.Features)
                            {
                                foreach (FeatureData featureData in subFeature.Choices)
                                {
                                    if (subFeature.FeatureType != "Skill Proficiencies - Set") { continue; }
                                    if (skillList.Contains(featureData.Name) == false) { skillList.Add(featureData.Name); }
                                }
                            }
                        }
                        if (LinkedBackground != null)
                        {
                            foreach (FeatureModel subFeature in LinkedBackground.Features)
                            {
                                foreach (FeatureData featureData in subFeature.Choices)
                                {
                                    if (subFeature.FeatureType != "Skill Proficiencies - Set") { continue; }
                                    if (skillList.Contains(featureData.Name) == false) { skillList.Add(featureData.Name); }
                                }
                            }
                        }

                        // SKILL PROFICIENCIES - CHOICE
                        foreach (ChoiceSet choiceSet in SkillChoiceSegments)
                        {
                            foreach (BoolOption choice in choiceSet.Choices)
                            {
                                if (choice.Marked && skillList.Contains(choice.Name) == false)
                                {
                                    skillList.Add(choice.Name);
                                }
                            }
                        }

                        skillList.Sort();
                        expertiseChoices.Add(new() { Source = pc.Name + " " + feature.Name + " Lv. " + feature.LevelAvailable, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                        foreach (string skill in skillList)
                        {
                            expertiseChoices.Last().Choices.Add(new() { Name = skill });
                            expertiseChoices.Last().Choices.Last().PropertyChanged += ExpertiseChoice_PropertyChanged;
                        }

                    }
                }

            }

            expertiseChoices = GetExistingChoiceMarkings(expertiseChoices, ExpertiseChoiceSegments.ToList());
            foreach (ChoiceSet choiceSet in expertiseChoices)
            {
                int choicesLeft = choiceSet.MaxChoices;
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked) { choicesLeft--; }
                }
                choiceSet.ChoicesRemaining = choicesLeft;
            }

            ExpertiseChoiceSegments = new(expertiseChoices);

        }
        private void UpdateSkillSets()
        {
            List<string> setSkills = new();
            List<ChoiceSet> skillChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                if (pclm == PlayerClasses.First())
                {
                    setSkills.AddRange(GetSkillSetProfsFromFeatureList(pc.Features.ToList(), "Skill Proficiencies - Set", pclm.ClassName));
                    skillChoices.AddRange(GetSkillChoiceSetsFromFeatureList(pc.Features.ToList(), "Skill Proficiencies - Choice", pclm.ClassName));
                }
                else
                {
                    setSkills.AddRange(GetSkillSetProfsFromFeatureList(pc.Features.ToList(), "Multiclass Skill Proficiencies - Set", pclm.ClassName));
                    skillChoices.AddRange(GetSkillChoiceSetsFromFeatureList(pc.Features.ToList(), "Multiclass Skill Proficiencies - Choice", pclm.ClassName));
                }
                
            }

            if (LinkedRace != null)
            {
                setSkills.AddRange(GetSkillSetProfsFromFeatureList(LinkedRace.Features.ToList(), "Skill Proficiencies - Set", LinkedRace.Name));
                skillChoices.AddRange(GetSkillChoiceSetsFromFeatureList(LinkedRace.Features.ToList(), "Skill Proficiencies - Choice", LinkedRace.Name));
            }

            if (LinkedSubrace != null)
            {
                setSkills.AddRange(GetSkillSetProfsFromFeatureList(LinkedSubrace.Features.ToList(), "Skill Proficiencies - Set", LinkedSubrace.Name));
                skillChoices.AddRange(GetSkillChoiceSetsFromFeatureList(LinkedSubrace.Features.ToList(), "Skill Proficiencies - Choice", LinkedSubrace.Name));
            }

            if (LinkedBackground != null)
            {
                setSkills.AddRange(GetSkillSetProfsFromFeatureList(LinkedBackground.Features.ToList(), "Skill Proficiencies - Set", LinkedBackground.Name));
                skillChoices.AddRange(GetSkillChoiceSetsFromFeatureList(LinkedBackground.Features.ToList(), "Skill Proficiencies - Choice", LinkedBackground.Name));
            }

            skillChoices = GetExistingChoiceMarkings(skillChoices, SkillChoiceSegments.ToList());
            foreach (ChoiceSet choiceSet in skillChoices)
            {
                int choicesLeft = choiceSet.MaxChoices;
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked) { choicesLeft--; }
                }
                choiceSet.ChoicesRemaining = choicesLeft;
            }

            SetSkillProfs = new(setSkills);
            SkillChoiceSegments = new(skillChoices);

            ObservableCollection<ChoiceSet> skillChoiceSegments = SkillChoiceSegments;
            if (UnmarkChoicesFromSets(ref skillChoiceSegments, SetSkillProfs)) { return; }

            UpdateExpertiseSets();

        }
        private void UpdateLanguageSets()
        {
            List<string> setLangs = new();
            List<ChoiceSet> langChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                if (pclm == PlayerClasses.First())
                {
                    setLangs.AddRange(GetLanguageSetProfsFromFeatureList(pc.Features.ToList(), "Language Proficiencies - Set", pclm.ClassName));
                    langChoices.AddRange(GetLanguageChoiceSetsFromFeatureList(pc.Features.ToList(), "Language Proficiencies - Choice", pclm.ClassName));
                }
                else
                {
                    setLangs.AddRange(GetLanguageSetProfsFromFeatureList(pc.Features.ToList(), "Multiclass Language Proficiencies - Set", pclm.ClassName));
                    langChoices.AddRange(GetLanguageChoiceSetsFromFeatureList(pc.Features.ToList(), "Multiclass Language Proficiencies - Choice", pclm.ClassName));
                }

            }

            if (LinkedRace != null)
            {
                setLangs.AddRange(GetLanguageSetProfsFromFeatureList(LinkedRace.Features.ToList(), "Language Proficiencies - Set", LinkedRace.Name));
                langChoices.AddRange(GetLanguageChoiceSetsFromFeatureList(LinkedRace.Features.ToList(), "Language Proficiencies - Choice", LinkedRace.Name));
            }
            if (LinkedSubrace != null)
            {
                setLangs.AddRange(GetLanguageSetProfsFromFeatureList(LinkedSubrace.Features.ToList(), "Language Proficiencies - Set", LinkedSubrace.Name));
                langChoices.AddRange(GetLanguageChoiceSetsFromFeatureList(LinkedSubrace.Features.ToList(), "Language Proficiencies - Choice", LinkedSubrace.Name));
            }
            if (LinkedBackground != null)
            {
                setLangs.AddRange(GetLanguageSetProfsFromFeatureList(LinkedBackground.Features.ToList(), "Language Proficiencies - Set", LinkedBackground.Name));
                langChoices.AddRange(GetLanguageChoiceSetsFromFeatureList(LinkedBackground.Features.ToList(), "Language Proficiencies - Choice", LinkedBackground.Name));
            }

            langChoices = GetExistingChoiceMarkings(langChoices, LanguageChoiceSegments.ToList());
            UpdateChoicesRemaining(ref langChoices);

            SetLanguageProfs = new(setLangs);
            LanguageChoiceSegments = new(langChoices);

            ObservableCollection<ChoiceSet> languageChoiceSegments = LanguageChoiceSegments;
            if (UnmarkChoicesFromSets(ref languageChoiceSegments, SetLanguageProfs)) { return; }

        }
        private void UpdateToolSets()
        {
            List<string> setTools = new();
            List<ChoiceSet> toolChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                if (pclm == PlayerClasses.First())
                {
                    setTools.AddRange(GetToolSetProfsFromFeatureList(pc.Features.ToList(), "Tool Proficiencies - Set", pclm.ClassName));
                    toolChoices.AddRange(GetToolChoiceSetsFromFeatureList(pc.Features.ToList(), "Tool Proficiencies - Choice", pclm.ClassName));
                }
                else
                {
                    setTools.AddRange(GetToolSetProfsFromFeatureList(pc.Features.ToList(), "Multiclass Tool Proficiencies - Set", pclm.ClassName));
                    toolChoices.AddRange(GetToolChoiceSetsFromFeatureList(pc.Features.ToList(), "Multiclass Tool Proficiencies - Choice", pclm.ClassName));
                }

            }

            if (LinkedRace != null)
            {
                setTools.AddRange(GetToolSetProfsFromFeatureList(LinkedRace.Features.ToList(), "Tool Proficiencies - Set", LinkedRace.Name));
                toolChoices.AddRange(GetToolChoiceSetsFromFeatureList(LinkedRace.Features.ToList(), "Tool Proficiencies - Choice", LinkedRace.Name));
            }
            if (LinkedBackground != null)
            {
                setTools.AddRange(GetToolSetProfsFromFeatureList(LinkedBackground.Features.ToList(), "Tool Proficiencies - Set", LinkedBackground.Name));
                toolChoices.AddRange(GetToolChoiceSetsFromFeatureList(LinkedBackground.Features.ToList(), "Tool Proficiencies - Choice", LinkedBackground.Name));
            }

            toolChoices = GetExistingChoiceMarkings(toolChoices, ToolChoiceSegments.ToList());
            UpdateChoicesRemaining(ref toolChoices);

            SetToolProfs = new(setTools);
            ToolChoiceSegments = new(toolChoices);

            ObservableCollection<ChoiceSet> toolChoiceSegments = ToolChoiceSegments;
            if (UnmarkChoicesFromSets(ref toolChoiceSegments, SetToolProfs)) { return; }

        }
        private void UpdateChoicesRemaining(ref List<ChoiceSet> choiceSets)
        {
            foreach (ChoiceSet choiceSet in choiceSets)
            {
                int choicesLeft = choiceSet.MaxChoices;
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked) { choicesLeft--; }
                }
                choiceSet.ChoicesRemaining = choicesLeft;
            }
        }
        private void UpdateWeaponSets()
        {
            List<string> setWeapons = new();
            List<ChoiceSet> weaponChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                if (pclm == PlayerClasses.First())
                {
                    setWeapons.AddRange(GetWeaponSetProfsFromFeatureList(pc.Features.ToList(), "Weapon Proficiencies - Set", pclm.ClassName));
                    weaponChoices.AddRange(GetWeaponChoiceSetsFromFeatureList(pc.Features.ToList(), "Weapon Proficiencies - Choice", pclm.ClassName));
                }
                else
                {
                    setWeapons.AddRange(GetWeaponSetProfsFromFeatureList(pc.Features.ToList(), "Multiclass Weapon Proficiencies - Set", pclm.ClassName));
                    weaponChoices.AddRange(GetWeaponChoiceSetsFromFeatureList(pc.Features.ToList(), "Multiclass Weapon Proficiencies - Choice", pclm.ClassName));
                }

            }

            if (LinkedRace != null)
            {
                setWeapons.AddRange(GetWeaponSetProfsFromFeatureList(LinkedRace.Features.ToList(), "Weapon Proficiencies - Set", LinkedRace.Name));
                weaponChoices.AddRange(GetWeaponChoiceSetsFromFeatureList(LinkedRace.Features.ToList(), "Weapon Proficiencies - Choice", LinkedRace.Name));
            }
            if (LinkedBackground != null)
            {
                setWeapons.AddRange(GetWeaponSetProfsFromFeatureList(LinkedBackground.Features.ToList(), "Weapon Proficiencies - Set", LinkedBackground.Name));
                weaponChoices.AddRange(GetWeaponChoiceSetsFromFeatureList(LinkedBackground.Features.ToList(), "Weapon Proficiencies - Choice", LinkedBackground.Name));
            }

            weaponChoices = GetExistingChoiceMarkings(weaponChoices, WeaponChoiceSegments.ToList());
            UpdateChoicesRemaining(ref weaponChoices);

            SetWeaponProfs = new(setWeapons);
            WeaponChoiceSegments = new(weaponChoices);

            ObservableCollection<ChoiceSet> weaponChoiceSegments = WeaponChoiceSegments;
            if (UnmarkChoicesFromSets(ref weaponChoiceSegments, SetWeaponProfs)) { return; }

        }
        private void UpdateSpellSets()
        {
            List<string> setSpells = new();
            List<ChoiceSet> spellChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }
                if (pc.HasSpellcasting)
                {
                    SpellTableRowModel spellRow = pc.SpellTableRows.FirstOrDefault(s => s.ClassLevel == pclm.ClassLevel);
                    if (spellRow == null) { break; }
                    spellChoices.Add(new() { Source = pc.Name + " Cantrips Known", MaxChoices = spellRow.CantripsKnown, ChoicesRemaining = spellRow.CantripsKnown });
                    foreach (SpellModel spell in Configuration.SpellRepository.Where(s => s.SpellLevel == 0 && s.IsValidated))
                    {
                        bool classMatch = false;
                        foreach (ConvertibleValue cv in spell.SpellClasses)
                        {
                            if (cv.Value == pc.Name) { classMatch = true; break; }
                        }
                        if (classMatch) 
                        { 
                            spellChoices.Last().Choices.Add(new() { Name = spell.Name, Description = HelperMethods.GetSpellDetailsTooltip(spell) });
                            spellChoices.Last().Choices.Last().PropertyChanged += SpellChoice_PropertyChanged;
                        }
                    }
                    spellChoices.Add(new() { Source = pc.Name + " Spells Known", MaxChoices = spellRow.SpellsKnown, ChoicesRemaining = spellRow.SpellsKnown });
                    if (pc.SpellsKnownPerLevelType == "Any") { spellChoices.Last().ChoiceRestricted = false; }
                    foreach (SpellModel spell in Configuration.SpellRepository.Where(s => s.SpellLevel >= 1 && s.IsValidated).OrderBy(s => s.SpellLevel).ThenBy(s => s.Name))
                    {
                        if (spellRow.SpellSlots_1st == 0 && spell.SpellLevel == 1) { continue; }
                        if (spellRow.SpellSlots_2nd == 0 && spell.SpellLevel == 2) { continue; }
                        if (spellRow.SpellSlots_3rd == 0 && spell.SpellLevel == 3) { continue; }
                        if (spellRow.SpellSlots_4th == 0 && spell.SpellLevel == 4) { continue; }
                        if (spellRow.SpellSlots_5th == 0 && spell.SpellLevel == 5) { continue; }
                        if (spellRow.SpellSlots_6th == 0 && spell.SpellLevel == 6) { continue; }
                        if (spellRow.SpellSlots_7th == 0 && spell.SpellLevel == 7) { continue; }
                        if (spellRow.SpellSlots_8th == 0 && spell.SpellLevel == 8) { continue; }
                        if (spellRow.SpellSlots_9th == 0 && spell.SpellLevel == 9) { continue; }
                        bool classMatch = false;
                        foreach (ConvertibleValue cv in spell.SpellClasses)
                        {
                            if (cv.Value == pc.Name) { classMatch = true; break; }
                        }
                        if (classMatch)
                        {
                            spellChoices.Last().Choices.Add(new() { Name = spell.Name, Description = HelperMethods.GetSpellDetailsTooltip(spell), Quantity = spell.SpellLevel });
                            spellChoices.Last().Choices.Last().PropertyChanged += SpellChoice_PropertyChanged;
                        }
                    }
                }
            }

            if (LinkedRace != null)
            {
                spellChoices.AddRange(GetAdditionalCantripsKnownFromFeatureList(LinkedRace.Features.ToList(), "Additional Known Cantrips", LinkedRace.Name));
            }
            if (LinkedSubrace != null)
            {
                spellChoices.AddRange(GetAdditionalCantripsKnownFromFeatureList(LinkedSubrace.Features.ToList(), "Additional Known Cantrips", LinkedSubrace.Name));
            }

            spellChoices = GetExistingChoiceMarkings(spellChoices, SpellChoiceSegments.ToList());
            UpdateChoicesRemaining(ref spellChoices);

            SpellChoiceSegments = new(spellChoices);

        }
        private void UpdateArmorSets()
        {
            List<string> setArmors = new();
            List<ChoiceSet> armorChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                if (pclm == PlayerClasses.First())
                {
                    setArmors.AddRange(GetWeaponSetProfsFromFeatureList(pc.Features.ToList(), "Armor Proficiencies - Set", pclm.ClassName));
                    armorChoices.AddRange(GetWeaponChoiceSetsFromFeatureList(pc.Features.ToList(), "Armor Proficiencies - Choice", pclm.ClassName));
                }
                else
                {
                    setArmors.AddRange(GetWeaponSetProfsFromFeatureList(pc.Features.ToList(), "Multiclass Armor Proficiencies - Set", pclm.ClassName));
                    armorChoices.AddRange(GetWeaponChoiceSetsFromFeatureList(pc.Features.ToList(), "Multiclass Armor Proficiencies - Choice", pclm.ClassName));
                }

            }

            if (LinkedRace != null)
            {
                setArmors.AddRange(GetArmorSetProfsFromFeatureList(LinkedRace.Features.ToList(), "Multiclass Armor Proficiencies - Set", LinkedRace.Name));
                armorChoices.AddRange(GetArmorChoiceSetsFromFeatureList(LinkedRace.Features.ToList(), "Multiclass Armor Proficiencies - Choice", LinkedRace.Name));
            }
            if (LinkedBackground != null)
            {
                setArmors.AddRange(GetArmorSetProfsFromFeatureList(LinkedBackground.Features.ToList(), "Multiclass Armor Proficiencies - Set", LinkedBackground.Name));
                armorChoices.AddRange(GetArmorChoiceSetsFromFeatureList(LinkedBackground.Features.ToList(), "Multiclass Armor Proficiencies - Choice", LinkedBackground.Name));
            }

            armorChoices = GetExistingChoiceMarkings(armorChoices, ArmorChoiceSegments.ToList());
            UpdateChoicesRemaining(ref armorChoices);

            SetArmorProfs = new(setArmors);
            ArmorChoiceSegments = new(armorChoices);

            ObservableCollection<ChoiceSet> armorChoiceSegments = ArmorChoiceSegments;
            if (UnmarkChoicesFromSets(ref armorChoiceSegments, SetArmorProfs)) { return; }

        }
        private void UpdateTraits()
        {
            List<TraitModel> setTraits = new();
            List<ChoiceSet> traitChoices = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(cls => cls.Name == pclm.ClassName);
                if (pc == null) { continue; }

                setTraits.AddRange(GetTraitSetProfsFromFeatureList(pc.Features.Where(x => x.LevelAvailable <= pclm.ClassLevel).ToList()));
                traitChoices.AddRange(GetTraitChoiceSetsFromFeatureList(pc.Features.Where(x => x.LevelAvailable <= pclm.ClassLevel).ToList(), pclm.ClassName));

                PlayerSubclassModel psc = Configuration.MainModelRef.ToolsView.PlayerSubclasses.FirstOrDefault(scls => scls.Name == pclm.SubClassName);
                if (psc == null) { continue; }

                setTraits.AddRange(GetTraitSetProfsFromFeatureList(psc.Features.Where(x => x.LevelAvailable <= pclm.ClassLevel).ToList()));
                traitChoices.AddRange(GetTraitChoiceSetsFromFeatureList(psc.Features.Where(x => x.LevelAvailable <= pclm.ClassLevel).ToList(), pclm.SubClassName));

            }
            if (LinkedRace != null)
            {
                setTraits.AddRange(GetTraitSetProfsFromFeatureList(LinkedRace.Features.ToList()));
                traitChoices.AddRange(GetTraitChoiceSetsFromFeatureList(LinkedRace.Features.ToList(), LinkedRace.Name));
            }
            if (LinkedSubrace != null)
            {
                setTraits.AddRange(GetTraitSetProfsFromFeatureList(LinkedSubrace.Features.ToList()));
                traitChoices.AddRange(GetTraitChoiceSetsFromFeatureList(LinkedSubrace.Features.ToList(), LinkedSubrace.Name));
            }
            if (LinkedBackground != null)
            {
                setTraits.AddRange(GetTraitSetProfsFromFeatureList(LinkedBackground.Features.ToList()));
                traitChoices.AddRange(GetTraitChoiceSetsFromFeatureList(LinkedBackground.Features.ToList(), LinkedBackground.Name));
            }

            foreach (BoolOption feat in FeatChoices)
            {
                if (feat.Marked)
                {
                    PlayerFeatModel playerFeat = Configuration.MainModelRef.ToolsView.PlayerFeats.FirstOrDefault(f => f.Name == feat.Name);
                    if (playerFeat == null) { continue; }
                    setTraits.AddRange(GetTraitSetProfsFromFeatureList(playerFeat.Features.ToList()));
                }
            }

            traitChoices = GetExistingChoiceMarkings(traitChoices, TraitChoiceSegments.ToList());
            UpdateChoicesRemaining(ref traitChoices);

            SetTraits = new(setTraits.OrderBy(t => t.Name));
            TraitChoiceSegments = new(traitChoices);

        }
        private void UpdateStartingEquipment()
        {
            List<ConvertibleValueSet> startingEquipmentSets = new();
            List<ItemLink> grantedEquipment = new();

            foreach (PlayerClassLinkModel pclm in PlayerClasses)
            {
                PlayerClassModel pc = Configuration.MainModelRef.ToolsView.PlayerClasses.FirstOrDefault(p => p.Name == pclm.ClassName);
                if (pc == null) { continue; }
                if (pc.EquipmentChoices.Count > 0)
                {
                    startingEquipmentSets.Add(new() { Label = pc.Name + " Starting Equipment", Values = pc.EquipmentChoices });
                }
                break; // Only primary class grants starting equipment
            }
            if (LinkedBackground != null)
            {
                StartingGold = LinkedBackground.GoldPieces;
                if (LinkedBackground.EquipmentChoices.Count > 0)
                {
                    startingEquipmentSets.Add(new() { Label = LinkedBackground.Name + " Starting Equipment", Values = LinkedBackground.EquipmentChoices });
                }
                foreach (ItemModel item in LinkedBackground.MandatoryEquipment)
                {
                    grantedEquipment.Add(new() { Name = item.Name, Quantity = item.Quantity });
                }
            }

            StartingEquipmentChoiceSets = new(startingEquipmentSets);
            GrantedEquipment = new(grantedEquipment);

        }
        private void UpdateOtherStatChoices()
        {
            List<ChoiceSet> statChoices = new();

            if (LinkedRace != null)
            {
                statChoices.AddRange(GetCharacterAttributeChoicesFromFeatureList(LinkedRace.Features.ToList(), LinkedRace.Name));
            }

            statChoices = GetExistingChoiceMarkings(statChoices, StatBonusChoiceSegments.ToList());

            StatBonusChoiceSegments = new(statChoices);

        }
        private static List<ChoiceSet> GetExistingChoiceMarkings(List<ChoiceSet> choiceSetsNew, List<ChoiceSet> choiceSetsOld)
        {
            foreach (ChoiceSet choiceSetNew in choiceSetsNew)
            {
                foreach (ChoiceSet langChoiceOld in choiceSetsOld)
                {
                    if (choiceSetNew.Source == langChoiceOld.Source)
                    {
                        foreach (BoolOption optNew in choiceSetNew.Choices)
                        {
                            foreach (BoolOption optOld in langChoiceOld.Choices)
                            {
                                if (optNew.Name == optOld.Name) { optNew.Marked = optOld.Marked; }
                            }
                        }
                    }
                }
            }
            return choiceSetsNew;
        }
        private static bool UnmarkChoicesFromSets(ref ObservableCollection<ChoiceSet> sourceChoices, List<string> sourceSets)
        {
            bool unmarkedOption = false;
            foreach (ChoiceSet choiceSet in sourceChoices)
            {
                foreach (BoolOption option in choiceSet.Choices)
                {
                    foreach (string setItem in sourceSets)
                    {
                        if (option.Marked && setItem.Contains(option.Name)) { option.Marked = false; unmarkedOption = true; }
                    }
                }
            }
            return unmarkedOption;
        }
        private List<CharacterAttributeSet> GetCharacterAttributeSetsFromFeatureList(List<FeatureModel> features, string setNamePrefix)
        {
            List<CharacterAttributeSet> attrSets = new();
            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == "Stat Bonuses - Set")
                {
                    attrSets.Add(new() { Name = setNamePrefix + " " + feature.Name });
                    attrSets.Last().MaxPoints = GetQuantityFromFeatureData(feature.Choices.ToList(), "Attribute Choice");
                    attrSets.Last().Strength = GetQuantityFromFeatureData(feature.Choices.ToList(), "Strength");
                    attrSets.Last().Dexterity = GetQuantityFromFeatureData(feature.Choices.ToList(), "Dexterity");
                    attrSets.Last().Constitution = GetQuantityFromFeatureData(feature.Choices.ToList(), "Constitution");
                    attrSets.Last().Intelligence = GetQuantityFromFeatureData(feature.Choices.ToList(), "Intelligence");
                    attrSets.Last().Wisdom = GetQuantityFromFeatureData(feature.Choices.ToList(), "Wisdom");
                    attrSets.Last().Charisma = GetQuantityFromFeatureData(feature.Choices.ToList(), "Charisma");
                    attrSets.Last().PropertyChanged += CharacterAttributeSet_PropertyChanged;
                }
            }
            return attrSets;
        }
        private List<ChoiceSet> GetCharacterAttributeChoicesFromFeatureList(List<FeatureModel> features, string setNamePrefix)
        {
            List<ChoiceSet> attrChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == "Stat Bonuses - Choice")
                {
                    attrChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        if (featureData.Name == "Attribute Choice")
                        {
                            foreach (string attribute in Configuration.AbilityTypes)
                            {
                                attrChoiceSets.Last().Choices.Add(new() { Name = string.Format("{0}{1} {2}", ((featureData.Quantity >= 0) ? "+" : ""), featureData.Quantity, attribute) });
                                attrChoiceSets.Last().Choices.Last().PropertyChanged += AttributeChoice_PropertyChanged;
                            }
                            continue;
                        }
                        attrChoiceSets.Last().Choices.Add(new() { Name = string.Format("{0}{1} {2}", ((featureData.Quantity >= 0) ? "+" : ""), featureData.Quantity, featureData.Name) });
                        attrChoiceSets.Last().Choices.Last().PropertyChanged += AttributeChoice_PropertyChanged;
                    }
                }
            }

            return attrChoiceSets;
        }
        private static List<string> GetLanguageSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setLangProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string languages = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count; i++)
                    {
                        if (i > 0) { languages += ", "; }
                        languages += feature.Choices[i].Name;
                    }
                    setLangProfs.Add(languages);
                }
            }

            return setLangProfs;
        }
        private List<ChoiceSet> GetLanguageChoiceSetsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<ChoiceSet> langChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    langChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        if (featureData.Name == "[Standard Languages]")
                        {
                            foreach (LanguageModel stdLang in Configuration.MainModelRef.ToolsView.Languages.Where(l => l.IsValidated && l.Type == "Standard"))
                            {
                                langChoiceSets.Last().Choices.Add(new() { Name = stdLang.Name });
                                langChoiceSets.Last().Choices.Last().PropertyChanged += LanguageChoice_PropertyChanged;
                            }
                            continue;
                        }
                        if (featureData.Name == "[Exotic Languages]")
                        {
                            foreach (LanguageModel exoLang in Configuration.MainModelRef.ToolsView.Languages.Where(l => l.IsValidated && l.Type == "Exotic"))
                            {
                                langChoiceSets.Last().Choices.Add(new() { Name = exoLang.Name });
                                langChoiceSets.Last().Choices.Last().PropertyChanged += LanguageChoice_PropertyChanged;
                            }
                            continue;
                        }
                        langChoiceSets.Last().Choices.Add(new() { Name = featureData.Name });
                        langChoiceSets.Last().Choices.Last().PropertyChanged += LanguageChoice_PropertyChanged;
                    }
                }
            }

            return langChoiceSets;
        }
        private List<ChoiceSet> GetAdditionalCantripsKnownFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<ChoiceSet> cantripChoices = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == "Additional Known Cantrips")
                {
                    foreach (FeatureData data in feature.Choices)
                    {
                        cantripChoices.Add(new() { Source = setNamePrefix + " " + data.Name + " Cantrips Known", MaxChoices = data.Quantity, ChoicesRemaining = data.Quantity });
                        foreach (SpellModel spell in Configuration.SpellRepository.Where(s => s.SpellLevel == 0))
                        {
                            bool classMatch = false;
                            foreach (ConvertibleValue cv in spell.SpellClasses)
                            {
                                if (cv.Value == data.Name) { classMatch = true; break; }
                            }
                            if (classMatch)
                            {
                                cantripChoices.Last().Choices.Add(new() { Name = spell.Name, Description = HelperMethods.GetSpellDetailsTooltip(spell), Quantity = spell.SpellLevel });
                                cantripChoices.Last().Choices.Last().PropertyChanged += SpellChoice_PropertyChanged;
                            }
                        }
                    }
                }
            }

            return cantripChoices;
        }
        private static List<string> GetToolSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setToolProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string tools = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count; i++)
                    {
                        if (i > 0) { tools += ", "; }
                        tools += feature.Choices[i].Name;
                    }
                    setToolProfs.Add(tools);
                }
            }

            return setToolProfs;
        }
        private List<ChoiceSet> GetToolChoiceSetsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<ChoiceSet> toolChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    toolChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        if (featureData.Name == "[Other Tools]")
                        {
                            foreach (ItemModel item in Configuration.MainModelRef.ItemBuilderView.AllItems.Where(i => i.IsValidated && i.Type == "Tool"))
                            {
                                toolChoiceSets.Last().Choices.Add(new() { Name = item.Name });
                                toolChoiceSets.Last().Choices.Last().PropertyChanged += ToolChoice_PropertyChanged;
                            }
                            continue;
                        }
                        if (featureData.Name == "[Artisan Tools]")
                        {
                            foreach (ItemModel item in Configuration.MainModelRef.ItemBuilderView.AllItems.Where(i => i.IsValidated && i.Type == "Artisan Tool"))
                            {
                                toolChoiceSets.Last().Choices.Add(new() { Name = item.Name });
                                toolChoiceSets.Last().Choices.Last().PropertyChanged += ToolChoice_PropertyChanged;
                            }
                            continue;
                        }
                        if (featureData.Name == "[Musical Instruments]")
                        {
                            foreach (ItemModel item in Configuration.MainModelRef.ItemBuilderView.AllItems.Where(i => i.IsValidated && i.Type == "Instrument"))
                            {
                                toolChoiceSets.Last().Choices.Add(new() { Name = item.Name });
                                toolChoiceSets.Last().Choices.Last().PropertyChanged += ToolChoice_PropertyChanged;
                            }
                            continue;
                        }
                        if (featureData.Name == "[Gaming Sets]")
                        {
                            foreach (ItemModel item in Configuration.MainModelRef.ItemBuilderView.AllItems.Where(i => i.IsValidated && i.Type == "Gaming Set"))
                            {
                                toolChoiceSets.Last().Choices.Add(new() { Name = item.Name });
                                toolChoiceSets.Last().Choices.Last().PropertyChanged += ToolChoice_PropertyChanged;
                            }
                            continue;
                        }
                        toolChoiceSets.Last().Choices.Add(new() { Name = featureData.Name });
                        toolChoiceSets.Last().Choices.Last().PropertyChanged += ToolChoice_PropertyChanged;

                    }
                }
            }

            return toolChoiceSets;
        }
        private static List<string> GetWeaponSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setWeaponProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string weapons = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count; i++)
                    {
                        if (i > 0) { weapons += ", "; }
                        weapons += feature.Choices[i].Name;
                    }
                    setWeaponProfs.Add(weapons);
                }
            }

            return setWeaponProfs;
        }
        private List<ChoiceSet> GetWeaponChoiceSetsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<ChoiceSet> weaponChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    weaponChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        weaponChoiceSets.Last().Choices.Add(new() { Name = featureData.Name });
                        weaponChoiceSets.Last().Choices.Last().PropertyChanged += WeaponChoice_PropertyChanged;

                    }
                }
            }

            return weaponChoiceSets;
        }
        private static List<string> GetArmorSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setArmorProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string armors = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count; i++)
                    {
                        if (i > 0) { armors += ", "; }
                        armors += feature.Choices[i].Name;
                    }
                    setArmorProfs.Add(armors);
                }
            }

            return setArmorProfs;
        }
        private List<ChoiceSet> GetArmorChoiceSetsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<ChoiceSet> armorChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    armorChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        armorChoiceSets.Last().Choices.Add(new() { Name = featureData.Name });
                        armorChoiceSets.Last().Choices.Last().PropertyChanged += ArmorChoice_PropertyChanged;

                    }
                }
            }

            return armorChoiceSets;
        }
        private static List<string> GetSkillSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setSkillProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string skills = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count; i++)
                    {
                        if (i > 0) { skills += ", "; }
                        skills += feature.Choices[i].Name;
                    }
                    setSkillProfs.Add(skills);
                }
            }

            return setSkillProfs;
        }
        private List<ChoiceSet> GetSkillChoiceSetsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<ChoiceSet> skillChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    skillChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + ((feature.LevelAvailable > 1) ? "Lv. " + feature.LevelAvailable + " " : "") + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        skillChoiceSets.Last().Choices.Add(new() { Name = featureData.Name });
                        skillChoiceSets.Last().Choices.Last().PropertyChanged += SkillChoice_PropertyChanged;

                    }
                }
            }

            return skillChoiceSets;
        }
        private static List<TraitModel> GetTraitSetProfsFromFeatureList(List<FeatureModel> features)
        {
            List<TraitModel> setTraits = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == "Trait")
                {
                    setTraits.Add(new() { Name = feature.Name, Description = feature.Details });
                }
            }

            return setTraits;
        }
        private List<ChoiceSet> GetTraitChoiceSetsFromFeatureList(List<FeatureModel> features, string setNamePrefix)
        {
            List<ChoiceSet> traitChoiceSets = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == "Traits - Choice")
                {
                    traitChoiceSets.Add(new ChoiceSet { Source = setNamePrefix + " " + ((feature.LevelAvailable > 1) ? "Lv. " + feature.LevelAvailable + " " : "") + feature.Name, MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                    foreach (FeatureData featureData in feature.Choices)
                    {
                        traitChoiceSets.Last().Choices.Add(new() { Name = featureData.Name, Description = featureData.Description });
                        traitChoiceSets.Last().Choices.Last().PropertyChanged += TraitChoice_PropertyChanged;

                    }
                }
                if (feature.FeatureType == "Eldritch Invocations Known")
                {
                    ChoiceSet existingFeature = traitChoiceSets.FirstOrDefault(t => t.Source == "Eldritch Invocations Known");
                    if (existingFeature != null)
                    {
                        if (existingFeature.MaxChoices < feature.ChoicesAllowed)
                        {
                            existingFeature.MaxChoices = feature.ChoicesAllowed;
                            existingFeature.ChoicesRemaining = feature.ChoicesAllowed;
                        }
                    }
                    else
                    {
                        traitChoiceSets.Add(new() { Source = "Eldritch Invocations Known", MaxChoices = feature.ChoicesAllowed, ChoicesRemaining = feature.ChoicesAllowed });
                        foreach (EldritchInvocation invocation in Configuration.MainModelRef.ToolsView.EldritchInvocations.Where(ei => ei.IsValidated))
                        {
                            traitChoiceSets.Last().Choices.Add(new() { Name = invocation.Name, Description = invocation.Description });
                            traitChoiceSets.Last().Choices.Last().PropertyChanged += TraitChoice_PropertyChanged;

                        }
                    }
                }
            }

            return traitChoiceSets;
        }
        private static int GetQuantityFromFeatureData(List<FeatureData> features, string featureDataName)
        {
            int total = 0;
            foreach (FeatureData featureData in features)
            {
                if (featureData.Name == featureDataName) { total += featureData.Quantity; }
            }
            return total;
        }
        private static Dictionary<string, int> GetChoiceMarkedCounts(ref ObservableCollection<ChoiceSet> sourceChoices)
        {
            Dictionary<string, int> counts = new();
            foreach (ChoiceSet choiceSet in sourceChoices)
            {
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked)
                    {
                        if (counts.ContainsKey(option.Name))
                        {
                            counts[option.Name]++;
                        }
                        else
                        {
                            counts.Add(option.Name, 1);
                        }
                    }
                }
            }
            return counts;
        }
        private void SkillChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ObservableCollection<ChoiceSet> skillChoiceSegments = SkillChoiceSegments;
            if (UnmarkChoicesFromSets(ref skillChoiceSegments, SetSkillProfs)) { return; }

            Dictionary<string, int> dupCheck = GetChoiceMarkedCounts(ref skillChoiceSegments);
            foreach (KeyValuePair<string, int> entry in dupCheck)
            {
                if (entry.Value > 1) { (sender as BoolOption).Marked = false; return; }
            }

            foreach (ChoiceSet skillChoiceSeg in SkillChoiceSegments)
            {
                int selectedSkillCount = 0;
                foreach (BoolOption skillChoice in skillChoiceSeg.Choices)
                {
                    if (skillChoice.Marked) { selectedSkillCount++; }
                }
                if (selectedSkillCount > skillChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedSkillCount--;
                }
                skillChoiceSeg.ChoicesRemaining = skillChoiceSeg.MaxChoices - selectedSkillCount;
            }

            UpdateExpertiseSets();

        }
        private void ExpertiseChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (ChoiceSet expChoiceSeg in ExpertiseChoiceSegments)
            {
                int selectedExpertiseCount = 0;
                foreach (BoolOption expChoice in expChoiceSeg.Choices)
                {
                    if (expChoice.Marked) { selectedExpertiseCount++; }
                }
                if (selectedExpertiseCount > expChoiceSeg.MaxChoices)
                {
                    if ((sender as BoolOption).Marked)
                    {
                        (sender as BoolOption).Marked = false;
                    }
                    else
                    {
                        BoolOption expChoice = expChoiceSeg.Choices.FirstOrDefault(s => s.Marked);
                        if (expChoice != null) { expChoice.Marked = false; }
                    }
                    selectedExpertiseCount--;
                }
                expChoiceSeg.ChoicesRemaining = expChoiceSeg.MaxChoices - selectedExpertiseCount;
            }
        }
        private void LanguageChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ObservableCollection<ChoiceSet> languageChoiceSegments = LanguageChoiceSegments;
            if (UnmarkChoicesFromSets(ref languageChoiceSegments, SetLanguageProfs)) { return; }

            Dictionary<string, int> dupCheck = GetChoiceMarkedCounts(ref languageChoiceSegments);
            foreach (KeyValuePair<string, int> entry in dupCheck)
            {
                if (entry.Value > 1) { (sender as BoolOption).Marked = false; return; }
            }

            foreach (ChoiceSet langChoiceSeg in LanguageChoiceSegments)
            {
                int selectedLangCount = 0;
                foreach (BoolOption langChoice in langChoiceSeg.Choices)
                {
                    if (langChoice.Marked) { selectedLangCount++; }
                }
                if (selectedLangCount > langChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedLangCount--;
                }
                langChoiceSeg.ChoicesRemaining = langChoiceSeg.MaxChoices - selectedLangCount;
            }
        }
        private void ToolChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ObservableCollection<ChoiceSet> toolChoiceSegments = ToolChoiceSegments;
            if (UnmarkChoicesFromSets(ref toolChoiceSegments, SetToolProfs)) { return; }

            Dictionary<string, int> dupCheck = GetChoiceMarkedCounts(ref toolChoiceSegments);
            foreach (KeyValuePair<string, int> entry in dupCheck)
            {
                if (entry.Value > 1) { (sender as BoolOption).Marked = false; return; }
            }

            foreach (ChoiceSet toolChoiceSeg in ToolChoiceSegments)
            {
                int selectedToolCount = 0;
                foreach (BoolOption langChoice in toolChoiceSeg.Choices)
                {
                    if (langChoice.Marked) { selectedToolCount++; }
                }
                if (selectedToolCount > toolChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedToolCount--;
                }
                toolChoiceSeg.ChoicesRemaining = toolChoiceSeg.MaxChoices - selectedToolCount;
            }
        }
        private void WeaponChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ObservableCollection<ChoiceSet> weaponChoiceSegments = WeaponChoiceSegments;
            if (UnmarkChoicesFromSets(ref weaponChoiceSegments, SetWeaponProfs)) { return; }

            Dictionary<string, int> dupCheck = GetChoiceMarkedCounts(ref weaponChoiceSegments);
            foreach (KeyValuePair<string, int> entry in dupCheck)
            {
                if (entry.Value > 1) { (sender as BoolOption).Marked = false; return; }
            }

            foreach (ChoiceSet weaponChoiceSeg in WeaponChoiceSegments)
            {
                int selectedWeaponCount = 0;
                foreach (BoolOption weaponChoice in weaponChoiceSeg.Choices)
                {
                    if (weaponChoice.Marked) { selectedWeaponCount++; }
                }
                if (selectedWeaponCount > weaponChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedWeaponCount--;
                }
                weaponChoiceSeg.ChoicesRemaining = weaponChoiceSeg.MaxChoices - selectedWeaponCount;
            }
        }
        private void ArmorChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ObservableCollection<ChoiceSet> armorChoiceSegments = ArmorChoiceSegments;
            if (UnmarkChoicesFromSets(ref armorChoiceSegments, SetArmorProfs)) { return; }

            Dictionary<string, int> dupCheck = GetChoiceMarkedCounts(ref armorChoiceSegments);
            foreach (KeyValuePair<string, int> entry in dupCheck)
            {
                if (entry.Value > 1) { (sender as BoolOption).Marked = false; return; }
            }

            foreach (ChoiceSet armorChoiceSeg in ArmorChoiceSegments)
            {
                int selectedArmorCount = 0;
                foreach (BoolOption armorChoice in armorChoiceSeg.Choices)
                {
                    if (armorChoice.Marked) { selectedArmorCount++; }
                }
                if (selectedArmorCount > armorChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedArmorCount--;
                }
                armorChoiceSeg.ChoicesRemaining = armorChoiceSeg.MaxChoices - selectedArmorCount;
            }
        }
        private void TraitChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (ChoiceSet traitChoiceSeg in TraitChoiceSegments)
            {
                int selectedTraitCount = 0;
                foreach (BoolOption traitChoice in traitChoiceSeg.Choices)
                {
                    if (traitChoice.Marked) { selectedTraitCount++; }
                }
                if (selectedTraitCount > traitChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedTraitCount--;
                }
                traitChoiceSeg.ChoicesRemaining = traitChoiceSeg.MaxChoices - selectedTraitCount;
            }
        }
        private void AttributeChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (ChoiceSet attrChoiceSeg in StatBonusChoiceSegments)
            {
                int selectedAttrCount = 0;
                foreach (BoolOption attrChoice in attrChoiceSeg.Choices)
                {
                    if (attrChoice.Marked) { selectedAttrCount++; }
                }
                if (selectedAttrCount > attrChoiceSeg.MaxChoices)
                {
                    (sender as BoolOption).Marked = false;
                    selectedAttrCount--;
                }
                attrChoiceSeg.ChoicesRemaining = attrChoiceSeg.MaxChoices - selectedAttrCount;
            }
            UpdateAttributeSets();
        }
        private void SpellChoice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ObservableCollection<ChoiceSet> spellChoiceSegments = SpellChoiceSegments;

            Dictionary<string, int> dupCheck = GetChoiceMarkedCounts(ref spellChoiceSegments);
            foreach (KeyValuePair<string, int> entry in dupCheck)
            {
                if (entry.Value > 1) { (sender as BoolOption).Marked = false; return; }
            }

            foreach (ChoiceSet spellChoiceSeg in SpellChoiceSegments)
            {
                if (spellChoiceSeg.ChoiceRestricted == false) { continue; }
                int selectedSpellCount = 0;
                foreach (BoolOption spellChoice in spellChoiceSeg.Choices)
                {
                    if (spellChoice.Marked) { selectedSpellCount++; }
                }
                if (selectedSpellCount > spellChoiceSeg.MaxChoices)
                {
                    if ((sender as BoolOption).Marked)
                    {
                        (sender as BoolOption).Marked = false;
                    }
                    else
                    {
                        BoolOption spell = spellChoiceSeg.Choices.FirstOrDefault(s => s.Marked);
                        if (spell != null) { spell.Marked = false; }
                    }
                    selectedSpellCount--;
                }
                spellChoiceSeg.ChoicesRemaining = spellChoiceSeg.MaxChoices - selectedSpellCount;
            }
        }
        private void CharacterAttributeFeatChoiceSelection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCharacterSheet();
        }
        private void Feat_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            int featPtsUsed = 0;
            foreach (BoolOption feat in FeatChoices)
            {
                if (feat.Marked) { featPtsUsed++; }
            }
            if (featPtsUsed > GetMaxFeatPoints())
            {
                if ((sender as BoolOption).Marked)
                {
                    (sender as BoolOption).Marked = false;
                }
                else
                {
                    BoolOption feat = FeatChoices.FirstOrDefault(f => f.Marked);
                    if (feat != null) { feat.Marked = false; }
                }
            }
            else
            {
                FeatPoints = GetMaxFeatPoints() - featPtsUsed;
            }
            UpdateCharacterSheet();
        }
        private void UpdateFeatUsage()
        {
            int featPtsUsed = 0;
            foreach (BoolOption feat in FeatChoices)
            {
                if (feat.Marked) { featPtsUsed++; }
            }
            FeatPoints = GetMaxFeatPoints() - featPtsUsed;
            if (FeatPoints < 0)
            {
                BoolOption featChoice = FeatChoices.FirstOrDefault(f => f.Marked);
                if (featChoice != null) { featChoice.Marked = false; }
            }
        }
        private void CharacterAttributeSet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CalculateFinalScores();
        }
        
        

    }

}
