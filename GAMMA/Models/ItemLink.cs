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
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region DropChance
        private int _DropChance;
        [XmlSaveMode(XSME.Single)]
        public int DropChance
        {
            get => _DropChance;
            set => SetAndNotify(ref _DropChance, value);
        }
        #endregion
        #region Quantity
        private int _Quantity;
        [XmlSaveMode(XSME.Single)]
        public int Quantity
        {
            get => _Quantity;
            set => SetAndNotify(ref _Quantity, value);
        }
        #endregion
        #region LinkedItem
        private ItemModel _LinkedItem;
        public ItemModel LinkedItem
        {
            get => _LinkedItem;
            set => SetAndNotify(ref _LinkedItem, value);
        }
        #endregion

        // Commands
        #region RemoveItemLink
        public ICommand RemoveItemLink => new RelayCommand(DoRemoveItemLink);
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
