using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using GAMMA.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace GAMMA.ViewModels
{
    public class MainViewModel : BaseModel
    {
        // Constructors
        public MainViewModel()
        {
            Configuration.MainModelRef = this;
            PlayerClasses = new();
            SpellcastingClasses = new();
            PlayerRaces = new();
            PlayerBackgrounds = new();
            WeatherRepository = new();
            CalendarRepository = new();
            SourcebookRepository = new();
            XmlMethods.XmlToObject(Configuration.SettingsDataFilePath, out SettingsViewModel sv);
            if (sv != null) { SettingsView = sv; } else { SettingsView = new SettingsViewModel(); }
            ItemBuilderView = new ItemBuilderViewModel();
            SpellBuilderView = new SpellBuilderViewModel();
            ToolsView = new ToolsViewModel();
            CreatureBuilderView = new CreatureBuilderViewModel();
            CharacterBuilderView = new CharacterBuilderViewModel();
            AudioView = new AudioViewModel();
            CampaignView = new();

            // Configuration Data
            Sources = Configuration.Sources.ToList();
            DiceSides = Configuration.DiceSides.ToList();
            ItemTypes = Configuration.ItemTypes.ToList();
            DamageTypes = Configuration.DamageTypes.ToList();
            CreatureTypes = Configuration.CreatureTypes.ToList();
            CreatureCategories = Configuration.CreatureCategories.ToList();
            Sizes = Configuration.Sizes.ToList();
            Alignments = Configuration.Alignments.ToList();
            SchoolsOfMagic = Configuration.SchoolsOfMagic.ToList();
            AoeShapes = Configuration.AoeShapes.ToList();
            AbilityTypes = Configuration.AbilityTypes.ToList();
            Environments = Configuration.Environments.ToList();
            IconNames = Configuration.IconList.ToList();
            ChallengeRatings = Configuration.ChallengeRatings.ToList();
            DamageProclivities = Configuration.DamageProclivities.ToList();
            VolumeSizes = Configuration.VolumeSizes.ToList();
            DrinkVolumes = Configuration.DrinkVolumes.ToList();
            FishingEnvironments = Configuration.FishingEnvironments.ToList();
            RarityLevels = Configuration.RarityLevels.ToList();
            SpellsKnownPerLevelOptions = Configuration.SpellsKnownPerLevelOptions.ToList();

            // Misc
            if (SettingsView.InDmModeModern) { TabSelected_Campaigns = true; }
            else { TabSelected_Players = true; }
            SettingsView.WebDriverStatus = "Closed";
            ApplicationVersion = "GAMMA 1.28.00 beta r2";

            Configuration.LoadComplete = true;

        }

        public IWebDriver WebDriver = null;
        public bool IsWebDriverOpen = false;

        // Databound Properties
        #region CreatureBuilderView
        private CreatureBuilderViewModel _CreatureBuilderView;
        public CreatureBuilderViewModel CreatureBuilderView
        {
            get
            {
                return _CreatureBuilderView;
            }
            set
            {
                _CreatureBuilderView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ItemBuilderView
        private ItemBuilderViewModel _ItemBuilderView;
        public ItemBuilderViewModel ItemBuilderView
        {
            get
            {
                return _ItemBuilderView;
            }
            set
            {
                _ItemBuilderView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellBuilderView
        private SpellBuilderViewModel _SpellBuilderView;
        public SpellBuilderViewModel SpellBuilderView
        {
            get
            {
                return _SpellBuilderView;
            }
            set
            {
                _SpellBuilderView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CharacterBuilderView
        private CharacterBuilderViewModel _CharacterBuilderView;
        public CharacterBuilderViewModel CharacterBuilderView
        {
            get
            {
                return _CharacterBuilderView;
            }
            set
            {
                _CharacterBuilderView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ToolsView
        private ToolsViewModel _ToolsView;
        public ToolsViewModel ToolsView
        {
            get
            {
                return _ToolsView;
            }
            set
            {
                _ToolsView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AudioView
        private AudioViewModel _AudioView;
        public AudioViewModel AudioView
        {
            get
            {
                return _AudioView;
            }
            set
            {
                _AudioView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SettingsView
        private SettingsViewModel _SettingsView;
        [XmlSaveMode("Single")]
        public SettingsViewModel SettingsView
        {
            get
            {
                return _SettingsView;
            }
            set
            {
                _SettingsView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CampaignView
        private CampaignBuilderViewModel _CampaignView;
        public CampaignBuilderViewModel CampaignView
        {
            get
            {
                return _CampaignView;
            }
            set
            {
                _CampaignView = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region WebDriverStatus
        private string _WebDriverStatus;
        public string WebDriverStatus
        {
            get
            {
                return _WebDriverStatus;
            }
            set
            {
                _WebDriverStatus = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ApplicationVersion
        private string _ApplicationVersion;
        public string ApplicationVersion
        {
            get
            {
                return _ApplicationVersion;
            }
            set
            {
                _ApplicationVersion = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ChangeLog
        private string _ChangeLog;
        public string ChangeLog
        {
            get
            {
                return _ChangeLog;
            }
            set
            {
                _ChangeLog = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Tab Selection
        #region TabSelected_CreatureBuilder
        private bool _TabSelected_CreatureBuilder;
        public bool TabSelected_CreatureBuilder
        {
            get
            {
                return _TabSelected_CreatureBuilder;
            }
            set
            {
                _TabSelected_CreatureBuilder = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TabSelected_SpellBuilder
        private bool _TabSelected_SpellBuilder;
        public bool TabSelected_SpellBuilder
        {
            get
            {
                return _TabSelected_SpellBuilder;
            }
            set
            {
                _TabSelected_SpellBuilder = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TabSelected_Players
        private bool _TabSelected_Players;
        public bool TabSelected_Players
        {
            get
            {
                return _TabSelected_Players;
            }
            set
            {
                _TabSelected_Players = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TabSelected_ItemBuilder
        private bool _TabSelected_ItemBuilder;
        public bool TabSelected_ItemBuilder
        {
            get
            {
                return _TabSelected_ItemBuilder;
            }
            set
            {
                _TabSelected_ItemBuilder = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TabSelected_Campaigns
        private bool _TabSelected_Campaigns;
        public bool TabSelected_Campaigns
        {
            get
            {
                return _TabSelected_Campaigns;
            }
            set
            {
                _TabSelected_Campaigns = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region GameTypes
        private List<string> _GameTypes;
        public List<string> GameTypes
        {
            get
            {
                return _GameTypes;
            }
            set
            {
                _GameTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Sources
        private List<string> _Sources;
        public List<string> Sources
        {
            get
            {
                return _Sources;
            }
            set
            {
                _Sources = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DiceSides
        private List<string> _DiceSides;
        public List<string> DiceSides
        {
            get
            {
                return _DiceSides;
            }
            set
            {
                _DiceSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ItemTypes
        private List<string> _ItemTypes;
        public List<string> ItemTypes
        {
            get
            {
                return _ItemTypes;
            }
            set
            {
                _ItemTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageTypes
        private List<string> _DamageTypes;
        public List<string> DamageTypes
        {
            get
            {
                return _DamageTypes;
            }
            set
            {
                _DamageTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CreatureTypes
        private List<string> _CreatureTypes;
        public List<string> CreatureTypes
        {
            get
            {
                return _CreatureTypes;
            }
            set
            {
                _CreatureTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CreatureCategories
        private List<string> _CreatureCategories;
        public List<string> CreatureCategories
        {
            get
            {
                return _CreatureCategories;
            }
            set
            {
                _CreatureCategories = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Sizes
        private List<string> _Sizes;
        public List<string> Sizes
        {
            get
            {
                return _Sizes;
            }
            set
            {
                _Sizes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Alignments
        private List<string> _Alignments;
        public List<string> Alignments
        {
            get
            {
                return _Alignments;
            }
            set
            {
                _Alignments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SchoolsOfMagic
        private List<string> _SchoolsOfMagic;
        [XmlSaveMode("")]
        public List<string> SchoolsOfMagic
        {
            get
            {
                return _SchoolsOfMagic;
            }
            set
            {
                _SchoolsOfMagic = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AoeShapes
        private List<string> _AoeShapes;
        [XmlSaveMode("")]
        public List<string> AoeShapes
        {
            get
            {
                return _AoeShapes;
            }
            set
            {
                _AoeShapes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AbilityTypes
        private List<string> _AbilityTypes;
        public List<string> AbilityTypes
        {
            get
            {
                return _AbilityTypes;
            }
            set
            {
                _AbilityTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Environments
        private List<string> _Environments;
        public List<string> Environments
        {
            get
            {
                return _Environments;
            }
            set
            {
                _Environments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IconNames
        private List<string> _IconNames;
        public List<string> IconNames
        {
            get
            {
                return _IconNames;
            }
            set
            {
                _IconNames = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ChallengeRatings
        private List<string> _ChallengeRatings;
        public List<string> ChallengeRatings
        {
            get
            {
                return _ChallengeRatings;
            }
            set
            {
                _ChallengeRatings = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DamageProclivities
        private List<string> _DamageProclivities;
        public List<string> DamageProclivities
        {
            get
            {
                return _DamageProclivities;
            }
            set
            {
                _DamageProclivities = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Themes
        private List<string> _Themes;
        public List<string> Themes
        {
            get
            {
                return _Themes;
            }
            set
            {
                _Themes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region VolumeSizes
        private List<string> _VolumeSizes;
        public List<string> VolumeSizes
        {
            get
            {
                return _VolumeSizes;
            }
            set
            {
                _VolumeSizes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region DrinkVolumes
        private List<string> _DrinkVolumes;
        public List<string> DrinkVolumes
        {
            get
            {
                return _DrinkVolumes;
            }
            set
            {
                _DrinkVolumes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region FishingEnvironments
        private List<string> _FishingEnvironments;
        public List<string> FishingEnvironments
        {
            get
            {
                return _FishingEnvironments;
            }
            set
            {
                _FishingEnvironments = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RarityLevels
        private List<string> _RarityLevels;
        public List<string> RarityLevels
        {
            get
            {
                return _RarityLevels;
            }
            set
            {
                _RarityLevels = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PlayerClasses
        private List<string> _PlayerClasses;
        public List<string> PlayerClasses
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
        #region PlayerRaces
        private List<string> _PlayerRaces;
        public List<string> PlayerRaces
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
        #region PlayerBackgrounds
        private List<string> _PlayerBackgrounds;
        public List<string> PlayerBackgrounds
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
        #region SpellsKnownPerLevelOptions
        private List<string> _SpellsKnownPerLevelOptions;
        public List<string> SpellsKnownPerLevelOptions
        {
            get
            {
                return _SpellsKnownPerLevelOptions;
            }
            set
            {
                _SpellsKnownPerLevelOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeatherRepository
        private List<string> _WeatherRepository;
        public List<string> WeatherRepository
        {
            get
            {
                return _WeatherRepository;
            }
            set
            {
                _WeatherRepository = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CalendarRepository
        private List<string> _CalendarRepository;
        public List<string> CalendarRepository
        {
            get
            {
                return _CalendarRepository;
            }
            set
            {
                _CalendarRepository = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SourcebookRepository
        private List<string> _SourcebookRepository;
        public List<string> SourcebookRepository
        {
            get
            {
                return _SourcebookRepository;
            }
            set
            {
                _SourcebookRepository = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NoteTypeRepository
        private List<string> _NoteTypeRepository;
        public List<string> NoteTypeRepository
        {
            get { return _NoteTypeRepository; }
            set { _NoteTypeRepository = value; NotifyPropertyChanged(); }
        }
        #endregion
        #region LanguageTypes
        public List<string> LanguageTypes
        {
            get
            {
                return new() { "Standard", "Exotic" };
            }
        }
        #endregion

        // Commands
        #region ProcessKeyboardShortcut
        private RelayCommand _ProcessKeyboardShortcut;
        public ICommand ProcessKeyboardShortcut
        {
            get
            {
                if (_ProcessKeyboardShortcut == null)
                {
                    _ProcessKeyboardShortcut = new RelayCommand(DoProcessKeyboardShortcut);
                }
                return _ProcessKeyboardShortcut;
            }
        }
        private void DoProcessKeyboardShortcut(object key)
        {
            switch (key.ToString())
            {
                case "CtrlS":
                    if (TabSelected_CreatureBuilder) { CreatureBuilderView.DoSaveCreatures(); }
                    if (TabSelected_SpellBuilder) { SpellBuilderView.DoSaveSpells(); }
                    if (TabSelected_Players) { CharacterBuilderView.DoSaveCharacters(); }
                    if (TabSelected_ItemBuilder) { ItemBuilderView.DoSaveItems(); }
                    if (TabSelected_Campaigns) { CampaignView.DoSaveCampaigns(); }
                    break;
                case "CtrlN":
                    if (TabSelected_Campaigns) { CampaignView.AddCampaign.Execute(null); }
                    if (TabSelected_CreatureBuilder) { CreatureBuilderView.AddCreature.Execute(null); }
                    if (TabSelected_ItemBuilder) { ItemBuilderView.AddItem.Execute(null); }
                    if (TabSelected_SpellBuilder) { SpellBuilderView.AddSpell.Execute(null); }
                    if (TabSelected_Players) { CharacterBuilderView.AddCharacter.Execute(null); }
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region OpenWebDriver
        private RelayCommand _OpenWebDriver;
        public ICommand OpenWebDriver
        {
            get
            {
                if (_OpenWebDriver == null)
                {
                    _OpenWebDriver = new RelayCommand(param => DoOpenWebDriver());
                }
                return _OpenWebDriver;
            }
        }
        private void DoOpenWebDriver()
        {
            try
            {
                WebDriver = CreateWebDriver();
                //WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                WebDriver.Navigate().GoToUrl("https://roll20.net/welcome");
                if (SettingsView.Roll20Email != "")
                {
                    WebDriver.FindElement(By.Id("email")).SendKeys(SettingsView.Roll20Email);
                }
                if (SettingsView.Roll20Password != "")
                {
                    WebDriver.FindElement(By.Id("password")).SendKeys(SettingsView.Roll20Password);
                }
                if (SettingsView.Roll20Email != "" && SettingsView.Roll20Password != "")
                {
                    WebDriver.FindElement(By.Id("login")).Click();
                    string game = "";
                    string character = "";
                    foreach (GameCharacterSelection pair in SettingsView.Roll20GameCharacterList)
                    {
                        if (pair.IsSelected == true)
                        {
                            game = pair.Game;
                            character = pair.Character;
                            break;
                        }
                    }
                    if (game != "")
                    {
                        Thread.Sleep(1000);
                        WebDriver.FindElement(By.LinkText(game)).Click();
                        WebDriver.FindElement(By.Id("playButton")).Click();
                        if (character != "")
                        {
                            CharacterModel defChar = Configuration.MainModelRef.CharacterBuilderView.Characters.FirstOrDefault(chr => chr.Name.Contains(character));
                            if (defChar != null)
                            {
                                Configuration.MainModelRef.TabSelected_Players = true;
                                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter = defChar;
                                Configuration.MainModelRef.CharacterBuilderView.ShowCharacterList = false;
                                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ShowActionHistory = false;
                                try
                                {
                                    Thread.Sleep(3000);
                                    IWebElement spkAs = WebDriver.FindElement(By.Id("speakingas"));
                                    spkAs.Click();
                                    spkAs.SendKeys(defChar.Name.Split()[0]);
                                    spkAs.SendKeys("\n");
                                }
                                catch { }
                                defChar.OutputLinkedToRoll20 = true;
                                HelperMethods.AddToRoll20Chat("/me has connected with " + ApplicationVersion + ".");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                new NotificationDialog(e.Message).ShowDialog();
            }
        }
        #endregion
        #region ResetDriver
        private RelayCommand _ResetDriver;
        public ICommand ResetDriver
        {
            get
            {
                if (_ResetDriver == null)
                {
                    _ResetDriver = new RelayCommand(param => DoResetDriver());
                }
                return _ResetDriver;
            }
        }
        private void DoResetDriver()
        {
            try
            {
                WebDriver.Quit();
            }
            catch { }
            SettingsView.WebDriverStatus = "Closed";
            foreach (CharacterModel character in CharacterBuilderView.Characters)
            {
                if (character.OutputLinkedToRoll20 == true)
                {
                    character.OutputLinkedToRoll20 = false;
                }
            }
        }
        #endregion
        #region GetDriver
        private RelayCommand _GetDriver;
        public ICommand GetDriver
        {
            get
            {
                if (_GetDriver == null)
                {
                    _GetDriver = new RelayCommand(param => DoGetDriver());
                }
                return _GetDriver;
            }
        }
        private void DoGetDriver()
        {
            try
            {
                System.Diagnostics.Process.Start("https://chromedriver.chromium.org/");
            }
            catch (Exception e)
            {
                HelperMethods.WriteToLogFile(e.Message, true);
            }
        }
        #endregion
        #region GenerateReport
        private RelayCommand _GenerateReport;
        public ICommand GenerateReport
        {
            get
            {
                if (_GenerateReport == null)
                {
                    _GenerateReport = new RelayCommand(DoGenerateReport);
                }
                return _GenerateReport;
            }
        }
        private void DoGenerateReport(object param)
        {
            if (param == null) { param = ""; }
            string message = param.ToString() switch
            {
                "Fish" => GenerateReport_Fish(),
                "Creature Sources" => GenerateReport_CreaturesMissingSource(),
                "Shop Coverage" => GenerateReport_ShopCoverage(),
                "Spells" => GenerateReport_Spells(),
                "Abilities" => GenerateReport_CustomAbilities(),
                _ => ""
            };
            if (message != "")
            {
                new NotificationDialog(message, "Report").ShowDialog();
            }
            else
            {
                new NotificationDialog("Invalid report type: " + param.ToString()).ShowDialog();
            }
        }
        #endregion

        // Private Methods
        private IWebDriver CreateWebDriver()
        {
            IWebDriver driver;
            ChromeOptions options;
            ChromeDriverService service;

            try
            {
                options = new ChromeOptions();
                options.AddArgument("no-sandbox");

                service = ChromeDriverService.CreateDefaultService(Configuration.WebDriverLocation);
                service.HideCommandPromptWindow = true;

                // Brave Browser test
                // options.BinaryLocation = @"C:\Program Files (x86)\BraveSoftware\Brave-Browser\Application\brave.exe";
                // it works but is not able to be shuffled in with other Brave tabs, is separate browser instance, likely for security reasons

                driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(180));
                driver.Manage().Window.Maximize();

                SettingsView.WebDriverStatus = "Active - Handle:" + driver.CurrentWindowHandle;
                return driver;
            }
            catch (Exception e)
            {
                string message = e.Message;
                if (message.Contains("Unable to locate element") && message.Contains("#speakingas")) { message = "Unable to interact with 'Speaking As' dropdown, please select manually in Roll20."; }
                if (message.Contains("This version of ChromeDriver only supports Chrome version")) { message += "\n\nUpdate Chrome or use the Get WebDriver button to download the most recent ChromeDriver."; }
                new NotificationDialog(message).ShowDialog();
                return null;
            }

        }

        // Private Methods - Reports
        private string GenerateReport_Fish()
        {
            string message = "Item Repository Fish Report:";
            foreach (string environment in Configuration.MainModelRef.FishingEnvironments)
            {
                message += "\nEnvironment: " + environment;
                foreach (string rarity in Configuration.MainModelRef.RarityLevels)
                {
                    message += "\n  Rarity: " + rarity + ", Count: " + Configuration.ItemRepository.Where(fish => fish.FishingEnvironment == environment && fish.Rarity == rarity).Count();
                }
            }
            return message;
        }
        private string GenerateReport_CreaturesMissingSource()
        {
            string message = "Creature Sourcebook Report:";
            message += "\nTotal Creatures: " + Configuration.CreatureRepository.Count();
            message += "\nCreatures Validated: " + Configuration.CreatureRepository.Where(crt => crt.IsValidated).Count();
            Dictionary<string, int> sourceCounts = new();
            foreach (CreatureModel creature in Configuration.CreatureRepository.Where(crt => crt.IsValidated))
            {
                if (sourceCounts.ContainsKey(creature.Sourcebook) == false) { sourceCounts.Add(creature.Sourcebook, 0); }
                sourceCounts[creature.Sourcebook]++;
            }
            foreach (KeyValuePair<string, int> pair in sourceCounts)
            {
                message += "\n" + ((pair.Key == "") ? "No Source" : pair.Key) + ": " + pair.Value;
            }
            return message;
        }
        private string GenerateReport_ShopCoverage()
        {
            string message = "Shop Coverage Report:";
            message += "\nMissing coverage for:";
            Dictionary<string, bool> itemTypes = new();
            foreach (string item in Configuration.ItemTypes)
            {
                itemTypes.Add(item, false);
            }
            foreach (ShopModel shop in ToolsView.Shops)
            {
                foreach (BoolOption option in shop.ItemTypes)
                {
                    if (option.Marked) { itemTypes[option.Name] = true; }
                }
            }
            foreach (KeyValuePair<string, bool> pair in itemTypes)
            {
                if (pair.Value == false) { message += "\n" + pair.Key; }
            }
            return message;
        }
        private string GenerateReport_Spells()
        {
            string message = "GAMMA Spells Report:";

            List<string> validatedButNoClasses = new();
            SortedDictionary<string, int> schoolCount = new();
            SortedDictionary<int, int> levelCount = new();

            foreach (SpellModel spell in SpellBuilderView.AllSpells)
            {
                if (schoolCount.ContainsKey(spell.SchoolOfMagic)) { schoolCount[spell.SchoolOfMagic]++; }
                else { schoolCount.Add(spell.SchoolOfMagic, 1); }

                if (levelCount.ContainsKey(spell.SpellLevel)) { levelCount[spell.SpellLevel]++; }
                else { levelCount.Add(spell.SpellLevel, 1); }

                if (spell.IsValidated && spell.SpellClasses.Count() == 0) { validatedButNoClasses.Add(spell.Name); }
            }

            if (schoolCount.Count() > 0)
            {
                
                message += "\n\nSpell count by school:\n";
                message += HelperMethods.GetStringFromDictionary(schoolCount);
            }

            if (levelCount.Count() > 0)
            {
                message += "\n\nSpell count by level:\n";
                message += HelperMethods.GetStringFromDictionary(levelCount, "Level ");
            }

            if (validatedButNoClasses.Count() > 0)
            {
                message += "\n\nValidated but no classes selected:\n";
                message += HelperMethods.GetStringFromList(validatedButNoClasses, "\n");
            }

            return message;
        }
        private string GenerateReport_CustomAbilities()
        {
            string message = "GAMMA Custom Ability Report:";

            List<string> creatureAbilityIssues = new();
            foreach (CreatureModel creature in CreatureBuilderView.AllCreatures)
            {
                creatureAbilityIssues.AddRange(CheckCustomAbility(creature.Name, creature.Abilities.ToList()));
            }

            List<string> spellAbilityIssues = new();
            foreach (SpellModel spell in SpellBuilderView.AllSpells)
            {
                spellAbilityIssues.AddRange(CheckCustomAbility(spell.Name, spell.PrimaryAbilities.ToList()));
                spellAbilityIssues.AddRange(CheckCustomAbility(spell.Name, spell.SecondaryAbilities.ToList()));
            }

            List<string> characterAbilityIssues = new();
            foreach (CharacterModel character in CharacterBuilderView.Characters)
            {
                characterAbilityIssues.AddRange(CheckCustomAbility(character.Name, character.Abilities.ToList()));
            }

            message += GetStringFromList(creatureAbilityIssues, "\n\nCreatures", "\n");
            message += GetStringFromList(creatureAbilityIssues, "\n\nSpells", "\n");
            message += GetStringFromList(creatureAbilityIssues, "\n\nCharacters", "\n");

            if (message == "GAMMA Custom Ability Report:") { message += "\n\nNo issues found."; }

            return message;

        }
        private List<string> CheckCustomAbility(string ownerName, List<CustomAbility> abilities)
        {
            List<string> issues = new();
            foreach (CustomAbility ability in abilities)
            {
                if (ability.QuantityToPerform == 0)
                {
                    issues.Add("Quantity Zero: " + ownerName + " - " + ability.Name);
                }
                foreach (CAPreAction preAction in ability.PreActions)
                {
                    // Check if PreAction Target is not a valid Variable name
                    CAVariable variable = ability.Variables.FirstOrDefault(v => v.Name == preAction.Target);
                    if (variable == null)
                    {
                        issues.Add("Invalid PreAction Target: " + ownerName + " - " + ability.Name + " - PreAction " + ability.PreActions.IndexOf(preAction) + " - " + preAction.Target);
                    }
                }
            }
            return issues;
        }
        private string GetStringFromList(List<string> lines, string stringPrefix = "", string linePrefix = "")
        {
            string message = "";
            if (lines.Count() > 0)
            {
                message += stringPrefix;
                foreach (string msg in lines)
                {
                    message += "\n" + msg;
                }
            }
            return message;
        }

    }
}
