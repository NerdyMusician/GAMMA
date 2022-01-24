using ExtensionMethods;
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
            Languages = "";
            Abilities = new();
            Counters = new();
            SpellLinks = new();
            Traits = new();
            ItemLinks = new();
            ActiveEffectAbilities = new();
            Environments = new();
            Vulnerabilities = "";
            Immunities = "";
            Resistances = "";
            ConditionImmunities = "";

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
            ShowTag_HasActiveEffects = (ActiveEffectAbilities.Count > 0);
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

        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region PortraitImagePath
        private string _PortraitImagePath;
        public string PortraitImagePath
        {
            get => _PortraitImagePath;
            set => SetAndNotify(ref _PortraitImagePath, value);
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
        #region TrackerIndicator
        private string _TrackerIndicator;
        [XmlSaveMode(XSME.Single)]
        public string TrackerIndicator
        {
            get => _TrackerIndicator;
            set => SetAndNotify(ref _TrackerIndicator, value);
        }
        #endregion
        #region DisplayName
        private string _DisplayName;
        [XmlSaveMode(XSME.Single)]
        public string DisplayName
        {
            get => _DisplayName;
            set => SetAndNotify(ref _DisplayName, value);
        }
        #endregion
        #region IsPlayer
        private bool _IsPlayer;
        [XmlSaveMode(XSME.Single)]
        public bool IsPlayer
        {
            get => _IsPlayer;
            set => SetAndNotify(ref _IsPlayer, value);
        }
        #endregion
        #region IsAlly
        private bool _IsAlly;
        [XmlSaveMode(XSME.Single)]
        public bool IsAlly
        {
            get => _IsAlly;
            set => SetAndNotify(ref _IsAlly, value);
        }
        #endregion
        #region IsNpc
        private bool _IsNpc;
        [XmlSaveMode(XSME.Single)]
        public bool IsNpc
        {
            get => _IsNpc;
            set => SetAndNotify(ref _IsNpc, value);
        }
        #endregion
        #region IsOoc
        private bool _IsOoc;
        [XmlSaveMode(XSME.Single)]
        public bool IsOoc
        {
            get => _IsOoc;
            set => SetAndNotify(ref _IsOoc, value);
        }
        #endregion
        #region HasMiniature
        private bool _HasMiniature;
        [XmlSaveMode(XSME.Single)]
        public bool HasMiniature
        {
            get => _HasMiniature;
            set => SetAndNotify(ref _HasMiniature, value);
        }
        #endregion
        #region MiniatureQuantity
        private int _MiniatureQuantity;
        [XmlSaveMode(XSME.Single)]
        public int MiniatureQuantity
        {
            get => _MiniatureQuantity;
            set => SetAndNotify(ref _MiniatureQuantity, value);
        }
        #endregion
        #region MiniatureLocation
        private string _MiniatureLocation;
        [XmlSaveMode(XSME.Single)]
        public string MiniatureLocation
        {
            get => _MiniatureLocation;
            set => SetAndNotify(ref _MiniatureLocation, value);
        }
        #endregion

        // Campaign View Only //
        #region PlayerName
        private string _PlayerName;
        [XmlSaveMode(XSME.Single)]
        public string PlayerName
        {
            get => _PlayerName;
            set => SetAndNotify(ref _PlayerName, value);
        }
        #endregion
        #region PlayerRaceAndClass
        private string _PlayerRaceAndClass;
        [XmlSaveMode(XSME.Single)]
        public string PlayerRaceAndClass
        {
            get => _PlayerRaceAndClass;
            set => SetAndNotify(ref _PlayerRaceAndClass, value);
        }
        #endregion
        #region PlayerPassivePerception
        private int _PlayerPassivePerception;
        [XmlSaveMode(XSME.Single)]
        public int PlayerPassivePerception
        {
            get => _PlayerPassivePerception;
            set => SetAndNotify(ref _PlayerPassivePerception, value);
        }
        #endregion
        #region PlayerSpellSaveDc
        private int _PlayerSpellSaveDc;
        [XmlSaveMode(XSME.Single)]
        public int PlayerSpellSaveDc
        {
            get => _PlayerSpellSaveDc;
            set => SetAndNotify(ref _PlayerSpellSaveDc, value);
        }
        #endregion

        #region CreatureCategory
        private string _CreatureCategory;
        [XmlSaveMode(XSME.Single)]
        public string CreatureCategory
        {
            get => _CreatureCategory;
            set => SetAndNotify(ref _CreatureCategory, value);
        }
        #endregion
        #region CreatureSubCategory
        private string _CreatureSubCategory;
        [XmlSaveMode(XSME.Single)]
        public string CreatureSubCategory
        {
            get => _CreatureSubCategory;
            set => SetAndNotify(ref _CreatureSubCategory, value);
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
        #region Notes
        private string _Notes;
        [XmlSaveMode(XSME.Single)]
        public string Notes
        {
            get => _Notes;
            set => SetAndNotify(ref _Notes, value);
        }
        #endregion
        #region CurrentHitPoints
        private int _CurrentHitPoints;
        [XmlSaveMode(XSME.Single)]
        public int CurrentHitPoints
        {
            get => _CurrentHitPoints;
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
        [XmlSaveMode(XSME.Single)]
        public int MaxHitPoints
        {
            get => _MaxHitPoints;
            set => SetAndNotify(ref _MaxHitPoints, value);
        }
        #endregion
        #region Status
        // Dynamic status for the Creature based on health or condition.
        private string _Status;
        public string Status
        {
            get => _Status;
            set => SetAndNotify(ref _Status, value);
        }
        #endregion
        #region Size
        // Size category of the Creature. (e.g. Medium, Large)
        private string _Size;
        [XmlSaveMode(XSME.Single)]
        public string Size
        {
            get => _Size;
            set => SetAndNotify(ref _Size, value);
        }
        #endregion
        #region Alignment
        // Moral alignment of the Creature. (e.g. Chaotic Neutral, Lawful Good)
        private string _Alignment;
        [XmlSaveMode(XSME.Single)]
        public string Alignment
        {
            get => _Alignment;
            set => SetAndNotify(ref _Alignment, value);
        }
        #endregion
        #region Environments
        private ObservableCollection<ConvertibleValue> _Environments;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Environments
        {
            get => _Environments;
            set => SetAndNotify(ref _Environments, value);
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
        #region Counters
        private ObservableCollection<CounterModel> _Counters;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CounterModel> Counters
        {
            get => _Counters;
            set => SetAndNotify(ref _Counters, value);
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
        #region ItemLinks
        private ObservableCollection<ItemLink> _ItemLinks;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemLink> ItemLinks
        {
            get => _ItemLinks;
            set => SetAndNotify(ref _ItemLinks, value);
        }
        #endregion
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode(XSME.Single)]
        public bool IsActive
        {
            get => _IsActive;
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
            get => _TrackerSearchMatch;
            set => SetAndNotify(ref _TrackerSearchMatch, value);
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
        #region CoinDrop
        private int _CoinDrop;
        [XmlSaveMode(XSME.Single)]
        public int CoinDrop
        {
            get => _CoinDrop;
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
            get => _ProcessedValue;
            set => SetAndNotify(ref _ProcessedValue, value);
        }
        #endregion
        #region HasBeenLooted
        private bool _HasBeenLooted;
        [XmlSaveMode(XSME.Single)]
        public bool HasBeenLooted
        {
            get => _HasBeenLooted;
            set => SetAndNotify(ref _HasBeenLooted, value);
        }
        #endregion
        #region IsConcentrating
        private bool _IsConcentrating;
        [XmlSaveMode(XSME.Single)]
        public bool IsConcentrating
        {
            get => _IsConcentrating;
            set => SetAndNotify(ref _IsConcentrating, value);
        }
        #endregion
        #region IsTarget
        private bool _IsTarget;
        public bool IsTarget
        {
            get => _IsTarget;
            set => SetAndNotify(ref _IsTarget, value);
        }
        #endregion
        #region HasGroupSaveAdvantage
        private bool _HasGroupSaveAdvantage;
        public bool HasGroupSaveAdvantage
        {
            get => _HasGroupSaveAdvantage;
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
            get => _HasGroupSaveDisadvantage;
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
            get => _QuantityToAdd;
            set => SetAndNotify(ref _QuantityToAdd, value);
        }
        #endregion
        #region TamingProgress
        private int _TamingProgress;
        [XmlSaveMode(XSME.Single)]
        public int TamingProgress
        {
            get => _TamingProgress;
            set => SetAndNotify(ref _TamingProgress, value);
        }
        #endregion

        // Databound Properties - Other
        #region HitDiceQuantity
        private int _HitDiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int HitDiceQuantity
        {
            get => _HitDiceQuantity;
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
        [XmlSaveMode(XSME.Single)]
        public int HitDiceQuality
        {
            get => _HitDiceQuality;
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
        [XmlSaveMode(XSME.Single)]
        public int HitDiceModifier
        {
            get => _HitDiceModifier;
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
            get => _AverageHitPoints;
            set => SetAndNotify(ref _AverageHitPoints, value);
        }
        #endregion

        #region ChallengeRating
        private string _ChallengeRating;
        [XmlSaveMode(XSME.Single)]
        public string ChallengeRating
        {
            get => _ChallengeRating;
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
            get => _ExperienceValue;
            set => SetAndNotify(ref _ExperienceValue, value);
        }
        #endregion

        #region ArmorClass
        private int _ArmorClass;
        [XmlSaveMode(XSME.Single)]
        public int ArmorClass
        {
            get => _ArmorClass;
            set => SetAndNotify(ref _ArmorClass, value);
        }
        #endregion
        #region ArmorType
        private string _ArmorType;
        [XmlSaveMode(XSME.Single)]
        public string ArmorType
        {
            get => _ArmorType;
            set => SetAndNotify(ref _ArmorType, value);
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
        #region FlySpeed
        private int _FlySpeed;
        [XmlSaveMode(XSME.Single)]
        public int FlySpeed
        {
            get => _FlySpeed;
            set => SetAndNotify(ref _FlySpeed, value);
        }
        #endregion
        #region ClimbSpeed
        private int _ClimbSpeed;
        [XmlSaveMode(XSME.Single)]
        public int ClimbSpeed
        {
            get => _ClimbSpeed;
            set => SetAndNotify(ref _ClimbSpeed, value);
        }
        #endregion
        #region BurrowSpeed
        private int _BurrowSpeed;
        [XmlSaveMode(XSME.Single)]
        public int BurrowSpeed
        {
            get => _BurrowSpeed;
            set => SetAndNotify(ref _BurrowSpeed, value);
        }
        #endregion
        #region SwimSpeed
        private int _SwimSpeed;
        [XmlSaveMode(XSME.Single)]
        public int SwimSpeed
        {
            get => _SwimSpeed;
            set => SetAndNotify(ref _SwimSpeed, value);
        }
        #endregion
        #region HighestSpeedType
        private string _HighestSpeedType;
        public string HighestSpeedType
        {
            get => _HighestSpeedType;
            set => SetAndNotify(ref _HighestSpeedType, value);
        }
        #endregion
        #region HighestSpeedValue
        private int _HighestSpeedValue;
        public int HighestSpeedValue
        {
            get => _HighestSpeedValue;
            set => SetAndNotify(ref _HighestSpeedValue, value);
        }
        #endregion

        #region ProficiencyBonus
        private int _ProficiencyBonus;
        public int ProficiencyBonus
        {
            get => _ProficiencyBonus;
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
        [XmlSaveMode(XSME.Single)]
        public int PassiveInsight
        {
            get => _PassiveInsight;
            set => SetAndNotify(ref _PassiveInsight, value);
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
        #region Darkvision
        private int _Darkvision;
        [XmlSaveMode(XSME.Single)]
        public int Darkvision
        {
            get => _Darkvision;
            set => SetAndNotify(ref _Darkvision, value);
        }
        #endregion
        #region Blindsight
        private int _Blindsight;
        [XmlSaveMode(XSME.Single)]
        public int Blindsight
        {
            get => _Blindsight;
            set => SetAndNotify(ref _Blindsight, value);
        }
        #endregion
        #region Truesight
        private int _Truesight;
        [XmlSaveMode(XSME.Single)]
        public int Truesight
        {
            get => _Truesight;
            set => SetAndNotify(ref _Truesight, value);
        }
        #endregion
        #region Tremorsense
        private int _Tremorsense;
        [XmlSaveMode(XSME.Single)]
        public int Tremorsense
        {
            get => _Tremorsense;
            set => SetAndNotify(ref _Tremorsense, value);
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
        #region Lore
        private string _Lore;
        [XmlSaveMode(XSME.Single)]
        public string Lore
        {
            get => _Lore;
            set => SetAndNotify(ref _Lore, value);
        }
        #endregion
        #region SpellcastingAbilities
        private List<string> _SpellcastingAbilities;
        public List<string> SpellcastingAbilities
        {
            get => _SpellcastingAbilities;
            set => SetAndNotify(ref _SpellcastingAbilities, value);
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
                UpdateModifiers();
            }
        }
        #endregion
        #region SpellSaveDc
        private int _SpellSaveDc;
        public int SpellSaveDc
        {
            get => _SpellSaveDc;
            set => SetAndNotify(ref _SpellSaveDc, value);
        }
        #endregion
        #region SpellAttackBonus
        private int _SpellAttackBonus;
        public int SpellAttackBonus
        {
            get => _SpellAttackBonus;
            set => SetAndNotify(ref _SpellAttackBonus, value);
        }
        #endregion
        #region SpellAbilityModifier
        private int _SpellAbilityModifier;
        public int SpellAbilityModifier
        {
            get => _SpellAbilityModifier;
            set => SetAndNotify(ref _SpellAbilityModifier, value);
        }
        #endregion
        #region IsInnateSpellcaster
        private bool _IsInnateSpellcaster;
        [XmlSaveMode(XSME.Single)]
        public bool IsInnateSpellcaster
        {
            get => _IsInnateSpellcaster;
            set => SetAndNotify(ref _IsInnateSpellcaster, value);
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
        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<TraitModel> Traits
        {
            get => _Traits;
            set => SetAndNotify(ref _Traits, value);
        }
        #endregion
        #region Traits_P1
        private ObservableCollection<TraitModel> _Traits_P1;
        public ObservableCollection<TraitModel> Traits_P1
        {
            get => _Traits_P1;
            set => SetAndNotify(ref _Traits_P1, value);
        }
        #endregion
        #region Traits_P2
        private ObservableCollection<TraitModel> _Traits_P2;
        public ObservableCollection<TraitModel> Traits_P2
        {
            get => _Traits_P2;
            set => SetAndNotify(ref _Traits_P2, value);
        }
        #endregion
        #region TraitEditModeEnabled
        private bool _TraitEditModeEnabled;
        public bool TraitEditModeEnabled
        {
            get => _TraitEditModeEnabled;
            set => SetAndNotify(ref _TraitEditModeEnabled, value);
        }
        #endregion

        // Databound Properties - Damage Type Proclivities
        #region Vulnerabilities
        private string _Vulnerabilities;
        public string Vulnerabilities
        {
            get => _Vulnerabilities;
            set => SetAndNotify(ref _Vulnerabilities, value);
        }
        #endregion
        #region Resistances
        private string _Resistances;
        public string Resistances
        {
            get => _Resistances;
            set => SetAndNotify(ref _Resistances, value);
        }
        #endregion
        #region Immunities
        private string _Immunities;
        public string Immunities
        {
            get => _Immunities;
            set => SetAndNotify(ref _Immunities, value);
        }
        #endregion
        #region ConditionImmunities
        private string _ConditionImmunities;
        public string ConditionImmunities
        {
            get => _ConditionImmunities;
            set => SetAndNotify(ref _ConditionImmunities, value);
        }
        #endregion

        #region DamageProclivity_Acid
        private string _DamageProclivity_Acid;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Acid
        {
            get => _DamageProclivity_Acid;
            set => SetAndNotify(ref _DamageProclivity_Acid, value);
        }
        #endregion
        #region DamageProclivity_Cold
        private string _DamageProclivity_Cold;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Cold
        {
            get => _DamageProclivity_Cold;
            set => SetAndNotify(ref _DamageProclivity_Cold, value);
        }
        #endregion
        #region DamageProclivity_Fire
        private string _DamageProclivity_Fire;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Fire
        {
            get => _DamageProclivity_Fire;
            set => SetAndNotify(ref _DamageProclivity_Fire, value);
        }
        #endregion
        #region DamageProclivity_Force
        private string _DamageProclivity_Force;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Force
        {
            get => _DamageProclivity_Force;
            set => SetAndNotify(ref _DamageProclivity_Force, value);
        }
        #endregion
        #region DamageProclivity_Lightning
        private string _DamageProclivity_Lightning;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Lightning
        {
            get => _DamageProclivity_Lightning;
            set => SetAndNotify(ref _DamageProclivity_Lightning, value);
        }
        #endregion
        #region DamageProclivity_Necrotic
        private string _DamageProclivity_Necrotic;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Necrotic
        {
            get => _DamageProclivity_Necrotic;
            set => SetAndNotify(ref _DamageProclivity_Necrotic, value);
        }
        #endregion
        #region DamageProclivity_Poison
        private string _DamageProclivity_Poison;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Poison
        {
            get => _DamageProclivity_Poison;
            set => SetAndNotify(ref _DamageProclivity_Poison, value);
        }
        #endregion
        #region DamageProclivity_Psychic
        private string _DamageProclivity_Psychic;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Psychic
        {
            get => _DamageProclivity_Psychic;
            set => SetAndNotify(ref _DamageProclivity_Psychic, value);
        }
        #endregion
        #region DamageProclivity_Radiant
        private string _DamageProclivity_Radiant;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Radiant
        {
            get => _DamageProclivity_Radiant;
            set => SetAndNotify(ref _DamageProclivity_Radiant, value);
        }
        #endregion
        #region DamageProclivity_Thunder
        private string _DamageProclivity_Thunder;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Thunder
        {
            get => _DamageProclivity_Thunder;
            set => SetAndNotify(ref _DamageProclivity_Thunder, value);
        }
        #endregion
        #region DamageProclivity_Slashing
        private string _DamageProclivity_Slashing;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Slashing
        {
            get => _DamageProclivity_Slashing;
            set => SetAndNotify(ref _DamageProclivity_Slashing, value);
        }
        #endregion
        #region DamageProclivity_Piercing
        private string _DamageProclivity_Piercing;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Piercing
        {
            get => _DamageProclivity_Piercing;
            set => SetAndNotify(ref _DamageProclivity_Piercing, value);
        }
        #endregion
        #region DamageProclivity_Bludgeoning
        private string _DamageProclivity_Bludgeoning;
        [XmlSaveMode(XSME.Single)]
        public string DamageProclivity_Bludgeoning
        {
            get => _DamageProclivity_Bludgeoning;
            set => SetAndNotify(ref _DamageProclivity_Bludgeoning, value);
        }
        #endregion

        #region IsImmune_Blinded
        private bool _IsImmune_Blinded;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Blinded
        {
            get => _IsImmune_Blinded;
            set => SetAndNotify(ref _IsImmune_Blinded, value);
        }
        #endregion
        #region IsImmune_Charmed
        private bool _IsImmune_Charmed;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Charmed
        {
            get => _IsImmune_Charmed;
            set => SetAndNotify(ref _IsImmune_Charmed, value);
        }
        #endregion
        #region IsImmune_Deafened
        private bool _IsImmune_Deafened;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Deafened
        {
            get => _IsImmune_Deafened;
            set => SetAndNotify(ref _IsImmune_Deafened, value);
        }
        #endregion
        #region IsImmune_Exhaustion
        private bool _IsImmune_Exhaustion;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Exhaustion
        {
            get => _IsImmune_Exhaustion;
            set => SetAndNotify(ref _IsImmune_Exhaustion, value);
        }
        #endregion
        #region IsImmune_Frightened
        private bool _IsImmune_Frightened;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Frightened
        {
            get => _IsImmune_Frightened;
            set => SetAndNotify(ref _IsImmune_Frightened, value);
        }
        #endregion
        #region IsImmune_Grappled
        private bool _IsImmune_Grappled;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Grappled
        {
            get => _IsImmune_Grappled;
            set => SetAndNotify(ref _IsImmune_Grappled, value);
        }
        #endregion
        #region IsImmune_Paralyzed
        private bool _IsImmune_Paralyzed;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Paralyzed
        {
            get => _IsImmune_Paralyzed;
            set => SetAndNotify(ref _IsImmune_Paralyzed, value);
        }
        #endregion
        #region IsImmune_Petrified
        private bool _IsImmune_Petrified;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Petrified
        {
            get => _IsImmune_Petrified;
            set => SetAndNotify(ref _IsImmune_Petrified, value);
        }
        #endregion
        #region IsImmune_Poisoned
        private bool _IsImmune_Poisoned;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Poisoned
        {
            get => _IsImmune_Poisoned;
            set => SetAndNotify(ref _IsImmune_Poisoned, value);
        }
        #endregion
        #region IsImmune_Prone
        private bool _IsImmune_Prone;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Prone
        {
            get => _IsImmune_Prone;
            set => SetAndNotify(ref _IsImmune_Prone, value);
        }
        #endregion
        #region IsImmune_Restrained
        private bool _IsImmune_Restrained;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Restrained
        {
            get => _IsImmune_Restrained;
            set => SetAndNotify(ref _IsImmune_Restrained, value);
        }
        #endregion
        #region IsImmune_Stunned
        private bool _IsImmune_Stunned;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Stunned
        {
            get => _IsImmune_Stunned;
            set => SetAndNotify(ref _IsImmune_Stunned, value);
        }
        #endregion
        #region IsImmune_Unconscious
        private bool _IsImmune_Unconscious;
        [XmlSaveMode(XSME.Single)]
        public bool IsImmune_Unconscious
        {
            get => _IsImmune_Unconscious;
            set => SetAndNotify(ref _IsImmune_Unconscious, value);
        }
        #endregion

        // Databound Properties - Attributes
        #region Attr_Strength
        private int _Attr_Strength;
        [XmlSaveMode(XSME.Single)]
        public int Attr_Strength
        {
            get => _Attr_Strength;
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
                    if (IsADepedency()) { return; }
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
            if (parts.Length > 1)
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
            if (parts.Length > 1)
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
            if (parts.Length > 1)
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
            HelperMethods.NotifyUser(message);
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
                HelperMethods.NotifyUser(Name + " has become hostile, please inform the DM.");
            }

            if (TamingProgress >= ExperienceValue)
            {
                character.Minions.Add(HelperMethods.DeepClone(this));
                character.CreaturePen.Remove(this);
                HelperMethods.NotifyUser(Name + " has become tamed and is now in the Combat - Minions section.");
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
        #region SortAbilities
        public ICommand SortAbilities => new RelayCommand(DoSortAbilities);
        private void DoSortAbilities(object param)
        {
            Abilities = new(Abilities.OrderBy(a => a.Name));
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

            if (totalHealth == 0) { totalHealth = 1; }
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
            UpdateDamageProclivityTexts();
            UpdateImmuneConditionText();

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
                ability.UpdateDropdownSuggestedValues();
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
        private bool IsADepedency()
        {
            List<string> foundDependencies = new();
            foreach (GameCampaign campaign in Configuration.MainModelRef.CampaignView.Campaigns)
            {
                foreach (CreatureModel creature in campaign.Combatants)
                {
                    if (creature.Name == this.Name) { foundDependencies.Add("Combatant in campaign " + campaign.Name); }
                }
                foreach (NpcModel npc in campaign.Npcs)
                {
                    if (npc.BaseCreatureName == this.Name) { foundDependencies.Add("Base creature for NPC " + npc.Name + " in campaign " + campaign.Name); }
                }
                foreach (CreaturePackModel creaturePack in campaign.Packs)
                {
                    foreach (PackCreatureModel packCreature in creaturePack.CreatureList)
                    {
                        if (packCreature.CreatureName == this.Name) { foundDependencies.Add("Pack creature for " + creaturePack.Name + " in campaign " + campaign.Name); }
                    }
                }
            }
            foreach (CharacterModel character in Configuration.MainModelRef.CharacterBuilderView.Characters)
            {
                foreach (CreatureModel minion in character.Minions)
                {
                    if (minion.Name == this.Name) { foundDependencies.Add("Minion for " + character.Name); }
                }
            }
            if (foundDependencies.Count > 0)
            {
                string message = "Unable to delete " + this.Name + ", dependencies found:\n";
                message += HelperMethods.GetStringFromList(foundDependencies, "\n");
                HelperMethods.NotifyUser(message, HelperMethods.UserNotificationType.Report);
                return true;
            }
            return false;
        }



    }
}
