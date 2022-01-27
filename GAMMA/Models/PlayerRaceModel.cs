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
            get => _AgeInfo;
            set => SetAndNotify(ref _AgeInfo, value);
        }
        #endregion
        #region AlignmentInfo
        private string _AlignmentInfo;
        [XmlSaveMode(XSME.Single)]
        public string AlignmentInfo
        {
            get => _AlignmentInfo;
            set => SetAndNotify(ref _AlignmentInfo, value);
        }
        #endregion
        #region SizeInfo
        private string _SizeInfo;
        [XmlSaveMode(XSME.Single)]
        public string SizeInfo
        {
            get => _SizeInfo;
            set => SetAndNotify(ref _SizeInfo, value);
        }
        #endregion
        #region Size
        private string _Size;
        [XmlSaveMode(XSME.Single)]
        public string Size
        {
            get => _Size;
            set => SetAndNotify(ref _Size, value);
        }
        #endregion
        #region FeatsGranted
        private int _FeatsGranted;
        [XmlSaveMode(XSME.Single)]
        public int FeatsGranted
        {
            get => _FeatsGranted;
            set => SetAndNotify(ref _FeatsGranted, value);
        }
        #endregion
        #region BaseSpeed
        private int _BaseSpeed;
        [XmlSaveMode(XSME.Single)]
        public int BaseSpeed
        {
            get => _BaseSpeed;
            set => SetAndNotify(ref _BaseSpeed, value);
        }
        #endregion
        #region Languages
        private string _Languages;
        [XmlSaveMode(XSME.Single)]
        public string Languages
        {
            get => _Languages;
            set => SetAndNotify(ref _Languages, value);
        }
        #endregion
        #region Darkvision
        private int _Darkvision;
        [XmlSaveMode(XSME.Single)]
        public int Darkvision
        {
            get => _Darkvision;
            set => SetAndNotify(ref _Darkvision, value);
        }
        #endregion
        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<TraitModel> Traits
        {
            get => _Traits;
            set => SetAndNotify(ref _Traits, value);
        }
        #endregion

        // Commands
        #region RemovePlayerRace
        public ICommand RemovePlayerRace => new RelayCommand(param => DoRemovePlayerRace());
        private void DoRemovePlayerRace()
        {
            Configuration.MainModelRef.ToolsView.PlayerRaces.Remove(this);
        }
        #endregion
        #region AddTrait
        public ICommand AddTrait => new RelayCommand(param => DoAddTrait());
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
