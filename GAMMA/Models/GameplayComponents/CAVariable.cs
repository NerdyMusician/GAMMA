using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GAMMA.Models.GameplayComponents
{
    [Serializable]
    public class CAVariable : BaseModel
    {
        // Constructors
        public CAVariable()
        {
            Name = "New Variable";
            Type = "Number";
            TypeOptions = new() { "Text", "Number", "Toggled Option" };
            Rolls = new();
            Modifiers = new();
            DoOutput = true;
        }
        public CAVariable(string name, string type, bool output = true)
        {
            Name = name;
            Type = type;
            TypeOptions = new() { "Text", "Number", "Toggled Option" };
            Rolls = new();
            Modifiers = new();
            DoOutput = output;
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
        #region Value
        private string _Value;
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IncludeHalfValue
        private bool _IncludeHalfValue;
        [XmlSaveMode(XSME.Single)]
        public bool IncludeHalfValue
        {
            get
            {
                return _IncludeHalfValue;
            }
            set
            {
                _IncludeHalfValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DoOutput
        private bool _DoOutput;
        [XmlSaveMode(XSME.Single)]
        public bool DoOutput
        {
            get
            {
                return _DoOutput;
            }
            set
            {
                _DoOutput = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InEditMode
        private bool _InEditMode;
        
        public bool InEditMode
        {
            get
            {
                return _InEditMode;
            }
            set
            {
                _InEditMode = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region TypeOptions
        private List<string> _TypeOptions;
        
        public List<string> TypeOptions
        {
            get
            {
                return _TypeOptions;
            }
            set
            {
                _TypeOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Rolls
        private List<string> _Rolls;
        public List<string> Rolls
        {
            get
            {
                return _Rolls;
            }
            set
            {
                _Rolls = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Modifiers
        private List<string> _Modifiers;
        public List<string> Modifiers
        {
            get
            {
                return _Modifiers;
            }
            set
            {
                _Modifiers = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveVariable
        public ICommand RemoveVariable => new RelayCommand(DoRemoveVariable);
        private void DoRemoveVariable(object param)
        {
            if (param.GetType() == typeof(CustomAbility))
            {
                (param as CustomAbility).Variables.Remove(this);
            }
        }
        #endregion

    }
}
