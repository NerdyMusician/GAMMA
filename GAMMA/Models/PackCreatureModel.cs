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
        #region IsHorde
        private bool _IsHorde;
        [XmlSaveMode(XSME.Single)]
        public bool IsHorde
        {
            get => _IsHorde;
            set => SetAndNotify(ref _IsHorde, value);
        }
        #endregion
        #region MaxHordeSize
        private int _MaxHordeSize;
        [XmlSaveMode(XSME.Single)]
        public int MaxHordeSize
        {
            get => _MaxHordeSize;
            set => SetAndNotify(ref _MaxHordeSize, value);
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
