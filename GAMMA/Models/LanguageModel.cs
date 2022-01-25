using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class LanguageModel : BaseModel
    {
        // Constructors
        public LanguageModel()
        {
            Name = "New Language";
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
        #region Type
        private string _Type;
        [XmlSaveMode(XSME.Single)]
        public string Type
        {
            get => _Type;
            set => SetAndNotify(ref _Type, value);
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
        #region TypicalSpeakers
        private string _TypicalSpeakers;
        [XmlSaveMode(XSME.Single)]
        public string TypicalSpeakers
        {
            get => _TypicalSpeakers;
            set => SetAndNotify(ref _TypicalSpeakers, value);
        }
        #endregion
        #region Script
        private string _Script;
        [XmlSaveMode(XSME.Single)]
        public string Script
        {
            get => _Script;
            set => SetAndNotify(ref _Script, value);
        }
        #endregion
        #region Dialects
        private string _Dialects;
        [XmlSaveMode(XSME.Single)]
        public string Dialects
        {
            get => _Dialects;
            set => SetAndNotify(ref _Dialects, value);
        }
        #endregion

        // Commands
        #region RemoveLanguage
        public ICommand RemoveLanguage => new RelayCommand(DoRemoveLanguage);
        private void DoRemoveLanguage(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("LanguageModel.RemoveLanguage called without parameter.", true); return; }
            switch (param.ToString())
            {
                case "ToolsView.ActiveLanguage":
                    Configuration.MainModelRef.ToolsView.Languages.Remove(this);
                    break;
                default:
                    HelperMethods.WriteToLogFile("LanguageModel.RemoveLanguage called with invalid parameter string \"" + param.ToString() + "\".", true);
                    return;
            }

        }
        #endregion

    }
}
