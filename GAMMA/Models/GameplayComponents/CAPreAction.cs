using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models.GameplayComponents
{
    [Serializable]
    public class CAPreAction : BaseModel
    {
        // Constructors
        public CAPreAction()
        {
            InitializeCollections();
        }
        public CAPreAction(string action, string target, string attackStat, bool useProf)
        {
            InitializeCollections();
            Action = action;
            Target = target;
            AttackAttribute = attackStat;
            UseProficiencyBonus = useProf;
        }
        public CAPreAction(string action, string target, int diceQty, int diceSides, bool doubleOnCrit)
        {
            InitializeCollections();
            Action = action;
            Target = target;
            DiceQuantity = diceQty;
            DiceQuality = diceSides;
            DoesDoubleOnCritical = doubleOnCrit;
        }
        public CAPreAction(string action, string target, int setValue)
        {
            InitializeCollections();
            Action = action;
            Target = target;
            SetValue = setValue.ToString();
        }
        public CAPreAction(string action, string target, string statForValue)
        {
            InitializeCollections();
            Action = action;
            Target = target;
            StatValue = statForValue;
        }
        public CAPreAction(string header, string action, string target, string setValue)
        {
            InitializeCollections();
            CustomDisplayText = header;
            Action = action;
            Target = target;
            SetValue = setValue;
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
                SetFormDisplays();
                UpdateDisplayText();
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
        #region Target
        private string _Target;
        [XmlSaveMode(XSME.Single)]
        public string Target
        {
            get => _Target;
            set
            {
                _Target = value;
                NotifyPropertyChanged();
                UpdateDisplayText();
            }
        }
        #endregion
        #region Targets
        private List<string> _Targets;
        public List<string> Targets
        {
            get => _Targets;
            set => SetAndNotify(ref _Targets, value);
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
        #region CustomDisplayText
        private string _CustomDisplayText;
        [XmlSaveMode(XSME.Single)]
        public string CustomDisplayText
        {
            get => _CustomDisplayText;
            set
            {
                _CustomDisplayText = value;
                NotifyPropertyChanged();
                UpdateDisplayText();
            }
        }
        #endregion

        // QA Prompt
        #region Question
        private string _Question;
        [XmlSaveMode(XSME.Single)]
        public string Question
        {
            get => _Question;
            set => SetAndNotify(ref _Question, value);
        }
        #endregion
        #region Answers
        private ObservableCollection<ConvertibleValue> _Answers;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Answers
        {
            get => _Answers;
            set => SetAndNotify(ref _Answers, value);
        }
        #endregion

        // Add Set Value
        #region SetValue
        private string _SetValue;
        [XmlSaveMode(XSME.Single)]
        public string SetValue
        {
            get => _SetValue;
            set => SetAndNotify(ref _SetValue, value);
        }
        #endregion

        // Add Roll
        #region DiceQuantity
        private int _DiceQuantity;
        [XmlSaveMode(XSME.Single)]
        public int DiceQuantity
        {
            get => _DiceQuantity;
            set => SetAndNotify(ref _DiceQuantity, value);
        }
        #endregion
        #region DiceQuality
        private int _DiceQuality;
        [XmlSaveMode(XSME.Single)]
        public int DiceQuality
        {
            get => _DiceQuality;
            set => SetAndNotify(ref _DiceQuality, value);
        }
        #endregion
        #region DoesDoubleOnCritical
        private bool _DoesDoubleOnCritical;
        [XmlSaveMode(XSME.Single)]
        public bool DoesDoubleOnCritical
        {
            get => _DoesDoubleOnCritical;
            set => SetAndNotify(ref _DoesDoubleOnCritical, value);
        }
        #endregion

        // Add Stat Value
        #region StatValue
        private string _StatValue;
        [XmlSaveMode(XSME.Single)]
        public string StatValue
        {
            get => _StatValue;
            set => SetAndNotify(ref _StatValue, value);
        }
        #endregion

        // Add Calculated Value
        #region CalculatedValueA
        private string _CalculatedValueA;
        [XmlSaveMode(XSME.Single)]
        public string CalculatedValueA
        {
            get => _CalculatedValueA;
            set => SetAndNotify(ref _CalculatedValueA, value);
        }
        #endregion
        #region CalculatedValueB
        private string _CalculatedValueB;
        [XmlSaveMode(XSME.Single)]
        public string CalculatedValueB
        {
            get => _CalculatedValueB;
            set => SetAndNotify(ref _CalculatedValueB, value);
        }
        #endregion
        #region Calculation
        private string _Calculation;
        [XmlSaveMode(XSME.Single)]
        public string Calculation
        {
            get => _Calculation;
            set => SetAndNotify(ref _Calculation, value);
        }
        #endregion
        #region CalculationOptions
        private List<string> _CalculationOptions;
        public List<string> CalculationOptions
        {
            get => _CalculationOptions;
            set => SetAndNotify(ref _CalculationOptions, value);
        }
        #endregion

        // Translate Values
        #region SourceVariable
        private string _SourceVariable;
        [XmlSaveMode(XSME.Single)]
        public string SourceVariable
        {
            get => _SourceVariable;
            set => SetAndNotify(ref _SourceVariable, value);
        }
        #endregion
        #region Pairs
        private ObservableCollection<StringPair> _Pairs;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<StringPair> Pairs
        {
            get => _Pairs;
            set => SetAndNotify(ref _Pairs, value);
        }
        #endregion

        // Make Attack Roll
        #region AttackAttribute
        private string _AttackAttribute;
        [XmlSaveMode(XSME.Single)]
        public string AttackAttribute
        {
            get => _AttackAttribute;
            set => SetAndNotify(ref _AttackAttribute, value);
        }
        #endregion
        #region AttackAttributes
        private List<string> _AttackAttributes;
        public List<string> AttackAttributes
        {
            get => _AttackAttributes;
            set => SetAndNotify(ref _AttackAttributes, value);
        }
        #endregion
        #region UseProficiencyBonus
        private bool _UseProficiencyBonus;
        [XmlSaveMode(XSME.Single)]
        public bool UseProficiencyBonus
        {
            get => _UseProficiencyBonus;
            set => SetAndNotify(ref _UseProficiencyBonus, value);
        }
        #endregion

        #region HasValueScaling
        private bool _HasValueScaling;
        [XmlSaveMode(XSME.Single)]
        public bool HasValueScaling
        {
            get => _HasValueScaling;
            set => SetAndNotify(ref _HasValueScaling, value);
        }
        #endregion
        #region ValueScaleRate
        private int _ValueScaleRate;
        [XmlSaveMode(XSME.Single)]
        public int ValueScaleRate
        {
            get => _ValueScaleRate;
            set => SetAndNotify(ref _ValueScaleRate, value);
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
        #region StatOptions
        private List<string> _StatOptions;
        public List<string> StatOptions
        {
            get => _StatOptions;
            set => SetAndNotify(ref _StatOptions, value);
        }
        #endregion

        #region ShowConditionValue
        private bool _ShowConditionValue;
        public bool ShowConditionValue
        {
            get => _ShowConditionValue;
            set => SetAndNotify(ref _ShowConditionValue, value);
        }
        #endregion
        #region ShowQuestion
        private bool _ShowQuestion;
        public bool ShowQuestion
        {
            get => _ShowQuestion;
            set => SetAndNotify(ref _ShowQuestion, value);
        }
        #endregion
        #region ShowScaling
        private bool _ShowScaling;
        public bool ShowScaling
        {
            get => _ShowScaling;
            set => SetAndNotify(ref _ShowScaling, value);
        }
        #endregion

        #region ShowSetValueForm
        private bool _ShowSetValueForm;
        public bool ShowSetValueForm
        {
            get => _ShowSetValueForm;
            set => SetAndNotify(ref _ShowSetValueForm, value);
        }
        #endregion
        #region ShowRollValueForm
        private bool _ShowRollValueForm;
        public bool ShowRollValueForm
        {
            get => _ShowRollValueForm;
            set => SetAndNotify(ref _ShowRollValueForm, value);
        }
        #endregion
        #region ShowStatValueForm
        private bool _ShowStatValueForm;
        public bool ShowStatValueForm
        {
            get => _ShowStatValueForm;
            set => SetAndNotify(ref _ShowStatValueForm, value);
        }
        #endregion
        #region ShowCalculatedValueForm
        private bool _ShowCalculatedValueForm;
        public bool ShowCalculatedValueForm
        {
            get => _ShowCalculatedValueForm;
            set => SetAndNotify(ref _ShowCalculatedValueForm, value);
        }
        #endregion
        #region ShowAttackForm
        private bool _ShowAttackForm;
        [XmlSaveMode(XSME.Single)]
        public bool ShowAttackForm
        {
            get => _ShowAttackForm;
            set => SetAndNotify(ref _ShowAttackForm, value);
        }
        #endregion
        #region ShowPairForm
        private bool _ShowPairForm;
        [XmlSaveMode(XSME.Single)]
        public bool ShowPairForm
        {
            get => _ShowPairForm;
            set => SetAndNotify(ref _ShowPairForm, value);
        }
        #endregion

        // Commands
        #region RemoveAction
        public ICommand RemoveAction => new RelayCommand(DoRemoveAction);
        private void DoRemoveAction(object param)
        {
            if (param.GetType() == typeof(CustomAbility))
            {
                (param as CustomAbility).PreActions.Remove(this);
            }
        }
        #endregion
        #region AddAnswer
        public ICommand AddAnswer => new RelayCommand(DoAddAnswer);
        private void DoAddAnswer(object param)
        {
            Answers.Add(new());
        }
        #endregion
        #region AddCondition
        public ICommand AddCondition => new RelayCommand(DoAddCondition);
        private void DoAddCondition(object param)
        {
            CACondition condition = new();
            condition.ConditionVariables.AddRange(Targets);
            if (Configuration.MainModelRef.TabSelected_Players)
            {
                foreach (CharacterAlterant alterant in Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Alterants)
                {
                    condition.ConditionVariables.Add(alterant.Name);
                }
            }
            Conditions.Add(condition);
        }
        #endregion
        #region MoveAction
        public ICommand MoveAction => new RelayCommand(DoMoveAction);
        private void DoMoveAction(object param)
        {
            if (param == null) { return; }
            if ((param as object[]) == null) { return; }

            try
            {
                CustomAbility ability = (param as object[])[0] as CustomAbility;
                string direction = (param as object[])[1].ToString();
                int currentPosition = ability.PreActions.IndexOf(this);
                if (currentPosition == 0 && direction == "Up") { return; }
                if (currentPosition == ability.PreActions.Count - 1 && direction == "Down") { return; }
                int newPosition = currentPosition;
                if (direction == "Up") { newPosition--; }
                if (direction == "Down") { newPosition++; }
                ability.PreActions.Move(currentPosition, newPosition);
            }
            catch (Exception e)
            {
                HelperMethods.NotifyUser("CAPreAction.DoMoveAction\n" + e.Message);
            }

            

        }
        #endregion
        #region AddPair
        public ICommand AddPair => new RelayCommand(DoAddPair);
        private void DoAddPair(object param)
        {
            Pairs.Add(new());
        }
        #endregion

        // Public Methods
        public void UpdateTargetList(List<string> variables)
        {
            Targets = new();
            Targets.AddRange(Configuration.InternalAbilityVariables);
            Targets.AddRange(variables);
        }

        // Private Methods
        private void SetFormDisplays()
        {
            ShowSetValueForm = false;
            ShowRollValueForm = false;
            ShowStatValueForm = false;
            ShowCalculatedValueForm = false;
            ShowQuestion = false;
            ShowScaling = false;
            ShowPairForm = false;

            List<string> scalableActions = new() { "Add Set Value", "Add Roll" };
            ShowScaling = scalableActions.Contains(Action);
            ShowSetValueForm = (Action == "Add Set Value" || Action == "Append Text");
            ShowRollValueForm = (Action == "Add Roll");
            ShowStatValueForm = (Action == "Add Stat Value");
            ShowCalculatedValueForm = (Action == "Add Calculated Value");
            ShowQuestion = (Action == "QA Prompt");
            ShowAttackForm = Action == "Make Attack Roll";
            ShowPairForm = Action == "Translate Value";

        }
        private void UpdateDisplayText()
        {
            DisplayText = !string.IsNullOrEmpty(CustomDisplayText) ? CustomDisplayText : string.Format("{0} - {1}", Action, Target);
        }
        private void InitializeCollections()
        {
            Answers = new();
            ActionOptions = new() { "Add Roll", "Add Set Value", "Add Stat Value", "Add Calculated Value", "QA Prompt", "Make Attack Roll", "Numeric Value Prompt", "Translate Value" };
            StatOptions = new() { "Spellcasting Ability Modifier", "Spellcasting Attack Modifier", "Spellcasting Save DC", "Proficiency Bonus", "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
            Conditions = new();
            CalculationOptions = new() { "multiplied by", "divided by", "plus", "minus" };
            AttackAttributes = new() { "None", "Strength", "Dexterity", "Spellcasting" };
            Targets = new();
            Targets.AddRange(Configuration.InternalAbilityVariables);
            Pairs = new();
        }

    }
}
