using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerClassLinkModel : BaseModel
    {
        // Constructors
        public PlayerClassLinkModel()
        {
            ClassList = Configuration.MainModelRef.PlayerClasses.ToList();
            SubclassList = new();
            ClassLevels = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            ClassLevel = 1;
        }

        // Databound Properties
        #region ClassName
        private string _ClassName;
        [XmlSaveMode(XSME.Single)]
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                _ClassName = value;
                NotifyPropertyChanged();
                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdateClassTotals();
                UpdateSubclassList();
            }
        }
        #endregion
        #region SubClassName
        private string _SubClassName;
        [XmlSaveMode(XSME.Single)]
        public string SubClassName
        {
            get
            {
                return _SubClassName;
            }
            set
            {
                _SubClassName = value;
                NotifyPropertyChanged();
                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdateClassTotals();
            }
        }
        #endregion
        #region ClassLevel
        private int _ClassLevel;
        [XmlSaveMode(XSME.Single)]
        public int ClassLevel
        {
            get
            {
                return _ClassLevel;
            }
            set
            {
                _ClassLevel = value;
                NotifyPropertyChanged();
                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                if (Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter == null) { return; }
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdateClassTotals();
            }
        }
        #endregion
        #region ClassLevels
        private List<int> _ClassLevels;
        public List<int> ClassLevels
        {
            get
            {
                return _ClassLevels;
            }
            set
            {
                _ClassLevels = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ClassList
        private List<string> _ClassList;
        public List<string> ClassList
        {
            get
            {
                return _ClassList;
            }
            set
            {
                _ClassList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SubclassList
        private List<string> _SubclassList;
        public List<string> SubclassList
        {
            get
            {
                return _SubclassList;
            }
            set
            {
                _SubclassList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveClassLink
        private RelayCommand _RemoveClassLink;
        public ICommand RemoveClassLink
        {
            get
            {
                if (_RemoveClassLink == null)
                {
                    _RemoveClassLink = new RelayCommand(param => DoRemoveClassLink());
                }
                return _RemoveClassLink;
            }
        }
        private void DoRemoveClassLink()
        {
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.PlayerClasses.Remove(this);
            Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.UpdateClassTotals();
        }
        #endregion

        // Public Methods
        public void UpdateSubclassList()
        {
            List<string> newSubList = new();
            foreach (PlayerSubclassModel subclass in Configuration.MainModelRef.ToolsView.PlayerSubclasses)
            {
                if (subclass.SubclassOf != ClassName || subclass.IsValidated == false) { continue; }
                newSubList.Add(subclass.Name);
            }
            SubclassList = new List<string>(newSubList);
        }

    }
}
