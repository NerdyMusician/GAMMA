using GAMMA.Toolbox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace GAMMA.Models.WebAutomation
{
    [Serializable]
    public class WebActionModel : BaseModel
    {
        // Constructors
        public WebActionModel()
        {
            InteractionTypes = new(){ "Click", "Text Input", "Go to URL", "Wait for X Seconds", "Press Enter Key" };
            TextInputSuggestions = new() { "{Login Name}", "{Login Pass}", "{Game Name}", "{Character Name}", "{App Version}", "{Message}", "{Active Character Name}" };
            TargetElementStack = new();
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
                ShowTargetStack = TargetFields.Contains(value);
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
        #region TargetElementStack
        private ObservableCollection<WebElementModel> _TargetElementStack;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<WebElementModel> TargetElementStack
        {
            get => _TargetElementStack;
            set => SetAndNotify(ref _TargetElementStack, value);
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
        #region ShowTargetStack
        private bool _ShowTargetStack;
        public bool ShowTargetStack
        {
            get => _ShowTargetStack;
            set => SetAndNotify (ref _ShowTargetStack, value);
        }
        #endregion

        // Private Properties
        private List<string> InputFields = new() { "Text Input", "Go to URL", "Wait for X Seconds" };
        private List<string> TargetFields = new() { "Click", "Text Input", "Press Enter Key" };

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
        #region AddElement
        public ICommand AddElement => new RelayCommand(DoAddElement);
        private void DoAddElement(object param)
        {
            TargetElementStack.Add(new());
        }
        #endregion

        // Public Methods
        public bool PerformWebAction(ref IWebDriver webDriver, string message = "")
        {
            
            string textInput = ReplaceTextPlaceholders(TextInputValue, message);
            if (InteractionType == "Go to URL")
            {
                webDriver.Navigate().GoToUrl(textInput);
            }
            if (InteractionType == "Click")
            {
                IWebElement element = GetWebElement(ref webDriver);
                if (element != null)
                {
                    element.Click();
                }
                else { return false; }
            }
            if (InteractionType == "Text Input")
            {
                IWebElement element = GetWebElement(ref webDriver);
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
                IWebElement element = GetWebElement(ref webDriver);
                if (element != null)
                {
                    element.SendKeys("\n");
                }
                else { return false; }
            }
            return true;
        }

        // Private Methods
        private IWebElement GetWebElement(ref IWebDriver webDriver)
        {
            IReadOnlyCollection<IWebElement> webElements = null;
            IWebElement targetElement = null;

            int i = 0;
            foreach (WebElementModel element in TargetElementStack)
            {
                string handleMatch = ReplaceTextPlaceholders(element.TargetElementMatchText);
                if (i > 0 && webElements == null) { continue; }
                if (webElements == null)
                {
                    if (element.TargetElementHandle == "ID")
                    {
                        webElements = webDriver.FindElements(By.Id(handleMatch));
                    }
                    if (element.TargetElementHandle == "Class")
                    {
                        webElements = webDriver.FindElements(By.ClassName(handleMatch));
                    }
                    if (element.TargetElementHandle == "Link Text")
                    {
                        webElements = webDriver.FindElements(By.LinkText(handleMatch));
                    }
                    if (element.TargetElementHandle == "CSS Selector")
                    {
                        webElements = webDriver.FindElements(By.CssSelector(handleMatch));
                    }
                    if (element.TargetElementHandle == "XPath")
                    {
                        webElements = webDriver.FindElements(By.XPath(handleMatch));
                    }
                    if (webElements.Count < element.ElementMatchIteration + 1)
                    {
                        return null;
                    }
                    
                    int x = 0;
                    foreach (IWebElement webElement in webElements)
                    {
                        if (x == element.ElementMatchIteration)
                        {
                            targetElement = webElement;
                        }
                        x++;
                    }
                }
                else
                {
                    if (element.TargetElementHandle == "ID")
                    {
                        webElements = targetElement.FindElements(By.Id(handleMatch));
                    }
                    if (element.TargetElementHandle == "Class")
                    {
                        webElements = targetElement.FindElements(By.ClassName(handleMatch));
                    }
                    if (element.TargetElementHandle == "Link Text")
                    {
                        webElements = targetElement.FindElements(By.LinkText(handleMatch));
                    }
                    if (element.TargetElementHandle == "CSS Selector")
                    {
                        webElements = targetElement.FindElements(By.CssSelector(handleMatch));
                    }
                    if (webElements.Count < element.ElementMatchIteration + 1)
                    {
                        return null;
                    }
                    int x = 0;
                    foreach (IWebElement webElement in webElements)
                    {
                        if (x == element.ElementMatchIteration)
                        {
                            targetElement = webElement;
                        }
                        x++;
                    }
                }
            }

            if (targetElement != null) { return targetElement; }
            return null;


        }
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
