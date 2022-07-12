using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models.GameplayComponents
{
    [Serializable]
    public class CustomAbility : BaseModel
    {
        // Private Constants
        private const string Mode_Creature = "Creature";
        private const string Mode_Character = "Character";
        private const string Mode_Test = "Test";

        // Constructors
        public CustomAbility()
        {
            InitializeCollections();
            Name = "New Ability";
            Type = "None";
            Output = string.Empty;
            Description = string.Empty;
            QuantityToPerform = 1;
            
        }
        public CustomAbility(int attackBonus, int damageDiceQuantity, int damageDiceSides, int damageBonus, string damageType)
        {
            InitializeCollections();
            Name = "Basic Attack";
            Type = "Melee";
            Output = string.Empty;
            Description = string.Empty;
            QuantityToPerform = 1;

            CAVariable attackVariable = new();
            CAVariable damageVariable = new();

            attackVariable.Name = "Attack";

            damageVariable.Name = $"{damageType} Damage";

            Variables.Add(attackVariable);
            Variables.Add(damageVariable);

            CAPreAction attackRoll = new();
            CAPreAction attackBonusAction = new();
            CAPreAction damageRoll = new();
            CAPreAction damageBonusAction = new();

            attackRoll.Target = attackVariable.Name;
            attackRoll.Action = "Make Attack Roll";
            attackRoll.AttackAttribute = "None";

            attackBonusAction.Target = attackVariable.Name;
            attackBonusAction.Action = "Add Set Value";
            attackBonusAction.SetValue = attackBonus.ToString();

            damageRoll.Target = damageVariable.Name;
            damageRoll.Action = "Add Roll";
            damageRoll.DiceQuantity = damageDiceQuantity;
            damageRoll.DiceQuality = damageDiceSides;
            damageRoll.DoesDoubleOnCritical = true;

            damageBonusAction.Target = damageVariable.Name;
            damageBonusAction.Action = "Add Set Value";
            damageBonusAction.SetValue = damageBonus.ToString();

            PreActions.Add(attackRoll);
            PreActions.Add(attackBonusAction);
            PreActions.Add(damageRoll);
            PreActions.Add(damageBonusAction);

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
        #region Type
        private string _Type;
        [XmlSaveMode(XSME.Single)]
        public string Type
        {
            get => _Type;
            set => SetAndNotify(ref _Type, value);
        }
        #endregion
        #region Types
        private List<string> _Types;
        public List<string> Types
        {
            get => _Types;
            set => SetAndNotify(ref _Types, value);
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
        #region Output
        private string _Output;
        [XmlSaveMode(XSME.Single)]
        public string Output
        {
            get => _Output;
            set => SetAndNotify(ref _Output, value);
        }
        #endregion

        #region QuantityToPerform
        private int _QuantityToPerform;
        [XmlSaveMode(XSME.Single)]
        public int QuantityToPerform
        {
            get => _QuantityToPerform;
            set => SetAndNotify(ref _QuantityToPerform, value);
        }
        #endregion
        #region DoesQuantityScale
        private bool _DoesQuantityScale;
        [XmlSaveMode(XSME.Single)]
        public bool DoesQuantityScale
        {
            get => _DoesQuantityScale;
            set => SetAndNotify(ref _DoesQuantityScale, value);
        }
        #endregion
        #region ScaleRate
        private int _ScaleRate;
        [XmlSaveMode(XSME.Single)]
        public int ScaleRate
        {
            get => _ScaleRate;
            set => SetAndNotify(ref _ScaleRate, value);
        }
        #endregion

        #region PresetScale
        private int _PresetScale;
        [XmlSaveMode(XSME.Single)]
        public int PresetScale
        {
            get => _PresetScale;
            set => SetAndNotify(ref _PresetScale, value);
        }
        #endregion

        #region Variables
        private ObservableCollection<CAVariable> _Variables;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CAVariable> Variables
        {
            get => _Variables;
            set => SetAndNotify(ref _Variables, value);
        }
        #endregion
        #region PreActions
        private ObservableCollection<CAPreAction> _PreActions;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CAPreAction> PreActions
        {
            get => _PreActions;
            set => SetAndNotify(ref _PreActions, value);
        }
        #endregion
        #region PostActions
        private ObservableCollection<CAPostAction> _PostActions;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CAPostAction> PostActions
        {
            get => _PostActions;
            set => SetAndNotify(ref _PostActions, value);
        }
        #endregion

        #region InEditMode
        private bool _InEditMode;
        public bool InEditMode
        {
            get => _InEditMode;
            set => SetAndNotify(ref _InEditMode, value);
        }
        #endregion

        // Commands
        #region RemoveAbility
        public ICommand RemoveAbility => new RelayCommand(DoRemoveAbility);
        private void DoRemoveAbility(object param)
        {
            if (param.GetType() == typeof(ObservableCollection<CustomAbility>))
            {
                (param as ObservableCollection<CustomAbility>).Remove(this);
            }
            if (param.GetType() == typeof(MainViewModel))
            {
                if (Configuration.MainModelRef.TabSelected_SpellBuilder)
                {
                    Configuration.MainModelRef.SpellBuilderView.ActiveSpell.PrimaryAbilities.Remove(this);
                    Configuration.MainModelRef.SpellBuilderView.ActiveSpell.SecondaryAbilities.Remove(this);
                }
                if (Configuration.MainModelRef.TabSelected_Players)
                {
                    if (param.ToString() == "Active Effects")
                    {
                        Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveEffectAbilities.Remove(this);
                    }
                    else
                    {
                        Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Abilities.Remove(this);
                    }
                }
                if (Configuration.MainModelRef.TabSelected_CreatureBuilder)
                {
                    Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Abilities.Remove(this);
                }
            }
            // TODO - removal of active effects from creatures
        }
        #endregion
        #region RemoveEffect
        public ICommand RemoveEffect => new RelayCommand(DoRemoveEffect);
        private void DoRemoveEffect(object param)
        {
            if (param.GetType() == typeof(CreatureModel))
            {
                (param as CreatureModel).ActiveEffectAbilities.Remove(this);
                return;
            }
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveEffectAbilities.Remove(this);
        }
        #endregion
        #region AddVariable
        public ICommand AddVariable => new RelayCommand(DoAddVariable);
        private void DoAddVariable(object param)
        {
            Variables.Add(new());
        }
        #endregion
        #region AddPreAction
        public ICommand AddPreAction => new RelayCommand(DoAddPreAction);
        private void DoAddPreAction(object param)
        {
            CAPreAction newPreAction = new();
            foreach (CAVariable v in Variables)
            {
                newPreAction.Targets.Add(v.Name);
            }
            PreActions.Add(newPreAction);
        }
        #endregion
        #region AddPostAction
        public ICommand AddPostAction => new RelayCommand(DoAddPostAction);
        private void DoAddPostAction(object param)
        {
            PostActions.Add(new());
        }
        #endregion
        #region UseAbility
        public ICommand UseAbility => new RelayCommand(DoUseAbility);
        private void DoUseAbility(object param)
        {
            if (param.GetType() == typeof(MainViewModel))
            {
                if (Configuration.MainModelRef.TabSelected_SpellBuilder) // Test button on spells
                {
                    ProcessAbility(null, null, "Normal", 1, out string message, out _);
                    HelperMethods.NotifyUser(message);
                }
                if (Configuration.MainModelRef.TabSelected_Players) // Active Effect Roll
                {
                    CharacterModel character = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter;
                    string message = character.Name + " uses " + Name + ".";
                    ProcessAbility(null, character, "Normal", PresetScale, out string abilityMessage, out _); // Cannot make additional layer of active effects
                    HelperMethods.AddToGameplayLog(message + "\n" + abilityMessage, "Default", true);
                }
            }
            if (param.GetType() == typeof(CreatureModel))
            {
                CreatureModel creature = (param as CreatureModel);
                if (Configuration.MainModelRef.TabSelected_Players)
                {
                    string message = "- " + creature.DisplayName + " uses " + Name + ".";
                    ProcessAbility(creature, null, "Normal", PresetScale, out string abilityMessage, out _); // Cannot make additional layer of active effects
                    HelperMethods.AddToGameplayLog(message + "\n" + abilityMessage, "Default", true);
                } // TODO - double check new active effects available for minions
                if (Configuration.MainModelRef.TabSelected_Campaigns)
                {
                    string message = creature.DisplayName + " uses " + Name + ".";
                    ProcessAbility(creature, null,  "Normal", PresetScale, out string abilityMessage, out _); // Cannot make additional layer of active effects
                    HelperMethods.AddToGameplayLog(message + "\n" + abilityMessage, "Spell");
                }
            }
            
        }
        #endregion
        #region MakeAttack
        public ICommand MakeAttack => new RelayCommand(DoMakeAttack);
        private void DoMakeAttack(object param)
        {
            CharacterModel attackingPlayer = null;
            CreatureModel attackingCreature = null;
            string attackMode = "Normal";
            bool useOptions = false;
            if ((param as object[]) == null)
            {
                if (param.GetType() == typeof(CreatureModel)) { attackingCreature = param as CreatureModel; attackingCreature.DisplayPopup_Weapons = false; attackingCreature.DisplayPopupAlt_Weapons = false; }
                else { attackingPlayer = (param as MainViewModel).CharacterBuilderView.ActiveCharacter; }
                if (param.GetType() == typeof(MainViewModel))
                {
                    if (Configuration.MainModelRef.TabSelected_CreatureBuilder) { attackingCreature = Configuration.MainModelRef.CreatureBuilderView.ActiveCreature; }
                }
            }
            else
            {
                if ((param as object[])[0].GetType() == typeof(CreatureModel)) { attackingCreature = (param as object[])[0] as CreatureModel; attackingCreature.DisplayPopup_Weapons = false; attackingCreature.DisplayPopupAlt_Weapons = false; }
                else { attackingPlayer = Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter; }
                attackMode = (param as object[])[1].ToString();
            }
            if (attackMode == "Options")
            {
                AbilityOptionSelection optionSelection = new(Name, Variables.ToList());
                if (optionSelection.ShowDialog() == true)
                {
                    foreach (BoolOption opt in optionSelection.Options)
                    {
                        Variables.First(v => v.Name == opt.Name).Value = opt.Marked.ToString();
                    }
                    useOptions = true;
                }
                else { return; }

            }
            if (attackMode == "LastOptions")
            {
                useOptions = true;
            }
            ProcessAbility(attackingCreature, attackingPlayer, attackMode, 0, out string attackMessage, out _, useOptions); // These abilities do not make spell effects
            string message = string.Empty;
            if (attackingPlayer != null) { message += attackingPlayer.Name + " uses " + Name + "."; }
            else if (attackingCreature != null) { message += attackingCreature.DisplayName + " uses " + Name + "."; }
            HelperMethods.AddToGameplayLog(message + attackMessage, "Attack", true);
            if (Configuration.MainModelRef.TabSelected_CreatureBuilder) // Test button on creatures
            {
                HelperMethods.NotifyUser(message + attackMessage);
            }
            if (Configuration.MainModelRef.TabSelected_SpellBuilder) // Test button on spells
            {
                HelperMethods.NotifyUser(message + attackMessage);
            }

        }
        #endregion
        #region DuplicateAbility
        public ICommand DuplicateAbility => new RelayCommand(DoDuplicateAbility);
        private void DoDuplicateAbility(object param)
        {
            CustomAbility clone = HelperMethods.DeepClone(this);
            clone.Name = "Copy of " + clone.Name;
            clone.UpdateDropdownSuggestedValues();

            if (Configuration.MainModelRef.TabSelected_Players)
            {
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Abilities.Add(clone);
            }
            if (Configuration.MainModelRef.TabSelected_SpellBuilder)
            {
                if (Configuration.MainModelRef.SpellBuilderView.ActiveSpell.IsTabSelected_PrimaryAbilities)
                {
                    Configuration.MainModelRef.SpellBuilderView.ActiveSpell.PrimaryAbilities.Add(clone);
                }
                else
                {
                    Configuration.MainModelRef.SpellBuilderView.ActiveSpell.SecondaryAbilities.Add(clone);
                }
            }
            if (Configuration.MainModelRef.TabSelected_CreatureBuilder)
            {
                Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Abilities.Add(clone);
            }
        }
        #endregion
        #region CopyAbility
        public ICommand CopyAbility => new RelayCommand(DoCopyAbility);
        private void DoCopyAbility(object param)
        {
            Configuration.CopiedAbility = HelperMethods.DeepClone(this);
        }
        #endregion

        // Public Methods
        public bool ProcessAbility(CreatureModel creature, CharacterModel character, string attackMode, int scale, out string message, out List<string> activeEffects, bool useOptions = false)
        {
            message = string.Empty;
            activeEffects = new();
            List<string> errors = new();

            errors = PreRunValidation();
            if (errors.Count > 1) { HelperMethods.NotifyUser(errors); return false; }

            List<CAVariable> variables = new();
            foreach (CAVariable v in Variables)
            {
                variables.Add(HelperMethods.DeepClone(v));
            }
            string mode = GetAbilityProcessingMode(creature, character);

            int rounds = QuantityToPerform;
            if (creature != null) { if (creature.IsHorde) { rounds += creature.CurrentHordeSize - 1; } }
            if (DoesQuantityScale)
            {
                rounds += (scale * ScaleRate);
            }

            // Add Preset Variables
            foreach (string v in Configuration.InternalAbilityVariables)
            {
                variables.Add(new() { Name = v, Type = "Internal Bool" });
            }
            if (useOptions)
            {
                List<string> optsUsed = new();
                foreach (CAVariable v in variables)
                {
                    bool.TryParse(v.Value, out bool usingOpt);
                    v.Value = usingOpt.ToString(); // force-fix LastOptions null issue
                    if (usingOpt) { optsUsed.Add(v.Name); }
                }
                if (optsUsed.Count > 0)
                {
                    message += "\nOptions used: ";
                    for (int i = 0; i < optsUsed.Count; i++)
                    {
                        if (i > 0) { message += ", "; }
                        message += optsUsed[i];
                    }
                }
            }

            errors = CheckPostActionCounterUsage((mode == Mode_Character) ? character.Counters.ToList() : creature.Counters.ToList());
            if (errors.Count > 1) { HelperMethods.NotifyUser(errors); return false; }

            for (int n = 0; n < rounds; n++)
            {
                if (n > 0) { message += $"\n................."; } // visual spacer
                PrepareCustomVariables(ref variables, useOptions);

                int attackRoll = 0;
                bool isCriticalHit = false;

                // Process Pre-Actions
                foreach (CAPreAction preAction in PreActions)
                {
                    CAVariable target = variables.FirstOrDefault(v => v.Name == preAction.Target)!;
                    bool conditionFailure = false;

                    // Check Conditions
                    foreach (CACondition condition in preAction.Conditions)
                    {
                        string valueA = string.Empty;
                        string valueB = string.Empty;
                        CAVariable conditionVariable = variables.FirstOrDefault(v => v.Name == condition.ConditionVariable);
                        CharacterAlterant alterant = null;
                        if (character != null) { alterant = character.Alterants.FirstOrDefault(a => a.Name == condition.ConditionVariable); }
                        CAVariable conditionValue = variables.FirstOrDefault(v => v.Name == condition.ConditionValue);
                        if (conditionVariable == null && alterant == null) { HelperMethods.NotifyUser("Variable or alterant \"" + condition.ConditionVariable + "\" not found."); return false; }

                        valueA = (conditionVariable != null) ? conditionVariable.Value : alterant.IsActive.ToString();
                        valueB = (conditionValue != null) ? conditionValue.Value : condition.ConditionValue;

                        if (condition.ConditionType == "Equal To")
                        {
                            if (valueA.ToUpper() != valueB.ToUpper()) { conditionFailure = true; }
                        }
                        if (condition.ConditionType == "Less Than" || condition.ConditionType == "Greater Than")
                        {
                            if (int.TryParse(valueA, out int a) == false || int.TryParse(valueB, out int b) == false)
                            {
                                conditionFailure = true;
                                continue;
                            }
                            if (condition.ConditionType == "Less Than")
                            {
                                if (a >= b) { conditionFailure = true; continue; }
                            }
                            if (condition.ConditionType == "Greater Than")
                            {
                                if (a <= b) { conditionFailure = true; continue; }
                            }
                        }
                        if (condition.ConditionType == "Contains")
                        {
                            if (!valueA.ToUpper().Contains(valueB.ToUpper())) { conditionFailure = true; }
                        }

                    }
                    if (conditionFailure) { continue; }

                    // Perform Action
                    if (preAction.Action == "Make Attack Roll")
                    {
                        CAVariable advVar = variables.First(v => v.Name == "[Attack with Advantage]");
                        CAVariable disVar = variables.First(v => v.Name == "[Attack with Disadvantage]");
                        bool useAdv = attackMode == "Advantage";
                        bool useDis = attackMode == "Disadvantage";
                        if (attackMode == "Sleeper Hit") { useAdv = true; }
                        if (advVar.Value == "True") { useAdv = true; }
                        if (disVar.Value == "True") { useDis = true; }
                        attackRoll = HelperMethods.RollD20(useAdv, useDis);
                        if (attackRoll == 20 || attackMode == "Sleeper Hit") 
                        { 
                            isCriticalHit = true;
                            variables.First(v => v.Name == "[Is Critical Hit]").Value = "True";
                        }
                        target.Rolls.Add(attackRoll.ToString() + (useAdv ? "A" : "") + (useDis ? "D" : ""));
                        int mod = 0;
                        switch (preAction.AttackAttribute)
                        {
                            case "Strength":
                                mod = (mode == Mode_Character) ? character.StrengthModifier : creature.StrengthModifier;
                                break;
                            case "Dexterity":
                                mod = (mode == Mode_Character) ? character.DexterityModifier : creature.DexterityModifier;
                                break;
                            case "Constitution":
                                mod = (mode == Mode_Character) ? character.ConstitutionModifier : creature.ConstitutionModifier;
                                break;
                            case "Intelligence":
                                mod = (mode == Mode_Character) ? character.IntelligenceModifier : creature.IntelligenceModifier;
                                break;
                            case "Wisdom":
                                mod = (mode == Mode_Character) ? character.WisdomModifier : creature.WisdomModifier;
                                break;
                            case "Charisma":
                                mod = (mode == Mode_Character) ? character.CharismaModifier : creature.CharismaModifier;
                                break;
                            case "Spellcasting":
                                mod = (mode == Mode_Character) ? character.SpellAbilityModifier : creature.SpellAbilityModifier;
                                break;
                            case "None":
                                break;
                            default:
                                HelperMethods.NotifyUser($"Unhandled attack attribute \"{preAction.Target}\".");
                                return false;
                        }
                        target.Modifiers.Add(mod.ToString());
                        attackRoll += mod;
                        if (preAction.UseProficiencyBonus)
                        {
                            mod = (mode == Mode_Character) ? character.ProficiencyBonus : creature.ProficiencyBonus;
                            target.Modifiers.Add(mod.ToString());
                            attackRoll += mod;
                        }
                        target.Value = attackRoll.ToString();
                    }

                    if (preAction.Action == AppData.PreAction_Add_Set_Value)
                    {
                        if (target.Type == "Number" && int.TryParse(preAction.SetValue, out int result) == true)
                        {
                            if (preAction.HasValueScaling)
                            {
                                result += (scale * preAction.ValueScaleRate);
                            }
                            target.Value = (Convert.ToInt32(target.Value) + result).ToString();
                            target.Modifiers.Add(result.ToString());
                        }
                        if (target.Type == "Number" && int.TryParse(preAction.SetValue, out int result2) == false)
                        {
                            HelperMethods.NotifyUser("Invalid value \"" + preAction.SetValue + "\" for variable type Number."); 
                            return false;
                        }
                        if (target.Type == "Text") { target.Value += preAction.SetValue; }
                        if (target.Type == "Attack Option" || target.Type == "Internal Bool") 
                        { 
                            if (bool.TryParse(preAction.SetValue, out _) == false)
                            {
                                if (CheckVariable(preAction.SetValue, Variables.ToList(), "Toggled Option", out CAVariable v) == false)
                                {
                                    HelperMethods.NotifyUser("Invalid value \"" + preAction.SetValue + "\" for variable type " + target.Type + ".");
                                    return false;
                                }
                                else
                                {
                                    target.Value = v.Value;
                                }
                            }
                            else
                            {
                                target.Value = preAction.SetValue;
                            }
                        }
                    }

                    if (preAction.Action == "Add Roll")
                    {
                        if (target.Type != "Number") { HelperMethods.NotifyUser("Variable \"" + target.Name + "\" is not a Number type, unable to add dice roll."); return false; }
                        int diceQty = preAction.DiceQuantity;
                        if (preAction.HasValueScaling)
                        {
                            diceQty += (scale * preAction.ValueScaleRate);
                        }
                        if (preAction.DoesDoubleOnCritical && isCriticalHit)
                        {
                            if (Configuration.MainModelRef.SettingsView.UseCriticalHitMaxDamage)
                            {
                                target.Value = (Convert.ToInt32(target.Value) + (preAction.DiceQuantity * preAction.DiceQuality)).ToString();
                                target.Modifiers.Add((preAction.DiceQuantity * preAction.DiceQuality).ToString());
                            }
                            else
                            {
                                diceQty += preAction.DiceQuantity;
                            }
                        }
                        HelperMethods.RollDice(diceQty, preAction.DiceQuality, 0, out int result, out List<string> rolls);
                        target.Value = (Convert.ToInt32(target.Value) + result).ToString();
                        target.Rolls.AddRange(rolls);
                    }

                    if (preAction.Action == "Add Stat Value")
                    {
                        int value = 0;

                        if (mode == "Test") { continue; }

                        if (mode == "Character")
                        {
                            value = preAction.StatValue switch
                            {
                                "Spellcasting Ability Modifier" => character.SpellAbilityModifier,
                                "Spellcasting Attack Modifier" => character.SpellAttackBonus,
                                "Spellcasting Save DC" => character.SpellSaveDc,
                                "Proficiency Bonus" => character.ProficiencyBonus,
                                "Strength" => character.StrengthModifier,
                                "Dexterity" => character.DexterityModifier,
                                "Constitution" => character.ConstitutionModifier,
                                "Intelligence" => character.IntelligenceModifier,
                                "Wisdom" => character.WisdomModifier,
                                "Charisma" => character.CharismaModifier,
                                _ => 0
                            };
                        }
                        if (mode == "Creature")
                        {
                            value = preAction.StatValue switch
                            {
                                "Spellcasting Ability Modifier" => creature.SpellAbilityModifier,
                                "Spellcasting Attack Modifier" => creature.SpellAttackBonus,
                                "Spellcasting Save DC" => creature.SpellSaveDc,
                                "Proficiency Bonus" => creature.ProficiencyBonus,
                                "Strength" => creature.StrengthModifier,
                                "Dexterity" => creature.DexterityModifier,
                                "Constitution" => creature.ConstitutionModifier,
                                "Intelligence" => creature.IntelligenceModifier,
                                "Wisdom" => creature.WisdomModifier,
                                "Charisma" => creature.CharismaModifier,
                                _ => 0
                            };
                        }

                        target.Value = (Convert.ToInt32(target.Value) + value).ToString();
                        target.Modifiers.Add(value.ToString());

                    }

                    if (preAction.Action == "QA Prompt")
                    {
                        if (string.IsNullOrEmpty(preAction.Question)) { HelperMethods.NotifyUser("Question is blank."); return false; }
                        if (preAction.Answers.Count == 0) { HelperMethods.NotifyUser("No answers available for question \"" + preAction.Question + "\"."); return false; }
                        Dictionary<string, string> swappers2 = new();
                        string newQuestion = preAction.Question;
                        foreach (CAVariable v in Variables)
                        {
                            swappers2.Add("{" + v.Name + "}", v.Value);
                        }
                        foreach (KeyValuePair<string, string> kvp in swappers2)
                        {
                            newQuestion = newQuestion.Replace(kvp.Key, kvp.Value);
                        }

                        QAPrompt question = new(newQuestion, preAction.Answers.ToList());
                        question.ShowDialog();
                        target.Value = question.Answer;
                    }

                    if (preAction.Action == "Add Calculated Value")
                    {
                        if (target.Type != "Number") { HelperMethods.NotifyUser("Variable \"" + target.Name + "\" is not a Number type, unable to perform calculation."); return false; }
                        if (int.TryParse(preAction.CalculatedValueA, out int calcA) == false)
                        {
                            CAVariable cVarA = Variables.FirstOrDefault(v => v.Name == preAction.CalculatedValueA);
                            if (cVarA == null) { HelperMethods.NotifyUser("Invalid value \"" + preAction.CalculatedValueA + "\" for calculation value A."); return false; }
                            if (cVarA.Type != "Number") { HelperMethods.NotifyUser("Variable \"" + cVarA.Name + "\" is not a Number type, unable to perform calculation."); return false; }
                            calcA = Convert.ToInt32(cVarA.Value);
                        }
                        if (int.TryParse(preAction.CalculatedValueB, out int calcB) == false)
                        {
                            CAVariable cVarB = Variables.FirstOrDefault(v => v.Name == preAction.CalculatedValueB);
                            if (cVarB == null) { HelperMethods.NotifyUser("Invalid value \"" + preAction.CalculatedValueB + "\" for calculation value B."); return false; }
                            if (cVarB.Type != "Number") { HelperMethods.NotifyUser("Variable \"" + cVarB.Name + "\" is not a Number type, unable to perform calculation."); return false; }
                            calcA = Convert.ToInt32(cVarB.Value);
                        }

                        if (preAction.Calculation == "multiplied by")
                        {
                            target.Value = (Convert.ToInt32(target.Value) + (calcA * calcB)).ToString();
                        }
                        if (preAction.Calculation == "divided by")
                        {
                            if (calcB == 0) { HelperMethods.NotifyUser("Cannot divide by zero."); return false; }
                            target.Value = (Convert.ToInt32(target.Value) + (calcA * calcB)).ToString();
                        }
                        if (preAction.Calculation == "plus")
                        {
                            target.Value = (Convert.ToInt32(target.Value) + (calcA + calcB)).ToString();
                        }
                        if (preAction.Calculation == "minus")
                        {
                            target.Value = (Convert.ToInt32(target.Value) + (calcA - calcB)).ToString();
                        }

                    }

                    if (preAction.Action == "Numeric Value Prompt")
                    {
                        if (target.Type != "Number") { HelperMethods.NotifyUser("Variable \"" + target.Name + "\" is not a Number type, unable to perform value prompt."); return false; }

                        ValuePrompt prompt = new(preAction.Question);
                        prompt.ShowDialog();

                        target.Value = (Convert.ToInt32(target.Value) + prompt.Value).ToString();

                    }

                    if (preAction.Action == "Translate Value")
                    {
                        CAVariable source = variables.FirstOrDefault(v => v.Name == preAction.SourceVariable);
                        if (source == null) { HelperMethods.NotifyUser("Invalid source \"" + preAction.SourceVariable + "\", variable not found."); return false; }

                        target.Value = source.Value;
                        foreach (StringPair pair in preAction.Pairs)
                        {
                            if (source.Value == pair.ValueA) 
                            { 
                                target.Value = pair.ValueB;
                                break;
                            }
                        }

                    }

                }

                // Process Output
                Dictionary<string, string> swappers = new();
                string newOutput = (n == 0) ? Output : ""; // Prevent repeat of output for certain spells (Minute Meteors)
                foreach (CAVariable v in variables)
                {
                    swappers.Add("{" + v.Name + "}", v.Value);
                }
                foreach (KeyValuePair<string, string> kvp in swappers)
                {
                    newOutput = newOutput.Replace(kvp.Key, kvp.Value);
                }
                message += newOutput;

                // Additional Output
                foreach (CAVariable v in variables)
                {
                    if (v.Type != "Number") { continue; }
                    if (v.DoOutput == false) { continue; }
                    message += "\n" + v.Name + ": " + v.Value;
                    if (v.IncludeHalfValue) { message += " (" + (Convert.ToInt32(v.Value) / 2) + ")"; }
                    if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
                    {
                        if (v.Rolls.Count > 0)
                        {
                            message += "\n" + v.Name + " (Roll): " + HelperMethods.GetFormattedDiceRolls(v.Rolls);
                        }
                        else
                        {
                            continue;
                        }
                        if (v.Modifiers.Count > 0)
                        {
                            message += " + " + HelperMethods.GetFormattedModifiers(v.Modifiers);
                        }
                    }
                    
                }

                // Process Post-Actions
                foreach (CAPostAction postAction in PostActions)
                {
                    // Check Condition
                    foreach (CACondition condition in postAction.Conditions)
                    {
                        string valueA = string.Empty;
                        string valueB = string.Empty;
                        CAVariable conditionVariable = variables.FirstOrDefault(v => v.Name == condition.ConditionVariable);
                        CAVariable conditionValue = variables.FirstOrDefault(v => v.Name == condition.ConditionValue);
                        if (conditionVariable == null) { HelperMethods.NotifyUser("Variable \"" + condition.ConditionVariable + "\" not found."); return false; }

                        valueA = conditionVariable.Value;
                        valueB = (conditionValue != null) ? conditionValue.Value : condition.ConditionValue;

                        if (condition.ConditionType == "Equal To")
                        {
                            if (valueA != valueB) { continue; }
                        }
                        if (condition.ConditionType == "Less Than" || condition.ConditionType == "Greater Than")
                        {
                            if (int.TryParse(valueA, out int a) == false || int.TryParse(valueB, out int b) == false)
                            {
                                { continue; }
                            }
                            if (condition.ConditionType == "Less Than")
                            {
                                if (a >= b) { continue; }
                            }
                            if (condition.ConditionType == "Greater Than")
                            {
                                if (a <= b) { continue; }
                            }
                        }
                        if (condition.ConditionType == "Contains")
                        {
                            if (!valueA.ToUpper().Contains(valueB.ToUpper())) { continue; }
                        }

                    }

                    // Perform Action
                    if (mode == Mode_Character)
                    {
                        if (postAction.Action == AppData.PostAction_Add_to_Current_HP)
                        {
                            if (CheckVariable(postAction.ValueA, variables, "Number", out CAVariable v) == false) { return false; }
                            character.CurrentHealth += Convert.ToInt32(v.Value);
                            message += $"\nHealed for {v.Value} hit points.";
                            if (character.CurrentHealth > character.MaxHealth) 
                            { 
                                character.CurrentHealth = character.MaxHealth;
                                message += $"\n{character.Name} is now fully healed!";
                            }
                        }

                        if (postAction.Action == AppData.PostAction_Subtract_from_Current_HP)
                        {
                            if (CheckVariable(postAction.ValueA, variables, "Number", out CAVariable v) == false) { return false; }
                            character.CurrentHealth -= Convert.ToInt32(v.Value);
                            if (character.CurrentHealth < 0) 
                            {
                                character.CurrentHealth = 0;
                                message += $"\n{character.Name} has gone to zero hit points!";
                            }
                        }

                        if (postAction.Action == AppData.PostAction_Add_to_Temporary_HP)
                        {
                            if (CheckVariable(postAction.ValueA, variables, "Number", out CAVariable v) == false) { return false; }
                            int newTempHp = Convert.ToInt32(v.Value);
                            if (newTempHp < character.TempHealth)
                            {
                                message += "\nAlready have a stronger source of temporary hit points.";
                                // Temporary hit points aren’t cumulative. If you have temporary hit points
                                // and receive more of them, you don’t add them together, unless a game feature
                                // says you can. Instead, you decide which temporary hit points to keep. (XGE)
                            }
                            else
                            {
                                character.TempHealth = newTempHp;
                                message += $"\nAdded {v.Value} temporary hit points.";
                            }
                        }

                        if (postAction.Action == AppData.PostAction_Add_Minions)
                        {
                            if (CheckVariable(postAction.ValueA, variables, "Text", out CAVariable v) == false) { return false; }
                            if (CheckVariable(postAction.ValueB, variables, "Number", out CAVariable v2) == false) { return false; }

                            CreatureModel minion = Configuration.MainModelRef.CreatureBuilderView.AllCreatures.FirstOrDefault(c => c.Name == v.Value);
                            if (minion == null) { HelperMethods.NotifyUser("Creature \"" + postAction.ValueA + "\" not found."); return false; }

                            for (int i = 0; i < Convert.ToInt32(v2.Value); i++)
                            {
                                character.AddCharacterMinion(minion);
                                message += $"\nAdded minion {character.Minions.Last().DisplayName}.";
                            }
                        }

                        if (postAction.Action == AppData.PostAction_Add_Active_Effect)
                        {
                            activeEffects.Add(postAction.ValueA);
                        }

                        if (postAction.Action == AppData.PostAction_Activate_Concentration)
                        {
                            character.IsConcentrating = true;
                        }

                        if (postAction.Action == AppData.PostAction_Expend_Counter)
                        {
                            CounterModel counter = character.Counters.FirstOrDefault(c => c.Name == postAction.ValueA);
                            int result = Convert.ToInt32(postAction.ValueB); // Validation moved to pre-pre-processing
                            counter.CurrentValue -= result;
                            message += $"\nExpended {result} use(s) of {counter.Name}.";
                        }

                        if (postAction.Action == AppData.PostAction_Activate_Alterant)
                        {
                            CharacterAlterant alterant = character.Alterants.FirstOrDefault(c => c.Name == postAction.ValueA);
                            List<string> newlyActivatedAlterants = new();
                            if (alterant == null)
                            {
                                message += $"\nAlterant \"{postAction.ValueA}\" not found.";
                                return false;
                            }
                            if (alterant.IsActive == false) { newlyActivatedAlterants.Add(alterant.Name); }
                            alterant.IsActive = true;
                            if (newlyActivatedAlterants.Count > 0)
                            {
                                message += $"\nActivated alterants: {HelperMethods.GetStringFromList(newlyActivatedAlterants, ",")}.";
                            }
                        }

                    }

                    if (mode == Mode_Creature)
                    {
                        if (postAction.Action == AppData.PostAction_Add_to_Current_HP)
                        {
                            if (CheckVariable(postAction.ValueA, variables, "Number", out CAVariable v) == false) { return false; }

                            creature.CurrentHitPoints += Convert.ToInt32(v.Value);
                            message += "\nHealed for " + v.Value + " hit points.";
                            if (creature.CurrentHitPoints > creature.MaxHitPoints)
                            {
                                creature.CurrentHitPoints = creature.MaxHitPoints;
                                message += "\n" + creature.Name + " is now fully healed!";
                            }

                        }

                        if (postAction.Action == AppData.PostAction_Subtract_from_Current_HP)
                        {
                            if (CheckVariable(postAction.ValueA, variables, "Number", out CAVariable v) == false) { return false; }

                            creature.CurrentHitPoints -= Convert.ToInt32(v.Value);

                            if (creature.CurrentHitPoints < 0)
                            {
                                creature.CurrentHitPoints = 0;
                                message += "\n" + creature.Name + " has gone to zero hit points!";
                            }

                        }

                        if (postAction.Action == AppData.PostAction_Add_Active_Effect)
                        {
                            activeEffects.Add(postAction.ValueA);
                        }

                        if (postAction.Action == AppData.PostAction_Activate_Concentration)
                        {
                            creature.IsConcentrating = true;
                        }

                        if (postAction.Action == AppData.PostAction_Expend_Counter)
                        {
                            CounterModel counter = creature.Counters.FirstOrDefault(c => c.Name == postAction.ValueA);
                            int result = Convert.ToInt32(postAction.ValueB); // Validation moved to pre-pre-processing
                            counter.CurrentValue -= result;
                            message += "\nExpended " + result + " use of " + counter.Name + ".";
                        }
                    }
                }
            }

            bool usesRoll = false;
            foreach (CAPreAction preAction in PreActions)
            {
                if (preAction.Action.Contains("Roll")) { usesRoll = true; break; }
            }
            if (usesRoll) { HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll); }
            else { HelperMethods.PlaySystemAudio(Configuration.SystemAudio_MenuSelect); }
            return true;

        }
        public void SetGeneratedDescription(CreatureModel creature)
        {
            if (Description != null && Description != "") { return; } // Don't overwrite existing description
            string description = Type += ": ";
            bool hasAttackRoll = false;
            int attackBonus = 0;
            string attackTarget = string.Empty;
            Dictionary<string, string> damageDiceRolls = new();
            Dictionary<string, List<int>> damageDiceMods = new();
            
            // Check for attack pre-action
            foreach (CAPreAction preAction in PreActions)
            {
                if (preAction.Action == "Make Attack Roll")
                {
                    hasAttackRoll = true;
                    attackTarget = preAction.Target;
                    break;
                }
            }

            // Get damage variables
            foreach (CAVariable variable in Variables)
            {
                if (variable.Name.ToUpper().Contains("DAMAGE"))
                {
                    try
                    {
                        damageDiceRolls.Add(variable.Name, "");
                        damageDiceMods.Add(variable.Name, new());
                    }
                    catch (Exception e)
                    {
                        HelperMethods.WriteToLogFile(e.Message + "\nPlease verify there are no duplicate variables for creature '" + creature.Name + "', ability '" + Name + "'.", true);
                        return;
                    }
                }
            }

            // Fill damage dictionaries
            foreach (CAPreAction preAction in PreActions)
            {
                if (damageDiceRolls.ContainsKey(preAction.Target) == false) { continue; }
                if (preAction.Conditions.Count > 0) { continue; } // Show only base damage
                if (preAction.Action == "Add Roll")
                {
                    if (damageDiceRolls[preAction.Target] == "")
                    {
                        damageDiceRolls[preAction.Target] = preAction.DiceQuantity + "d" + preAction.DiceQuality;
                    }
                    else
                    {
                        damageDiceRolls[preAction.Target] += ", " + preAction.DiceQuantity + "d" + preAction.DiceQuality;
                    }
                }
                if (preAction.Action == "Add Set Value")
                {
                    int.TryParse(preAction.SetValue, out int setVal);
                    damageDiceMods[preAction.Target].Add(setVal);
                }
                if (preAction.Action == "Add Stat Value")
                {
                    damageDiceMods[preAction.Target].Add(GetIntValueFromCreatureStat(preAction.StatValue, creature));
                }
            }

            // Set Attack Text
            if (hasAttackRoll)
            {
                description += "Attack: ";
                foreach (CAPreAction preAction in PreActions)
                {
                    if (preAction.Target != attackTarget) { continue; }
                    if (preAction.Action == "Make Attack Roll")
                    {
                        attackBonus += GetIntValueFromCreatureStat(preAction.AttackAttribute, creature);
                    }
                    if (preAction.Action == "Add Set Value")
                    {
                        int.TryParse(preAction.SetValue, out int setVal);
                        attackBonus += setVal;
                    }
                    if (preAction.Action == "Add Stat Value")
                    {
                        attackBonus += GetIntValueFromCreatureStat(preAction.StatValue, creature);
                    }
                }
                description += HelperMethods.GetModifierSymbol(attackBonus) + attackBonus + " to hit. ";
            }

            // Set Damage Text
            if (damageDiceRolls.Count > 0 || damageDiceMods.Count > 0)
            {
                description += "Hit: ";
                foreach (KeyValuePair<string, string> damageRoll in damageDiceRolls)
                {
                    bool hasRoll = false;
                    if (damageDiceRolls.First().Key != damageRoll.Key) { description += ", "; }
                    description += "(";
                    if (damageRoll.Value != "")
                    {
                        hasRoll = true;
                        description += "[" + damageRoll.Value + "] ";
                    }
                    foreach (KeyValuePair<string, List<int>> damageMod in damageDiceMods)
                    {
                        if (damageMod.Key != damageRoll.Key) { continue; }
                        if (damageMod.Value.Count == 0) { continue; }
                        if (hasRoll) { description += HelperMethods.GetModifierSymbol(damageMod.Value.First()); }
                        for (int i = 0; i < damageMod.Value.Count; i++)
                        {
                            if (i > 0) { description += " " + HelperMethods.GetModifierSymbol(damageMod.Value[i]); }
                            description += damageMod.Value[i];
                        }
                    }
                    description += ") " + damageRoll.Key.ToLower();
                }
            }

            if (hasAttackRoll || damageDiceRolls.Count > 0 || damageDiceMods.Count > 0) { description += ". "; }
            description += Output;

            Description = description;

        }
        public void UpdateDropdownSuggestedValues()
        {
            Types = new() { "Melee", "Ranged", "Magic Weapon", "Spell", "Other Ability" };
            List<string> variables = new();
            List<string> conditions = new();
            foreach (CAVariable variable in Variables)
            {
                variable.ResetTypeOptions();
                variables.Add(variable.Name);
                conditions.Add(variable.Name);
            }
            foreach (CAPreAction preAction in PreActions)
            {
                preAction.UpdateTargetList(variables);
                foreach (CACondition condition in preAction.Conditions)
                {
                    condition.UpdateVariableList(conditions);
                }
            }
            foreach (CAPostAction postAction in PostActions)
            {
                foreach (CACondition condition in postAction.Conditions)
                {
                    condition.UpdateVariableList(conditions);
                }
            }
        }
        public void PopulateFromQuickForm(bool includeSaveDc = false)
        {
            QuickAddAbilityDialog abilityDialog = new(includeSaveDc);
            if (abilityDialog.ShowDialog() == true)
            {
                Name = abilityDialog.AbilityName;
                if (includeSaveDc)
                {
                    Variables.Add(new("Save DC", "Number", false));
                    if (abilityDialog.SaveDc > 0)
                    {
                        PreActions.Add(new("Add Set Value", "Save DC", abilityDialog.SaveDc));
                    }
                    else
                    {
                        PreActions.Add(new("Add Stat Value", "Save DC", "Spellcasting Save DC"));
                    }
                }
                else
                {
                    Variables.Add(new("Attack", "Number"));
                    PreActions.Add(new("Make Attack Roll", "Attack", abilityDialog.UtilizedStat, true));
                }
                if (abilityDialog.DamageDiceQuantityA > 0)
                {
                    string varName = abilityDialog.DamageTypeA + " Damage";
                    Variables.Add(new(varName, "Number"));
                    Variables.Last().IncludeHalfValue = abilityDialog.IncludeHalfDamage;
                    PreActions.Add(new("Add Roll", varName, abilityDialog.DamageDiceQuantityA, abilityDialog.DamageDiceSidesA, !includeSaveDc));
                    if (abilityDialog.AddModToDamageA) { PreActions.Add(new("Add Stat Value", varName, abilityDialog.UtilizedStat)); }
                }
                if (abilityDialog.DamageDiceQuantityB > 0)
                {
                    string varName = abilityDialog.DamageTypeB + " Damage";
                    Variables.Add(new(varName, "Number"));
                    Variables.Last().IncludeHalfValue = abilityDialog.IncludeHalfDamage;
                    PreActions.Add(new("Add Roll", varName, abilityDialog.DamageDiceQuantityB, abilityDialog.DamageDiceSidesB, !includeSaveDc));
                    if (abilityDialog.AddModToDamageB) { PreActions.Add(new("Add Stat Value", varName, abilityDialog.UtilizedStat)); }
                }

                // Create Output for saving throw
                if (includeSaveDc)
                {
                    Output += "Target(s) must make a DC {Save DC} " + abilityDialog.UtilizedStat + " saving throw";
                    if (abilityDialog.DamageDiceQuantityA > 0)
                    {
                        string dmgNameA = abilityDialog.DamageTypeA + " Damage";
                        Output += ", or take {" + dmgNameA + "} " + abilityDialog.DamageTypeA.ToLower() + " damage";
                        if (abilityDialog.DamageDiceQuantityB > 0)
                        {
                            string dmgNameB = abilityDialog.DamageTypeB + " Damage";
                            Output += ", and {" + dmgNameB + "} " + abilityDialog.DamageTypeB.ToLower() + " damage on a failed save";
                            if (abilityDialog.IncludeHalfDamage)
                            {
                                Output += ", or half as much on a successful one.";
                            }
                            else
                            {
                                Output += ".";
                            }
                        }
                        else if (abilityDialog.IncludeHalfDamage)
                        {
                            Output += ", or half as much on a successful one.";
                        }
                        else
                        {
                            Output += " on a failed save.";
                        }
                    }
                    else
                    {
                        Output += ".";
                    }
                }

            }
        }

        // Private Methods
        private List<string> PreRunValidation()
        {
            List<string> errors = new();
            errors.Add($"Pre-run validation has failed for {Name}.");

            foreach (CAPreAction preAction in PreActions)
            {
                CAVariable target = Variables.FirstOrDefault(v => v.Name == preAction.Target);
                if (target == null) { errors.Add($"Invalid target \"{preAction.Target}\", variable not found."); }

                foreach (CACondition condition in preAction.Conditions)
                {
                    if (condition.ConditionType == string.Empty) { errors.Add($"Missing condition type for {preAction.Action}:{preAction.Target}."); }
                }

            }

            return errors;

        }
        private List<string> CheckPostActionCounterUsage(List<CounterModel> counters)
        {
            List<string> errors = new();
            errors.Add($"Counter validation has failed for {Name}.");

            foreach (CAPostAction postAction in PostActions)
            {
                if (postAction.Action == AppData.PostAction_Expend_Counter)
                {
                    CounterModel counter = counters.FirstOrDefault(c => c.Name == postAction.ValueA);
                    if (counter == null)
                    {
                        errors.Add($"Counter \"{postAction.ValueA}\" not found.");
                    }
                    bool validInt = int.TryParse(postAction.ValueB, out int result);
                    if (validInt == false)
                    {
                        errors.Add($"Invalid value \"{postAction.ValueB}\", expected number.");
                    }
                    if (counter.CurrentValue - (result * QuantityToPerform) < 0)
                    {
                        errors.Add($"Insufficient counter amount available for \"{postAction.ValueA}\".");
                    }
                }
            }

            return errors;

        }
        private static void PrepareCustomVariables(ref List<CAVariable> variables, bool useOptions)
        {
            foreach (CAVariable v in variables)
            {
                if (v.Type == AppData.VarType_Text) { v.Value = string.Empty; }
                if (v.Type == AppData.VarType_Number) { v.Value = "0"; }
                if (v.Type == AppData.VarType_Toggled_Option)
                {
                    if (useOptions == false) { v.Value = "False"; }
                }
                if (v.Type == AppData.SpecType_Internal_Bool) { v.Value = "False"; }
                v.Rolls = new();
                v.Modifiers = new();
            }
        }
        private static bool CheckVariable(string variableName, List<CAVariable> variables, string expectedType, out CAVariable v)
        {
            v = variables.FirstOrDefault(v => v.Name == variableName);
            if (v == null) { HelperMethods.NotifyUser("Invalid target \"" + variableName + "\", variable not found."); return false; }
            if (v.Type != expectedType) { HelperMethods.NotifyUser("Expected variable type \"" + expectedType + "\", matched variable is of type \"" + v.Type + "\"."); return false; }
            return true;
        }
        private static int GetIntValueFromCreatureStat(string stat, CreatureModel creature)
        {
            return stat switch
            {
                "Strength" => creature.StrengthModifier,
                "Dexterity" => creature.DexterityModifier,
                "Constitution" => creature.ConstitutionModifier,
                "Intelligence" => creature.IntelligenceModifier,
                "Wisdom" => creature.WisdomModifier,
                "Charisma" => creature.CharismaModifier,
                "Spellcasting Attack Modifier" => creature.SpellAttackBonus,
                "Spellcasting Ability Modifier" => creature.SpellAbilityModifier,
                "Proficiency Bonus" => creature.ProficiencyBonus,
                _ => 0
            };
        }
        private static string GetAbilityProcessingMode(CreatureModel creature, CharacterModel character)
        {
            if (character != null) { return Mode_Character; }
            if (creature != null) { return Mode_Creature; }
            return Mode_Test;
        }
        private void InitializeCollections()
        {
            Types = new() { "Melee", "Ranged", "Magic Weapon", "Spell", "Other Ability" };
            Variables = new();
            PreActions = new();
            PostActions = new();
        }

    }
}
