using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerSubraceModel : PlayerBuildingBlock
    {
        // Constructors
        public PlayerSubraceModel()
        {
            Name = "New Subrace";
            //Traits = new ObservableCollection<TraitModel>();
        }

        // Databound Properties
        #region SubraceOf
        private string _SubraceOf;
        [XmlSaveMode("Single")]
        public string SubraceOf
        {
            get
            {
                return _SubraceOf;
            }
            set
            {
                _SubraceOf = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemovePlayerSubrace
        private RelayCommand _RemovePlayerSubrace;
        public ICommand RemovePlayerSubrace
        {
            get
            {
                if (_RemovePlayerSubrace == null)
                {
                    _RemovePlayerSubrace = new RelayCommand(param => DoRemovePlayerSubrace());
                }
                return _RemovePlayerSubrace;
            }
        }
        private void DoRemovePlayerSubrace()
        {
            Configuration.MainModelRef.ToolsView.PlayerSubraces.Remove(this);
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            PlayerSubraceModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.PlayerSubraces.Insert(Configuration.MainModelRef.ToolsView.PlayerSubraces.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActivePlayerSubrace = duplicate;
        }
        #endregion

    }
}
