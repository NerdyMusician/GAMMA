using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class Sourcebook : BaseModel
    {
        // Constructors
        public Sourcebook()
        {
            Name = "New Sourcebook";
            PreserveFromDataWipe = true;
            IsValidated = false;
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region PreserveFromDataWipe
        private bool _PreserveFromDataWipe;
        [XmlSaveMode(XSME.Single)]
        public bool PreserveFromDataWipe
        {
            get => _PreserveFromDataWipe;
            set => SetAndNotify(ref _PreserveFromDataWipe, value);
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get => _IsValidated;
            set => SetAndNotify(ref _IsValidated, value);
        }
        #endregion

        // Commands
        public ICommand RemoveSourcebook => new RelayCommand(DoRemoveSourcebook);
        private void DoRemoveSourcebook(object param)
        {
            if (IsReferenced()) { return; }
            Configuration.MainModelRef.ToolsView.Sourcebooks.Remove(this);
        }

        // Private Methods
        private bool IsReferenced()
        {
            List<string> references = new();

            foreach (CreatureModel creature in Configuration.MainModelRef.CreatureBuilderView.AllCreatures)
            {
                if (creature.Sourcebook == this.Name) { references.Add($"Creature : {creature.Name}"); }
            }
            foreach (ItemModel item in Configuration.MainModelRef.ItemBuilderView.AllItems)
            {
                if (item.Sourcebook == this.Name) { references.Add($"Item : {item.Name}"); }
            }
            foreach (SpellModel spell in Configuration.MainModelRef.SpellBuilderView.AllSpells)
            {
                if (spell.Sourcebook == this.Name) { references.Add($"Spell : {spell.Name}"); }
            }

            if (references.Count > 0) 
            {
                HelperMethods.NotifyUser("Is a sourcebook for:\n" + HelperMethods.GetStringFromList(references, "\n"), HelperMethods.UserNotificationType.Report);
                return true; 
            }

            return false;

        }

    }
}
