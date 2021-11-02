using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class AttackOptionModel : BaseModel
    {
        // Constructors
        public AttackOptionModel()
        {
            Types = Configuration.AttackOptionTypes.ToList();
            DamageTypes = Configuration.DamageTypes.ToList();
        }

        // Databound Properties
        #region UseOption
        private bool _UseOption;
        [XmlSaveMode("Single")]
        public bool UseOption
        {
            get
            {
                return _UseOption;
            }
            set
            {
                _UseOption = value;
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
                SetFieldVisibility(value);
            }
        }
        #endregion
        #region IntValue
        private int _IntValue;
        [XmlSaveMode("Single")]
        public int IntValue
        {
            get
            {
                return _IntValue;
            }
            set
            {
                _IntValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IntValue2
        private int _IntValue2;
        [XmlSaveMode("Single")]
        public int IntValue2
        {
            get
            {
                return _IntValue2;
            }
            set
            {
                _IntValue2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region StrValue
        private string _StrValue;
        [XmlSaveMode("Single")]
        public string StrValue
        {
            get
            {
                return _StrValue;
            }
            set
            {
                _StrValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AutoText
        private string _AutoText;
        public string AutoText
        {
            get
            {
                return _AutoText;
            }
            set
            {
                _AutoText = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ShowTextBox
        private bool _ShowTextBox;
        public bool ShowTextBox
        {
            get
            {
                return _ShowTextBox;
            }
            set
            {
                _ShowTextBox = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowDiceFields
        private bool _ShowDiceFields;
        public bool ShowDiceFields
        {
            get
            {
                return _ShowDiceFields;
            }
            set
            {
                _ShowDiceFields = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowDamageType
        private bool _ShowDamageType;
        public bool ShowDamageType
        {
            get
            {
                return _ShowDamageType;
            }
            set
            {
                _ShowDamageType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Dropdown Sources
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
        #region RemoveOption
        private RelayCommand _RemoveOption;
        public ICommand RemoveOption
        {
            get
            {
                if (_RemoveOption == null)
                {
                    _RemoveOption = new RelayCommand(DoRemoveOption);
                }
                return _RemoveOption;
            }
        }
        private void DoRemoveOption(object param)
        {
            (param as AttackModel).AttackOptions.Remove(this);
        }
        #endregion

        // Public Methods
        public void UpdateAutoText()
        {
            AutoText = Type switch
            {
                "Damage Modifier" => "(" + HelperMethods.GetModifierSymbol(IntValue) + IntValue + " damage)",
                "Attack Modifier" => "(" + HelperMethods.GetModifierSymbol(IntValue) + IntValue + " to attack)",
                "Extra Damage on Critical - Roll" => string.Format("(+{0}d{1} {2} damage on critical hit)", IntValue, IntValue2, StrValue),
                "Extra Damage on Hit - Roll" => string.Format("(+{0}d{1} {2} damage on hit)", IntValue, IntValue2, StrValue),
                "Extra Damage on Hit - Set" => "(" + HelperMethods.GetModifierSymbol(IntValue) + IntValue + " " + StrValue + " damage on hit)",
                "Extra Damage on Critical - Set" => "(" + HelperMethods.GetModifierSymbol(IntValue) + IntValue + " " + StrValue + " damage on critical hit)",
                "Use Advantage" => "(advantage to hit)",
                "Use Disadvantage" => "(disadvantage to hit)",
                "Alternate Base Attack Damage" => "(" + IntValue + "d" + IntValue2 + ")",
                _ => ""
            };
        }

        // Private Methods
        private void SetFieldVisibility(string type)
        {
            ShowTextBox = false;
            ShowDiceFields = false;
            ShowDamageType = false;
            switch (type)
            {
                case "Damage Modifier":
                case "Attack Modifier":
                case "Reroll Attack Damage Die at or Below":
                case "Reroll Attack Damage Roll Total at or Below":
                    ShowTextBox = true;
                    break;
                case "Extra Damage on Critical - Set":
                case "Extra Damage on Hit - Set":
                    ShowTextBox = true;
                    ShowDamageType = true;
                    break;
                case "Extra Damage on Critical - Roll":
                case "Extra Damage on Hit - Roll":
                    ShowDiceFields = true;
                    ShowDamageType = true;
                    break;
                case "Alternate Base Attack Damage":
                    ShowDiceFields = true;
                    break;
                case "Use Advantage":
                    break;
            }
        }
        

    }
}
