using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerRaceModel : PlayerBuildingBlock
    {
        // Constructors
        public PlayerRaceModel()
        {
            Name = "New Player Race";
            Traits = new();
        }

        // Databound Properties
        #region AgeInfo
        private string _AgeInfo;
        [XmlSaveMode(XSME.Single)]
        public string AgeInfo
        {
            get
            {
                return _AgeInfo;
            }
            set
            {
                _AgeInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AlignmentInfo
        private string _AlignmentInfo;
        [XmlSaveMode(XSME.Single)]
        public string AlignmentInfo
        {
            get
            {
                return _AlignmentInfo;
            }
            set
            {
                _AlignmentInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SizeInfo
        private string _SizeInfo;
        [XmlSaveMode(XSME.Single)]
        public string SizeInfo
        {
            get
            {
                return _SizeInfo;
            }
            set
            {
                _SizeInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Size
        private string _Size;
        [XmlSaveMode(XSME.Single)]
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region FeatsGranted
        private int _FeatsGranted;
        [XmlSaveMode(XSME.Single)]
        public int FeatsGranted
        {
            get
            {
                return _FeatsGranted;
            }
            set
            {
                _FeatsGranted = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region BaseSpeed
        private int _BaseSpeed;
        [XmlSaveMode(XSME.Single)]
        public int BaseSpeed
        {
            get
            {
                return _BaseSpeed;
            }
            set
            {
                _BaseSpeed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Languages
        private string _Languages;
        [XmlSaveMode(XSME.Single)]
        public string Languages
        {
            get
            {
                return _Languages;
            }
            set
            {
                _Languages = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Darkvision
        private int _Darkvision;
        [XmlSaveMode(XSME.Single)]
        public int Darkvision
        {
            get
            {
                return _Darkvision;
            }
            set
            {
                _Darkvision = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<TraitModel> Traits
        {
            get
            {
                return _Traits;
            }
            set
            {
                _Traits = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemovePlayerRace
        private RelayCommand _RemovePlayerRace;
        public ICommand RemovePlayerRace
        {
            get
            {
                if (_RemovePlayerRace == null)
                {
                    _RemovePlayerRace = new RelayCommand(param => DoRemovePlayerRace());
                }
                return _RemovePlayerRace;
            }
        }
        private void DoRemovePlayerRace()
        {
            Configuration.MainModelRef.ToolsView.PlayerRaces.Remove(this);
        }
        #endregion
        #region AddTrait
        private RelayCommand _AddTrait;
        public ICommand AddTrait
        {
            get
            {
                if (_AddTrait == null)
                {
                    _AddTrait = new RelayCommand(param => DoAddTrait());
                }
                return _AddTrait;
            }
        }
        private void DoAddTrait()
        {
            Traits.Add(new TraitModel());
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            PlayerRaceModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.PlayerRaces.Insert(Configuration.MainModelRef.ToolsView.PlayerRaces.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActivePlayerRace = duplicate;
        }
        #endregion

    }
}
