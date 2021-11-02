using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class SpellLink : BaseModel
    {
        // Constructors
        public SpellLink()
        {

        }

        // Databound Properties
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
        #region IsPrepared
        private bool _IsPrepared;
        [XmlSaveMode("Single")]
        public bool IsPrepared
        {
            get
            {
                return _IsPrepared;
            }
            set
            {
                _IsPrepared = value;
                NotifyPropertyChanged();

                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdatePreparedSpellCount();

            }
        }
        #endregion
        #region LinkedSpell
        private SpellModel _LinkedSpell;
        public SpellModel LinkedSpell
        {
            get
            {
                return _LinkedSpell;
            }
            set
            {
                _LinkedSpell = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveSpellLink
        private RelayCommand _RemoveSpellLink;
        public ICommand RemoveSpellLink
        {
            get
            {
                if (_RemoveSpellLink == null)
                {
                    _RemoveSpellLink = new RelayCommand(DoRemoveSpellLink);
                }
                return _RemoveSpellLink;
            }
        }
        private void DoRemoveSpellLink(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("Missing parameter for DoRemoveSpellLink", true); return; }
            string location = param.ToString();
            switch (location)
            {
                case "Active Player":
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.SpellLinks.Remove(this);
                    break;
                case "Active Creature":
                    Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.SpellLinks.Remove(this);
                    break;
                default:
                    HelperMethods.WriteToLogFile("Unhandled parameter in DoRemoveSpellLink: " + location, true);
                    break;
            }
        }
        #endregion

    }
}
