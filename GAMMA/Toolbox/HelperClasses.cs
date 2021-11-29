using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Toolbox
{
    [Serializable]
    public class BoolOption : BaseModel
    {
        // Constructors 
        public BoolOption()
        {
            
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
        #region Quantity
        private int _Quantity;
        [XmlSaveMode("Single")]
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ChoiceSet
        private string _ChoiceSet;
        [XmlSaveMode("Single")]
        public string ChoiceSet
        {
            get
            {
                return _ChoiceSet;
            }
            set
            {
                _ChoiceSet = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Marked
        private bool _Marked;
        [XmlSaveMode("Single")]
        public bool Marked
        {
            get
            {
                return _Marked;
            }
            set
            {
                _Marked = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }

    [Serializable]
    public class LabeledNumber : BaseModel
    {
        // Constructors
        public LabeledNumber(List<string> nameSuggestions)
        {
            Name = "";
            Value = 0;
            NameSuggestions = nameSuggestions;
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
        #region NameSuggestions
        private List<string> _NameSuggestions;
        public List<string> NameSuggestions
        {
            get
            {
                return _NameSuggestions;
            }
            set
            {
                _NameSuggestions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Value
        private int _Value;
        [XmlSaveMode("Single")]
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveValue
        public ICommand RemoveValue => new RelayCommand(DoRemoveValue);
        private void DoRemoveValue(object param)
        {
            if (param == null) { return; }
            if (param.GetType() == typeof(CharacterAlterant))
            {
                (param as CharacterAlterant).StatChanges.Remove(this);
            }
            if (param.GetType() == typeof(MainViewModel))
            {
                if (Configuration.MainModelRef.TabSelected_ItemBuilder)
                {
                    Configuration.MainModelRef.ItemBuilderView.ActiveItem.StatChanges.Remove(this);
                }
            }
        }
        #endregion

    }

    [Serializable]
    public class ConvertibleValue : BaseModel
    {
        // Constructors
        public ConvertibleValue()
        {
            ValueType = "String";
        }
        public ConvertibleValue(string value)
        {
            Value = value;
            ValueType = "String";
        }

        // Databound Properties
        #region Value
        private string _Value;
        [XmlSaveMode("Single")]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ValueType
        private string _ValueType;
        [XmlSaveMode("Single")]
        public string ValueType
        {
            get
            {
                return _ValueType;
            }
            set
            {
                _ValueType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveConvertibleValue
        private RelayCommand _RemoveConvertibleValue;
        public ICommand RemoveConvertibleValue
        {
            get
            {
                if (_RemoveConvertibleValue == null)
                {
                    _RemoveConvertibleValue = new RelayCommand(DoRemoveConvertibleValue);
                }
                return _RemoveConvertibleValue;
            }
        }
        private void DoRemoveConvertibleValue(object param)
        {
            if (param == null) { new NotificationDialog("Invalid convertible value removal parameter.").ShowDialog(); return; }
            string location = param.ToString();
            switch (location)
            {
                case "Active Player Class":
                    Configuration.MainModelRef.ToolsView.ActivePlayerClass.EquipmentChoices.Remove(this);
                    break;
                case "Active Spell Classes":
                    Configuration.MainModelRef.SpellBuilderView.ActiveSpell.SpellClasses.Remove(this);
                    break;
                case "Class Weapon Proficiencies":
                case "Class Tool Proficiencies":
                case "Race Language Proficiencies":
                    break;
                case "ActivePlayerBackground.EquipmentChoices":
                    Configuration.MainModelRef.ToolsView.ActivePlayerBackground.EquipmentChoices.Remove(this);
                    break;
                case "Active Creature Environments":
                    Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Environments.Remove(this);
                    break;
                default:
                    new NotificationDialog("Unhandled parameter \"" + location + "\" in DoRemoveConvertibleValue").ShowDialog();
                    break;
            }
        }
        #endregion
        #region RemoveFromCollection
        public ICommand RemoveFromCollection => new RelayCommand(DoRemoveFromCollection);
        private void DoRemoveFromCollection(object param)
        {
            if (param.GetType() == typeof(CAPreAction))
            {
                (param as CAPreAction).Answers.Remove(this);
            }
        }
        #endregion
        #region AddItemsToActiveCharacterStartingEquipment
        private RelayCommand _AddItemsToActiveCharacterStartingEquipment;
        public ICommand AddItemsToActiveCharacterStartingEquipment
        {
            get
            {
                if (_AddItemsToActiveCharacterStartingEquipment == null)
                {
                    _AddItemsToActiveCharacterStartingEquipment = new RelayCommand(param => DoAddItemsToActiveCharacterStartingEquipment());
                }
                return _AddItemsToActiveCharacterStartingEquipment;
            }
        }
        private void DoAddItemsToActiveCharacterStartingEquipment()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList(), Value);
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    foreach (ItemLink itemLink in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ChosenEquipment)
                    {
                        if (item.Name == itemLink.Name)
                        {
                            itemLink.Quantity += itemLink.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ChosenEquipment.Add(new() { Name = item.Name, Quantity = item.Quantity });
                }
            }
        }
        #endregion

    }

    [Serializable]
    public class ConvertibleValueSet : BaseModel
    {
        // Constructors
        public ConvertibleValueSet()
        {
            Values = new();
        }

        // Databound Properties
        #region Label
        private string _Label;
        [XmlSaveMode("Single")]
        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                _Label = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Values
        private ObservableCollection<ConvertibleValue> _Values;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ConvertibleValue> Values
        {
            get
            {
                return _Values;
            }
            set
            {
                _Values = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        

    }

}
