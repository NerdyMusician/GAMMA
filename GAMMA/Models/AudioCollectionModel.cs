using System.Collections.ObjectModel;

namespace GAMMA.Models
{
    public class AudioCollectionModel : BaseModel
    {
        // Constructors
        public AudioCollectionModel()
        {
            AudioFiles = new ObservableCollection<AudioFileModel>();
        }

        // Databound Properties
        #region CollectionName
        private string _CollectionName;
        public string CollectionName
        {
            get
            {
                return _CollectionName;
            }
            set
            {
                _CollectionName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AudioFiles
        private ObservableCollection<AudioFileModel> _AudioFiles;
        public ObservableCollection<AudioFileModel> AudioFiles
        {
            get
            {
                return _AudioFiles;
            }
            set
            {
                _AudioFiles = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsExpanded
        private bool _IsExpanded;
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                _IsExpanded = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }
}
