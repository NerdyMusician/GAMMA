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
        [XmlSaveMode("Single")]
        public int LowValue
        {
            get
            {
                return _LowValue;
            }
            set
            {
                _LowValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region HighValue
        private int _HighValue;
        [XmlSaveMode("Single")]
        public int HighValue
        {
            get
            {
                return _HighValue;
            }
            set
            {
                _HighValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Description
        private string _Description;
        [XmlSaveMode("Single")]
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

        // Commands
        #region RemoveRow
        private RelayCommand _RemoveRow;
        public ICommand RemoveRow
        {
            get
            {
                if (_RemoveRow == null)
                {
                    _RemoveRow = new RelayCommand(param => DoRemoveRow());
                }
                return _RemoveRow;
            }
        }
        private void DoRemoveRow()
        {
            Configuration.MainModelRef.ToolsView.ActiveRollTable.TableRows.Remove(this);
        }
        #endregion

    }
}
