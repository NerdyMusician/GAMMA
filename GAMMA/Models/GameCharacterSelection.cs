using GAMMA.Toolbox;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class GameCharacterSelection : BaseModel
    {
        // Constructors
        public GameCharacterSelection()
        {

        }

        // Databound Properties
        #region IsSelected
        private bool _IsSelected;
        [XmlSaveMode(XSME.Single)]
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                NotifyPropertyChanged();
                if (value) { UnselectExceptThis(); }
            }
        }
        #endregion
        #region Game
        private string _Game;
        [XmlSaveMode(XSME.Single)]
        public string Game
        {
            get => _Game;
            set => SetAndNotify(ref _Game, value);
        }
        #endregion
        #region Character
        private string _Character;
        [XmlSaveMode(XSME.Single)]
        public string Character
        {
            get => _Character;
            set => SetAndNotify(ref _Character, value);
        }
        #endregion

        // Commands
        #region RemovePair
        public ICommand RemovePair => new RelayCommand(param => DoRemovePair());
        private void DoRemovePair()
        {
            Configuration.MainModelRef.SettingsView.Roll20GameCharacterList.Remove(this);
        }
        #endregion

        // Private Methods
        private void UnselectExceptThis()
        {
            if (Configuration.MainModelRef.SettingsView == null) { return; }
            foreach (GameCharacterSelection pair in Configuration.MainModelRef.SettingsView.Roll20GameCharacterList)
            {
                if (pair != this) { pair.IsSelected = false; }
            }
        }

    }
}
