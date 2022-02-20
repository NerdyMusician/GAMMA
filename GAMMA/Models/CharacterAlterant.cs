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
            get => _Name;
            set => SetAndNotify(ref _Name, value);
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
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode(XSME.Single)]
        public bool IsActive
        {
            get => _IsActive;
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
            get => _InEditMode;
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
            get => _StatChanges;
            set => SetAndNotify(ref _StatChanges, value);
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
            UpdateCharacterStats();
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
        private static void UpdateCharacterStats()
        {
            if (Configuration.LoadComplete == false) { return; }
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdateModifiers();
        }

    }
}
