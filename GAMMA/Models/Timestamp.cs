using GAMMA.Toolbox;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class Timestamp : BaseModel
    {
        // Constructors
        public Timestamp()
        {
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
        #region TimeInfo
        private string _TimeInfo;
        [XmlSaveMode(XSME.Single)]
        public string TimeInfo
        {
            get => _TimeInfo;
            set => SetAndNotify(ref _TimeInfo, value);
        }
        #endregion

        // Commands
        #region UpdateTimestamp
        public ICommand UpdateTimestamp => new RelayCommand(DoUpdateTimestamp);
        private void DoUpdateTimestamp(object param)
        {
            GameCampaign campaign = Configuration.MainModelRef.CampaignView.ActiveCampaign;
            TimeInfo = "Day " + campaign.AdventureDayCount + ", " + campaign.TimeDigits + " " + campaign.TimeIndicator;
        }
        #endregion
        #region RemoveTimestamp
        public ICommand RemoveTimestamp => new RelayCommand(DoRemoveTimestamp);
        private void DoRemoveTimestamp(object param)
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.Timestamps.Remove(this);
        }
        #endregion

    }
}
