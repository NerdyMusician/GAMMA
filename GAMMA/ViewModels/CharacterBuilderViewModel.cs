using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class CharacterBuilderViewModel : BaseModel
    {
        // Constructors
        public CharacterBuilderViewModel()
        {
            XmlMethods.XmlToList(Configuration.CharacterDataFilePath, out List<CharacterModel> characters);
            Characters = new ObservableCollection<CharacterModel>(characters);
            foreach (CharacterModel character in Characters)
            {
                character.SetPropertyChanged();
                character.UpdateClassTotals();
                character.UpdateSubclassLists();
                character.UpdateInventoryStats();
                character.ConnectSpellLinks();
                character.UpdatePreparedSpellCount();
                foreach (CreatureModel minion in character.Minions)
                {
                    minion.SetFormattedTexts();
                }
                character.UpdateInventoryItemCategories();
                foreach (InventoryModel inventory in character.Inventories)
                {
                    inventory.GetUpdatedItemData();
                    inventory.UpdateFilteredList();
                }
                character.ConnectItemLinks();
                character.UpdateModifiers();
                character.UpdateAbilityDropdowns();
                character.MaxHealth = character.GetCalculatedMaxHitPoints(character.ConstitutionModifier);
                character.ValidateCharacterCreation();
            }
            ShowCharacterList = true;
            RunNullSpellLinkCheck();
        }

        // Databound Properties
        #region Characters
        private ObservableCollection<CharacterModel> _Characters;
        public ObservableCollection<CharacterModel> Characters
        {
            get
            {
                return _Characters;
            }
            set
            {
                _Characters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveCharacter
        private CharacterModel _ActiveCharacter;
        public CharacterModel ActiveCharacter
        {
            get
            {
                return _ActiveCharacter;
            }
            set
            {
                if (_ActiveCharacter != null && value != null)
                {
                    if (_ActiveCharacter.Name != value.Name)
                    {
                        LastActiveCharacter = _ActiveCharacter;
                    }
                }
                _ActiveCharacter = value;
                NotifyPropertyChanged();
                if (value == null) { return; }
                Configuration.HasUsedCharacterBuilder = true;
                if (value.OutputLinkedToRoll20 == true)
                {
                    HelperMethods.SwitchRoll20ChatAsCurrent();
                }
            }
        }
        #endregion
        #region LastActiveCharacter
        private CharacterModel _LastActiveCharacter;
        public CharacterModel LastActiveCharacter
        {
            get
            {
                return _LastActiveCharacter;
            }
            set
            {
                _LastActiveCharacter = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowCharacterList
        private bool _ShowCharacterList;
        public bool ShowCharacterList
        {
            get
            {
                return _ShowCharacterList;
            }
            set
            {
                _ShowCharacterList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddCharacter
        private RelayCommand _AddCharacter;
        public ICommand AddCharacter
        {
            get
            {
                if (_AddCharacter == null)
                {
                    _AddCharacter = new RelayCommand(param => DoAddCharacter());
                }
                return _AddCharacter;
            }
        }
        private void DoAddCharacter()
        {
            Characters.Add(new CharacterModel());
            ActiveCharacter = Characters.Last();
            ActiveCharacter.Inventories.Add(new InventoryModel(ActiveCharacter));
            ActiveCharacter.Inventories[0].Name = "Backpack";
            ActiveCharacter.Inventories[0].IsCarried = true;
        }
        #endregion
        #region SaveCharacters
        private RelayCommand _SaveCharacters;
        public ICommand SaveCharacters
        {
            get
            {
                if (_SaveCharacters == null)
                {
                    _SaveCharacters = new RelayCommand(param => DoSaveCharacters());
                }
                return _SaveCharacters;
            }
        }
        public bool DoSaveCharacters(bool notifyUser = true)
        {
            if (RunSaveValidation() == false) { return false; }
            if (Characters.Count() == 0)
            {
                // Prevents zero character save crash
                XDocument blankDoc = new();
                blankDoc.Add(new XElement("CharacterModelSet"));
                blankDoc.Save("Data/Characters.xml");
                return true;
            }
            XDocument itemDocument = new();
            itemDocument.Add(XmlMethods.ListToXml(Characters.ToList()));
            itemDocument.Save("Data/Characters.xml");
            HelperMethods.WriteToLogFile("Characters Saved.", notifyUser);
            return true;
        }
        #endregion
        #region SortCharacters
        private RelayCommand _SortCharacters;
        public ICommand SortCharacters
        {
            get
            {
                if (_SortCharacters == null)
                {
                    _SortCharacters = new RelayCommand(param => DoSortCharacters());
                }
                return _SortCharacters;
            }
        }
        private void DoSortCharacters()
        {
            Characters = new(Characters.OrderBy(c => c.Name));
        }
        #endregion
        #region ImportCharacters
        private RelayCommand _ImportCharacters;
        public ICommand ImportCharacters
        {
            get
            {
                if (_ImportCharacters == null)
                {
                    _ImportCharacters = new RelayCommand(param => DoImportCharacters());
                }
                return _ImportCharacters;
            }
        }
        private void DoImportCharacters()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current character list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            if (DoSaveCharacters() == false) { return; }

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerCharacters(openWindow.FileName, out string message);
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion
        #region ToggleLastCharacter
        private RelayCommand _ToggleLastCharacter;
        public ICommand ToggleLastCharacter
        {
            get
            {
                if (_ToggleLastCharacter == null)
                {
                    _ToggleLastCharacter = new RelayCommand(param => DoToggleLastCharacter());
                }
                return _ToggleLastCharacter;
            }
        }
        private void DoToggleLastCharacter()
        {
            ActiveCharacter = LastActiveCharacter;
            if (ActiveCharacter.OutputLinkedToRoll20)
            {
                HelperMethods.SwitchRoll20ChatAsCurrent();
            }
        }
        #endregion
        #region OpenCharacterCreator
        private RelayCommand _OpenCharacterCreator;
        public ICommand OpenCharacterCreator
        {
            get
            {
                if (_OpenCharacterCreator == null)
                {
                    _OpenCharacterCreator = new RelayCommand(DoOpenCharacterCreator);
                }
                return _OpenCharacterCreator;
            }
        }
        private void DoOpenCharacterCreator(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("No parameter passed for CharacterBuilderViewModel.DoOpenCharacterCreator.", true); return; }
            try
            {
                switch (param.ToString())
                {
                    case "New":
                        DoAddCharacter();
                        CharacterCreatorDialog characterCreator = new(ActiveCharacter);
                        if (characterCreator.ShowDialog() == true)
                        {
                            int activeChar = Characters.IndexOf(ActiveCharacter);
                            ActiveCharacter = null;
                            Characters.RemoveAt(activeChar);
                            Characters.Add(characterCreator.DataContext as CharacterModel);
                            ActiveCharacter = Characters.Last();
                            ActiveCharacter.ReinitializeEventHandlers();
                            ActiveCharacter.ResetSpellSlots();
                        }
                        else
                        {
                            Characters.Remove(ActiveCharacter);
                        }
                        break;
                    case "Existing":
                        int originalCharacter = Characters.IndexOf(ActiveCharacter);
                        Characters.Add(HelperMethods.DeepClone(ActiveCharacter));
                        ActiveCharacter = Characters.Last();
                        ActiveCharacter.UpdateCharacterSheet();
                        CharacterCreatorDialog characterEditor = new(ActiveCharacter);
                        if (characterEditor.ShowDialog() == true)
                        {
                            Characters.RemoveAt(originalCharacter);
                            ActiveCharacter.ReinitializeEventHandlers();
                        }
                        else
                        {
                            Characters.Remove(ActiveCharacter);
                            ActiveCharacter = Characters[originalCharacter];
                        }
                        break;
                    default:
                        HelperMethods.WriteToLogFile("Unhandled parameter " + param.ToString() + " in CharacterBuilderViewModel.DoOpenCharacterCreator.", true);
                        return;
                }
            }
            catch (Exception e)
            {
                HelperMethods.WriteToLogFile(e.Message, true);
                ActiveCharacter.DisplayCharacterCreationWarning = true;
                return;
            }
            
        }
        #endregion

        // Public Methods
        public void RunNullSpellLinkCheck()
        {
            string message = "Missing spell links:";
            foreach (CharacterModel character in Characters)
            {
                foreach (SpellLink link in character.SpellLinks)
                {
                    if (link.LinkedSpell == null) { message += "\n" + character.Name + " : " + link.Name; }
                }
            }
            if (message != "Missing spell links:") { new NotificationDialog(message).ShowDialog(); }
        }

        // Private Methods
        private bool RunSaveValidation()
        {
            List<string> validationFailures = new();
            List<string> characterNames = new();
            foreach (CharacterModel character in Characters)
            {
                if (characterNames.Contains(character.Name) == false)
                {
                    characterNames.Add(character.Name);
                }
                else
                {
                    validationFailures.Add("Duplicate character name \"" + character.Name +"\"");
                }
            }

            if (validationFailures.Count() > 0)
            {
                string message = "Validation Failures Encountered:";
                foreach (string failure in validationFailures)
                {
                    message += "\n" + failure;
                }
                message += "\n\nCharacter Data has not been saved.";
                new NotificationDialog(message).ShowDialog();
                return false;
            }

            return true;

        }

    }
}
