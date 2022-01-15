using System.Collections.ObjectModel;

namespace GAMMA.Models
{
    public class AudioCollectionModel : BaseModel
    {
        // Constructors
        public AudioCollectionModel()
        {
            AudioFiles = new();
        }

        // Databound Properties
        #region CollectionName
        private string _CollectionName;
        public string CollectionName
        {
            get => _CollectionName;
            set => SetAndNotify(ref _CollectionName, value);
        }
        #endregion
        #region AudioFiles
        private ObservableCollection<AudioFileModel> _AudioFiles;
        public ObservableCollection<AudioFileModel> AudioFiles
        {
            get => _AudioFiles;
            set => SetAndNotify(ref _AudioFiles, value);
        }
        #endregion
        #region IsExpanded
        private bool _IsExpanded;
        public bool IsExpanded
        {
            get => _IsExpanded;
            set => SetAndNotify(ref _IsExpanded, value);
        }
        #endregion

    }
}
