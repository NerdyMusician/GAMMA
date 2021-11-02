using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class PlayerSubclassModel : PlayerBuildingBlock
    {
        // Constructors
        public PlayerSubclassModel()
        {
            Name = "New Player Subclass";
            ClassList = Configuration.MainModelRef.PlayerClasses;
            Traits = new();
            SpellcastingClasses = Configuration.MainModelRef.SpellcastingClasses;
        }

        // Databound Properties
        #region SubclassOf
        private string _SubclassOf;
        [XmlSaveMode("Single")]
        public string SubclassOf
        {
            get
            {
                return _SubclassOf;
            }
            set
            {
                _SubclassOf = value;
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
        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<TraitModel> Traits
        {
            get
            {
                return _Traits;
            }
            set
            {
                _Traits = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellcastingClasses
        private List<string> _SpellcastingClasses;
        public List<string> SpellcastingClasses
        {
            get
            {
                return _SpellcastingClasses;
            }
            set
            {
                _SpellcastingClasses = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellcastingClass
        private string _SpellcastingClass;
        [XmlSaveMode("Single")]
        public string SpellcastingClass
        {
            get
            {
                return _SpellcastingClass;
            }
            set
            {
                _SpellcastingClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveSubclass
        private RelayCommand _RemoveSubclass;
        public ICommand RemoveSubclass
        {
            get
            {
                if (_RemoveSubclass == null)
                {
                    _RemoveSubclass = new RelayCommand(param => DoRemoveSubclass());
                }
                return _RemoveSubclass;
            }
        }
        private void DoRemoveSubclass()
        {
            Configuration.MainModelRef.ToolsView.PlayerSubclasses.Remove(this);
        }
        #endregion
        #region AddTrait
        private RelayCommand _AddTrait;
        public ICommand AddTrait
        {
            get
            {
                if (_AddTrait == null)
                {
                    _AddTrait = new RelayCommand(param => DoAddTrait());
                }
                return _AddTrait;
            }
        }
        private void DoAddTrait()
        {
            Traits.Add(new TraitModel());
        }
        #endregion
        #region Duplicate
        public ICommand Duplicate => new RelayCommand(param => DoDuplicate());
        private void DoDuplicate()
        {
            PlayerSubclassModel duplicate = HelperMethods.DeepClone(this);
            duplicate.Name = "Copy of " + duplicate.Name;
            duplicate.IsValidated = false;
            Configuration.MainModelRef.ToolsView.PlayerSubclasses.Insert(Configuration.MainModelRef.ToolsView.PlayerSubclasses.IndexOf(this), duplicate);
            Configuration.MainModelRef.ToolsView.ActivePlayerSubclass = duplicate;
        }
        #endregion

    }
}
