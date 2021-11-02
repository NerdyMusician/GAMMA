using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerFeatModel : PlayerBuildingBlock
    {
        // Constructors
        public PlayerFeatModel()
        {
            Name = "New Feat";
        }

        // Databound Properties
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

        // Commands
        #region RemoveFeat
        private RelayCommand _RemoveFeat;
        public ICommand RemoveFeat
        {
            get
            {
                if (_RemoveFeat == null)
                {
                    _RemoveFeat = new RelayCommand(DoRemoveFeat);
                }
                return _RemoveFeat;
            }
        }
        private void DoRemoveFeat(object param)
        {
            Configuration.MainModelRef.ToolsView.PlayerFeats.Remove(this);
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            PlayerFeatModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.PlayerFeats.Insert(Configuration.MainModelRef.ToolsView.PlayerFeats.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActivePlayerFeat = duplicate;
        }
        #endregion

    }
}
