using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class TraitModel : BaseModel
    {
        // Constructors
        public TraitModel()
        {
            Name = "New Trait";
            Description = "";
        }

        // Databound Properties
        #region LevelObtained
        private int _LevelObtained;
        [XmlSaveMode("Single")]
        public int LevelObtained
        {
            get
            {
                return _LevelObtained;
            }
            set
            {
                _LevelObtained = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
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
            }
        }
        #endregion

        // Commands
        #region RemoveTraitFromPlayer
        private RelayCommand _RemoveTraitFromPlayer;
        public ICommand RemoveTraitFromPlayer
        {
            get
            {
                if (_RemoveTraitFromPlayer == null)
                {
                    _RemoveTraitFromPlayer = new RelayCommand(param => DoRemoveTraitFromPlayer());
                }
                return _RemoveTraitFromPlayer;
            }
        }
        private void DoRemoveTraitFromPlayer()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Traits.Remove(this);
        }
        #endregion
        #region RemoveTraitFromCreature
        private RelayCommand _RemoveTraitFromCreature;
        public ICommand RemoveTraitFromCreature
        {
            get
            {
                if (_RemoveTraitFromCreature == null)
                {
                    _RemoveTraitFromCreature = new RelayCommand(param => DoRemoveTraitFromCreature());
                }
                return _RemoveTraitFromCreature;
            }
        }
        private void DoRemoveTraitFromCreature()
        {
            Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Traits.Remove(this);
        }
        #endregion
        #region RemoveTrait
        private RelayCommand _RemoveTrait;
        public ICommand RemoveTrait
        {
            get
            {
                if (_RemoveTrait == null)
                {
                    _RemoveTrait = new RelayCommand(DoRemoveTrait);
                }
                return _RemoveTrait;
            }
        }
        private void DoRemoveTrait(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("Invalid parameter to trait removal method.", true); return; }
            switch (param.ToString())
            {
                case "Active Player Class":
                case "Active Player Subrace":
                case "Active Player Background":
                    break;
                case "Active Player Subclass":
                    Configuration.MainModelRef.ToolsView.ActivePlayerSubclass.Traits.Remove(this);
                    break;
                case "Active Player Race":
                    Configuration.MainModelRef.ToolsView.ActivePlayerRace.Traits.Remove(this);
                    break;
                default:
                    HelperMethods.WriteToLogFile("Unhandled trait removal parameter: " + param.ToString(), true);
                    break;
            }
        }
        #endregion

    }
}
