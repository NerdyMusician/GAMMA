using GAMMA.Models;
using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GAMMA.ViewModels
{
    public class ShopViewModel : BaseModel
    {
        // Constructors
        public ShopViewModel(List<ItemModel> shopItems, List<ItemModel> characterItems, int characterCoinage)
        {
            Configuration.ShopRef = this;
            ShopItems = new ObservableCollection<ItemModel>(shopItems);
            CharacterItems = new ObservableCollection<ItemModel>(characterItems);
            CharacterOfferedItems = new ObservableCollection<ItemModel>();
            ShopOfferedItems = new ObservableCollection<ItemModel>();
            CharacterCoinage = characterCoinage;
            TransactionValue = 0;
        }

        // Databound Properties
        #region ShopItems
        private ObservableCollection<ItemModel> _ShopItems;
        public ObservableCollection<ItemModel> ShopItems
        {
            get
            {
                return _ShopItems;
            }
            set
            {
                _ShopItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShopOfferedItems
        private ObservableCollection<ItemModel> _ShopOfferedItems;
        public ObservableCollection<ItemModel> ShopOfferedItems
        {
            get
            {
                return _ShopOfferedItems;
            }
            set
            {
                _ShopOfferedItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharacterItems
        private ObservableCollection<ItemModel> _CharacterItems;
        public ObservableCollection<ItemModel> CharacterItems
        {
            get
            {
                return _CharacterItems;
            }
            set
            {
                _CharacterItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharacterOfferedItems
        private ObservableCollection<ItemModel> _CharacterOfferedItems;
        public ObservableCollection<ItemModel> CharacterOfferedItems
        {
            get
            {
                return _CharacterOfferedItems;
            }
            set
            {
                _CharacterOfferedItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharacterCoinage
        private int _CharacterCoinage;
        public int CharacterCoinage
        {
            get
            {
                return _CharacterCoinage;
            }
            set
            {
                _CharacterCoinage = value;
                NotifyPropertyChanged();
                ProcessedCharacterCoinage = HelperMethods.GetDerivedCoinage(value);
            }
        }
        #endregion
        #region ProcessedCharacterCoinage
        private string _ProcessedCharacterCoinage;
        public string ProcessedCharacterCoinage
        {
            get
            {
                return _ProcessedCharacterCoinage;
            }
            set
            {
                _ProcessedCharacterCoinage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TransactionValue
        private int _TransactionValue;
        public int TransactionValue
        {
            get
            {
                return _TransactionValue;
            }
            set
            {
                _TransactionValue = value;
                NotifyPropertyChanged();
                ProcessedValue = (value < 0) ? HelperMethods.GetDerivedCoinage(value * -1) : HelperMethods.GetDerivedCoinage(value);
                ToWho = (value < 0) ? "To Vendor: " : "To Player: ";
            }
        }
        #endregion
        #region ProcessedValue
        private string _ProcessedValue;
        public string ProcessedValue
        {
            get
            {
                return _ProcessedValue;
            }
            set
            {
                _ProcessedValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ToWho
        private string _ToWho;
        public string ToWho
        {
            get
            {
                return _ToWho;
            }
            set
            {
                _ToWho = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Public Methods
        public void UpdateTransactionValue()
        {
            int playerValue = 0;
            int shopValue = 0;

            foreach (ItemModel item in CharacterOfferedItems)
            {
                playerValue += item.RawValue * item.Quantity;
            }

            foreach (ItemModel item in ShopOfferedItems)
            {
                shopValue += item.RawValue * item.Quantity;
            }

            playerValue = Convert.ToInt32((decimal)playerValue * 0.6m);

            TransactionValue = playerValue - shopValue;

        }
        public void UndoCharacterOfferings()
        {
            if (CharacterOfferedItems.Count <= 0) { return; }
            do
            {
                CharacterOfferedItems.First().DoRetractItemToSell(1);
            }
            while (CharacterOfferedItems.Count > 0);
        }

    }
}
