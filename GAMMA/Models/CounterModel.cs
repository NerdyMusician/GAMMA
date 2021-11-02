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
        [XmlSaveMode("Single")]
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
        #region CurrentValue
        private int _CurrentValue;
        [XmlSaveMode("Single")]
        public int CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                _CurrentValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MaxValue
        private int _MaxValue;
        [XmlSaveMode("Single")]
        public int MaxValue
        {
            get
            {
                return _MaxValue;
            }
            set
            {
                _MaxValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ResetOnRest
        private bool _ResetOnRest;
        [XmlSaveMode("Single")]
        public bool ResetOnRest
        {
            get
            {
                return _ResetOnRest;
            }
            set
            {
                _ResetOnRest = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ResetOnShortRest
        private bool _ResetOnShortRest;
        [XmlSaveMode("Single")]
        public bool ResetOnShortRest
        {
            get
            {
                return _ResetOnShortRest;
            }
            set
            {
                _ResetOnShortRest = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Description
        private string _Description;
        [XmlSaveMode("Single")]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowOptions
        private bool _ShowOptions;
        public bool ShowOptions
        {
            get
            {
                return _ShowOptions;
            }
            set
            {
                _ShowOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveCounterFromPlayer
        private RelayCommand _RemoveCounterFromPlayer;
        public ICommand RemoveCounterFromPlayer
        {
            get
            {
                if (_RemoveCounterFromPlayer == null)
                {
                    _RemoveCounterFromPlayer = new RelayCommand(param => DoRemoveCounterFromPlayer());
                }
                return _RemoveCounterFromPlayer;
            }
        }
        private void DoRemoveCounterFromPlayer()
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
