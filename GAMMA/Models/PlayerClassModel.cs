using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerClassModel : PlayerBuildingBlock
    {
        // Constructors
        public PlayerClassModel()
        {
            Name = "New Player Class";
        }

        // Databound Properties
        #region HitDice
        private int _HitDice;
        [XmlSaveMode(XSME.Single)]
        public int HitDice
        {
            get
            {
                return _HitDice;
            }
            set
            {
                _HitDice = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemovePlayerClass
        private RelayCommand _RemovePlayerClass;
        public ICommand RemovePlayerClass
        {
            get
            {
                if (_RemovePlayerClass == null)
                {
                    _RemovePlayerClass = new RelayCommand(DoRemovePlayerClass);
                }
                return _RemovePlayerClass;
            }
        }
        private void DoRemovePlayerClass(object param)
        {
            if (param.ToString() == "Configuration")
            {
                Configuration.MainModelRef.ToolsView.PlayerClasses.Remove(this);
            }
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            PlayerClassModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.PlayerClasses.Insert(Configuration.MainModelRef.ToolsView.PlayerClasses.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActivePlayerClass = duplicate;
        }
        #endregion

    }
}
