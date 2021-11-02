using GAMMA.Models.GameplayComponents;
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
    public class SpellModel : BaseModel
    {
        // Constructors
        public SpellModel()
        {
            Name = "New Spell";
            ConsumedMaterials = new ObservableCollection<ItemModel>();
            ActiveEffects = new ObservableCollection<ActiveEffectModel>();
            SpellClasses = new ObservableCollection<ConvertibleValue>();
            NumberOfAttacks = 1;
            AttackDamageScale = 1;
            AttackTargetScale = 1;
            HealingScale = 1;
            SaveDamageScale = 1;
            PrimaryAbilities = new();
            SecondaryAbilities = new();
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
        #region SpellClasses
        private ObservableCollection<ConvertibleValue> _SpellClasses;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ConvertibleValue> SpellClasses
        {
            get
            {
                return _SpellClasses;
            }
            set
            {
                _SpellClasses = value;
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

        #region SchoolOfMagic
        private string _SchoolOfMagic;
        [XmlSaveMode("Single")]
        public string SchoolOfMagic
        {
            get
            {
                return _SchoolOfMagic;
            }
            set
            {
                _SchoolOfMagic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CastingTime
        private string _CastingTime;
        [XmlSaveMode("Single")]
        public string CastingTime
        {
            get
            {
                return _CastingTime;
            }
            set
            {
                _CastingTime = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellDuration
        private string _SpellDuration;
        [XmlSaveMode("Single")]
        public string SpellDuration
        {
            get
            {
                return _SpellDuration;
            }
            set
            {
                _SpellDuration = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellLevel
        private int _SpellLevel;
        [XmlSaveMode("Single")]
        public int SpellLevel
        {
            get
            {
                return _SpellLevel;
            }
            set
            {
                _SpellLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Range
        private string _Range;
        [XmlSaveMode("Single")]
        public string Range
        {
            get
            {
                return _Range;
            }
            set
            {
                _Range = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasAreaOfEffect
        private bool _HasAreaOfEffect;
        [XmlSaveMode("Single")]
        public bool HasAreaOfEffect
        {
            get
            {
                return _HasAreaOfEffect;
            }
            set
            {
                _HasAreaOfEffect = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeRange
        private int _AoeRange;
        [XmlSaveMode("Single")]
        public int AoeRange
        {
            get
            {
                return _AoeRange;
            }
            set
            {
                _AoeRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeRange2
        private int _AoeRange2;
        [XmlSaveMode("Proto")]
        public int AoeRange2
        {
            get
            {
                return _AoeRange2;
            }
            set
            {
                _AoeRange2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeShape
        private string _AoeShape;
        [XmlSaveMode("Single")]
        public string AoeShape
        {
            get
            {
                return _AoeShape;
            }
            set
            {
                _AoeShape = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region IsPrepared
        private bool _IsPrepared;
        [XmlSaveMode("Single")]
        public bool IsPrepared
        {
            get
            {
                return _IsPrepared;
            }
            set
            {
                _IsPrepared = value;
                NotifyPropertyChanged();

                //if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                //if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
                //Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdatePreparedSpellCount();

            }
        }
        #endregion
        #region HasScaling
        // Indicates if the spell should show a list of higher level casting options
        private bool _HasScaling;
        [XmlSaveMode("Single")]
        public bool HasScaling
        {
            get
            {
                return _HasScaling;
            }
            set
            {
                _HasScaling = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RitualCapable
        private bool _RitualCapable;
        [XmlSaveMode("Single")]
        public bool RitualCapable
        {
            get
            {
                return _RitualCapable;
            }
            set
            {
                _RitualCapable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresConcentration
        private bool _RequiresConcentration;
        [XmlSaveMode("Single")]
        public bool RequiresConcentration
        {
            get
            {
                return _RequiresConcentration;
            }
            set
            {
                _RequiresConcentration = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresSomatic
        private bool _RequiresSomatic;
        [XmlSaveMode("Single")]
        public bool RequiresSomatic
        {
            get
            {
                return _RequiresSomatic;
            }
            set
            {
                _RequiresSomatic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresVerbal
        private bool _RequiresVerbal;
        [XmlSaveMode("Single")]
        public bool RequiresVerbal
        {
            get
            {
                return _RequiresVerbal;
            }
            set
            {
                _RequiresVerbal = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasAttackStat
        private bool _HasAttackStat;
        [XmlSaveMode("Single")]
        public bool HasAttackStat
        {
            get
            {
                return _HasAttackStat;
            }
            set
            {
                _HasAttackStat = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsAutoHit
        private bool _IsAutoHit;
        [XmlSaveMode("Single")]
        public bool IsAutoHit
        {
            get
            {
                return _IsAutoHit;
            }
            set
            {
                _IsAutoHit = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageDiceQuantity
        private int _DamageDiceQuantity;
        [XmlSaveMode("Single")]
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
        [XmlSaveMode("Single")]
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
        #region DamageDiceModifier
        private int _DamageDiceModifier;
        [XmlSaveMode("Single")]
        public int DamageDiceModifier
        {
            get
            {
                return _DamageDiceModifier;
            }
            set
            {
                _DamageDiceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region UsesSpellcastingMod_Damage
        private bool _UsesSpellcastingMod_Damage;
        [XmlSaveMode("Single")]
        public bool UsesSpellcastingMod_Damage
        {
            get
            {
                return _UsesSpellcastingMod_Damage;
            }
            set
            {
                _UsesSpellcastingMod_Damage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageType
        private string _DamageType;
        [XmlSaveMode("Single")]
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
        #region NumberOfAttacks
        private int _NumberOfAttacks;
        [XmlSaveMode("Single")]
        public int NumberOfAttacks
        {
            get
            {
                return _NumberOfAttacks;
            }
            set
            {
                _NumberOfAttacks = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasTargetScaling
        private bool _HasTargetScaling;
        [XmlSaveMode("Single")]
        public bool HasTargetScaling
        {
            get
            {
                return _HasTargetScaling;
            }
            set
            {
                _HasTargetScaling = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttackTargetScale
        private int _AttackTargetScale;
        [XmlSaveMode("Single")]
        public int AttackTargetScale
        {
            get
            {
                return _AttackTargetScale;
            }
            set
            {
                _AttackTargetScale = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasAttackDamageScaling
        private bool _HasAttackDamageScaling;
        [XmlSaveMode("Single")]
        public bool HasAttackDamageScaling
        {
            get
            {
                return _HasAttackDamageScaling;
            }
            set
            {
                _HasAttackDamageScaling = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttackDamageScale
        private int _AttackDamageScale;
        [XmlSaveMode("Single")]
        public int AttackDamageScale
        {
            get
            {
                return _AttackDamageScale;
            }
            set
            {
                _AttackDamageScale = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasAltAttackDamage
        private bool _HasAltAttackDamage;
        [XmlSaveMode("Single")]
        public bool HasAltAttackDamage
        {
            get
            {
                return _HasAltAttackDamage;
            }
            set
            {
                _HasAltAttackDamage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltDamageDiceQuantity
        private int _AltDamageDiceQuantity;
        [XmlSaveMode("Single")]
        public int AltDamageDiceQuantity
        {
            get
            {
                return _AltDamageDiceQuantity;
            }
            set
            {
                _AltDamageDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltDamageDiceQuality
        private int _AltDamageDiceQuality;
        [XmlSaveMode("Single")]
        public int AltDamageDiceQuality
        {
            get
            {
                return _AltDamageDiceQuality;
            }
            set
            {
                _AltDamageDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltDamageDiceCondition
        private string _AltDamageDiceCondition;
        [XmlSaveMode("Single")]
        public string AltDamageDiceCondition
        {
            get
            {
                return _AltDamageDiceCondition;
            }
            set
            {
                _AltDamageDiceCondition = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasSaveDamageScaling
        private bool _HasSaveDamageScaling;
        [XmlSaveMode("Single")]
        public bool HasSaveDamageScaling
        {
            get
            {
                return _HasSaveDamageScaling;
            }
            set
            {
                _HasSaveDamageScaling = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageScale
        private int _SaveDamageScale;
        [XmlSaveMode("Single")]
        public int SaveDamageScale
        {
            get
            {
                return _SaveDamageScale;
            }
            set
            {
                _SaveDamageScale = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasHealingScaling
        private bool _HasHealingScaling;
        [XmlSaveMode("Single")]
        public bool HasHealingScaling
        {
            get
            {
                return _HasHealingScaling;
            }
            set
            {
                _HasHealingScaling = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HealingScale
        private int _HealingScale;
        [XmlSaveMode("Single")]
        public int HealingScale
        {
            get
            {
                return _HealingScale;
            }
            set
            {
                _HealingScale = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasHealingStat
        private bool _HasHealingStat;
        [XmlSaveMode("Single")]
        public bool HasHealingStat
        {
            get
            {
                return _HasHealingStat;
            }
            set
            {
                _HasHealingStat = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HealingDiceQuantity
        private int _HealingDiceQuantity;
        [XmlSaveMode("Single")]
        public int HealingDiceQuantity
        {
            get
            {
                return _HealingDiceQuantity;
            }
            set
            {
                _HealingDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HealingDiceQuality
        private int _HealingDiceQuality;
        [XmlSaveMode("Single")]
        public int HealingDiceQuality
        {
            get
            {
                return _HealingDiceQuality;
            }
            set
            {
                _HealingDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HealingDiceModifier
        private int _HealingDiceModifier;
        [XmlSaveMode("Single")]
        public int HealingDiceModifier
        {
            get
            {
                return _HealingDiceModifier;
            }
            set
            {
                _HealingDiceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region UsesSpellcastingMod_Healing
        private bool _UsesSpellcastingMod_Healing;
        [XmlSaveMode("Single")]
        public bool UsesSpellcastingMod_Healing
        {
            get
            {
                return _UsesSpellcastingMod_Healing;
            }
            set
            {
                _UsesSpellcastingMod_Healing = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasAltHealingDice
        private bool _HasAltHealingDice;
        [XmlSaveMode("Single")]
        public bool HasAltHealingDice
        {
            get
            {
                return _HasAltHealingDice;
            }
            set
            {
                _HasAltHealingDice = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltHealingDiceQuantity
        private int _AltHealingDiceQuantity;
        [XmlSaveMode("Single")]
        public int AltHealingDiceQuantity
        {
            get
            {
                return _AltHealingDiceQuantity;
            }
            set
            {
                _AltHealingDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltHealingDiceQuality
        private int _AltHealingDiceQuality;
        [XmlSaveMode("Single")]
        public int AltHealingDiceQuality
        {
            get
            {
                return _AltHealingDiceQuality;
            }
            set
            {
                _AltHealingDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltHealingDiceCondition
        private string _AltHealingDiceCondition;
        [XmlSaveMode("Single")]
        public string AltHealingDiceCondition
        {
            get
            {
                return _AltHealingDiceCondition;
            }
            set
            {
                _AltHealingDiceCondition = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasSaveStat
        private bool _HasSaveStat;
        [XmlSaveMode("Single")]
        public bool HasSaveStat
        {
            get
            {
                return _HasSaveStat;
            }
            set
            {
                _HasSaveStat = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveAbility
        private string _SaveAbility;
        [XmlSaveMode("Single")]
        public string SaveAbility
        {
            get
            {
                return _SaveAbility;
            }
            set
            {
                _SaveAbility = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageDiceQuantity
        private int _SaveDamageDiceQuantity;
        [XmlSaveMode("Single")]
        public int SaveDamageDiceQuantity
        {
            get
            {
                return _SaveDamageDiceQuantity;
            }
            set
            {
                _SaveDamageDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageDiceQuality
        private int _SaveDamageDiceQuality;
        [XmlSaveMode("Single")]
        public int SaveDamageDiceQuality
        {
            get
            {
                return _SaveDamageDiceQuality;
            }
            set
            {
                _SaveDamageDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageDiceModifier
        private int _SaveDamageDiceModifier;
        [XmlSaveMode("Single")]
        public int SaveDamageDiceModifier
        {
            get
            {
                return _SaveDamageDiceModifier;
            }
            set
            {
                _SaveDamageDiceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region UsesSpellcastingMod_Save
        private bool _UsesSpellcastingMod_Save;
        [XmlSaveMode("Single")]
        public bool UsesSpellcastingMod_Save
        {
            get
            {
                return _UsesSpellcastingMod_Save;
            }
            set
            {
                _UsesSpellcastingMod_Save = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageType
        private string _SaveDamageType;
        [XmlSaveMode("Single")]
        public string SaveDamageType
        {
            get
            {
                return _SaveDamageType;
            }
            set
            {
                _SaveDamageType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsHalfDamageOnSave
        private bool _IsHalfDamageOnSave;
        [XmlSaveMode("Single")]
        public bool IsHalfDamageOnSave
        {
            get
            {
                return _IsHalfDamageOnSave;
            }
            set
            {
                _IsHalfDamageOnSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveEffect
        private string _SaveEffect;
        [XmlSaveMode("Single")]
        public string SaveEffect
        {
            get
            {
                return _SaveEffect;
            }
            set
            {
                _SaveEffect = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasAltSaveDamage
        private bool _HasAltSaveDamage;
        [XmlSaveMode("Single")]
        public bool HasAltSaveDamage
        {
            get
            {
                return _HasAltSaveDamage;
            }
            set
            {
                _HasAltSaveDamage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltSaveDamageDiceQuantity
        private int _AltSaveDamageDiceQuantity;
        [XmlSaveMode("Single")]
        public int AltSaveDamageDiceQuantity
        {
            get
            {
                return _AltSaveDamageDiceQuantity;
            }
            set
            {
                _AltSaveDamageDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltSaveDamageDiceQuality
        private int _AltSaveDamageDiceQuality;
        [XmlSaveMode("Single")]
        public int AltSaveDamageDiceQuality
        {
            get
            {
                return _AltSaveDamageDiceQuality;
            }
            set
            {
                _AltSaveDamageDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AltSaveDamageCondition
        private string _AltSaveDamageCondition;
        [XmlSaveMode("Single")]
        public string AltSaveDamageCondition
        {
            get
            {
                return _AltSaveDamageCondition;
            }
            set
            {
                _AltSaveDamageCondition = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasSpecialEffect
        private bool _HasSpecialEffect;
        [XmlSaveMode("Single")]
        public bool HasSpecialEffect
        {
            get
            {
                return _HasSpecialEffect;
            }
            set
            {
                _HasSpecialEffect = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpecialEffectUsesDescription
        private bool _SpecialEffectUsesDescription;
        [XmlSaveMode("Single")]
        public bool SpecialEffectUsesDescription
        {
            get
            {
                return _SpecialEffectUsesDescription;
            }
            set
            {
                _SpecialEffectUsesDescription = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpecialEffect
        private string _SpecialEffect;
        [XmlSaveMode("Single")]
        public string SpecialEffect
        {
            get
            {
                return _SpecialEffect;
            }
            set
            {
                _SpecialEffect = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region RequiresMaterial
        private bool _RequiresMaterial;
        [XmlSaveMode("Single")]
        public bool RequiresMaterial
        {
            get
            {
                return _RequiresMaterial;
            }
            set
            {
                _RequiresMaterial = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DoesConsumeMaterials
        private bool _DoesConsumeMaterials;
        [XmlSaveMode("Single")]
        public bool DoesConsumeMaterials
        {
            get
            {
                return _DoesConsumeMaterials;
            }
            set
            {
                _DoesConsumeMaterials = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConsumedMaterials
        private ObservableCollection<ItemModel> _ConsumedMaterials;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemModel> ConsumedMaterials
        {
            get
            {
                return _ConsumedMaterials;
            }
            set
            {
                _ConsumedMaterials = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Materials
        private string _Materials;
        [XmlSaveMode("Single")]
        public string Materials
        {
            get
            {
                return _Materials;
            }
            set
            {
                _Materials = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region DoesCreateActiveEffects
        private bool _DoesCreateActiveEffects;
        [XmlSaveMode("Single")]
        public bool DoesCreateActiveEffects
        {
            get
            {
                return _DoesCreateActiveEffects;
            }
            set
            {
                _DoesCreateActiveEffects = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveEffects
        private ObservableCollection<ActiveEffectModel> _ActiveEffects;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ActiveEffectModel> ActiveEffects
        {
            get
            {
                return _ActiveEffects;
            }
            set
            {
                _ActiveEffects = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PrimaryAbilities
        private ObservableCollection<CustomAbility> _PrimaryAbilities;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomAbility> PrimaryAbilities
        {
            get
            {
                return _PrimaryAbilities;
            }
            set
            {
                _PrimaryAbilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SecondaryAbilities
        private ObservableCollection<CustomAbility> _SecondaryAbilities;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomAbility> SecondaryAbilities
        {
            get
            {
                return _SecondaryAbilities;
            }
            set
            {
                _SecondaryAbilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region SchoolsOfMagic
        private List<string> _SchoolsOfMagic;
        [XmlSaveMode("")]
        public List<string> SchoolsOfMagic
        {
            get
            {
                return _SchoolsOfMagic;
            }
            set
            {
                _SchoolsOfMagic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeShapes
        private List<string> _AoeShapes;
        [XmlSaveMode("")]
        public List<string> AoeShapes
        {
            get
            {
                return _AoeShapes;
            }
            set
            {
                _AoeShapes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageDiceSides
        private List<string> _DamageDiceSides;
        [XmlSaveMode("")]
        public List<string> DamageDiceSides
        {
            get
            {
                return _DamageDiceSides;
            }
            set
            {
                _DamageDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageTypes
        private List<string> _DamageTypes;
        [XmlSaveMode("")]
        public List<string> DamageTypes
        {
            get
            {
                return _DamageTypes;
            }
            set
            {
                _DamageTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageTypes
        private List<string> _SaveDamageTypes;
        [XmlSaveMode("")]
        public List<string> SaveDamageTypes
        {
            get
            {
                return _SaveDamageTypes;
            }
            set
            {
                _SaveDamageTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HealingDiceSides
        private List<string> _HealingDiceSides;
        [XmlSaveMode("")]
        public List<string> HealingDiceSides
        {
            get
            {
                return _HealingDiceSides;
            }
            set
            {
                _HealingDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveAttributes
        private List<string> _SaveAttributes;
        [XmlSaveMode("")]
        public List<string> SaveAttributes
        {
            get
            {
                return _SaveAttributes;
            }
            set
            {
                _SaveAttributes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageDiceSides
        private List<string> _SaveDamageDiceSides;
        [XmlSaveMode("")]
        public List<string> SaveDamageDiceSides
        {
            get
            {
                return _SaveDamageDiceSides;
            }
            set
            {
                _SaveDamageDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Display
        #region DisplayFieldSet_DndSpell
        private bool _DisplayFieldSet_DndSpell;
        public bool DisplayFieldSet_DndSpell
        {
            get
            {
                return _DisplayFieldSet_DndSpell;
            }
            set
            {
                _DisplayFieldSet_DndSpell = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveSpellFromCreature
        private RelayCommand _RemoveSpellFromCreature;
        public ICommand RemoveSpellFromCreature
        {
            get
            {
                if (_RemoveSpellFromCreature == null)
                {
                    _RemoveSpellFromCreature = new RelayCommand(param => DoRemoveSpellFromCreature(this));
                }
                return _RemoveSpellFromCreature;
            }
        }
        private void DoRemoveSpellFromCreature(SpellModel spellToRemove)
        {
            Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Spells.Remove(spellToRemove);
        }
        #endregion
        #region RemoveSpellFromPlayer
        private RelayCommand _RemoveSpellFromPlayer;
        public ICommand RemoveSpellFromPlayer
        {
            get
            {
                if (_RemoveSpellFromPlayer == null)
                {
                    _RemoveSpellFromPlayer = new RelayCommand(param => DoRemoveSpellFromPlayer(this));
                }
                return _RemoveSpellFromPlayer;
            }
        }
        private void DoRemoveSpellFromPlayer(SpellModel spellToRemove)
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Spells.Remove(spellToRemove);
        }
        #endregion
        #region CastSpellAbility
        public ICommand CastSpellAbility => new RelayCommand(DoCastSpellAbility);
        private void DoCastSpellAbility(object param)
        {
            CreatureModel creature = null;
            CharacterModel character = null;
            CustomAbility abilityToUse = null;
            string message = "";
            string name = "";
            string mode = "Normal";
            int castingLevel = 0;
            bool castAsRitual = false;

            // Pre-Cast Check
            //if ((param as object[]) == null) { HelperMethods.NotifyUser("Invalid spellcasting parameter."); return; } // Player base level casting doesn't use multi-binding
            if (PrimaryAbilities.Count() == 0) { HelperMethods.NotifyUser("No abilities available for this spell, please verify data."); return; }

            // Creature / Character Retrieval
            if ((param as object[]) == null)
            {
                if (param.GetType() == typeof(CreatureModel))
                {
                    creature = param as CreatureModel;
                    creature.DisplayPopup_Spells = false;
                    creature.DisplayPopupAlt_Spells = false;
                    name = creature.DisplayName;
                }
                else
                {
                    character = (param as MainViewModel).CharacterBuilderView.ActiveCharacter;
                    name = character.Name;
                }
                castingLevel = SpellLevel;
            }
            else
            {
                if ((param as object[])[0].GetType() == typeof(CreatureModel))
                {
                    creature = (param as object[])[0] as CreatureModel;
                    creature.DisplayPopup_Spells = false;
                    creature.DisplayPopupAlt_Spells = false;
                    name = creature.DisplayName;
                }
                else
                {
                    character = ((param as object[])[0] as MainViewModel).CharacterBuilderView.ActiveCharacter;
                    name = character.Name;
                }

                if (int.TryParse((param as object[])[1].ToString(), out castingLevel) == false)
                {
                    if ((param as object[])[1].ToString() == "R") { castAsRitual = true; castingLevel = SpellLevel; }
                }

                if ((param as object[]).Length == 3)
                {
                    mode = (param as object[])[2].ToString();
                }

            }

            // Spell Slot Check
            if (castAsRitual == false)
            {
                if (character != null)
                {
                    if (HelperMethods.CheckForSpellSlots(ref character, castingLevel) == false)
                    {
                        HelperMethods.AddToPlayerLog("Insufficient spell slots to cast " + Name + ".");
                        return;
                    }
                }
                if (creature != null && Configuration.MainModelRef.SettingsView.EnforceCreatureSpellSlots == true)
                {
                    if (creature.IsInnateSpellcaster == false)
                    {
                        if (HelperMethods.CheckForSpellSlots(ref creature, castingLevel) == false)
                        {
                            if (Configuration.MainModelRef.TabSelected_Campaigns)
                            {
                                HelperMethods.AddToCampaignMessages(creature.DisplayName + " has insufficient spell slots to cast " + Name + ".", "Spell");
                                return;
                            }
                        }
                    }
                }
            }

            if (character != null)
            {
                if (Configuration.MainModelRef.SettingsView.EnforceSpellComponentConsumption)
                {
                    if (CheckForSpellMaterials(ref character) == false)
                    {
                        HelperMethods.AddToPlayerLog("Insufficient casting materials.");
                        return;
                    }
                }

                foreach (ItemModel component in this.ConsumedMaterials)
                {
                    foreach (ItemModel backpackItem in character.Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == component.Name)
                        {
                            backpackItem.Quantity--;
                        }
                    }
                }
            }

            if (PrimaryAbilities.Count() > 1)
            {
                List<ConvertibleValue> abilityNames = new();
                foreach (CustomAbility ability in PrimaryAbilities)
                {
                    abilityNames.Add(new(ability.Name));
                }
                QAPrompt prompt = new("Which ability do you want to use?", abilityNames);
                prompt.ShowDialog();
                abilityToUse = PrimaryAbilities.First(a => a.Name == prompt.Answer);
            }
            else
            {
                abilityToUse = PrimaryAbilities.First();
            }

            // Build Initial Casting Output
            if (castAsRitual)
            {
                message += name + " casts " + Name + " as a ritual.";
            }
            else if (SpellLevel > 0)
            {
                message += name + " casts " + Name + " at level " + castingLevel + ".";
            }
            else
            {
                message += name + " casts " + Name + ".";
            }

            // Set Casting Scale
            int castingScale = 0;
            if (SpellLevel == 0)
            {
                if (mode == "Creature")
                {
                    if (int.TryParse(creature.ChallengeRating, out castingLevel) == false)
                    {
                        castingLevel = 1;
                    }
                    castingScale += CantripScaleAdd(castingLevel); 
                }
                if (mode == "Character") { castingScale += CantripScaleAdd(character.Level); }
            }

            bool abilityResult = abilityToUse.ProcessAbility(creature, character, mode, castingScale, out string abilityMessage, out List<string> activeEffects);
            if (abilityResult == true)
            {
                // Update Message
                message += abilityMessage;

                // Check Active Effect Additions
                foreach (string effect in activeEffects)
                {
                    CustomAbility effectMatch = SecondaryAbilities.FirstOrDefault(e => e.Name == effect);
                    if (effectMatch == null) { message += "\nCould not find Secondary Ability \"" + effect + "\"."; continue; }

                    CustomAbility newEffect = HelperMethods.DeepClone(effectMatch);
                    newEffect.PresetScale = (castingLevel - SpellLevel);

                    if (character != null)
                    {
                        character.ActiveEffectAbilities.Add(newEffect);
                    }
                    if (creature != null)
                    {
                        creature.ActiveEffectAbilities.Add(newEffect);
                    }
                }

                // Output Message
                if (character != null)
                {
                    HelperMethods.AddToPlayerLog(message, "Default", true);
                }
                if (creature != null)
                {
                    if (Configuration.MainModelRef.TabSelected_Campaigns)
                    {
                        HelperMethods.AddToCampaignMessages(message, "Spell");
                    }
                    else
                    {
                        HelperMethods.AddToPlayerLog(message, "Default", true);
                    }
                }
                
            }
            else
            {
                message += "\nAn error has occurred while processing the ability.";
                if (Configuration.MainModelRef.TabSelected_Campaigns)
                {
                    HelperMethods.AddToCampaignMessages(message, "Spell");
                }
                else
                {
                    HelperMethods.AddToPlayerLog(message, "Default", true);
                }
            }

        }
        #endregion
        #region CastSpell
        private RelayCommand _CastSpell;
        public ICommand CastSpell
        {
            get
            {
                if (_CastSpell == null)
                {
                    _CastSpell = new RelayCommand(DoCastSpell);
                }
                return _CastSpell;
            }
        }
        private void DoCastSpell(object parameters)
        {
            CreatureModel casterE = new CreatureModel();
            CharacterModel casterP = new CharacterModel();
            bool creatureMode = false;
            bool castAsRitual = false;
            string diceRolls;
            string altDiceRolls;
            string rollType = "Normal";
            string message = "";
            string name;
            int spellcastingAbilityModifier;
            int spellSaveDc;
            int attackRollFinal;
            int attackModifier;
            int attackTotal;
            int saveDamageRoll = 0;
            int saveDamageTotal;
            int healingRoll = 0;
            int healingTotal;
            int castingLevel = SpellLevel;
            int casterLevel;

            if ((parameters as object[]) == null)
            {
                if (parameters.GetType() == typeof(CreatureModel)) 
                { 
                    casterE = parameters as CreatureModel; 
                    creatureMode = true; 
                    casterE.DisplayPopup_Spells = false;
                    casterE.DisplayPopupAlt_Spells = false;
                    name = casterE.DisplayName;
                    if (int.TryParse(casterE.ChallengeRating, out casterLevel) == false)
                    {
                        casterLevel = 1;
                    } 
                }
                else 
                { 
                    casterP = (parameters as MainViewModel).CharacterBuilderView.ActiveCharacter; 
                    name = casterP.Name;
                    casterLevel = casterP.TotalLevel;
                }
            }
            else
            {
                if ((parameters as object[])[0].GetType() == typeof(CreatureModel)) 
                { 
                    casterE = (parameters as object[])[0] as CreatureModel; 
                    creatureMode = true; 
                    casterE.DisplayPopup_Spells = false;
                    casterE.DisplayPopupAlt_Spells = false;
                    name = casterE.Name + " " + casterE.TrackerIndicator;
                    if (int.TryParse(casterE.ChallengeRating, out casterLevel) == false)
                    {
                        casterLevel = 1;
                    }
                }
                else 
                { 
                    casterP = ((parameters as object[])[0] as MainViewModel).CharacterBuilderView.ActiveCharacter; 
                    name = casterP.Name;
                    casterLevel = casterP.TotalLevel;
                }

                if (int.TryParse((parameters as object[])[1].ToString(), out castingLevel) == false)
                {
                    rollType = (parameters as object[])[1].ToString();
                    if (rollType == "R") { castAsRitual = true; castingLevel = SpellLevel; }
                }

            }

            if (creatureMode == false)
            {
                //if (IsPrepared == false) // Not all classes operate on a prepared spell mechanic
                //{
                //    casterP.ActionHistory.Insert(0, "This spell has not been prepared.");
                //    return;
                //}

                if (CheckForSpellMaterials(ref casterP) == false)
                {
                    casterP.ActionHistory.Insert(0, "Insufficient casting materials.");
                    return;
                }

                if (castAsRitual == false)
                {
                    if (HelperMethods.CheckForSpellSlots(ref casterP, castingLevel) == false)
                    {
                        casterP.ActionHistory.Insert(0, "Insufficient spell slots to cast " + Name + ".");
                        return;
                    }
                }         

                foreach (ItemModel component in this.ConsumedMaterials)
                {
                    foreach (ItemModel backpackItem in casterP.Inventories[0].AllItems)
                    {
                        if (backpackItem.Name == component.Name)
                        {
                            backpackItem.Quantity--;
                        }
                    }
                }

            }

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);

            if (creatureMode)
            {
                spellcastingAbilityModifier = HelperMethods.GetAttributeModifier(HelperMethods.GetAttributeScore(casterE.SpellcastingAbility, casterE));
                spellSaveDc = 8 + spellcastingAbilityModifier + casterE.ProficiencyBonus;
            }
            else
            {
                spellcastingAbilityModifier = casterP.SpellAbilityModifier;
                spellSaveDc = casterP.SpellSaveDc;
            }

            if (SpellLevel > 0)
            {
                message += string.Format("{0} casts {1} at level {2}{3}. ", name, Name, castingLevel, (castAsRitual) ? " as a ritual" : "");
            }
            else
            {
                message += string.Format("{0} casts {1}. ", name, Name);
            }

            if (HasAttackStat)
            {
                int targets = NumberOfAttacks;
                if (HasTargetScaling) 
                { 
                    if (SpellLevel > 0) { targets += (castingLevel - SpellLevel) * AttackTargetScale; }
                    else { targets += CantripScaleAdd(casterLevel); }
                }

                for (int t = 0; t < targets; t++)
                {
                    int damageTotal;
                    int damageRoll = 0;
                    int damageModifier = DamageDiceModifier;
                    int damageDice;
                    int altDamageTotal;
                    int altDamageRoll = 0;
                    int altDamageModifier = DamageDiceModifier;
                    int altDamageDice;
                    bool gotCritical = false;

                    attackRollFinal = HelperMethods.RollD20(rollType == "Advantage", rollType == "Disadvantage");

                    if (t > 0)
                    {
                        message += "\n..........."; // String to swap for Roll20 output with /me
                    }

                    if (IsAutoHit == false)
                    {
                        if (attackRollFinal == 20)
                        {
                            gotCritical = true;
                        }
                        if (attackRollFinal == 1)
                        {
                            if (targets > 1)
                            {
                                message += string.Format("\nAttack {0}: Miss", t + 1);
                                continue;
                            }
                            else
                            {
                                message += "Spell misses. ";
                                continue;
                            }
                        }
                    }

                    diceRolls = "";
                    altDiceRolls = "";
                    damageDice = DamageDiceQuantity;
                    altDamageDice = AltDamageDiceQuantity;

                    if (gotCritical)
                    {
                        if (Configuration.MainModelRef.SettingsView.UseCriticalHitMaxDamage == false)
                        {
                            damageDice += DamageDiceQuantity;
                            altDamageDice += AltDamageDiceQuantity;
                        }
                    }
                    if (HasAttackDamageScaling)
                    {
                        if (SpellLevel > 0) 
                        { 
                            damageDice += (castingLevel - SpellLevel) * AttackDamageScale;
                            altDamageDice += (castingLevel - SpellLevel) * AttackDamageScale;
                        }
                        else 
                        { 
                            damageDice += CantripScaleAdd(casterLevel);
                            altDamageDice += CantripScaleAdd(casterLevel);
                        }
                    }
                    for (int i = 0; i < damageDice; i++)
                    {
                        int diceRoll = Configuration.RNG.Next(1, DamageDiceQuality + 1);
                        damageRoll += diceRoll;
                        if (i > 0)
                        {
                            diceRolls += " + ";
                        }
                        diceRolls += diceRoll;
                    }
                    for (int i = 0; i < altDamageDice; i++)
                    {
                        int diceRoll = Configuration.RNG.Next(1, AltDamageDiceQuality + 1);
                        altDamageRoll += diceRoll;
                        if (i > 0)
                        {
                            altDiceRolls += " + ";
                        }
                        altDiceRolls += diceRoll;
                    }

                    if (creatureMode)
                    {
                        attackModifier = casterE.ProficiencyBonus + spellcastingAbilityModifier;
                    }
                    else
                    {
                        attackModifier = casterP.SpellAttackBonus;
                    }

                    attackTotal = attackRollFinal + attackModifier;

                    if (UsesSpellcastingMod_Damage)
                    {
                        damageModifier += spellcastingAbilityModifier;
                        altDamageModifier += spellcastingAbilityModifier;
                    }
                    if (gotCritical && Configuration.MainModelRef.SettingsView.UseCriticalHitMaxDamage)
                    {
                        damageModifier += (DamageDiceQuality * DamageDiceQuantity);
                        altDamageModifier += (AltDamageDiceQuality * AltDamageDiceQuantity);
                    }
                    damageTotal = damageRoll + damageModifier;
                    altDamageTotal = altDamageRoll + altDamageModifier;

                    if (IsAutoHit == false)
                    {
                        if (targets > 1) { message += "\nAttack " + (t + 1) + ":"; }
                        message += ((targets > 1) ? "\n  " : "\n") + "Attack Result: " + attackTotal + ((gotCritical) ? " (Critical)" : "");
                    }
                    else
                    {
                        message += "\nAttack Auto-Hits";
                    }
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += ((targets > 1) ? "\n  " : "\n") + "Attack Roll: [" + attackRollFinal + "] + " + attackModifier; }
                    message += ((targets > 1) ? "\n  " : "\n") + "Damage Result: " + damageTotal + " " + DamageType.ToLower() + " damage";
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += ((targets > 1) ? "\n  " : "\n") + "Damage Roll: [" + diceRolls + "] + " + damageModifier; }
                    if (HasAltAttackDamage)
                    {
                        message += ((targets > 1) ? "\n  " : "\n") + "Alt Damage Result: " + altDamageTotal + " " + DamageType.ToLower() + " damage";
                        if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += ((targets > 1) ? "\n  " : "\n") + "Alt Damage Roll: [" + altDiceRolls + "] + " + altDamageModifier; }
                        message += "\nAlt Damage Condition: " + AltDamageDiceCondition;
                    }

                }

            }
            if (HasHealingStat)
            {
                diceRolls = "";
                altDiceRolls = "";
                int healingModifier;
                int altHealingModifier;
                int healDiceNum = HealingDiceQuantity;
                int altHealDiceNum = AltHealingDiceQuantity;
                int altHealingRoll = 0;
                int altHealingTotal;

                if (HasHealingScaling)
                {
                    if (SpellLevel > 0) 
                    { 
                        healDiceNum += (castingLevel - SpellLevel) * HealingScale;
                        altHealDiceNum += (castingLevel - SpellLevel) * HealingScale;
                    }
                    else 
                    { 
                        healDiceNum += CantripScaleAdd(casterLevel);
                        altHealDiceNum += CantripScaleAdd(casterLevel);
                    }
                }

                for (int i = 0; i < healDiceNum; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, HealingDiceQuality + 1);
                    healingRoll += diceRoll;
                    if (i > 0)
                    {
                        diceRolls += " + ";
                    }
                    diceRolls += diceRoll;
                }
                for (int i = 0; i < altHealDiceNum; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, AltHealingDiceQuality + 1);
                    altHealingRoll += diceRoll;
                    if (i > 0)
                    {
                        altDiceRolls += " + ";
                    }
                    altDiceRolls += diceRoll;
                }

                healingModifier = HealingDiceModifier;
                altHealingModifier = HealingDiceModifier;

                if (UsesSpellcastingMod_Healing) 
                { 
                    healingModifier += spellcastingAbilityModifier;
                    altHealingModifier += spellcastingAbilityModifier;
                }

                healingTotal = healingRoll + healingModifier;
                altHealingTotal = altHealingRoll + altHealingModifier;

                message += "\nHealing Result: " + healingTotal + " hit points";
                if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nHealing Roll: [" + diceRolls + "] + " + healingModifier; }
                if (HasAltHealingDice)
                {
                    message += "\nAlt Healing Result: " + altHealingTotal + " hit points";
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nAlt Healing Roll: [" + altDiceRolls + "] + " + altHealingModifier; }
                    message += "\nAlt Healing Condition: " + AltHealingDiceCondition;
                }

            }
            if (HasSaveStat)
            {
                diceRolls = "";
                altDiceRolls = "";

                int saveDmgDiceNum = SaveDamageDiceQuantity;
                int altSaveDmgDiceNum = AltSaveDamageDiceQuantity;

                int altSaveDamageRoll = 0;
                int altSaveDamageTotal;

                if (HasSaveDamageScaling)
                {
                    if (SpellLevel > 0) 
                    { 
                        saveDmgDiceNum += (castingLevel - SpellLevel) * SaveDamageScale;
                        altSaveDmgDiceNum += (castingLevel - SpellLevel) * SaveDamageScale;
                    }
                    else 
                    { 
                        saveDmgDiceNum += CantripScaleAdd(casterLevel);
                        altSaveDmgDiceNum += CantripScaleAdd(casterLevel);
                    }
                }

                for (int i = 0; i < saveDmgDiceNum; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, SaveDamageDiceQuality + 1);
                    saveDamageRoll += diceRoll;
                    if (i > 0)
                    {
                        diceRolls += " + ";
                    }
                    diceRolls += diceRoll;
                }
                for (int i = 0; i < altSaveDmgDiceNum; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, AltSaveDamageDiceQuality + 1);
                    altSaveDamageRoll += diceRoll;
                    if (i > 0)
                    {
                        altDiceRolls += " + ";
                    }
                    altDiceRolls += diceRoll;
                }

                saveDamageTotal = saveDamageRoll + SaveDamageDiceModifier;
                altSaveDamageTotal = altSaveDamageRoll + SaveDamageDiceModifier;

                message += string.Format("{0} must make a DC {1} {2} saving throw.",
                    (HasAreaOfEffect) ? string.Format("Targets in a {0} ft.{1} {2}", AoeRange, (AoeShape == "Sphere") ? " radius" : "", AoeShape.ToLower()) : "Target",
                    spellSaveDc,
                    SaveAbility);
                if (saveDamageTotal > 0)
                {
                    message += "\nDamage on Fail: " + saveDamageTotal + " " + SaveDamageType + " damage";
                    if (IsHalfDamageOnSave) { message += "\nDamage on Pass: " + (saveDamageTotal / 2) + " " + SaveDamageType + " damage"; }
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nDamage Roll: [" + diceRolls + "] + " + SaveDamageDiceModifier; }
                    if (HasAltSaveDamage)
                    {
                        message += "\nAlt Damage on Fail: " + altSaveDamageTotal + " " + SaveDamageType + " damage";
                        if (IsHalfDamageOnSave) { message += "\nAlt Damage on Pass: " + (altSaveDamageTotal / 2) + " " + SaveDamageType + " damage"; }
                        if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nAlt Damage Roll: [" + altDiceRolls + "] + " + SaveDamageDiceModifier; }
                        message += "\nAlt Damage Condition: " + AltSaveDamageCondition;
                    }
                }
                message += (SaveEffect.Length > 0) ? "\nEffect: " + SaveEffect : "";

            }
            if (HasSpecialEffect)
            {
                message += "\n";
                message += (SpecialEffectUsesDescription) ? Description : SpecialEffect;
            }
            if (RequiresConcentration)
            {
                message += "\nThis spell requires concentration. ";
                if (creatureMode)
                {
                    if (casterE.IsConcentrating)
                    {
                        message += "\nPrevious concentration spell has ended. ";
                    }
                    casterE.IsConcentrating = true;
                }
                else
                {
                    if (casterP.IsConcentrating)
                    {
                        message += "\nPrevious concentration spell has ended. ";
                    }
                    casterP.IsConcentrating = true;
                }
            }
            if (DoesCreateActiveEffects)
            {
                if (creatureMode)
                {
                    if (RequiresConcentration)
                    {
                        List<ActiveEffectModel> filteredEffects = new List<ActiveEffectModel>();
                        foreach (ActiveEffectModel effect in casterE.ActiveEffects)
                        {
                            if (effect.ConcentrationDependent) { continue; }
                            else { filteredEffects.Add(effect); }
                        }
                        casterE.ActiveEffects = new ObservableCollection<ActiveEffectModel>(filteredEffects);
                    }
                    foreach (ActiveEffectModel effect in ActiveEffects)
                    {
                        ActiveEffectModel newEffect = HelperMethods.DeepClone(effect);
                        newEffect.BaseLevel = SpellLevel;
                        newEffect.CastLevel = castingLevel;
                        newEffect.CasterName = (creatureMode) ? casterE.DisplayName : casterP.Name;
                        newEffect.AttackMod = (creatureMode) ? casterE.SpellAttackBonus : casterP.SpellAttackBonus;
                        newEffect.DiceModifier += (newEffect.UseSpellcastingMod) ? ((creatureMode) ? casterE.SpellAbilityModifier : casterP.SpellAbilityModifier) : 0;
                        newEffect.ConcentrationDependent = RequiresConcentration;
                        casterE.ActiveEffects.Add(newEffect);
                        message += "\nActive Effect added: " + newEffect.Name;
                    }
                }
                else
                {
                    if (RequiresConcentration)
                    {
                        List<ActiveEffectModel> filteredEffects = new List<ActiveEffectModel>();
                        foreach (ActiveEffectModel effect in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveEffects)
                        {
                            if (effect.ConcentrationDependent) { continue; }
                            else { filteredEffects.Add(effect); }
                        }
                        Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveEffects = new ObservableCollection<ActiveEffectModel>(filteredEffects);
                    }
                    foreach (ActiveEffectModel effect in ActiveEffects)
                    {
                        ActiveEffectModel newEffect = HelperMethods.DeepClone(effect);
                        newEffect.BaseLevel = SpellLevel;
                        newEffect.CastLevel = castingLevel;
                        newEffect.CasterName = (creatureMode) ? casterE.DisplayName : casterP.Name;
                        newEffect.AttackMod = (creatureMode) ? casterE.SpellAttackBonus : casterP.SpellAttackBonus;
                        newEffect.DiceModifier += (newEffect.UseSpellcastingMod) ? ((creatureMode) ? casterE.SpellAbilityModifier : casterP.SpellAbilityModifier) : 0;
                        newEffect.ConcentrationDependent = RequiresConcentration;
                        Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveEffects.Add(newEffect);
                        message += "\nActive Effect added: " + newEffect.Name;
                    }
                }
            }

            //if (Configuration.MainModelRef.TabSelected_Tracker)
            //{
            //    HelperMethods.AddToEncounterLog(message);
            //}
            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Spell");
            }
            else
            {
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }

        }
        #endregion
        #region DuplicateSpell
        public ICommand DuplicateSpell
        {
            get
            {
                return new RelayCommand(param => DoDuplicateSpell());
            }
        }
        private void DoDuplicateSpell()
        {
            SpellModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.SpellBuilderView.AllSpells.Insert(Configuration.MainModelRef.SpellBuilderView.AllSpells.IndexOf(this), duplicate);
            Configuration.MainModelRef.SpellBuilderView.FilteredSpells.Insert(Configuration.MainModelRef.SpellBuilderView.FilteredSpells.IndexOf(this), duplicate);
            Configuration.MainModelRef.SpellBuilderView.ActiveSpell = duplicate;
        }
        #endregion
        #region DeleteSpell
        public ICommand DeleteSpell
        {
            get
            {
                return new RelayCommand(param => DoDeleteSpell());
            }
        }
        private void DoDeleteSpell()
        {
            Configuration.MainModelRef.SpellBuilderView.AllSpells.Remove(this);
            Configuration.MainModelRef.SpellBuilderView.FilteredSpells.Remove(this);
        }
        #endregion
        #region AddConsumedMaterial
        private RelayCommand _AddConsumedMaterial;
        public ICommand AddConsumedMaterial
        {
            get
            {
                if (_AddConsumedMaterial == null)
                {
                    _AddConsumedMaterial = new RelayCommand(param => DoAddConsumedMaterial());
                }
                return _AddConsumedMaterial;
            }
        }
        private void DoAddConsumedMaterial()
        {
            ObjectSelectionDialog itemSelect = new ObjectSelectionDialog(Configuration.ItemRepository.ToList());

            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                ItemModel itemToAdd = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel);
                itemToAdd.Quantity = 1;

                ConsumedMaterials.Add(itemToAdd);

            }
        }
        #endregion
        #region AddActiveEffect
        private RelayCommand _AddActiveEffect;
        public ICommand AddActiveEffect
        {
            get
            {
                if (_AddActiveEffect == null)
                {
                    _AddActiveEffect = new RelayCommand(param => DoAddActiveEffect());
                }
                return _AddActiveEffect;
            }
        }
        private void DoAddActiveEffect()
        {
            ActiveEffects.Add(new ActiveEffectModel());
        }
        #endregion
        #region AddSpellClass
        private RelayCommand _AddSpellClass;
        public ICommand AddSpellClass
        {
            get
            {
                if (_AddSpellClass == null)
                {
                    _AddSpellClass = new RelayCommand(param => DoAddSpellClass());
                }
                return _AddSpellClass;
            }
        }
        private void DoAddSpellClass()
        {
            //SpellClasses.Add(new ConvertibleValue());
            List<ConvertibleValue> options = new();
            foreach (string sc in Configuration.MainModelRef.SpellcastingClasses)
            {
                options.Add(new(sc));
            }
            MultiObjectSelectionDialog selectionDialog = new(options, "Spellcasting Classes");
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ConvertibleValue cv in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCVs)
                {
                    bool existingFound = false;
                    foreach (ConvertibleValue opt in SpellClasses)
                    {
                        if (opt.Value == cv.Value)
                        {
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    SpellClasses.Add(new() { Value = cv.Value });
                }
                SpellClasses = new(SpellClasses.OrderBy(item => item.Value));
            }
        }
        #endregion
        #region AddAbility
        public ICommand AddAbility => new RelayCommand(DoAddAbility);
        private void DoAddAbility(object param)
        {
            if (param == null) { return; }
            if (param.ToString() == "Primary") { PrimaryAbilities.Add(new()); }
            if (param.ToString() == "Secondary") { SecondaryAbilities.Add(new()); }
        }
        #endregion

        // Public Methods
        public void UpdateAbilityDropdowns()
        {
            foreach (CustomAbility ability in PrimaryAbilities)
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
            foreach (CustomAbility ability in SecondaryAbilities)
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

        // Private Methods
        private bool CheckForSpellMaterials(ref CharacterModel caster)
        {
            foreach (ItemModel component in this.ConsumedMaterials)
            {
                bool haveItem = false;
                foreach (ItemModel backpackItem in caster.Inventories[0].AllItems)
                {
                    if (backpackItem.Name == component.Name)
                    {
                        if (backpackItem.Quantity < component.Quantity)
                        {
                            return false;
                        }
                        else
                        {
                            haveItem = true;
                        }
                    }
                }
                if (haveItem == false)
                {
                    return false;
                }
            }

            return true;

        }
        private int CantripScaleAdd(int casterLevel)
        {
            return casterLevel switch
            {
                5 or 6 or 7 or 8 or 9 or 10 => 1,
                11 or 12 or 13 or 14 or 15 or 16 => 2,
                17 or 18 or 19 or 20 => 3,
                _ => 0,
            };
        }

    }
}
