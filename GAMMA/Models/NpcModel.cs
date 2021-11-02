using GAMMA.Toolbox;
using GAMMA.Windows;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class NpcModel : BaseModel
    {
        // Constructors
        public NpcModel()
        {
            Name = "New Character";
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
        #region BaseCreatureName
        private string _BaseCreatureName;
        [XmlSaveMode("Single")]
        public string BaseCreatureName
        {
            get
            {
                return _BaseCreatureName;
            }
            set
            {
                _BaseCreatureName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsFriendly
        private bool _IsFriendly;
        [XmlSaveMode("Single")]
        public bool IsFriendly
        {
            get
            {
                return _IsFriendly;
            }
            set
            {
                _IsFriendly = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region SelectBaseCreature
        private RelayCommand _SelectBaseCreature;
        public ICommand SelectBaseCreature
        {
            get
            {
                if (_SelectBaseCreature == null)
                {
                    _SelectBaseCreature = new RelayCommand(param => DoSelectBaseCreature());
                }
                return _SelectBaseCreature;
            }
        }
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
        private RelayCommand _RemoveNpcFromCampaign;
        public ICommand RemoveNpcFromCampaign
        {
            get
            {
                if (_RemoveNpcFromCampaign == null)
                {
                    _RemoveNpcFromCampaign = new RelayCommand(param => DoRemoveNpcFromCampaign());
                }
                return _RemoveNpcFromCampaign;
            }
        }
        private void DoRemoveNpcFromCampaign()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.Npcs.Remove(this);
        }
        #endregion

    }
}
