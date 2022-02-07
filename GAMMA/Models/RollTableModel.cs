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
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region TableRows
        private ObservableCollection<RollTableRowModel> _TableRows;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<RollTableRowModel> TableRows
        {
            get => _TableRows;
            set => SetAndNotify(ref _TableRows, value);
        }
        #endregion
        #region HasModifier
        private bool _HasModifier;
        [XmlSaveMode(XSME.Single)]
        public bool HasModifier
        {
            get => _HasModifier;
            set => SetAndNotify(ref _HasModifier, value);
        }
        #endregion
        #region ModifierInfo
        private string _ModifierInfo;
        [XmlSaveMode(XSME.Single)]
        public string ModifierInfo
        {
            get => _ModifierInfo;
            set => SetAndNotify(ref _ModifierInfo, value);
        }
        #endregion
        #region AvailableToPlayers
        private bool _AvailableToPlayers;
        [XmlSaveMode(XSME.Single)]
        public bool AvailableToPlayers
        {
            get => _AvailableToPlayers;
            set => SetAndNotify(ref _AvailableToPlayers, value);
        }
        #endregion

        // Commands
        #region AddRow
        public ICommand AddRow => new RelayCommand(param => DoAddRow());
        private void DoAddRow()
        {
            TableRows.Add(new RollTableRowModel());
        }
        #endregion
        #region RemoveTable
        public ICommand RemoveTable => new RelayCommand(param => DoRemoveTable());
        private void DoRemoveTable()
        {
            Configuration.MainModelRef.ToolsView.RollTables.Remove(this);
        }
        #endregion
        #region RollTable
        public ICommand RollTable => new RelayCommand(param => DoRollTable());
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
                NumberInputDialog numberInput = new(Name + " Table Modifier", ModifierInfo);
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
                HelperMethods.AddToGameplayLog(msg, "Default", true);
            }
            else
            {
                HelperMethods.NotifyUser("Result: " + roll + "\n" + msg);
            }

        }
        #endregion

    }
}
