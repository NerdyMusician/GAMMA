using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class AudioFileModel : BaseModel
    {
        // Constructors
        public AudioFileModel(string filePath)
        {
            FullPath = filePath;
            Name = filePath.Split('\\').Last().Split('.')[0];
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
        #region FullPath
        private string _FullPath;
        [XmlSaveMode("Single")]
        public string FullPath
        {
            get
            {
                return _FullPath;
            }
            set
            {
                _FullPath = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsAvailable
        private bool _IsAvailable;
        [XmlSaveMode("Single")]
        public bool IsAvailable
        {
            get
            {
                return _IsAvailable;
            }
            set
            {
                _IsAvailable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Tags
        private ObservableCollection<ConvertibleValue> _Tags;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<ConvertibleValue> Tags
        {
            get
            {
                return _Tags;
            }
            set
            {
                _Tags = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region PlayMusic
        private RelayCommand _PlayMusic;
        public ICommand PlayMusic
        {
            get
            {
                if (_PlayMusic == null)
                {
                    _PlayMusic = new RelayCommand(param => DoPlayMusic());
                }
                return _PlayMusic;
            }
        }
        private void DoPlayMusic()
        {
            Configuration.MainModelRef.AudioView.DoChangeMusic(FullPath);
            Configuration.MainModelRef.AudioView.NowPlaying_Music = Name;
        }
        #endregion
        #region PlaySfx
        private RelayCommand _PlaySfx;
        public ICommand PlaySfx
        {
            get
            {
                if (_PlaySfx == null)
                {
                    _PlaySfx = new RelayCommand(param => DoPlaySfx());
                }
                return _PlaySfx;
            }
        }
        private void DoPlaySfx()
        {
            Configuration.MainModelRef.AudioView.DoChangeSfx(FullPath);
            Configuration.MainModelRef.AudioView.NowPlaying_Sfx = Name;
        }
        #endregion

    }
}
