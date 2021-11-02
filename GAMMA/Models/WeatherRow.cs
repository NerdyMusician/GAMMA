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
        [XmlSaveMode("Single")]
        public string Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
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
        #region LowValue
        private int _LowValue;
        [XmlSaveMode("Single")]
        public int LowValue
        {
            get
            {
                return _LowValue;
            }
            set
            {
                _LowValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HighValue
        private int _HighValue;
        [XmlSaveMode("Single")]
        public int HighValue
        {
            get
            {
                return _HighValue;
            }
            set
            {
                _HighValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands


        // Public Methods


        // Private Methods


    }
}
