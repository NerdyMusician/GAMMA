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
        #region TimeInfo
        private string _TimeInfo;
        [XmlSaveMode(XSME.Single)]
        public string TimeInfo
        {
            get
            {
                return _TimeInfo;
            }
            set
            {
                _TimeInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region UpdateTimestamp
        private RelayCommand _UpdateTimestamp;
        public ICommand UpdateTimestamp
        {
            get
            {
                if (_UpdateTimestamp == null)
                {
                    _UpdateTimestamp = new RelayCommand(DoUpdateTimestamp);
                }
                return _UpdateTimestamp;
            }
        }
        private void DoUpdateTimestamp(object param)
        {
            GameCampaign campaign = Configuration.MainModelRef.CampaignView.ActiveCampaign;
            TimeInfo = "Day " + campaign.AdventureDayCount + ", " + campaign.TimeDigits + " " + campaign.TimeIndicator;
        }
        #endregion
        #region RemoveTimestamp
        private RelayCommand _RemoveTimestamp;
        public ICommand RemoveTimestamp
        {
            get
            {
                if (_RemoveTimestamp == null)
                {
                    _RemoveTimestamp = new RelayCommand(DoRemoveTimestamp);
                }
                return _RemoveTimestamp;
            }
        }
        private void DoRemoveTimestamp(object param)
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.Timestamps.Remove(this);
        }
        #endregion

        // Public Methods

        // Private Methods

    }
}
