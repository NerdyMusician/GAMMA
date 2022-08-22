using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class HitDiceSet : BaseModel
    {
        // Constructors
        public HitDiceSet()
        {
            HitDiceSides = Configuration.DiceSides;
        }

        // Databound Properties
        #region CurrentHitDice
        private int _CurrentHitDice;
        [XmlSaveMode(XSME.Single)]
        public int CurrentHitDice
        {
            get => _CurrentHitDice;
            set => SetAndNotify(ref _CurrentHitDice, value);
        }
        #endregion
        #region MaxHitDice
        private int _MaxHitDice;
        [XmlSaveMode(XSME.Single)]
        public int MaxHitDice
        {
            get => _MaxHitDice;
            set => SetAndNotify(ref _MaxHitDice, value);
        }
        #endregion
        #region HitDiceSides
        private List<string> _HitDiceSides;
        public List<string> HitDiceSides
        {
            get => _HitDiceSides;
            set => SetAndNotify(ref _HitDiceSides, value);
        }
        #endregion
        #region HitDiceQuality
        private int _HitDiceQuality;
        [XmlSaveMode(XSME.Single)]
        public int HitDiceQuality
        {
            get => _HitDiceQuality;
            set => SetAndNotify(ref _HitDiceQuality, value);
        }
        #endregion

        // Commands
        #region RollHitDice
        public ICommand RollHitDice => new RelayCommand(DoRollHitDice);
        private void DoRollHitDice(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("No parameter passed to HitDiceSet.RollHitDice", true); return; }

            CharacterModel RefChar = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            if (CurrentHitDice <= 0) { HelperMethods.AddToGameplayLog(RefChar.Name + " has no more hit dice."); return; }
            if (RefChar.CurrentHealth == RefChar.MaxHealth) { HelperMethods.AddToGameplayLog(RefChar.Name + " is at max health already."); return; }

            string diceLimit = param.ToString();
            string message = string.Empty;
            int diceRoll;
            int total;
            List<int> rolls = new();
            int healTotal = 0;
            int modTotal = 0;
            switch (diceLimit)
            {
                case "Single":
                    HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
                    CurrentHitDice--;
                    diceRoll = Configuration.RNG.Next(1, HitDiceQuality + 1);
                    total = diceRoll + RefChar.ConstitutionModifier;
                    RefChar.CurrentHealth += total;
                    if (RefChar.CurrentHealth > RefChar.MaxHealth) { RefChar.CurrentHealth = RefChar.MaxHealth; }
                    message = RefChar.Name + " rolled a hit die.";
                    message += "\nResult: " + total + " hit point" + ((total > 1) ? "s" : "");
                    message += Configuration.MainModelRef.SettingsView.ShowDiceRolls ? "\nRoll: [" + diceRoll + "] + " + RefChar.ConstitutionModifier : "";
                    break;
                case "ToBloodied":
                    if (RefChar.Status == "Bloodied" || RefChar.Status == "Bruised" || RefChar.Status == "Fine")
                    {
                        HelperMethods.AddToGameplayLog("Character is already at Bloodied or better.");
                        return;
                    }
                    do
                    {
                        diceRoll = Configuration.RNG.Next(1, HitDiceQuality + 1);
                        total = diceRoll + RefChar.ConstitutionModifier;
                        RefChar.CurrentHealth += total;
                        rolls.Add(diceRoll);
                        modTotal += RefChar.ConstitutionModifier;
                        healTotal += total;
                        CurrentHitDice--;
                    }
                    while (Convert.ToDouble(RefChar.CurrentHealth) / Convert.ToDouble(RefChar.MaxHealth) < 0.25 && CurrentHitDice > 0);
                    message = RefChar.Name + " rolled hit dice to Bloodied.";
                    message += "\nResult: " + healTotal + " hit point" + ((healTotal > 1) ? "s" : "");
                    message += Configuration.MainModelRef.SettingsView.ShowDiceRolls ? "\nRoll: [" + HelperMethods.GetStringFromList(rolls, " + ") + "] + " + modTotal : "";
                    break;
                case "ToBruised":
                    if (RefChar.Status == "Bruised" || RefChar.Status == "Fine")
                    {
                        HelperMethods.AddToGameplayLog("Character is already at Bruised or better.");
                        return;
                    }
                    do
                    {
                        diceRoll = Configuration.RNG.Next(1, HitDiceQuality + 1);
                        total = diceRoll + RefChar.ConstitutionModifier;
                        RefChar.CurrentHealth += total;
                        rolls.Add(diceRoll);
                        modTotal += RefChar.ConstitutionModifier;
                        healTotal += total;
                        CurrentHitDice--;
                    }
                    while (Convert.ToDouble(RefChar.CurrentHealth) / Convert.ToDouble(RefChar.MaxHealth) < 0.50 && CurrentHitDice > 0);
                    message = RefChar.Name + " rolled hit dice to Bruised.";
                    message += "\nResult: " + healTotal + " hit point" + ((healTotal > 1) ? "s" : "");
                    message += Configuration.MainModelRef.SettingsView.ShowDiceRolls ? "\nRoll: [" + HelperMethods.GetStringFromList(rolls, " + ") + "] + " + modTotal : "";
                    break;
                case "ToFine":
                    if (RefChar.Status == "Fine")
                    {
                        HelperMethods.AddToGameplayLog("Character is already at Fine or better.");
                        return;
                    }
                    do
                    {
                        diceRoll = Configuration.RNG.Next(1, HitDiceQuality + 1);
                        total = diceRoll + RefChar.ConstitutionModifier;
                        RefChar.CurrentHealth += total;
                        rolls.Add(diceRoll);
                        modTotal += RefChar.ConstitutionModifier;
                        healTotal += total;
                        CurrentHitDice--;
                    }
                    while (Convert.ToDouble(RefChar.CurrentHealth) / Convert.ToDouble(RefChar.MaxHealth) < 0.75 && CurrentHitDice > 0);
                    message = RefChar.Name + " rolled hit dice to Fine.";
                    message += "\nResult: " + healTotal + " hit point" + ((healTotal > 1) ? "s" : "");
                    message += Configuration.MainModelRef.SettingsView.ShowDiceRolls ? "\nRoll: [" + HelperMethods.GetStringFromList(rolls, " + ") + "] + " + modTotal : "";
                    break;
                case "ToNearMax":
                    int maxRoll = HitDiceQuality + RefChar.ConstitutionModifier;
                    if (RefChar.MaxHealth - RefChar.CurrentHealth < maxRoll)
                    {
                        HelperMethods.AddToGameplayLog("Character is already within one hit dice of maximum health.");
                        return;
                    }
                    do
                    {
                        diceRoll = Configuration.RNG.Next(1, HitDiceQuality + 1);
                        total = diceRoll + RefChar.ConstitutionModifier;
                        RefChar.CurrentHealth += total;
                        rolls.Add(diceRoll);
                        modTotal += RefChar.ConstitutionModifier;
                        healTotal += total;
                        CurrentHitDice--;
                    }
                    while (RefChar.MaxHealth - RefChar.CurrentHealth >= maxRoll && CurrentHitDice > 0);
                    message = RefChar.Name + " rolled hit dice to near max.";
                    message += "\nResult: " + healTotal + " hit point" + ((healTotal > 1) ? "s" : "");
                    message += Configuration.MainModelRef.SettingsView.ShowDiceRolls ? "\nRoll: [" + HelperMethods.GetStringFromList(rolls, " + ") + "] + " + modTotal : "";
                    break;
                default:
                    HelperMethods.WriteToLogFile("Invalid parameter " + diceLimit + " passed to HitDiceSet.RollHitDice", true);
                    return;
            }
            HelperMethods.AddToGameplayLog(message, "Default", true);
        }
        #endregion
        #region RemoveHitDice
        public ICommand RemoveHitDice => new RelayCommand(param => DoRemoveHitDice());
        private void DoRemoveHitDice()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.HitDiceSets.Remove(this);
        }
        #endregion

    }
}
