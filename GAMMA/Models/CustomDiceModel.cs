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
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region DiceQuantity
        private int _DiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int DiceQuantity
        {
            get => _DiceQuantity;
            set => SetAndNotify(ref _DiceQuantity, value);
        }
        #endregion
        #region DiceSides
        private int _DiceSides;
        [XmlSaveMode(XSME.Single)]
        public int DiceSides
        {
            get => _DiceSides;
            set => SetAndNotify(ref _DiceSides, value);
        }
        #endregion
        #region DiceModifier
        private int _DiceModifier;
        [XmlSaveMode(XSME.Single)]
        public int DiceModifier
        {
            get => _DiceModifier;
            set => SetAndNotify(ref _DiceModifier, value);
        }
        #endregion

        // Commands
        #region RollDice
        public ICommand RollDice => new RelayCommand(DoRollDice);
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
            HelperMethods.AddToGameplayLog(msg, "Default", true);
        }
        #endregion
        #region RemoveDiceSet
        public ICommand RemoveDiceSet => new RelayCommand(param => DoRemoveDiceSet());
        private void DoRemoveDiceSet()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.CustomDiceSets.Remove(this);
        }
        #endregion

    }
}
