using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GAMMA.Models.WebAutomation
{
    [Serializable]
    public class WebElementModel : BaseModel
    {
        // Constructors
        public WebElementModel()
        {
            ElementMatchIteration = 0;
            TargetElementHandles = new() { "ID", "Class", "Link Text", "CSS Selector" };
            TextInputSuggestions = new() { "{Login Name}", "{Login Pass}", "{Game Name}", "{Character Name}", "{App Version}", "{Message}", "{Active Character Name}" };

        }

        // Databound Properties
        #region TargetElementHandle
        private string _TargetElementHandle;
        [XmlSaveMode(XSME.Single)]
        public string TargetElementHandle
        {
            get => _TargetElementHandle;
            set => SetAndNotify(ref _TargetElementHandle, value);
        }
        #endregion
        #region TargetElementHandles
        private List<string> _TargetElementHandles;
        public List<string> TargetElementHandles
        {
            get => _TargetElementHandles;
            set => SetAndNotify(ref _TargetElementHandles, value);
        }
        #endregion
        #region TargetElementMatchText
        private string _TargetElementMatchText;
        [XmlSaveMode(XSME.Single)]
        public string TargetElementMatchText
        {
            get => _TargetElementMatchText;
            set => SetAndNotify(ref _TargetElementMatchText, value);
        }
        #endregion
        #region ElementMatchIteration
        private int _ElementMatchIteration;
        [XmlSaveMode(XSME.Single)]
        public int ElementMatchIteration
        {
            get => _ElementMatchIteration;
            set => SetAndNotify(ref _ElementMatchIteration, value);
        }
        #endregion
        #region TextInputSuggestions
        private List<string> _TextInputSuggestions;
        public List<string> TextInputSuggestions
        {
            get => _TextInputSuggestions;
            set => SetAndNotify(ref _TextInputSuggestions, value);
        }
        #endregion

        // Commands
        #region RemoveElement
        public ICommand RemoveElement => new RelayCommand(DoRemoveElement);
        private void DoRemoveElement(object param)
        {
            if (param.GetType() == typeof(WebActionModel))
            {
                (param as WebActionModel).TargetElementStack.Remove(this);
            }
        }
        #endregion

    }
}
