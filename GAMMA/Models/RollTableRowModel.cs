using GAMMA.Toolbox;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class RollTableRowModel : BaseModel
    {
        // Constructor
        public RollTableRowModel()
        {

        }

        // Databound Properties
        #region LowValue
        private int _LowValue;
        [XmlSaveMode(XSME.Single)]
        public int LowValue
        {
            get => _LowValue;
            set => SetAndNotify(ref _LowValue, value);
        }
        #endregion
        #region HighValue
        private int _HighValue;
        [XmlSaveMode(XSME.Single)]
        public int HighValue
        {
            get => _HighValue;
            set => SetAndNotify(ref _HighValue, value);
        }
        #endregion
        #region Description
        private string _Description;
        [XmlSaveMode(XSME.Single)]
        public string Description
        {
            get => _Description;
            set => SetAndNotify(ref _Description, value);
        }
        #endregion

        // Commands
        #region RemoveRow
        public ICommand RemoveRow => new RelayCommand(param => DoRemoveRow());
        private void DoRemoveRow()
        {
            Configuration.MainModelRef.ToolsView.ActiveRollTable.TableRows.Remove(this);
        }
        #endregion

    }
}
