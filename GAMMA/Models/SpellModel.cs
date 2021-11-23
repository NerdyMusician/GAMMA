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
            ConsumedMaterials = new ObservableCollection<ItemModel>();
            SpellClasses = new ObservableCollection<ConvertibleValue>();
            PrimaryAbilities = new();
            SecondaryAbilities = new();
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode("Single")]
        public string Sourcebook
        {
            get
            {
                return _Sourcebook;
            }
            set
            {
                _Sourcebook = value;
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
        #region SpellClasses
        private ObservableCollection<ConvertibleValue> _SpellClasses;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ConvertibleValue> SpellClasses
        {
            get
            {
                return _SpellClasses;
            }
            set
            {
                _SpellClasses = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode("Single")]
        public bool IsValidated
        {
            get
            {
                return _IsValidated;
            }
            set
            {
                _IsValidated = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SchoolOfMagic
        private string _SchoolOfMagic;
        [XmlSaveMode("Single")]
        public string SchoolOfMagic
        {
            get
            {
                return _SchoolOfMagic;
            }
            set
            {
                _SchoolOfMagic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CastingTime
        private string _CastingTime;
        [XmlSaveMode("Single")]
        public string CastingTime
        {
            get
            {
                return _CastingTime;
            }
            set
            {
                _CastingTime = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellDuration
        private string _SpellDuration;
        [XmlSaveMode("Single")]
        public string SpellDuration
        {
            get
            {
                return _SpellDuration;
            }
            set
            {
                _SpellDuration = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellLevel
        private int _SpellLevel;
        [XmlSaveMode("Single")]
        public int SpellLevel
        {
            get
            {
                return _SpellLevel;
            }
            set
            {
                _SpellLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Range
        private string _Range;
        [XmlSaveMode("Single")]
        public string Range
        {
            get
            {
                return _Range;
            }
            set
            {
                _Range = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasAreaOfEffect
        private bool _HasAreaOfEffect;
        [XmlSaveMode("Single")]
        public bool HasAreaOfEffect
        {
            get
            {
                return _HasAreaOfEffect;
            }
            set
            {
                _HasAreaOfEffect = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeRange
        private int _AoeRange;
        [XmlSaveMode("Single")]
        public int AoeRange
        {
            get
            {
                return _AoeRange;
            }
            set
            {
                _AoeRange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeRange2
        private int _AoeRange2;
        [XmlSaveMode("Proto")]
        public int AoeRange2
        {
            get
            {
                return _AoeRange2;
            }
            set
            {
                _AoeRange2 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeShape
        private string _AoeShape;
        [XmlSaveMode("Single")]
        public string AoeShape
        {
            get
            {
                return _AoeShape;
            }
            set
            {
                _AoeShape = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region IsPrepared
        private bool _IsPrepared;
        [XmlSaveMode("Single")]
        public bool IsPrepared
        {
            get
            {
                return _IsPrepared;
            }
            set
            {
                _IsPrepared = value;
                NotifyPropertyChanged();

            }
        }
        #endregion
        #region HasScaling
        // Indicates if the spell should show a list of higher level casting options
        private bool _HasScaling;
        [XmlSaveMode("Single")]
        public bool HasScaling
        {
            get
            {
                return _HasScaling;
            }
            set
            {
                _HasScaling = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RitualCapable
        private bool _RitualCapable;
        [XmlSaveMode("Single")]
        public bool RitualCapable
        {
            get
            {
                return _RitualCapable;
            }
            set
            {
                _RitualCapable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresConcentration
        private bool _RequiresConcentration;
        [XmlSaveMode("Single")]
        public bool RequiresConcentration
        {
            get
            {
                return _RequiresConcentration;
            }
            set
            {
                _RequiresConcentration = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresSomatic
        private bool _RequiresSomatic;
        [XmlSaveMode("Single")]
        public bool RequiresSomatic
        {
            get
            {
                return _RequiresSomatic;
            }
            set
            {
                _RequiresSomatic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RequiresVerbal
        private bool _RequiresVerbal;
        [XmlSaveMode("Single")]
        public bool RequiresVerbal
        {
            get
            {
                return _RequiresVerbal;
            }
            set
            {
                _RequiresVerbal = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region RequiresMaterial
        private bool _RequiresMaterial;
        [XmlSaveMode("Single")]
        public bool RequiresMaterial
        {
            get
            {
                return _RequiresMaterial;
            }
            set
            {
                _RequiresMaterial = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DoesConsumeMaterials
        private bool _DoesConsumeMaterials;
        [XmlSaveMode("Single")]
        public bool DoesConsumeMaterials
        {
            get
            {
                return _DoesConsumeMaterials;
            }
            set
            {
                _DoesConsumeMaterials = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ConsumedMaterials
        private ObservableCollection<ItemModel> _ConsumedMaterials;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ItemModel> ConsumedMaterials
        {
            get
            {
                return _ConsumedMaterials;
            }
            set
            {
                _ConsumedMaterials = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Materials
        private string _Materials;
        [XmlSaveMode("Single")]
        public string Materials
        {
            get
            {
                return _Materials;
            }
            set
            {
                _Materials = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PrimaryAbilities
        private ObservableCollection<CustomAbility> _PrimaryAbilities;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomAbility> PrimaryAbilities
        {
            get
            {
                return _PrimaryAbilities;
            }
            set
            {
                _PrimaryAbilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SecondaryAbilities
        private ObservableCollection<CustomAbility> _SecondaryAbilities;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CustomAbility> SecondaryAbilities
        {
            get
            {
                return _SecondaryAbilities;
            }
            set
            {
                _SecondaryAbilities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region SchoolsOfMagic
        private List<string> _SchoolsOfMagic;
        [XmlSaveMode("")]
        public List<string> SchoolsOfMagic
        {
            get
            {
                return _SchoolsOfMagic;
            }
            set
            {
                _SchoolsOfMagic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeShapes
        private List<string> _AoeShapes;
        [XmlSaveMode("")]
        public List<string> AoeShapes
        {
            get
            {
                return _AoeShapes;
            }
            set
            {
                _AoeShapes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageDiceSides
        private List<string> _DamageDiceSides;
        [XmlSaveMode("")]
        public List<string> DamageDiceSides
        {
            get
            {
                return _DamageDiceSides;
            }
            set
            {
                _DamageDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageTypes
        private List<string> _DamageTypes;
        [XmlSaveMode("")]
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
        #region SaveDamageTypes
        private List<string> _SaveDamageTypes;
        [XmlSaveMode("")]
        public List<string> SaveDamageTypes
        {
            get
            {
                return _SaveDamageTypes;
            }
            set
            {
                _SaveDamageTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HealingDiceSides
        private List<string> _HealingDiceSides;
        [XmlSaveMode("")]
        public List<string> HealingDiceSides
        {
            get
            {
                return _HealingDiceSides;
            }
            set
            {
                _HealingDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveAttributes
        private List<string> _SaveAttributes;
        [XmlSaveMode("")]
        public List<string> SaveAttributes
        {
            get
            {
                return _SaveAttributes;
            }
            set
            {
                _SaveAttributes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SaveDamageDiceSides
        private List<string> _SaveDamageDiceSides;
        [XmlSaveMode("")]
        public List<string> SaveDamageDiceSides
        {
            get
            {
                return _SaveDamageDiceSides;
            }
            set
            {
                _SaveDamageDiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Display
        #region DisplayFieldSet_DndSpell
        private bool _DisplayFieldSet_DndSpell;
        public bool DisplayFieldSet_DndSpell
        {
            get
            {
                return _DisplayFieldSet_DndSpell;
            }
            set
            {
                _DisplayFieldSet_DndSpell = value;
                NotifyPropertyChanged();
            }
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
            string message = "";
            string name = "";
            string mode = "Normal";
            int castingLevel = 0;
            bool castAsRitual = false;

            // Pre-Cast Check
            if (PrimaryAbilities.Count() == 0) { HelperMethods.NotifyUser("No abilities available for this spell, please verify data."); return; }

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
                        HelperMethods.AddToPlayerLog("Insufficient spell slots to cast " + Name + ".");
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
                                HelperMethods.AddToCampaignMessages(creature.DisplayName + " has insufficient spell slots to cast " + Name + ".", "Spell");
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
                        HelperMethods.AddToPlayerLog("Insufficient casting materials.");
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

            if (PrimaryAbilities.Count() > 1)
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
                message += name + " casts " + Name + " as a ritual.";
            }
            else if (SpellLevel > 0)
            {
                message += name + " casts " + Name + " at level " + castingLevel + ".";
            }
            else
            {
                message += name + " casts " + Name + ".";
            }

            // Set Casting Scale
            int castingScale = 0;
            if (SpellLevel == 0)
            {
                if (mode == "Creature")
                {
                    if (int.TryParse(creature.ChallengeRating, out castingLevel) == false)
                    {
                        castingLevel = 1;
                    }
                    castingScale += CantripScaleAdd(castingLevel); 
                }
                if (mode == "Character") { castingScale += CantripScaleAdd(character.Level); }
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
                if (character != null)
                {
                    HelperMethods.AddToPlayerLog(message, "Default", true);
                }
                if (creature != null)
                {
                    if (Configuration.MainModelRef.TabSelected_Campaigns)
                    {
                        HelperMethods.AddToCampaignMessages(message, "Spell");
                    }
                    else
                    {
                        HelperMethods.AddToPlayerLog(message, "Default", true);
                    }
                }
                
            }
            else
            {
                message += "\nAn error has occurred while processing the ability.";
                if (Configuration.MainModelRef.TabSelected_Campaigns)
                {
                    HelperMethods.AddToCampaignMessages(message, "Spell");
                }
                else
                {
                    HelperMethods.AddToPlayerLog(message, "Default", true);
                }
            }

        }
        #endregion
        #region DuplicateSpell
        public ICommand DuplicateSpell
        {
            get
            {
                return new RelayCommand(param => DoDuplicateSpell());
            }
        }
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
        public ICommand DeleteSpell
        {
            get
            {
                return new RelayCommand(param => DoDeleteSpell());
            }
        }
        private void DoDeleteSpell()
        {
            Configuration.MainModelRef.SpellBuilderView.AllSpells.Remove(this);
            Configuration.MainModelRef.SpellBuilderView.FilteredSpells.Remove(this);
        }
        #endregion
        #region AddConsumedMaterial
        private RelayCommand _AddConsumedMaterial;
        public ICommand AddConsumedMaterial
        {
            get
            {
                if (_AddConsumedMaterial == null)
                {
                    _AddConsumedMaterial = new RelayCommand(param => DoAddConsumedMaterial());
                }
                return _AddConsumedMaterial;
            }
        }
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
        private RelayCommand _AddSpellClass;
        public ICommand AddSpellClass
        {
            get
            {
                if (_AddSpellClass == null)
                {
                    _AddSpellClass = new RelayCommand(param => DoAddSpellClass());
                }
                return _AddSpellClass;
            }
        }
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
        private int CantripScaleAdd(int casterLevel)
        {
            return casterLevel switch
            {
                5 or 6 or 7 or 8 or 9 or 10 => 1,
                11 or 12 or 13 or 14 or 15 or 16 => 2,
                17 or 18 or 19 or 20 => 3,
                _ => 0,
            };
        }

    }
}
