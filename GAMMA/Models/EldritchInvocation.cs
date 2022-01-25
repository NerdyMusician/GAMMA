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
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
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
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode(XSME.Single)]
        public string Sourcebook
        {
            get => _Sourcebook;
            set => SetAndNotify(ref _Sourcebook, value);
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

        // Commands
        #region RemoveEldritchInvocation
        public ICommand RemoveEldritchInvocation => new RelayCommand(DoRemoveEldritchInvocation);
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
