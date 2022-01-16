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
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region FullPath
        private string _FullPath;
        [XmlSaveMode(XSME.Single)]
        public string FullPath
        {
            get => _FullPath;
            set => SetAndNotify(ref _FullPath, value);
        }
        #endregion
        #region IsAvailable
        private bool _IsAvailable;
        [XmlSaveMode(XSME.Single)]
        public bool IsAvailable
        {
            get => _IsAvailable;
            set => SetAndNotify(ref _IsAvailable, value);
        }
        #endregion
        #region Tags
        private ObservableCollection<ConvertibleValue> _Tags;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<ConvertibleValue> Tags
        {
            get => _Tags;
            set => SetAndNotify(ref _Tags, value);
        }
        #endregion

        // Commands
        #region PlayMusic
        public ICommand PlayMusic => new RelayCommand(DoPlayMusic);
        private void DoPlayMusic(object param)
        {
            Configuration.MainModelRef.AudioView.DoChangeMusic(FullPath);
            Configuration.MainModelRef.AudioView.NowPlaying_Music = Name;
        }
        #endregion
        #region PlaySfx
        public ICommand PlaySfx => new RelayCommand(DoPlaySfx);
        private void DoPlaySfx(object param)
        {
            Configuration.MainModelRef.AudioView.DoChangeSfx(FullPath);
            Configuration.MainModelRef.AudioView.NowPlaying_Sfx = Name;
        }
        #endregion

    }
}
