using GAMMA.Toolbox;
using GAMMA.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class RollTableModel : BaseModel
    {
        // Constructors
        public RollTableModel()
        {
            Name = "New Roll Table";
            TableRows = new ObservableCollection<RollTableRowModel>();
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
        #region TableRows
        private ObservableCollection<RollTableRowModel> _TableRows;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<RollTableRowModel> TableRows
        {
            get
            {
                return _TableRows;
            }
            set
            {
                _TableRows = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HasModifier
        private bool _HasModifier;
        [XmlSaveMode("Single")]
        public bool HasModifier
        {
            get
            {
                return _HasModifier;
            }
            set
            {
                _HasModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ModifierInfo
        private string _ModifierInfo;
        [XmlSaveMode("Single")]
        public string ModifierInfo
        {
            get
            {
                return _ModifierInfo;
            }
            set
            {
                _ModifierInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AvailableToPlayers
        private bool _AvailableToPlayers;
        [XmlSaveMode("Single")]
        public bool AvailableToPlayers
        {
            get
            {
                return _AvailableToPlayers;
            }
            set
            {
                _AvailableToPlayers = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddRow
        private RelayCommand _AddRow;
        public ICommand AddRow
        {
            get
            {
                if (_AddRow == null)
                {
                    _AddRow = new RelayCommand(param => DoAddRow());
                }
                return _AddRow;
            }
        }
        private void DoAddRow()
        {
            TableRows.Add(new RollTableRowModel());
        }
        #endregion
        #region RemoveTable
        private RelayCommand _RemoveTable;
        public ICommand RemoveTable
        {
            get
            {
                if (_RemoveTable == null)
                {
                    _RemoveTable = new RelayCommand(param => DoRemoveTable());
                }
                return _RemoveTable;
            }
        }
        private void DoRemoveTable()
        {
            Configuration.MainModelRef.ToolsView.RollTables.Remove(this);
        }
        #endregion
        #region RollTable
        private RelayCommand _RollTable;
        public ICommand RollTable
        {
            get
            {
                if (_RollTable == null)
                {
                    _RollTable = new RelayCommand(param => DoRollTable());
                }
                return _RollTable;
            }
        }
        private void DoRollTable()
        {
            int minVal = 1;
            int maxVal = 1;
            string msg = "";

            foreach (RollTableRowModel row in TableRows)
            {
                if (row.LowValue < minVal) { minVal = row.LowValue; }
                if (row.HighValue > maxVal) { maxVal = row.HighValue; }
            }

            int roll = Configuration.RNG.Next(minVal, maxVal + 1);
            if (HasModifier)
            {
                NumberInputDialog numberInput = new NumberInputDialog(Name + " Table Modifier", ModifierInfo);
                if (numberInput.ShowDialog() == true)
                {
                    roll += numberInput.Number;
                    if (roll > maxVal) { roll = maxVal; }
                }
                else
                {
                    return;
                }
            }

            foreach (RollTableRowModel row in TableRows)
            {
                if (roll >= row.LowValue && roll <= row.HighValue)
                {
                    if (msg.Length > 0) { msg += "\n"; }
                    msg += row.Description;
                }
            }

            if (Configuration.MainModelRef.TabSelected_Players)
            {
                msg = msg.Insert(0, Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Name + " rolled on the " + Name + " table.\nResult: " + roll + "\n");
                HelperMethods.AddToPlayerLog(msg, "Default", true);
            }
            else
            {
                new NotificationDialog("Result: " + roll + "\n" + msg).ShowDialog();
            }

        }
        #endregion

    }
}
