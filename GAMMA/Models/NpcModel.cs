using GAMMA.Toolbox;
using GAMMA.Windows;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public partial class NpcModel : BaseModel
    {
        // Constructors
        public NpcModel()
        {
            Name = "New Character";
            IsActive = true;
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
        #region Description
        private string _Description;
        [XmlSaveMode(XSME.Single)]
        public string Description
        {
            get => _Description;
            set => SetAndNotify(ref _Description, value);
        }
        #endregion
        #region BaseCreatureName
        private string _BaseCreatureName;
        [XmlSaveMode(XSME.Single)]
        public string BaseCreatureName
        {
            get => _BaseCreatureName;
            set => SetAndNotify(ref _BaseCreatureName, value);
        }
        #endregion
        #region IsFriendly
        private bool _IsFriendly;
        [XmlSaveMode(XSME.Single)]
        public bool IsFriendly
        {
            get => _IsFriendly;
            set => SetAndNotify(ref _IsFriendly, value);
        }
        #endregion
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode(XSME.Single)]
        public bool IsActive
        {
            get => _IsActive;
            set => SetAndNotify(ref _IsActive, value);
        }
        #endregion

        // Commands
        #region SelectBaseCreature
        public ICommand SelectBaseCreature => new RelayCommand(param => DoSelectBaseCreature());
        private void DoSelectBaseCreature()
        {
            ObjectSelectionDialog itemSelect;

            itemSelect = new ObjectSelectionDialog(Configuration.CreatureRepository.Where(creature => creature.IsPlayer == false && creature.IsValidated).ToList());
            if (itemSelect.ShowDialog() == true)
            {
                if (itemSelect.SelectedObject == null) { return; }

                BaseCreatureName = (itemSelect.SelectedObject as CreatureModel).Name;

            }

        }
        #endregion
        #region RemoveNpcFromCampaign
        public ICommand RemoveNpcFromCampaign => new RelayCommand(param => DoRemoveNpcFromCampaign());
        private void DoRemoveNpcFromCampaign()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.Npcs.Remove(this);
        }
        #endregion


    }
}
