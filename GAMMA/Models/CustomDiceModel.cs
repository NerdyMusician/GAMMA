using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class CustomDiceModel : BaseModel
    {
        // Constructors
        public CustomDiceModel()
        {
            Name = "New Custom Roll";
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

        // Commands
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
            string msg = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Name + " made a custom roll: " + Name + " (" + DiceQuantity + "d" + DiceSides + "+" + DiceModifier + ").";
            string dr = "";
            int rslt = 0;
            for (int i = 0; i < DiceQuantity; i++)
            {
                int roll = Configuration.RNG.Next(1, DiceSides + 1);
                if (i > 0) { dr += " + "; }
                dr += roll;
                rslt += roll;
            }
            rslt += DiceModifier;
            msg += "\nResult: " + rslt;
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls) { msg += "\nRoll: [" + dr + "] + " + DiceModifier; }
            HelperMethods.AddToPlayerLog(msg, "Default", true);
        }
        #endregion
        #region RemoveDiceSet
        private RelayCommand _RemoveDiceSet;
        public ICommand RemoveDiceSet
        {
            get
            {
                if (_RemoveDiceSet == null)
                {
                    _RemoveDiceSet = new RelayCommand(param => DoRemoveDiceSet());
                }
                return _RemoveDiceSet;
            }
        }
        private void DoRemoveDiceSet()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.CustomDiceSets.Remove(this);
        }
        #endregion

    }
}
