using GAMMA.Toolbox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class WebActionModel : BaseModel
    {
        // Constructors
        public WebActionModel()
        {
            ElementMatchIteration = 0;
            InteractionTypes = new(){ "Click", "Text Input", "Go to URL", "Wait for X Seconds", "Press Enter Key" };
            TargetElementHandles = new(){ "ID", "Class", "Link Text" };
            TextInputSuggestions = new() { "{Login Name}", "{Login Pass}", "{Game Name}", "{Character Name}", "{App Version}" };
        }

        // Databound Properties
        #region InteractionType
        private string _InteractionType;
        [XmlSaveMode(XSME.Single)]
        public string InteractionType
        {
            get => _InteractionType;
            set => SetAndNotify(ref _InteractionType, value);
        }
        #endregion
        #region InteractionTypes
        private List<string> _InteractionTypes;
        public List<string> InteractionTypes
        {
            get => _InteractionTypes;
            set => SetAndNotify(ref _InteractionTypes, value);
        }
        #endregion
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
        #region TextInputValue
        private string _TextInputValue;
        [XmlSaveMode(XSME.Single)]
        public string TextInputValue
        {
            get => _TextInputValue;
            set => SetAndNotify(ref _TextInputValue, value);
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
        #region RemoveWebAction
        public ICommand RemoveWebAction => new RelayCommand(DoRemoveWebAction);
        private void DoRemoveWebAction(object param)
        {
            if (param == null) { return; }
            if (param.ToString() == "Startup")
            {
                Configuration.MainModelRef.SettingsView.StartupWebActions.Remove(this);
            }
            if (param.ToString() == "Output")
            {
                Configuration.MainModelRef.SettingsView.OutputWebActions.Remove(this);
            }
        }
        #endregion
        #region AddActionAbove
        public ICommand AddActionAbove => new RelayCommand(DoAddActionAbove);
        private void DoAddActionAbove(object param)
        {
            if (param == null) { return; }
            
            if (param.ToString() == "Startup")
            {
                int index = Configuration.MainModelRef.SettingsView.StartupWebActions.IndexOf(this);
                Configuration.MainModelRef.SettingsView.StartupWebActions.Insert(index, new());
            }
            if (param.ToString() == "Output")
            {
                int index = Configuration.MainModelRef.SettingsView.OutputWebActions.IndexOf(this);
                Configuration.MainModelRef.SettingsView.OutputWebActions.Insert(index, new());
            }
        }
        #endregion
        #region AddActionBelow
        public ICommand AddActionBelow => new RelayCommand(DoAddActionBelow);
        private void DoAddActionBelow(object param)
        {
            if (param == null) { return; }

            if (param.ToString() == "Startup")
            {
                int index = Configuration.MainModelRef.SettingsView.StartupWebActions.IndexOf(this);
                if (Configuration.MainModelRef.SettingsView.StartupWebActions.Last() == this)
                {
                    Configuration.MainModelRef.SettingsView.StartupWebActions.Add(new());
                }
                Configuration.MainModelRef.SettingsView.StartupWebActions.Insert(index + 1, new());
            }
            if (param.ToString() == "Output")
            {
                int index = Configuration.MainModelRef.SettingsView.OutputWebActions.IndexOf(this);
                if (Configuration.MainModelRef.SettingsView.OutputWebActions.Last() == this)
                {
                    Configuration.MainModelRef.SettingsView.OutputWebActions.Add(new());
                }
                Configuration.MainModelRef.SettingsView.OutputWebActions.Insert(index + 1, new());
            }
        }
        #endregion

        // Public Methods
        public bool PerformWebAction(ref IWebDriver webDriver)
        {
            string handleMatch = ReplaceTextPlaceholders(TargetElementMatchText);
            string textInput = ReplaceTextPlaceholders(TextInputValue);
            if (InteractionType == "Go to URL")
            {
                webDriver.Navigate().GoToUrl(textInput);
            }
            if (InteractionType == "Click")
            {
                IWebElement element = GetWebElement(ref webDriver, handleMatch);
                if (element != null)
                {
                    element.Click();
                }
                else { return false; }
            }
            if (InteractionType == "Text Input")
            {
                IWebElement element = GetWebElement(ref webDriver, handleMatch);
                if (element != null)
                {
                    element.SendKeys(textInput);
                }
                else { return false; }
            }
            if (InteractionType == "Wait for X Seconds")
            {
                if (int.TryParse(textInput, out int value))
                {
                    Thread.Sleep(value * 1000);
                }
            }
            if (InteractionType == "Press Enter Key")
            {
                IWebElement element = GetWebElement(ref webDriver, handleMatch);
                if (element != null)
                {
                    element.SendKeys("\n");
                }
                else { return false; }
            }
            return true;
        }

        // Private Methods
        private IWebElement GetWebElement(ref IWebDriver webDriver, string handleMatch)
        {
            IReadOnlyCollection<IWebElement> webElements = null;
            if (TargetElementHandle == "ID")
            {
                webElements = webDriver.FindElements(By.Id(handleMatch));
            }
            if (TargetElementHandle == "Class")
            {
                webElements = webDriver.FindElements(By.ClassName(handleMatch));
            }
            if (TargetElementHandle == "Link Text")
            {
                webElements = webDriver.FindElements(By.LinkText(handleMatch));
            }

            if (webElements == null) { return null; }
            if (webElements.Count < ElementMatchIteration + 1)
            {
                return null;
            }
            int i = 0;
            foreach (IWebElement webElement in webElements)
            {
                if (i == ElementMatchIteration)
                {
                    return webElement;
                }
                i++;
            }
            return null;


        }
        private string ReplaceTextPlaceholders(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            GameCharacterSelection game = Configuration.MainModelRef.SettingsView.Roll20GameCharacterList.FirstOrDefault(g => g.IsSelected);
            text = text.Replace("{Login Name}", Configuration.MainModelRef.SettingsView.Roll20Email);
            text = text.Replace("{Login Pass}", Configuration.MainModelRef.SettingsView.Roll20Password);
            text = text.Replace("{Game Name}", (game != null) ? game.Game : "");
            text = text.Replace("{Character Name}", (game != null) ? game.Character : "");
            return text;
        }

    }
}
