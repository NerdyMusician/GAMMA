using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class SpellBuilderViewModel : BaseModel
    {
        // Constructors
        public SpellBuilderViewModel()
        {
            XmlMethods.XmlToList(Configuration.SpellDataFilePath, out List<SpellModel> spells);
            AllSpells = new ObservableCollection<SpellModel>(spells);
            
            foreach (SpellModel spell in AllSpells)
            {
                spell.UpdateAbilityDropdowns();
            }

            FilteredSpells = new ObservableCollection<SpellModel>(AllSpells.ToList());
            Configuration.SpellRepository = AllSpells.ToList();
            SpellSearchText = "";

        }

        // Databound Properties
        #region AllSpells
        private ObservableCollection<SpellModel> _AllSpells;
        public ObservableCollection<SpellModel> AllSpells
        {
            get
            {
                return _AllSpells;
            }
            set
            {
                _AllSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSpells
        private ObservableCollection<SpellModel> _FilteredSpells;
        public ObservableCollection<SpellModel> FilteredSpells
        {
            get
            {
                return _FilteredSpells;
            }
            set
            {
                _FilteredSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveSpell
        private SpellModel _ActiveSpell;
        public SpellModel ActiveSpell
        {
            get
            {
                return _ActiveSpell;
            }
            set
            {
                _ActiveSpell = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSearchText
        private string _SpellSearchText;
        public string SpellSearchText
        {
            get
            {
                return _SpellSearchText;
            }
            set
            {
                _SpellSearchText = value;
                NotifyPropertyChanged();
                UpdateSpellFilter();
            }
        }
        #endregion

        #region Count_AllSpells
        private int _Count_AllSpells;
        public int Count_AllSpells
        {
            get
            {
                return _Count_AllSpells;
            }
            set
            {
                _Count_AllSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Count_FilteredSpells
        private int _Count_FilteredSpells;
        public int Count_FilteredSpells
        {
            get
            {
                return _Count_FilteredSpells;
            }
            set
            {
                _Count_FilteredSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddSpell
        private RelayCommand _AddSpell;
        public ICommand AddSpell
        {
            get
            {
                if (_AddSpell == null)
                {
                    _AddSpell = new RelayCommand(param => DoAddSpell());
                }
                return _AddSpell;
            }
        }
        private void DoAddSpell()
        {
            AllSpells.Add(new SpellModel());
            FilteredSpells.Add(AllSpells.Last());
            ActiveSpell = FilteredSpells.Last();
        }
        #endregion
        #region SaveSpells
        private RelayCommand _SaveSpells;
        public ICommand SaveSpells
        {
            get
            {
                if (_SaveSpells == null)
                {
                    _SaveSpells = new RelayCommand(param => DoSaveSpells());
                }
                return _SaveSpells;
            }
        }
        public bool DoSaveSpells(bool notifyUser = true)
        {
            if (AllSpells.Count() == 0)
            {
                // Prevents zero spell save crash
                XDocument blankDoc = new XDocument();
                blankDoc.Add(new XElement("SpellModelSet"));
                blankDoc.Save("Data/Spells.xml");
                return true;
            }
            List<string> duplicateSpells = new();
            foreach (SpellModel spell in AllSpells)
            {
                if (AllSpells.Where(aItem => aItem.Name == spell.Name).Count() > 1)
                {
                    if (duplicateSpells.Contains(spell.Name) == false) { duplicateSpells.Add(spell.Name); }
                }
            }
            if (duplicateSpells.Count() > 0)
            {
                string message = "Duplicate spells found:\n";
                foreach (string item in duplicateSpells)
                {
                    message += item + "\n";
                }
                new NotificationDialog(message).ShowDialog();
                return false;
            }
            XDocument itemDocument = new XDocument();
            itemDocument.Add(XmlMethods.ListToXml(AllSpells.ToList()));
            itemDocument.Save("Data/Spells.xml");
            Configuration.SpellRepository = AllSpells.ToList();
            HelperMethods.WriteToLogFile("Spells Saved.", notifyUser);

            return true;
        }
        #endregion
        #region SortSpells
        private RelayCommand _SortSpells;
        public ICommand SortSpells
        {
            get
            {
                if (_SortSpells == null)
                {
                    _SortSpells = new RelayCommand(param => DoSortSpells());
                }
                return _SortSpells;
            }
        }
        private void DoSortSpells()
        {
            AllSpells = new ObservableCollection<SpellModel>(AllSpells.OrderBy(item => item.Name));
            FilteredSpells = new ObservableCollection<SpellModel>(FilteredSpells.OrderBy(item => item.Name));
        }
        #endregion
        #region ImportSpells
        private RelayCommand _ImportSpells;
        public ICommand ImportSpells
        {
            get
            {
                if (_ImportSpells == null)
                {
                    _ImportSpells = new RelayCommand(param => DoImportSpells());
                }
                return _ImportSpells;
            }
        }
        private void DoImportSpells()
        {
            OpenFileDialog openWindow = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new YesNoDialog("Prior to import, the current spell list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            if (DoSaveSpells() == false) { return; }

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Spells(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion
        #region ClearSpellSearch
        private RelayCommand _ClearSpellSearch;
        public ICommand ClearSpellSearch
        {
            get
            {
                if (_ClearSpellSearch == null)
                {
                    _ClearSpellSearch = new RelayCommand(param => DoClearSpellSearch());
                }
                return _ClearSpellSearch;
            }
        }
        private void DoClearSpellSearch()
        {
            SpellSearchText = "";
        }
        #endregion

        // Private Methods
        private void UpdateSpellFilter()
        {
            ObservableCollection<SpellModel> filteredSpells = new ObservableCollection<SpellModel>();
            foreach (SpellModel spell in AllSpells)
            {
                if (spell.Name.ToUpper().Contains(SpellSearchText.ToUpper()) == false) { continue; }
                filteredSpells.Add(spell);
            }
            FilteredSpells = new ObservableCollection<SpellModel>(filteredSpells.OrderBy(spell => spell.Name));

            Count_FilteredSpells = FilteredSpells.Count();
            Count_AllSpells = AllSpells.Count();

        }

    }
}
