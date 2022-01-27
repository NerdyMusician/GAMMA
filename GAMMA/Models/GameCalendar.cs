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
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode(XSME.Single)]
        public bool IsValidated
        {
            get => _IsValidated;
            set => SetAndNotify(ref _IsValidated, value);
        }
        #endregion
        #region WeeksPerMonth
        private int _WeeksPerMonth;
        [XmlSaveMode(XSME.Single)]
        public int WeeksPerMonth
        {
            get => _WeeksPerMonth;
            set => SetAndNotify(ref _WeeksPerMonth, value);
        }
        #endregion
        #region UseEras
        private bool _UseEras;
        [XmlSaveMode(XSME.Single)]
        public bool UseEras
        {
            get => _UseEras;
            set => SetAndNotify(ref _UseEras, value);
        }
        #endregion
        #region YearsPerEra
        private int _YearsPerEra;
        [XmlSaveMode(XSME.Single)]
        public int YearsPerEra
        {
            get => _YearsPerEra;
            set => SetAndNotify(ref _YearsPerEra, value);
        }
        #endregion
        #region Months
        private ObservableCollection<ConvertibleValue> _Months;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Months
        {
            get => _Months;
            set => SetAndNotify(ref _Months, value);
        }
        #endregion
        #region Days
        private ObservableCollection<ConvertibleValue> _Days;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Days
        {
            get => _Days;
            set => SetAndNotify(ref _Days, value);
        }
        #endregion

        // Commands
        #region RemoveCalendar
        public ICommand RemoveCalendar => new RelayCommand(DoRemoveCalendar);
        private void DoRemoveCalendar(object param)
        {
            Configuration.MainModelRef.ToolsView.Calendars.Remove(this);
        }
        #endregion
        #region AddDay
        public ICommand AddDay => new RelayCommand(DoAddDay);
        private void DoAddDay(object param)
        {
            Days.Add(new());
        }
        #endregion
        #region AddMonth
        public ICommand AddMonth => new RelayCommand(DoAddMonth);
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

    }
}
