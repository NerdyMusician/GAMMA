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
            get => _Description;
            set => SetAndNotify(ref _Description, value);
        }
        #endregion
        #region GoldPieces
        private int _GoldPieces;
        [XmlSaveMode(XSME.Single)]
        public int GoldPieces
        {
            get => _GoldPieces;
            set => SetAndNotify(ref _GoldPieces, value);
        }
        #endregion
        #region MandatoryEquipment
        private ObservableCollection<ItemModel> _MandatoryEquipment;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ItemModel> MandatoryEquipment
        {
            get => _MandatoryEquipment;
            set => SetAndNotify(ref _MandatoryEquipment, value);
        }
        #endregion

        // Commands
        #region RemovePlayerBackground
        public ICommand RemovePlayerBackground => new RelayCommand(param => DoRemovePlayerBackground());
        private void DoRemovePlayerBackground()
        {
            Configuration.MainModelRef.ToolsView.PlayerBackgrounds.Remove(this);
        }
        #endregion
        #region AddMandatoryEquipment
        public ICommand AddMandatoryEquipment => new RelayCommand(param => DoAddMandatoryEquipment());
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
