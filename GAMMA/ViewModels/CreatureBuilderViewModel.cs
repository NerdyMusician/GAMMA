using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class CreatureBuilderViewModel : BaseModel
    {
        // Constructors
        public CreatureBuilderViewModel()
        {
            XmlMethods.XmlToList(Configuration.CreatureDataFilePath, out List<CreatureModel> creatures, out _);
            AllCreatures = new ObservableCollection<CreatureModel>(creatures);
            FilteredCreatures = new ObservableCollection<CreatureModel>(AllCreatures.ToList());
            CreatureTypeFilters = new ObservableCollection<BoolOption>();
            SetFilterLists();
            Configuration.CreatureRepository = AllCreatures.ToList();
            CreatureSubCategories = new();
            UpdateSubCategories();
            UpdateArmorTypes();
            CreatureSearchText = "";
            foreach (CreatureModel creature in AllCreatures)
            {
                creature.ConnectSpellLinks();
                creature.ConnectItemLinks();
                creature.UpdateAbilityDropdowns();
                creature.UpdateAbilityDescriptions();
            }
        }

        // Databound Properties - Core
        #region AllCreatures
        private ObservableCollection<CreatureModel> _AllCreatures;
        public ObservableCollection<CreatureModel> AllCreatures
        {
            get
            {
                return _AllCreatures;
            }
            set
            {
                _AllCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredCreatures
        private ObservableCollection<CreatureModel> _FilteredCreatures;
        public ObservableCollection<CreatureModel> FilteredCreatures
        {
            get
            {
                return _FilteredCreatures;
            }
            set
            {
                _FilteredCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveCreature
        private CreatureModel _ActiveCreature;
        public CreatureModel ActiveCreature
        {
            get
            {
                return _ActiveCreature;
            }
            set
            {
                _ActiveCreature = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CreatureSearchText
        private string _CreatureSearchText;
        public string CreatureSearchText
        {
            get
            {
                return _CreatureSearchText;
            }
            set
            {
                _CreatureSearchText = value;
                NotifyPropertyChanged();
                UpdateFilteredCreatureList();
            }
        }
        #endregion

        #region CreatureSubCategories
        private List<string> _CreatureSubCategories;
        public List<string> CreatureSubCategories
        {
            get
            {
                return _CreatureSubCategories;
            }
            set
            {
                _CreatureSubCategories = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ArmorTypes
        private List<string> _ArmorTypes;
        public List<string> ArmorTypes
        {
            get
            {
                return _ArmorTypes;
            }
            set
            {
                _ArmorTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region IsFilterMenuOpen
        private bool _IsFilterMenuOpen;
        public bool IsFilterMenuOpen
        {
            get
            {
                return _IsFilterMenuOpen;
            }
            set
            {
                _IsFilterMenuOpen = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Filter_PlayersOnly
        private bool _Filter_PlayersOnly;
        public bool Filter_PlayersOnly
        {
            get
            {
                return _Filter_PlayersOnly;
            }
            set
            {
                _Filter_PlayersOnly = value;
                NotifyPropertyChanged();
                UpdateFilteredCreatureList();
            }
        }
        #endregion
        #region Filter_ValidatedOnly
        private bool _Filter_ValidatedOnly;
        public bool Filter_ValidatedOnly
        {
            get
            {
                return _Filter_ValidatedOnly;
            }
            set
            {
                _Filter_ValidatedOnly = value;
                NotifyPropertyChanged();
                UpdateFilteredCreatureList();
            }
        }
        #endregion
        #region Filter_MiniatureOnly
        private bool _Filter_MiniatureOnly;
        public bool Filter_MiniatureOnly
        {
            get
            {
                return _Filter_MiniatureOnly;
            }
            set
            {
                _Filter_MiniatureOnly = value;
                NotifyPropertyChanged();
                UpdateFilteredCreatureList();
            }
        }
        #endregion

        #region Count_AllCreatures
        private int _Count_AllCreatures;
        public int Count_AllCreatures
        {
            get
            {
                return _Count_AllCreatures;
            }
            set
            {
                _Count_AllCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Count_FilteredCreatures
        private int _Count_FilteredCreatures;
        public int Count_FilteredCreatures
        {
            get
            {
                return _Count_FilteredCreatures;
            }
            set
            {
                _Count_FilteredCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CreatureTypeFilters
        private ObservableCollection<BoolOption> _CreatureTypeFilters;
        public ObservableCollection<BoolOption> CreatureTypeFilters
        {
            get
            {
                return _CreatureTypeFilters;
            }
            set
            {
                _CreatureTypeFilters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddCreature
        private RelayCommand _AddCreature;
        public ICommand AddCreature
        {
            get
            {
                if (_AddCreature == null)
                {
                    _AddCreature = new RelayCommand(param => DoAddCreature());
                }
                return _AddCreature;
            }
        }
        private void DoAddCreature()
        {
            AllCreatures.Add(new CreatureModel());
            FilteredCreatures.Add(AllCreatures.Last());
            ActiveCreature = FilteredCreatures.Last();
            UpdateSubCategories();
            UpdateArmorTypes();
        }
        #endregion
        #region SaveCreatures
        private RelayCommand _SaveCreatures;
        public ICommand SaveCreatures
        {
            get
            {
                if (_SaveCreatures == null)
                {
                    _SaveCreatures = new RelayCommand(param => DoSaveCreatures());
                }
                return _SaveCreatures;
            }
        }
        public bool DoSaveCreatures(bool notifyUser = true)
        {
            // CheckCreatureLoot();
            if (AllCreatures.Count() == 0)
            {
                // Prevents zero creature save crash
                XDocument blankDoc = new();
                blankDoc.Add(new XElement("CreatureModelSet"));
                blankDoc.Save("Data/Creatures.xml");
                return true;
            }
            List<string> duplicateCreatures = new();
            foreach (CreatureModel creature in AllCreatures)
            {
                if (AllCreatures.Where(aItem => aItem.Name == creature.Name).Count() > 1)
                {
                    if (duplicateCreatures.Contains(creature.Name) == false) { duplicateCreatures.Add(creature.Name); }
                }
            }
            if (duplicateCreatures.Count() > 0)
            {
                string message = "Duplicate creatures found:\n";
                foreach (string item in duplicateCreatures)
                {
                    message += item + "\n";
                }
                HelperMethods.NotifyUser(message);
                return false;
            }
            XDocument itemDocument = new();
            itemDocument.Add(XmlMethods.ListToXml(AllCreatures.ToList()));
            itemDocument.Save("Data/Creatures.xml");
            Configuration.CreatureRepository = AllCreatures.ToList();
            HelperMethods.WriteToLogFile("Creatures Saved.", notifyUser);
            UpdateSubCategories();
            UpdateArmorTypes();
            return true;
        }
        #endregion
        #region SortCreatures
        private RelayCommand _SortCreatures;
        public ICommand SortCreatures
        {
            get
            {
                if (_SortCreatures == null)
                {
                    _SortCreatures = new RelayCommand(param => DoSortCreatures());
                }
                return _SortCreatures;
            }
        }
        private void DoSortCreatures()
        {
            AllCreatures = new ObservableCollection<CreatureModel>(AllCreatures.OrderBy(item => item.Name));
            FilteredCreatures = new ObservableCollection<CreatureModel>(FilteredCreatures.OrderBy(item => item.Name));
        }
        #endregion
        #region ImportCreatures
        private RelayCommand _ImportCreatures;
        public ICommand ImportCreatures
        {
            get
            {
                if (_ImportCreatures == null)
                {
                    _ImportCreatures = new RelayCommand(param => DoImportCreatures());
                }
                return _ImportCreatures;
            }
        }
        private void DoImportCreatures()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current creature list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            if (DoSaveCreatures() == false) { return; }

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Creatures(openWindow.FileName, out string message);
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion
        #region ClearCreatureSearch
        private RelayCommand _ClearCreatureSearch;
        public ICommand ClearCreatureSearch
        {
            get
            {
                if (_ClearCreatureSearch == null)
                {
                    _ClearCreatureSearch = new RelayCommand(param => DoClearCreatureSearch());
                }
                return _ClearCreatureSearch;
            }
        }
        private void DoClearCreatureSearch()
        {
            CreatureSearchText = "";
        }
        #endregion
        #region SelectFilters
        private RelayCommand _SelectFilters;
        public ICommand SelectFilters
        {
            get
            {
                if (_SelectFilters == null)
                {
                    _SelectFilters = new RelayCommand(DoSelectFilters);
                }
                return _SelectFilters;
            }
        }
        private void DoSelectFilters(object filter)
        {
            foreach (BoolOption option in CreatureTypeFilters)
            {
                option.Marked = (filter.ToString() == "All") ? true : false;
            }
        }
        #endregion
        #region PerformSelectiveExport
        public ICommand PerformSelectiveExport => new RelayCommand(param => DoPerformSelectiveExport());
        private void DoPerformSelectiveExport()
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.CreatureRepository, "Creatures");
            if (selectionDialog.ShowDialog() == true)
            {
                SaveFileDialog saveWindow = new()
                {
                    Filter = "XML Files (*.xml)|*.xml",
                    FileName = "Exported Creatures.xml",
                    InitialDirectory = Environment.CurrentDirectory
                };
                if (saveWindow.ShowDialog() == true)
                {
                    XDocument itemDocument = new();
                    itemDocument.Add(XmlMethods.ListToXml((selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCreatures));
                    itemDocument.Save(saveWindow.FileName);
                    YesNoDialog question = new("Creatures Exported\nOpen file explorer to file?");
                    if (question.ShowDialog() == true)
                    {
                        if (question.Answer == false) { return; }
                        string argument = "/select, \"" + saveWindow.FileName + "\"";
                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }
                }
            }
        }
        #endregion

        // Private Methods
        private void UpdateSubCategories()
        {
            List<string> newSubs = new();
            foreach (CreatureModel creature in AllCreatures)
            {
                if (newSubs.Contains(creature.CreatureSubCategory) == true) { continue; }
                newSubs.Add(creature.CreatureSubCategory);
            }
            newSubs.Sort();
            CreatureSubCategories = newSubs.ToList();
        }
        private void UpdateArmorTypes()
        {
            List<string> newTypes = new();
            foreach (CreatureModel creature in AllCreatures)
            {
                if (newTypes.Contains(creature.ArmorType) == true) { continue; }
                newTypes.Add(creature.ArmorType);
            }
            newTypes.Sort();
            ArmorTypes = newTypes.ToList();
        }
        private void UpdateFilteredCreatureList()
        {
            ObservableCollection<CreatureModel> filteredCreatures = new();
            foreach (CreatureModel creature in AllCreatures)
            {
                if (creature.Name.ToUpper().Contains(CreatureSearchText.ToUpper()) == false) { continue; }
                if (Filter_PlayersOnly == true && creature.IsPlayer == false) { continue; }
                if (Filter_ValidatedOnly == true && creature.IsValidated == false) { continue; }
                if (Filter_MiniatureOnly == true && creature.HasMiniature == false) { continue; }
                BoolOption filter = CreatureTypeFilters.FirstOrDefault(filter => filter.Name == creature.CreatureCategory);
                if (filter == null) { continue; }
                if (filter.Marked) { filteredCreatures.Add(creature); }
            }
            FilteredCreatures = new ObservableCollection<CreatureModel>(filteredCreatures.OrderBy(creature => creature.Name));

            Count_FilteredCreatures = FilteredCreatures.Count();
            Count_AllCreatures = AllCreatures.Count();
        }
        private void SetFilterLists()
        {
            foreach (string type in Configuration.CreatureCategories)
            {
                CreatureTypeFilters.Add(new BoolOption { Name = type, Marked = true });
                CreatureTypeFilters.Last().PropertyChanged += new PropertyChangedEventHandler(CreatureTypeFilter_PropertyChanged);
            }
        }
        private void CreatureTypeFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFilteredCreatureList();
        }

    }
}