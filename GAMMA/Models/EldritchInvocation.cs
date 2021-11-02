using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class EldritchInvocation : BaseModel
    {
        // Constructors
        public EldritchInvocation()
        {
            Name = "New Eldritch Invocation";
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode("Single")]
        public string Sourcebook
        {
            get
            {
                return _Sourcebook;
            }
            set
            {
                _Sourcebook = value;
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

        // Commands
        #region RemoveEldritchInvocation
        private RelayCommand _RemoveEldritchInvocation;
        public ICommand RemoveEldritchInvocation
        {
            get
            {
                if (_RemoveEldritchInvocation == null)
                {
                    _RemoveEldritchInvocation = new RelayCommand(DoRemoveEldritchInvocation);
                }
                return _RemoveEldritchInvocation;
            }
        }
        private void DoRemoveEldritchInvocation(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("No parameter passed for DoRemoveEldritchInvocation.", true); return; }
            string location = param.ToString();

            switch (location)
            {
                case "Source List":
                    Configuration.MainModelRef.ToolsView.EldritchInvocations.Remove(this);
                    return;
                default:
                    HelperMethods.WriteToLogFile("Unhandled parameter \"" + location + "\" in DoRemoveEldritchInvocation.", true);
                    return;
            }

        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            EldritchInvocation duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.EldritchInvocations.Insert(Configuration.MainModelRef.ToolsView.EldritchInvocations.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActiveEldritchInvocation = duplicate;
        }
        #endregion

    }
}
