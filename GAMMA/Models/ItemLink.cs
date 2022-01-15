using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class ItemLink : BaseModel
    {
        // Constructors
        public ItemLink()
        {

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
        #region DropChance
        private int _DropChance;
        [XmlSaveMode(XSME.Single)]
        public int DropChance
        {
            get
            {
                return _DropChance;
            }
            set
            {
                _DropChance = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Quantity
        private int _Quantity;
        [XmlSaveMode(XSME.Single)]
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LinkedItem
        private ItemModel _LinkedItem;
        public ItemModel LinkedItem
        {
            get
            {
                return _LinkedItem;
            }
            set
            {
                _LinkedItem = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveItemLink
        private RelayCommand _RemoveItemLink;
        public ICommand RemoveItemLink
        {
            get
            {
                if (_RemoveItemLink == null)
                {
                    _RemoveItemLink = new RelayCommand(DoRemoveItemLink);
                }
                return _RemoveItemLink;
            }
        }
        private void DoRemoveItemLink(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("Missing parameter for DoRemoveItemLink", true); return; }
            string location = param.ToString();
            switch (location)
            {
                case "Active Creature":
                    Configuration.MainModelRef.CreatureBuilderView.ActiveCreature.ItemLinks.Remove(this);
                    break;
                case "Active Loot Box":
                    Configuration.MainModelRef.ToolsView.ActiveLootBox.ItemLinks.Remove(this);
                    break;
                case "Active Character Chosen Equipment":
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ChosenEquipment.Remove(this);
                    break;
                case "Character Accessories":
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.EquippedAccessories.Remove(this);
                    break;
                default:
                    HelperMethods.WriteToLogFile("Unhandled parameter in DoRemoveItemLink: " + location, true);
                    break;
            }
        }
        #endregion

    }
}
