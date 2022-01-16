using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerBackgroundModel : PlayerBuildingBlock
    {
        // Constructors
        public PlayerBackgroundModel()
        {
            Name = "New Player Background";
            MandatoryEquipment = new ObservableCollection<ItemModel>();
            EquipmentChoices = new ObservableCollection<ConvertibleValue>();
        }

        // Databound Properties
        #region Description
        private string _Description;
        [XmlSaveMode(XSME.Single)]
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
        #region GoldPieces
        private int _GoldPieces;
        [XmlSaveMode(XSME.Single)]
        public int GoldPieces
        {
            get
            {
                return _GoldPieces;
            }
            set
            {
                _GoldPieces = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MandatoryEquipment
        private ObservableCollection<ItemModel> _MandatoryEquipment;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> MandatoryEquipment
        {
            get
            {
                return _MandatoryEquipment;
            }
            set
            {
                _MandatoryEquipment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemovePlayerBackground
        private RelayCommand _RemovePlayerBackground;
        public ICommand RemovePlayerBackground
        {
            get
            {
                if (_RemovePlayerBackground == null)
                {
                    _RemovePlayerBackground = new RelayCommand(param => DoRemovePlayerBackground());
                }
                return _RemovePlayerBackground;
            }
        }
        private void DoRemovePlayerBackground()
        {
            Configuration.MainModelRef.ToolsView.PlayerBackgrounds.Remove(this);
        }
        #endregion
        #region AddMandatoryEquipment
        private RelayCommand _AddMandatoryEquipment;
        public ICommand AddMandatoryEquipment
        {
            get
            {
                if (_AddMandatoryEquipment == null)
                {
                    _AddMandatoryEquipment = new RelayCommand(param => DoAddMandatoryEquipment());
                }
                return _AddMandatoryEquipment;
            }
        }
        private void DoAddMandatoryEquipment()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.ItemRepository.Where(item => item.IsValidated).ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                foreach (ItemModel item in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    bool existingFound = false;
                    ItemModel itemToAdd = HelperMethods.DeepClone(item);
                    foreach (ItemModel cItem in MandatoryEquipment)
                    {
                        if (cItem.Name == item.Name)
                        {
                            cItem.Quantity += item.Quantity;
                            existingFound = true;
                            break;
                        }
                    }
                    if (existingFound) { continue; }
                    MandatoryEquipment.Add(itemToAdd);
                }
            }

        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            PlayerBackgroundModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.PlayerBackgrounds.Insert(Configuration.MainModelRef.ToolsView.PlayerBackgrounds.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActivePlayerBackground = duplicate;
        }
        #endregion

    }
}
