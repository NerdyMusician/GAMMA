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
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region PreserveFromDataWipe
        private bool _PreserveFromDataWipe;
        [XmlSaveMode(XSME.Single)]
        public bool PreserveFromDataWipe
        {
            get => _PreserveFromDataWipe;
            set => SetAndNotify(ref _PreserveFromDataWipe, value);
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get => _IsValidated;
            set => SetAndNotify(ref _IsValidated, value);
        }
        #endregion

    }
}
