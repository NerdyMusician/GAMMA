using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get
            {
                return _IsSelected;
            }
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
            get
            {
                return _Game;
            }
            set
            {
                _Game = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Character
        private string _Character;
        [XmlSaveMode(XSME.Single)]
        public string Character
        {
            get
            {
                return _Character;
            }
            set
            {
                _Character = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemovePair
        private RelayCommand _RemovePair;
        public ICommand RemovePair
        {
            get
            {
                if (_RemovePair == null)
                {
                    _RemovePair = new RelayCommand(param => DoRemovePair());
                }
                return _RemovePair;
            }
        }
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
