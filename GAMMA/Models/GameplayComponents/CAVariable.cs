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
            TypeOptions = new(AppData.VarTypes);
            Rolls = new();
            Modifiers = new();
            DoOutput = true;
        }
        public CAVariable(string name, string type, bool output = true)
        {
            InitializeCollections();
            Name = name;
            Type = type;
            DoOutput = output;
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
        #region Type
        private string _Type;
        [XmlSaveMode(XSME.Single)]
        public string Type
        {
            get => _Type;
            set => SetAndNotify(ref _Type, value);
        }
        #endregion
        #region Value
        private string _Value;
        public string Value
        {
            get => _Value;
            set => SetAndNotify(ref _Value, value);
        }
        #endregion
        #region IncludeHalfValue
        private bool _IncludeHalfValue;
        [XmlSaveMode(XSME.Single)]
        public bool IncludeHalfValue
        {
            get => _IncludeHalfValue;
            set => SetAndNotify(ref _IncludeHalfValue, value);
        }
        #endregion
        #region DoOutput
        private bool _DoOutput;
        [XmlSaveMode(XSME.Single)]
        public bool DoOutput
        {
            get => _DoOutput;
            set => SetAndNotify(ref _DoOutput, value);
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

        #region TypeOptions
        private List<string> _TypeOptions;
        public List<string> TypeOptions
        {
            get => _TypeOptions;
            set => SetAndNotify(ref _TypeOptions, value);
        }
        #endregion
        #region Rolls
        private List<string> _Rolls;
        public List<string> Rolls
        {
            get => _Rolls;
            set => SetAndNotify(ref _Rolls, value);
        }
        #endregion
        #region Modifiers
        private List<string> _Modifiers;
        public List<string> Modifiers
        {
            get => _Modifiers;
            set => SetAndNotify(ref _Modifiers, value);
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

        // Public Methods
        public void ResetTypeOptions()
        {
            TypeOptions = new(AppData.VarTypes);
        }

        // Private Methods
        private void InitializeCollections()
        {
            TypeOptions = new(AppData.VarTypes);
            Rolls = new();
            Modifiers = new();
        }

    }
}
