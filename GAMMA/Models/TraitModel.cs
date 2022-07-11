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
            Description = string.Empty;
        }

        // Databound Properties
        #region LevelObtained
        private int _LevelObtained;
        [XmlSaveMode(XSME.Single)]
        public int LevelObtained
        {
            get => _LevelObtained;
            set => SetAndNotify(ref _LevelObtained, value);
        }
        #endregion
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
        #region InEditMode
        private bool _InEditMode;
        public bool InEditMode
        {
            get => _InEditMode;
            set => SetAndNotify(ref _InEditMode, value);
        }
        #endregion

        // Commands
        #region RemoveTraitFromPlayer
        public ICommand RemoveTraitFromPlayer => new RelayCommand(param => DoRemoveTraitFromPlayer());
        private void DoRemoveTraitFromPlayer()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Traits.Remove(this);
        }
        #endregion
        #region RemoveTraitFromCreature
        public ICommand RemoveTraitFromCreature => new RelayCommand(param => DoRemoveTraitFromCreature());
        private void DoRemoveTraitFromCreature()
        {
            Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Traits.Remove(this);
        }
        #endregion
        #region RemoveTrait
        public ICommand RemoveTrait => new RelayCommand(DoRemoveTrait);
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
