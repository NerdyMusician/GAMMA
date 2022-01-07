using GAMMA.Models;
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
    public class ToolsViewModel : BaseModel
    {
        // Constructors
        public ToolsViewModel()
        {
            XmlMethods.XmlToList(Configuration.SourcebookDataFilePath, out List<Sourcebook> sourcebooks);
            Sourcebooks = new(sourcebooks);
            SetConfigList_Sourcebooks();

            XmlMethods.XmlToList(Configuration.LootBoxDataFilePath, out List<LootBoxModel> lootBoxes);
            LootBoxes = new ObservableCollection<LootBoxModel>(lootBoxes);
            foreach (LootBoxModel lootBox in LootBoxes)
            {
                lootBox.UpdateToNewLootSystem();
                lootBox.ConnectItemLinks();
            }

            XmlMethods.XmlToList(Configuration.RollTableDataFilePath, out List<RollTableModel> rollTables);
            RollTables = new ObservableCollection<RollTableModel>(rollTables);

            XmlMethods.XmlToList(Configuration.EldritchInvocationsDataFilePath, out List<EldritchInvocation> invocations);
            EldritchInvocations = new(invocations);

            XmlMethods.XmlToList(Configuration.PlayerClassDataFilePath, out List<PlayerClassModel> playerClasses);
            PlayerClasses = new ObservableCollection<PlayerClassModel>(playerClasses);
            SetConfigList_PlayerClasses();
            SetConfigList_SpellcastingClasses();
            DataCleanup_SpellsKnownPerLevel();

            XmlMethods.XmlToList(Configuration.PlayerSubclassDataFilePath, out List<PlayerSubclassModel> playerSubclasses);
            PlayerSubclasses = new ObservableCollection<PlayerSubclassModel>(playerSubclasses);

            XmlMethods.XmlToList(Configuration.PlayerRaceDataFilePath, out List<PlayerRaceModel> playerRaces);
            PlayerRaces = new ObservableCollection<PlayerRaceModel>(playerRaces);
            SetConfigList_PlayerRaces();

            XmlMethods.XmlToList(Configuration.PlayerSubraceDataFilePath, out List<PlayerSubraceModel> playerSubraces);
            PlayerSubraces = new ObservableCollection<PlayerSubraceModel>(playerSubraces);

            XmlMethods.XmlToList(Configuration.PlayerBackgroundDataFilePath, out List<PlayerBackgroundModel> playerBackgrounds);
            PlayerBackgrounds = new ObservableCollection<PlayerBackgroundModel>(playerBackgrounds);
            SetConfigList_PlayerBackgrounds();

            XmlMethods.XmlToList(Configuration.PlayerFeatDataFilePath, out List<PlayerFeatModel> playerFeats);
            PlayerFeats = new ObservableCollection<PlayerFeatModel>(playerFeats);

            XmlMethods.XmlToList(Configuration.LanguageDataFilePath, out List<LanguageModel> languages);
            Languages = new ObservableCollection<LanguageModel>(languages);

            XmlMethods.XmlToList(Configuration.NoteTypeDataFilePath, out List<NoteType> noteTypes);
            NoteTypes = new(noteTypes);

            XmlMethods.XmlToList(Configuration.ShopDataFilePath, out List<ShopModel> shops);
            Shops = new ObservableCollection<ShopModel>(shops);
            foreach (ShopModel shop in Shops)
            {
                shop.SetItemTypes();
            }

            XmlMethods.XmlToList(Configuration.WeatherDataFilePath, out List<Weather> weathers);
            Weathers = new(weathers);
            SetConfigList_Weathers();

            XmlMethods.XmlToList(Configuration.CalendarDataFilePath, out List<GameCalendar> calendars);
            Calendars = new(calendars);
            SetConfigList_Calendars();

            ScaledImageHeight = 32;
            ScaledImageWidth = 32;

            PlayerRollTables = new ObservableCollection<RollTableModel>();

            SetPlayerRollTables();

        }

        // Databound Properties
        #region LootBoxes
        private ObservableCollection<LootBoxModel> _LootBoxes;
        public ObservableCollection<LootBoxModel> LootBoxes
        {
            get
            {
                return _LootBoxes;
            }
            set
            {
                _LootBoxes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveLootBox
        private LootBoxModel _ActiveLootBox;
        public LootBoxModel ActiveLootBox
        {
            get
            {
                return _ActiveLootBox;
            }
            set
            {
                _ActiveLootBox = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ScaledImageSource
        private string _ScaledImageSource;
        public string ScaledImageSource
        {
            get
            {
                return _ScaledImageSource;
            }
            set
            {
                _ScaledImageSource = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ScaledImageHeight
        private int _ScaledImageHeight;
        public int ScaledImageHeight
        {
            get
            {
                return _ScaledImageHeight;
            }
            set
            {
                _ScaledImageHeight = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ScaledImageWidth
        private int _ScaledImageWidth;
        public int ScaledImageWidth
        {
            get
            {
                return _ScaledImageWidth;
            }
            set
            {
                _ScaledImageWidth = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region RollTables
        private ObservableCollection<RollTableModel> _RollTables;
        public ObservableCollection<RollTableModel> RollTables
        {
            get
            {
                return _RollTables;
            }
            set
            {
                _RollTables = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PlayerRollTables
        private ObservableCollection<RollTableModel> _PlayerRollTables;
        public ObservableCollection<RollTableModel> PlayerRollTables
        {
            get
            {
                return _PlayerRollTables;
            }
            set
            {
                _PlayerRollTables = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveRollTable
        private RollTableModel _ActiveRollTable;
        public RollTableModel ActiveRollTable
        {
            get
            {
                return _ActiveRollTable;
            }
            set
            {
                _ActiveRollTable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Languages
        private ObservableCollection<LanguageModel> _Languages;
        public ObservableCollection<LanguageModel> Languages
        {
            get
            {
                return _Languages;
            }
            set
            {
                _Languages = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveLanguage
        private LanguageModel _ActiveLanguage;
        public LanguageModel ActiveLanguage
        {
            get
            {
                return _ActiveLanguage;
            }
            set
            {
                _ActiveLanguage = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PlayerClasses
        private ObservableCollection<PlayerClassModel> _PlayerClasses;
        public ObservableCollection<PlayerClassModel> PlayerClasses
        {
            get
            {
                return _PlayerClasses;
            }
            set
            {
                _PlayerClasses = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayerClass
        private PlayerClassModel _ActivePlayerClass;
        public PlayerClassModel ActivePlayerClass
        {
            get
            {
                return _ActivePlayerClass;
            }
            set
            {
                _ActivePlayerClass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PlayerSubclasses
        private ObservableCollection<PlayerSubclassModel> _PlayerSubclasses;
        public ObservableCollection<PlayerSubclassModel> PlayerSubclasses
        {
            get
            {
                return _PlayerSubclasses;
            }
            set
            {
                _PlayerSubclasses = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayerSubclass
        private PlayerSubclassModel _ActivePlayerSubclass;
        public PlayerSubclassModel ActivePlayerSubclass
        {
            get
            {
                return _ActivePlayerSubclass;
            }
            set
            {
                _ActivePlayerSubclass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PlayerRaces
        private ObservableCollection<PlayerRaceModel> _PlayerRaces;
        public ObservableCollection<PlayerRaceModel> PlayerRaces
        {
            get
            {
                return _PlayerRaces;
            }
            set
            {
                _PlayerRaces = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayerRace
        private PlayerRaceModel _ActivePlayerRace;
        public PlayerRaceModel ActivePlayerRace
        {
            get
            {
                return _ActivePlayerRace;
            }
            set
            {
                _ActivePlayerRace = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PlayerSubraces
        private ObservableCollection<PlayerSubraceModel> _PlayerSubraces;
        public ObservableCollection<PlayerSubraceModel> PlayerSubraces
        {
            get
            {
                return _PlayerSubraces;
            }
            set
            {
                _PlayerSubraces = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayerSubrace
        private PlayerSubraceModel _ActivePlayerSubrace;
        public PlayerSubraceModel ActivePlayerSubrace
        {
            get
            {
                return _ActivePlayerSubrace;
            }
            set
            {
                _ActivePlayerSubrace = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PlayerBackgrounds
        private ObservableCollection<PlayerBackgroundModel> _PlayerBackgrounds;
        public ObservableCollection<PlayerBackgroundModel> PlayerBackgrounds
        {
            get
            {
                return _PlayerBackgrounds;
            }
            set
            {
                _PlayerBackgrounds = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayerBackground
        private PlayerBackgroundModel _ActivePlayerBackground;
        public PlayerBackgroundModel ActivePlayerBackground
        {
            get
            {
                return _ActivePlayerBackground;
            }
            set
            {
                _ActivePlayerBackground = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PlayerFeats
        private ObservableCollection<PlayerFeatModel> _PlayerFeats;
        public ObservableCollection<PlayerFeatModel> PlayerFeats
        {
            get
            {
                return _PlayerFeats;
            }
            set
            {
                _PlayerFeats = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayerFeat
        private PlayerFeatModel _ActivePlayerFeat;
        public PlayerFeatModel ActivePlayerFeat
        {
            get
            {
                return _ActivePlayerFeat;
            }
            set
            {
                _ActivePlayerFeat = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Shops
        private ObservableCollection<ShopModel> _Shops;
        public ObservableCollection<ShopModel> Shops
        {
            get
            {
                return _Shops;
            }
            set
            {
                _Shops = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveShop
        private ShopModel _ActiveShop;
        public ShopModel ActiveShop
        {
            get
            {
                return _ActiveShop;
            }
            set
            {
                _ActiveShop = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region EldritchInvocations
        private ObservableCollection<EldritchInvocation> _EldritchInvocations;
        public ObservableCollection<EldritchInvocation> EldritchInvocations
        {
            get
            {
                return _EldritchInvocations;
            }
            set
            {
                _EldritchInvocations = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveEldritchInvocation
        private EldritchInvocation _ActiveEldritchInvocation;
        public EldritchInvocation ActiveEldritchInvocation
        {
            get
            {
                return _ActiveEldritchInvocation;
            }
            set
            {
                _ActiveEldritchInvocation = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Weathers
        private ObservableCollection<Weather> _Weathers;
        public ObservableCollection<Weather> Weathers
        {
            get
            {
                return _Weathers;
            }
            set
            {
                _Weathers = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveWeather
        private Weather _ActiveWeather;
        public Weather ActiveWeather
        {
            get
            {
                return _ActiveWeather;
            }
            set
            {
                _ActiveWeather = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Calendars
        private ObservableCollection<GameCalendar> _Calendars;
        public ObservableCollection<GameCalendar> Calendars
        {
            get
            {
                return _Calendars;
            }
            set
            {
                _Calendars = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveCalendar
        private GameCalendar _ActiveCalendar;
        public GameCalendar ActiveCalendar
        {
            get
            {
                return _ActiveCalendar;
            }
            set
            {
                _ActiveCalendar = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Sourcebooks
        private ObservableCollection<Sourcebook> _Sourcebooks;
        public ObservableCollection<Sourcebook> Sourcebooks
        {
            get
            {
                return _Sourcebooks;
            }
            set
            {
                _Sourcebooks = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveSourcebook
        private Sourcebook _ActiveSourcebook;
        public Sourcebook ActiveSourcebook
        {
            get
            {
                return _ActiveSourcebook;
            }
            set
            {
                _ActiveSourcebook = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region NoteTypes
        private ObservableCollection<NoteType> _NoteTypes;
        public ObservableCollection<NoteType> NoteTypes
        {
            get { return _NoteTypes; }
            set { _NoteTypes = value; NotifyPropertyChanged(); }
        }
        #endregion
        #region ActiveNoteType
        private NoteType _ActiveNoteType;
        public NoteType ActiveNoteType
        {
            get { return _ActiveNoteType; }
            set { _ActiveNoteType = value; NotifyPropertyChanged(); }
        }
        #endregion

        // Commands
        #region AddLootBox
        public ICommand AddLootBox => new RelayCommand(DoAddLootBox);
        private void DoAddLootBox(object param)
        {
            LootBoxes.Add(new LootBoxModel());
            ActiveLootBox = LootBoxes.Last();
        }
        #endregion
        #region SaveLootBoxes
        private RelayCommand _SaveLootBoxes;
        public ICommand SaveLootBoxes
        {
            get
            {
                if (_SaveLootBoxes == null)
                {
                    _SaveLootBoxes = new RelayCommand(param => DoSaveLootBoxes());
                }
                return _SaveLootBoxes;
            }
        }
        public void DoSaveLootBoxes(bool notifyUser = true)
        {
            XDocument lootboxDocument = new();
            if (LootBoxes.Count() == 0)
            {
                // Prevents zero count collection save crash
                lootboxDocument.Add(new XElement("LootBoxModelSet"));
                lootboxDocument.Save(Configuration.LootBoxDataFilePath);
                return;
            }
            lootboxDocument.Add(XmlMethods.ListToXml(LootBoxes.ToList()));
            lootboxDocument.Save("Data/LootBoxes.xml");
            HelperMethods.WriteToLogFile("Loot Boxes Saved.", notifyUser);
        }
        #endregion
        #region SortLootBoxes
        private RelayCommand _SortLootBoxes;
        public ICommand SortLootBoxes
        {
            get
            {
                if (_SortLootBoxes == null)
                {
                    _SortLootBoxes = new RelayCommand(param => DoSortLootBoxes());
                }
                return _SortLootBoxes;
            }
        }
        private void DoSortLootBoxes()
        {
            LootBoxes = new ObservableCollection<LootBoxModel>(LootBoxes.OrderBy(crt => crt.Name));
        }
        #endregion
        #region ImportLootBoxes
        private RelayCommand _ImportLootBoxes;
        public ICommand ImportLootBoxes
        {
            get
            {
                if (_ImportLootBoxes == null)
                {
                    _ImportLootBoxes = new RelayCommand(param => DoImportLootBoxes());
                }
                return _ImportLootBoxes;
            }
        }
        private void DoImportLootBoxes()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current loot box list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveLootBoxes();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_LootBoxes(openWindow.FileName, out string message);
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddRollTable
        private RelayCommand _AddRollTable;
        public ICommand AddRollTable
        {
            get
            {
                if (_AddRollTable == null)
                {
                    _AddRollTable = new RelayCommand(param => DoAddRollTable());
                }
                return _AddRollTable;
            }
        }
        private void DoAddRollTable()
        {
            RollTables.Add(new RollTableModel());
            ActiveRollTable = RollTables.Last();
        }
        #endregion
        #region SaveRollTables
        private RelayCommand _SaveRollTables;
        public ICommand SaveRollTables
        {
            get
            {
                if (_SaveRollTables == null)
                {
                    _SaveRollTables = new RelayCommand(param => DoSaveRollTables());
                }
                return _SaveRollTables;
            }
        }
        public void DoSaveRollTables(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (RollTables.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("RollTableModelSet"));
                xmlDoc.Save(Configuration.RollTableDataFilePath);
                return;
            }
            xmlDoc.Add(XmlMethods.ListToXml(RollTables.ToList()));
            xmlDoc.Save("Data/RollTables.xml");
            HelperMethods.WriteToLogFile("Roll Tables Saved.", notifyUser);

            SetPlayerRollTables();

        }
        #endregion
        #region SortRollTables
        private RelayCommand _SortRollTables;
        public ICommand SortRollTables
        {
            get
            {
                if (_SortRollTables == null)
                {
                    _SortRollTables = new RelayCommand(param => DoSortRollTables());
                }
                return _SortRollTables;
            }
        }
        private void DoSortRollTables()
        {
            RollTables = new ObservableCollection<RollTableModel>(RollTables.OrderBy(crt => crt.Name));
        }
        #endregion
        #region ImportRollTables
        private RelayCommand _ImportRollTables;
        public ICommand ImportRollTables
        {
            get
            {
                if (_ImportRollTables == null)
                {
                    _ImportRollTables = new RelayCommand(param => DoImportRollTables());
                }
                return _ImportRollTables;
            }
        }
        private void DoImportRollTables()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current roll table list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveRollTables();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_RollTables(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region SelectImageForScaling
        private RelayCommand _SelectImageForScaling;
        public ICommand SelectImageForScaling
        {
            get
            {
                if (_SelectImageForScaling == null)
                {
                    _SelectImageForScaling = new RelayCommand(param => DoSelectImageForScaling());
                }
                return _SelectImageForScaling;
            }
        }
        private void DoSelectImageForScaling()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "Image Files (*.png)|*.png"
            };

            if (openWindow.ShowDialog() == true)
            {
                ScaledImageSource = openWindow.FileName;
            }
        }
        #endregion

        #region AddPlayerClass
        private RelayCommand _AddPlayerClass;
        public ICommand AddPlayerClass
        {
            get
            {
                if (_AddPlayerClass == null)
                {
                    _AddPlayerClass = new RelayCommand(param => DoAddPlayerClass());
                }
                return _AddPlayerClass;
            }
        }
        private void DoAddPlayerClass()
        {
            PlayerClasses.Add(new PlayerClassModel());
            ActivePlayerClass = PlayerClasses.Last();
        }
        #endregion
        #region SortPlayerClasses
        private RelayCommand _SortPlayerClasses;
        public ICommand SortPlayerClasses
        {
            get
            {
                if (_SortPlayerClasses == null)
                {
                    _SortPlayerClasses = new RelayCommand(param => DoSortPlayerClasses());
                }
                return _SortPlayerClasses;
            }
        }
        private void DoSortPlayerClasses()
        {
            PlayerClasses = new ObservableCollection<PlayerClassModel>(PlayerClasses.OrderBy(pc => pc.Name));
        }
        #endregion
        #region SavePlayerClasses
        private RelayCommand _SavePlayerClasses;
        public ICommand SavePlayerClasses
        {
            get
            {
                if (_SavePlayerClasses == null)
                {
                    _SavePlayerClasses = new RelayCommand(param => DoSavePlayerClasses());
                }
                return _SavePlayerClasses;
            }
        }
        public void DoSavePlayerClasses(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerClasses.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("PlayerClassModelSet"));
                xmlDoc.Save(Configuration.PlayerClassDataFilePath);
                return;
            }
            xmlDoc.Add(XmlMethods.ListToXml(PlayerClasses.ToList()));
            xmlDoc.Save(Configuration.PlayerClassDataFilePath);
            HelperMethods.WriteToLogFile("Player Classes Saved.", notifyUser);
            SetConfigList_PlayerClasses();
        }
        #endregion
        #region ImportPlayerClasses
        private RelayCommand _ImportPlayerClasses;
        public ICommand ImportPlayerClasses
        {
            get
            {
                if (_ImportPlayerClasses == null)
                {
                    _ImportPlayerClasses = new RelayCommand(param => DoImportPlayerClasses());
                }
                return _ImportPlayerClasses;
            }
        }
        private void DoImportPlayerClasses()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current player class list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSavePlayerClasses();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerClasses(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddPlayerSubclass
        private RelayCommand _AddPlayerSubclass;
        public ICommand AddPlayerSubclass
        {
            get
            {
                if (_AddPlayerSubclass == null)
                {
                    _AddPlayerSubclass = new RelayCommand(param => DoAddPlayerSubclass());
                }
                return _AddPlayerSubclass;
            }
        }
        private void DoAddPlayerSubclass()
        {
            PlayerSubclasses.Add(new PlayerSubclassModel());
            ActivePlayerSubclass = PlayerSubclasses.Last();
        }
        #endregion
        #region SortPlayerSubclasses
        public ICommand SortPlayerSubclasses => new RelayCommand(DoSortPlayerSubclasses);
        private void DoSortPlayerSubclasses(object param)
        {
            if (param == null)
            {
                PlayerSubclasses = new(PlayerSubclasses.OrderBy(pc => pc.Name));
            }
            else if (param.ToString() == "SubclassOf")
            {
                PlayerSubclasses = new(PlayerSubclasses.OrderBy(s => s.SubclassOf).ThenBy(s => s.Name));
            }
        }
        #endregion
        #region SavePlayerSubclasses
        private RelayCommand _SavePlayerSubclasses;
        public ICommand SavePlayerSubclasses
        {
            get
            {
                if (_SavePlayerSubclasses == null)
                {
                    _SavePlayerSubclasses = new RelayCommand(param => DoSavePlayerSubclasses());
                }
                return _SavePlayerSubclasses;
            }
        }
        public void DoSavePlayerSubclasses(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerSubclasses.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("PlayerSublassModelSet"));
                xmlDoc.Save(Configuration.PlayerSubclassDataFilePath);
                return;
            }
            xmlDoc.Add(XmlMethods.ListToXml(PlayerSubclasses.ToList()));
            xmlDoc.Save(Configuration.PlayerSubclassDataFilePath);
            HelperMethods.WriteToLogFile("Player Subclasses Saved.", notifyUser);
        }
        #endregion
        #region ImportPlayerSubclasses
        private RelayCommand _ImportPlayerSubclasses;
        public ICommand ImportPlayerSubclasses
        {
            get
            {
                if (_ImportPlayerSubclasses == null)
                {
                    _ImportPlayerSubclasses = new RelayCommand(param => DoImportPlayerSubclasses());
                }
                return _ImportPlayerSubclasses;
            }
        }
        private void DoImportPlayerSubclasses()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current player subclass list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSavePlayerSubclasses();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerSubclasses(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddPlayerRace
        private RelayCommand _AddPlayerRace;
        public ICommand AddPlayerRace
        {
            get
            {
                if (_AddPlayerRace == null)
                {
                    _AddPlayerRace = new RelayCommand(param => DoAddPlayerRace());
                }
                return _AddPlayerRace;
            }
        }
        private void DoAddPlayerRace()
        {
            PlayerRaces.Add(new PlayerRaceModel());
            ActivePlayerRace = PlayerRaces.Last();
        }
        #endregion
        #region SortPlayerRaces
        private RelayCommand _SortPlayerRaces;
        public ICommand SortPlayerRaces
        {
            get
            {
                if (_SortPlayerRaces == null)
                {
                    _SortPlayerRaces = new RelayCommand(param => DoSortPlayerRaces());
                }
                return _SortPlayerRaces;
            }
        }
        private void DoSortPlayerRaces()
        {
            PlayerRaces = new ObservableCollection<PlayerRaceModel>(PlayerRaces.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SavePlayerRaces
        private RelayCommand _SavePlayerRaces;
        public ICommand SavePlayerRaces
        {
            get
            {
                if (_SavePlayerRaces == null)
                {
                    _SavePlayerRaces = new RelayCommand(param => DoSavePlayerRaces());
                }
                return _SavePlayerRaces;
            }
        }
        public void DoSavePlayerRaces(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerRaces.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("PlayerRaceModelSet"));
                xmlDoc.Save(Configuration.PlayerRaceDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(PlayerRaces.ToList()));
                xmlDoc.Save(Configuration.PlayerRaceDataFilePath);
            }
            HelperMethods.WriteToLogFile("Player Races Saved.", notifyUser);
            SetConfigList_PlayerRaces();
        }
        #endregion
        #region ImportPlayerRaces
        private RelayCommand _ImportPlayerRaces;
        public ICommand ImportPlayerRaces
        {
            get
            {
                if (_ImportPlayerRaces == null)
                {
                    _ImportPlayerRaces = new RelayCommand(param => DoImportPlayerRaces());
                }
                return _ImportPlayerRaces;
            }
        }
        private void DoImportPlayerRaces()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current player race list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSavePlayerRaces();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerRaces(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddPlayerSubrace
        private RelayCommand _AddPlayerSubrace;
        public ICommand AddPlayerSubrace
        {
            get
            {
                if (_AddPlayerSubrace == null)
                {
                    _AddPlayerSubrace = new RelayCommand(param => DoAddPlayerSubrace());
                }
                return _AddPlayerSubrace;
            }
        }
        private void DoAddPlayerSubrace()
        {
            PlayerSubraces.Add(new PlayerSubraceModel());
            ActivePlayerSubrace = PlayerSubraces.Last();
        }
        #endregion
        #region SortPlayerSubraces
        public ICommand SortPlayerSubraces => new RelayCommand(DoSortPlayerSubraces);
        private void DoSortPlayerSubraces(object param)
        {
            if (param == null)
            {
                PlayerSubraces = new(PlayerSubraces.OrderBy(crt => crt.Name));
            }
            else if (param.ToString() == "SubraceOf")
            {
                PlayerSubraces = new(PlayerSubraces.OrderBy(s => s.SubraceOf).ThenBy(s => s.Name));
            }
        }
        #endregion
        #region SavePlayerSubraces
        private RelayCommand _SavePlayerSubraces;
        public ICommand SavePlayerSubraces
        {
            get
            {
                if (_SavePlayerSubraces == null)
                {
                    _SavePlayerSubraces = new RelayCommand(param => DoSavePlayerSubraces());
                }
                return _SavePlayerSubraces;
            }
        }
        public void DoSavePlayerSubraces(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerSubraces.Count() == 0)
            {
                xmlDoc.Add(new XElement("PlayerSubraceModelSet"));
                xmlDoc.Save(Configuration.PlayerSubraceDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(PlayerSubraces.ToList()));
                xmlDoc.Save(Configuration.PlayerSubraceDataFilePath);
            }
            HelperMethods.WriteToLogFile("Player Subraces Saved.", notifyUser);
        }
        #endregion
        #region ImportPlayerSubraces
        private RelayCommand _ImportPlayerSubraces;
        public ICommand ImportPlayerSubraces
        {
            get
            {
                if (_ImportPlayerSubraces == null)
                {
                    _ImportPlayerSubraces = new RelayCommand(param => DoImportPlayerSubraces());
                }
                return _ImportPlayerSubraces;
            }
        }
        private void DoImportPlayerSubraces()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current player subrace list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSavePlayerSubraces();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerSubraces(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddPlayerBackground
        private RelayCommand _AddPlayerBackground;
        public ICommand AddPlayerBackground
        {
            get
            {
                if (_AddPlayerBackground == null)
                {
                    _AddPlayerBackground = new RelayCommand(param => DoAddPlayerBackground());
                }
                return _AddPlayerBackground;
            }
        }
        private void DoAddPlayerBackground()
        {
            PlayerBackgrounds.Add(new PlayerBackgroundModel());
            ActivePlayerBackground = PlayerBackgrounds.Last();
        }
        #endregion
        #region SortPlayerBackgrounds
        private RelayCommand _SortPlayerBackgrounds;
        public ICommand SortPlayerBackgrounds
        {
            get
            {
                if (_SortPlayerBackgrounds == null)
                {
                    _SortPlayerBackgrounds = new RelayCommand(param => DoSortPlayerBackgrounds());
                }
                return _SortPlayerBackgrounds;
            }
        }
        private void DoSortPlayerBackgrounds()
        {
            PlayerBackgrounds = new ObservableCollection<PlayerBackgroundModel>(PlayerBackgrounds.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SavePlayerBackgrounds
        private RelayCommand _SavePlayerBackgrounds;
        public ICommand SavePlayerBackgrounds
        {
            get
            {
                if (_SavePlayerBackgrounds == null)
                {
                    _SavePlayerBackgrounds = new RelayCommand(param => DoSavePlayerBackgrounds());
                }
                return _SavePlayerBackgrounds;
            }
        }
        public void DoSavePlayerBackgrounds(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerBackgrounds.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("PlayerBackgroundModelSet"));
                xmlDoc.Save(Configuration.PlayerBackgroundDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(PlayerBackgrounds.ToList()));
                xmlDoc.Save(Configuration.PlayerBackgroundDataFilePath);
            }
            HelperMethods.WriteToLogFile("Player Backgrounds Saved.", notifyUser);
            SetConfigList_PlayerBackgrounds();
        }
        #endregion
        #region ImportPlayerBackgrounds
        private RelayCommand _ImportPlayerBackgrounds;
        public ICommand ImportPlayerBackgrounds
        {
            get
            {
                if (_ImportPlayerBackgrounds == null)
                {
                    _ImportPlayerBackgrounds = new RelayCommand(param => DoImportPlayerBackgrounds());
                }
                return _ImportPlayerBackgrounds;
            }
        }
        private void DoImportPlayerBackgrounds()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current player background list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSavePlayerBackgrounds();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerBackgrounds(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddPlayerFeat
        private RelayCommand _AddPlayerFeat;
        public ICommand AddPlayerFeat
        {
            get
            {
                if (_AddPlayerFeat == null)
                {
                    _AddPlayerFeat = new RelayCommand(param => DoAddPlayerFeat());
                }
                return _AddPlayerFeat;
            }
        }
        private void DoAddPlayerFeat()
        {
            PlayerFeats.Add(new PlayerFeatModel());
            ActivePlayerFeat = PlayerFeats.Last();
        }
        #endregion
        #region SortPlayerFeats
        private RelayCommand _SortPlayerFeats;
        public ICommand SortPlayerFeats
        {
            get
            {
                if (_SortPlayerFeats == null)
                {
                    _SortPlayerFeats = new RelayCommand(param => DoSortPlayerFeats());
                }
                return _SortPlayerFeats;
            }
        }
        private void DoSortPlayerFeats()
        {
            PlayerFeats = new ObservableCollection<PlayerFeatModel>(PlayerFeats.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SavePlayerFeats
        private RelayCommand _SavePlayerFeats;
        public ICommand SavePlayerFeats
        {
            get
            {
                if (_SavePlayerFeats == null)
                {
                    _SavePlayerFeats = new RelayCommand(param => DoSavePlayerFeats());
                }
                return _SavePlayerFeats;
            }
        }
        public void DoSavePlayerFeats(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerFeats.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("PlayerFeatModelSet"));
                xmlDoc.Save(Configuration.PlayerFeatDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(PlayerFeats.ToList()));
                xmlDoc.Save(Configuration.PlayerFeatDataFilePath);
            }
            HelperMethods.WriteToLogFile("Player Feats Saved.", notifyUser);
        }
        #endregion
        #region ImportPlayerFeats
        private RelayCommand _ImportPlayerFeats;
        public ICommand ImportPlayerFeats
        {
            get
            {
                if (_ImportPlayerFeats == null)
                {
                    _ImportPlayerFeats = new RelayCommand(param => DoImportPlayerFeats());
                }
                return _ImportPlayerFeats;
            }
        }
        private void DoImportPlayerFeats()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current player feat list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSavePlayerFeats();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_PlayerFeats(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddLanguage
        private RelayCommand _AddLanguage;
        public ICommand AddLanguage
        {
            get
            {
                if (_AddLanguage == null)
                {
                    _AddLanguage = new RelayCommand(param => DoAddLanguage());
                }
                return _AddLanguage;
            }
        }
        private void DoAddLanguage()
        {
            Languages.Add(new LanguageModel());
            ActiveLanguage = Languages.Last();
        }
        #endregion
        #region SortLanguages
        private RelayCommand _SortLanguages;
        public ICommand SortLanguages
        {
            get
            {
                if (_SortLanguages == null)
                {
                    _SortLanguages = new RelayCommand(param => DoSortLanguages());
                }
                return _SortLanguages;
            }
        }
        private void DoSortLanguages()
        {
            Languages = new ObservableCollection<LanguageModel>(Languages.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SaveLanguages
        private RelayCommand _SaveLanguages;
        public ICommand SaveLanguages
        {
            get
            {
                if (_SaveLanguages == null)
                {
                    _SaveLanguages = new RelayCommand(param => DoSaveLanguages());
                }
                return _SaveLanguages;
            }
        }
        public void DoSaveLanguages(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (Languages.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("LanguageModelSet"));
                xmlDoc.Save(Configuration.LanguageDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(Languages.ToList()));
                xmlDoc.Save(Configuration.LanguageDataFilePath);
            }
            HelperMethods.WriteToLogFile("Languages Saved.", notifyUser);
        }
        #endregion
        #region ImportLanguages
        private RelayCommand _ImportLanguages;
        public ICommand ImportLanguages
        {
            get
            {
                if (_ImportLanguages == null)
                {
                    _ImportLanguages = new RelayCommand(param => DoImportLanguages());
                }
                return _ImportLanguages;
            }
        }
        private void DoImportLanguages()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current language list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveLanguages();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Languages(openWindow.FileName, out string message);
                new NotificationDialog(message).ShowDialog();

            }
        }
        #endregion

        #region AddShop
        private RelayCommand _AddShop;
        public ICommand AddShop
        {
            get
            {
                if (_AddShop == null)
                {
                    _AddShop = new RelayCommand(param => DoAddShop());
                }
                return _AddShop;
            }
        }
        private void DoAddShop()
        {
            Shops.Add(new ShopModel());
            Shops.Last().SetItemTypes();
            ActiveShop = Shops.Last();
        }
        #endregion
        #region SortShops
        private RelayCommand _SortShops;
        public ICommand SortShops
        {
            get
            {
                if (_SortShops == null)
                {
                    _SortShops = new RelayCommand(param => DoSortShops());
                }
                return _SortShops;
            }
        }
        private void DoSortShops()
        {
            Shops = new ObservableCollection<ShopModel>(Shops.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SaveShops
        private RelayCommand _SaveShops;
        public ICommand SaveShops
        {
            get
            {
                if (_SaveShops == null)
                {
                    _SaveShops = new RelayCommand(param => DoSaveShops());
                }
                return _SaveShops;
            }
        }
        public void DoSaveShops(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (Shops.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("ShopModelSet"));
                xmlDoc.Save(Configuration.ShopDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(Shops.ToList()));
                xmlDoc.Save(Configuration.ShopDataFilePath);
            }
            HelperMethods.WriteToLogFile("Shops Saved.", notifyUser);
        }
        #endregion
        #region ImportShops
        private RelayCommand _ImportShops;
        public ICommand ImportShops
        {
            get
            {
                if (_ImportShops == null)
                {
                    _ImportShops = new RelayCommand(param => DoImportShops());
                }
                return _ImportShops;
            }
        }
        private void DoImportShops()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current shop list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveShops();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Shops(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddEldritchInvocation
        private RelayCommand _AddEldritchInvocation;
        public ICommand AddEldritchInvocation
        {
            get
            {
                if (_AddEldritchInvocation == null)
                {
                    _AddEldritchInvocation = new RelayCommand(param => DoAddEldritchInvocation());
                }
                return _AddEldritchInvocation;
            }
        }
        private void DoAddEldritchInvocation()
        {
            EldritchInvocations.Add(new EldritchInvocation());
            ActiveEldritchInvocation = EldritchInvocations.Last();
        }
        #endregion
        #region SortEldritchInvocations
        private RelayCommand _SortEldritchInvocations;
        public ICommand SortEldritchInvocations
        {
            get
            {
                if (_SortEldritchInvocations == null)
                {
                    _SortEldritchInvocations = new RelayCommand(param => DoSortEldritchInvocations());
                }
                return _SortEldritchInvocations;
            }
        }
        private void DoSortEldritchInvocations()
        {
            EldritchInvocations = new ObservableCollection<EldritchInvocation>(EldritchInvocations.OrderBy(ei => ei.Name));
        }
        #endregion
        #region SaveEldritchInvocations
        private RelayCommand _SaveEldritchInvocations;
        public ICommand SaveEldritchInvocations
        {
            get
            {
                if (_SaveEldritchInvocations == null)
                {
                    _SaveEldritchInvocations = new RelayCommand(param => DoSaveEldritchInvocations());
                }
                return _SaveEldritchInvocations;
            }
        }
        public void DoSaveEldritchInvocations(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (EldritchInvocations.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("EldritchInvocationModelSet"));
                xmlDoc.Save(Configuration.EldritchInvocationsDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(EldritchInvocations.ToList()));
                xmlDoc.Save(Configuration.EldritchInvocationsDataFilePath);
            }
            HelperMethods.WriteToLogFile("Eldritch Invocations Saved.", notifyUser);
        }
        #endregion
        #region ImportEldritchInvocations
        private RelayCommand _ImportEldritchInvocations;
        public ICommand ImportEldritchInvocations
        {
            get
            {
                if (_ImportEldritchInvocations == null)
                {
                    _ImportEldritchInvocations = new RelayCommand(param => DoImportEldritchInvocations());
                }
                return _ImportEldritchInvocations;
            }
        }
        private void DoImportEldritchInvocations()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current eldritch invocation list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveEldritchInvocations();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Invocations(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddWeather
        private RelayCommand _AddWeather;
        public ICommand AddWeather
        {
            get
            {
                if (_AddWeather == null)
                {
                    _AddWeather = new RelayCommand(param => DoAddWeather());
                }
                return _AddWeather;
            }
        }
        private void DoAddWeather()
        {
            Weather newWeather = new() { Name = "New Weather" };
            newWeather.WeatherEntries.Add(new() { Icon = "Icon_Weather_Clear", Name = "Clear", LowValue = 1, HighValue = 34, Description = "No effects." });
            newWeather.WeatherEntries.Add(new() { Icon = "Icon_Weather_PartlyCloudy", Name = "Partly Cloudy", LowValue = 35, HighValue = 49, Description = "No effects." });
            newWeather.WeatherEntries.Add(new() { Icon = "Icon_Weather_Cloudy", Name = "Cloudy", LowValue = 50, HighValue = 64, Description = "Creatures no longer affected by Sunlight Sensitivity." });
            newWeather.WeatherEntries.Add(new() { Icon = "Icon_Weather_Raining", Name = "Light Rain", LowValue = 65, HighValue = 76, Description = "Creatures no longer affected by Sunlight Sensitivity.\n-1 to Passive Perception outdoors.\nUnable to rest outdoors." });
            newWeather.WeatherEntries.Add(new() { Icon = "Icon_Weather_HeavyRain", Name = "Heavy Rain", LowValue = 77, HighValue = 88, Description = "Creatures no longer affected by Sunlight Sensitivity.\n-2 to Passive Perception outdoors.\nUnable to rest outdoors." });
            newWeather.WeatherEntries.Add(new() { Icon = "Icon_Weather_Storm", Name = "Heavy Storm", LowValue = 89, HighValue = 100, Description = "Creatures no longer affected by Sunlight Sensitivity.\n-5 to Passive Perception outdoors.\nUnable to rest outdoors.\nEvery hour without shelter raises exhaustion by 1." });
            Weathers.Add(newWeather);
            ActiveWeather = Weathers.Last();
        }
        #endregion
        #region SortWeathers
        private RelayCommand _SortWeathers;
        public ICommand SortWeathers
        {
            get
            {
                if (_SortWeathers == null)
                {
                    _SortWeathers = new RelayCommand(param => DoSortWeathers());
                }
                return _SortWeathers;
            }
        }
        private void DoSortWeathers()
        {
            Weathers = new ObservableCollection<Weather>(Weathers.OrderBy(ei => ei.Name));
        }
        #endregion
        #region SaveWeathers
        private RelayCommand _SaveWeathers;
        public ICommand SaveWeathers
        {
            get
            {
                if (_SaveWeathers == null)
                {
                    _SaveWeathers = new RelayCommand(param => DoSaveWeathers());
                }
                return _SaveWeathers;
            }
        }
        public void DoSaveWeathers(bool notifyUser = true)
        {
            if (ValidateData_Weathers() == false) { return; }
            XDocument xmlDoc = new();
            if (Weathers.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("WeatherSet"));
                xmlDoc.Save(Configuration.WeatherDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(Weathers.ToList()));
                xmlDoc.Save(Configuration.WeatherDataFilePath);
            }
            HelperMethods.WriteToLogFile("Weathers Saved.", notifyUser);
        }
        #endregion
        #region ImportWeathers
        private RelayCommand _ImportWeathers;
        public ICommand ImportWeathers
        {
            get
            {
                if (_ImportWeathers == null)
                {
                    _ImportWeathers = new RelayCommand(param => DoImportWeathers());
                }
                return _ImportWeathers;
            }
        }
        private void DoImportWeathers()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current weather list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveWeathers();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Weather(openWindow.FileName, out string message);
                new NotificationDialog(message).ShowDialog();

            }
        }
        #endregion

        #region AddCalendar
        private RelayCommand _AddCalendar;
        public ICommand AddCalendar
        {
            get
            {
                if (_AddCalendar == null)
                {
                    _AddCalendar = new RelayCommand(param => DoAddCalendar());
                }
                return _AddCalendar;
            }
        }
        private void DoAddCalendar()
        {
            GameCalendar newCalendar = new();
            Calendars.Add(newCalendar);
            ActiveCalendar = Calendars.Last();
        }
        #endregion
        #region SortCalendars
        private RelayCommand _SortCalendars;
        public ICommand SortCalendars
        {
            get
            {
                if (_SortCalendars == null)
                {
                    _SortCalendars = new RelayCommand(param => DoSortCalendars());
                }
                return _SortCalendars;
            }
        }
        private void DoSortCalendars()
        {
            Calendars = new ObservableCollection<GameCalendar>(Calendars.OrderBy(ei => ei.Name));
        }
        #endregion
        #region SaveCalendars
        private RelayCommand _SaveCalendars;
        public ICommand SaveCalendars
        {
            get
            {
                if (_SaveCalendars == null)
                {
                    _SaveCalendars = new RelayCommand(param => DoSaveCalendars());
                }
                return _SaveCalendars;
            }
        }
        public void DoSaveCalendars(bool notifyUser = true)
        {
            if (ValidateData_Calendars() == false) { return; }
            XDocument xmlDoc = new();
            if (Calendars.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("GameCalendarSet"));
                xmlDoc.Save(Configuration.CalendarDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(Calendars.ToList()));
                xmlDoc.Save(Configuration.CalendarDataFilePath);
            }
            HelperMethods.WriteToLogFile("Calendars Saved.", notifyUser);
        }
        #endregion
        #region ImportCalendars
        private RelayCommand _ImportCalendars;
        public ICommand ImportCalendars
        {
            get
            {
                if (_ImportCalendars == null)
                {
                    _ImportCalendars = new RelayCommand(param => DoImportCalendars());
                }
                return _ImportCalendars;
            }
        }
        private void DoImportCalendars()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current calendar list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveCalendars();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Calendars(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddSourcebook
        private RelayCommand _AddSourcebook;
        public ICommand AddSourcebook
        {
            get
            {
                if (_AddSourcebook == null)
                {
                    _AddSourcebook = new RelayCommand(param => DoAddSourcebook());
                }
                return _AddSourcebook;
            }
        }
        private void DoAddSourcebook()
        {
            Sourcebooks.Add(new());
            ActiveSourcebook = Sourcebooks.Last();
        }
        #endregion
        #region SortSourcebooks
        private RelayCommand _SortSourcebooks;
        public ICommand SortSourcebooks
        {
            get
            {
                if (_SortSourcebooks == null)
                {
                    _SortSourcebooks = new RelayCommand(param => DoSortSourcebooks());
                }
                return _SortSourcebooks;
            }
        }
        private void DoSortSourcebooks()
        {
            Sourcebooks = new(Sourcebooks.OrderBy(sb => sb.Name));
        }
        #endregion
        #region SaveSourcebooks
        private RelayCommand _SaveSourcebooks;
        public ICommand SaveSourcebooks
        {
            get
            {
                if (_SaveSourcebooks == null)
                {
                    _SaveSourcebooks = new RelayCommand(param => DoSaveSourcebooks());
                }
                return _SaveSourcebooks;
            }
        }
        public void DoSaveSourcebooks(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (Sourcebooks.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("SourcebookSet"));
                xmlDoc.Save(Configuration.SourcebookDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(Sourcebooks.ToList()));
                xmlDoc.Save(Configuration.SourcebookDataFilePath);
            }
            HelperMethods.WriteToLogFile("Sourcebooks Saved.", notifyUser);
            SetConfigList_Sourcebooks();
        }
        #endregion
        #region ImportSourcebooks
        private RelayCommand _ImportSourcebooks;
        public ICommand ImportSourcebooks
        {
            get
            {
                if (_ImportSourcebooks == null)
                {
                    _ImportSourcebooks = new RelayCommand(param => DoImportSourcebooks());
                }
                return _ImportSourcebooks;
            }
        }
        private void DoImportSourcebooks()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current sourcebook list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveSourcebooks();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Sourcebooks(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        #region AddNoteType
        public ICommand AddNoteType => new RelayCommand(DoAddNoteType);
        private void DoAddNoteType(object param)
        {
            NoteTypes.Add(new());
            ActiveNoteType = NoteTypes.Last();
        }
        #endregion
        #region SortNoteTypes
        public ICommand SortNoteTypes => new RelayCommand(DoSortNoteTypes);
        private void DoSortNoteTypes(object param)
        {
            NoteTypes = new(NoteTypes.OrderBy(nt => nt.Name));
        }
        #endregion
        #region SaveNoteTypes
        public ICommand SaveNoteTypes => new RelayCommand(param => DoSaveNoteTypes());
        public void DoSaveNoteTypes(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (NoteTypes.Count() == 0)
            {
                // Prevents zero count collection save crash
                xmlDoc.Add(new XElement("NoteTypeSet"));
                xmlDoc.Save(Configuration.NoteTypeDataFilePath);
            }
            else
            {
                xmlDoc.Add(XmlMethods.ListToXml(NoteTypes.ToList()));
                xmlDoc.Save(Configuration.NoteTypeDataFilePath);
            }
            HelperMethods.WriteToLogFile("Note Types Saved.", notifyUser);
            SetConfigList_NoteTypes();
        }
        #endregion
        #region ImportNoteTypes
        public ICommand ImportNoteTypes => new RelayCommand(DoImportNoteTypes);
        private void DoImportNoteTypes(object param)
        {
            OpenFileDialog openWindow = new()
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new("Prior to import, the current note type list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveNoteTypes();

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_NoteTypes(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        // Public Methods
        public void DataCleanup_SpellsKnownPerLevel()
        {
            foreach (PlayerClassModel pClass in PlayerClasses)
            {
                if (pClass.HasSpellcasting == false) { continue; }
                List<SpellTableRowModel> cleanRows = new();
                foreach (SpellTableRowModel dirtyRow in pClass.SpellTableRows)
                {
                    if (dirtyRow.SpellSlots_1st == 0 && dirtyRow.CantripsKnown == 0) { continue; }
                    cleanRows.Add(dirtyRow);
                }
                if (cleanRows.Count() < 20 && cleanRows.Count() > 0)
                {
                    for (int i = 0; i <= 20; i++)
                    {
                        bool foundMatch = false;
                        foreach (SpellTableRowModel row in cleanRows)
                        {
                            if (row.ClassLevel == i + 1) { foundMatch = true; break; }
                        }
                        if (foundMatch) { continue; }
                        cleanRows.Add(new() { ClassLevel = i + 1, SpellsKnownMode = cleanRows.First().SpellsKnownMode });
                        
                    }
                }
                pClass.SpellTableRows = new(cleanRows.OrderBy(r => r.ClassLevel));
            }
        }

        // Private Methods
        private void SetPlayerRollTables()
        {
            PlayerRollTables.Clear();
            foreach (RollTableModel rollTable in RollTables)
            {
                if (rollTable.AvailableToPlayers)
                {
                    PlayerRollTables.Add(rollTable);
                }
            }
        }
        private void SetConfigList_PlayerClasses()
        {
            if (Configuration.MainModelRef.PlayerClasses == null) { return; }
            Configuration.MainModelRef.PlayerClasses.Clear();
            foreach (PlayerClassModel pc in PlayerClasses)
            {
                if (pc.IsValidated == false) { continue; }
                Configuration.MainModelRef.PlayerClasses.Add(pc.Name);
            }
        }
        private void SetConfigList_SpellcastingClasses()
        {
            if (Configuration.MainModelRef.SpellcastingClasses == null) { return; }
            Configuration.MainModelRef.SpellcastingClasses.Clear();
            foreach (PlayerClassModel pc in PlayerClasses)
            {
                if (pc.IsValidated == false) { continue; }
                if (pc.HasSpellcasting == false) { continue; }
                Configuration.MainModelRef.SpellcastingClasses.Add(pc.Name);
            }
        }
        private void SetConfigList_PlayerRaces()
        {
            if (Configuration.MainModelRef.PlayerRaces == null) { return; }
            Configuration.MainModelRef.PlayerRaces.Clear();
            foreach (PlayerRaceModel pr in PlayerRaces)
            {
                if (pr.IsValidated == false) { continue; }
                Configuration.MainModelRef.PlayerRaces.Add(pr.Name);
            }
        }
        private void SetConfigList_PlayerBackgrounds()
        {
            if (Configuration.MainModelRef.PlayerBackgrounds == null) { return; }
            Configuration.MainModelRef.PlayerBackgrounds.Clear();
            foreach (PlayerBackgroundModel pb in PlayerBackgrounds)
            {
                if (pb.IsValidated == false) { continue; }
                Configuration.MainModelRef.PlayerBackgrounds.Add(pb.Name);
            }
        }
        private void SetConfigList_Weathers()
        {
            if (Configuration.MainModelRef.WeatherRepository == null) { return; }
            Configuration.MainModelRef.WeatherRepository.Clear();
            foreach (Weather weather in Weathers)
            {
                if (weather.IsValidated == false) { continue; }
                Configuration.MainModelRef.WeatherRepository.Add(weather.Name);
            }
        }
        private void SetConfigList_Calendars()
        {
            if (Configuration.MainModelRef.CalendarRepository == null) { return; }
            Configuration.MainModelRef.CalendarRepository.Clear();
            foreach (GameCalendar calendar in Calendars)
            {
                if (calendar.IsValidated == false) { continue; }
                Configuration.MainModelRef.CalendarRepository.Add(calendar.Name);
            }
        }
        private void SetConfigList_Sourcebooks()
        {
            if (Configuration.MainModelRef.SourcebookRepository == null) { return; }
            Configuration.MainModelRef.SourcebookRepository.Clear();
            foreach (Sourcebook sourcebook in Sourcebooks)
            {
                if (sourcebook.IsValidated == false) { continue; }
                Configuration.MainModelRef.SourcebookRepository.Add(sourcebook.Name);
            }
        }
        private void SetConfigList_NoteTypes()
        {
            if (Configuration.MainModelRef.NoteTypeRepository == null)
            {
                Configuration.MainModelRef.NoteTypeRepository = new();
            }
            else
            {
                Configuration.MainModelRef.NoteTypeRepository.Clear();
            }
            foreach (NoteType noteType in NoteTypes)
            {
                Configuration.MainModelRef.NoteTypeRepository.Add(noteType.Name);
            }
        }
        private bool ValidateData_Weathers()
        {
            List<string> messages = new();
            foreach (Weather w in Weathers)
            {
                int matches = 0;
                for (int i = 1; i <= 100; i++)
                {
                    foreach (WeatherRow wr in w.WeatherEntries)
                    {
                        if (i >= wr.LowValue && i <= wr.HighValue) { matches++; }
                    }
                }
                if (matches < 100)
                {
                    messages.Add(w.Name + " has a value range coverage gap.");
                }
                if (matches > 100)
                {
                    messages.Add(w.Name + " has overlapping value coverage.");
                }
            }
            if (messages.Count() > 0)
            {
                string message = "Weather errors found:";
                foreach (string msg in messages)
                {
                    message += "\n" + msg;
                }
                new NotificationDialog(message, "Report").ShowDialog();
                return false;
            }
            return true;
        }
        private bool ValidateData_Calendars()
        {
            List<string> messages = new();
            foreach (GameCalendar calendar in Calendars)
            {
                if (calendar.Name == "") { messages.Add("Calendar missing name."); }
                if (calendar.Days.Count() == 0) { messages.Add(calendar.Name + " has no days."); }
                if (calendar.Months.Count() == 0) { messages.Add(calendar.Name + " has no months."); }
                if (calendar.WeeksPerMonth <= 0) { messages.Add(calendar.Name + " has invalid value for Weeks Per Month."); }
                if (calendar.UseEras == true && calendar.YearsPerEra < 1) { messages.Add(calendar.Name + " has invalid value for Years Per Era."); }
            }
            if (messages.Count() > 0)
            {
                string message = "Calendar errors found:";
                foreach (string msg in messages)
                {
                    message += "\n" + msg;
                }
                new NotificationDialog(message, "Report").ShowDialog();
                return false;
            }
            return true;
        }

    }
}
