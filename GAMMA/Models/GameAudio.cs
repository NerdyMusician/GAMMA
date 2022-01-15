using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class GameAudio : BaseModel
    {
        // Constructors
        public GameAudio()
        {
            SearchTags = new();
        }

        // Databound Properties
        #region DisplayName
        private string _DisplayName;
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilePath
        public string _FilePath;
        [XmlSaveMode(XSME.Single)]
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                _FilePath = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SearchTags
        private ObservableCollection<ConvertibleValue> _SearchTags;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> SearchTags
        {
            get
            {
                return _SearchTags;
            }
            set
            {
                _SearchTags = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddSearchTag
        public ICommand AddSearchTag => new RelayCommand(DoAddSearchTag);
        private void DoAddSearchTag(object param)
        {

        }
        #endregion

    }
}
