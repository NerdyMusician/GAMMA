using GAMMA.Toolbox;
using System;

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
        #region PreserveFromDataWipe
        private bool _PreserveFromDataWipe;
        [XmlSaveMode("Single")]
        public bool PreserveFromDataWipe
        {
            get
            {
                return _PreserveFromDataWipe;
            }
            set
            {
                _PreserveFromDataWipe = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode("Single")]
        public bool IsValidated
        {
            get
            {
                return _IsValidated;
            }
            set
            {
                _IsValidated = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }
}
