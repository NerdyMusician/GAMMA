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
        }

        // Databound Properties
        #region SubraceOf
        private string _SubraceOf;
        [XmlSaveMode(XSME.Single)]
        public string SubraceOf
        {
            get => _SubraceOf;
            set => SetAndNotify(ref _SubraceOf, value);
        }
        #endregion

        // Commands
        #region RemovePlayerSubrace
        public ICommand RemovePlayerSubrace => new RelayCommand(param => DoRemovePlayerSubrace());
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
