using GAMMA.Models;
using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.ViewModels
{
    public class AudioViewModel : BaseModel
    {
        // Constructors
        public AudioViewModel()
        {
            MusicVolumeInt = 100;
            SfxVolumeInt = 100;
            SystemVolumeInt = 100;
            MusicVolumeDec = 1;
            SfxVolumeDec = 1;
            SystemVolumeDec = 1;
            MusicFiles = new ObservableCollection<AudioCollectionModel>();
            SfxFiles = new ObservableCollection<AudioCollectionModel>();
            GetMusicFiles();
            GetSfxFiles();
        }

        // Databound Properties
        #region SfxSource
        private Uri _SfxSource;
        public Uri SfxSource
        {
            get
            {
                return _SfxSource;
            }
            set
            {
                _SfxSource = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MusicSource
        private Uri _MusicSource;
        public Uri MusicSource
        {
            get
            {
                return _MusicSource;
            }
            set
            {
                _MusicSource = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SystemAudioSource
        private Uri _SystemAudioSource;
        public Uri SystemAudioSource
        {
            get
            {
                return _SystemAudioSource;
            }
            set
            {
                _SystemAudioSource = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MusicVolumeDec
        private double _MusicVolumeDec;
        public double MusicVolumeDec
        {
            get
            {
                return _MusicVolumeDec;
            }
            set
            {
                _MusicVolumeDec = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MusicVolumeInt
        private int _MusicVolumeInt;
        public int MusicVolumeInt
        {
            get
            {
                return _MusicVolumeInt;
            }
            set
            {
                _MusicVolumeInt = value;
                NotifyPropertyChanged();
                MusicVolumeDec = Convert.ToDouble(value) / 100;
            }
        }
        #endregion
        #region SfxVolumeDec
        private double _SfxVolumeDec;
        public double SfxVolumeDec
        {
            get
            {
                return _SfxVolumeDec;
            }
            set
            {
                _SfxVolumeDec = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SfxVolumeInt
        private int _SfxVolumeInt;
        public int SfxVolumeInt
        {
            get
            {
                return _SfxVolumeInt;
            }
            set
            {
                _SfxVolumeInt = value;
                NotifyPropertyChanged();
                SfxVolumeDec = Convert.ToDouble(value) / 100;
            }
        }
        #endregion
        #region SystemVolumeDec
        private double _SystemVolumeDec;
        public double SystemVolumeDec
        {
            get
            {
                return _SystemVolumeDec;
            }
            set
            {
                _SystemVolumeDec = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SystemVolumeInt
        private int _SystemVolumeInt;
        public int SystemVolumeInt
        {
            get
            {
                return _SystemVolumeInt;
            }
            set
            {
                _SystemVolumeInt = value;
                NotifyPropertyChanged();
                SystemVolumeDec = Convert.ToDouble(value) / 100;
            }
        }
        #endregion
        #region MusicFiles
        private ObservableCollection<AudioCollectionModel> _MusicFiles;
        public ObservableCollection<AudioCollectionModel> MusicFiles
        {
            get
            {
                return _MusicFiles;
            }
            set
            {
                _MusicFiles = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SfxFiles
        private ObservableCollection<AudioCollectionModel> _SfxFiles;
        public ObservableCollection<AudioCollectionModel> SfxFiles
        {
            get
            {
                return _SfxFiles;
            }
            set
            {
                _SfxFiles = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NowPlaying_Music
        private string _NowPlaying_Music;
        public string NowPlaying_Music
        {
            get
            {
                return _NowPlaying_Music;
            }
            set
            {
                _NowPlaying_Music = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NowPlaying_Sfx
        private string _NowPlaying_Sfx;
        public string NowPlaying_Sfx
        {
            get
            {
                return _NowPlaying_Sfx;
            }
            set
            {
                _NowPlaying_Sfx = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region ChangeMusic
        private RelayCommand _ChangeMusic;
        public ICommand ChangeMusic
        {
            get
            {
                if (_ChangeMusic == null)
                {
                    _ChangeMusic = new RelayCommand(DoChangeMusic);
                }
                return _ChangeMusic;
            }
        }
        public void DoChangeMusic(object fileName)
        {
            MusicSource = new Uri(fileName.ToString(), UriKind.Absolute);
        }
        #endregion
        #region ChangeSfx
        private RelayCommand _ChangeSfx;
        public ICommand ChangeSfx
        {
            get
            {
                if (_ChangeSfx == null)
                {
                    _ChangeSfx = new RelayCommand(DoChangeSfx);
                }
                return _ChangeSfx;
            }
        }
        public void DoChangeSfx(object fileName)
        {
            SfxSource = new Uri(fileName.ToString(), UriKind.Absolute);
        }
        #endregion
        #region ChangeSystemAudio
        private RelayCommand _ChangeSystemAudio;
        public ICommand ChangeSystemAudio
        {
            get
            {
                if (_ChangeSystemAudio == null)
                {
                    _ChangeSystemAudio = new RelayCommand(DoChangeSystemAudio);
                }
                return _ChangeSystemAudio;
            }
        }
        public void DoChangeSystemAudio(object fileName)
        {
            SystemAudioSource = new Uri(fileName.ToString(), UriKind.Absolute);
        }
        #endregion

        // Private Methods
        private void GetMusicFiles()
        {
            string[] musicFolders = Directory.GetDirectories(Environment.CurrentDirectory + "/Audio/Music");
            foreach (string folder in musicFolders)
            {
                MusicFiles.Add(new AudioCollectionModel());
                MusicFiles.Last().CollectionName = folder.Split('\\').Last();
                string[] audioFiles = Directory.GetFiles(folder);
                foreach (string audioFile in audioFiles)
                {
                    string fileType = audioFile.Split('.').Last();
                    if (fileType == "mp3" || fileType == "wav")
                    {
                        MusicFiles.Last().AudioFiles.Add(new AudioFileModel(audioFile));
                    }
                }
            }
        }
        private void GetSfxFiles()
        {
            string[] sfxFolders = Directory.GetDirectories(Environment.CurrentDirectory + "/Audio/Sfx");
            foreach (string folder in sfxFolders)
            {
                SfxFiles.Add(new AudioCollectionModel());
                SfxFiles.Last().CollectionName = folder.Split('\\').Last();
                string[] audioFiles = Directory.GetFiles(folder);
                foreach (string audioFile in audioFiles)
                {
                    string fileType = audioFile.Split('.').Last();
                    if (fileType == "mp3" || fileType == "wav")
                    {
                        SfxFiles.Last().AudioFiles.Add(new AudioFileModel(audioFile));
                    }
                }
            }
        }

    }
}
