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
    public class AttackModel : BaseModel
    {
        // Constructors
        public AttackModel()
        {
            Name = "New Attack";

            DamageTypes = Configuration.DamageTypes.ToList();
            AbilityTypes = Configuration.AbilityTypes.ToList();
            AttackTypes = Configuration.AttackTypes.ToList();

            AttackOptions = new ObservableCollection<AttackOptionModel>();
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

        #region AttackModifier
        private int _AttackModifier;
        [XmlSaveMode("Single")]
        public int AttackModifier
        {
            get
            {
                return _AttackModifier;
            }
            set
            {
                _AttackModifier = value;
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

        #region HasExtraDamageOnHit
        private bool _HasExtraDamageOnHit;
        [XmlSaveMode("Single")]
        public bool HasExtraDamageOnHit
        {
            get
            {
                return _HasExtraDamageOnHit;
            }
            set
            {
                _HasExtraDamageOnHit = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraDamageDiceQuantity
        private int _ExtraDamageDiceQuantity;
        [XmlSaveMode("Single")]
        public int ExtraDamageDiceQuantity
        {
            get
            {
                return _ExtraDamageDiceQuantity;
            }
            set
            {
                _ExtraDamageDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraDamageDiceQuality
        private int _ExtraDamageDiceQuality;
        [XmlSaveMode("Single")]
        public int ExtraDamageDiceQuality
        {
            get
            {
                return _ExtraDamageDiceQuality;
            }
            set
            {
                _ExtraDamageDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraDamageDiceModifier
        private int _ExtraDamageDiceModifier;
        [XmlSaveMode("Single")]
        public int ExtraDamageDiceModifier
        {
            get
            {
                return _ExtraDamageDiceModifier;
            }
            set
            {
                _ExtraDamageDiceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraDamageType
        private string _ExtraDamageType;
        [XmlSaveMode("Single")]
        public string ExtraDamageType
        {
            get
            {
                return _ExtraDamageType;
            }
            set
            {
                _ExtraDamageType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasRange
        private bool _HasRange;
        [XmlSaveMode("Single")]
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
        #region ShortRange
        private int _ShortRange;
        [XmlSaveMode("Single")]
        public int ShortRange
        {
            get
            {
                return _ShortRange;
            }
            set
            {
                _ShortRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LongRange
        private int _LongRange;
        [XmlSaveMode("Single")]
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

        #region HasMultipleTargets
        private bool _HasMultipleTargets;
        [XmlSaveMode("Single")]
        public bool HasMultipleTargets
        {
            get
            {
                return _HasMultipleTargets;
            }
            set
            {
                _HasMultipleTargets = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Targets
        private int _Targets;
        [XmlSaveMode("Single")]
        public int Targets
        {
            get
            {
                return _Targets;
            }
            set
            {
                _Targets = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ShowOptions
        private bool _ShowOptions;
        public bool ShowOptions
        {
            get
            {
                return _ShowOptions;
            }
            set
            {
                _ShowOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region HasAttackRoll
        private bool _HasAttackRoll;
        [XmlSaveMode("Single")]
        public bool HasAttackRoll
        {
            get
            {
                return _HasAttackRoll;
            }
            set
            {
                _HasAttackRoll = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasSaveEffect
        private bool _HasSaveEffect;
        [XmlSaveMode("Single")]
        public bool HasSaveEffect
        {
            get
            {
                return _HasSaveEffect;
            }
            set
            {
                _HasSaveEffect = value;
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
        #region SaveDifficulty
        private int _SaveDifficulty;
        [XmlSaveMode("Single")]
        public int SaveDifficulty
        {
            get
            {
                return _SaveDifficulty;
            }
            set
            {
                _SaveDifficulty = value;
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
        #region ExtraSaveDamageDiceQuantity
        private int _ExtraSaveDamageDiceQuantity;
        [XmlSaveMode("Single")]
        public int ExtraSaveDamageDiceQuantity
        {
            get
            {
                return _ExtraSaveDamageDiceQuantity;
            }
            set
            {
                _ExtraSaveDamageDiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraSaveDamageDiceQuality
        private int _ExtraSaveDamageDiceQuality;
        [XmlSaveMode("Single")]
        public int ExtraSaveDamageDiceQuality
        {
            get
            {
                return _ExtraSaveDamageDiceQuality;
            }
            set
            {
                _ExtraSaveDamageDiceQuality = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraSaveDamageDiceModifier
        private int _ExtraSaveDamageDiceModifier;
        [XmlSaveMode("Single")]
        public int ExtraSaveDamageDiceModifier
        {
            get
            {
                return _ExtraSaveDamageDiceModifier;
            }
            set
            {
                _ExtraSaveDamageDiceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ExtraSaveDamageType
        private string _ExtraSaveDamageType;
        [XmlSaveMode("Single")]
        public string ExtraSaveDamageType
        {
            get
            {
                return _ExtraSaveDamageType;
            }
            set
            {
                _ExtraSaveDamageType = value;
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

        #region AffectsArea
        private bool _AffectsArea;
        [XmlSaveMode("Single")]
        public bool AffectsArea
        {
            get
            {
                return _AffectsArea;
            }
            set
            {
                _AffectsArea = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AreaSize
        private int _AreaSize;
        [XmlSaveMode("Single")]
        public int AreaSize
        {
            get
            {
                return _AreaSize;
            }
            set
            {
                _AreaSize = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AreaShape
        private string _AreaShape;
        [XmlSaveMode("Single")]
        public string AreaShape
        {
            get
            {
                return _AreaShape;
            }
            set
            {
                _AreaShape = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region AttackOptions
        private ObservableCollection<AttackOptionModel> _AttackOptions;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<AttackOptionModel> AttackOptions
        {
            get
            {
                return _AttackOptions;
            }
            set
            {
                _AttackOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Formatted Texts
        #region FT_Info
        private string _FT_Info;
        public string FT_Info
        {
            get
            {
                return _FT_Info;
            }
            set
            {
                _FT_Info = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Dropdown Sources
        #region DamageTypes
        private List<string> _DamageTypes;
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
        #region AbilityTypes
        private List<string> _AbilityTypes;
        public List<string> AbilityTypes
        {
            get
            {
                return _AbilityTypes;
            }
            set
            {
                _AbilityTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttackTypes
        private List<string> _AttackTypes;
        public List<string> AttackTypes
        {
            get
            {
                return _AttackTypes;
            }
            set
            {
                _AttackTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region MakeAttack
        private RelayCommand _MakeAttack;
        public ICommand MakeAttack
        {
            get
            {
                if (_MakeAttack == null)
                {
                    _MakeAttack = new RelayCommand(DoMakeAttack);
                }
                return _MakeAttack;
            }
        }
        private void DoMakeAttack(object parameters)
        {
            CharacterModel attackingPlayer = new();
            CreatureModel attackingCreature = new();
            bool CreatureMode = false;
            bool useAdvantage;
            bool useDisadvantage;
            string diceRolls;
            bool gotCritical = false;
            int finalRoll;
            int attackTotal;
            int attackModifier;
            int damageRoll = 0;
            int damageModifier = 0;
            int damageTotal;
            int damageDiceQuality = DamageDiceQuality;
            int damageDiceQuantity = DamageDiceQuantity;
            string message = "";
            List<AttackOptionModel> options = new List<AttackOptionModel>();

            if ((parameters as object[]) == null)
            {
                if (parameters.GetType() == typeof(CreatureModel)) { attackingCreature = parameters as CreatureModel; CreatureMode = true; attackingCreature.DisplayPopup_Weapons = false; attackingCreature.DisplayPopupAlt_Weapons = false; }
                else { attackingPlayer = (parameters as MainViewModel).CharacterBuilderView.ActiveCharacter; }

                useAdvantage = false;
                useDisadvantage = false;
            }
            else
            {
                if ((parameters as object[])[0].GetType() == typeof(CreatureModel)) { attackingCreature = (parameters as object[])[0] as CreatureModel; CreatureMode = true; attackingCreature.DisplayPopup_Weapons = false; attackingCreature.DisplayPopupAlt_Weapons = false; }
                else { attackingPlayer = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter; }
                useAdvantage = ((parameters as object[])[1].ToString() == "Advantage");
                useDisadvantage = ((parameters as object[])[1].ToString() == "Disadvantage");
                if ((parameters as object[])[1].ToString() == "Options")
                {
                    AttackOptionWindow optionWindow = new AttackOptionWindow(this);

                    if (optionWindow.ShowDialog() == true)
                    {
                        options = optionWindow.Options;
                    }
                    else
                    {
                        return;
                    }
                }
                if ((parameters as object[])[1].ToString() == "LastOptions")
                {
                    options = this.AttackOptions.Where(opt => opt.UseOption).ToList();
                }
            }

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);

            foreach (AttackOptionModel option in options)
            {
                if (option.Type == "Use Advantage") { useAdvantage = true; }
                if (option.Type == "Use Disadvantage") { useDisadvantage = true; }
            }

            if (HasAttackRoll)
            {
                finalRoll = HelperMethods.RollD20(useAdvantage, useDisadvantage);

                if (finalRoll == 20)
                {
                    gotCritical = true;
                }
                if (finalRoll == 1)
                {
                    if (CreatureMode) 
                    { 
                        if (Configuration.MainModelRef.TabSelected_Players)
                        {
                            HelperMethods.AddToPlayerLog(string.Format("{0} attacked with {1} and Missed!", attackingCreature.DisplayName, Name), "Default", true);
                        }
                        else if (Configuration.MainModelRef.TabSelected_Campaigns)
                        {
                            HelperMethods.AddToCampaignMessages(string.Format("{0} attacked with {1} and Missed!", attackingCreature.Name, Name), "Attack");
                        }
                        //else
                        //{
                        //    HelperMethods.AddToEncounterLog(string.Format("{0} attacked with {1} and Missed!", attackingCreature.Name, Name));
                        //}
                    }
                    else 
                    {
                        HelperMethods.AddToPlayerLog(string.Format("{0} attacked with {1} and Missed!", attackingPlayer.Name, Name), "Default", true);
                    }
                    return;
                }

                diceRolls = "";

                foreach (AttackOptionModel option in options)
                {
                    if (option.Type == "Alternate Base Attack Damage") 
                    {
                        damageDiceQuantity = option.IntValue;
                        damageDiceQuality = option.IntValue2;
                    }
                }

                if (gotCritical)
                {
                    if (Configuration.MainModelRef.SettingsView.UseCriticalHitMaxDamage == false)
                    {
                        damageDiceQuantity += DamageDiceQuantity;
                    }
                    else
                    {
                        damageModifier += (DamageDiceQuality * DamageDiceQuantity);
                    }
                }
                for (int i = 0; i < damageDiceQuantity; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, damageDiceQuality + 1);
                    foreach (AttackOptionModel option in options)
                    {
                        if (option.Type == "Reroll Attack Damage Die at or Below")
                        {
                            if (diceRoll <= option.IntValue)
                            {
                                int altDiceRoll = Configuration.RNG.Next(1, damageDiceQuality + 1);
                                diceRoll = (altDiceRoll > diceRoll) ? altDiceRoll : diceRoll;
                            }
                        }
                    }
                    damageRoll += diceRoll;
                    if (i > 0)
                    {
                        diceRolls += " + ";
                    }
                    diceRolls += diceRoll;
                }

                attackModifier = AttackModifier;

                foreach (AttackOptionModel option in options)
                {
                    if (option.Type == "Attack Modifier") { attackModifier += option.IntValue; }
                }

                attackTotal = finalRoll + attackModifier;
                damageModifier += DamageDiceModifier;

                foreach (AttackOptionModel option in options)
                {
                    if (option.Type == "Damage Modifier") { damageModifier += option.IntValue; }
                }

                damageTotal = damageRoll + damageModifier;
                
                message += string.Format("{0} attacked{1}{2} with {3}.",
                    CreatureMode ? attackingCreature.DisplayName : attackingPlayer.Name,
                    useAdvantage ? " with advantage" : "",
                    useDisadvantage ? " with disadvantage" : "",
                    Name);
                if (options.Count() > 0)
                {
                    message += "\nOptions used: ";
                    foreach (AttackOptionModel option in options)
                    {
                        message += option.Name;
                        if (options.Last() != option)
                        {
                            message += ", ";
                        }
                    }
                }
                message += "\nAttack Result: " + attackTotal + ((gotCritical) ? " (Critical)" : "");
                if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nAttack Roll: [" + finalRoll + "] + " + attackModifier; }
                message += "\nDamage Result: " + damageTotal + " " + DamageType + " damage";
                if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nDamage Roll: [" + diceRolls + "] + " + damageModifier; }

            }

            if (HasExtraDamageOnHit)
            {
                int extraDamageRoll = 0;
                int diceRoll;
                int total;
                diceRolls = "";

                for (int i = 0; i < ExtraDamageDiceQuantity; i++)
                {
                    diceRoll = Configuration.RNG.Next(1, ExtraDamageDiceQuality + 1);
                    extraDamageRoll += diceRoll;
                    if (i > 0)
                    {
                        diceRolls += " + ";
                    }
                    diceRolls += diceRoll;
                }

                total = extraDamageRoll + ExtraDamageDiceModifier;

                message += "\nExtra Damage: " + total + " " + ExtraDamageType + " damage";
                if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nExtra Damage Roll: [" + diceRolls + "] + " + ExtraDamageDiceModifier; }

            }

            foreach (AttackOptionModel option in options)
            {
                int extraDmg = 0;
                string rolltext = "";
                if (option.Type == "Extra Damage on Critical - Roll" && gotCritical)
                {
                    for (int i = 0; i < option.IntValue; i++)
                    {
                        int roll = Configuration.RNG.Next(1, option.IntValue2 + 1);
                        extraDmg += roll;
                        rolltext += roll;
                        if (i < option.IntValue - 1)
                        {
                            rolltext += " + ";
                        }
                    }
                }
                if (option.Type == "Extra Damage on Hit - Roll")
                {
                    for (int i = 0; i < option.IntValue; i++)
                    {
                        int roll = Configuration.RNG.Next(1, option.IntValue2 + 1);
                        extraDmg += roll;
                        rolltext += roll;
                        if (i < option.IntValue - 1)
                        {
                            rolltext += " + ";
                        }
                    }
                }
                if (option.Type == "Extra Damage on Hit - Set") { extraDmg += option.IntValue; }
                if (option.Type == "Extra Damage on Critical - Set" && gotCritical) { extraDmg += option.IntValue; }
                if (extraDmg > 0)
                {
                    message += "\n" + option.Name + " Result: " + extraDmg + " " + option.StrValue + " damage";
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        message += "\n" + option.Name + " Roll: [" + rolltext + "]";
                    }
                }
            }

            if (HasSaveEffect)
            {
                int saveDamageRoll = 0;
                int extraSaveDamageRoll = 0;
                int saveDamageTotal;
                int extraSaveDamageTotal;
                diceRolls = "";
                string extraDiceRolls = "";

                for (int i = 0; i < SaveDamageDiceQuantity; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, SaveDamageDiceQuality + 1);
                    saveDamageRoll += diceRoll;
                    if (i > 0)
                    {
                        diceRolls += " + ";
                    }
                    diceRolls += diceRoll;
                }
                for (int i = 0; i < ExtraSaveDamageDiceQuantity; i++)
                {
                    int diceRoll = Configuration.RNG.Next(1, ExtraSaveDamageDiceQuality + 1);
                    extraSaveDamageRoll += diceRoll;
                    if (i > 0)
                    {
                        extraDiceRolls += " + ";
                    }
                    extraDiceRolls += diceRoll;
                }

                saveDamageTotal = saveDamageRoll + SaveDamageDiceModifier;
                extraSaveDamageTotal = extraSaveDamageRoll + ExtraSaveDamageDiceModifier;

                if (message == "")
                {
                    message += string.Format("{0} used {1}.", (CreatureMode) ? attackingCreature.Name : attackingPlayer.Name, Name);
                }
                message += "\nTarget must make a DC " + SaveDifficulty + " " + SaveAbility + " saving throw.";
                if (saveDamageTotal > 0)
                {
                    message += "\nDamage on Fail: " + saveDamageTotal + " " + SaveDamageType.ToLower() + " damage";
                    if (IsHalfDamageOnSave) { message += "\nDamage on Pass: " + (saveDamageTotal / 2) + " " + SaveDamageType.ToLower() + " damage"; }
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nDamage Roll: [" + diceRolls + "] + " + SaveDamageDiceModifier; }
                }
                if (extraSaveDamageTotal > 0)
                {
                    message += "\nExtra Damage on Fail: " + extraSaveDamageTotal + " " + ExtraSaveDamageType.ToLower() + " damage";
                    if (IsHalfDamageOnSave) { message += "\nExtra Damage on Pass: " + (extraSaveDamageTotal / 2) + " " + ExtraSaveDamageType.ToLower() + " damage"; }
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nExtra Damage Roll: [" + extraDiceRolls + "] + " + ExtraSaveDamageDiceModifier; }
                }
                message += (SaveEffect != null && SaveEffect != "") ? "\nEffect: " + SaveEffect : "";

            }

            if (HasSpecialEffect)
            {
                if (message == "")
                {
                    message += string.Format("{0} used {1}.", (CreatureMode) ? attackingCreature.Name : attackingPlayer.Name, Name);
                }
                message += "\n" + SpecialEffect;
            }

            //if (Configuration.MainModelRef.TabSelected_Tracker)
            //{
            //    HelperMethods.AddToEncounterLog(message);
            //}
            else if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Attack");
            }
            else
            {
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }

        }
        #endregion
        #region RemoveAttackFromCreature
        private RelayCommand _RemoveAttackFromCreature;
        public ICommand RemoveAttackFromCreature
        {
            get
            {
                if (_RemoveAttackFromCreature == null)
                {
                    _RemoveAttackFromCreature = new RelayCommand(param => DoRemoveAttackFromCreature(this));
                }
                return _RemoveAttackFromCreature;
            }
        }
        private void DoRemoveAttackFromCreature(AttackModel attackToRemove)
        {
            
            Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Attacks.Remove(attackToRemove);
        }
        #endregion
        #region RemoveAttackFromPlayer
        private RelayCommand _RemoveAttackFromPlayer;
        public ICommand RemoveAttackFromPlayer
        {
            get
            {
                if (_RemoveAttackFromPlayer == null)
                {
                    _RemoveAttackFromPlayer = new RelayCommand(param => DoRemoveAttackFromPlayer(this));
                }
                return _RemoveAttackFromPlayer;
            }
        }
        private void DoRemoveAttackFromPlayer(AttackModel attackToRemove)
        {

            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Attacks.Remove(attackToRemove);
        }
        #endregion
        #region AddAttackOption
        private RelayCommand _AddAttackOption;
        public ICommand AddAttackOption
        {
            get
            {
                if (_AddAttackOption == null)
                {
                    _AddAttackOption = new RelayCommand(param => DoAddAttackOption());
                }
                return _AddAttackOption;
            }
        }
        private void DoAddAttackOption()
        {
            AttackOptions.Add(new AttackOptionModel());
        }
        #endregion

        // Public Methods
        public void SetFormattedTexts()
        {
            FT_Info = "";
            if (HasAttackRoll) 
            {
                FT_Info += string.Format("{0}{1} to hit, {2}d{3}{4}{5} {6} damage. ", 
                    HelperMethods.GetModifierSymbol(AttackModifier), 
                    AttackModifier, DamageDiceQuantity, DamageDiceQuality, 
                    HelperMethods.GetModifierSymbol(DamageDiceModifier), DamageDiceModifier, DamageType); 
            }
            if (HasExtraDamageOnHit) 
            {
                FT_Info += string.Format("{0}d{1}{2}{3} {4} damage. ",
                    ExtraDamageDiceQuantity,
                    ExtraDamageDiceQuality,
                    HelperMethods.GetModifierSymbol(ExtraDamageDiceModifier), ExtraDamageDiceModifier, ExtraDamageType);
            }
            if (HasRange) { FT_Info += "Range (" + ShortRange + "/" + LongRange + ") "; }
            if (HasSpecialEffect) { FT_Info += SpecialEffect + " "; }
            if (HasSaveEffect)
            {
                FT_Info += string.Format("DC {0} {1} saving throw. {2}{3}{4}",
                    SaveDifficulty, SaveAbility,
                    SaveDamageDiceQuantity > 0 ? string.Format("\n{0}d{1}{2}{3} {4} damage. ",
                        SaveDamageDiceQuantity, SaveDamageDiceQuality,
                        HelperMethods.GetModifierSymbol(SaveDamageDiceModifier), SaveDamageDiceModifier, SaveDamageType) : "",
                    ExtraSaveDamageDiceQuantity > 0 ? string.Format("\n{0}d{1}{2}{3} {4} damage. ",
                        ExtraSaveDamageDiceQuantity, ExtraSaveDamageDiceQuality,
                        HelperMethods.GetModifierSymbol(ExtraSaveDamageDiceModifier), ExtraSaveDamageDiceModifier, ExtraSaveDamageType) : "",
                    SaveEffect != "" ? "\n" + SaveEffect : "");
            }
        }

    }
}
