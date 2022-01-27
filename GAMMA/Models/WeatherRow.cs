using GAMMA.Toolbox;
using System;

namespace GAMMA.Models
{
    [Serializable]
    public class WeatherRow : BaseModel
    {
        // Constructors
        public WeatherRow()
        {

        }

        // Databound Properties
        #region Icon
        private string _Icon;
        [XmlSaveMode(XSME.Single)]
        public string Icon
        {
            get => _Icon;
            set => SetAndNotify(ref _Icon, value);
        }
        #endregion
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
        #region LowValue
        private int _LowValue;
        [XmlSaveMode(XSME.Single)]
        public int LowValue
        {
            get => _LowValue;
            set => SetAndNotify(ref _LowValue, value);
        }
        #endregion
        #region HighValue
        private int _HighValue;
        [XmlSaveMode(XSME.Single)]
        public int HighValue
        {
            get => _HighValue;
            set => SetAndNotify(ref _HighValue, value);
        }
        #endregion

    }
}
