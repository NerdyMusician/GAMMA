using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class GameCalendar : BaseModel
    {
        // Constructors
        public GameCalendar()
        {
            Name = "New Calendar";
            Days = new();
            Months = new();
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
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get
            {
                return _IsValidated;
            }
            set
            {
                _IsValidated = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeeksPerMonth
        private int _WeeksPerMonth;
        [XmlSaveMode(XSME.Single)]
        public int WeeksPerMonth
        {
            get
            {
                return _WeeksPerMonth;
            }
            set
            {
                _WeeksPerMonth = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region UseEras
        private bool _UseEras;
        [XmlSaveMode(XSME.Single)]
        public bool UseEras
        {
            get
            {
                return _UseEras;
            }
            set
            {
                _UseEras = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region YearsPerEra
        private int _YearsPerEra;
        [XmlSaveMode(XSME.Single)]
        public int YearsPerEra
        {
            get
            {
                return _YearsPerEra;
            }
            set
            {
                _YearsPerEra = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Months
        private ObservableCollection<ConvertibleValue> _Months;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Months
        {
            get
            {
                return _Months;
            }
            set
            {
                _Months = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Days
        private ObservableCollection<ConvertibleValue> _Days;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Days
        {
            get
            {
                return _Days;
            }
            set
            {
                _Days = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveCalendar
        private RelayCommand _RemoveCalendar;
        public ICommand RemoveCalendar
        {
            get
            {
                if (_RemoveCalendar == null)
                {
                    _RemoveCalendar = new RelayCommand(DoRemoveCalendar);
                }
                return _RemoveCalendar;
            }
        }
        private void DoRemoveCalendar(object param)
        {
            Configuration.MainModelRef.ToolsView.Calendars.Remove(this);
        }
        #endregion
        #region AddDay
        private RelayCommand _AddDay;
        public ICommand AddDay
        {
            get
            {
                if (_AddDay == null)
                {
                    _AddDay = new RelayCommand(DoAddDay);
                }
                return _AddDay;
            }
        }
        private void DoAddDay(object param)
        {
            Days.Add(new());
        }
        #endregion
        #region AddMonth
        private RelayCommand _AddMonth;
        public ICommand AddMonth
        {
            get
            {
                if (_AddMonth == null)
                {
                    _AddMonth = new RelayCommand(DoAddMonth);
                }
                return _AddMonth;
            }
        }
        private void DoAddMonth(object param)
        {
            Months.Add(new());
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            GameCalendar duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.Calendars.Insert(Configuration.MainModelRef.ToolsView.Calendars.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActiveCalendar = duplicate;
        }
        #endregion

        // Public Methods

        // Private Methods

    }
}
