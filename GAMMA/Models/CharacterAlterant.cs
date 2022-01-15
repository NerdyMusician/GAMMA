using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class CharacterAlterant : BaseModel
    {
        // Constructors
        public CharacterAlterant()
        {
            Name = "New Alterant";
            StatChanges = new();
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
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
        [XmlSaveMode(XSME.Single)]
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
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode(XSME.Single)]
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
                NotifyPropertyChanged();
                UpdateCharacterStats();
            }
        }
        #endregion
        #region InEditMode
        private bool _InEditMode;
        public bool InEditMode
        {
            get
            {
                return _InEditMode;
            }
            set
            {
                _InEditMode = value;
                NotifyPropertyChanged();
                UpdateCharacterStats();
            }
        }
        #endregion
        #region StatChanges
        private ObservableCollection<LabeledNumber> _StatChanges;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<LabeledNumber> StatChanges
        {
            get
            {
                return _StatChanges;
            }
            set
            {
                _StatChanges = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddStatChange
        public ICommand AddStatChange => new RelayCommand(DoAddStatChange);
        private void DoAddStatChange(object param)
        {
            StatChanges.Add(new(Configuration.AlterantStats));
        }
        #endregion
        #region RemoveAlterant
        public ICommand RemoveAlterant => new RelayCommand(DoRemoveAlterant);
        private void DoRemoveAlterant(object param)
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Alterants.Remove(this);
        }
        #endregion
        #region ToggleActive
        public ICommand ToggleActive => new RelayCommand(DoToggleActive);
        private void DoToggleActive(object param)
        {
            IsActive = !IsActive;
        }
        #endregion

        // Private Methods
        private void UpdateCharacterStats()
        {
            if (Configuration.LoadComplete == false) { return; }
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdateModifiers();
        }

    }
}
