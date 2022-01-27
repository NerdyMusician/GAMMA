﻿using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GAMMA.Models.GameplayComponents
{
    [Serializable]
    public class CACondition : BaseModel
    {
        // Constructors
        public CACondition()
        {
            ConditionVariables = new() { "[Is Critical Hit]" };
            ConditionTypes = new() { "Equal To", "Greater Than", "Less Than", "Contains" };
        }

        // Databound Properties
        #region ConditionVariable
        private string _ConditionVariable;
        [XmlSaveMode(XSME.Single)]
        public string ConditionVariable
        {
            get => _ConditionVariable;
            set
            {
                _ConditionVariable = value;
                NotifyPropertyChanged();
                UpdateDisplayText();
            }
        }
        #endregion
        #region ConditionType
        private string _ConditionType;
        [XmlSaveMode(XSME.Single)]
        public string ConditionType
        {
            get => _ConditionType;
            set
            {
                _ConditionType = value;
                NotifyPropertyChanged();
                UpdateDisplayText();
            }
        }
        #endregion
        #region ConditionValue
        private string _ConditionValue;
        [XmlSaveMode(XSME.Single)]
        public string ConditionValue
        {
            get => _ConditionValue;
            set
            {
                _ConditionValue = value;
                NotifyPropertyChanged();
                UpdateDisplayText();
            }
        }
        #endregion

        #region ConditionVariables
        private List<string> _ConditionVariables;
        public List<string> ConditionVariables
        {
            get => _ConditionVariables;
            set => SetAndNotify(ref _ConditionVariables, value);
        }
        #endregion
        #region ConditionTypes
        private List<string> _ConditionTypes;
        public List<string> ConditionTypes
        {
            get => _ConditionTypes;
            set => SetAndNotify(ref _ConditionTypes, value);
        }
        #endregion

        #region InEditMode
        private bool _InEditMode;
        public bool InEditMode
        {
            get => _InEditMode;
            set => SetAndNotify(ref _InEditMode, value);
        }
        #endregion
        #region DisplayText
        private string _DisplayText;
        public string DisplayText
        {
            get => _DisplayText;
            set => SetAndNotify(ref _DisplayText, value);
        }
        #endregion

        // Commands
        #region RemoveCondition
        public ICommand RemoveCondition => new RelayCommand(DoRemoveCondition);
        private void DoRemoveCondition(object param)
        {
            if (param.GetType() == typeof(CAPreAction))
            {
                (param as CAPreAction).Conditions.Remove(this);
            }
            if (param.GetType() == typeof(CAPostAction))
            {
                (param as CAPostAction).Conditions.Remove(this);
            }
        }
        #endregion

        // Public Methods
        public void UpdateVariableList(List<string> variables)
        {
            ConditionVariables = new() { "[Is Critical Hit]" };
            ConditionVariables.AddRange(variables);
        }

        // Private Methods
        private void UpdateDisplayText()
        {
            string cType = ConditionType switch
            {
                "Equal To" => "==",
                "Greater Than" => ">",
                "Less Than" => "<",
                _ => "?"
            };
            DisplayText = string.Format("{0} {1} {2}", ConditionVariable, cType, ConditionValue);
        }

    }
}
