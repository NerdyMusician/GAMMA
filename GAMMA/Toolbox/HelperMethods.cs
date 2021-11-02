﻿using GAMMA.Models;
using GAMMA.ViewModels;
using GAMMA.Windows;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;

namespace GAMMA.Toolbox
{
    public static class HelperMethods
    {
        // Public Methods
        //public static void AddToEncounterLog(string message)
        //{
        //    if (Configuration.MainModelRef.TrackerView == null) { return; }
        //    Configuration.MainModelRef.TrackerView.EventMessages.Insert(0, message);
        //}
        public static void AddToPlayerLog(string message, string type = "", bool copyToWeb = false)
        {
            //CharacterBuilderViewModel charView = Configuration.MainModelRef.CharacterBuilderView;
            //charView.ActiveCharacter.ActionHistory.Insert(0, message.Replace("\n..........",""));
            //if (charView.ActiveCharacter.OutputLinkedToRoll20 && copyToWeb) 
            //{ 
            //    if (message.Contains(charView.ActiveCharacter.Name))
            //    {
            //        AddToRoll20Chat(message.Replace(charView.ActiveCharacter.Name, "/me ").Replace("...........", "/me -"));
            //    }
            //    else
            //    {
            //        AddToRoll20Chat(message.Insert(0, "/me minion "));
            //    }
            //}
            CharacterModel activeChar = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
            activeChar.Messages.Insert(0, new() { MessageType = type, MessageContent = message.Replace("\n..........", "") });
            if (activeChar.OutputLinkedToRoll20 && copyToWeb)
            {
                if (message.Contains(activeChar.Name))
                {
                    AddToRoll20Chat(message.Replace(activeChar.Name, "/me ").Replace("...........", "/me -"));
                }
                else
                {
                    AddToRoll20Chat(message.Insert(0, "/me | "));
                }
            }
        }
        public static void AddToCampaignMessages(string message, string type, bool copyToWeb = false)
        {
            if (Configuration.MainModelRef.CampaignView == null) { return; }
            GameCampaign activeCampaign = Configuration.MainModelRef.CampaignView.ActiveCampaign;
            if (activeCampaign == null) { WriteToLogFile("Unable to write to campaign messages, no active campaign selected."); return; }
            activeCampaign.Messages.Insert(0, new(type, message));
            if (activeCampaign.LinkOutputToWeb && copyToWeb)
            {
                AddToRoll20Chat(message);
            }
        }
        public static void AddToRoll20Chat(string message)
        {
            IWebDriver driverRef = Configuration.MainModelRef.WebDriver;
            CharacterModel characterRef = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;

            try
            {
                
                string errMessage = "";

                if (driverRef == null) { characterRef.OutputLinkedToRoll20 = false; errMessage += "\nNo Web Driver detected."; }
                if (driverRef.CurrentWindowHandle == null) { characterRef.OutputLinkedToRoll20 = false; errMessage += "\nNo window handle available."; }
                else if (driverRef.FindElement(By.Id("textchat-input")) == null) { characterRef.OutputLinkedToRoll20 = false; errMessage += "\nCannot find text chat element."; }

                if (errMessage != "")
                {
                    new NotificationDialog(errMessage).ShowDialog();
                    return;
                }

                Configuration.MainModelRef.WebDriver.FindElement(By.Id("ui-id-1")).Click();
                IWebElement chatField = Configuration.MainModelRef.WebDriver.FindElement(By.Id("textchat-input")).FindElement(By.CssSelector("textarea"));
                chatField.SendKeys(message);
                chatField.SendKeys("\n");
            }
            catch (Exception e)
            {
                characterRef.OutputLinkedToRoll20 = false;
                AddToPlayerLog(characterRef.Name + " has disconnected from Roll20.");
                new NotificationDialog(e.Message).ShowDialog();
            }
        }
        public static bool SwitchRoll20ChatAsCurrent()
        {
            IWebDriver driverRef = Configuration.MainModelRef.WebDriver;
            CharacterModel characterRef = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;

            try
            {

                string errMessage = "";

                if (driverRef == null) { characterRef.OutputLinkedToRoll20 = false; errMessage += "\nNo Web Driver detected."; }
                if (driverRef.CurrentWindowHandle == null) { characterRef.OutputLinkedToRoll20 = false; errMessage += "\nNo window handle available."; }
                if (driverRef.FindElement(By.Id("speakingas")) == null) { characterRef.OutputLinkedToRoll20 = false; errMessage += "\nCannot find Speaking As dropdown."; }

                if (errMessage != "")
                {
                    new NotificationDialog(errMessage).ShowDialog();
                    return false;
                }

                IWebElement spkAs = driverRef.FindElement(By.Id("speakingas"));
                spkAs.Click();
                spkAs.SendKeys(characterRef.Name.Split()[0]);
                spkAs.SendKeys("\n");
                return true;

            }
            catch (Exception e)
            {
                //characterRef.LinkOutputToRoll20 = false;
                string message = e.Message;
                if (message.Contains("Unable to locate element") && message.Contains("#speakingas")) { message = "Unable to interact with Roll20 chat, please ensure that you are fully logged into the Roll20 tabletop game you wish to join."; }
                new NotificationDialog(message).ShowDialog();
                return false;
            }

        }
        public static void InteractWithWebElement(List<WebActionModel> webActions)
        {
            IWebDriver driverRef = Configuration.MainModelRef.WebDriver;
            try
            {
                if (driverRef == null) { new NotificationDialog("No Web Driver detected.").ShowDialog(); return; }
                if (driverRef.CurrentWindowHandle == null) { new NotificationDialog("No window handle available.").ShowDialog(); return; }

                foreach (WebActionModel webAction in webActions)
                {
                    ReadOnlyCollection<IWebElement> webElements = webAction.TargetElementHandle switch
                    {
                        "ID" => driverRef.FindElements(By.Id(webAction.TargetElementMatchText)),
                        "Class" => driverRef.FindElements(By.ClassName(webAction.TargetElementMatchText)),
                        "Link Text" => driverRef.FindElements(By.PartialLinkText(webAction.TargetElementMatchText)),
                        _ => null
                    };
                    if (webElements == null) { new NotificationDialog("Invalid or missing Target Element Handle for Web Action.").ShowDialog(); return; }
                    if (webElements.Count() == 0) { new NotificationDialog("Invalid or missing Target Element Handle for Web Action.").ShowDialog(); return; }
                    IWebElement webElement = webElements[webAction.ElementMatchIteration];
                    switch (webAction.InteractionType)
                    {
                        case "Click":
                            webElement.Click();
                            break;
                        case "Text Input":
                            webElement.SendKeys(webAction.TextInputValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                new NotificationDialog(e.Message).ShowDialog();
            }
        }
        public static void NotifyUser(string message)
        {
            new NotificationDialog(message).ShowDialog();
        }
        public static void WriteToLogFile(string message, bool notifyUser = false)
        {
            if (notifyUser) { new NotificationDialog(message).ShowDialog(); }
            File.AppendAllText("log.txt", DateTime.Now + ": " + message + "\n");
        }
        public static void ClearLogFile(bool notifyUser = false)
        {
            File.WriteAllText("log.txt", "");
            if (notifyUser) { new NotificationDialog("Log file cleared.").ShowDialog(); }
        }
        public static Style GetStyle(string name)
        {
            return Configuration.framework.FindResource(name) as Style;
        }
        public static int GetAttributeModifier(int attributeScore)
        {
            return attributeScore switch
            {
                1 => -5,
                2 => -4,
                3 => -4,
                4 => -3,
                5 => -3,
                6 => -2,
                7 => -2,
                8 => -1,
                9 => -1,
                10 => 0,
                11 => 0,
                12 => 1,
                13 => 1,
                14 => 2,
                15 => 2,
                16 => 3,
                17 => 3,
                18 => 4,
                19 => 4,
                20 => 5,
                21 => 5,
                22 => 6,
                23 => 6,
                24 => 7,
                25 => 7,
                26 => 8,
                27 => 8,
                28 => 9,
                29 => 9,
                30 => 10,
                _ => 0
            };
        }
        public static int GetProfBonusFromCr(string cr)
        {
            return cr switch
            {
                "5" => 3,
                "6" => 3,
                "7" => 3,
                "8" => 3,
                "9" => 4,
                "10" => 4,
                "11" => 4,
                "12" => 4,
                "13" => 5,
                "14" => 5,
                "15" => 5,
                "16" => 5,
                "17" => 6,
                "18" => 6,
                "19" => 6,
                "20" => 6,
                "21" => 7,
                "22" => 7,
                "23" => 7,
                "24" => 7,
                "25" => 8,
                "26" => 8,
                "27" => 8,
                "28" => 8,
                "29" => 9,
                "30" => 9,
                _ => 2
            };
        }
        public static int GetAttributeScore(string attribute, CreatureModel Creature)
        {
            foreach (PropertyInfo propertyInfo in Creature.GetType().GetProperties())
            {
                if (propertyInfo.Name.Contains("Attr_" + attribute))
                {
                    return Convert.ToInt32(propertyInfo.GetValue(Creature));
                }
            }
            return 0;
        }
        public static int GetPointCostFromScore(int score)
        {
            return score switch
            {
                8 => 0,
                9 => 1,
                10 => 2,
                11 => 3,
                12 => 4,
                13 => 5,
                14 => 7,
                15 => 9,
                _ => 100
            };
        }
        public static int GetSkillModifier(string skill, CreatureModel Creature)
        {
            int baseAttrMod = skill switch
            {
                "Acrobatics" => GetAttributeModifier(Creature.Attr_Dexterity),
                "Animal Handling" => GetAttributeModifier(Creature.Attr_Wisdom),
                "Arcana" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Athletics" => GetAttributeModifier(Creature.Attr_Strength),
                "Deception" => GetAttributeModifier(Creature.Attr_Charisma),
                "History" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Insight" => GetAttributeModifier(Creature.Attr_Wisdom),
                "Intimidation" => GetAttributeModifier(Creature.Attr_Charisma),
                "Investigation" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Medicine" => GetAttributeModifier(Creature.Attr_Wisdom),
                "Nature" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Perception" => GetAttributeModifier(Creature.Attr_Wisdom),
                "Performance" => GetAttributeModifier(Creature.Attr_Charisma),
                "Persuasion" => GetAttributeModifier(Creature.Attr_Charisma),
                "Religion" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Sleight of Hand" => GetAttributeModifier(Creature.Attr_Dexterity),
                "Stealth" => GetAttributeModifier(Creature.Attr_Dexterity),
                "Survival" => GetAttributeModifier(Creature.Attr_Wisdom),
                _ => 0
            };
            foreach (PropertyInfo propertyInfo in Creature.GetType().GetProperties())
            {
                if (propertyInfo.Name.Contains("IsProf_" + skill) || propertyInfo.Name.Contains("IsExpert_" + skill))
                {
                    bool.TryParse(propertyInfo.GetValue(Creature).ToString(), out bool isProf);
                    if (isProf)
                    {
                        baseAttrMod += Creature.ProficiencyBonus;
                    }
                }
            }
            return baseAttrMod;
        }
        public static int GetAttributeCheck(string ability, CreatureModel Creature)
        {
            int abilityMod = ability switch
            {
                "Strength" => GetAttributeModifier(Creature.Attr_Strength),
                "Dexterity" => GetAttributeModifier(Creature.Attr_Dexterity),
                "Constitution" => GetAttributeModifier(Creature.Attr_Constitution),
                "Intelligence" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Wisdom" => GetAttributeModifier(Creature.Attr_Wisdom),
                "Charisma" => GetAttributeModifier(Creature.Attr_Charisma),
                _ => 0
            };
            return abilityMod;
        }
        public static int GetSaveModifier(string ability, CreatureModel Creature)
        {
            int baseSaveMod = ability switch
            {
                "Strength" => GetAttributeModifier(Creature.Attr_Strength),
                "Dexterity" => GetAttributeModifier(Creature.Attr_Dexterity),
                "Constitution" => GetAttributeModifier(Creature.Attr_Constitution),
                "Intelligence" => GetAttributeModifier(Creature.Attr_Intelligence),
                "Wisdom" => GetAttributeModifier(Creature.Attr_Wisdom),
                "Charisma" => GetAttributeModifier(Creature.Attr_Charisma),
                _ => 0
            };
            foreach (PropertyInfo propertyInfo in Creature.GetType().GetProperties())
            {
                if (propertyInfo.Name.Contains("HasSave_" + ability))
                {
                    bool.TryParse(propertyInfo.GetValue(Creature).ToString(), out bool hasSave);
                    if (hasSave)
                    {
                        baseSaveMod += Creature.ProficiencyBonus;
                    }
                }
            }
            return baseSaveMod;
        }
        public static string GetFormattedDiceRolls(List<string> rolls)
        {
            string output = "[";
            for (int i = 0; i < rolls.Count(); i++)
            {
                if (i > 0) { output += " + "; }
                output += rolls[i];
            }
            output += "]";
            return output;
        }
        public static string GetFormattedModifiers(List<string> mods)
        {
            string output = "";
            for (int i = 0; i < mods.Count(); i++)
            {
                if (i > 0) { output += " + "; }
                output += mods[i];
            }
            return output;
        }
        public static bool CheckForSpellSlots(ref CharacterModel caster, int spellLevel)
        {
            switch (spellLevel)
            {
                case 1:
                    if (caster.L1SpellsAvailable <= 0) { return false; }
                    else { caster.L1SpellsAvailable--; }
                    break;
                case 2:
                    if (caster.L2SpellsAvailable <= 0) { return false; }
                    else { caster.L2SpellsAvailable--; }
                    break;
                case 3:
                    if (caster.L3SpellsAvailable <= 0) { return false; }
                    else { caster.L3SpellsAvailable--; }
                    break;
                case 4:
                    if (caster.L4SpellsAvailable <= 0) { return false; }
                    else { caster.L4SpellsAvailable--; }
                    break;
                case 5:
                    if (caster.L5SpellsAvailable <= 0) { return false; }
                    else { caster.L5SpellsAvailable--; }
                    break;
                case 6:
                    if (caster.L6SpellsAvailable <= 0) { return false; }
                    else { caster.L6SpellsAvailable--; }
                    break;
                case 7:
                    if (caster.L7SpellsAvailable <= 0) { return false; }
                    else { caster.L7SpellsAvailable--; }
                    break;
                case 8:
                    if (caster.L8SpellsAvailable <= 0) { return false; }
                    else { caster.L8SpellsAvailable--; }
                    break;
                case 9:
                    if (caster.L9SpellsAvailable <= 0) { return false; }
                    else { caster.L9SpellsAvailable--; }
                    break;
                default:
                    break;
            }

            return true;

        }
        public static bool CheckForSpellSlots(ref CreatureModel caster, int spellLevel)
        {
            switch (spellLevel)
            {
                case 1:
                    if (caster.L1SpellsAvailable <= 0) { return false; }
                    else { caster.L1SpellsAvailable--; }
                    break;
                case 2:
                    if (caster.L2SpellsAvailable <= 0) { return false; }
                    else { caster.L2SpellsAvailable--; }
                    break;
                case 3:
                    if (caster.L3SpellsAvailable <= 0) { return false; }
                    else { caster.L3SpellsAvailable--; }
                    break;
                case 4:
                    if (caster.L4SpellsAvailable <= 0) { return false; }
                    else { caster.L4SpellsAvailable--; }
                    break;
                case 5:
                    if (caster.L5SpellsAvailable <= 0) { return false; }
                    else { caster.L5SpellsAvailable--; }
                    break;
                case 6:
                    if (caster.L6SpellsAvailable <= 0) { return false; }
                    else { caster.L6SpellsAvailable--; }
                    break;
                case 7:
                    if (caster.L7SpellsAvailable <= 0) { return false; }
                    else { caster.L7SpellsAvailable--; }
                    break;
                case 8:
                    if (caster.L8SpellsAvailable <= 0) { return false; }
                    else { caster.L8SpellsAvailable--; }
                    break;
                case 9:
                    if (caster.L9SpellsAvailable <= 0) { return false; }
                    else { caster.L9SpellsAvailable--; }
                    break;
                default:
                    break;
            }

            return true;

        }
        public static int GetXpFromLevel(int level)
        {
            return level switch
            {
                2 => 300,
                3 => 900,
                4 => 2700,
                5 => 6500,
                6 => 14000,
                7 => 23000,
                8 => 34000,
                9 => 48000,
                10 => 64000,
                11 => 85000,
                12 => 100000,
                13 => 120000,
                14 => 140000,
                15 => 165000,
                16 => 195000,
                17 => 225000,
                18 => 265000,
                19 => 305000,
                20 => 355000,
                _ => 0
            };
        }
        public static int RollD20(bool useAdvantage = false, bool useDisadvantage = false)
        {
            int firstRoll = Configuration.RNG.Next(1, 21);
            int secondRoll = Configuration.RNG.Next(1, 21);
            if (useAdvantage)
            {
                return (firstRoll > secondRoll) ? firstRoll : secondRoll;
            }
            else if (useDisadvantage)
            {
                return (firstRoll < secondRoll) ? firstRoll : secondRoll;
            }
            else
            {
                return firstRoll;
            }
        }
        public static bool SaveToXml(IEnumerable itemList, string setName, string dataPath, bool showNotification = false)
        {
            try
            {
                bool hasItems = false;
                foreach (object i in itemList)
                {
                    hasItems = true;
                    break;
                }
                if (hasItems == false)
                {
                    // Prevents zero item save crash
                    XDocument blankDoc = new();
                    blankDoc.Add(new XElement(setName));
                    blankDoc.Save(dataPath);
                }
                else
                {
                    XDocument itemDocument = new();
                    itemDocument.Add(XmlMethods.ListToXml(itemList));
                    itemDocument.Save(dataPath);
                }
            }
            catch (Exception e)
            {
                WriteToLogFile(e.Message, true);
                return false;
            }
            if (showNotification)
            {
                new NotificationDialog("Data Saved to " + dataPath.Split('/')[1] + ".").ShowDialog();
            }
            return true;
        }
        public static string GetModifierSymbol(int num)
        {
            return (num < 0) ? "" : "+";
        }
        public static bool DoesStartWithVowel(string word)
        {
            List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            return vowels.Contains(word[0]);
        }
        public static string GetSuffixFromNumber(int number)
        {
            if (number.ToString().Length >= 2)
            {
                string lastNums = number.ToString().Substring(number.ToString().Length - 2);
                if (lastNums == "11" || lastNums == "12" || lastNums == "13") { return "th"; }
            }
            int lastDigit = Convert.ToInt32(number.ToString().Last().ToString());
            return lastDigit switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        public static string GetSuffixFromNumber(long number)
        {
            if (number.ToString().Length >= 2)
            {
                string lastNums = number.ToString().Substring(number.ToString().Length - 2);
                if (lastNums == "11" || lastNums == "12" || lastNums == "13") { return "th"; }
            }
            int lastDigit = Convert.ToInt32(number.ToString().Last().ToString());
            return lastDigit switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
        public static string GetStringFromList(List<string> list, string separator = "\n\n")
        {
            string fullText = "";
            for (int i = 0; i < list.Count(); i++)
            {
                if (i + 1 < list.Count())
                {
                    fullText += list[i] + separator;
                }
                else
                {
                    fullText += list[i];
                }
            }
            return fullText;
        }
        public static string GetStringFromList(List<int> list, string separator = "\n\n")
        {
            string fullText = "";
            for (int i = 0; i < list.Count(); i++)
            {
                if (i + 1 < list.Count())
                {
                    fullText += list[i].ToString() + separator;
                }
                else
                {
                    fullText += list[i].ToString();
                }
            }
            return fullText;
        }
        public static string GetStringFromDictionary(SortedDictionary<string, int> dictionary, string spacer = " : ", string separator = "\n")
        {
            string fullText = "";
            if (dictionary.Count() <= 0) { return fullText; }
            string firstKey = dictionary.First().Key;
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                if (pair.Key != firstKey) 
                {
                    fullText += separator;
                }
                fullText += pair.Key + spacer + pair.Value.ToString();
            }
            return fullText;
        }
        public static string GetStringFromDictionary(SortedDictionary<int, int> dictionary, string header, string spacer = " : ", string separator = "\n")
        {
            string fullText = "";
            if (dictionary.Count() <= 0) { return fullText; }
            int firstKey = dictionary.First().Key;
            foreach (KeyValuePair<int, int> pair in dictionary)
            {
                if (pair.Key != firstKey)
                {
                    fullText += separator;
                }
                fullText += header + pair.Key + spacer + pair.Value.ToString();
            }
            return fullText;
        }
        public static void RollDice(int qty, int sides, out int result, out List<string> rolls)
        {
            rolls = new();
            result = 0;
            for (int i = 0; i < qty; i++)
            {
                int roll = Configuration.RNG.Next(1, sides + 1);
                rolls.Add(roll.ToString());
                result += roll;
            }
            
        }
        public static string PadNumbers(string input)
        {
            // https://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }
        public static string GetDerivedCoinage(int rawValue)
        {
            int plat;
            int gold;
            int silver;
            int copper;
            string processedValue;

            plat = (Configuration.MainModelRef.SettingsView.UsePlatinum) ? rawValue / 1000 : 0;
            gold = (rawValue - (plat * 1000)) / 100;
            silver = (rawValue - (plat * 1000) - (gold * 100)) / 10;
            copper = rawValue - (plat * 1000) - (gold * 100) - (silver * 10);

            processedValue = string.Format("{0}{1}{2}{3}",
                (plat > 0) ? plat + "pp " : "",
                (gold > 0) ? gold + "gp " : "",
                (silver > 0) ? silver + "sp " : "",
                (copper > 0) ? copper + "cp" : "");

            if (rawValue == 0) { processedValue = "--"; }
            return processedValue;

        }
        public static string GetSpellDetailsTooltip(SpellModel spell)
        {
            string tooltip = "\n";

            //tooltip += spell.Name + "\n";
            if (spell.SpellLevel == 0)
            {
                tooltip += spell.SchoolOfMagic + " Cantrip\n";
            }
            else
            {
                tooltip += "Level " + spell.SpellLevel + " " + spell.SchoolOfMagic + " Spell\n";
            }
            tooltip += spell.Range + ", " + spell.CastingTime + ", " + spell.SpellDuration + " (";
            if (spell.RequiresVerbal) { tooltip += "V"; }
            if (spell.RequiresSomatic) { tooltip += "S"; }
            if (spell.RequiresMaterial) { tooltip += "M*"; }
            tooltip += ")\n\n";
            tooltip += spell.Description + "\n\n";
            if (spell.RequiresMaterial) { tooltip += "* - (" + spell.Materials + ")"; }

            return tooltip;
        }
        public static List<ConvertibleValue> GetConvertibleValueList(List<string> values)
        {
            List<ConvertibleValue> cvList = new();
            foreach (string val in values)
            {
                cvList.Add(new(val));
            }
            return cvList;
        }
        public static int GetAverageOfDice(int diceQty, int diceSides, int diceMod)
        {
            return ((diceQty * diceSides) / 2) + (diceQty / 2) + diceMod;
        }
        public static int CalculateDamage(bool pass, EncounterMultiTargetDialog attack, CreatureModel creature)
        {
            int primaryDamage = attack.PrimaryDamageOnFail;
            int secondaryDamage = attack.SecondaryDamageOnFail;

            if (pass)
            {
                if (attack.HalfOnSave)
                {
                    primaryDamage /= 2;
                    secondaryDamage /= 2;
                }
                else
                {
                    return 0;
                }
            }

            primaryDamage = attack.PrimaryDamageType switch
            {
                "Acid" => AdjustDamageFromProclivity(creature.DamageProclivity_Acid, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Cold" => AdjustDamageFromProclivity(creature.DamageProclivity_Cold, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Fire" => AdjustDamageFromProclivity(creature.DamageProclivity_Fire, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Force" => AdjustDamageFromProclivity(creature.DamageProclivity_Force, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Lightning" => AdjustDamageFromProclivity(creature.DamageProclivity_Lightning, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Necrotic" => AdjustDamageFromProclivity(creature.DamageProclivity_Necrotic, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Poison" => AdjustDamageFromProclivity(creature.DamageProclivity_Poison, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Psychic" => AdjustDamageFromProclivity(creature.DamageProclivity_Psychic, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Radiant" => AdjustDamageFromProclivity(creature.DamageProclivity_Radiant, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Thunder" => AdjustDamageFromProclivity(creature.DamageProclivity_Thunder, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Bludgeoning" => AdjustDamageFromProclivity(creature.DamageProclivity_Bludgeoning, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Slashing" => AdjustDamageFromProclivity(creature.DamageProclivity_Slashing, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Piercing" => AdjustDamageFromProclivity(creature.DamageProclivity_Piercing, primaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                _ => primaryDamage
            };

            secondaryDamage = attack.SecondaryDamageType switch
            {
                "Acid" => AdjustDamageFromProclivity(creature.DamageProclivity_Acid, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Cold" => AdjustDamageFromProclivity(creature.DamageProclivity_Cold, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Fire" => AdjustDamageFromProclivity(creature.DamageProclivity_Fire, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Force" => AdjustDamageFromProclivity(creature.DamageProclivity_Force, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Lightning" => AdjustDamageFromProclivity(creature.DamageProclivity_Lightning, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Necrotic" => AdjustDamageFromProclivity(creature.DamageProclivity_Necrotic, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Poison" => AdjustDamageFromProclivity(creature.DamageProclivity_Poison, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Psychic" => AdjustDamageFromProclivity(creature.DamageProclivity_Psychic, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Radiant" => AdjustDamageFromProclivity(creature.DamageProclivity_Radiant, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Thunder" => AdjustDamageFromProclivity(creature.DamageProclivity_Thunder, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Bludgeoning" => AdjustDamageFromProclivity(creature.DamageProclivity_Bludgeoning, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Slashing" => AdjustDamageFromProclivity(creature.DamageProclivity_Slashing, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                "Piercing" => AdjustDamageFromProclivity(creature.DamageProclivity_Piercing, secondaryDamage, attack.IsMagicWeapon, attack.IsAdamantineWeapon, attack.IsSilveredWeapon),
                _ => secondaryDamage
            };

            return primaryDamage + secondaryDamage;

        }
        public static int AdjustDamageFromProclivity(string proclivity, int damage, bool isMagicWep, bool isAdamWep, bool isSilverWep)
        {
            if (proclivity == "Vulnerable") { return damage * 2; }
            if (proclivity == "Resistant") { return damage / 2; }
            if (proclivity == "Immune") { return 0; }
            if (proclivity == "Resistant if Non-Magical" && isMagicWep == false) { return damage / 2; }
            if (proclivity == "Resistant if Non-Adamantine" && (isMagicWep == false && isAdamWep == false)) { return damage / 2; }
            if (proclivity == "Resistant if Non-Silvered" && (isMagicWep == false && isSilverWep == false)) { return damage / 2; }
            if (proclivity == "Immune if Non-Magical" && isMagicWep == false) { return 0; }
            if (proclivity == "Immune if Non-Adamantine" && (isMagicWep == false && isAdamWep == false)) { return 0; }
            if (proclivity == "Immune if Non-Silvered" && (isMagicWep == false && isSilverWep == false)) { return 0; }
            return damage;
        }
        public static void CheckNoteSearch(ObservableCollection<NoteModel> notes, string text, bool? useCaseMatch, bool? searchHeader, bool? searchContent, out bool matchFound)
        {
            matchFound = false;

            foreach (NoteModel note in notes)
            {
                note.IsSearchMatch = false;
                note.IsExpanded = false;

                if (searchHeader == true)
                {
                    if (useCaseMatch == true)
                    {
                        if (note.Header.Contains(text)) { note.IsSearchMatch = true; }
                    }
                    else
                    {
                        if (note.Header.ToUpper().Contains(text.ToUpper())) { note.IsSearchMatch = true; }
                    }
                }
                if (searchContent == true)
                {
                    if (useCaseMatch == true)
                    {
                        if (note.Content.Contains(text)) { note.IsSearchMatch = true; }
                    }
                    else
                    {
                        if (note.Content.ToUpper().Contains(text.ToUpper())) { note.IsSearchMatch = true; }
                    }
                }

                if (note.IsSearchMatch == true) { matchFound = true; }

                if (note.SubNotes.Count() > 0)
                {
                    CheckNoteSearch(note.SubNotes, text, useCaseMatch, searchHeader, searchContent, out bool subMatchFound);
                    if (subMatchFound == true) { note.IsExpanded = true; matchFound = true; }
                }

            }

        }
        public static void ClearNoteSearch(ObservableCollection<NoteModel> notes)
        {
            foreach (NoteModel note in notes)
            {
                note.IsExpanded = false;
                note.IsSearchMatch = false;
                if (note.SubNotes.Count() > 0)
                {
                    ClearNoteSearch(note.SubNotes);
                }
            }
        }
        public static void PlaySystemAudio(string filepath)
        {
            if (Configuration.MainModelRef.SettingsView.EnableSoundEffects == false) { return; }
            if ((filepath == Configuration.SystemAudio_DiceRoll || filepath == Configuration.SystemAudio_MenuSelect) && Configuration.MainModelRef.SettingsView.EnableSfx_DiceRoll == false) { return; }
            if (filepath == Configuration.SystemAudio_ShopItemMove && Configuration.MainModelRef.SettingsView.EnableSfx_ShopItemMove == false) { return; }
            if (filepath == Configuration.SystemAudio_ShopGreeting && Configuration.MainModelRef.SettingsView.EnableSfx_ShopGreeting == false) { return; }
            if (filepath == Configuration.SystemAudio_ShopFarewell && Configuration.MainModelRef.SettingsView.EnableSfx_ShopGreeting == false) { return; }
            Configuration.MainModelRef.AudioView.DoChangeSystemAudio(filepath);
        }
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                
                return (T)formatter.Deserialize(ms);
            }
        }

    }
}