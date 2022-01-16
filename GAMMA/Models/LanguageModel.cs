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
        [XmlSaveMode(XSME.Single)]
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
        #region Type
        private string _Type;
        [XmlSaveMode(XSME.Single)]
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Sourcebook
        private string _Sourcebook;
        [XmlSaveMode(XSME.Single)]
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
        #region TypicalSpeakers
        private string _TypicalSpeakers;
        [XmlSaveMode(XSME.Single)]
        public string TypicalSpeakers
        {
            get
            {
                return _TypicalSpeakers;
            }
            set
            {
                _TypicalSpeakers = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Script
        private string _Script;
        [XmlSaveMode(XSME.Single)]
        public string Script
        {
            get
            {
                return _Script;
            }
            set
            {
                _Script = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Dialects
        private string _Dialects;
        [XmlSaveMode(XSME.Single)]
        public string Dialects
        {
            get
            {
                return _Dialects;
            }
            set
            {
                _Dialects = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveLanguage
        private RelayCommand _RemoveLanguage;
        public ICommand RemoveLanguage
        {
            get
            {
                if (_RemoveLanguage == null)
                {
                    _RemoveLanguage = new RelayCommand(DoRemoveLanguage);
                }
                return _RemoveLanguage;
            }
        }
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
