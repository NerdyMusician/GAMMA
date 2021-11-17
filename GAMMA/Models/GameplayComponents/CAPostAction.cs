using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models.GameplayComponents
{
    [Serializable]
    public class CAPostAction : BaseModel
    {
        // Constructors
        public CAPostAction()
        {
            ActionOptions = new()
            {
                "Activate Concentration",
                "Activate Alterant",
                "Add Minions",
                "Add to Current HP",
                "Add to Temporary HP", 
                "Add Active Effect",
                "Expend Counter",
                "Subtract from Current HP",
            };
            Conditions = new();
            ValueSetA = new();
            ValueSetB = new();
        }

        // Databound Properties
        #region Action
        private string _Action;
        [XmlSaveMode("Single")]
        public string Action
        {
            get
            {
                return _Action;
            }
            set
            {
                _Action = value;
                NotifyPropertyChanged();
                SetForms();
            }
        }
        #endregion
        #region Conditions
        private ObservableCollection<CACondition> _Conditions;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<CACondition> Conditions
        {
            get
            {
                return _Conditions;
            }
            set
            {
                _Conditions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ValueSetA
        private List<string> _ValueSetA;
        public List<string> ValueSetA
        {
            get
            {
                return _ValueSetA;
            }
            set
            {
                _ValueSetA = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ValueSetB
        private List<string> _ValueSetB;
        public List<string> ValueSetB
        {
            get
            {
                return _ValueSetB;
            }
            set
            {
                _ValueSetB = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ValueA
        private string _ValueA;
        [XmlSaveMode("Single")]
        public string ValueA
        {
            get
            {
                return _ValueA;
            }
            set
            {
                _ValueA = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ValueB
        private string _ValueB;
        [XmlSaveMode("Single")]
        public string ValueB
        {
            get
            {
                return _ValueB;
            }
            set
            {
                _ValueB = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LabelA
        private string _LabelA;
        public string LabelA
        {
            get
            {
                return _LabelA;
            }
            set
            {
                _LabelA = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LabelB
        private string _LabelB;
        public string LabelB
        {
            get
            {
                return _LabelB;
            }
            set
            {
                _LabelB = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ActionOptions
        private List<string> _ActionOptions;
        [XmlSaveMode("None")]
        public List<string> ActionOptions
        {
            get
            {
                return _ActionOptions;
            }
            set
            {
                _ActionOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ShowValueA
        private bool _ShowValueA;
        public bool ShowValueA
        {
            get
            {
                return _ShowValueA;
            }
            set
            {
                _ShowValueA = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowValueB
        private bool _ShowValueB;
        public bool ShowValueB
        {
            get
            {
                return _ShowValueB;
            }
            set
            {
                _ShowValueB = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region InEditMode
        private bool _InEditMode;
        [XmlSaveMode("None")]
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

        // Commands
        #region RemoveAction
        public ICommand RemoveAction => new RelayCommand(DoRemoveAction);
        private void DoRemoveAction(object param)
        {
            if (param.GetType() == typeof(CustomAbility))
            {
                (param as CustomAbility).PostActions.Remove(this);
            }
        }
        #endregion
        #region AddCondition
        public ICommand AddCondition => new RelayCommand(DoAddCondition);
        private void DoAddCondition(object param)
        {
            Conditions.Add(new());
        }
        #endregion

        // Private Methods
        private void SetForms()
        {
            ShowValueA = false;
            ShowValueB = false;

            if (Action == "Add to Current HP") { ShowValueA = true; LabelA = "Value"; }
            if (Action == "Subtract from Current HP") { ShowValueA = true; LabelA = "Value"; }
            if (Action == "Add to Temporary HP") { ShowValueA = true; LabelA = "Value"; }
            if (Action == "Add Minions") { ShowValueA = true; LabelA = "Creature Name"; ShowValueB = true; LabelB = "Quantity"; }
            if (Action == "Add Active Effect") { ShowValueA = true; LabelA = "Ability"; }
            if (Action == "Expend Counter")
            {
                ShowValueA = true; LabelA = "Counter"; 
                ShowValueB = true; LabelB = "Amount";
                ValueSetA = new();
                if (Configuration.MainModelRef.TabSelected_Players)
                {
                    foreach (CounterModel counter in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Counters)
                    {
                        ValueSetA.Add(counter.Name);
                    }
                }
                if (Configuration.MainModelRef.TabSelected_CreatureBuilder)
                {
                    foreach (CounterModel counter in Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Counters)
                    {
                        ValueSetA.Add(counter.Name);
                    }
                }
            }
            if (Action == "Activate Concentration") { }
            if (Action == "Activate Alterant") { ShowValueA = true; LabelA = "Alterant"; }

        }

    }
}
