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
        [XmlSaveMode(XSME.Single)]
        public string SubclassOf
        {
            get => _SubclassOf;
            set => SetAndNotify(ref _SubclassOf, value);
        }
        #endregion
        #region ClassList
        private List<string> _ClassList;
        public List<string> ClassList
        {
            get => _ClassList;
            set => SetAndNotify(ref _ClassList, value);
        }
        #endregion
        #region Traits
        private ObservableCollection<TraitModel> _Traits;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<TraitModel> Traits
        {
            get => _Traits;
            set => SetAndNotify(ref _Traits, value);
        }
        #endregion
        #region SpellcastingClasses
        private List<string> _SpellcastingClasses;
        public List<string> SpellcastingClasses
        {
            get => _SpellcastingClasses;
            set => SetAndNotify(ref _SpellcastingClasses, value);
        }
        #endregion
        #region SpellcastingClass
        private string _SpellcastingClass;
        [XmlSaveMode(XSME.Single)]
        public string SpellcastingClass
        {
            get => _SpellcastingClass;
            set => SetAndNotify(ref _SpellcastingClass, value);
        }
        #endregion

        // Commands
        #region RemoveSubclass
        public ICommand RemoveSubclass => new RelayCommand(param => DoRemoveSubclass());
        private void DoRemoveSubclass()
        {
            Configuration.MainModelRef.ToolsView.PlayerSubclasses.Remove(this);
        }
        #endregion
        #region AddTrait
        private RelayCommand _AddTrait;
        public ICommand AddTrait => new RelayCommand(param => DoAddTrait());
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
