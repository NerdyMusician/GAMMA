using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class CreatureModel : EntityModel
    {
        public CreatureModel()
        {
            Name = "New Creature";
            Abilities = new();
            Counters = new();
            SpellLinks = new();
            Traits = new();
            ItemLinks = new();
            ActiveEffectAbilities = new();
            Environments = new();

            DamageProclivity_Acid = "Normal";
            DamageProclivity_Cold = "Normal";
            DamageProclivity_Fire = "Normal";
            DamageProclivity_Force = "Normal";
            DamageProclivity_Lightning = "Normal";
            DamageProclivity_Necrotic = "Normal";
            DamageProclivity_Poison = "Normal";
            DamageProclivity_Psychic = "Normal";
            DamageProclivity_Radiant = "Normal";
            DamageProclivity_Thunder = "Normal";
            DamageProclivity_Bludgeoning = "Normal";
            DamageProclivity_Slashing = "Normal";
            DamageProclivity_Piercing = "Normal";

        }

        private void ActiveEffects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ShowTag_HasActiveEffects = (ActiveEffectAbilities.Count() > 0);
        }

        private void GenerateTraitAndAbilityLists()
        {
            List<BoolOption> traitsAndAbilities = new();
            int totalCharacterCount = 0;
            foreach (TraitModel trait in Traits)
            {
                traitsAndAbilities.Add(new() { Name = trait.Name, Description = trait.Description });
                totalCharacterCount += trait.Name.Length + trait.Description.Length;
            }
            foreach (CustomAbility ability in Abilities)
            {
                traitsAndAbilities.Add(new() { Name = ability.Name, Description = ability.Description });
                totalCharacterCount += ability.Name.Length + ability.Description.Length;
            }

            int currentLeftSideLength = FT_Senses.Length + Languages.Length + Vulnerabilities.Length + Resistances.Length + Immunities.Length + ConditionImmunities.Length;
            bool flipToRightSide = false;

            FT_TraitsAndAbilities_P1 = new();
            FT_TraitsAndAbilities_P2 = new();
            foreach (BoolOption traitOrAbility in traitsAndAbilities)
            {
                if (currentLeftSideLength > (totalCharacterCount / 2)) { flipToRightSide = true; }
                if (flipToRightSide == false)
                {
                    FT_TraitsAndAbilities_P1.Add(traitOrAbility);
                    currentLeftSideLength += traitOrAbility.Name.Length + traitOrAbility.Description.Length;
                }
                else
                {
                    FT_TraitsAndAbilities_P2.Add(traitOrAbility);
                }
            }

        }

        // Databound Properties - Core
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
        #region PortraitImagePath
        private string _PortraitImagePath;
        public string PortraitImagePath
        {
            get
            {
                return _PortraitImagePath;
            }
            set
            {
                _PortraitImagePath = value;
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
        #region TrackerIndicator
        private string _TrackerIndicator;
        [XmlSaveMode("Single")]
        public string TrackerIndicator
        {
            get
            {
                return _TrackerIndicator;
            }
            set
            {
                _TrackerIndicator = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayName
        private string _DisplayName;
        [XmlSaveMode("Single")]
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsPlayer
        private bool _IsPlayer;
        [XmlSaveMode("Single")]
        public bool IsPlayer
        {
            get
            {
                return _IsPlayer;
            }
            set
            {
                _IsPlayer = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsAlly
        private bool _IsAlly;
        [XmlSaveMode("Single")]
        public bool IsAlly
        {
            get
            {
                return _IsAlly;
            }
            set
            {
                _IsAlly = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsNpc
        private bool _IsNpc;
        [XmlSaveMode("Single")]
        public bool IsNpc
        {
            get
            {
                return _IsNpc;
            }
            set
            {
                _IsNpc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsOoc
        private bool _IsOoc;
        [XmlSaveMode("Single")]
        public bool IsOoc
        {
            get
            {
                return _IsOoc;
            }
            set
            {
                _IsOoc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasMiniature
        private bool _HasMiniature;
        [XmlSaveMode("Single")]
        public bool HasMiniature
        {
            get
            {
                return _HasMiniature;
            }
            set
            {
                _HasMiniature = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MiniatureQuantity
        private int _MiniatureQuantity;
        [XmlSaveMode("Single")]
        public int MiniatureQuantity
        {
            get
            {
                return _MiniatureQuantity;
            }
            set
            {
                _MiniatureQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MiniatureLocation
        private string _MiniatureLocation;
        [XmlSaveMode("Single")]
        public string MiniatureLocation
        {
            get
            {
                return _MiniatureLocation;
            }
            set
            {
                _MiniatureLocation = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Campaign View Only //
        #region PlayerName
        private string _PlayerName;
        [XmlSaveMode("Single")]
        public string PlayerName
        {
            get
            {
                return _PlayerName;
            }
            set
            {
                _PlayerName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PlayerRaceAndClass
        private string _PlayerRaceAndClass;
        [XmlSaveMode("Single")]
        public string PlayerRaceAndClass
        {
            get
            {
                return _PlayerRaceAndClass;
            }
            set
            {
                _PlayerRaceAndClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PlayerPassivePerception
        private int _PlayerPassivePerception;
        [XmlSaveMode("Single")]
        public int PlayerPassivePerception
        {
            get
            {
                return _PlayerPassivePerception;
            }
            set
            {
                _PlayerPassivePerception = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PlayerSpellSaveDc
        private int _PlayerSpellSaveDc;
        [XmlSaveMode("Single")]
        public int PlayerSpellSaveDc
        {
            get
            {
                return _PlayerSpellSaveDc;
            }
            set
            {
                _PlayerSpellSaveDc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region CreatureCategory
        private string _CreatureCategory;
        [XmlSaveMode("Single")]
        public string CreatureCategory
        {
            get
            {
                return _CreatureCategory;
            }
            set
            {
                _CreatureCategory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CreatureSubCategory
        private string _CreatureSubCategory;
        [XmlSaveMode("Single")]
        public string CreatureSubCategory
        {
            get
            {
                return _CreatureSubCategory;
            }
            set
            {
                _CreatureSubCategory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Description
        // Description or lore for the Creature.
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
        #region Notes
        // Current GM notes for the specific Creature
        private string _Notes;
        [XmlSaveMode("Single")]
        public string Notes
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
        #region CurrentHitPoints
        private int _CurrentHitPoints;
        [XmlSaveMode("Single")]
        public int CurrentHitPoints
        {
            get
            {
                return _CurrentHitPoints;
            }
            set
            {
                _CurrentHitPoints = value;
                NotifyPropertyChanged();
                UpdateStatus();
            }
        }
        #endregion
        #region MaxHitPoints
        private int _MaxHitPoints;
        [XmlSaveMode("Single")]
        public int MaxHitPoints
        {
            get
            {
                return _MaxHitPoints;
            }
            set
            {
                _MaxHitPoints = value;
                NotifyPropertyChanged();
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
        #region Size
        // Size category of the Creature. (e.g. Medium, Large)
        private string _Size;
        [XmlSaveMode("Single")]
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Alignment
        // Moral alignment of the Creature. (e.g. Chaotic Neutral, Lawful Good)
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
        #region Environments
        private ObservableCollection<ConvertibleValue> _Environments;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ConvertibleValue> Environments
        {
            get
            {
                return _Environments;
            }
            set
            {
                _Environments = value;
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
        #region ItemLinks
        private ObservableCollection<ItemLink> _ItemLinks;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemLink> ItemLinks
        {
            get
            {
                return _ItemLinks;
            }
            set
            {
                _ItemLinks = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode("Single")]
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
                NotifyPropertyChanged();

                if (Configuration.MainModelRef.TabSelected_Campaigns)
                {
                    GameCampaign campaign = Configuration.MainModelRef.CampaignView.ActiveCampaign;
                    if (campaign == null) { return; }

                    if (value)
                    {
                        foreach (CreatureModel creature in campaign.Combatants)
                        {
                            if (creature != this)
                            {
                                creature.IsActive = false;
                            }
                        }
                    }
                    
                    campaign.UpdateActiveCombatant();

                }

            }
        }
        #endregion
        #region TrackerSearchMatch
        private bool _TrackerSearchMatch;
        public bool TrackerSearchMatch
        {
            get
            {
                return _TrackerSearchMatch;
            }
            set
            {
                _TrackerSearchMatch = value;
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
        #region CoinDrop
        private int _CoinDrop;
        [XmlSaveMode("Single")]
        public int CoinDrop
        {
            get
            {
                return _CoinDrop;
            }
            set
            {
                _CoinDrop = value;
                NotifyPropertyChanged();
                ProcessedValue = HelperMethods.GetDerivedCoinage(value / 2) + " - " + HelperMethods.GetDerivedCoinage(value);
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
        #region HasBeenLooted
        private bool _HasBeenLooted;
        [XmlSaveMode("Single")]
        public bool HasBeenLooted
        {
            get
            {
                return _HasBeenLooted;
            }
            set
            {
                _HasBeenLooted = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
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
                _IsConcentrating = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsTarget
        private bool _IsTarget;
        public bool IsTarget
        {
            get
            {
                return _IsTarget;
            }
            set
            {
                _IsTarget = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasGroupSaveAdvantage
        private bool _HasGroupSaveAdvantage;
        public bool HasGroupSaveAdvantage
        {
            get
            {
                return _HasGroupSaveAdvantage;
            }
            set
            {
                _HasGroupSaveAdvantage = value;
                NotifyPropertyChanged();

                if (value == true && HasGroupSaveDisadvantage == true)
                {
                    HasGroupSaveDisadvantage = false;
                }
            }
        }
        #endregion
        #region HasGroupSaveDisadvantage
        private bool _HasGroupSaveDisadvantage;
        public bool HasGroupSaveDisadvantage
        {
            get
            {
                return _HasGroupSaveDisadvantage;
            }
            set
            {
                _HasGroupSaveDisadvantage = value;
                NotifyPropertyChanged();

                if (value == true && HasGroupSaveAdvantage == true)
                {
                    HasGroupSaveAdvantage = false;
                }
            }
        }
        #endregion
        #region QuantityToAdd
        private int _QuantityToAdd;
        public int QuantityToAdd
        {
            get
            {
                return _QuantityToAdd;
            }
            set
            {
                _QuantityToAdd = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TamingProgress
        private int _TamingProgress;
        [XmlSaveMode("Single")]
        public int TamingProgress
        {
            get
            {
                return _TamingProgress;
            }
            set
            {
                _TamingProgress = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Other
        #region HitDiceQuantity
        private int _HitDiceQuantity;
        [XmlSaveMode("Single")]
        public int HitDiceQuantity
        {
            get
            {
                return _HitDiceQuantity;
            }
            set
            {
                _HitDiceQuantity = value;
                NotifyPropertyChanged();
                UpdateAverageHitPoints();
            }
        }
        #endregion
        #region HitDiceQuality
        private int _HitDiceQuality;
        [XmlSaveMode("Single")]
        public int HitDiceQuality
        {
            get
            {
                return _HitDiceQuality;
            }
            set
            {
                _HitDiceQuality = value;
                NotifyPropertyChanged();
                UpdateAverageHitPoints();
            }
        }
        #endregion
        #region HitDiceModifier
        private int _HitDiceModifier;
        [XmlSaveMode("Single")]
        public int HitDiceModifier
        {
            get
            {
                return _HitDiceModifier;
            }
            set
            {
                _HitDiceModifier = value;
                NotifyPropertyChanged();
                UpdateAverageHitPoints();
            }
        }
        #endregion
        #region AverageHitPoints
        private int _AverageHitPoints;
        public int AverageHitPoints
        {
            get
            {
                return _AverageHitPoints;
            }
            set
            {
                _AverageHitPoints = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ChallengeRating
        private string _ChallengeRating;
        [XmlSaveMode("Single")]
        public string ChallengeRating
        {
            get
            {
                return _ChallengeRating;
            }
            set
            {
                _ChallengeRating = value;
                NotifyPropertyChanged();
                UpdateExperienceValue(value);
                ProficiencyBonus = HelperMethods.GetProfBonusFromCr(value);
            }
        }
        #endregion
        #region ExperienceValue
        private int _ExperienceValue;
        public int ExperienceValue
        {
            get
            {
                return _ExperienceValue;
            }
            set
            {
                _ExperienceValue = value;
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
            }
        }
        #endregion
        #region ArmorType
        private string _ArmorType;
        [XmlSaveMode("Single")]
        public string ArmorType
        {
            get
            {
                return _ArmorType;
            }
            set
            {
                _ArmorType = value;
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
        #region FlySpeed
        private int _FlySpeed;
        [XmlSaveMode("Single")]
        public int FlySpeed
        {
            get
            {
                return _FlySpeed;
            }
            set
            {
                _FlySpeed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ClimbSpeed
        private int _ClimbSpeed;
        [XmlSaveMode("Single")]
        public int ClimbSpeed
        {
            get
            {
                return _ClimbSpeed;
            }
            set
            {
                _ClimbSpeed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region BurrowSpeed
        private int _BurrowSpeed;
        [XmlSaveMode("Single")]
        public int BurrowSpeed
        {
            get
            {
                return _BurrowSpeed;
            }
            set
            {
                _BurrowSpeed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SwimSpeed
        private int _SwimSpeed;
        [XmlSaveMode("Single")]
        public int SwimSpeed
        {
            get
            {
                return _SwimSpeed;
            }
            set
            {
                _SwimSpeed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HighestSpeedType
        private string _HighestSpeedType;
        public string HighestSpeedType
        {
            get
            {
                return _HighestSpeedType;
            }
            set
            {
                _HighestSpeedType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HighestSpeedValue
        private int _HighestSpeedValue;
        public int HighestSpeedValue
        {
            get
            {
                return _HighestSpeedValue;
            }
            set
            {
                _HighestSpeedValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

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

                UpdateModifiers();
            }
        }
        #endregion
        #region PassiveInsight
        private int _PassiveInsight;
        [XmlSaveMode("Single")]
        public int PassiveInsight
        {
            get
            {
                return _PassiveInsight;
            }
            set
            {
                _PassiveInsight = value;
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
        #region Blindsight
        private int _Blindsight;
        [XmlSaveMode("Single")]
        public int Blindsight
        {
            get
            {
                return _Blindsight;
            }
            set
            {
                _Blindsight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Truesight
        private int _Truesight;
        [XmlSaveMode("Single")]
        public int Truesight
        {
            get
            {
                return _Truesight;
            }
            set
            {
                _Truesight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Tremorsense
        private int _Tremorsense;
        [XmlSaveMode("Single")]
        public int Tremorsense
        {
            get
            {
                return _Tremorsense;
            }
            set
            {
                _Tremorsense = value;
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
        #region Lore
        private string _Lore;
        [XmlSaveMode("Single")]
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
        #region SpellcastingAbilities
        private List<string> _SpellcastingAbilities;
        public List<string> SpellcastingAbilities
        {
            get
            {
                return _SpellcastingAbilities;
            }
            set
            {
                _SpellcastingAbilities = value;
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
                UpdateModifiers();
            }
        }
        #endregion
        #region SpellSaveDc
        private int _SpellSaveDc;
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
        #region IsInnateSpellcaster
        private bool _IsInnateSpellcaster;
        [XmlSaveMode("Single")]
        public bool IsInnateSpellcaster
        {
            get
            {
                return _IsInnateSpellcaster;
            }
            set
            {
                _IsInnateSpellcaster = value;
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
        #region Traits_P1
        private ObservableCollection<TraitModel> _Traits_P1;
        public ObservableCollection<TraitModel> Traits_P1
        {
            get
            {
                return _Traits_P1;
            }
            set
            {
                _Traits_P1 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Traits_P2
        private ObservableCollection<TraitModel> _Traits_P2;
        public ObservableCollection<TraitModel> Traits_P2
        {
            get
            {
                return _Traits_P2;
            }
            set
            {
                _Traits_P2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
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

        // Databound Properties - Damage Type Proclivities
        #region Vulnerabilities
        private string _Vulnerabilities;
        public string Vulnerabilities
        {
            get
            {
                return _Vulnerabilities;
            }
            set
            {
                _Vulnerabilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Resistances
        private string _Resistances;
        public string Resistances
        {
            get
            {
                return _Resistances;
            }
            set
            {
                _Resistances = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Immunities
        private string _Immunities;
        public string Immunities
        {
            get
            {
                return _Immunities;
            }
            set
            {
                _Immunities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConditionImmunities
        private string _ConditionImmunities;
        public string ConditionImmunities
        {
            get
            {
                return _ConditionImmunities;
            }
            set
            {
                _ConditionImmunities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region DamageProclivity_Acid
        private string _DamageProclivity_Acid;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Acid
        {
            get
            {
                return _DamageProclivity_Acid;
            }
            set
            {
                _DamageProclivity_Acid = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Cold
        private string _DamageProclivity_Cold;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Cold
        {
            get
            {
                return _DamageProclivity_Cold;
            }
            set
            {
                _DamageProclivity_Cold = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Fire
        private string _DamageProclivity_Fire;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Fire
        {
            get
            {
                return _DamageProclivity_Fire;
            }
            set
            {
                _DamageProclivity_Fire = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Force
        private string _DamageProclivity_Force;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Force
        {
            get
            {
                return _DamageProclivity_Force;
            }
            set
            {
                _DamageProclivity_Force = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Lightning
        private string _DamageProclivity_Lightning;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Lightning
        {
            get
            {
                return _DamageProclivity_Lightning;
            }
            set
            {
                _DamageProclivity_Lightning = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Necrotic
        private string _DamageProclivity_Necrotic;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Necrotic
        {
            get
            {
                return _DamageProclivity_Necrotic;
            }
            set
            {
                _DamageProclivity_Necrotic = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Poison
        private string _DamageProclivity_Poison;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Poison
        {
            get
            {
                return _DamageProclivity_Poison;
            }
            set
            {
                _DamageProclivity_Poison = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Psychic
        private string _DamageProclivity_Psychic;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Psychic
        {
            get
            {
                return _DamageProclivity_Psychic;
            }
            set
            {
                _DamageProclivity_Psychic = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Radiant
        private string _DamageProclivity_Radiant;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Radiant
        {
            get
            {
                return _DamageProclivity_Radiant;
            }
            set
            {
                _DamageProclivity_Radiant = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Thunder
        private string _DamageProclivity_Thunder;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Thunder
        {
            get
            {
                return _DamageProclivity_Thunder;
            }
            set
            {
                _DamageProclivity_Thunder = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Slashing
        private string _DamageProclivity_Slashing;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Slashing
        {
            get
            {
                return _DamageProclivity_Slashing;
            }
            set
            {
                _DamageProclivity_Slashing = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Piercing
        private string _DamageProclivity_Piercing;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Piercing
        {
            get
            {
                return _DamageProclivity_Piercing;
            }
            set
            {
                _DamageProclivity_Piercing = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion
        #region DamageProclivity_Bludgeoning
        private string _DamageProclivity_Bludgeoning;
        [XmlSaveMode("Single")]
        public string DamageProclivity_Bludgeoning
        {
            get
            {
                return _DamageProclivity_Bludgeoning;
            }
            set
            {
                _DamageProclivity_Bludgeoning = value;
                NotifyPropertyChanged();
                UpdateDamageProclivityTexts();
            }
        }
        #endregion

        #region IsImmune_Blinded
        private bool _IsImmune_Blinded;
        [XmlSaveMode("Single")]
        public bool IsImmune_Blinded
        {
            get
            {
                return _IsImmune_Blinded;
            }
            set
            {
                _IsImmune_Blinded = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Charmed
        private bool _IsImmune_Charmed;
        [XmlSaveMode("Single")]
        public bool IsImmune_Charmed
        {
            get
            {
                return _IsImmune_Charmed;
            }
            set
            {
                _IsImmune_Charmed = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Deafened
        private bool _IsImmune_Deafened;
        [XmlSaveMode("Single")]
        public bool IsImmune_Deafened
        {
            get
            {
                return _IsImmune_Deafened;
            }
            set
            {
                _IsImmune_Deafened = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Exhaustion
        private bool _IsImmune_Exhaustion;
        [XmlSaveMode("Single")]
        public bool IsImmune_Exhaustion
        {
            get
            {
                return _IsImmune_Exhaustion;
            }
            set
            {
                _IsImmune_Exhaustion = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Frightened
        private bool _IsImmune_Frightened;
        [XmlSaveMode("Single")]
        public bool IsImmune_Frightened
        {
            get
            {
                return _IsImmune_Frightened;
            }
            set
            {
                _IsImmune_Frightened = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Grappled
        private bool _IsImmune_Grappled;
        [XmlSaveMode("Single")]
        public bool IsImmune_Grappled
        {
            get
            {
                return _IsImmune_Grappled;
            }
            set
            {
                _IsImmune_Grappled = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Paralyzed
        private bool _IsImmune_Paralyzed;
        [XmlSaveMode("Single")]
        public bool IsImmune_Paralyzed
        {
            get
            {
                return _IsImmune_Paralyzed;
            }
            set
            {
                _IsImmune_Paralyzed = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Petrified
        private bool _IsImmune_Petrified;
        [XmlSaveMode("Single")]
        public bool IsImmune_Petrified
        {
            get
            {
                return _IsImmune_Petrified;
            }
            set
            {
                _IsImmune_Petrified = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Poisoned
        private bool _IsImmune_Poisoned;
        [XmlSaveMode("Single")]
        public bool IsImmune_Poisoned
        {
            get
            {
                return _IsImmune_Poisoned;
            }
            set
            {
                _IsImmune_Poisoned = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Prone
        private bool _IsImmune_Prone;
        [XmlSaveMode("Single")]
        public bool IsImmune_Prone
        {
            get
            {
                return _IsImmune_Prone;
            }
            set
            {
                _IsImmune_Prone = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Restrained
        private bool _IsImmune_Restrained;
        [XmlSaveMode("Single")]
        public bool IsImmune_Restrained
        {
            get
            {
                return _IsImmune_Restrained;
            }
            set
            {
                _IsImmune_Restrained = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Stunned
        private bool _IsImmune_Stunned;
        [XmlSaveMode("Single")]
        public bool IsImmune_Stunned
        {
            get
            {
                return _IsImmune_Stunned;
            }
            set
            {
                _IsImmune_Stunned = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion
        #region IsImmune_Unconscious
        private bool _IsImmune_Unconscious;
        [XmlSaveMode("Single")]
        public bool IsImmune_Unconscious
        {
            get
            {
                return _IsImmune_Unconscious;
            }
            set
            {
                _IsImmune_Unconscious = value;
                NotifyPropertyChanged();
                UpdateImmuneConditionText();
            }
        }
        #endregion

        // Databound Properties - Attributes
        #region Attr_Strength
        private int _Attr_Strength;
        [XmlSaveMode("Single")]
        public int Attr_Strength
        {
            get
            {
                return _Attr_Strength;
            }
            set
            {
                _Attr_Strength = value;
                NotifyPropertyChanged();

                UpdateModifiers();
            }
        }
        #endregion
        #region Attr_Dexterity
        private int _Attr_Dexterity;
        [XmlSaveMode("Single")]
        public int Attr_Dexterity
        {
            get
            {
                return _Attr_Dexterity;
            }
            set
            {
                _Attr_Dexterity = value;
                NotifyPropertyChanged();

                UpdateModifiers();
            }
        }
        #endregion
        #region Attr_Constitution
        private int _Attr_Constitution;
        [XmlSaveMode("Single")]
        public int Attr_Constitution
        {
            get
            {
                return _Attr_Constitution;
            }
            set
            {
                _Attr_Constitution = value;
                NotifyPropertyChanged();

                UpdateModifiers();
            }
        }
        #endregion
        #region Attr_Intelligence
        private int _Attr_Intelligence;
        [XmlSaveMode("Single")]
        public int Attr_Intelligence
        {
            get
            {
                return _Attr_Intelligence;
            }
            set
            {
                _Attr_Intelligence = value;
                NotifyPropertyChanged();

                UpdateModifiers();
            }
        }
        #endregion
        #region Attr_Wisdom
        private int _Attr_Wisdom;
        [XmlSaveMode("Single")]
        public int Attr_Wisdom
        {
            get
            {
                return _Attr_Wisdom;
            }
            set
            {
                _Attr_Wisdom = value;
                NotifyPropertyChanged();

                SetPassivePerception();

                UpdateModifiers();
            }
        }
        #endregion
        #region Attr_Charisma
        private int _Attr_Charisma;
        [XmlSaveMode("Single")]
        public int Attr_Charisma
        {
            get
            {
                return _Attr_Charisma;
            }
            set
            {
                _Attr_Charisma = value;
                NotifyPropertyChanged();

                UpdateModifiers();
            }
        }
        #endregion

        #region StrengthModifier
        private int _StrengthModifier;
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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

        // Databound Properties - D&D Saves
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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

        // Databound Properties - D&D Proficiencies
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

        #region AcrobaticsModifier
        private int _AcrobaticsModifier;
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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

        // Databound Properties - Display
        #region DisplayPopup_Weapons
        private bool _DisplayPopup_Weapons;
        public bool DisplayPopup_Weapons
        {
            get
            {
                return _DisplayPopup_Weapons;
            }
            set
            {
                _DisplayPopup_Weapons = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopup_Spells
        private bool _DisplayPopup_Spells;
        public bool DisplayPopup_Spells
        {
            get
            {
                return _DisplayPopup_Spells;
            }
            set
            {
                _DisplayPopup_Spells = value;
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
            }
        }
        #endregion
        #region DisplayPopup_Notes
        private bool _DisplayPopup_Notes;
        public bool DisplayPopup_Notes
        {
            get
            {
                return _DisplayPopup_Notes;
            }
            set
            {
                _DisplayPopup_Notes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopup_Conditions
        private bool _DisplayPopup_Conditions;
        public bool DisplayPopup_Conditions
        {
            get
            {
                return _DisplayPopup_Conditions;
            }
            set
            {
                _DisplayPopup_Conditions = value;
                NotifyPropertyChanged();
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
            }
        }
        #endregion
        #region DisplayPopupAlt_Weapons
        private bool _DisplayPopupAlt_Weapons;
        public bool DisplayPopupAlt_Weapons
        {
            get
            {
                return _DisplayPopupAlt_Weapons;
            }
            set
            {
                _DisplayPopupAlt_Weapons = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Spells
        private bool _DisplayPopupAlt_Spells;
        public bool DisplayPopupAlt_Spells
        {
            get
            {
                return _DisplayPopupAlt_Spells;
            }
            set
            {
                _DisplayPopupAlt_Spells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Skills
        private bool _DisplayPopupAlt_Skills;
        public bool DisplayPopupAlt_Skills
        {
            get
            {
                return _DisplayPopupAlt_Skills;
            }
            set
            {
                _DisplayPopupAlt_Skills = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Saves
        private bool _DisplayPopupAlt_Saves;
        public bool DisplayPopupAlt_Saves
        {
            get
            {
                return _DisplayPopupAlt_Saves;
            }
            set
            {
                _DisplayPopupAlt_Saves = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Checks
        private bool _DisplayPopupAlt_Checks;
        public bool DisplayPopupAlt_Checks
        {
            get
            {
                return _DisplayPopupAlt_Checks;
            }
            set
            {
                _DisplayPopupAlt_Checks = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Notes
        private bool _DisplayPopupAlt_Notes;
        public bool DisplayPopupAlt_Notes
        {
            get
            {
                return _DisplayPopupAlt_Notes;
            }
            set
            {
                _DisplayPopupAlt_Notes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Conditions
        private bool _DisplayPopupAlt_Conditions;
        public bool DisplayPopupAlt_Conditions
        {
            get
            {
                return _DisplayPopupAlt_Conditions;
            }
            set
            {
                _DisplayPopupAlt_Conditions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DisplayPopupAlt_Health
        private bool _DisplayPopupAlt_Health;
        public bool DisplayPopupAlt_Health
        {
            get
            {
                return _DisplayPopupAlt_Health;
            }
            set
            {
                _DisplayPopupAlt_Health = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Tags
        #region ShowTag_HasCondition
        private bool _ShowTag_HasCondition;
        public bool ShowTag_HasCondition
        {
            get
            {
                return _ShowTag_HasCondition;
            }
            set
            {
                _ShowTag_HasCondition = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowTag_HasActiveEffects
        private bool _ShowTag_HasActiveEffects;
        public bool ShowTag_HasActiveEffects
        {
            get
            {
                return _ShowTag_HasActiveEffects;
            }
            set
            {
                _ShowTag_HasActiveEffects = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Conditions
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
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
                CheckConditions();
            }
        }
        #endregion

        // Databound Properties - Formatted Texts
        #region FT_Speeds
        private string _FT_Speeds;
        public string FT_Speeds
        {
            get
            {
                return _FT_Speeds;
            }
            set
            {
                _FT_Speeds = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_HitPoints
        private string _FT_HitPoints;
        public string FT_HitPoints
        {
            get
            {
                return _FT_HitPoints;
            }
            set
            {
                _FT_HitPoints = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_ArmorClass
        private string _FT_ArmorClass;
        public string FT_ArmorClass
        {
            get
            {
                return _FT_ArmorClass;
            }
            set
            {
                _FT_ArmorClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Header
        private string _FT_Header;
        public string FT_Header
        {
            get
            {
                return _FT_Header;
            }
            set
            {
                _FT_Header = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Strength
        private string _FT_Strength;
        public string FT_Strength
        {
            get
            {
                return _FT_Strength;
            }
            set
            {
                _FT_Strength = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Dexterity
        private string _FT_Dexterity;
        public string FT_Dexterity
        {
            get
            {
                return _FT_Dexterity;
            }
            set
            {
                _FT_Dexterity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Constitution
        private string _FT_Constitution;
        public string FT_Constitution
        {
            get
            {
                return _FT_Constitution;
            }
            set
            {
                _FT_Constitution = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Intelligence
        private string _FT_Intelligence;
        public string FT_Intelligence
        {
            get
            {
                return _FT_Intelligence;
            }
            set
            {
                _FT_Intelligence = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Wisdom
        private string _FT_Wisdom;
        public string FT_Wisdom
        {
            get
            {
                return _FT_Wisdom;
            }
            set
            {
                _FT_Wisdom = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Charisma
        private string _FT_Charisma;
        public string FT_Charisma
        {
            get
            {
                return _FT_Charisma;
            }
            set
            {
                _FT_Charisma = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_Senses
        private string _FT_Senses;
        public string FT_Senses
        {
            get
            {
                return _FT_Senses;
            }
            set
            {
                _FT_Senses = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region FT_TraitsAndAbilities_P1
        private List<BoolOption> _FT_TraitsAndAbilities_P1;
        public List<BoolOption> FT_TraitsAndAbilities_P1
        {
            get
            {
                return _FT_TraitsAndAbilities_P1;
            }
            set
            {
                _FT_TraitsAndAbilities_P1 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FT_TraitsAndAbilities_P2
        private List<BoolOption> _FT_TraitsAndAbilities_P2;
        public List<BoolOption> FT_TraitsAndAbilities_P2
        {
            get
            {
                return _FT_TraitsAndAbilities_P2;
            }
            set
            {
                _FT_TraitsAndAbilities_P2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
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
        #region AddItemLink
        private RelayCommand _AddItemLink;
        public ICommand AddItemLink
        {
            get
            {
                if (_AddItemLink == null)
                {
                    _AddItemLink = new RelayCommand(param => DoAddItemLink());
                }
                return _AddItemLink;
            }
        }
        private void DoAddItemLink()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    foreach (ItemLink link in ItemLinks)
                    {
                        if (link.Name == item.Name)
                        {
                            link.Quantity += item.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    ItemLinks.Add(new ItemLink { Name = item.Name, DropChance = 100, Quantity = item.Quantity, LinkedItem = item });
                }
            }
        }
        #endregion
        #region SortItemLinks
        public ICommand SortItemLinks => new RelayCommand(DoSortItemLinks);
        private void DoSortItemLinks(object param)
        {
            ItemLinks = new(ItemLinks.OrderBy(i => i.Name).ToList());
        }
        #endregion
        #region AddSpellLink
        private RelayCommand _AddSpellLink;
        public ICommand AddSpellLink
        {
            get
            {
                if (_AddSpellLink == null)
                {
                    _AddSpellLink = new RelayCommand(param => DoAddSpellLink());
                }
                return _AddSpellLink;
            }
        }
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
        #region SortSpellLinks
        private RelayCommand _SortSpellLinks;
        public ICommand SortSpellLinks
        {
            get
            {
                if (_SortSpellLinks == null)
                {
                    _SortSpellLinks = new RelayCommand(param => DoSortSpellLinks());
                }
                return _SortSpellLinks;
            }
        }
        private void DoSortSpellLinks()
        {
            SpellLinks = new(SpellLinks.OrderBy(sl => sl.LinkedSpell.SpellLevel).ThenBy(sl => sl.Name));
        }
        #endregion
        #region AddTraitToCreature
        private RelayCommand _AddTraitToCreature;
        public ICommand AddTraitToCreature
        {
            get
            {
                if (_AddTraitToCreature == null)
                {
                    _AddTraitToCreature = new RelayCommand(param => DoAddTraitToCreature());
                }
                return _AddTraitToCreature;
            }
        }
        private void DoAddTraitToCreature()
        {
            Traits.Add(new TraitModel());
            Traits.Last().InEditMode = true;
        }
        #endregion
        #region RemoveCreature
        private RelayCommand _RemoveCreature;
        public ICommand RemoveCreature
        {
            get
            {
                if (_RemoveCreature == null)
                {
                    _RemoveCreature = new RelayCommand(DoRemoveCreature);
                }
                return _RemoveCreature;
            }
        }
        private void DoRemoveCreature(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("Invalid parameter for DoRemoveCreature.", true); return; }
            switch (param.ToString())
            {
                case "Character Minions":
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Minions.Remove(this);
                    break;
                case "Creature Builder":
                    Configuration.MainModelRef.CreatureBuilderView.AllCreatures.Remove(this);
                    Configuration.MainModelRef.CreatureBuilderView.FilteredCreatures.Remove(this);
                    break;
                case "Taming Pen":
                    YesNoDialog question = new("Remove " + Name + " from taming list?");
                    if (question.ShowDialog() == true)
                    {
                        if (question.Answer == false) { return; }
                        Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.CreaturePen.Remove(this);
                    }
                    break;
                case "Active Campaign Encounter":
                    Configuration.MainModelRef.CampaignView.ActiveCampaign.Combatants.Remove(this);
                    Configuration.MainModelRef.CampaignView.ActiveCampaign.SortCombatants();
                    break;
                case "Active Campaign Player":
                    Configuration.MainModelRef.CampaignView.ActiveCampaign.Players.Remove(this);
                    break;
                default:
                    HelperMethods.WriteToLogFile("Unhandled parameter \"" + param.ToString() + "\" for DoRemoveCreature.", true);
                    break;
            }
        }
        #endregion
        #region AdjustHitPoints
        private RelayCommand _AdjustHitPoints;
        public ICommand AdjustHitPoints
        {
            get
            {
                if (_AdjustHitPoints == null)
                {
                    _AdjustHitPoints = new RelayCommand(DoAdjustHitPoints);
                }
                return _AdjustHitPoints;
            }
        }
        private void DoAdjustHitPoints(object amount)
        {
            int newHealth = CurrentHitPoints + Convert.ToInt32(amount);
            if (newHealth < 0) { newHealth = 0; }
            if (newHealth > MaxHitPoints) { newHealth = MaxHitPoints; }
            CurrentHitPoints = newHealth;
        }
        #endregion
        #region MakeSkillCheck
        private RelayCommand _MakeSkillCheck;
        public ICommand MakeSkillCheck
        {
            get
            {
                if (_MakeSkillCheck == null)
                {
                    _MakeSkillCheck = new RelayCommand(DoMakeSkillCheck);
                }
                return _MakeSkillCheck;
            }
        }
        private void DoMakeSkillCheck(object parameter)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            string[] parts = parameter.ToString().Split(',');
            string skill = parts[0];
            bool useDisadvantage = false;
            bool useAdvantage = false;
            if (parts.Count() > 1)
            {
                useDisadvantage = (parts[1] == "Disadvantage");
                useAdvantage = (parts[1] == "Advantage");
            }
            int skillModifier = HelperMethods.GetSkillModifier(skill.ToString(), this);
            int total;
            int finalRoll;
            int firstRoll = Configuration.RNG.Next(1, 21);
            int secondRoll = Configuration.RNG.Next(1, 21);
            if (useAdvantage)
            {
                finalRoll = (firstRoll >= secondRoll) ? firstRoll : secondRoll;
            }
            else if (useDisadvantage)
            {
                finalRoll = (firstRoll >= secondRoll) ? secondRoll : firstRoll;
            }
            else
            {
                finalRoll = firstRoll;
            }
            total = finalRoll + skillModifier;
            DisplayPopup_Skills = false;
            DisplayPopupAlt_Skills = false;

            string message = string.Format("{0} made {1} {2} check{3}{4}.",
                DisplayName,
                (HelperMethods.DoesStartWithVowel(skill.ToString())) ? "an" : "a",
                skill.ToString(),
                (useAdvantage) ? " with advantage" : "",
                (useDisadvantage) ? " with disadvantage" : "");
            message += "\nResult: " + total;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nRoll: [" + finalRoll + "] + " + skillModifier; }

            //if (Configuration.MainModelRef.TabSelected_Tracker)
            //{
            //    HelperMethods.AddToEncounterLog(message);
            //}
            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Skill Check");
            }
            else
            {
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }

        }
        #endregion
        #region MakeSavingThrow
        private RelayCommand _MakeSavingThrow;
        public ICommand MakeSavingThrow
        {
            get
            {
                if (_MakeSavingThrow == null)
                {
                    _MakeSavingThrow = new RelayCommand(DoMakeSavingThrow);
                }
                return _MakeSavingThrow;
            }
        }
        private void DoMakeSavingThrow(object parameter)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            string[] parts = parameter.ToString().Split(',');
            string save = parts[0];
            bool useDisadvantage = false;
            bool useAdvantage = false;
            if (parts.Count() > 1)
            {
                useDisadvantage = (parts[1] == "Disadvantage");
                useAdvantage = (parts[1] == "Advantage");
            }
            int saveModifier = HelperMethods.GetSaveModifier(save.ToString(), this);
            int total;
            int finalRoll;
            int firstRoll = Configuration.RNG.Next(1, 21);
            int secondRoll = Configuration.RNG.Next(1, 21);
            if (useAdvantage)
            {
                finalRoll = (firstRoll >= secondRoll) ? firstRoll : secondRoll;
            }
            else if (useDisadvantage)
            {
                finalRoll = (firstRoll >= secondRoll) ? secondRoll : firstRoll;
            }
            else
            {
                finalRoll = firstRoll;
            }
            total = finalRoll + saveModifier;
            DisplayPopup_Saves = false;
            DisplayPopupAlt_Saves = false;

            string message = string.Format("{0} made {1} {2} saving throw{3}{4}.",
                DisplayName,
                (HelperMethods.DoesStartWithVowel(save.ToString())) ? "an" : "a",
                save.ToString(),
                (useAdvantage) ? " with advantage" : "",
                (useDisadvantage) ? " with disadvantage" : "");
            message += "\nResult: " + total;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nRoll: [" + finalRoll + "] + " + saveModifier; }

            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Saving Throw");
            }
            else
            {
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }

        }
        #endregion
        #region MakeAttributeCheck
        private RelayCommand _MakeAttributeCheck;
        public ICommand MakeAttributeCheck
        {
            get
            {
                if (_MakeAttributeCheck == null)
                {
                    _MakeAttributeCheck = new RelayCommand(DoMakeAttributeCheck);
                }
                return _MakeAttributeCheck;
            }
        }
        private void DoMakeAttributeCheck(object parameter)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            string[] parts = parameter.ToString().Split(',');
            string attribute = parts[0];
            bool useDisadvantage = false;
            bool useAdvantage = false;
            if (parts.Count() > 1)
            {
                useDisadvantage = (parts[1] == "Disadvantage");
                useAdvantage = (parts[1] == "Advantage");
            }
            int attrMod = HelperMethods.GetAttributeCheck(attribute.ToString(), this);
            int total;
            int finalRoll;
            int firstRoll = Configuration.RNG.Next(1, 21);
            int secondRoll = Configuration.RNG.Next(1, 21);
            if (useAdvantage)
            {
                finalRoll = (firstRoll >= secondRoll) ? firstRoll : secondRoll;
            }
            else if (useDisadvantage)
            {
                finalRoll = (firstRoll >= secondRoll) ? secondRoll : firstRoll;
            }
            else
            {
                finalRoll = firstRoll;
            }
            total = finalRoll + attrMod;
            DisplayPopup_Saves = false;
            DisplayPopupAlt_Saves = false;

            string message = string.Format("{0} made {1} {2} check{3}{4}.",
                DisplayName,
                (HelperMethods.DoesStartWithVowel(attribute.ToString())) ? "an" : "a",
                attribute.ToString(),
                (useAdvantage) ? " with advantage" : "",
                (useDisadvantage) ? " with disadvantage" : "");

            message += "\nResult: " + total;

            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nRoll: [" + finalRoll + "] + " + attrMod; }

            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Ability Check");
            }
            else
            {
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }
            DisplayPopup_Checks = false;
            DisplayPopupAlt_Checks = false;

        }
        #endregion
        #region RollLoot
        private RelayCommand _RollLoot;
        public ICommand RollLoot
        {
            get
            {
                if (_RollLoot == null)
                {
                    _RollLoot = new RelayCommand(param => DoRollLoot());
                }
                return _RollLoot;
            }
        }
        private void DoRollLoot()
        {

            if (HasBeenLooted)
            {
                if (Configuration.MainModelRef.TabSelected_Campaigns)
                {
                    HelperMethods.AddToCampaignMessages(DisplayName + " has already been looted.", "Loot");
                }
                return;
            }
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            string message = "Loot found on " + DisplayName + ": ";
            if (CoinDrop > 0)
            {
                int coinRoll = Configuration.RNG.Next(CoinDrop / 2, CoinDrop + 1);
                message += "\nMoney: " + HelperMethods.GetDerivedCoinage(coinRoll);
            }
            foreach (ItemLink link in ItemLinks)
            {
                int dropQty = 0;
                for (int i = 0; i < link.Quantity; i++)
                {
                    dropQty += (Configuration.RNG.Next(1, 101) <= link.DropChance) ? 1 : 0;
                }
                if (dropQty > 0) { message += string.Format("\n{0} x {1}", dropQty, link.Name); }
            }
            //if (Configuration.MainModelRef.TabSelected_Tracker)
            //{
            //    HelperMethods.AddToEncounterLog(message);
            //}
            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Loot");
            }
            HasBeenLooted = true;

        }
        #endregion
        #region TestLootRoll
        private RelayCommand _TestLootRoll;
        public ICommand TestLootRoll
        {
            get
            {
                if (_TestLootRoll == null)
                {
                    _TestLootRoll = new RelayCommand(param => DoTestLootRoll());
                }
                return _TestLootRoll;
            }
        }
        private void DoTestLootRoll()
        {
            string message = "Loot found on " + Name + ": ";
            if (CoinDrop > 0)
            {
                int coinRoll = Configuration.RNG.Next(CoinDrop / 2, CoinDrop + 1);
                message += "\nMoney: " + HelperMethods.GetDerivedCoinage(coinRoll);
            }
            foreach (ItemLink link in ItemLinks)
            {
                int dropQty = 0;
                for (int i = 0; i < link.Quantity; i++)
                {
                    dropQty += (Configuration.RNG.Next(1, 101) <= link.DropChance) ? 1 : 0;
                }
                if (dropQty > 0) { message += string.Format("\n{0} x {1}", dropQty, link.Name); }
            }
            new NotificationDialog(message).ShowDialog();
        }
        #endregion
        #region DuplicateCreature
        public ICommand DuplicateCreature
        {
            get
            {
                return new RelayCommand(param => DoDuplicateCreature());
            }
        }
        private void DoDuplicateCreature()
        {
            CreatureModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.CreatureBuilderView.AllCreatures.Insert(Configuration.MainModelRef.CreatureBuilderView.AllCreatures.IndexOf(this), duplicate);
            Configuration.MainModelRef.CreatureBuilderView.FilteredCreatures.Insert(Configuration.MainModelRef.CreatureBuilderView.FilteredCreatures.IndexOf(this), duplicate);
            Configuration.MainModelRef.CreatureBuilderView.ActiveCreature = duplicate;
        }
        #endregion
        #region ToggleEnemyAlly
        private RelayCommand _ToggleEnemyAlly;
        public ICommand ToggleEnemyAlly
        {
            get
            {
                if (_ToggleEnemyAlly == null)
                {
                    _ToggleEnemyAlly = new RelayCommand(param => DoToggleEnemyAlly());
                }
                return _ToggleEnemyAlly;
            }
        }
        private void DoToggleEnemyAlly()
        {
            IsAlly = !IsAlly;
        }
        #endregion
        #region ToggleNpc
        private RelayCommand _ToggleNpc;
        public ICommand ToggleNpc
        {
            get
            {
                if (_ToggleNpc == null)
                {
                    _ToggleNpc = new RelayCommand(param => DoToggleNpc());
                }
                return _ToggleNpc;
            }
        }
        private void DoToggleNpc()
        {
            IsNpc = !IsNpc;
            GetPortraitFilepath();
        }
        #endregion
        #region ToggleOoc
        private RelayCommand _ToggleOoc;
        public ICommand ToggleOoc
        {
            get
            {
                if (_ToggleOoc == null)
                {
                    _ToggleOoc = new RelayCommand(param => DoToggleOoc());
                }
                return _ToggleOoc;
            }
        }
        private void DoToggleOoc()
        {
            IsOoc = !IsOoc;
            // Configuration.MainModelRef.TrackerView.SortCombatants();
        }
        #endregion
        #region SortTraits
        private RelayCommand _SortTraits;
        public ICommand SortTraits
        {
            get
            {
                if (_SortTraits == null)
                {
                    _SortTraits = new RelayCommand(param => DoSortTraits());
                }
                return _SortTraits;
            }
        }
        private void DoSortTraits()
        {
            Traits = new ObservableCollection<TraitModel>(Traits.OrderBy(trait => trait.Name));
        }
        #endregion
        #region ToggleActiveCombatant
        private RelayCommand _ToggleActiveCombatant;
        public ICommand ToggleActiveCombatant
        {
            get
            {
                if (_ToggleActiveCombatant == null)
                {
                    _ToggleActiveCombatant = new RelayCommand(param => DoToggleActiveCombatant());
                }
                return _ToggleActiveCombatant;
            }
        }
        private void DoToggleActiveCombatant()
        {
            IsActive = !IsActive;
        }
        #endregion
        #region PerformTamingSession
        private RelayCommand _PerformTamingSession;
        public ICommand PerformTamingSession
        {
            get
            {
                if (_PerformTamingSession == null)
                {
                    _PerformTamingSession = new RelayCommand(DoPerformTamingSession);
                }
                return _PerformTamingSession;
            }
        }
        private void DoPerformTamingSession(object adv)
        {
            if (adv == null) { adv = ""; }
            CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            bool useAdv = (adv.ToString() == "A");
            bool useDis = (adv.ToString() == "D");

            int diceResult = HelperMethods.RollD20(useAdv, useDis);
            int finalResult = diceResult + character.AnimalHandlingModifier;
            int difficulty = 10 + Attr_Intelligence;
            difficulty = Size switch
            {
                "Large" => difficulty + 2,
                "Huge" => difficulty + 5,
                "Gargantuan" => difficulty + 10,
                _ => difficulty
            };
            string message = character.Name + " attempts to train " + Name + ".";
            message += "\nCheck DC: " + difficulty;
            message += "\nResult: " + finalResult;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
            {
                message += "\nRoll: [" + diceResult + "] + " + character.AnimalHandlingModifier;
            }

            TamingProgress += (finalResult - difficulty + 1);

            message += finalResult switch
            {
                int n when (n >= difficulty) => "\nRelation with " + Name + " has improved.",
                int n when (n < difficulty) => "\nRelation with " + Name + " has worsened.",
                _ => "\nRelation with " + Name + " is unchanged."
            };

            HelperMethods.AddToPlayerLog(message);

            if (TamingProgress <= 0)
            {
                character.CreaturePen.Remove(this);
                new NotificationDialog(Name + " has become hostile, please inform the DM.").ShowDialog();
            }

            if (TamingProgress >= ExperienceValue)
            {
                character.Minions.Add(HelperMethods.DeepClone(this));
                character.CreaturePen.Remove(this);
                new NotificationDialog(Name + " has become tamed and is now in the Combat - Minions section.").ShowDialog();
            }

        }
        #endregion
        #region ResetSpellSlots
        public ICommand ResetSpellSlots => new RelayCommand(DoResetSpellSlots);
        private void DoResetSpellSlots(object param)
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
        #endregion
        #region AddCounter
        public ICommand AddCounter => new RelayCommand(param => DoAddCounter());
        private void DoAddCounter()
        {
            Counters.Add(new CounterModel());
        }
        #endregion
        #region RollInitiative
        public ICommand RollInitiative => new RelayCommand(DoRollInitiative);
        private void DoRollInitiative(object param)
        {
            Initiative = Configuration.RNG.Next(1, 21) + HelperMethods.GetAttributeModifier(Attr_Dexterity);
            string message = Name + " rolled for initiative and got " + Initiative + ".";
            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Initiative");
                Configuration.MainModelRef.CampaignView.ActiveCampaign.SortByInitiative.Execute(null);
            }
            if (Configuration.MainModelRef.TabSelected_Players)
            {
                HelperMethods.AddToPlayerLog(message, "Initiative", true);
            }
        }
        #endregion
        #region AddEnvironment
        public ICommand AddEnvironment => new RelayCommand(DoAddEnvironment);
        private void DoAddEnvironment(object param)
        {
            List<ConvertibleValue> options = new();
            foreach (string sc in Configuration.CreatureEnvironments)
            {
                options.Add(new(sc));
            }
            MultiObjectSelectionDialog selectionDialog = new(options, "Creature Environments");
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ConvertibleValue cv in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCVs)
                {
                    bool existingFound = false;
                    foreach (ConvertibleValue opt in Environments)
                    {
                        if (opt.Value == cv.Value)
                        {
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    Environments.Add(new() { Value = cv.Value });
                }
                Environments = new(Environments.OrderBy(item => item.Value));
            }
        }
        #endregion

        // Public Methods
        public void RollHitPoints(bool useAverage)
        {
            int totalHealth = 0;

            if (useAverage)
            {
                totalHealth = HelperMethods.GetAverageOfDice(HitDiceQuantity, HitDiceQuality, HitDiceModifier);
            }
            else
            {
                for (int i = 0; i < HitDiceQuantity; i++)
                {
                    totalHealth += Configuration.RNG.Next(1, HitDiceQuality);
                }
                totalHealth += HitDiceModifier;
            }

            MaxHitPoints = totalHealth;
            CurrentHitPoints = totalHealth;

        }
        public void SetPassivePerception()
        {
            PassivePerception = 10 + HelperMethods.GetAttributeModifier(Attr_Wisdom) + ((IsProf_Perception) ? ProficiencyBonus : 0) + ((IsExpert_Perception) ? ProficiencyBonus : 0);
        }
        public void UpdateStatus()
        {
            if (MaxHitPoints == 0) { return; }
            double healthPercentage = Convert.ToDouble(CurrentHitPoints) / Convert.ToDouble(MaxHitPoints);
            if (healthPercentage >= 0.76) { Status = "Fine"; return; }
            if (healthPercentage >= 0.51) { Status = "Bruised"; return; }
            if (healthPercentage >= 0.26) { Status = "Bloodied"; return; }
            if (healthPercentage >= 0.01) { Status = "Wounded"; return; }
            Status = "Dead"; return;
        }
        public void SetFormattedTexts()
        {
            FT_Speeds = "";
            if (Speed > 0) { FT_Speeds += Speed + " ft."; }
            if (SwimSpeed > 0) { FT_Speeds += ((FT_Speeds == "") ? "" : ", ") + "Swim " + SwimSpeed + " ft."; }
            if (FlySpeed > 0) { FT_Speeds += ((FT_Speeds == "") ? "" : ", ") + "Fly " + FlySpeed + " ft."; }
            if (ClimbSpeed > 0) { FT_Speeds += ((FT_Speeds == "") ? "" : ", ") + "Climb " + ClimbSpeed + " ft."; }
            if (BurrowSpeed > 0) { FT_Speeds += ((FT_Speeds == "") ? "" : ", ") + "Burrow " + BurrowSpeed + " ft."; }

            FT_HitPoints = HitDiceQuantity + "d" + HitDiceQuality + " + " + HitDiceModifier;
            FT_ArmorClass = ArmorClass + ((ArmorType != "" && ArmorType != null) ? " (" + ArmorType + ")" : "");
            FT_Header = string.Format("{0} {1}{2}, {3}, CR {4} ({5} XP)", Size, CreatureCategory, ((CreatureSubCategory != "") ? " (" + CreatureSubCategory + ")" : ""), Alignment, ChallengeRating, ExperienceValue);

            FT_Strength = string.Format("{0} ({1}{2})", Attr_Strength, ((StrengthModifier >= 0) ? "+" : ""), StrengthModifier);
            FT_Dexterity = string.Format("{0} ({1}{2})", Attr_Dexterity, ((DexterityModifier >= 0) ? "+" : ""), DexterityModifier);
            FT_Constitution = string.Format("{0} ({1}{2})", Attr_Constitution, ((ConstitutionModifier >= 0) ? "+" : ""), ConstitutionModifier);
            FT_Intelligence = string.Format("{0} ({1}{2})", Attr_Intelligence, ((IntelligenceModifier >= 0) ? "+" : ""), IntelligenceModifier);
            FT_Wisdom = string.Format("{0} ({1}{2})", Attr_Wisdom, ((WisdomModifier >= 0) ? "+" : ""), WisdomModifier);
            FT_Charisma = string.Format("{0} ({1}{2})", Attr_Charisma, ((CharismaModifier >= 0) ? "+" : ""), CharismaModifier);

            FT_Senses = "";
            if (Blindsight > 0) { FT_Senses += "Blindsight " + Blindsight + " ft."; }
            if (Darkvision > 0) { FT_Senses += ((FT_Senses == "") ? "" : ", ") + "Darkvision " + Darkvision + " ft."; }
            if (Tremorsense > 0) { FT_Senses += ((FT_Senses == "") ? "" : ", ") + "Tremorsense " + Tremorsense + " ft."; }
            if (Truesight > 0) { FT_Senses += ((FT_Senses == "") ? "" : ", ") + "Truesight " + Truesight + " ft."; }
            if (PassivePerception > 0) { FT_Senses += ((FT_Senses == "") ? "" : ", ") + "Passive Perception " + PassivePerception; }

            GenerateTraitAndAbilityLists();

        }
        public void ConnectSpellLinks()
        {
            foreach (SpellLink link in SpellLinks)
            {
                link.LinkedSpell = Configuration.SpellRepository.FirstOrDefault(s => s.Name == link.Name);
            }
        }
        public void ConnectItemLinks()
        {
            foreach (ItemLink link in ItemLinks)
            {
                link.LinkedItem = Configuration.ItemRepository.FirstOrDefault(i => i.Name == link.Name);
            }
        }
        public void UpdateAverageHitPoints()
        {
            AverageHitPoints = HelperMethods.GetAverageOfDice(HitDiceQuantity, HitDiceQuality, HitDiceModifier);
        }
        public void RefreshSpellSlots()
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
        public void RefreshCounters()
        {
            foreach (CounterModel counter in Counters)
            {
                counter.CurrentValue = counter.MaxValue;
            }
        }
        public void GetPortraitFilepath()
        {
            string directory = Environment.CurrentDirectory;
            if (IsNpc) { directory += "/Images/Npcs/"; }
            else if (IsPlayer) { directory += "/Images/Players/"; }
            else { directory += "/Images/Creatures/"; }

            string imagePng = directory + Name + ".png";
            string imageJpg = directory + Name + ".jpg";
            string imageGif = directory + Name + ".gif";
            if (File.Exists(imagePng)) { PortraitImagePath = imagePng; return; }
            if (File.Exists(imageJpg)) { PortraitImagePath = imageJpg; return; }
            if (File.Exists(imageGif)) { PortraitImagePath = imageGif; return; }

            PortraitImagePath = CreatureCategory switch
            {
                "Aberration" => "/Images/Icons/aberration.png",
                "Beast" => "/Images/Icons/beast.png",
                "Celestial" => "/Images/Icons/celestial.png",
                "Construct" => "/Images/Icons/construct.png",
                "Dragon" => "/Images/Icons/dragonclaw.png",
                "Elemental" => "/Images/Icons/spell.png",
                "Fey" => "/Images/Icons/fey.png",
                "Fiend" => "/Images/Icons/fiend.png",
                "Giant" => "/Images/Icons/step.png",
                "Humanoid" => "/Images/Icons/smile.png",
                "Monstrosity" => "/Images/Icons/monster.png",
                "Ooze" => "/Images/Icons/orb.png",
                "Plant" => "/Images/Icons/tree.png",
                "Undead" => "/Images/Icons/dm_skull.png",
                _ => "/Images/Icons/cube.png"
            };

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
        public void UpdateAbilityDescriptions()
        {
            if (IsValidated == false) { return; } // Don't backfill descriptions for unfinished creatures
            foreach (CustomAbility ability in Abilities)
            {
                ability.SetGeneratedDescription(this);
            }
        }
        public void SetHighestSpeedValues()
        {
            string type = "";
            int val = 0;

            if (Speed > val) { type = "Walk"; val = Speed; }
            if (FlySpeed > val) { type = "Fly"; val = FlySpeed; }
            if (ClimbSpeed > val) { type = "Climb"; val = ClimbSpeed; }
            if (BurrowSpeed > val) { type = "Burrow"; val = BurrowSpeed; }
            if (SwimSpeed > val) { type = "Swim"; val = SwimSpeed; }

            HighestSpeedType = type;
            HighestSpeedValue = val;

        }

        // Private Methods
        private void UpdateModifiers()
        {
            // Update Base Attribute Modifiers
            StrengthModifier = HelperMethods.GetAttributeModifier(Attr_Strength);
            DexterityModifier = HelperMethods.GetAttributeModifier(Attr_Dexterity);
            ConstitutionModifier = HelperMethods.GetAttributeModifier(Attr_Constitution);
            IntelligenceModifier = HelperMethods.GetAttributeModifier(Attr_Intelligence);
            WisdomModifier = HelperMethods.GetAttributeModifier(Attr_Wisdom);
            CharismaModifier = HelperMethods.GetAttributeModifier(Attr_Charisma);

            // Update Spellcasting Modifiers
            SpellAbilityModifier = SpellcastingAbility switch
            {
                "Strength" => StrengthModifier,
                "Dexterity" => DexterityModifier,
                "Constitution" => ConstitutionModifier,
                "Intelligence" => IntelligenceModifier,
                "Wisdom" => WisdomModifier,
                "Charisma" => CharismaModifier,
                _ => 0
            };
            SpellSaveDc = 8 + ProficiencyBonus + SpellAbilityModifier;
            SpellAttackBonus = ProficiencyBonus + SpellAbilityModifier;

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
            AcrobaticsModifier = DexterityModifier;
            AnimalHandlingModifier = WisdomModifier;
            ArcanaModifier = IntelligenceModifier;
            AthleticsModifier = StrengthModifier;
            DeceptionModifier = CharismaModifier;
            HistoryModifier = IntelligenceModifier;
            InsightModifier = WisdomModifier;
            IntimidationModifier = CharismaModifier;
            InvestigationModifier = IntelligenceModifier;
            MedicineModifier = WisdomModifier;
            NatureModifier = IntelligenceModifier;
            PerceptionModifier = WisdomModifier;
            PerformanceModifier = CharismaModifier;
            PersuasionModifier = CharismaModifier;
            ReligionModifier = IntelligenceModifier;
            SleightOfHandModifier = DexterityModifier;
            StealthModifier = DexterityModifier;
            SurvivalModifier = WisdomModifier;
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

        }
        private void UpdateExperienceValue(string challengeRating)
        {
            ExperienceValue = challengeRating switch
            {
                "0" => 10,
                "1/8" => 25,
                "1/4" => 50,
                "1/2" => 100,
                "1" => 200,
                "2" => 450,
                "3" => 700,
                "4" => 1100,
                "5" => 1800,
                "6" => 2300,
                "7" => 2900,
                "8" => 3900,
                "9" => 5000,
                "10" => 5900,
                "11" => 7200,
                "12" => 8400,
                "13" => 10000,
                "14" => 11500,
                "15" => 13000,
                "16" => 15000,
                "17" => 18000,
                "18" => 20000,
                "19" => 22000,
                "20" => 25000,
                "21" => 33000,
                "22" => 41000,
                "23" => 50000,
                "24" => 62000,
                "25" => 75000,
                "26" => 90000,
                "27" => 105000,
                "28" => 120000,
                "29" => 135000,
                "30" => 155000,
                _ => 0
            };
        }
        private void CheckConditions()
        {
            ShowTag_HasCondition = false;
            if (IsBlinded) { ShowTag_HasCondition = true; }
            if (IsCharmed) { ShowTag_HasCondition = true; }
            if (IsDeafened) { ShowTag_HasCondition = true; }
            if (IsFrightened) { ShowTag_HasCondition = true; }
            if (IsGrappled) { ShowTag_HasCondition = true; }
            if (IsIncapacitated) { ShowTag_HasCondition = true; }
            if (IsInvisible) { ShowTag_HasCondition = true; }
            if (IsParalyzed) { ShowTag_HasCondition = true; }
            if (IsPetrified) { ShowTag_HasCondition = true; }
            if (IsPoisoned) { ShowTag_HasCondition = true; }
            if (IsProne) { ShowTag_HasCondition = true; }
            if (IsRestrained) { ShowTag_HasCondition = true; }
            if (IsStunned) { ShowTag_HasCondition = true; }
            if (IsUnconscious) { ShowTag_HasCondition = true; }
        }
        private void UpdateImmuneConditionText()
        {
            string text = "";
            text += (IsImmune_Blinded) ? "Blinded, " : "";
            text += (IsImmune_Charmed) ? "Charmed, " : "";
            text += (IsImmune_Deafened) ? "Deafened, " : "";
            text += (IsImmune_Exhaustion) ? "Exhaustion, " : "";
            text += (IsImmune_Frightened) ? "Frightened, " : "";
            text += (IsImmune_Grappled) ? "Grappled, " : "";
            text += (IsImmune_Paralyzed) ? "Paralyzed, " : "";
            text += (IsImmune_Petrified) ? "Petrified, " : "";
            text += (IsImmune_Poisoned) ? "Poisoned, " : "";
            text += (IsImmune_Prone) ? "Prone, " : "";
            text += (IsImmune_Restrained) ? "Restrained, " : "";
            text += (IsImmune_Stunned) ? "Stunned, " : "";
            text += (IsImmune_Unconscious) ? "Unconscious, " : "";
            text = text.Trim();
            if (text != "")
            {
                if (text.Last() == ',') { text = text.Substring(0, text.Length - 1); }
            }
            
            ConditionImmunities = text;
        }
        private void UpdateDamageProclivityTexts()
        {
            string vul = "";
            string res = "";
            string imm = "";

            vul += (DamageProclivity_Acid == "Vulnerable") ? "Acid, " : "";
            vul += (DamageProclivity_Cold == "Vulnerable") ? "Cold, " : "";
            vul += (DamageProclivity_Fire == "Vulnerable") ? "Fire, " : "";
            vul += (DamageProclivity_Force == "Vulnerable") ? "Force, " : "";
            vul += (DamageProclivity_Lightning == "Vulnerable") ? "Lightning, " : "";
            vul += (DamageProclivity_Necrotic == "Vulnerable") ? "Necrotic, " : "";
            vul += (DamageProclivity_Poison == "Vulnerable") ? "Poison, " : "";
            vul += (DamageProclivity_Psychic == "Vulnerable") ? "Psychic, " : "";
            vul += (DamageProclivity_Radiant == "Vulnerable") ? "Radiant, " : "";
            vul += (DamageProclivity_Thunder == "Vulnerable") ? "Thunder, " : "";
            vul += (DamageProclivity_Bludgeoning == "Vulnerable") ? "Bludgeoning, " : "";
            vul += (DamageProclivity_Piercing == "Vulnerable") ? "Piercing, " : "";
            vul += (DamageProclivity_Slashing == "Vulnerable") ? "Slashing, " : "";
            vul = vul.Trim();
            if (vul != "")
            {
                if (vul.Last() == ',') { vul = vul.Substring(0, vul.Length - 1); }
            }
            

            res += (DamageProclivity_Acid == "Resistant") ? "Acid, " : "";
            res += (DamageProclivity_Cold == "Resistant") ? "Cold, " : "";
            res += (DamageProclivity_Fire == "Resistant") ? "Fire, " : "";
            res += (DamageProclivity_Force == "Resistant") ? "Force, " : "";
            res += (DamageProclivity_Lightning == "Resistant") ? "Lightning, " : "";
            res += (DamageProclivity_Necrotic == "Resistant") ? "Necrotic, " : "";
            res += (DamageProclivity_Poison == "Resistant") ? "Poison, " : "";
            res += (DamageProclivity_Psychic == "Resistant") ? "Psychic, " : "";
            res += (DamageProclivity_Radiant == "Resistant") ? "Radiant, " : "";
            res += (DamageProclivity_Thunder == "Resistant") ? "Thunder, " : "";
            res += (DamageProclivity_Bludgeoning == "Resistant") ? "Bludgeoning, " : "";
            res += (DamageProclivity_Bludgeoning == "Resistant if Non-Magical") ? "Non-Magical Bludgeoning, " : "";
            res += (DamageProclivity_Bludgeoning == "Resistant if Non-Adamantine") ? "Non-Adamantine Bludgeoning, " : "";
            res += (DamageProclivity_Bludgeoning == "Resistant if Non-Silvered") ? "Non-Silvered Bludgeoning, " : "";
            res += (DamageProclivity_Piercing == "Resistant") ? "Piercing, " : "";
            res += (DamageProclivity_Piercing == "Resistant if Non-Magical") ? "Non-Magical Piercing, " : "";
            res += (DamageProclivity_Piercing == "Resistant if Non-Adamantine") ? "Non-Adamantine Piercing, " : "";
            res += (DamageProclivity_Piercing == "Resistant if Non-Silvered") ? "Non-Silvered Piercing, " : "";
            res += (DamageProclivity_Slashing == "Resistant") ? "Slashing, " : "";
            res += (DamageProclivity_Slashing == "Resistant if Non-Magical") ? "Non-Magical Slashing, " : "";
            res += (DamageProclivity_Slashing == "Resistant if Non-Adamantine") ? "Non-Adamantine Slashing, " : "";
            res += (DamageProclivity_Slashing == "Resistant if Non-Silvered") ? "Non-Silvered Slashing, " : "";
            
            res = res.Trim();
            if (res != "")
            {
                if (res.Last() == ',') { res = res.Substring(0, res.Length - 1); }
            }

            imm += (DamageProclivity_Acid == "Immune") ? "Acid, " : "";
            imm += (DamageProclivity_Cold == "Immune") ? "Cold, " : "";
            imm += (DamageProclivity_Fire == "Immune") ? "Fire, " : "";
            imm += (DamageProclivity_Force == "Immune") ? "Force, " : "";
            imm += (DamageProclivity_Lightning == "Immune") ? "Lightning, " : "";
            imm += (DamageProclivity_Necrotic == "Immune") ? "Necrotic, " : "";
            imm += (DamageProclivity_Poison == "Immune") ? "Poison, " : "";
            imm += (DamageProclivity_Psychic == "Immune") ? "Psychic, " : "";
            imm += (DamageProclivity_Radiant == "Immune") ? "Radiant, " : "";
            imm += (DamageProclivity_Thunder == "Immune") ? "Thunder, " : "";
            imm += (DamageProclivity_Bludgeoning == "Immune") ? "Bludgeoning, " : "";
            imm += (DamageProclivity_Bludgeoning == "Immune if Non-Magical") ? "Non-Magical Bludgeoning, " : "";
            imm += (DamageProclivity_Bludgeoning == "Immune if Non-Adamantine") ? "Non-Adamantine Bludgeoning, " : "";
            imm += (DamageProclivity_Bludgeoning == "Immune if Non-Silvered") ? "Non-Silvered Bludgeoning, " : "";
            imm += (DamageProclivity_Piercing == "Immune") ? "Piercing, " : "";
            imm += (DamageProclivity_Piercing == "Immune if Non-Magical") ? "Non-Magical Piercing, " : "";
            imm += (DamageProclivity_Piercing == "Immune if Non-Adamantine") ? "Non-Adamantine Piercing, " : "";
            imm += (DamageProclivity_Piercing == "Immune if Non-Silvered") ? "Non-Silvered Piercing, " : "";
            imm += (DamageProclivity_Slashing == "Immune") ? "Slashing, " : "";
            imm += (DamageProclivity_Slashing == "Immune if Non-Magical") ? "Non-Magical Slashing, " : "";
            imm += (DamageProclivity_Slashing == "Immune if Non-Adamantine") ? "Non-Adamantine Slashing, " : "";
            imm += (DamageProclivity_Slashing == "Immune if Non-Silvered") ? "Non-Silvered Slashing, " : "";
            
            imm = imm.Trim();
            if (imm != "")
            {
                if (imm.Last() == ',') { imm = imm.Substring(0, imm.Length - 1); }
            }

            Vulnerabilities = vul;
            Resistances = res;
            Immunities = imm;


        }
        
        

    }
}
