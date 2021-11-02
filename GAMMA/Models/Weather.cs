using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class Weather : BaseModel
    {
        // Constructors
        public Weather()
        {
            Climates = new()
            {
                "Temperate",
                "Arid",
                "Frozen"
            };
            WeatherEntries = new();
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
        #region IsValidated
        private bool _IsValidated;
        [XmlSaveMode("Single")]
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
        #region Climate
        private string _Climate;
        [XmlSaveMode("Single")]
        public string Climate
        {
            get
            {
                return _Climate;
            }
            set
            {
                _Climate = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Climates
        private List<string> _Climates;
        public List<string> Climates
        {
            get
            {
                return _Climates;
            }
            set
            {
                _Climates = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeatherEntries
        private ObservableCollection<WeatherRow> _WeatherEntries;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<WeatherRow> WeatherEntries
        {
            get
            {
                return _WeatherEntries;
            }
            set
            {
                _WeatherEntries = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddWeatherEntry
        private RelayCommand _AddWeatherEntry;
        public ICommand AddWeatherEntry
        {
            get
            {
                if (_AddWeatherEntry == null)
                {
                    _AddWeatherEntry = new RelayCommand(DoAddWeatherEntry);
                }
                return _AddWeatherEntry;
            }
        }
        private void DoAddWeatherEntry(object param)
        {
        }
        #endregion
        #region SortWeatherEntries
        private RelayCommand _SortWeatherEntries;
        public ICommand SortWeatherEntries
        {
            get
            {
                if (_SortWeatherEntries == null)
                {
                    _SortWeatherEntries = new RelayCommand(DoSortWeatherEntries);
                }
                return _SortWeatherEntries;
            }
        }
        private void DoSortWeatherEntries(object param)
        {
        }
        #endregion
        #region RemoveWeather
        private RelayCommand _RemoveWeather;
        public ICommand RemoveWeather
        {
            get
            {
                if (_RemoveWeather == null)
                {
                    _RemoveWeather = new RelayCommand(DoRemoveWeather);
                }
                return _RemoveWeather;
            }
        }
        private void DoRemoveWeather(object param)
        {
            Configuration.MainModelRef.ToolsView.Weathers.Remove(this);
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            Weather duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.Weathers.Insert(Configuration.MainModelRef.ToolsView.Weathers.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActiveWeather = duplicate;
        }
        #endregion

        // Public Methods

        // Private Methods
    }
}
