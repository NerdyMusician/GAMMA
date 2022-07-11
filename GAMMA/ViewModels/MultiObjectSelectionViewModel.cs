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
            SourceTextSearch = string.Empty;
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
            SourceTextSearch = string.Empty;
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
            SourceTextSearch = string.Empty;
        }
        public MultiObjectSelectionViewModel(List<NpcModel> npcs)
        {
            SourceNpcs = new ObservableCollection<NpcModel>(npcs);
            FilteredSourceNpcs = new ObservableCollection<NpcModel>();
            SelectedNpcs = new ObservableCollection<NpcModel>();
            Mode = "Npcs";
            Filters = new ObservableCollection<BoolOption>();
            SecondaryFilters = new();
            SourceTextSearch = string.Empty;
        }
        public MultiObjectSelectionViewModel(List<ConvertibleValue> cvs, string mode)
        {
            SourceCVs = new(cvs);
            FilteredSourceCVs = new();
            SelectedCVs = new();
            Mode = mode;
            Filters = new();
            SecondaryFilters = new();
            SourceTextSearch = string.Empty;
        }
        public MultiObjectSelectionViewModel(List<GameNote> records, string mode)
        {
            InitializeCollections();
            SourceNotes = new(records);
            Mode = mode;
            SourceTextSearch = string.Empty;
        }
        private void InitializeCollections()
        {
            SourceNotes = new();
            FilteredSourceNotes = new();
            SelectedNotes = new();
        }

        // Databound Properties
        #region Mode
        private string _Mode;
        public string Mode
        {
            get => _Mode;
            set => SetAndNotify(ref _Mode, value);
        }
        #endregion
        #region ShowFilters
        private bool _ShowFilters;
        public bool ShowFilters
        {
            get => _ShowFilters;
            set => SetAndNotify(ref _ShowFilters, value);
        }
        #endregion
        #region Filters
        private ObservableCollection<BoolOption> _Filters;
        public ObservableCollection<BoolOption> Filters
        {
            get => _Filters;
            set => SetAndNotify(ref _Filters, value);
        }
        #endregion
        #region SecondaryFilters
        private ObservableCollection<BoolOption> _SecondaryFilters;
        public ObservableCollection<BoolOption> SecondaryFilters
        {
            get => _SecondaryFilters;
            set => SetAndNotify(ref _SecondaryFilters, value);
        }
        #endregion

        #region SourceCreatures
        private ObservableCollection<CreatureModel> _SourceCreatures;
        public ObservableCollection<CreatureModel> SourceCreatures
        {
            get => _SourceCreatures;
            set => SetAndNotify(ref _SourceCreatures, value);
        }
        #endregion
        #region FilteredSourceCreatures
        private ObservableCollection<CreatureModel> _FilteredSourceCreatures;
        public ObservableCollection<CreatureModel> FilteredSourceCreatures
        {
            get => _FilteredSourceCreatures;
            set => SetAndNotify(ref _FilteredSourceCreatures, value);
        }
        #endregion
        #region SelectedCreatures
        private ObservableCollection<CreatureModel> _SelectedCreatures;
        public ObservableCollection<CreatureModel> SelectedCreatures
        {
            get => _SelectedCreatures;
            set => SetAndNotify(ref _SelectedCreatures, value);
        }
        #endregion

        #region SourceItems
        private ObservableCollection<ItemModel> _SourceItems;
        public ObservableCollection<ItemModel> SourceItems
        {
            get => _SourceItems;
            set => SetAndNotify(ref _SourceItems, value);
        }
        #endregion
        #region FilteredSourceItems
        private ObservableCollection<ItemModel> _FilteredSourceItems;
        public ObservableCollection<ItemModel> FilteredSourceItems
        {
            get => _FilteredSourceItems;
            set => SetAndNotify(ref _FilteredSourceItems, value);
        }
        #endregion
        #region SelectedItems
        private ObservableCollection<ItemModel> _SelectedItems;
        public ObservableCollection<ItemModel> SelectedItems
        {
            get => _SelectedItems;
            set => SetAndNotify(ref _SelectedItems, value);
        }
        #endregion

        #region SourceSpells
        private ObservableCollection<SpellModel> _SourceSpells;
        public ObservableCollection<SpellModel> SourceSpells
        {
            get => _SourceSpells;
            set => SetAndNotify(ref _SourceSpells, value);
        }
        #endregion
        #region FilteredSourceSpells
        private ObservableCollection<SpellModel> _FilteredSourceSpells;
        public ObservableCollection<SpellModel> FilteredSourceSpells
        {
            get => _FilteredSourceSpells;
            set => SetAndNotify(ref _FilteredSourceSpells, value);
        }
        #endregion
        #region SelectedSpells
        private ObservableCollection<SpellModel> _SelectedSpells;
        public ObservableCollection<SpellModel> SelectedSpells
        {
            get => _SelectedSpells;
            set => SetAndNotify(ref _SelectedSpells, value);
        }
        #endregion

        #region SourceNpcs
        private ObservableCollection<NpcModel> _SourceNpcs;
        public ObservableCollection<NpcModel> SourceNpcs
        {
            get => _SourceNpcs;
            set => SetAndNotify(ref _SourceNpcs, value);
        }
        #endregion
        #region FilteredSourceNpcs
        private ObservableCollection<NpcModel> _FilteredSourceNpcs;
        public ObservableCollection<NpcModel> FilteredSourceNpcs
        {
            get => _FilteredSourceNpcs;
            set => SetAndNotify(ref _FilteredSourceNpcs, value);
        }
        #endregion
        #region SelectedNpcs
        private ObservableCollection<NpcModel> _SelectedNpcs;
        public ObservableCollection<NpcModel> SelectedNpcs
        {
            get => _SelectedNpcs;
            set => SetAndNotify(ref _SelectedNpcs, value);
        }
        #endregion

        #region SourceCVs
        private ObservableCollection<ConvertibleValue> _SourceCVs;
        public ObservableCollection<ConvertibleValue> SourceCVs
        {
            get => _SourceCVs;
            set => SetAndNotify(ref _SourceCVs, value);
        }
        #endregion
        #region FilteredSourceCVs
        private ObservableCollection<ConvertibleValue> _FilteredSourceCVs;
        public ObservableCollection<ConvertibleValue> FilteredSourceCVs
        {
            get => _FilteredSourceCVs;
            set => SetAndNotify(ref _FilteredSourceCVs, value);
        }
        #endregion
        #region SelectedCVs
        private ObservableCollection<ConvertibleValue> _SelectedCVs;
        public ObservableCollection<ConvertibleValue> SelectedCVs
        {
            get => _SelectedCVs;
            set => SetAndNotify(ref _SelectedCVs, value);
        }
        #endregion

        #region SourceTextSearch
        private string _SourceTextSearch;
        public string SourceTextSearch
        {
            get => _SourceTextSearch;
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
            get => _Count_SourceAll;
            set => SetAndNotify(ref _Count_SourceAll, value);
        }
        #endregion
        #region Count_SourceFiltered
        private int _Count_SourceFiltered;
        public int Count_SourceFiltered
        {
            get => _Count_SourceFiltered;
            set => SetAndNotify(ref _Count_SourceFiltered, value);
        }
        #endregion

        private ObservableCollection<GameNote> _SourceNotes;
        public ObservableCollection<GameNote> SourceNotes
        {
            get => _SourceNotes;
            set => SetAndNotify(ref _SourceNotes, value);
        }
        private ObservableCollection<GameNote> _FilteredSourceNotes;
        public ObservableCollection<GameNote> FilteredSourceNotes
        {
            get => _FilteredSourceNotes;
            set => SetAndNotify(ref _FilteredSourceNotes, value);
        }
        private ObservableCollection<GameNote> _SelectedNotes;
        public ObservableCollection<GameNote> SelectedNotes
        {
            get => _SelectedNotes;
            set => SetAndNotify(ref _SelectedNotes, value);
        }

        // Commands
        #region SelectFilters
        public ICommand SelectFilters => new RelayCommand(DoSelectFilters);
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
                        if (creature.CreatureCategory == null) { creature.CreatureCategory = string.Empty; }
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
                case "Associated Notes":
                    FilteredSourceNotes.Clear();
                    foreach (GameNote note in SourceNotes)
                    {
                        if (note.Name.ToUpper().Contains(SourceTextSearch.ToUpper()) == true) { FilteredSourceNotes.Add(note); }
                    }
                    Count_SourceFiltered = FilteredSourceNotes.Count();
                    Count_SourceAll = SourceNotes.Count();
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
