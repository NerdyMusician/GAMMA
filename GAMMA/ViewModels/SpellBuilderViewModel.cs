using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class SpellBuilderViewModel : BaseModel
    {
        // Constructors
        public SpellBuilderViewModel()
        {
            XmlMethods.XmlToList(Configuration.SpellDataFilePath, out List<SpellModel> spells);
            AllSpells = new ObservableCollection<SpellModel>(spells);

            // Update to 1.26 CustomAbility
            foreach (SpellModel spell in AllSpells)
            {
                if (spell.PrimaryAbilities.Count() > 0) { continue; } // Assumes it has already been translated
                if (spell.IsValidated == false) { continue; } // Assumes it is unfinished spell

                // Start Building Primary Ability
                spell.PrimaryAbilities.Add(new());
                CustomAbility ability = spell.PrimaryAbilities.Last();
                ability.Name = spell.Name;
                ability.Type = "Spell";
                ability.Output = "";

                if (spell.HasAttackStat)
                {
                    ability.QuantityToPerform = spell.NumberOfAttacks;
                    ability.DoesQuantityScale = spell.HasTargetScaling;
                    ability.ScaleRate = spell.AttackTargetScale;
                    if (spell.IsAutoHit == false)
                    {
                        ability.Variables.Add(new() { Name = "Attack", Type = "Number" });
                        ability.PreActions.Add(new() { Action = "Make Attack Roll", Target = "Attack", AttackAttribute = "Spellcasting", UseProficiencyBonus = true });
                        //ability.PreActions.Add(new() { Action = "Add Roll", Target = "Attack", DiceQuantity = 1, DiceQuality = 20 });
                        //ability.PreActions.Add(new() { Action = "Add Stat Value", Target = "Attack", StatValue = "Spellcasting Attack Modifier" });
                    }
                    if (spell.DamageType is not "" and not null)
                    {
                        string dmgName = spell.DamageType + " Damage";
                        ability.Variables.Add(new() { Name = dmgName, Type = "Number" });
                        if (spell.HasAltAttackDamage) 
                        { 
                            ability.Variables.Add(new() { Name = "Attack Question", Type = "Text" });
                            ability.PreActions.Add(new() { Action = "QA Prompt", Target = "Attack Question", Question = spell.AltDamageDiceCondition, Answers = new() { new("Yes"), new("No") } });
                            if (spell.DamageDiceQuantity > 0) 
                            {
                                ability.PreActions.Add(new()
                                {
                                    Action = "Add Roll",
                                    Target = dmgName,
                                    Conditions = new()
                                    {
                                        new CACondition()
                                        {
                                            ConditionVariable = "Attack Question",
                                            ConditionType = "Equal To",
                                            ConditionValue = "No"
                                        }
                                    },
                                    DiceQuantity = spell.DamageDiceQuantity,
                                    DiceQuality = spell.DamageDiceQuality,
                                    HasValueScaling = spell.HasAttackDamageScaling,
                                    ValueScaleRate = spell.AttackDamageScale,
                                    DoesDoubleOnCritical = true
                                });
                            }
                            if (spell.AltDamageDiceQuantity > 0)
                            {
                                ability.PreActions.Add(new()
                                {
                                    Action = "Add Roll",
                                    Target = dmgName,
                                    Conditions = new()
                                    {
                                        new CACondition()
                                        {
                                            ConditionVariable = "Attack Question",
                                            ConditionType = "Equal To",
                                            ConditionValue = "Yes"
                                        }
                                    },
                                    DiceQuantity = spell.AltDamageDiceQuantity,
                                    DiceQuality = spell.AltDamageDiceQuality,
                                    HasValueScaling = spell.HasAttackDamageScaling,
                                    ValueScaleRate = spell.AttackDamageScale,
                                    DoesDoubleOnCritical = true
                                });
                            }
                        }
                        else
                        {
                            if (spell.DamageDiceQuantity > 0) 
                            {
                                ability.PreActions.Add(new() 
                                {
                                    Action = "Add Roll",
                                    Target = dmgName,
                                    DiceQuantity = spell.DamageDiceQuantity,
                                    DiceQuality = spell.DamageDiceQuality,
                                    HasValueScaling = spell.HasAttackDamageScaling,
                                    ValueScaleRate = spell.AttackDamageScale,
                                    DoesDoubleOnCritical = true
                                });
                            }
                        }
                        if (spell.DamageDiceModifier > 0) { ability.PreActions.Add(new() { Action = "Add Set Value", Target = dmgName, SetValue = spell.DamageDiceModifier.ToString() }); }
                        if (spell.UsesSpellcastingMod_Healing) { ability.PreActions.Add(new() { Action = "Add Stat Value", Target = dmgName, StatValue = "Spellcasting Ability Modifier" }); }
                    }
                }
                if (spell.HasHealingStat)
                {
                    ability.Variables.Add(new() { Name = "Healing", Type = "Number" });
                    if (spell.HasAltHealingDice)
                    {
                        ability.Variables.Add(new() { Name = "Healing Question", Type = "Text" });
                        ability.PreActions.Add(new() { Action = "QA Prompt", Target = "Healing Question", Question = spell.AltHealingDiceCondition, Answers = new() { new("Yes"), new("No") } });
                        if (spell.HealingDiceQuantity > 0)
                        {
                            ability.PreActions.Add(new()
                            {
                                Action = "Add Roll",
                                Target = "Healing",
                                Conditions = new()
                                {
                                    new CACondition()
                                    {
                                        ConditionVariable = "Healing Question",
                                        ConditionType = "Equal To",
                                        ConditionValue = "No"
                                    }
                                },
                                DiceQuantity = spell.HealingDiceQuantity,
                                DiceQuality = spell.HealingDiceQuality,
                                HasValueScaling = spell.HasHealingScaling,
                                ValueScaleRate = spell.HealingScale
                            });
                        }
                        if (spell.AltDamageDiceQuantity > 0)
                        {
                            ability.PreActions.Add(new()
                            {
                                Action = "Add Roll",
                                Target = "Healing",
                                Conditions = new()
                                {
                                    new CACondition()
                                    {
                                        ConditionVariable = "Healing Question",
                                        ConditionType = "Equal To",
                                        ConditionValue = "Yes"
                                    }
                                },
                                DiceQuantity = spell.AltHealingDiceQuantity,
                                DiceQuality = spell.AltHealingDiceQuality,
                                HasValueScaling = spell.HasHealingScaling,
                                ValueScaleRate = spell.HealingScale
                            });
                        }
                    }
                    else
                    {
                        if (spell.HealingDiceQuantity > 0) 
                        {
                            ability.PreActions.Add(new() 
                            {
                                Action = "Add Roll",
                                Target = "Healing",
                                DiceQuantity = spell.HealingDiceQuantity,
                                DiceQuality = spell.HealingDiceQuality,
                                HasValueScaling = spell.HasHealingScaling,
                                ValueScaleRate = spell.HealingScale
                            });
                        }
                    }
                    if (spell.DamageDiceModifier > 0) { ability.PreActions.Add(new() { Action = "Add Set Value", Target = "Healing", SetValue = spell.HealingDiceModifier.ToString() }); }
                    if (spell.UsesSpellcastingMod_Damage) { ability.PreActions.Add(new() { Action = "Add Stat Value", Target = "Healing", StatValue = "Spellcasting Ability Modifier" }); }

                }
                if (spell.HasSaveStat)
                {
                    ability.Output += "Target(s) must make a DC {Save DC} " + spell.SaveAbility + " save.";
                    ability.Output += "\n" + spell.SaveEffect;

                    ability.Variables.Add(new() { Name = "Save DC", Type = "Number" });
                    ability.PreActions.Add(new() { Action = "Add Stat Value", Target = "Save DC", StatValue = "Spellcasting Save DC" });

                    if (spell.SaveDamageType is not "" and not null)
                    {
                        string dmgName = spell.SaveDamageType + " Damage";
                        ability.Variables.Add(new() { Name = dmgName, Type = "Number", IncludeHalfValue = spell.IsHalfDamageOnSave });
                        if (spell.HasAltSaveDamage)
                        {
                            ability.Variables.Add(new() { Name = "Save Question", Type = "Text" });
                            ability.PreActions.Add(new() { Action = "QA Prompt", Target = "Save Question", Question = spell.AltSaveDamageCondition, Answers = new() { new("Yes"), new("No") } });
                            if (spell.SaveDamageDiceQuantity > 0)
                            {
                                ability.PreActions.Add(new()
                                {
                                    Action = "Add Roll",
                                    Target = dmgName,
                                    Conditions = new()
                                    {
                                        new CACondition()
                                        {
                                            ConditionVariable = "Save Question",
                                            ConditionType = "Equal To",
                                            ConditionValue = "No"
                                        }
                                    },
                                    DiceQuantity = spell.SaveDamageDiceQuantity,
                                    DiceQuality = spell.SaveDamageDiceQuality,
                                    HasValueScaling = spell.HasSaveDamageScaling,
                                    ValueScaleRate = spell.SaveDamageScale
                                });
                            }
                            if (spell.AltSaveDamageDiceQuantity > 0)
                            {
                                ability.PreActions.Add(new()
                                {
                                    Action = "Add Roll",
                                    Target = dmgName,
                                    Conditions = new()
                                    {
                                        new CACondition()
                                        {
                                            ConditionVariable = "Save Question",
                                            ConditionType = "Equal To",
                                            ConditionValue = "Yes"
                                        }
                                    },
                                    DiceQuantity = spell.AltSaveDamageDiceQuantity,
                                    DiceQuality = spell.AltSaveDamageDiceQuality,
                                    HasValueScaling = spell.HasSaveDamageScaling,
                                    ValueScaleRate = spell.SaveDamageScale
                                });
                            }
                        }
                        else
                        {
                            if (spell.SaveDamageDiceQuantity > 0)
                            {
                                ability.PreActions.Add(new()
                                {
                                    Action = "Add Roll",
                                    Target = dmgName,
                                    DiceQuantity = spell.SaveDamageDiceQuantity,
                                    DiceQuality = spell.SaveDamageDiceQuality,
                                    HasValueScaling = spell.HasSaveDamageScaling,
                                    ValueScaleRate = spell.SaveDamageScale
                                });
                            }
                        }
                        if (spell.SaveDamageDiceModifier > 0) { ability.PreActions.Add(new() { Action = "Add Set Value", Target = dmgName, SetValue = spell.SaveDamageDiceModifier.ToString() }); }
                        if (spell.UsesSpellcastingMod_Save) { ability.PreActions.Add(new() { Action = "Add Stat Value", Target = dmgName, StatValue = "Spellcasting Ability Modifier" }); }

                    }

                }
                if (spell.HasSpecialEffect)
                {
                    if (spell.SpecialEffectUsesDescription) { ability.Output += "\n" + spell.Description; }
                    else { ability.Output += "\n" + spell.SpecialEffect; }
                }
                if (spell.DoesCreateActiveEffects)
                {
                    foreach (ActiveEffectModel activeEffect in spell.ActiveEffects)
                    {
                        spell.SecondaryAbilities.Add(new() { Name = activeEffect.Name, Description = activeEffect.Description });
                        CustomAbility secondaryAbility = spell.SecondaryAbilities.Last();
                        if (activeEffect.EffectType == "Attack")
                        {
                            secondaryAbility.Variables.Add(new() { Name = "Attack", Type = "Number" });
                            secondaryAbility.PreActions.Add(new() { Action = "Add Roll", Target = "Attack", DiceQuantity = 1, DiceQuality = 20 });
                            secondaryAbility.PreActions.Add(new() { Action = "Add Stat Value", Target = "Attack", StatValue = "Spellcasting Attack Modifier" });
                        }
                        if (activeEffect.EffectType == "Attack" || activeEffect.EffectType == "Damage")
                        {
                            string dmgName = activeEffect.DamageType + " Damage";
                            secondaryAbility.Variables.Add(new() { Name = dmgName, Type = "Number" });
                            secondaryAbility.PreActions.Add(new() 
                            { 
                                Action = "Add Roll", 
                                Target = dmgName, 
                                DiceQuantity = activeEffect.DiceQuantity, 
                                DiceQuality = activeEffect.DiceSides, 
                                HasValueScaling = activeEffect.DoDiceScale,
                                ValueScaleRate = activeEffect.DiceScale
                            });
                            if (activeEffect.UseSpellcastingMod) { ability.PreActions.Add(new() { Action = "Add Stat Value", Target = dmgName, StatValue = "Spellcasting Ability Modifier" }); }
                        }
                        if (activeEffect.EffectType == "Healing")
                        {
                            secondaryAbility.Variables.Add(new() { Name = "Healing", Type = "Number" });
                            secondaryAbility.PreActions.Add(new()
                            {
                                Action = "Add Roll",
                                Target = "Healing",
                                DiceQuantity = activeEffect.DiceQuantity,
                                DiceQuality = activeEffect.DiceSides,
                                HasValueScaling = activeEffect.DoDiceScale,
                                ValueScaleRate = activeEffect.DiceScale
                            });
                            if (activeEffect.UseSpellcastingMod) { ability.PreActions.Add(new() { Action = "Add Stat Value", Target = "Healing", StatValue = "Spellcasting Ability Modifier" }); }
                        }
                        ability.PostActions.Add(new() { Action = "Add Active Effect", ValueA = secondaryAbility.Name });

                    }
                }

            }
            
            foreach (SpellModel spell in AllSpells)
            {
                spell.UpdateAbilityDropdowns();
            }

            FilteredSpells = new ObservableCollection<SpellModel>(AllSpells.ToList());
            Configuration.SpellRepository = AllSpells.ToList();
            SpellSearchText = "";

        }

        // Databound Properties
        #region AllSpells
        private ObservableCollection<SpellModel> _AllSpells;
        public ObservableCollection<SpellModel> AllSpells
        {
            get
            {
                return _AllSpells;
            }
            set
            {
                _AllSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSpells
        private ObservableCollection<SpellModel> _FilteredSpells;
        public ObservableCollection<SpellModel> FilteredSpells
        {
            get
            {
                return _FilteredSpells;
            }
            set
            {
                _FilteredSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveSpell
        private SpellModel _ActiveSpell;
        public SpellModel ActiveSpell
        {
            get
            {
                return _ActiveSpell;
            }
            set
            {
                _ActiveSpell = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSearchText
        private string _SpellSearchText;
        public string SpellSearchText
        {
            get
            {
                return _SpellSearchText;
            }
            set
            {
                _SpellSearchText = value;
                NotifyPropertyChanged();
                UpdateSpellFilter();
            }
        }
        #endregion

        #region Count_AllSpells
        private int _Count_AllSpells;
        public int Count_AllSpells
        {
            get
            {
                return _Count_AllSpells;
            }
            set
            {
                _Count_AllSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Count_FilteredSpells
        private int _Count_FilteredSpells;
        public int Count_FilteredSpells
        {
            get
            {
                return _Count_FilteredSpells;
            }
            set
            {
                _Count_FilteredSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddSpell
        private RelayCommand _AddSpell;
        public ICommand AddSpell
        {
            get
            {
                if (_AddSpell == null)
                {
                    _AddSpell = new RelayCommand(param => DoAddSpell());
                }
                return _AddSpell;
            }
        }
        private void DoAddSpell()
        {
            AllSpells.Add(new SpellModel());
            FilteredSpells.Add(AllSpells.Last());
            ActiveSpell = FilteredSpells.Last();
        }
        #endregion
        #region SaveSpells
        private RelayCommand _SaveSpells;
        public ICommand SaveSpells
        {
            get
            {
                if (_SaveSpells == null)
                {
                    _SaveSpells = new RelayCommand(param => DoSaveSpells());
                }
                return _SaveSpells;
            }
        }
        public bool DoSaveSpells(bool notifyUser = true)
        {
            if (AllSpells.Count() == 0)
            {
                // Prevents zero spell save crash
                XDocument blankDoc = new XDocument();
                blankDoc.Add(new XElement("SpellModelSet"));
                blankDoc.Save("Data/Spells.xml");
                return true;
            }
            List<string> duplicateSpells = new List<string>();
            foreach (SpellModel spell in AllSpells)
            {
                if (AllSpells.Where(aItem => aItem.Name == spell.Name).Count() > 1)
                {
                    if (duplicateSpells.Contains(spell.Name) == false) { duplicateSpells.Add(spell.Name); }
                }
            }
            if (duplicateSpells.Count() > 0)
            {
                string message = "Duplicate spells found:\n";
                foreach (string item in duplicateSpells)
                {
                    message += item + "\n";
                }
                new NotificationDialog(message).ShowDialog();
                return false;
            }
            XDocument itemDocument = new XDocument();
            itemDocument.Add(XmlMethods.ListToXml(AllSpells.ToList()));
            itemDocument.Save("Data/Spells.xml");
            Configuration.SpellRepository = AllSpells.ToList();
            HelperMethods.WriteToLogFile("Spells Saved.", notifyUser);

            return true;
        }
        #endregion
        #region SortSpells
        private RelayCommand _SortSpells;
        public ICommand SortSpells
        {
            get
            {
                if (_SortSpells == null)
                {
                    _SortSpells = new RelayCommand(param => DoSortSpells());
                }
                return _SortSpells;
            }
        }
        private void DoSortSpells()
        {
            AllSpells = new ObservableCollection<SpellModel>(AllSpells.OrderBy(item => item.Name));
            FilteredSpells = new ObservableCollection<SpellModel>(FilteredSpells.OrderBy(item => item.Name));
        }
        #endregion
        #region ImportSpells
        private RelayCommand _ImportSpells;
        public ICommand ImportSpells
        {
            get
            {
                if (_ImportSpells == null)
                {
                    _ImportSpells = new RelayCommand(param => DoImportSpells());
                }
                return _ImportSpells;
            }
        }
        private void DoImportSpells()
        {
            OpenFileDialog openWindow = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new YesNoDialog("Prior to import, the current spell list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            if (DoSaveSpells() == false) { return; }

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Spells(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion
        #region ClearSpellSearch
        private RelayCommand _ClearSpellSearch;
        public ICommand ClearSpellSearch
        {
            get
            {
                if (_ClearSpellSearch == null)
                {
                    _ClearSpellSearch = new RelayCommand(param => DoClearSpellSearch());
                }
                return _ClearSpellSearch;
            }
        }
        private void DoClearSpellSearch()
        {
            SpellSearchText = "";
        }
        #endregion

        // Private Methods
        private void UpdateSpellFilter()
        {
            ObservableCollection<SpellModel> filteredSpells = new ObservableCollection<SpellModel>();
            foreach (SpellModel spell in AllSpells)
            {
                if (spell.Name.ToUpper().Contains(SpellSearchText.ToUpper()) == false) { continue; }
                filteredSpells.Add(spell);
            }
            FilteredSpells = new ObservableCollection<SpellModel>(filteredSpells.OrderBy(spell => spell.Name));

            Count_FilteredSpells = FilteredSpells.Count();
            Count_AllSpells = AllSpells.Count();

        }

    }
}
