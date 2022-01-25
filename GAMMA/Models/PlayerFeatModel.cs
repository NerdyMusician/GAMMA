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
        [XmlSaveMode(XSME.Single)]
        public string Description
        {
            get => _Description;
            set => SetAndNotify(ref _Description, value);
        }
        #endregion

        // Commands
        #region RemoveFeat
        public ICommand RemoveFeat => new RelayCommand(DoRemoveFeat);
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
