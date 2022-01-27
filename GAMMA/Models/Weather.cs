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
        #region Climate
        private string _Climate;
        [XmlSaveMode(XSME.Single)]
        public string Climate
        {
            get => _Climate;
            set => SetAndNotify(ref _Climate, value);
        }
        #endregion
        #region Climates
        private List<string> _Climates;
        public List<string> Climates
        {
            get => _Climates;
            set => SetAndNotify(ref _Climates, value);
        }
        #endregion
        #region WeatherEntries
        private ObservableCollection<WeatherRow> _WeatherEntries;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<WeatherRow> WeatherEntries
        {
            get => _WeatherEntries;
            set => SetAndNotify(ref _WeatherEntries, value);
        }
        #endregion

        // Commands
        #region RemoveWeather
        public ICommand RemoveWeather => new RelayCommand(DoRemoveWeather);
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

    }
}
