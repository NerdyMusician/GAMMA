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
            get => _CreatureName;
            set => SetAndNotify(ref _CreatureName, value);
        }
        #endregion
        #region Quantity
        private int _Quantity;
        [XmlSaveMode(XSME.Single)]
        public int Quantity
        {
            get => _Quantity;
            set => SetAndNotify(ref _Quantity, value);
        }
        #endregion

        // Commands
        #region RemoveCreature
        public ICommand RemoveCreature => new RelayCommand(param => DoRemoveCreature());
        private void DoRemoveCreature()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActivePack.CreatureList.Remove(this);
        }
        #endregion
        #region RemoveNpc
        public ICommand RemoveNpc => new RelayCommand(param => DoRemoveNpc());
        private void DoRemoveNpc()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActivePack.NpcList.Remove(this);
        }
        #endregion

    }
}
