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
            get => _SfxSource;
            set => SetAndNotify(ref _SfxSource, value);
        }
        #endregion
        #region MusicSource
        private Uri _MusicSource;
        public Uri MusicSource
        {
            get => _MusicSource;
            set => SetAndNotify(ref _MusicSource, value);
        }
        #endregion
        #region SystemAudioSource
        private Uri _SystemAudioSource;
        public Uri SystemAudioSource
        {
            get => _SystemAudioSource;
            set => SetAndNotify(ref _SystemAudioSource, value);
        }
        #endregion
        #region MusicVolumeDec
        private double _MusicVolumeDec;
        public double MusicVolumeDec
        {
            get => _MusicVolumeDec;
            set => SetAndNotify(ref _MusicVolumeDec, value);
        }
        #endregion
        #region MusicVolumeInt
        private int _MusicVolumeInt;
        public int MusicVolumeInt
        {
            get => _MusicVolumeInt;
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
            get => _SfxVolumeDec;
            set => SetAndNotify(ref _SfxVolumeDec, value);
        }
        #endregion
        #region SfxVolumeInt
        private int _SfxVolumeInt;
        public int SfxVolumeInt
        {
            get => _SfxVolumeInt;
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
            get => _SystemVolumeDec;
            set => SetAndNotify(ref _SystemVolumeDec, value);
        }
        #endregion
        #region SystemVolumeInt
        private int _SystemVolumeInt;
        public int SystemVolumeInt
        {
            get => _SystemVolumeInt;
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
            get => _MusicFiles;
            set => SetAndNotify(ref _MusicFiles, value);
        }
        #endregion
        #region SfxFiles
        private ObservableCollection<AudioCollectionModel> _SfxFiles;
        public ObservableCollection<AudioCollectionModel> SfxFiles
        {
            get => _SfxFiles;
            set => SetAndNotify(ref _SfxFiles, value);
        }
        #endregion
        #region NowPlaying_Music
        private string _NowPlaying_Music;
        public string NowPlaying_Music
        {
            get => _NowPlaying_Music;
            set => SetAndNotify(ref _NowPlaying_Music, value);
        }
        #endregion
        #region NowPlaying_Sfx
        private string _NowPlaying_Sfx;
        public string NowPlaying_Sfx
        {
            get => _NowPlaying_Sfx;
            set => SetAndNotify(ref _NowPlaying_Sfx, value);
        }
        #endregion

        // Commands
        #region ChangeMusic
        public ICommand ChangeMusic => new RelayCommand(DoChangeMusic);
        public void DoChangeMusic(object fileName)
        {
            MusicSource = new Uri(fileName.ToString(), UriKind.Absolute);
        }
        #endregion
        #region ChangeSfx
        public ICommand ChangeSfx => new RelayCommand(DoChangeSfx);
        public void DoChangeSfx(object fileName)
        {
            SfxSource = new Uri(fileName.ToString(), UriKind.Absolute);
        }
        #endregion
        #region ChangeSystemAudio
        public ICommand ChangeSystemAudio => new RelayCommand(DoChangeSystemAudio);
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
