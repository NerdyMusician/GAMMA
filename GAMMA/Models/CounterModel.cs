using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class CounterModel : BaseModel
    {
        // Constructors
        public CounterModel()
        {
            Name = "New Counter";
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
        #region CurrentValue
        private int _CurrentValue;
        [XmlSaveMode(XSME.Single)]
        public int CurrentValue
        {
            get => _CurrentValue;
            set => SetAndNotify(ref _CurrentValue, value);
        }
        #endregion
        #region MaxValue
        private int _MaxValue;
        [XmlSaveMode(XSME.Single)]
        public int MaxValue
        {
            get => _MaxValue;
            set => SetAndNotify(ref _MaxValue, value);
        }
        #endregion
        #region ResetOnRest
        private bool _ResetOnRest;
        [XmlSaveMode(XSME.Single)]
        public bool ResetOnRest
        {
            get => _ResetOnRest;
            set => SetAndNotify(ref _ResetOnRest, value);
        }
        #endregion
        #region ResetOnShortRest
        private bool _ResetOnShortRest;
        [XmlSaveMode(XSME.Single)]
        public bool ResetOnShortRest
        {
            get => _ResetOnShortRest;
            set => SetAndNotify(ref _ResetOnShortRest, value);
        }
        #endregion
        #region Description
        private string _Description;
        [XmlSaveMode(XSME.Single)]
        public string Description
        {
            get => _Description;
            set => SetAndNotify(ref _Description, value);
        }
        #endregion
        #region ShowOptions
        private bool _ShowOptions;
        public bool ShowOptions
        {
            get => _ShowOptions;
            set => SetAndNotify(ref _ShowOptions, value);
        }
        #endregion

        // Commands
        #region RemoveCounterFromPlayer
        public ICommand RemoveCounterFromPlayer => new RelayCommand(DoRemoveCounterFromPlayer);
        private void DoRemoveCounterFromPlayer(object param)
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Counters.Remove(this);
        }
        #endregion
        #region RemoveCounterFromCreature
        public ICommand RemoveCounterFromCreature => new RelayCommand(param => DoRemoveCounterFromCreature());
        private void DoRemoveCounterFromCreature()
        {
            Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.Counters.Remove(this);
        }
        #endregion
        #region AlterCurrentCount
        private RelayCommand _AlterCurrentCount;
        public ICommand AlterCurrentCount
        {
            get
            {
                if (_AlterCurrentCount == null)
                {
                    _AlterCurrentCount = new RelayCommand(DoAlterCurrentCount);
                }
                return _AlterCurrentCount;
            }
        }
        private void DoAlterCurrentCount(object amount)
        {
            int.TryParse(amount.ToString(), out int amt);
            if ((CurrentValue + amt) < 0) { CurrentValue = 0; }
            else if ((CurrentValue + amt) > MaxValue) { CurrentValue = MaxValue; }
            else { CurrentValue += amt; }
        }
        #endregion
        #region ResetCount
        public ICommand ResetCount => new RelayCommand(DoResetCount);
        private void DoResetCount(object param)
        {
            CurrentValue = MaxValue;
        }
        #endregion

    }
}
