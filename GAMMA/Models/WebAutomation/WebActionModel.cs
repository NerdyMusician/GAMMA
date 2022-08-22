using GAMMA.Toolbox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace GAMMA.Models.WebAutomation
{
    [Serializable]
    public class WebActionModel : BaseModel
    {
        // Web Actions
        const string ActionClick = "Click";
        const string ActionTextInput = "Text Input";
        const string ActionGoToUrl = "Go to URL";
        const string ActionWait = "Wait for X Seconds";
        const string ActionPressEnter = "Press Enter Key";

        // Text Input Suggestions
        const string InputLoginName = "{Login Name}";
        const string InputLoginPass = "{Login Pass}";
        const string InputGameName = "{Game Name}";
        const string InputCharacterName = "{Character Name}";
        const string InputAppVersion = "{App Version}";
        const string InputMessage = "{Message}";
        const string InputActiveCharacterName = "{Active Character Name}";

        // Constructors
        public WebActionModel()
        {
            InteractionTypes = new() { ActionClick, ActionTextInput, ActionGoToUrl, ActionWait, ActionPressEnter };
            TextInputSuggestions = new() { InputLoginName, InputLoginPass, InputGameName, InputCharacterName, InputAppVersion, InputMessage, InputActiveCharacterName };
        }

        // Databound Properties
        #region InteractionType
        private string _InteractionType;
        [XmlSaveMode(XSME.Single)]
        public string InteractionType
        {
            get => _InteractionType;
            set
            {
                _InteractionType = value;
                NotifyPropertyChanged();
                ShowInputField = InputFields.Contains(value);
                ShowTargetField = TargetFields.Contains(value);
            }
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
        #region ShowInputField
        private bool _ShowInputField;
        public bool ShowInputField
        {
            get => _ShowInputField;
            set => SetAndNotify(ref _ShowInputField, value);
        }
        #endregion
        private bool _ShowTargetField;
        public bool ShowTargetField
        {
            get => _ShowTargetField;
            set => SetAndNotify(ref _ShowTargetField, value);
        }
        private string _TargetElement;
        [XmlSaveMode(XSME.Single)]
        public string TargetElement
        {
            get => _TargetElement;
            set => SetAndNotify(ref _TargetElement, value);
        }

        // Private Properties
        private List<string> InputFields = new() { ActionTextInput, ActionGoToUrl, ActionWait };
        private List<string> TargetFields = new() { ActionClick, ActionTextInput, ActionPressEnter };

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
            if (param.ToString() == "Switchback")
            {
                Configuration.MainModelRef.SettingsView.SwitchbackWebActions.Remove(this);
            }
        }
        #endregion

        // Public Methods
        public bool PerformWebAction(ref IWebDriver webDriver, string message = "")
        {
            
            string textInput = ReplaceTextPlaceholders(TextInputValue, message);
            string targetElement = ReplaceTextPlaceholders(TargetElement, message);
            if (InteractionType == ActionGoToUrl)
            {
                webDriver.Navigate().GoToUrl(textInput);
            }
            if (InteractionType == ActionClick)
            {
                IWebElement element = webDriver.FindElement(By.XPath(targetElement));
                if (element != null)
                {
                    element.Click();
                }
                else { return false; }
            }
            if (InteractionType == ActionTextInput)
            {
                IWebElement element = webDriver.FindElement(By.XPath(targetElement));
                if (element != null)
                {
                    element.SendKeys(textInput);
                }
                else { return false; }
            }
            if (InteractionType == ActionWait)
            {
                if (int.TryParse(textInput, out int value))
                {
                    Thread.Sleep(value * 1000);
                }
            }
            if (InteractionType == ActionPressEnter)
            {
                IWebElement element = webDriver.FindElement(By.XPath(targetElement));
                if (element != null)
                {
                    element.SendKeys("\n");
                }
                else { return false; }
            }
            return true;
        }

        // Private Methods
        private string ReplaceTextPlaceholders(string text, string message = "")
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            GameCharacterSelection game = Configuration.MainModelRef.SettingsView.Roll20GameCharacterList.FirstOrDefault(g => g.IsSelected);
            text = text.Replace("{Login Name}", Configuration.MainModelRef.SettingsView.Roll20Email);
            text = text.Replace("{Login Pass}", Configuration.MainModelRef.SettingsView.Roll20Password);
            text = text.Replace("{Game Name}", (game != null) ? game.Game : "");
            text = text.Replace("{Character Name}", (game != null) ? game.Character : "");
            text = text.Replace("{App Version}", Configuration.MainModelRef.ApplicationVersion);
            text = text.Replace("{Message}", message);
            if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter != null)
            {
                text = text.Replace("{Active Character Name}", Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Name);
            }
            return text;
        }

    }
}
