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
            ActionOptions = new(AppData.PostActions);
            Conditions = new();
            ValueSetA = new();
            ValueSetB = new();
        }

        // Databound Properties
        #region Action
        private string _Action;
        [XmlSaveMode(XSME.Single)]
        public string Action
        {
            get => _Action;
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
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CACondition> Conditions
        {
            get => _Conditions;
            set => SetAndNotify(ref _Conditions, value);
        }
        #endregion
        #region ValueSetA
        private List<string> _ValueSetA;
        public List<string> ValueSetA
        {
            get => _ValueSetA;
            set => SetAndNotify(ref _ValueSetA, value);
        }
        #endregion
        #region ValueSetB
        private List<string> _ValueSetB;
        public List<string> ValueSetB
        {
            get => _ValueSetB;
            set => SetAndNotify(ref _ValueSetB, value);
        }
        #endregion
        #region ValueA
        private string _ValueA;
        [XmlSaveMode(XSME.Single)]
        public string ValueA
        {
            get => _ValueA;
            set => SetAndNotify(ref _ValueA, value);
        }
        #endregion
        #region ValueB
        private string _ValueB;
        [XmlSaveMode(XSME.Single)]
        public string ValueB
        {
            get => _ValueB;
            set => SetAndNotify(ref _ValueB, value);
        }
        #endregion
        #region LabelA
        private string _LabelA;
        public string LabelA
        {
            get => _LabelA;
            set => SetAndNotify(ref _LabelA, value);
        }
        #endregion
        #region LabelB
        private string _LabelB;
        public string LabelB
        {
            get => _LabelB;
            set => SetAndNotify(ref _LabelB, value);
        }
        #endregion

        #region ActionOptions
        private List<string> _ActionOptions;
        
        public List<string> ActionOptions
        {
            get => _ActionOptions;
            set => SetAndNotify(ref _ActionOptions, value);
        }
        #endregion

        #region ShowValueA
        private bool _ShowValueA;
        public bool ShowValueA
        {
            get => _ShowValueA;
            set => SetAndNotify(ref _ShowValueA, value);
        }
        #endregion
        #region ShowValueB
        private bool _ShowValueB;
        public bool ShowValueB
        {
            get => _ShowValueB;
            set => SetAndNotify(ref _ShowValueB, value);
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

            if (Action == AppData.PostAction_Add_to_Current_HP) { ShowValueA = true; LabelA = "Value"; }
            if (Action == AppData.PostAction_Subtract_from_Current_Hp) { ShowValueA = true; LabelA = "Value"; }
            if (Action == AppData.PostAction_Add_to_Temporary_HP) { ShowValueA = true; LabelA = "Value"; }
            if (Action == AppData.PostAction_Add_Minions) { ShowValueA = true; LabelA = "Creature Name"; ShowValueB = true; LabelB = "Quantity"; }
            if (Action == AppData.PostAction_Add_Active_Effect) 
            { 
                ShowValueA = true; 
                LabelA = "Ability"; 
                if (Configuration.MainModelRef.TabSelected_SpellBuilder)
                {
                    ValueSetA = new();
                    foreach (CustomAbility activeEffect in Configuration.MainModelRef.SpellBuilderView.ActiveSpell.SecondaryAbilities)
                    {
                        ValueSetA.Add(activeEffect.Name);
                    }
                }
            }
            if (Action == AppData.PostAction_Expend_Counter)
            {
                ShowValueA = true; LabelA = "Counter"; 
                ShowValueB = true; LabelB = "Amount";
                ValueSetA = new();
                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
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
            if (Action == AppData.PostAction_Activate_Concentration) { }
            if (Action == AppData.PostAction_Activate_Alterant) { ShowValueA = true; LabelA = "Alterant"; }
            if (Action == AppData.PostAction_Roll_Table) 
            { 
                ShowValueA = true; 
                LabelA = "Table";
                ValueSetA = new();
                foreach (RollTableModel rollTable in Configuration.MainModelRef.ToolsView.RollTables)
                {
                    if (rollTable.AvailableToPlayers) { ValueSetA.Add(rollTable.Name); }
                }
            }

        }

    }
}
