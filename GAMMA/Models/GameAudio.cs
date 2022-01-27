using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;
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
            get => _DisplayName;
            set => SetAndNotify(ref _DisplayName, value);
        }
        #endregion
        #region FilePath
        public string _FilePath;
        [XmlSaveMode(XSME.Single)]
        public string FilePath
        {
            get => _FilePath;
            set => SetAndNotify(ref _FilePath, value);
        }
        #endregion
        #region SearchTags
        private ObservableCollection<ConvertibleValue> _SearchTags;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> SearchTags
        {
            get => _SearchTags;
            set => SetAndNotify(ref _SearchTags, value);
        }
        #endregion

        // Commands
        #region AddSearchTag
        public ICommand AddSearchTag => new RelayCommand(DoAddSearchTag);
        private void DoAddSearchTag(object param)
        {
            // TODO
        }
        #endregion

    }
}
