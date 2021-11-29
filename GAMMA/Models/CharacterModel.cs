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
using System.Windows;
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
        #region Icon
        private string _Icon;
        [XmlSaveMode("Single")]
        public string Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Race
        private string _Race;
        [XmlSaveMode("Single")]
        public string Race
        {
            get
            {
                return _Race;
            }
            set
            {
                _Race = value;
                NotifyPropertyChanged();
                LinkedRace = Configuration.MainModelRef.ToolsView.PlayerRaces.FirstOrDefault(race => race.Name == value);
                UpdateSubraceList();
                if (Subraces.Contains(Subrace) == false) { Subrace = ""; }
                UpdateDisplayRace();
            }
        }
        #endregion
        #region Alignment
        private string _Alignment;
        [XmlSaveMode("Single")]
        public string Alignment
        {
            get
            {
                return _Alignment;
            }
            set
            {
                _Alignment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ClassInfo
        private string _ClassInfo;
        [XmlSaveMode("Single")]
        public string ClassInfo
        {
            get
            {
                return _ClassInfo;
            }
            set
            {
                _ClassInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Level
        private int _Level;
        [XmlSaveMode("Single")]
        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
                NotifyPropertyChanged();
                ProficiencyBonus = HelperMethods.GetProfBonusFromCr(Level.ToString());
            }
        }
        #endregion
        #region Background
        private string _Background;
        [XmlSaveMode("Single")]
        public string Background
        {
            get
            {
                return _Background;
            }
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
        [XmlSaveMode("Single")]
        public string Personality
        {
            get
            {
                return _Personality;
            }
            set
            {
                _Personality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Ideals
        private string _Ideals;
        [XmlSaveMode("Single")]
        public string Ideals
        {
            get
            {
                return _Ideals;
            }
            set
            {
                _Ideals = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Bonds
        private string _Bonds;
        [XmlSaveMode("Single")]
        public string Bonds
        {
            get
            {
                return _Bonds;
            }
            set
            {
                _Bonds = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Flaws
        private string _Flaws;
        [XmlSaveMode("Single")]
        public string Flaws
        {
            get
            {
                return _Flaws;
            }
            set
            {
                _Flaws = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Backstory
        private string _Backstory;
        [XmlSaveMode("Single")]
        public string Backstory
        {
            get
            {
                return _Backstory;
            }
            set
            {
                _Backstory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Languages
        private string _Languages;
        [XmlSaveMode("Single")]
        public string Languages
        {
            get
            {
                return _Languages;
            }
            set
            {
                _Languages = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OtherProficiencies
        private string _OtherProficiencies;
        [XmlSaveMode("Single")]
        public string OtherProficiencies
        {
            get
            {
                return _OtherProficiencies;
            }
            set
            {
                _OtherProficiencies = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ToolProficiencies
        private ObservableCollection<ItemModel> _ToolProficiencies;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemModel> ToolProficiencies
        {
            get
            {
                return _ToolProficiencies;
            }
            set
            {
                _ToolProficiencies = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RawWeight
        private int _RawWeight;
        [XmlSaveMode("Single")]
        public int RawWeight
        {
            get
            {
                return _RawWeight;
            }
            set
            {
                _RawWeight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RawHeight
        private int _RawHeight;
        [XmlSaveMode("Single")]
        public int RawHeight
        {
            get
            {
                return _RawHeight;
            }
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
            get
            {
                return _ProcessedHeight;
            }
            set
            {
                _ProcessedHeight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PlayerClasses
        private ObservableCollection<PlayerClassLinkModel> _PlayerClasses;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<PlayerClassLinkModel> PlayerClasses
        {
            get
            {
                return _PlayerClasses;
            }
            set
            {
                _PlayerClasses = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ClassAutoText
        private string _ClassAutoText;
        public string ClassAutoText
        {
            get
            {
                return _ClassAutoText;
            }
            set
            {
                _ClassAutoText = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SubclassAutoText
        private string _SubclassAutoText;
        public string SubclassAutoText
        {
            get
            {
                return _SubclassAutoText;
            }
            set
            {
                _SubclassAutoText = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TotalLevel
        private int _TotalLevel;
        public int TotalLevel
        {
            get
            {
                return _TotalLevel;
            }
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
            get
            {
                return _Subraces;
            }
            set
            {
                _Subraces = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Subrace
        private string _Subrace;
        [XmlSaveMode("Single")]
        public string Subrace
        {
            get
            {
                return _Subrace;
            }
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
        [XmlSaveMode("Single")]
        public int ExperiencePoints
        {
            get
            {
                return _ExperiencePoints;
            }
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
            get
            {
                return _XpToNext;
            }
            set
            {
                _XpToNext = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayRace
        private string _DisplayRace;
        public string DisplayRace
        {
            get
            {
                return _DisplayRace;
            }
            set
            {
                _DisplayRace = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ActionHistory
        private ObservableCollection<string> _ActionHistory;
        [XmlSaveMode("")]
        public ObservableCollection<string> ActionHistory
        {
            get
            {
                return _ActionHistory;
            }
            set
            {
                _ActionHistory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OutputLinkedToRoll20
        private bool _OutputLinkedToRoll20;
        public bool OutputLinkedToRoll20
        {
            get
            {
                return _OutputLinkedToRoll20;
            }
            set
            {
                _OutputLinkedToRoll20 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Messages
        private ObservableCollection<GameMessage> _Messages;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<GameMessage> Messages
        {
            get
            {
                return _Messages;
            }
            set
            {
                _Messages = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region FinalArmorClass
        private int _FinalArmorClass;
        public int FinalArmorClass
        {
            get
            {
                return _FinalArmorClass;
            }
            set
            {
                _FinalArmorClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FinalSpeed
        private int _FinalSpeed;
        public int FinalSpeed
        {
            get
            {
                return _FinalSpeed;
            }
            set
            {
                _FinalSpeed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Character Creation
        #region HasCompletedCharacterCreation
        private bool _HasCompletedCharacterCreation;
        [XmlSaveMode("Single")]
        public bool HasCompletedCharacterCreation
        {
            get
            {
                return _HasCompletedCharacterCreation;
            }
            set
            {
                _HasCompletedCharacterCreation = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region BaseAttributePoints
        private int _BaseAttributePoints;
        [XmlSaveMode("Single")]
        public int BaseAttributePoints
        {
            get
            {
                return _BaseAttributePoints;
            }
            set
            {
                _BaseAttributePoints = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StrengthBaseScore
        private int _StrengthBaseScore;
        [XmlSaveMode("Single")]
        public int StrengthBaseScore
        {
            get
            {
                return _StrengthBaseScore;
            }
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
        [XmlSaveMode("Single")]
        public int DexterityBaseScore
        {
            get
            {
                return _DexterityBaseScore;
            }
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
        [XmlSaveMode("Single")]
        public int ConstitutionBaseScore
        {
            get
            {
                return _ConstitutionBaseScore;
            }
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
        [XmlSaveMode("Single")]
        public int IntelligenceBaseScore
        {
            get
            {
                return _IntelligenceBaseScore;
            }
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
        [XmlSaveMode("Single")]
        public int WisdomBaseScore
        {
            get
            {
                return _WisdomBaseScore;
            }
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
        [XmlSaveMode("Single")]
        public int CharismaBaseScore
        {
            get
            {
                return _CharismaBaseScore;
            }
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
            get
            {
                return _StrengthFinalScore;
            }
            set
            {
                _StrengthFinalScore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DexterityFinalScore
        private int _DexterityFinalScore;
        public int DexterityFinalScore
        {
            get
            {
                return _DexterityFinalScore;
            }
            set
            {
                _DexterityFinalScore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConstitutionFinalScore
        private int _ConstitutionFinalScore;
        public int ConstitutionFinalScore
        {
            get
            {
                return _ConstitutionFinalScore;
            }
            set
            {
                _ConstitutionFinalScore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntelligenceFinalScore
        private int _IntelligenceFinalScore;
        public int IntelligenceFinalScore
        {
            get
            {
                return _IntelligenceFinalScore;
            }
            set
            {
                _IntelligenceFinalScore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WisdomFinalScore
        private int _WisdomFinalScore;
        public int WisdomFinalScore
        {
            get
            {
                return _WisdomFinalScore;
            }
            set
            {
                _WisdomFinalScore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharismaFinalScore
        private int _CharismaFinalScore;
        public int CharismaFinalScore
        {
            get
            {
                return _CharismaFinalScore;
            }
            set
            {
                _CharismaFinalScore = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region StrengthFinalModifier
        private int _StrengthFinalModifier;
        public int StrengthFinalModifier
        {
            get
            {
                return _StrengthFinalModifier;
            }
            set
            {
                _StrengthFinalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DexterityFinalModifier
        private int _DexterityFinalModifier;
        public int DexterityFinalModifier
        {
            get
            {
                return _DexterityFinalModifier;
            }
            set
            {
                _DexterityFinalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConstitutionFinalModifier
        private int _ConstitutionFinalModifier;
        public int ConstitutionFinalModifier
        {
            get
            {
                return _ConstitutionFinalModifier;
            }
            set
            {
                _ConstitutionFinalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntelligenceFinalModifier
        private int _IntelligenceFinalModifier;
        public int IntelligenceFinalModifier
        {
            get
            {
                return _IntelligenceFinalModifier;
            }
            set
            {
                _IntelligenceFinalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WisdomFinalModifier
        private int _WisdomFinalModifier;
        public int WisdomFinalModifier
        {
            get
            {
                return _WisdomFinalModifier;
            }
            set
            {
                _WisdomFinalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharismaFinalModifier
        private int _CharismaFinalModifier;
        public int CharismaFinalModifier
        {
            get
            {
                return _CharismaFinalModifier;
            }
            set
            {
                _CharismaFinalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        
        #region FeatPoints
        private int _FeatPoints;
        public int FeatPoints
        {
            get
            {
                return _FeatPoints;
            }
            set
            {
                _FeatPoints = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Races
        private List<string> _Races;
        public List<string> Races
        {
            get
            {
                return _Races;
            }
            set
            {
                _Races = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LinkedRace
        private PlayerRaceModel _LinkedRace;
        public PlayerRaceModel LinkedRace
        {
            get
            {
                return _LinkedRace;
            }
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
            get
            {
                return _LinkedSubrace;
            }
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
            get
            {
                return _Backgrounds;
            }
            set
            {
                _Backgrounds = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LinkedBackground
        private PlayerBackgroundModel _LinkedBackground;
        public PlayerBackgroundModel LinkedBackground
        {
            get
            {
                return _LinkedBackground;
            }
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
            get
            {
                return _Alignments;
            }
            set
            {
                _Alignments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SetLanguageProfs
        private List<string> _SetLanguageProfs;
        public List<string> SetLanguageProfs
        {
            get
            {
                return _SetLanguageProfs;
            }
            set
            {
                _SetLanguageProfs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LanguageChoiceSegments
        private ObservableCollection<ChoiceSet> _LanguageChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> LanguageChoiceSegments
        {
            get
            {
                return _LanguageChoiceSegments;
            }
            set
            {
                _LanguageChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SetToolProfs
        private List<string> _SetToolProfs;
        public List<string> SetToolProfs
        {
            get
            {
                return _SetToolProfs;
            }
            set
            {
                _SetToolProfs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ToolChoiceSegments
        private ObservableCollection<ChoiceSet> _ToolChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> ToolChoiceSegments
        {
            get
            {
                return _ToolChoiceSegments;
            }
            set
            {
                _ToolChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SetSkillProfs
        private List<string> _SetSkillProfs;
        public List<string> SetSkillProfs
        {
            get
            {
                return _SetSkillProfs;
            }
            set
            {
                _SetSkillProfs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SkillChoiceSegments
        private ObservableCollection<ChoiceSet> _SkillChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> SkillChoiceSegments
        {
            get
            {
                return _SkillChoiceSegments;
            }
            set
            {
                _SkillChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExpertiseChoiceSegments
        private ObservableCollection<ChoiceSet> _ExpertiseChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> ExpertiseChoiceSegments
        {
            get
            {
                return _ExpertiseChoiceSegments;
            }
            set
            {
                _ExpertiseChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SetWeaponProfs
        private List<string> _SetWeaponProfs;
        public List<string> SetWeaponProfs
        {
            get
            {
                return _SetWeaponProfs;
            }
            set
            {
                _SetWeaponProfs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeaponChoiceSegments
        private ObservableCollection<ChoiceSet> _WeaponChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> WeaponChoiceSegments
        {
            get
            {
                return _WeaponChoiceSegments;
            }
            set
            {
                _WeaponChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SetArmorProfs
        private List<string> _SetArmorProfs;
        public List<string> SetArmorProfs
        {
            get
            {
                return _SetArmorProfs;
            }
            set
            {
                _SetArmorProfs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ArmorChoiceSegments
        private ObservableCollection<ChoiceSet> _ArmorChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> ArmorChoiceSegments
        {
            get
            {
                return _ArmorChoiceSegments;
            }
            set
            {
                _ArmorChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region AttributeFeatChoices
        private ObservableCollection<ChoiceSet> _AttributeFeatChoices;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> AttributeFeatChoices
        {
            get
            {
                return _AttributeFeatChoices;
            }
            set
            {
                _AttributeFeatChoices = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FeatChoices
        private ObservableCollection<BoolOption> _FeatChoices;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<BoolOption> FeatChoices
        {
            get
            {
                return _FeatChoices;
            }
            set
            {
                _FeatChoices = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region CCAttributeSets
        private ObservableCollection<CharacterAttributeSet> _CCAttributeSets;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CharacterAttributeSet> CCAttributeSets
        {
            get
            {
                return _CCAttributeSets;
            }
            set
            {
                _CCAttributeSets = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region StartingGold
        private int _StartingGold;
        public int StartingGold
        {
            get
            {
                return _StartingGold;
            }
            set
            {
                _StartingGold = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StartingEquipmentChoiceSets
        private ObservableCollection<ConvertibleValueSet> _StartingEquipmentChoiceSets;
        public ObservableCollection<ConvertibleValueSet> StartingEquipmentChoiceSets
        {
            get
            {
                return _StartingEquipmentChoiceSets;
            }
            set
            {
                _StartingEquipmentChoiceSets = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ChosenEquipment
        private ObservableCollection<ItemLink> _ChosenEquipment;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemLink> ChosenEquipment
        {
            get
            {
                return _ChosenEquipment;
            }
            set
            {
                _ChosenEquipment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region GrantedEquipment
        private ObservableCollection<ItemLink> _GrantedEquipment;
        public ObservableCollection<ItemLink> GrantedEquipment
        {
            get
            {
                return _GrantedEquipment;
            }
            set
            {
                _GrantedEquipment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region TraitChoiceSegments
        private ObservableCollection<ChoiceSet> _TraitChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> TraitChoiceSegments
        {
            get
            {
                return _TraitChoiceSegments;
            }
            set
            {
                _TraitChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SetTraits
        private ObservableCollection<TraitModel> _SetTraits;
        public ObservableCollection<TraitModel> SetTraits
        {
            get
            {
                return _SetTraits;
            }
            set
            {
                _SetTraits = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region StatBonusChoiceSegments
        private ObservableCollection<ChoiceSet> _StatBonusChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> StatBonusChoiceSegments
        {
            get
            {
                return _StatBonusChoiceSegments;
            }
            set
            {
                _StatBonusChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MaxHpCalc
        private int _MaxHpCalc;
        public int MaxHpCalc
        {
            get
            {
                return _MaxHpCalc;
            }
            set
            {
                _MaxHpCalc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SpellChoiceSegments
        private ObservableCollection<ChoiceSet> _SpellChoiceSegments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ChoiceSet> SpellChoiceSegments
        {
            get
            {
                return _SpellChoiceSegments;
            }
            set
            {
                _SpellChoiceSegments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Attributes and Skills
        #region StrengthScore
        private int _StrengthScore;
        [XmlSaveMode("Single")]
        public int StrengthScore
        {
            get
            {
                return _StrengthScore;
            }
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
        [XmlSaveMode("Single")]
        public int DexterityScore
        {
            get
            {
                return _DexterityScore;
            }
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
        [XmlSaveMode("Single")]
        public int ConstitutionScore
        {
            get
            {
                return _ConstitutionScore;
            }
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
        [XmlSaveMode("Single")]
        public int IntelligenceScore
        {
            get
            {
                return _IntelligenceScore;
            }
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
        [XmlSaveMode("Single")]
        public int WisdomScore
        {
            get
            {
                return _WisdomScore;
            }
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
        [XmlSaveMode("Single")]
        public int CharismaScore
        {
            get
            {
                return _CharismaScore;
            }
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
        [XmlSaveMode("Single")]
        public int StrengthBonus
        {
            get
            {
                return _StrengthBonus;
            }
            set
            {
                _StrengthBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DexterityBonus
        private int _DexterityBonus;
        [XmlSaveMode("Single")]
        public int DexterityBonus
        {
            get
            {
                return _DexterityBonus;
            }
            set
            {
                _DexterityBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConstitutionBonus
        private int _ConstitutionBonus;
        [XmlSaveMode("Single")]
        public int ConstitutionBonus
        {
            get
            {
                return _ConstitutionBonus;
            }
            set
            {
                _ConstitutionBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntelligenceBonus
        private int _IntelligenceBonus;
        [XmlSaveMode("Single")]
        public int IntelligenceBonus
        {
            get
            {
                return _IntelligenceBonus;
            }
            set
            {
                _IntelligenceBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WisdomBonus
        private int _WisdomBonus;
        [XmlSaveMode("Single")]
        public int WisdomBonus
        {
            get
            {
                return _WisdomBonus;
            }
            set
            {
                _WisdomBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharismaBonus
        private int _CharismaBonus;
        [XmlSaveMode("Single")]
        public int CharismaBonus
        {
            get
            {
                return _CharismaBonus;
            }
            set
            {
                _CharismaBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region StrengthModifier
        private int _StrengthModifier;
        public int StrengthModifier
        {
            get
            {
                return _StrengthModifier;
            }
            set
            {
                _StrengthModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DexterityModifier
        private int _DexterityModifier;
        public int DexterityModifier
        {
            get
            {
                return _DexterityModifier;
            }
            set
            {
                _DexterityModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConstitutionModifier
        private int _ConstitutionModifier;
        public int ConstitutionModifier
        {
            get
            {
                return _ConstitutionModifier;
            }
            set
            {
                _ConstitutionModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntelligenceModifier
        private int _IntelligenceModifier;
        public int IntelligenceModifier
        {
            get
            {
                return _IntelligenceModifier;
            }
            set
            {
                _IntelligenceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WisdomModifier
        private int _WisdomModifier;
        public int WisdomModifier
        {
            get
            {
                return _WisdomModifier;
            }
            set
            {
                _WisdomModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharismaModifier
        private int _CharismaModifier;
        public int CharismaModifier
        {
            get
            {
                return _CharismaModifier;
            }
            set
            {
                _CharismaModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasSave_Strength
        private bool _HasSave_Strength;
        [XmlSaveMode("Single")]
        public bool HasSave_Strength
        {
            get
            {
                return _HasSave_Strength;
            }
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
        [XmlSaveMode("Single")]
        public bool HasSave_Dexterity
        {
            get
            {
                return _HasSave_Dexterity;
            }
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
        [XmlSaveMode("Single")]
        public bool HasSave_Constitution
        {
            get
            {
                return _HasSave_Constitution;
            }
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
        [XmlSaveMode("Single")]
        public bool HasSave_Intelligence
        {
            get
            {
                return _HasSave_Intelligence;
            }
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
        [XmlSaveMode("Single")]
        public bool HasSave_Wisdom
        {
            get
            {
                return _HasSave_Wisdom;
            }
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
        [XmlSaveMode("Single")]
        public bool HasSave_Charisma
        {
            get
            {
                return _HasSave_Charisma;
            }
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
            get
            {
                return _StrengthSave;
            }
            set
            {
                _StrengthSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DexteritySave
        private int _DexteritySave;
        public int DexteritySave
        {
            get
            {
                return _DexteritySave;
            }
            set
            {
                _DexteritySave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConstitutionSave
        private int _ConstitutionSave;
        public int ConstitutionSave
        {
            get
            {
                return _ConstitutionSave;
            }
            set
            {
                _ConstitutionSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntelligenceSave
        private int _IntelligenceSave;
        public int IntelligenceSave
        {
            get
            {
                return _IntelligenceSave;
            }
            set
            {
                _IntelligenceSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WisdomSave
        private int _WisdomSave;
        public int WisdomSave
        {
            get
            {
                return _WisdomSave;
            }
            set
            {
                _WisdomSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharismaSave
        private int _CharismaSave;
        public int CharismaSave
        {
            get
            {
                return _CharismaSave;
            }
            set
            {
                _CharismaSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region IsProf_Acrobatics
        private bool _IsProf_Acrobatics;
        [XmlSaveMode("Single")]
        public bool IsProf_Acrobatics
        {
            get
            {
                return _IsProf_Acrobatics;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_AnimalHandling
        {
            get
            {
                return _IsProf_AnimalHandling;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Arcana
        {
            get
            {
                return _IsProf_Arcana;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Athletics
        {
            get
            {
                return _IsProf_Athletics;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Deception
        {
            get
            {
                return _IsProf_Deception;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_History
        {
            get
            {
                return _IsProf_History;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Insight
        {
            get
            {
                return _IsProf_Insight;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Intimidation
        {
            get
            {
                return _IsProf_Intimidation;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Investigation
        {
            get
            {
                return _IsProf_Investigation;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Medicine
        {
            get
            {
                return _IsProf_Medicine;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Nature
        {
            get
            {
                return _IsProf_Nature;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Perception
        {
            get
            {
                return _IsProf_Perception;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Performance
        {
            get
            {
                return _IsProf_Performance;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Persuasion
        {
            get
            {
                return _IsProf_Persuasion;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Religion
        {
            get
            {
                return _IsProf_Religion;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_SleightOfHand
        {
            get
            {
                return _IsProf_SleightOfHand;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Stealth
        {
            get
            {
                return _IsProf_Stealth;
            }
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
        [XmlSaveMode("Single")]
        public bool IsProf_Survival
        {
            get
            {
                return _IsProf_Survival;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Acrobatics
        {
            get
            {
                return _IsExpert_Acrobatics;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_AnimalHandling
        {
            get
            {
                return _IsExpert_AnimalHandling;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Arcana
        {
            get
            {
                return _IsExpert_Arcana;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Athletics
        {
            get
            {
                return _IsExpert_Athletics;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Deception
        {
            get
            {
                return _IsExpert_Deception;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_History
        {
            get
            {
                return _IsExpert_History;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Insight
        {
            get
            {
                return _IsExpert_Insight;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Intimidation
        {
            get
            {
                return _IsExpert_Intimidation;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Investigation
        {
            get
            {
                return _IsExpert_Investigation;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Medicine
        {
            get
            {
                return _IsExpert_Medicine;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Nature
        {
            get
            {
                return _IsExpert_Nature;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Perception
        {
            get
            {
                return _IsExpert_Perception;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Performance
        {
            get
            {
                return _IsExpert_Performance;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Persuasion
        {
            get
            {
                return _IsExpert_Persuasion;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Religion
        {
            get
            {
                return _IsExpert_Religion;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_SleightOfHand
        {
            get
            {
                return _IsExpert_SleightOfHand;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Stealth
        {
            get
            {
                return _IsExpert_Stealth;
            }
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
        [XmlSaveMode("Single")]
        public bool IsExpert_Survival
        {
            get
            {
                return _IsExpert_Survival;
            }
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
        [XmlSaveMode("Single")]
        public int AcrobaticsBonus
        {
            get
            {
                return _AcrobaticsBonus;
            }
            set
            {
                _AcrobaticsBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AnimalHandlingBonus
        private int _AnimalHandlingBonus;
        [XmlSaveMode("Single")]
        public int AnimalHandlingBonus
        {
            get
            {
                return _AnimalHandlingBonus;
            }
            set
            {
                _AnimalHandlingBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ArcanaBonus
        private int _ArcanaBonus;
        [XmlSaveMode("Single")]
        public int ArcanaBonus
        {
            get
            {
                return _ArcanaBonus;
            }
            set
            {
                _ArcanaBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AthleticsBonus
        private int _AthleticsBonus;
        [XmlSaveMode("Single")]
        public int AthleticsBonus
        {
            get
            {
                return _AthleticsBonus;
            }
            set
            {
                _AthleticsBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeceptionBonus
        private int _DeceptionBonus;
        [XmlSaveMode("Single")]
        public int DeceptionBonus
        {
            get
            {
                return _DeceptionBonus;
            }
            set
            {
                _DeceptionBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HistoryBonus
        private int _HistoryBonus;
        [XmlSaveMode("Single")]
        public int HistoryBonus
        {
            get
            {
                return _HistoryBonus;
            }
            set
            {
                _HistoryBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InsightBonus
        private int _InsightBonus;
        [XmlSaveMode("Single")]
        public int InsightBonus
        {
            get
            {
                return _InsightBonus;
            }
            set
            {
                _InsightBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntimidationBonus
        private int _IntimidationBonus;
        [XmlSaveMode("Single")]
        public int IntimidationBonus
        {
            get
            {
                return _IntimidationBonus;
            }
            set
            {
                _IntimidationBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InvestigationBonus
        private int _InvestigationBonus;
        [XmlSaveMode("Single")]
        public int InvestigationBonus
        {
            get
            {
                return _InvestigationBonus;
            }
            set
            {
                _InvestigationBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MedicineBonus
        private int _MedicineBonus;
        [XmlSaveMode("Single")]
        public int MedicineBonus
        {
            get
            {
                return _MedicineBonus;
            }
            set
            {
                _MedicineBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NatureBonus
        private int _NatureBonus;
        [XmlSaveMode("Single")]
        public int NatureBonus
        {
            get
            {
                return _NatureBonus;
            }
            set
            {
                _NatureBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PerceptionBonus
        private int _PerceptionBonus;
        [XmlSaveMode("Single")]
        public int PerceptionBonus
        {
            get
            {
                return _PerceptionBonus;
            }
            set
            {
                _PerceptionBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PerformanceBonus
        private int _PerformanceBonus;
        [XmlSaveMode("Single")]
        public int PerformanceBonus
        {
            get
            {
                return _PerformanceBonus;
            }
            set
            {
                _PerformanceBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PersuasionBonus
        private int _PersuasionBonus;
        [XmlSaveMode("Single")]
        public int PersuasionBonus
        {
            get
            {
                return _PersuasionBonus;
            }
            set
            {
                _PersuasionBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ReligionBonus
        private int _ReligionBonus;
        [XmlSaveMode("Single")]
        public int ReligionBonus
        {
            get
            {
                return _ReligionBonus;
            }
            set
            {
                _ReligionBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SleightOfHandBonus
        private int _SleightOfHandBonus;
        [XmlSaveMode("Single")]
        public int SleightOfHandBonus
        {
            get
            {
                return _SleightOfHandBonus;
            }
            set
            {
                _SleightOfHandBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StealthBonus
        private int _StealthBonus;
        [XmlSaveMode("Single")]
        public int StealthBonus
        {
            get
            {
                return _StealthBonus;
            }
            set
            {
                _StealthBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SurvivalBonus
        private int _SurvivalBonus;
        [XmlSaveMode("Single")]
        public int SurvivalBonus
        {
            get
            {
                return _SurvivalBonus;
            }
            set
            {
                _SurvivalBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region AcrobaticsModifier
        private int _AcrobaticsModifier;
        public int AcrobaticsModifier
        {
            get
            {
                return _AcrobaticsModifier;
            }
            set
            {
                _AcrobaticsModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AnimalHandlingModifier
        private int _AnimalHandlingModifier;
        public int AnimalHandlingModifier
        {
            get
            {
                return _AnimalHandlingModifier;
            }
            set
            {
                _AnimalHandlingModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ArcanaModifier
        private int _ArcanaModifier;
        public int ArcanaModifier
        {
            get
            {
                return _ArcanaModifier;
            }
            set
            {
                _ArcanaModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AthleticsModifier
        private int _AthleticsModifier;
        public int AthleticsModifier
        {
            get
            {
                return _AthleticsModifier;
            }
            set
            {
                _AthleticsModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeceptionModifier
        private int _DeceptionModifier;
        public int DeceptionModifier
        {
            get
            {
                return _DeceptionModifier;
            }
            set
            {
                _DeceptionModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HistoryModifier
        private int _HistoryModifier;
        public int HistoryModifier
        {
            get
            {
                return _HistoryModifier;
            }
            set
            {
                _HistoryModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InsightModifier
        private int _InsightModifier;
        public int InsightModifier
        {
            get
            {
                return _InsightModifier;
            }
            set
            {
                _InsightModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntimidationModifier
        private int _IntimidationModifier;
        public int IntimidationModifier
        {
            get
            {
                return _IntimidationModifier;
            }
            set
            {
                _IntimidationModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InvestigationModifier
        private int _InvestigationModifier;
        [XmlSaveMode("Single")]
        public int InvestigationModifier
        {
            get
            {
                return _InvestigationModifier;
            }
            set
            {
                _InvestigationModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MedicineModifier
        private int _MedicineModifier;
        public int MedicineModifier
        {
            get
            {
                return _MedicineModifier;
            }
            set
            {
                _MedicineModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NatureModifier
        private int _NatureModifier;
        public int NatureModifier
        {
            get
            {
                return _NatureModifier;
            }
            set
            {
                _NatureModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PerceptionModifier
        private int _PerceptionModifier;
        public int PerceptionModifier
        {
            get
            {
                return _PerceptionModifier;
            }
            set
            {
                _PerceptionModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PerformanceModifier
        private int _PerformanceModifier;
        public int PerformanceModifier
        {
            get
            {
                return _PerformanceModifier;
            }
            set
            {
                _PerformanceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PersuasionModifier
        private int _PersuasionModifier;
        public int PersuasionModifier
        {
            get
            {
                return _PersuasionModifier;
            }
            set
            {
                _PersuasionModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ReligionModifier
        private int _ReligionModifier;
        public int ReligionModifier
        {
            get
            {
                return _ReligionModifier;
            }
            set
            {
                _ReligionModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SleightOfHandModifier
        private int _SleightOfHandModifier;
        public int SleightOfHandModifier
        {
            get
            {
                return _SleightOfHandModifier;
            }
            set
            {
                _SleightOfHandModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StealthModifier
        private int _StealthModifier;
        public int StealthModifier
        {
            get
            {
                return _StealthModifier;
            }
            set
            {
                _StealthModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SurvivalModifier
        private int _SurvivalModifier;
        public int SurvivalModifier
        {
            get
            {
                return _SurvivalModifier;
            }
            set
            {
                _SurvivalModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region AllSkillBonus
        private int _AllSkillBonus;
        [XmlSaveMode("Single")]
        public int AllSkillBonus
        {
            get
            {
                return _AllSkillBonus;
            }
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
        [XmlSaveMode("Single")]
        public int Darkvision
        {
            get
            {
                return _Darkvision;
            }
            set
            {
                _Darkvision = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasPowerfulBuild
        private bool _HasPowerfulBuild;
        [XmlSaveMode("Single")]
        public bool HasPowerfulBuild
        {
            get
            {
                return _HasPowerfulBuild;
            }
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
        [XmlSaveMode("Single")]
        public int HitPointMaxBonus
        {
            get
            {
                return _HitPointMaxBonus;
            }
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
            get
            {
                return _ProficiencyBonus;
            }
            set
            {
                _ProficiencyBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Speed
        private int _Speed;
        [XmlSaveMode("Single")]
        public int Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ArmorClass
        private int _ArmorClass;
        [XmlSaveMode("Single")]
        public int ArmorClass
        {
            get
            {
                return _ArmorClass;
            }
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
        [XmlSaveMode("Single")]
        public int AltArmorClass
        {
            get
            {
                return _AltArmorClass;
            }
            set
            {
                _AltArmorClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PassivePerception
        private int _PassivePerception;
        [XmlSaveMode("Single")]
        public int PassivePerception
        {
            get
            {
                return _PassivePerception;
            }
            set
            {
                _PassivePerception = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Initiative
        private int _Initiative;
        [XmlSaveMode("Single")]
        public int Initiative
        {
            get
            {
                return _Initiative;
            }
            set
            {
                _Initiative = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region CurrentHealth
        private int _CurrentHealth;
        [XmlSaveMode("Single")]
        public int CurrentHealth
        {
            get
            {
                return _CurrentHealth;
            }
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
        [XmlSaveMode("Single")]
        public int MaxHealth
        {
            get
            {
                return _MaxHealth;
            }
            set
            {
                _MaxHealth = value;
                NotifyPropertyChanged();
                UpdateStatus();
            }
        }
        #endregion
        #region Status
        // Dynamic status for the Creature based on health or condition.
        private string _Status;
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TempHealth
        private int _TempHealth;
        [XmlSaveMode("Single")]
        public int TempHealth
        {
            get
            {
                return _TempHealth;
            }
            set
            {
                _TempHealth = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HitDiceSets
        private ObservableCollection<HitDiceSet> _HitDiceSets;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<HitDiceSet> HitDiceSets
        {
            get
            {
                return _HitDiceSets;
            }
            set
            {
                _HitDiceSets = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region DisplayPopup_Skills
        private bool _DisplayPopup_Skills;
        public bool DisplayPopup_Skills
        {
            get
            {
                return _DisplayPopup_Skills;
            }
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
            get
            {
                return _DisplayPopup_Saves;
            }
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
            get
            {
                return _DisplayPopup_Checks;
            }
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
            get
            {
                return _DisplayPopup_Dice;
            }
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
            get
            {
                return _DisplayPopup_Health;
            }
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
            get
            {
                return _DisplayPopup_Tools;
            }
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
        [XmlSaveMode("Single")]
        public bool DisplayPopup_Rest
        {
            get
            {
                return _DisplayPopup_Rest;
            }
            set
            {
                _DisplayPopup_Rest = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopup_Tables
        private bool _DisplayPopup_Tables;
        public bool DisplayPopup_Tables
        {
            get
            {
                return _DisplayPopup_Tables;
            }
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
            get
            {
                return _DisplayPopup_StandardActions;
            }
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
            get
            {
                return _CustomDiceQuantity;
            }
            set
            {
                _CustomDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CustomDiceSides
        private int _CustomDiceSides;
        public int CustomDiceSides
        {
            get
            {
                return _CustomDiceSides;
            }
            set
            {
                _CustomDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CustomDiceMod
        private int _CustomDiceMod;
        public int CustomDiceMod
        {
            get
            {
                return _CustomDiceMod;
            }
            set
            {
                _CustomDiceMod = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ToolsInInventory
        private ObservableCollection<ItemModel> _ToolsInInventory;
        public ObservableCollection<ItemModel> ToolsInInventory
        {
            get
            {
                return _ToolsInInventory;
            }
            set
            {
                _ToolsInInventory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Minions
        private ObservableCollection<CreatureModel> _Minions;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CreatureModel> Minions
        {
            get
            {
                return _Minions;
            }
            set
            {
                _Minions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Counters
        private ObservableCollection<CounterModel> _Counters;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CounterModel> Counters
        {
            get
            {
                return _Counters;
            }
            set
            {
                _Counters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Abilities
        private ObservableCollection<CustomAbility> _Abilities;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomAbility> Abilities
        {
            get
            {
                return _Abilities;
            }
            set
            {
                _Abilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Alterants
        private ObservableCollection<CharacterAlterant> _Alterants;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CharacterAlterant> Alterants
        {
            get
            {
                return _Alterants;
            }
            set
            {
                _Alterants = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region CombatNotes
        private string _CombatNotes;
        [XmlSaveMode("Single")]
        public string CombatNotes
        {
            get
            {
                return _CombatNotes;
            }
            set
            {
                _CombatNotes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region CustomDiceSets
        private ObservableCollection<CustomDiceModel> _CustomDiceSets;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomDiceModel> CustomDiceSets
        {
            get
            {
                return _CustomDiceSets;
            }
            set
            {
                _CustomDiceSets = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region RollTables
        private List<RollTableModel> _RollTables;
        public List<RollTableModel> RollTables
        {
            get
            {
                return _RollTables;
            }
            set
            {
                _RollTables = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region DeathSave1
        private bool _DeathSave1;
        public bool DeathSave1
        {
            get
            {
                return _DeathSave1;
            }
            set
            {
                _DeathSave1 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeathSave2
        private bool _DeathSave2;
        public bool DeathSave2
        {
            get
            {
                return _DeathSave2;
            }
            set
            {
                _DeathSave2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeathSave3
        private bool _DeathSave3;
        public bool DeathSave3
        {
            get
            {
                return _DeathSave3;
            }
            set
            {
                _DeathSave3 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeathFail1
        private bool _DeathFail1;
        public bool DeathFail1
        {
            get
            {
                return _DeathFail1;
            }
            set
            {
                _DeathFail1 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeathFail2
        private bool _DeathFail2;
        public bool DeathFail2
        {
            get
            {
                return _DeathFail2;
            }
            set
            {
                _DeathFail2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DeathFail3
        private bool _DeathFail3;
        public bool DeathFail3
        {
            get
            {
                return _DeathFail3;
            }
            set
            {
                _DeathFail3 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region IsBlinded
        private bool _IsBlinded;
        [XmlSaveMode("Single")]
        public bool IsBlinded
        {
            get
            {
                return _IsBlinded;
            }
            set
            {
                _IsBlinded = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsCharmed
        private bool _IsCharmed;
        [XmlSaveMode("Single")]
        public bool IsCharmed
        {
            get
            {
                return _IsCharmed;
            }
            set
            {
                _IsCharmed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsDeafened
        private bool _IsDeafened;
        [XmlSaveMode("Single")]
        public bool IsDeafened
        {
            get
            {
                return _IsDeafened;
            }
            set
            {
                _IsDeafened = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsFrightened
        private bool _IsFrightened;
        [XmlSaveMode("Single")]
        public bool IsFrightened
        {
            get
            {
                return _IsFrightened;
            }
            set
            {
                _IsFrightened = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsGrappled
        private bool _IsGrappled;
        [XmlSaveMode("Single")]
        public bool IsGrappled
        {
            get
            {
                return _IsGrappled;
            }
            set
            {
                _IsGrappled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsIncapacitated
        private bool _IsIncapacitated;
        [XmlSaveMode("Single")]
        public bool IsIncapacitated
        {
            get
            {
                return _IsIncapacitated;
            }
            set
            {
                _IsIncapacitated = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsInvisible
        private bool _IsInvisible;
        [XmlSaveMode("Single")]
        public bool IsInvisible
        {
            get
            {
                return _IsInvisible;
            }
            set
            {
                _IsInvisible = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsParalyzed
        private bool _IsParalyzed;
        [XmlSaveMode("Single")]
        public bool IsParalyzed
        {
            get
            {
                return _IsParalyzed;
            }
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
        [XmlSaveMode("Single")]
        public bool IsPetrified
        {
            get
            {
                return _IsPetrified;
            }
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
        [XmlSaveMode("Single")]
        public bool IsPoisoned
        {
            get
            {
                return _IsPoisoned;
            }
            set
            {
                _IsPoisoned = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsProne
        private bool _IsProne;
        [XmlSaveMode("Single")]
        public bool IsProne
        {
            get
            {
                return _IsProne;
            }
            set
            {
                _IsProne = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsRestrained
        private bool _IsRestrained;
        [XmlSaveMode("Single")]
        public bool IsRestrained
        {
            get
            {
                return _IsRestrained;
            }
            set
            {
                _IsRestrained = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsStunned
        private bool _IsStunned;
        [XmlSaveMode("Single")]
        public bool IsStunned
        {
            get
            {
                return _IsStunned;
            }
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
        [XmlSaveMode("Single")]
        public bool IsUnconscious
        {
            get
            {
                return _IsUnconscious;
            }
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
        [XmlSaveMode("Single")]
        public int ExhaustionLevel
        {
            get
            {
                return _ExhaustionLevel;
            }
            set
            {
                _ExhaustionLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntoxicationLevel
        private int _IntoxicationLevel;
        [XmlSaveMode("Single")]
        public int IntoxicationLevel
        {
            get
            {
                return _IntoxicationLevel;
            }
            set
            {
                _IntoxicationLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Traits
        #region TraitEditModeEnabled
        private bool _TraitEditModeEnabled;
        public bool TraitEditModeEnabled
        {
            get
            {
                return _TraitEditModeEnabled;
            }
            set
            {
                _TraitEditModeEnabled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<TraitModel> Traits
        {
            get
            {
                return _Traits;
            }
            set
            {
                _Traits = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Spellcasting
        #region IsConcentrating
        private bool _IsConcentrating;
        [XmlSaveMode("Single")]
        public bool IsConcentrating
        {
            get
            {
                return _IsConcentrating;
            }
            set
            {
                bool brokeConcentration = (_IsConcentrating == true && value == false);
                _IsConcentrating = value;
                NotifyPropertyChanged();
                if (brokeConcentration)
                {
                    HelperMethods.AddToPlayerLog("Concentration has been broken.");
                }
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
                SetSpellcastingStats();
            }
        }
        #endregion
        #region SpellSaveDc
        private int _SpellSaveDc;
        [XmlSaveMode("Single")]
        public int SpellSaveDc
        {
            get
            {
                return _SpellSaveDc;
            }
            set
            {
                _SpellSaveDc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellAttackBonus
        private int _SpellAttackBonus;
        [XmlSaveMode("Single")]
        public int SpellAttackBonus
        {
            get
            {
                return _SpellAttackBonus;
            }
            set
            {
                _SpellAttackBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellAbilityModifier
        private int _SpellAbilityModifier;
        [XmlSaveMode("Single")]
        public int SpellAbilityModifier
        {
            get
            {
                return _SpellAbilityModifier;
            }
            set
            {
                _SpellAbilityModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region L1SpellsAvailable
        private int _L1SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L1SpellsAvailable
        {
            get
            {
                return _L1SpellsAvailable;
            }
            set
            {
                _L1SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L1SpellsMax
        private int _L1SpellsMax;
        [XmlSaveMode("Single")]
        public int L1SpellsMax
        {
            get
            {
                return _L1SpellsMax;
            }
            set
            {
                _L1SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L2SpellsAvailable
        private int _L2SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L2SpellsAvailable
        {
            get
            {
                return _L2SpellsAvailable;
            }
            set
            {
                _L2SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L2SpellsMax
        private int _L2SpellsMax;
        [XmlSaveMode("Single")]
        public int L2SpellsMax
        {
            get
            {
                return _L2SpellsMax;
            }
            set
            {
                _L2SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L3SpellsAvailable
        private int _L3SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L3SpellsAvailable
        {
            get
            {
                return _L3SpellsAvailable;
            }
            set
            {
                _L3SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L3SpellsMax
        private int _L3SpellsMax;
        [XmlSaveMode("Single")]
        public int L3SpellsMax
        {
            get
            {
                return _L3SpellsMax;
            }
            set
            {
                _L3SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L4SpellsAvailable
        private int _L4SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L4SpellsAvailable
        {
            get
            {
                return _L4SpellsAvailable;
            }
            set
            {
                _L4SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L4SpellsMax
        private int _L4SpellsMax;
        [XmlSaveMode("Single")]
        public int L4SpellsMax
        {
            get
            {
                return _L4SpellsMax;
            }
            set
            {
                _L4SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L5SpellsAvailable
        private int _L5SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L5SpellsAvailable
        {
            get
            {
                return _L5SpellsAvailable;
            }
            set
            {
                _L5SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L5SpellsMax
        private int _L5SpellsMax;
        [XmlSaveMode("Single")]
        public int L5SpellsMax
        {
            get
            {
                return _L5SpellsMax;
            }
            set
            {
                _L5SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L6SpellsAvailable
        private int _L6SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L6SpellsAvailable
        {
            get
            {
                return _L6SpellsAvailable;
            }
            set
            {
                _L6SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L6SpellsMax
        private int _L6SpellsMax;
        [XmlSaveMode("Single")]
        public int L6SpellsMax
        {
            get
            {
                return _L6SpellsMax;
            }
            set
            {
                _L6SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L7SpellsAvailable
        private int _L7SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L7SpellsAvailable
        {
            get
            {
                return _L7SpellsAvailable;
            }
            set
            {
                _L7SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L7SpellsMax
        private int _L7SpellsMax;
        [XmlSaveMode("Single")]
        public int L7SpellsMax
        {
            get
            {
                return _L7SpellsMax;
            }
            set
            {
                _L7SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L8SpellsAvailable
        private int _L8SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L8SpellsAvailable
        {
            get
            {
                return _L8SpellsAvailable;
            }
            set
            {
                _L8SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L8SpellsMax
        private int _L8SpellsMax;
        [XmlSaveMode("Single")]
        public int L8SpellsMax
        {
            get
            {
                return _L8SpellsMax;
            }
            set
            {
                _L8SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L9SpellsAvailable
        private int _L9SpellsAvailable;
        [XmlSaveMode("Single")]
        public int L9SpellsAvailable
        {
            get
            {
                return _L9SpellsAvailable;
            }
            set
            {
                _L9SpellsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region L9SpellsMax
        private int _L9SpellsMax;
        [XmlSaveMode("Single")]
        public int L9SpellsMax
        {
            get
            {
                return _L9SpellsMax;
            }
            set
            {
                _L9SpellsMax = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SpellPreparedCount
        private int _SpellPreparedCount;
        public int SpellPreparedCount
        {
            get
            {
                return _SpellPreparedCount;
            }
            set
            {
                _SpellPreparedCount = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellLinks
        private ObservableCollection<SpellLink> _SpellLinks;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<SpellLink> SpellLinks
        {
            get
            {
                return _SpellLinks;
            }
            set
            {
                _SpellLinks = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ActiveEffectAbilities
        private ObservableCollection<CustomAbility> _ActiveEffectAbilities;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomAbility> ActiveEffectAbilities
        {
            get
            {
                return _ActiveEffectAbilities;
            }
            set
            {
                _ActiveEffectAbilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Equipment
        #region MainHandItem
        private string _MainHandItem;
        [XmlSaveMode("Single")]
        public string MainHandItem
        {
            get
            {
                return _MainHandItem;
            }
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
        [XmlSaveMode("None")]
        public ItemModel MainHandLinkedItem
        {
            get
            {
                return _MainHandLinkedItem;
            }
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
        [XmlSaveMode("Single")]
        public string OffHandItem
        {
            get
            {
                return _OffHandItem;
            }
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
        [XmlSaveMode("None")]
        public ItemModel OffHandLinkedItem
        {
            get
            {
                return _OffHandLinkedItem;
            }
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
        [XmlSaveMode("Single")]
        public string ArmorItem
        {
            get
            {
                return _ArmorItem;
            }
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
        [XmlSaveMode("None")]
        public ItemModel ArmorLinkedItem
        {
            get
            {
                return _ArmorLinkedItem;
            }
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
        [XmlSaveMode("Single")]
        public string AttunedItemA
        {
            get
            {
                return _AttunedItemA;
            }
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
        [XmlSaveMode("Single")]
        public string AttunedItemB
        {
            get
            {
                return _AttunedItemB;
            }
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
        [XmlSaveMode("Single")]
        public string AttunedItemC
        {
            get
            {
                return _AttunedItemC;
            }
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
        [XmlSaveMode("None")]
        public ItemModel AttunedLinkedItemA
        {
            get
            {
                return _AttunedLinkedItemA;
            }
            set
            {
                _AttunedLinkedItemA = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttunedLinkedItemB
        private ItemModel _AttunedLinkedItemB;
        [XmlSaveMode("None")]
        public ItemModel AttunedLinkedItemB
        {
            get
            {
                return _AttunedLinkedItemB;
            }
            set
            {
                _AttunedLinkedItemB = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttunedLinkedItemC
        private ItemModel _AttunedLinkedItemC;
        [XmlSaveMode("None")]
        public ItemModel AttunedLinkedItemC
        {
            get
            {
                return _AttunedLinkedItemC;
            }
            set
            {
                _AttunedLinkedItemC = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region EquippedAccessories
        private ObservableCollection<ItemLink> _EquippedAccessories;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemLink> EquippedAccessories
        {
            get
            {
                return _EquippedAccessories;
            }
            set
            {
                _EquippedAccessories = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Base
        #region CarryingCapacity
        private decimal _CarryingCapacity;
        public decimal CarryingCapacity
        {
            get
            {
                return _CarryingCapacity;
            }
            set
            {
                _CarryingCapacity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region EncumbranceStatus
        private string _EncumbranceStatus;
        public string EncumbranceStatus
        {
            get
            {
                return _EncumbranceStatus;
            }
            set
            {
                _EncumbranceStatus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TotalWeight
        private decimal _TotalWeight;
        public decimal TotalWeight
        {
            get
            {
                return _TotalWeight;
            }
            set
            {
                _TotalWeight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TotalValue
        private string _TotalValue;
        public string TotalValue
        {
            get
            {
                return _TotalValue;
            }
            set
            {
                _TotalValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - 1.23.00
        #region CarriedWeight
        private decimal _CarriedWeight;
        public decimal CarriedWeight
        {
            get
            {
                return _CarriedWeight;
            }
            set
            {
                _CarriedWeight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CarriedCurrency
        private string _CarriedCurrency;
        public string CarriedCurrency
        {
            get
            {
                return _CarriedCurrency;
            }
            set
            {
                _CarriedCurrency = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Inventories
        private ObservableCollection<InventoryModel> _Inventories;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<InventoryModel> Inventories
        {
            get
            {
                return _Inventories;
            }
            set
            {
                _Inventories = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Crafting
        #region CraftingBench
        private ObservableCollection<ItemModel> _CraftingBench;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemModel> CraftingBench
        {
            get
            {
                return _CraftingBench;
            }
            set
            {
                _CraftingBench = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Enchanting
        #region EnchantingTable
        private ObservableCollection<ItemModel> _EnchantingTable;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemModel> EnchantingTable
        {
            get
            {
                return _EnchantingTable;
            }
            set
            {
                _EnchantingTable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Animal Taming
        #region CreaturePen
        private ObservableCollection<CreatureModel> _CreaturePen;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CreatureModel> CreaturePen
        {
            get
            {
                return _CreaturePen;
            }
            set
            {
                _CreaturePen = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Alchemy
        #region HerbalismEnvironment
        private string _HerbalismEnvironment;
        public string HerbalismEnvironment
        {
            get
            {
                return _HerbalismEnvironment;
            }
            set
            {
                _HerbalismEnvironment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HerbalismResultText
        private string _HerbalismResultText;
        public string HerbalismResultText
        {
            get
            {
                return _HerbalismResultText;
            }
            set
            {
                _HerbalismResultText = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Inventory - Fishing
        #region FishingEnvironment
        private string _FishingEnvironment;
        [XmlSaveMode("Single")]
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
        #region FishingBonus
        private int _FishingBonus;
        [XmlSaveMode("Single")]
        public int FishingBonus
        {
            get
            {
                return _FishingBonus;
            }
            set
            {
                _FishingBonus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Notes
        #region Notes
        private ObservableCollection<NoteModel> _Notes;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<NoteModel> Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveNote
        private NoteModel _ActiveNote;
        public NoteModel ActiveNote
        {
            get
            {
                return _ActiveNote;
            }
            set
            {
                _ActiveNote = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Misc
        #region OnInfoTab
        private bool _OnInfoTab;
        public bool OnInfoTab
        {
            get
            {
                return _OnInfoTab;
            }
            set
            {
                _OnInfoTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnSkillsTab
        private bool _OnSkillsTab;
        public bool OnSkillsTab
        {
            get
            {
                return _OnSkillsTab;
            }
            set
            {
                _OnSkillsTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnCombatTab
        private bool _OnCombatTab;
        public bool OnCombatTab
        {
            get
            {
                return _OnCombatTab;
            }
            set
            {
                _OnCombatTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnMinionsTab
        private bool _OnMinionsTab;
        public bool OnMinionsTab
        {
            get
            {
                return _OnMinionsTab;
            }
            set
            {
                _OnMinionsTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnTraitsTab
        private bool _OnTraitsTab;
        public bool OnTraitsTab
        {
            get
            {
                return _OnTraitsTab;
            }
            set
            {
                _OnTraitsTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnSpellcastingTab
        private bool _OnSpellcastingTab;
        public bool OnSpellcastingTab
        {
            get
            {
                return _OnSpellcastingTab;
            }
            set
            {
                _OnSpellcastingTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnInventoryTab
        private bool _OnInventoryTab;
        public bool OnInventoryTab
        {
            get
            {
                return _OnInventoryTab;
            }
            set
            {
                _OnInventoryTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region OnNotesTab
        private bool _OnNotesTab;
        public bool OnNotesTab
        {
            get
            {
                return _OnNotesTab;
            }
            set
            {
                _OnNotesTab = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowActionHistory
        private bool _ShowActionHistory;
        public bool ShowActionHistory
        {
            get
            {
                return _ShowActionHistory;
            }
            set
            {
                _ShowActionHistory = value;
                NotifyPropertyChanged();
            }
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
            if (parts.Count() > 1)
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
                    string dr = "";
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
                    HelperMethods.AddToPlayerLog(msg, "Default", true);
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

            HelperMethods.AddToPlayerLog(output, "Default", true);

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

            HelperMethods.AddToPlayerLog(string.Format("{0} rolled for initiative{3}{4}.\nResult: {1}{2}",
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
            HelperMethods.AddToPlayerLog(Name + " rolls 1d" + diceSides + " and gets " + result + ".", "Default", true);
        }
        #endregion
        #region FlipCoin
        public ICommand FlipCoin => new RelayCommand(param => DoFlipCoin());
        private void DoFlipCoin()
        {
            int result = Configuration.RNG.Next(1, 3);
            HelperMethods.AddToPlayerLog(string.Format("{0} flips a coin and gets {1}.", Name, (result == 1) ? "heads" : "tails"), "Default", true);
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
            HelperMethods.AddToPlayerLog(Name + " took a short rest.");

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
            HelperMethods.AddToPlayerLog(Name + " took a long rest and recovered health and spell slots.");
        }
        #endregion
        #region RollDeathSave
        public ICommand RollDeathSave => new RelayCommand(param => DoRollDeathSave());
        private void DoRollDeathSave()
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int roll = Configuration.RNG.Next(1, 21);
            string message = "";

            if (roll == 1) { _DeathFails += 2; message += Name + " makes a death save (" + roll + ") and gets two failures."; }
            if (roll >= 2 && roll <= 9) { _DeathFails++; message += Name + " makes a death save (" + roll + ") and gets one failure."; }
            if (roll >= 10 && roll <= 19) { _DeathSaves++; message += Name + " makes a death save (" + roll + ") and gets one success."; }
            if (roll == 20) { _DeathSaves = 0; _DeathFails = 0; CurrentHealth = 1; message += Name + " has recovered."; }

            if (_DeathSaves >= 1) { DeathSave1 = true; }
            if (_DeathSaves >= 2) { DeathSave2 = true; }
            if (_DeathSaves >= 3) { DeathSave3 = true; message += "\n" + Name + " has stabilized."; }

            if (_DeathFails >= 1) { DeathFail1 = true; }
            if (_DeathFails >= 2) { DeathFail2 = true; }
            if (_DeathFails >= 3) { DeathFail3 = true; message += "\n" + Name + " has died."; }

            HelperMethods.AddToPlayerLog(message, "Default", true);

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
            if (Minions.Count() == 0) { return; }
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

                string missingItems = "";

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
                    new NotificationDialog("Missing Imbuing Lens.").ShowDialog();
                    return;
                }
                if (hasBaseItem == false)
                {
                    new NotificationDialog("Missing base item: " + itemToAdd.EnchantingBaseItem).ShowDialog();
                    return;
                }

                string missingRunes = "";
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
                    new NotificationDialog(missingRunes.Insert(0, "Insufficient enchanting runes:")).ShowDialog();
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
            HerbalismResultText = "";
            int ingredientsRoll = Configuration.RNG.Next(1, 5);
            int profBonus = (ToolProficiencies.FirstOrDefault(tool => tool.Name == "Herbalism Kit") != null) ? ProficiencyBonus : 0;
            int numberOfIngredientsFound = ingredientsRoll + profBonus;
            List<ItemModel> environmentIngredients = Configuration.MainModelRef.ItemBuilderView.AllItems.Where(item => item.Type == "Ingredient" && item.Environment == HerbalismEnvironment && item.IsValidated).ToList();
            List<ItemModel> ingredientsFound = new();
            for (int i = 0; i < numberOfIngredientsFound; i++)
            {
                ItemModel newIngredient = HelperMethods.DeepClone(environmentIngredients[Configuration.RNG.Next(1, environmentIngredients.Count())]);
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

                string message = "";
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

                new NotificationDialog(message).ShowDialog();

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
            string message = "Are you sure you want to auto-sort your notes?" +
                "\nSort Order" +
                "\n1. Category: Location" +
                "\n2. Category: Faction" +
                "\n3. Category: Vendor" +
                "\n4. Category: Character" +
                "\n5. Category: Quest" +
                "\n6. Category: Miscellaneous" +
                "\n7. Header" +
                "\nQuest sub notes are not sorted.";
            MessageBoxResult result = MessageBox.Show(message, "Auto-Sort", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) { return; }

            Notes = new ObservableCollection<NoteModel>(SortNoteList(Notes.ToList()));

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
            string msg = "";
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
                HelperMethods.AddToPlayerLog(msg, "Default", true);
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
            if (HitDiceSets.Count() > 0)
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
                new NotificationDialog("Please select a valid fishing environment.").ShowDialog();
                return;
            }
            if (param == null) { param = ""; }
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
            if (roll == 1) { HelperMethods.AddToPlayerLog(Name + " found nothing while fishing, tough luck!", "Default", true); return; }
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
            if (fish.Count() == 0) { fish = Configuration.ItemRepository.Where(item => item.Type == "Fish").ToList(); }
            if (fish.Count() == 0) { new NotificationDialog("Tell your DM to put fish into the data repository.").ShowDialog(); return; }
            int fishRoll = 0;
            for (int i = 0; i < attempts; i++) 
            {
                int attempt = Configuration.RNG.Next(0, fish.Count());
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
                    HelperMethods.AddToPlayerLog(message, "Default", true);
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
            HelperMethods.AddToPlayerLog(message, "Default", true);

        }
        #endregion
        #region ProcessGroupSave
        public ICommand ProcessGroupSave => new RelayCommand(param => DoProcessGroupSave());
        private void DoProcessGroupSave()
        {
            if (Minions.Count() <= 0) { new NotificationDialog("You have no minions.").ShowDialog(); return; }
            EncounterMultiTargetDialog targetDialog = new(Minions.ToList());
            if (targetDialog.ShowDialog() == true)
            {
                if (targetDialog.SelectedCreatures.Count() <= 0) { return; }
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
                        default:
                            break;
                    }
                }
                HelperMethods.AddToPlayerLog(message, "Save", true);
            }
        }
        #endregion
        #region AddInventory
        public ICommand AddInventory => new RelayCommand(param => DoAddInventory());
        private void DoAddInventory()
        {
            if (Inventories.Count() >= 6) { new NotificationDialog("Inventory tab limit is 6.").ShowDialog(); return; }
            Inventories.Add(new InventoryModel(this));
        }
        #endregion
        #region ToggleRoll20Link
        public ICommand ToggleRoll20Link => new RelayCommand(DoToggleRoll20Link);
        private void DoToggleRoll20Link(object param)
        {
            bool desiredState = !OutputLinkedToRoll20;
            bool canLink = false;
            if (Configuration.MainModelRef.WebDriver == null && desiredState == true)
            {
                HelperMethods.NotifyUser("The WebDriver is not started or not available.\nGo to Settings > WebDriver > Open Roll20 to start it.");
                return;
            }
            else
            {
                canLink = HelperMethods.SwitchRoll20ChatAsCurrent();
                if (canLink && desiredState == true) { HelperMethods.AddToPlayerLog(Name + " has connected to Roll20", "Default", true); }
                OutputLinkedToRoll20 = true;
            }
            if (desiredState == false)
            {
                HelperMethods.AddToPlayerLog(Name + " has disconnected from Roll20.");
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
                    MainHandItem = "";
                    break;
                case "OffHand":
                    OffHandItem = "";
                    break;
                case "Armor":
                    ArmorItem = "";
                    break;
                case "AttunedSlotA":
                    AttunedItemA = "";
                    break;
                case "AttunedSlotB":
                    AttunedItemB = "";
                    break;
                case "AttunedSlotC":
                    AttunedItemC = "";
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
            if (param.ToString().Contains(",") == false)
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
                BaseAttributePoints = 27 - pointsUsed;
                StrengthBaseScore = nextStr;
                DexterityBaseScore = nextDex;
                ConstitutionBaseScore = nextCon;
                IntelligenceBaseScore = nextInt;
                WisdomBaseScore = nextWis;
                CharismaBaseScore = nextCha;
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

            LinkedRace = LinkedRace;
            LinkedSubrace = LinkedSubrace;

        }
        #endregion

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
            ClassAutoText = "";
            if (PlayerClasses.Count() <= 0) { return; }
            if (PlayerClasses.Count() == 1)
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
        public bool ValidateCharacterCreation()
        {
            List<string> warnings = new();
            List<string> errors = new();

            if (Name == "") { errors.Add("Name is blank."); }
            if (TotalLevel == 0) { errors.Add("No character levels selected."); }
            if (TotalLevel >= 20) { errors.Add("Total level exceeds 20."); }

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
                            string stat = "";
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
            if (Subraces.Count() > 0 && LinkedSubrace == null) { errors.Add("No subrace selected."); }
            if (LinkedBackground == null) { errors.Add("No background selected."); }

            foreach (ChoiceSet choiceSet in SkillChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

            foreach (ChoiceSet choiceSet in ExpertiseChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

            foreach (ChoiceSet choiceSet in TraitChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

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

            foreach (ChoiceSet choiceSet in SpellChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

            foreach (ChoiceSet choiceSet in ToolChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

            foreach (ChoiceSet choiceSet in ArmorChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

            foreach (ChoiceSet choiceSet in WeaponChoiceSegments)
            {
                if (choiceSet.ChoiceRestricted && choiceSet.ChoicesRemaining > 0)
                {
                    errors.Add(choiceSet.Source + " has " + choiceSet.ChoicesRemaining + " choices remaining.");
                }
            }

            string output = "ERRORS:";
            foreach (string error in errors)
            {
                output += "\n" + error;
            }
            output += "\n\nWARNINGS:";
            foreach (string warning in warnings)
            {
                output += "\n" + warning;
            }

            if (errors.Count() > 0)
            {
                new NotificationDialog(output, "Report").ShowDialog();
                return false;
            }
            if (warnings.Count() > 0)
            {
                new NotificationDialog(output, "Report").ShowDialog();
                YesNoDialog question = new("Potential issues found, complete character creation anyway?");
                question.ShowDialog();
                if (question.Answer == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;

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
            if (Speed == 0) { Speed = LinkedRace.BaseSpeed; }
            if (ArmorClass == 0) { ArmorClass = 10 + DexterityModifier; }
            if (Darkvision == 0) { Darkvision = LinkedRace.Darkvision; }

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
            PlayerClassModel baseClass = Configuration.MainModelRef.ToolsView.PlayerClasses.First(c => c.Name == PlayerClasses.First().ClassName);
            foreach (FeatureModel feature in baseClass.Features)
            {
                if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
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
            foreach (FeatureModel feature in LinkedRace.Features)
            {
                if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
            }

            if (LinkedSubrace != null)
            {
                foreach (FeatureModel feature in LinkedSubrace.Features)
                {
                    if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                    if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
                }
            }

            foreach (FeatureModel feature in LinkedBackground.Features)
            {
                if (feature.FeatureType == "Saving Throws - Set") { CCI_SavingThrows_Set(feature.Choices.ToList()); }
                if (feature.FeatureType == "Skill Proficiencies - Set") { CCI_SkillProficiencies_Set(feature.Choices.ToList()); }
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
            if (HasCompletedCharacterCreation == false)
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
                Inventories[0].SearchText = "";

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

            tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", LinkedRace.Features.ToList(), tools);
            if (LinkedSubrace != null) { tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", LinkedRace.Features.ToList(), tools); }
            tools = AddOntoStringListFromFeatures("Tool Proficiencies - Set", LinkedBackground.Features.ToList(), tools);

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

            otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", LinkedRace.Features.ToList(), otherProfs);
            otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", LinkedRace.Features.ToList(), otherProfs);

            if (LinkedSubrace != null)
            {
                otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", LinkedSubrace.Features.ToList(), otherProfs);
                otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", LinkedSubrace.Features.ToList(), otherProfs);
            }

            otherProfs = AddOntoStringListFromFeatures("Armor Proficiencies - Set", LinkedBackground.Features.ToList(), otherProfs);
            otherProfs = AddOntoStringListFromFeatures("Weapon Proficiencies - Set", LinkedBackground.Features.ToList(), otherProfs);

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
            string output = "";
            for (int i = 0; i < otherProfs.Count(); i++)
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

            languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", LinkedRace.Features.ToList(), languages);
            if (LinkedSubrace != null)
            {
                languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", LinkedSubrace.Features.ToList(), languages);
            }
            languages = AddOntoStringListFromFeatures("Language Proficiencies - Set", LinkedBackground.Features.ToList(), languages);

            foreach (ChoiceSet choiceSet in LanguageChoiceSegments)
            {
                foreach (BoolOption choice in choiceSet.Choices)
                {
                    if (choice.Marked && languages.Contains(choice.Name) == false) { languages.Add(choice.Name); }
                }
            }

            languages.Sort();
            string output = "";
            for (int i = 0; i < languages.Count(); i++)
            {
                if (i > 0) { output += ", "; }
                output += languages[i];
            }
            output = output.Replace("[", "").Replace("]", "");

            Languages = output;

        }
        private List<string> AddOntoStringListFromFeatures(string featureType, List<FeatureModel> features, List<string> endList)
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
                    ac += (ArmorLinkedItem.DexterityAcLimit <= DexterityModifier) ? ArmorLinkedItem.DexterityAcLimit : DexterityModifier;
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
            List<NoteModel> sortedNotes = notes.OrderBy(note =>
            note.Category == "Location" ? 1 :
            note.Category == "District" ? 2 :
            note.Category == "Faction" ? 3 :
            note.Category == "Character" ? 4 :
            note.Category == "Quest" ? 5 :
            note.Category == "Map" ? 6 :
            note.Category == "Vendor" ? 7 : 8).ThenBy(note => note.Header).ToList();
            foreach (NoteModel note in sortedNotes)
            {
                if (note.Category == "Quest") { continue; }
                note.SubNotes = new ObservableCollection<NoteModel>(SortNoteList(note.SubNotes.ToList()));
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
            foreach (ChoiceSet choiceSet in toolChoices)
            {
                int choicesLeft = choiceSet.MaxChoices;
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked) { choicesLeft--; }
                }
                choiceSet.ChoicesRemaining = choicesLeft;
            }

            SetToolProfs = new(setTools);
            ToolChoiceSegments = new(toolChoices);

            ObservableCollection<ChoiceSet> toolChoiceSegments = ToolChoiceSegments;
            if (UnmarkChoicesFromSets(ref toolChoiceSegments, SetToolProfs)) { return; }

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
            foreach (ChoiceSet choiceSet in spellChoices)
            {
                int choicesLeft = choiceSet.MaxChoices;
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked) { choicesLeft--; }
                }
                choiceSet.ChoicesRemaining = choicesLeft;
            }

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
            foreach (ChoiceSet choiceSet in traitChoices)
            {
                int choicesLeft = choiceSet.MaxChoices;
                foreach (BoolOption option in choiceSet.Choices)
                {
                    if (option.Marked) { choicesLeft--; }
                }
                choiceSet.ChoicesRemaining = choicesLeft;
            }

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
                if (pc.EquipmentChoices.Count() > 0)
                {
                    startingEquipmentSets.Add(new() { Label = pc.Name + " Starting Equipment", Values = pc.EquipmentChoices });
                }
                break; // Only primary class grants starting equipment
            }
            if (LinkedBackground != null)
            {
                StartingGold = LinkedBackground.GoldPieces;
                if (LinkedBackground.EquipmentChoices.Count() > 0)
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
        private List<ChoiceSet> GetExistingChoiceMarkings(List<ChoiceSet> choiceSetsNew, List<ChoiceSet> choiceSetsOld)
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
        private bool UnmarkChoicesFromSets(ref ObservableCollection<ChoiceSet> sourceChoices, List<string> sourceSets)
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
        private List<string> GetLanguageSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setLangProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string languages = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count(); i++)
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
        private List<string> GetToolSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setToolProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string tools = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count(); i++)
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
        private List<string> GetWeaponSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setWeaponProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string weapons = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count(); i++)
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
        private List<string> GetArmorSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setArmorProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string armors = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count(); i++)
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
        private List<string> GetSkillSetProfsFromFeatureList(List<FeatureModel> features, string featureType, string setNamePrefix)
        {
            List<string> setSkillProfs = new();

            foreach (FeatureModel feature in features)
            {
                if (feature.FeatureType == featureType)
                {
                    string skills = setNamePrefix + " " + feature.Name + ": ";
                    for (int i = 0; i < feature.Choices.Count(); i++)
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
        private List<TraitModel> GetTraitSetProfsFromFeatureList(List<FeatureModel> features)
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
        private int GetQuantityFromFeatureData(List<FeatureData> features, string featureDataName)
        {
            int total = 0;
            foreach (FeatureData featureData in features)
            {
                if (featureData.Name == featureDataName) { total += featureData.Quantity; }
            }
            return total;
        }
        private Dictionary<string, int> GetChoiceMarkedCounts(ref ObservableCollection<ChoiceSet> sourceChoices)
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
