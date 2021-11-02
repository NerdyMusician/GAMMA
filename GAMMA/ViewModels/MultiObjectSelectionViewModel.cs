using GAMMA.Models;
using GAMMA.Toolbox;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.ViewModels
{
    public class MultiObjectSelectionViewModel : BaseModel
    {
        // Constructors
        public MultiObjectSelectionViewModel(List<CreatureModel> creatures, string mode)
        {
            SourceCreatures = new ObservableCollection<CreatureModel>(creatures);
            FilteredSourceCreatures = new ObservableCollection<CreatureModel>();
            SelectedCreatures = new ObservableCollection<CreatureModel>();
            Mode = mode; // Potential modes: Creatures, Players
            Filters = new ObservableCollection<BoolOption>();
            SecondaryFilters = new();
            foreach (string type in Configuration.CreatureCategories)
            {
                Filters.Add(new BoolOption { Name = type, Marked = true });
                Filters.Last().PropertyChanged += new PropertyChangedEventHandler(Filter_PropertyChanged);
            }
            SourceTextSearch = "";
        }
        public MultiObjectSelectionViewModel(List<ItemModel> items)
        {
            SourceItems = new ObservableCollection<ItemModel>(items);
            FilteredSourceItems = new ObservableCollection<ItemModel>();
            SelectedItems = new ObservableCollection<ItemModel>();
            Mode = "Items";
            Filters = new ObservableCollection<BoolOption>();
            SecondaryFilters = new();
            foreach (string type in Configuration.ItemTypes)
            {
                Filters.Add(new BoolOption { Name = type, Marked = true });
                Filters.Last().PropertyChanged += new PropertyChangedEventHandler(Filter_PropertyChanged);
            }
            SourceTextSearch = "";
        }
        public MultiObjectSelectionViewModel(List<SpellModel> spells)
        {
            SourceSpells = new ObservableCollection<SpellModel>(spells);
            FilteredSourceSpells = new ObservableCollection<SpellModel>();
            SelectedSpells = new ObservableCollection<SpellModel>();
            Mode = "Spells";
            Filters = new ObservableCollection<BoolOption>();
            SecondaryFilters = new();
            foreach (string type in Configuration.SchoolsOfMagic)
            {
                Filters.Add(new BoolOption { Name = type, Marked = true });
                Filters.Last().PropertyChanged += new PropertyChangedEventHandler(Filter_PropertyChanged);
            }
            for (int i = 0; i < 10; i++)
            {
                SecondaryFilters.Add(new BoolOption { Name = "Level " + i, Marked = true });
                SecondaryFilters.Last().PropertyChanged += new PropertyChangedEventHandler(Filter_PropertyChanged);
            }
            SourceTextSearch = "";
        }
        public MultiObjectSelectionViewModel(List<NpcModel> npcs)
        {
            SourceNpcs = new ObservableCollection<NpcModel>(npcs);
            FilteredSourceNpcs = new ObservableCollection<NpcModel>();
            SelectedNpcs = new ObservableCollection<NpcModel>();
            Mode = "Npcs";
            Filters = new ObservableCollection<BoolOption>();
            SecondaryFilters = new();
            SourceTextSearch = "";
        }
        public MultiObjectSelectionViewModel(List<ConvertibleValue> cvs, string mode)
        {
            SourceCVs = new(cvs);
            FilteredSourceCVs = new();
            SelectedCVs = new();
            Mode = mode;
            Filters = new();
            SecondaryFilters = new();
            SourceTextSearch = "";
        }

        // Databound Properties
        #region Mode
        private string _Mode;
        public string Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                _Mode = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowFilters
        private bool _ShowFilters;
        public bool ShowFilters
        {
            get
            {
                return _ShowFilters;
            }
            set
            {
                _ShowFilters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Filters
        private ObservableCollection<BoolOption> _Filters;
        public ObservableCollection<BoolOption> Filters
        {
            get
            {
                return _Filters;
            }
            set
            {
                _Filters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SecondaryFilters
        private ObservableCollection<BoolOption> _SecondaryFilters;
        public ObservableCollection<BoolOption> SecondaryFilters
        {
            get
            {
                return _SecondaryFilters;
            }
            set
            {
                _SecondaryFilters = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SourceCreatures
        private ObservableCollection<CreatureModel> _SourceCreatures;
        public ObservableCollection<CreatureModel> SourceCreatures
        {
            get
            {
                return _SourceCreatures;
            }
            set
            {
                _SourceCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSourceCreatures
        private ObservableCollection<CreatureModel> _FilteredSourceCreatures;
        public ObservableCollection<CreatureModel> FilteredSourceCreatures
        {
            get
            {
                return _FilteredSourceCreatures;
            }
            set
            {
                _FilteredSourceCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SelectedCreatures
        private ObservableCollection<CreatureModel> _SelectedCreatures;
        public ObservableCollection<CreatureModel> SelectedCreatures
        {
            get
            {
                return _SelectedCreatures;
            }
            set
            {
                _SelectedCreatures = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SourceItems
        private ObservableCollection<ItemModel> _SourceItems;
        public ObservableCollection<ItemModel> SourceItems
        {
            get
            {
                return _SourceItems;
            }
            set
            {
                _SourceItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSourceItems
        private ObservableCollection<ItemModel> _FilteredSourceItems;
        public ObservableCollection<ItemModel> FilteredSourceItems
        {
            get
            {
                return _FilteredSourceItems;
            }
            set
            {
                _FilteredSourceItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SelectedItems
        private ObservableCollection<ItemModel> _SelectedItems;
        public ObservableCollection<ItemModel> SelectedItems
        {
            get
            {
                return _SelectedItems;
            }
            set
            {
                _SelectedItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SourceSpells
        private ObservableCollection<SpellModel> _SourceSpells;
        public ObservableCollection<SpellModel> SourceSpells
        {
            get
            {
                return _SourceSpells;
            }
            set
            {
                _SourceSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSourceSpells
        private ObservableCollection<SpellModel> _FilteredSourceSpells;
        public ObservableCollection<SpellModel> FilteredSourceSpells
        {
            get
            {
                return _FilteredSourceSpells;
            }
            set
            {
                _FilteredSourceSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SelectedSpells
        private ObservableCollection<SpellModel> _SelectedSpells;
        public ObservableCollection<SpellModel> SelectedSpells
        {
            get
            {
                return _SelectedSpells;
            }
            set
            {
                _SelectedSpells = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SourceNpcs
        private ObservableCollection<NpcModel> _SourceNpcs;
        public ObservableCollection<NpcModel> SourceNpcs
        {
            get
            {
                return _SourceNpcs;
            }
            set
            {
                _SourceNpcs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSourceNpcs
        private ObservableCollection<NpcModel> _FilteredSourceNpcs;
        public ObservableCollection<NpcModel> FilteredSourceNpcs
        {
            get
            {
                return _FilteredSourceNpcs;
            }
            set
            {
                _FilteredSourceNpcs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SelectedNpcs
        private ObservableCollection<NpcModel> _SelectedNpcs;
        public ObservableCollection<NpcModel> SelectedNpcs
        {
            get
            {
                return _SelectedNpcs;
            }
            set
            {
                _SelectedNpcs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SourceCVs
        private ObservableCollection<ConvertibleValue> _SourceCVs;
        public ObservableCollection<ConvertibleValue> SourceCVs
        {
            get
            {
                return _SourceCVs;
            }
            set
            {
                _SourceCVs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FilteredSourceCVs
        private ObservableCollection<ConvertibleValue> _FilteredSourceCVs;
        public ObservableCollection<ConvertibleValue> FilteredSourceCVs
        {
            get
            {
                return _FilteredSourceCVs;
            }
            set
            {
                _FilteredSourceCVs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SelectedCVs
        private ObservableCollection<ConvertibleValue> _SelectedCVs;
        public ObservableCollection<ConvertibleValue> SelectedCVs
        {
            get
            {
                return _SelectedCVs;
            }
            set
            {
                _SelectedCVs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SourceTextSearch
        private string _SourceTextSearch;
        public string SourceTextSearch
        {
            get
            {
                return _SourceTextSearch;
            }
            set
            {
                _SourceTextSearch = value;
                NotifyPropertyChanged();
                UpdateFilteredList();
            }
        }
        #endregion
        #region Count_SourceAll
        private int _Count_SourceAll;
        public int Count_SourceAll
        {
            get
            {
                return _Count_SourceAll;
            }
            set
            {
                _Count_SourceAll = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Count_SourceFiltered
        private int _Count_SourceFiltered;
        public int Count_SourceFiltered
        {
            get
            {
                return _Count_SourceFiltered;
            }
            set
            {
                _Count_SourceFiltered = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
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
            foreach (BoolOption option in Filters)
            {
                option.Marked = (filter.ToString() == "All") ? true : false;
            }
            foreach (BoolOption option in SecondaryFilters)
            {
                option.Marked = (filter.ToString() == "All") ? true : false;
            }
        }
        #endregion

        // Private Methods
        private void UpdateFilteredList()
        {
            switch (Mode)
            {
                case "Items":
                    FilteredSourceItems.Clear();
                    foreach (ItemModel item in SourceItems)
                    {
                        if (item.Name.ToUpper().Contains(SourceTextSearch.ToUpper()) == false) { continue; }
                        BoolOption option = Filters.FirstOrDefault(filter => filter.Name == item.Type);
                        if (option == null) { continue; }
                        if (option.Marked) { FilteredSourceItems.Add(item); }
                    }

                    Count_SourceFiltered = FilteredSourceItems.Count();
                    Count_SourceAll = SourceItems.Count();

                    break;
                case "Spells":
                    FilteredSourceSpells.Clear();
                    foreach (SpellModel spell in SourceSpells)
                    {
                        if (spell.Name.ToUpper().Contains(SourceTextSearch.ToUpper()) == false) { continue; }
                        if (Filters.First(filter => filter.Name == spell.SchoolOfMagic).Marked &&
                            SecondaryFilters.First(filter => filter.Name.Split()[1] == spell.SpellLevel.ToString()).Marked) { FilteredSourceSpells.Add(spell); }
                    }

                    Count_SourceFiltered = FilteredSourceSpells.Count();
                    Count_SourceAll = SourceSpells.Count();

                    break;
                case "Creatures":
                case "Players":
                    FilteredSourceCreatures.Clear();
                    foreach (CreatureModel creature in SourceCreatures)
                    {
                        if (creature.Name.ToUpper().Contains(SourceTextSearch.ToUpper()) == false) { continue; }
                        if (creature.CreatureCategory == null) { creature.CreatureCategory = ""; }
                        if (Filters.First(filter => filter.Name == creature.CreatureCategory).Marked) { FilteredSourceCreatures.Add(creature); }
                    }

                    Count_SourceFiltered = FilteredSourceCreatures.Count();
                    Count_SourceAll = SourceCreatures.Count();

                    break;
                case "Npcs":
                    FilteredSourceNpcs.Clear();
                    foreach (NpcModel npc in SourceNpcs)
                    {
                        if (npc.Name.ToUpper().Contains(SourceTextSearch.ToUpper()) == true) { FilteredSourceNpcs.Add(npc); }
                    }

                    Count_SourceFiltered = FilteredSourceNpcs.Count();
                    Count_SourceAll = SourceNpcs.Count();

                    break;
                default:
                    FilteredSourceCVs.Clear();
                    foreach (ConvertibleValue cv in SourceCVs)
                    {
                        if (cv.Value.ToUpper().Contains(SourceTextSearch.ToUpper()) == true) { FilteredSourceCVs.Add(cv); }
                    }
                    Count_SourceFiltered = FilteredSourceCVs.Count();
                    Count_SourceAll = SourceCVs.Count();
                    break;
            }
        }
        private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFilteredList();
        }

    }
}
