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
            SetConfigList_NoteTypes();

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
            get => _LootBoxes;
            set => SetAndNotify(ref _LootBoxes, value);
        }
        #endregion
        #region ActiveLootBox
        private LootBoxModel _ActiveLootBox;
        public LootBoxModel ActiveLootBox
        {
            get => _ActiveLootBox;
            set => SetAndNotify(ref _ActiveLootBox, value);
        }
        #endregion

        #region ScaledImageSource
        private string _ScaledImageSource;
        public string ScaledImageSource
        {
            get => _ScaledImageSource;
            set => SetAndNotify(ref _ScaledImageSource, value);
        }
        #endregion
        #region ScaledImageHeight
        private int _ScaledImageHeight;
        public int ScaledImageHeight
        {
            get => _ScaledImageHeight;
            set => SetAndNotify(ref _ScaledImageHeight, value);
        }
        #endregion
        #region ScaledImageWidth
        private int _ScaledImageWidth;
        public int ScaledImageWidth
        {
            get => _ScaledImageWidth;
            set => SetAndNotify(ref _ScaledImageWidth, value);
        }
        #endregion

        #region RollTables
        private ObservableCollection<RollTableModel> _RollTables;
        public ObservableCollection<RollTableModel> RollTables
        {
            get => _RollTables;
            set => SetAndNotify(ref _RollTables, value);
        }
        #endregion
        #region PlayerRollTables
        private ObservableCollection<RollTableModel> _PlayerRollTables;
        public ObservableCollection<RollTableModel> PlayerRollTables
        {
            get => _PlayerRollTables;
            set => SetAndNotify(ref _PlayerRollTables, value);
        }
        #endregion
        #region ActiveRollTable
        private RollTableModel _ActiveRollTable;
        public RollTableModel ActiveRollTable
        {
            get => _ActiveRollTable;
            set => SetAndNotify(ref _ActiveRollTable, value);
        }
        #endregion

        #region Languages
        private ObservableCollection<LanguageModel> _Languages;
        public ObservableCollection<LanguageModel> Languages
        {
            get => _Languages;
            set => SetAndNotify(ref _Languages, value);
        }
        #endregion
        #region ActiveLanguage
        private LanguageModel _ActiveLanguage;
        public LanguageModel ActiveLanguage
        {
            get => _ActiveLanguage;
            set => SetAndNotify(ref _ActiveLanguage, value);
        }
        #endregion

        #region PlayerClasses
        private ObservableCollection<PlayerClassModel> _PlayerClasses;
        public ObservableCollection<PlayerClassModel> PlayerClasses
        {
            get => _PlayerClasses;
            set => SetAndNotify(ref _PlayerClasses, value);
        }
        #endregion
        #region ActivePlayerClass
        private PlayerClassModel _ActivePlayerClass;
        public PlayerClassModel ActivePlayerClass
        {
            get => _ActivePlayerClass;
            set => SetAndNotify(ref _ActivePlayerClass, value);
        }
        #endregion

        #region PlayerSubclasses
        private ObservableCollection<PlayerSubclassModel> _PlayerSubclasses;
        public ObservableCollection<PlayerSubclassModel> PlayerSubclasses
        {
            get => _PlayerSubclasses;
            set => SetAndNotify(ref _PlayerSubclasses, value);
        }
        #endregion
        #region ActivePlayerSubclass
        private PlayerSubclassModel _ActivePlayerSubclass;
        public PlayerSubclassModel ActivePlayerSubclass
        {
            get => _ActivePlayerSubclass;
            set => SetAndNotify(ref _ActivePlayerSubclass, value);
        }
        #endregion

        #region PlayerRaces
        private ObservableCollection<PlayerRaceModel> _PlayerRaces;
        public ObservableCollection<PlayerRaceModel> PlayerRaces
        {
            get => _PlayerRaces;
            set => SetAndNotify(ref _PlayerRaces, value);
        }
        #endregion
        #region ActivePlayerRace
        private PlayerRaceModel _ActivePlayerRace;
        public PlayerRaceModel ActivePlayerRace
        {
            get => _ActivePlayerRace;
            set => SetAndNotify(ref _ActivePlayerRace, value);
        }
        #endregion

        #region PlayerSubraces
        private ObservableCollection<PlayerSubraceModel> _PlayerSubraces;
        public ObservableCollection<PlayerSubraceModel> PlayerSubraces
        {
            get => _PlayerSubraces;
            set => SetAndNotify(ref _PlayerSubraces, value);
        }
        #endregion
        #region ActivePlayerSubrace
        private PlayerSubraceModel _ActivePlayerSubrace;
        public PlayerSubraceModel ActivePlayerSubrace
        {
            get => _ActivePlayerSubrace;
            set => SetAndNotify(ref _ActivePlayerSubrace, value);
        }
        #endregion

        #region PlayerBackgrounds
        private ObservableCollection<PlayerBackgroundModel> _PlayerBackgrounds;
        public ObservableCollection<PlayerBackgroundModel> PlayerBackgrounds
        {
            get => _PlayerBackgrounds;
            set => SetAndNotify(ref _PlayerBackgrounds, value);
        }
        #endregion
        #region ActivePlayerBackground
        private PlayerBackgroundModel _ActivePlayerBackground;
        public PlayerBackgroundModel ActivePlayerBackground
        {
            get => _ActivePlayerBackground;
            set => SetAndNotify(ref _ActivePlayerBackground, value);
        }
        #endregion

        #region PlayerFeats
        private ObservableCollection<PlayerFeatModel> _PlayerFeats;
        public ObservableCollection<PlayerFeatModel> PlayerFeats
        {
            get => _PlayerFeats;
            set => SetAndNotify(ref _PlayerFeats, value);
        }
        #endregion
        #region ActivePlayerFeat
        private PlayerFeatModel _ActivePlayerFeat;
        public PlayerFeatModel ActivePlayerFeat
        {
            get => _ActivePlayerFeat;
            set => SetAndNotify(ref _ActivePlayerFeat, value);
        }
        #endregion

        #region Shops
        private ObservableCollection<ShopModel> _Shops;
        public ObservableCollection<ShopModel> Shops
        {
            get => _Shops;
            set => SetAndNotify(ref _Shops, value);
        }
        #endregion
        #region ActiveShop
        private ShopModel _ActiveShop;
        public ShopModel ActiveShop
        {
            get => _ActiveShop;
            set => SetAndNotify(ref _ActiveShop, value);
        }
        #endregion

        #region EldritchInvocations
        private ObservableCollection<EldritchInvocation> _EldritchInvocations;
        public ObservableCollection<EldritchInvocation> EldritchInvocations
        {
            get => _EldritchInvocations;
            set => SetAndNotify(ref _EldritchInvocations, value);
        }
        #endregion
        #region ActiveEldritchInvocation
        private EldritchInvocation _ActiveEldritchInvocation;
        public EldritchInvocation ActiveEldritchInvocation
        {
            get => _ActiveEldritchInvocation;
            set => SetAndNotify(ref _ActiveEldritchInvocation, value);
        }
        #endregion

        #region Weathers
        private ObservableCollection<Weather> _Weathers;
        public ObservableCollection<Weather> Weathers
        {
            get => _Weathers;
            set => SetAndNotify(ref _Weathers, value);
        }
        #endregion
        #region ActiveWeather
        private Weather _ActiveWeather;
        public Weather ActiveWeather
        {
            get => _ActiveWeather;
            set => SetAndNotify(ref _ActiveWeather, value);
        }
        #endregion

        #region Calendars
        private ObservableCollection<GameCalendar> _Calendars;
        public ObservableCollection<GameCalendar> Calendars
        {
            get => _Calendars;
            set => SetAndNotify(ref _Calendars, value);
        }
        #endregion
        #region ActiveCalendar
        private GameCalendar _ActiveCalendar;
        public GameCalendar ActiveCalendar
        {
            get => _ActiveCalendar;
            set => SetAndNotify(ref _ActiveCalendar, value);
        }
        #endregion

        #region Sourcebooks
        private ObservableCollection<Sourcebook> _Sourcebooks;
        public ObservableCollection<Sourcebook> Sourcebooks
        {
            get => _Sourcebooks;
            set => SetAndNotify(ref _Sourcebooks, value);
        }
        #endregion
        #region ActiveSourcebook
        private Sourcebook _ActiveSourcebook;
        public Sourcebook ActiveSourcebook
        {
            get => _ActiveSourcebook;
            set => SetAndNotify(ref _ActiveSourcebook, value);
        }
        #endregion

        #region NoteTypes
        private ObservableCollection<NoteType> _NoteTypes;
        public ObservableCollection<NoteType> NoteTypes
        {
            get => _NoteTypes;
            set => SetAndNotify(ref _NoteTypes, value);
        }
        #endregion
        #region ActiveNoteType
        private NoteType _ActiveNoteType;
        public NoteType ActiveNoteType
        {
            get => _ActiveNoteType;
            set => SetAndNotify(ref _ActiveNoteType, value);
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
        public ICommand SaveLootBoxes => new RelayCommand(param => DoSaveLootBoxes());
        public void DoSaveLootBoxes(bool notifyUser = true)
        {
            XDocument lootboxDocument = new();
            if (LootBoxes.Count == 0)
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
        public ICommand SortLootBoxes => new RelayCommand(param => DoSortLootBoxes());
        private void DoSortLootBoxes()
        {
            LootBoxes = new ObservableCollection<LootBoxModel>(LootBoxes.OrderBy(crt => crt.Name));
        }
        #endregion
        #region ImportLootBoxes
        public ICommand ImportLootBoxes => new RelayCommand(param => DoImportLootBoxes());
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
        public ICommand AddRollTable => new RelayCommand(param => DoAddRollTable());
        private void DoAddRollTable()
        {
            RollTables.Add(new RollTableModel());
            ActiveRollTable = RollTables.Last();
        }
        #endregion
        #region SaveRollTables
        public ICommand SaveRollTables => new RelayCommand(param => DoSaveRollTables());
        public void DoSaveRollTables(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (RollTables.Count == 0)
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
        public ICommand SortRollTables => new RelayCommand(param => DoSortRollTables());
        private void DoSortRollTables()
        {
            RollTables = new ObservableCollection<RollTableModel>(RollTables.OrderBy(crt => crt.Name));
        }
        #endregion
        #region ImportRollTables
        public ICommand ImportRollTables => new RelayCommand(param => DoImportRollTables());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region SelectImageForScaling
        public ICommand SelectImageForScaling => new RelayCommand(param => DoSelectImageForScaling());
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
        public ICommand AddPlayerClass => new RelayCommand(param => DoAddPlayerClass());
        private void DoAddPlayerClass()
        {
            PlayerClasses.Add(new PlayerClassModel());
            ActivePlayerClass = PlayerClasses.Last();
        }
        #endregion
        #region SortPlayerClasses
        public ICommand SortPlayerClasses => new RelayCommand(param => DoSortPlayerClasses());
        private void DoSortPlayerClasses()
        {
            PlayerClasses = new(PlayerClasses.OrderBy(pc => pc.Name));
        }
        #endregion
        #region SavePlayerClasses
        public ICommand SavePlayerClasses => new RelayCommand(param => DoSavePlayerClasses());
        public void DoSavePlayerClasses(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerClasses.Count == 0)
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
        public ICommand ImportPlayerClasses => new RelayCommand(param => DoImportPlayerClasses());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddPlayerSubclass
        public ICommand AddPlayerSubclass => new RelayCommand(param => DoAddPlayerSubclass());
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
        public ICommand SavePlayerSubclasses => new RelayCommand(param => DoSavePlayerSubclasses());
        public void DoSavePlayerSubclasses(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerSubclasses.Count == 0)
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
        public ICommand ImportPlayerSubclasses => new RelayCommand(param => DoImportPlayerSubclasses());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddPlayerRace
        public ICommand AddPlayerRace => new RelayCommand(param => DoAddPlayerRace());
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
            if (PlayerRaces.Count == 0)
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
        public ICommand ImportPlayerRaces => new RelayCommand(param => DoImportPlayerRaces());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddPlayerSubrace
        public ICommand AddPlayerSubrace => new RelayCommand(param => DoAddPlayerSubrace());
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
        public ICommand SavePlayerSubraces => new RelayCommand(param => DoSavePlayerSubraces());
        public void DoSavePlayerSubraces(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerSubraces.Count == 0)
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
        public ICommand ImportPlayerSubraces => new RelayCommand(param => DoImportPlayerSubraces());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddPlayerBackground
        public ICommand AddPlayerBackground => new RelayCommand(param => DoAddPlayerBackground());
        private void DoAddPlayerBackground()
        {
            PlayerBackgrounds.Add(new());
            ActivePlayerBackground = PlayerBackgrounds.Last();
        }
        #endregion
        #region SortPlayerBackgrounds
        public ICommand SortPlayerBackgrounds => new RelayCommand(param => DoSortPlayerBackgrounds());
        private void DoSortPlayerBackgrounds()
        {
            PlayerBackgrounds = new ObservableCollection<PlayerBackgroundModel>(PlayerBackgrounds.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SavePlayerBackgrounds
        public ICommand SavePlayerBackgrounds => new RelayCommand(param => DoSavePlayerBackgrounds());
        public void DoSavePlayerBackgrounds(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerBackgrounds.Count == 0)
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
        public ICommand ImportPlayerBackgrounds => new RelayCommand(param => DoImportPlayerBackgrounds());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddPlayerFeat
        public ICommand AddPlayerFeat => new RelayCommand(param => DoAddPlayerFeat());
        private void DoAddPlayerFeat()
        {
            PlayerFeats.Add(new PlayerFeatModel());
            ActivePlayerFeat = PlayerFeats.Last();
        }
        #endregion
        #region SortPlayerFeats
        public ICommand SortPlayerFeats => new RelayCommand(param => DoSortPlayerFeats());
        private void DoSortPlayerFeats()
        {
            PlayerFeats = new(PlayerFeats.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SavePlayerFeats
        public ICommand SavePlayerFeats => new RelayCommand(param => DoSavePlayerFeats());
        public void DoSavePlayerFeats(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (PlayerFeats.Count == 0)
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
        public ICommand ImportPlayerFeats => new RelayCommand(param => DoImportPlayerFeats());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddLanguage
        public ICommand AddLanguage => new RelayCommand(param => DoAddLanguage());
        private void DoAddLanguage()
        {
            Languages.Add(new());
            ActiveLanguage = Languages.Last();
        }
        #endregion
        #region SortLanguages
        private RelayCommand _SortLanguages;
        public ICommand SortLanguages => new RelayCommand(param => DoSortLanguages());
        private void DoSortLanguages()
        {
            Languages = new(Languages.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SaveLanguages
        public ICommand SaveLanguages => new RelayCommand(param => DoSaveLanguages());
        public void DoSaveLanguages(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (Languages.Count == 0)
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
        public ICommand ImportLanguages => new RelayCommand(param => DoImportLanguages());
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
                HelperMethods.NotifyUser(message);

            }
        }
        #endregion

        #region AddShop
        public ICommand AddShop => new RelayCommand(param => DoAddShop());
        private void DoAddShop()
        {
            Shops.Add(new ShopModel());
            Shops.Last().SetItemTypes();
            ActiveShop = Shops.Last();
        }
        #endregion
        #region SortShops
        public ICommand SortShops => new RelayCommand(param => DoSortShops());
        private void DoSortShops()
        {
            Shops = new(Shops.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SaveShops
        public ICommand SaveShops => new RelayCommand(param => DoSaveShops());
        public void DoSaveShops(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (Shops.Count == 0)
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
        public ICommand ImportShops => new RelayCommand(param => DoImportShops());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddEldritchInvocation
        public ICommand AddEldritchInvocation => new RelayCommand(param => DoAddEldritchInvocation());
        private void DoAddEldritchInvocation()
        {
            EldritchInvocations.Add(new());
            ActiveEldritchInvocation = EldritchInvocations.Last();
        }
        #endregion
        #region SortEldritchInvocations
        public ICommand SortEldritchInvocations => new RelayCommand(param => DoSortEldritchInvocations());
        private void DoSortEldritchInvocations()
        {
            EldritchInvocations = new ObservableCollection<EldritchInvocation>(EldritchInvocations.OrderBy(ei => ei.Name));
        }
        #endregion
        #region SaveEldritchInvocations
        public ICommand SaveEldritchInvocations => new RelayCommand(param => DoSaveEldritchInvocations());
        public void DoSaveEldritchInvocations(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (EldritchInvocations.Count == 0)
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
        public ICommand ImportEldritchInvocations => new RelayCommand(param => DoImportEldritchInvocations());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddWeather
        public ICommand AddWeather => new RelayCommand(param => DoAddWeather());
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
        public ICommand SortWeathers => new RelayCommand(param => DoSortWeathers());
        private void DoSortWeathers()
        {
            Weathers = new(Weathers.OrderBy(ei => ei.Name));
        }
        #endregion
        #region SaveWeathers
        public ICommand SaveWeathers => new RelayCommand(param => DoSaveWeathers());
        public void DoSaveWeathers(bool notifyUser = true)
        {
            if (ValidateData_Weathers() == false) { return; }
            XDocument xmlDoc = new();
            if (Weathers.Count == 0)
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
        public ICommand ImportWeathers => new RelayCommand(param => DoImportWeathers());
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
                HelperMethods.NotifyUser(message);

            }
        }
        #endregion

        #region AddCalendar
        public ICommand AddCalendar => new RelayCommand(param => DoAddCalendar());
        private void DoAddCalendar()
        {
            GameCalendar newCalendar = new();
            Calendars.Add(newCalendar);
            ActiveCalendar = Calendars.Last();
        }
        #endregion
        #region SortCalendars
        public ICommand SortCalendars => new RelayCommand(param => DoSortCalendars());
        private void DoSortCalendars()
        {
            Calendars = new(Calendars.OrderBy(ei => ei.Name));
        }
        #endregion
        #region SaveCalendars
        public ICommand SaveCalendars => new RelayCommand(param => DoSaveCalendars());
        public void DoSaveCalendars(bool notifyUser = true)
        {
            if (ValidateData_Calendars() == false) { return; }
            XDocument xmlDoc = new();
            if (Calendars.Count == 0)
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
        public ICommand ImportCalendars => new RelayCommand(param => DoImportCalendars());
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
                HelperMethods.NotifyUser(message);
            }
        }
        #endregion

        #region AddSourcebook
        public ICommand AddSourcebook => new RelayCommand(param => DoAddSourcebook());
        private void DoAddSourcebook()
        {
            Sourcebooks.Add(new());
            ActiveSourcebook = Sourcebooks.Last();
        }
        #endregion
        #region SortSourcebooks
        public ICommand SortSourcebooks => new RelayCommand(param => DoSortSourcebooks());
        private void DoSortSourcebooks()
        {
            Sourcebooks = new(Sourcebooks.OrderBy(sb => sb.Name));
        }
        #endregion
        #region SaveSourcebooks
        public ICommand SaveSourcebooks => new RelayCommand(param => DoSaveSourcebooks());
        public void DoSaveSourcebooks(bool notifyUser = true)
        {
            XDocument xmlDoc = new();
            if (Sourcebooks.Count == 0)
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
        public ICommand ImportSourcebooks => new RelayCommand(param => DoImportSourcebooks());
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
                HelperMethods.NotifyUser(message);
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
            if (NoteTypes.Count == 0)
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
                HelperMethods.NotifyUser(message);
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
                if (cleanRows.Count < 20 && cleanRows.Count > 0)
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
            if (messages.Count > 0)
            {
                string message = "Weather errors found:";
                foreach (string msg in messages)
                {
                    message += "\n" + msg;
                }
                HelperMethods.NotifyUser(message, HelperMethods.UserNotificationType.Report);
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
                if (calendar.Days.Count == 0) { messages.Add(calendar.Name + " has no days."); }
                if (calendar.Months.Count == 0) { messages.Add(calendar.Name + " has no months."); }
                if (calendar.WeeksPerMonth <= 0) { messages.Add(calendar.Name + " has invalid value for Weeks Per Month."); }
                if (calendar.UseEras == true && calendar.YearsPerEra < 1) { messages.Add(calendar.Name + " has invalid value for Years Per Era."); }
            }
            if (messages.Count > 0)
            {
                string message = "Calendar errors found:";
                foreach (string msg in messages)
                {
                    message += "\n" + msg;
                }
                HelperMethods.NotifyUser(message, HelperMethods.UserNotificationType.Report);
                return false;
            }
            return true;
        }

    }
}
