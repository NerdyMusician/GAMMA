using GAMMA.Toolbox;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class PackCreatureModel : BaseModel
    {
        // Constructors
        public PackCreatureModel()
        {

        }

        // Databound Properties
        #region CreatureName
        private string _CreatureName;
        [XmlSaveMode(XSME.Single)]
        public string CreatureName
        {
            get
            {
                return _CreatureName;
            }
            set
            {
                _CreatureName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Quantity
        private int _Quantity;
        [XmlSaveMode(XSME.Single)]
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveCreature
        private RelayCommand _RemoveCreature;
        public ICommand RemoveCreature
        {
            get
            {
                if (_RemoveCreature == null)
                {
                    _RemoveCreature = new RelayCommand(param => DoRemoveCreature());
                }
                return _RemoveCreature;
            }
        }
        private void DoRemoveCreature()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActivePack.CreatureList.Remove(this);
        }
        #endregion
        #region RemoveNpc
        private RelayCommand _RemoveNpc;
        public ICommand RemoveNpc
        {
            get
            {
                if (_RemoveNpc == null)
                {
                    _RemoveNpc = new RelayCommand(param => DoRemoveNpc());
                }
                return _RemoveNpc;
            }
        }
        private void DoRemoveNpc()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActivePack.NpcList.Remove(this);
        }
        #endregion

    }
}
