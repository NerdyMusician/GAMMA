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
            ConsumedMaterials = new();
            SpellClasses = new();
            PrimaryAbilities = new();
            SecondaryAbilities = new();
            IsTabSelected_PrimaryAbilities = true;
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode(XSME.Single)]
        public string Sourcebook
        {
            get => _Sourcebook;
            set => SetAndNotify(ref _Sourcebook, value);
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
        #region SpellClasses
        private ObservableCollection<ConvertibleValue> _SpellClasses;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> SpellClasses
        {
            get => _SpellClasses;
            set => SetAndNotify(ref _SpellClasses, value);
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get => _IsValidated;
            set => SetAndNotify(ref _IsValidated, value);
        }
        #endregion

        #region IsTabSelected_PrimaryAbilities
        private bool _IsTabSelected_PrimaryAbilities;
        public bool IsTabSelected_PrimaryAbilities
        {
            get => _IsTabSelected_PrimaryAbilities;
            set => SetAndNotify(ref _IsTabSelected_PrimaryAbilities, value);
        }
        #endregion
        #region IsTabSelected_SecondaryAbilities
        private bool _IsTabSelected_SecondaryAbilities;
        public bool IsTabSelected_SecondaryAbilities
        {
            get => _IsTabSelected_SecondaryAbilities;
            set => SetAndNotify(ref _IsTabSelected_SecondaryAbilities, value);
        }
        #endregion

        #region SchoolOfMagic
        private string _SchoolOfMagic;
        [XmlSaveMode(XSME.Single)]
        public string SchoolOfMagic
        {
            get => _SchoolOfMagic;
            set => SetAndNotify(ref _SchoolOfMagic, value);
        }
        #endregion
        #region CastingTime
        private string _CastingTime;
        [XmlSaveMode(XSME.Single)]
        public string CastingTime
        {
            get => _CastingTime;
            set => SetAndNotify(ref _CastingTime, value);
        }
        #endregion
        #region SpellDuration
        private string _SpellDuration;
        [XmlSaveMode(XSME.Single)]
        public string SpellDuration
        {
            get => _SpellDuration;
            set => SetAndNotify(ref _SpellDuration, value);
        }
        #endregion
        #region SpellLevel
        private int _SpellLevel;
        [XmlSaveMode(XSME.Single)]
        public int SpellLevel
        {
            get => _SpellLevel;
            set => SetAndNotify(ref _SpellLevel, value);
        }
        #endregion

        #region Range
        private string _Range;
        [XmlSaveMode(XSME.Single)]
        public string Range
        {
            get => _Range;
            set => SetAndNotify(ref _Range, value);
        }
        #endregion
        #region HasAreaOfEffect
        private bool _HasAreaOfEffect;
        [XmlSaveMode(XSME.Single)]
        public bool HasAreaOfEffect
        {
            get => _HasAreaOfEffect;
            set => SetAndNotify(ref _HasAreaOfEffect, value);
        }
        #endregion
        #region AoeRange
        private int _AoeRange;
        [XmlSaveMode(XSME.Single)]
        public int AoeRange
        {
            get => _AoeRange;
            set => SetAndNotify(ref _AoeRange, value);
        }
        #endregion
        #region AoeRange2
        private int _AoeRange2;
        public int AoeRange2
        {
            get => _AoeRange2;
            set => SetAndNotify(ref _AoeRange2, value);
        }
        #endregion
        #region AoeShape
        private string _AoeShape;
        [XmlSaveMode(XSME.Single)]
        public string AoeShape
        {
            get => _AoeShape;
            set => SetAndNotify(ref _AoeShape, value);
        }
        #endregion

        #region IsPrepared
        private bool _IsPrepared;
        [XmlSaveMode(XSME.Single)]
        public bool IsPrepared
        {
            get => _IsPrepared;
            set => SetAndNotify(ref _IsPrepared, value);
        }
        #endregion
        #region HasScaling
        // Indicates if the spell should show a list of higher level casting options
        private bool _HasScaling;
        [XmlSaveMode(XSME.Single)]
        public bool HasScaling
        {
            get => _HasScaling;
            set => SetAndNotify(ref _HasScaling, value);
        }
        #endregion
        #region RitualCapable
        private bool _RitualCapable;
        [XmlSaveMode(XSME.Single)]
        public bool RitualCapable
        {
            get => _RitualCapable;
            set => SetAndNotify(ref _RitualCapable, value);
        }
        #endregion
        #region RequiresConcentration
        private bool _RequiresConcentration;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresConcentration
        {
            get => _RequiresConcentration;
            set => SetAndNotify(ref _RequiresConcentration, value);
        }
        #endregion
        #region RequiresSomatic
        private bool _RequiresSomatic;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresSomatic
        {
            get => _RequiresSomatic;
            set => SetAndNotify(ref _RequiresSomatic, value);
        }
        #endregion
        #region RequiresVerbal
        private bool _RequiresVerbal;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresVerbal
        {
            get => _RequiresVerbal;
            set => SetAndNotify(ref _RequiresVerbal, value);
        }
        #endregion

        #region RequiresMaterial
        private bool _RequiresMaterial;
        [XmlSaveMode(XSME.Single)]
        public bool RequiresMaterial
        {
            get => _RequiresMaterial;
            set => SetAndNotify(ref _RequiresMaterial, value);
        }
        #endregion
        #region DoesConsumeMaterials
        private bool _DoesConsumeMaterials;
        [XmlSaveMode(XSME.Single)]
        public bool DoesConsumeMaterials
        {
            get => _DoesConsumeMaterials;
            set => SetAndNotify(ref _DoesConsumeMaterials, value);
        }
        #endregion
        #region ConsumedMaterials
        private ObservableCollection<ItemModel> _ConsumedMaterials;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> ConsumedMaterials
        {
            get => _ConsumedMaterials;
            set => SetAndNotify(ref _ConsumedMaterials, value);
        }
        #endregion
        #region Materials
        private string _Materials;
        [XmlSaveMode(XSME.Single)]
        public string Materials
        {
            get => _Materials;
            set => SetAndNotify(ref _Materials, value);
        }
        #endregion

        #region PrimaryAbilities
        private ObservableCollection<CustomAbility> _PrimaryAbilities;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CustomAbility> PrimaryAbilities
        {
            get => _PrimaryAbilities;
            set => SetAndNotify(ref _PrimaryAbilities, value);
        }
        #endregion
        #region SecondaryAbilities
        private ObservableCollection<CustomAbility> _SecondaryAbilities;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CustomAbility> SecondaryAbilities
        {
            get => _SecondaryAbilities;
            set => SetAndNotify(ref _SecondaryAbilities, value);
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region SchoolsOfMagic
        private List<string> _SchoolsOfMagic;
        public List<string> SchoolsOfMagic
        {
            get => _SchoolsOfMagic;
            set => SetAndNotify(ref _SchoolsOfMagic, value);
        }
        #endregion
        #region AoeShapes
        private List<string> _AoeShapes;
        public List<string> AoeShapes
        {
            get => _AoeShapes;
            set => SetAndNotify(ref _AoeShapes, value);
        }
        #endregion
        #region DamageDiceSides
        private List<string> _DamageDiceSides;
        public List<string> DamageDiceSides
        {
            get => _DamageDiceSides;
            set => SetAndNotify(ref _DamageDiceSides, value);
        }
        #endregion
        #region DamageTypes
        private List<string> _DamageTypes;
        public List<string> DamageTypes
        {
            get => _DamageTypes;
            set => SetAndNotify(ref _DamageTypes, value);
        }
        #endregion
        #region SaveDamageTypes
        private List<string> _SaveDamageTypes;
        public List<string> SaveDamageTypes
        {
            get => _SaveDamageTypes;
            set => SetAndNotify(ref _SaveDamageTypes, value);
        }
        #endregion
        #region HealingDiceSides
        private List<string> _HealingDiceSides;
        public List<string> HealingDiceSides
        {
            get => _HealingDiceSides;
            set => SetAndNotify(ref _HealingDiceSides, value);
        }
        #endregion
        #region SaveAttributes
        private List<string> _SaveAttributes;
        public List<string> SaveAttributes
        {
            get => _SaveAttributes;
            set => SetAndNotify(ref _SaveAttributes, value);
        }
        #endregion
        #region SaveDamageDiceSides
        private List<string> _SaveDamageDiceSides;
        public List<string> SaveDamageDiceSides
        {
            get => _SaveDamageDiceSides;
            set => SetAndNotify(ref _SaveDamageDiceSides, value);
        }
        #endregion

        // Databound Properties - Display
        #region DisplayFieldSet_DndSpell
        private bool _DisplayFieldSet_DndSpell;
        public bool DisplayFieldSet_DndSpell
        {
            get => _DisplayFieldSet_DndSpell;
            set => SetAndNotify(ref _DisplayFieldSet_DndSpell, value);
        }
        #endregion

        // Commands
        #region CastSpellAbility
        public ICommand CastSpellAbility => new RelayCommand(DoCastSpellAbility);
        private void DoCastSpellAbility(object param)
        {
            CreatureModel creature = null;
            CharacterModel character = null;
            CustomAbility abilityToUse = null;
            string message = string.Empty;
            string name = string.Empty;
            string mode = "Normal";
            int castingLevel = 0;
            bool castAsRitual = false;

            // Pre-Cast Check
            if (PrimaryAbilities.Count == 0) { HelperMethods.NotifyUser("No abilities available for this spell, please verify data."); return; }

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
                        HelperMethods.AddToGameplayLog("Insufficient spell slots to cast " + Name + ".");
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
                                HelperMethods.AddToGameplayLog(creature.DisplayName + " has insufficient spell slots to cast " + Name + ".", "Spell");
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
                        HelperMethods.AddToGameplayLog("Insufficient casting materials.");
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

            if (PrimaryAbilities.Count > 1)
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
                message += name + " casts " + Name + " as a ritual. ";
            }
            else if (SpellLevel > 0)
            {
                message += name + " casts " + Name + " at level " + castingLevel + ". ";
            }
            else
            {
                message += name + " casts " + Name + ". ";
            }

            // Set Casting Scale
            int castingScale = 0;
            if (SpellLevel == 0)
            {
                if (creature != null)
                {
                    if (int.TryParse(creature.ChallengeRating, out castingLevel) == false)
                    {
                        castingLevel = 1;
                    }
                    castingScale += CantripScaleAdd(castingLevel); 
                }
                else if (character != null) { castingScale += CantripScaleAdd(character.TotalLevel); }
            }
            else
            {
                castingScale = castingLevel - SpellLevel;
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
                HelperMethods.AddToGameplayLog(message, "Spell", true);
                //if (character != null)
                //{
                //    HelperMethods.AddToPlayerLog(message, "Default", true);
                //}
                //if (creature != null)
                //{
                //    if (Configuration.MainModelRef.TabSelected_Campaigns)
                //    {
                //        HelperMethods.AddToCampaignMessages(message, "Spell");
                //    }
                //    else
                //    {
                //        HelperMethods.AddToPlayerLog(message, "Default", true);
                //    }
                //}
                
            }
            else
            {
                message += "\nAn error has occurred while processing the ability.";
                HelperMethods.AddToGameplayLog(message, "Spell", true);
                //if (Configuration.MainModelRef.TabSelected_Campaigns)
                //{
                //    HelperMethods.AddToCampaignMessages(message, "Spell");
                //}
                //else
                //{
                //    HelperMethods.AddToGameplayLog(message, "Default", true);
                //}
            }

        }
        #endregion
        #region DuplicateSpell
        public ICommand DuplicateSpell => new RelayCommand(param => DoDuplicateSpell());
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
        public ICommand DeleteSpell => new RelayCommand(param => DoDeleteSpell());
        private void DoDeleteSpell()
        {
            if (IsADepedency()) { return; }
            Configuration.MainModelRef.SpellBuilderView.AllSpells.Remove(this);
            Configuration.MainModelRef.SpellBuilderView.FilteredSpells.Remove(this);
        }
        #endregion
        #region AddConsumedMaterial
        public ICommand AddConsumedMaterial => new RelayCommand(param => DoAddConsumedMaterial());
        private void DoAddConsumedMaterial()
        {
            ObjectSelectionDialog itemSelect = new(Configuration.ItemRepository.ToList());

            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }
                ItemModel itemToAdd = HelperMethods.DeepClone(itemSelect.SelectedObject as ItemModel);
                itemToAdd.Quantity = 1;

                ConsumedMaterials.Add(itemToAdd);

            }
        }
        #endregion
        #region AddSpellClass
        public ICommand AddSpellClass => new RelayCommand(param => DoAddSpellClass());
        private void DoAddSpellClass()
        {
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
            if (param.ToString() == "Primary Quick Attack")
            {
                PrimaryAbilities.Add(new());
                if (!PrimaryAbilities.Last().PopulateFromQuickForm()) { PrimaryAbilities.Remove(PrimaryAbilities.Last()); }
            }
            if (param.ToString() == "Primary Quick Save")
            {
                PrimaryAbilities.Add(new());
                if (!PrimaryAbilities.Last().PopulateFromQuickForm(true)) { PrimaryAbilities.Remove(PrimaryAbilities.Last()); }
            }
            if (param.ToString() == "Secondary Quick Attack")
            {
                SecondaryAbilities.Add(new());
                if (!SecondaryAbilities.Last().PopulateFromQuickForm()) { SecondaryAbilities.Remove(SecondaryAbilities.Last()); }
            }
            if (param.ToString() == "Secondary Quick Save")
            {
                SecondaryAbilities.Add(new());
                if (!SecondaryAbilities.Last().PopulateFromQuickForm(true)) { SecondaryAbilities.Remove(SecondaryAbilities.Last()); }
            }
        }
        #endregion
        #region PasteAbility
        public ICommand PasteAbility => new RelayCommand(DoPasteAbility);
        private void DoPasteAbility(object param)
        {
            if (Configuration.CopiedAbility == null) { return; }
            if (param == null) { return; }
            if (param.ToString() == "Primary")
            {
                PrimaryAbilities.Add(HelperMethods.DeepClone(Configuration.CopiedAbility));
            }
            if (param.ToString() == "Secondary")
            {
                SecondaryAbilities.Add(HelperMethods.DeepClone(Configuration.CopiedAbility));
            }
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
        private static int CantripScaleAdd(int casterLevel)
        {
            return casterLevel switch
            {
                5 or 6 or 7 or 8 or 9 or 10 => 1,
                11 or 12 or 13 or 14 or 15 or 16 => 2,
                17 or 18 or 19 or 20 => 3,
                _ => 0,
            };
        }
        private bool IsADepedency()
        {
            List<string> foundDependencies = new();
            foreach (CreatureModel creature in Configuration.CreatureRepository)
            {
                foreach (SpellLink spell in creature.SpellLinks)
                {
                    if (this.Name == spell.Name) { foundDependencies.Add("Spell for creature " + creature.Name); }
                }
            }
            foreach (CharacterModel character in Configuration.MainModelRef.CharacterBuilderView.Characters)
            {
                foreach (SpellLink spell in character.SpellLinks)
                {
                    if (this.Name == spell.Name) { foundDependencies.Add("Spell for character " + character.Name); }
                }
            }
            foreach (PlayerClassModel playerClass in Configuration.MainModelRef.ToolsView.PlayerClasses)
            {
                foreach (FeatureModel feature in playerClass.Features)
                {
                    foreach (FeatureData item in feature.Choices)
                    {
                        if (item.Name == this.Name) { foundDependencies.Add("Spell for class " + playerClass.Name); }
                    }
                }
            }
            foreach (PlayerSubclassModel playerSubClass in Configuration.MainModelRef.ToolsView.PlayerSubclasses)
            {
                foreach (FeatureModel feature in playerSubClass.Features)
                {
                    foreach (FeatureData item in feature.Choices)
                    {
                        if (item.Name == this.Name) { foundDependencies.Add("Spell for subclass " + playerSubClass.Name); }
                    }
                }
            }
            foreach (PlayerRaceModel playerRace in Configuration.MainModelRef.ToolsView.PlayerRaces)
            {
                foreach (FeatureModel feature in playerRace.Features)
                {
                    foreach (FeatureData item in feature.Choices)
                    {
                        if (item.Name == this.Name) { foundDependencies.Add("Spell for race " + playerRace.Name); }
                    }
                }
            }
            foreach (PlayerSubraceModel playerSubrace in Configuration.MainModelRef.ToolsView.PlayerSubraces)
            {
                foreach (FeatureModel feature in playerSubrace.Features)
                {
                    foreach (FeatureData item in feature.Choices)
                    {
                        if (item.Name == this.Name) { foundDependencies.Add("Spell for subrace " + playerSubrace.Name); }
                    }
                }
            }
            foreach (PlayerBackgroundModel playerBackground in Configuration.MainModelRef.ToolsView.PlayerBackgrounds)
            {
                foreach (FeatureModel feature in playerBackground.Features)
                {
                    foreach (FeatureData item in feature.Choices)
                    {
                        if (item.Name == this.Name) { foundDependencies.Add("Spell for background " + playerBackground.Name); }
                    }
                }
            }
            foreach (PlayerFeatModel playerFeat in Configuration.MainModelRef.ToolsView.PlayerFeats)
            {
                foreach (FeatureModel feature in playerFeat.Features)
                {
                    foreach (FeatureData item in feature.Choices)
                    {
                        if (item.Name == this.Name) { foundDependencies.Add("Spell for feat " + playerFeat.Name); }
                    }
                }
            }

            if (foundDependencies.Count > 0)
            {
                string message = "Unable to delete " + this.Name + ", dependencies found:\n";
                message += HelperMethods.GetStringFromList(foundDependencies, "\n");
                HelperMethods.NotifyUser(message, HelperMethods.UserNotificationType.Report);
                return true;
            }
            return false;
        }

    }
}
