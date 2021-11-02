using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class ActiveEffectModel : BaseModel
    {
        // Constructors
        public ActiveEffectModel()
        {
            Name = "New Effect";
            EffectTypes = new List<string>
            {
                "Attack", "Damage", "Healing", "Buff", "Debuff"
            };
            DamageTypes = Configuration.DamageTypes.ToList();
            DiceScale = 1;
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
        #region EffectType
        private string _EffectType;
        [XmlSaveMode("Single")]
        public string EffectType
        {
            get
            {
                return _EffectType;
            }
            set
            {
                _EffectType = value;
                NotifyPropertyChanged();

                SetFieldDisplay(value);
            }
        }
        #endregion
        #region EffectTypes
        private List<string> _EffectTypes;
        public List<string> EffectTypes
        {
            get
            {
                return _EffectTypes;
            }
            set
            {
                _EffectTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region BaseLevel
        private int _BaseLevel;
        [XmlSaveMode("Single")]
        public int BaseLevel
        {
            get
            {
                return _BaseLevel;
            }
            set
            {
                _BaseLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CastLevel
        private int _CastLevel;
        [XmlSaveMode("Single")]
        public int CastLevel
        {
            get
            {
                return _CastLevel;
            }
            set
            {
                _CastLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CasterName
        private string _CasterName;
        [XmlSaveMode("Single")]
        public string CasterName
        {
            get
            {
                return _CasterName;
            }
            set
            {
                _CasterName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttackMod
        private int _AttackMod;
        [XmlSaveMode("Single")]
        public int AttackMod
        {
            get
            {
                return _AttackMod;
            }
            set
            {
                _AttackMod = value;
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
        #region ShowFieldSet_Dice
        private bool _ShowFieldSet_Dice;
        public bool ShowFieldSet_Dice
        {
            get
            {
                return _ShowFieldSet_Dice;
            }
            set
            {
                _ShowFieldSet_Dice = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConcentrationDependent
        private bool _ConcentrationDependent;
        [XmlSaveMode("Single")]
        public bool ConcentrationDependent
        {
            get
            {
                return _ConcentrationDependent;
            }
            set
            {
                _ConcentrationDependent = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region DiceQuantity
        private int _DiceQuantity;
        [XmlSaveMode("Single")]
        public int DiceQuantity
        {
            get
            {
                return _DiceQuantity;
            }
            set
            {
                _DiceQuantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DiceSides
        private int _DiceSides;
        [XmlSaveMode("Single")]
        public int DiceSides
        {
            get
            {
                return _DiceSides;
            }
            set
            {
                _DiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DiceModifier
        private int _DiceModifier;
        [XmlSaveMode("Single")]
        public int DiceModifier
        {
            get
            {
                return _DiceModifier;
            }
            set
            {
                _DiceModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DoDiceScale
        private bool _DoDiceScale;
        [XmlSaveMode("Single")]
        public bool DoDiceScale
        {
            get
            {
                return _DoDiceScale;
            }
            set
            {
                _DoDiceScale = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DiceScale
        private int _DiceScale;
        [XmlSaveMode("Single")]
        public int DiceScale
        {
            get
            {
                return _DiceScale;
            }
            set
            {
                _DiceScale = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region UseSpellcastingMod
        private bool _UseSpellcastingMod;
        [XmlSaveMode("Single")]
        public bool UseSpellcastingMod
        {
            get
            {
                return _UseSpellcastingMod;
            }
            set
            {
                _UseSpellcastingMod = value;
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

        // Commands
        #region RemoveEffect
        private RelayCommand _RemoveEffect;
        public ICommand RemoveEffect
        {
            get
            {
                if (_RemoveEffect == null)
                {
                    _RemoveEffect = new RelayCommand(DoRemoveEffect);
                }
                return _RemoveEffect;
            }
        }
        private void DoRemoveEffect(object param)
        {
            if (param.ToString() == "Spell") { Configuration.MainModelRef.SpellBuilderView.ActiveSpell.ActiveEffects.Remove(this); }
            if (param.ToString() == "Character") { Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveEffects.Remove(this); }
            if (param.GetType() == typeof(CreatureModel)) { (param as CreatureModel).ActiveEffects.Remove(this); }
        }
        #endregion
        #region RollDice
        private RelayCommand _RollDice;
        public ICommand RollDice
        {
            get
            {
                if (_RollDice == null)
                {
                    _RollDice = new RelayCommand(DoRollDice);
                }
                return _RollDice;
            }
        }
        private void DoRollDice(object param)
        {
            int rollTotal = 0;
            int diceQuantity = DiceQuantity;
            string message;
            List<string> diceRolls = new();
            if (param == null) { param = ""; }
            bool useAdvantage = (param.ToString() == "Advantage");
            bool useDisadvantage = (param.ToString() == "Disadvantage");

            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);

            message = CasterName + " uses " + Name + ":";

            if (EffectType == "Attack")
            {
                int roll = HelperMethods.RollD20(useAdvantage, useDisadvantage);
                message += "\nAttack Result: " + (roll + AttackMod);
                if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { message += "\nAttack Roll: [" + roll + "] + " + AttackMod; }
            }
            if (DoDiceScale)
            {
                diceQuantity += ((CastLevel - BaseLevel) * DiceScale);
            }
            for (int i = 0; i < diceQuantity; i++)
            {
                int diceRoll = Configuration.RNG.Next(1, DiceSides + 1);
                rollTotal += diceRoll;
                diceRolls.Add(diceRoll.ToString());
            }
            rollTotal += DiceModifier;
            
            message += "\n" + EffectType + " Total: " + rollTotal + ((EffectType == "Damage" || EffectType == "Attack") ? " " + DamageType + " damage" : "");
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
            {
                message += "\n" + EffectType + " Roll: [";
                for (int i = 0; i < diceRolls.Count; i++)
                {
                    if (i < diceRolls.Count - 1)
                    {
                        message += diceRolls[i] + " + ";
                    }
                    else
                    {
                        message += diceRolls[i];
                    }
                }
                message += "] + " + DiceModifier;
            }
            if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                HelperMethods.AddToCampaignMessages(message, "Spell");
            }
            //if (Configuration.MainModelRef.TabSelected_Tracker)
            //{
            //    HelperMethods.AddToEncounterLog(message);
            //}
            if (Configuration.MainModelRef.TabSelected_Players)
            {
                HelperMethods.AddToPlayerLog(message, "Default", true);
            }
        }
        #endregion

        // Private Methods
        private void SetFieldDisplay(string type)
        {
            ShowFieldSet_Dice = (type == "Damage" || type == "Healing" || type == "Attack");
        }

    }
}
