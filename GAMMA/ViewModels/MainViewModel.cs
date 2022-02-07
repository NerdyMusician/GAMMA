using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.Models.WebAutomation;
using GAMMA.Toolbox;
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
            
            ApplicationVersion = "GAMMA 1.29.00 beta";

            Configuration.LoadComplete = true;

        }

        public IWebDriver WebDriver = null;
        public bool IsWebDriverOpen = false;

        // Databound Properties
        #region CreatureBuilderView
        private CreatureBuilderViewModel _CreatureBuilderView;
        public CreatureBuilderViewModel CreatureBuilderView
        {
            get => _CreatureBuilderView;
            set => SetAndNotify(ref _CreatureBuilderView, value);
        }
        #endregion
        #region ItemBuilderView
        private ItemBuilderViewModel _ItemBuilderView;
        public ItemBuilderViewModel ItemBuilderView
        {
            get => _ItemBuilderView;
            set => SetAndNotify(ref _ItemBuilderView, value);
        }
        #endregion
        #region SpellBuilderView
        private SpellBuilderViewModel _SpellBuilderView;
        public SpellBuilderViewModel SpellBuilderView
        {
            get => _SpellBuilderView;
            set => SetAndNotify(ref _SpellBuilderView, value);
        }
        #endregion
        #region CharacterBuilderView
        private CharacterBuilderViewModel _CharacterBuilderView;
        public CharacterBuilderViewModel CharacterBuilderView
        {
            get => _CharacterBuilderView;
            set => SetAndNotify(ref _CharacterBuilderView, value);
        }
        #endregion
        #region ToolsView
        private ToolsViewModel _ToolsView;
        public ToolsViewModel ToolsView
        {
            get => _ToolsView;
            set => SetAndNotify(ref _ToolsView, value);
        }
        #endregion
        #region AudioView
        private AudioViewModel _AudioView;
        public AudioViewModel AudioView
        {
            get => _AudioView;
            set => SetAndNotify(ref _AudioView, value);
        }
        #endregion
        #region SettingsView
        private SettingsViewModel _SettingsView;
        [XmlSaveMode(XSME.Single)]
        public SettingsViewModel SettingsView
        {
            get => _SettingsView;
            set => SetAndNotify(ref _SettingsView, value);
        }
        #endregion
        #region CampaignView
        private CampaignBuilderViewModel _CampaignView;
        public CampaignBuilderViewModel CampaignView
        {
            get => _CampaignView;
            set => SetAndNotify(ref _CampaignView, value);
        }
        #endregion

        #region WebDriverStatus
        private string _WebDriverStatus;
        public string WebDriverStatus
        {
            get => _WebDriverStatus;
            set => SetAndNotify(ref _WebDriverStatus, value);
        }
        #endregion
        #region ApplicationVersion
        private string _ApplicationVersion;
        public string ApplicationVersion
        {
            get => _ApplicationVersion;
            set => SetAndNotify(ref _ApplicationVersion, value);
        }
        #endregion

        // Databound Properties - Tab Selection
        #region TabSelected_CreatureBuilder
        private bool _TabSelected_CreatureBuilder;
        public bool TabSelected_CreatureBuilder
        {
            get => _TabSelected_CreatureBuilder;
            set => SetAndNotify(ref _TabSelected_CreatureBuilder, value);
        }
        #endregion
        #region TabSelected_SpellBuilder
        private bool _TabSelected_SpellBuilder;
        public bool TabSelected_SpellBuilder
        {
            get => _TabSelected_SpellBuilder;
            set => SetAndNotify(ref _TabSelected_SpellBuilder, value);
        }
        #endregion
        #region TabSelected_Players
        private bool _TabSelected_Players;
        public bool TabSelected_Players
        {
            get => _TabSelected_Players;
            set => SetAndNotify(ref _TabSelected_Players, value);
        }
        #endregion
        #region TabSelected_ItemBuilder
        private bool _TabSelected_ItemBuilder;
        public bool TabSelected_ItemBuilder
        {
            get => _TabSelected_ItemBuilder;
            set => SetAndNotify(ref _TabSelected_ItemBuilder, value);
        }
        #endregion
        #region TabSelected_Campaigns
        private bool _TabSelected_Campaigns;
        public bool TabSelected_Campaigns
        {
            get => _TabSelected_Campaigns;
            set => SetAndNotify(ref _TabSelected_Campaigns, value);
        }
        #endregion

        // Databound Properties - Dropdown Sources
        #region GameTypes
        private List<string> _GameTypes;
        public List<string> GameTypes
        {
            get => _GameTypes;
            set => SetAndNotify(ref _GameTypes, value);
        }
        #endregion
        #region Sources
        private List<string> _Sources;
        public List<string> Sources
        {
            get => _Sources;
            set => SetAndNotify(ref _Sources, value);
        }
        #endregion
        #region DiceSides
        private List<string> _DiceSides;
        public List<string> DiceSides
        {
            get => _DiceSides;
            set => SetAndNotify(ref _DiceSides, value);
        }
        #endregion
        #region ItemTypes
        private List<string> _ItemTypes;
        public List<string> ItemTypes
        {
            get => _ItemTypes;
            set => SetAndNotify(ref _ItemTypes, value);
        }
        #endregion
        #region DamageTypes
        private List<string> _DamageTypes;
        public List<string> DamageTypes
        {
            get => _DamageTypes;
            set => SetAndNotify(ref _DamageTypes, value);
        }
        #endregion
        #region CreatureTypes
        private List<string> _CreatureTypes;
        public List<string> CreatureTypes
        {
            get => _CreatureTypes;
            set => SetAndNotify(ref _CreatureTypes, value);
        }
        #endregion
        #region CreatureCategories
        private List<string> _CreatureCategories;
        public List<string> CreatureCategories
        {
            get => _CreatureCategories;
            set => SetAndNotify(ref _CreatureCategories, value);
        }
        #endregion
        #region Sizes
        private List<string> _Sizes;
        public List<string> Sizes
        {
            get => _Sizes;
            set => SetAndNotify(ref _Sizes, value);
        }
        #endregion
        #region Alignments
        private List<string> _Alignments;
        public List<string> Alignments
        {
            get => _Alignments;
            set => SetAndNotify(ref _Alignments, value);
        }
        #endregion
        #region SchoolsOfMagic
        private List<string> _SchoolsOfMagic;
        public List<string> SchoolsOfMagic
        {
            get => _SchoolsOfMagic;
            set => SetAndNotify(ref _SchoolsOfMagic, value);
        }
        #endregion
        #region AoeShapes
        private List<string> _AoeShapes;
        public List<string> AoeShapes
        {
            get => _AoeShapes;
            set => SetAndNotify(ref _AoeShapes, value);
        }
        #endregion
        #region AbilityTypes
        private List<string> _AbilityTypes;
        public List<string> AbilityTypes
        {
            get => _AbilityTypes;
            set => SetAndNotify(ref _AbilityTypes, value);
        }
        #endregion
        #region Environments
        private List<string> _Environments;
        public List<string> Environments
        {
            get => _Environments;
            set => SetAndNotify(ref _Environments, value);
        }
        #endregion
        #region IconNames
        private List<string> _IconNames;
        public List<string> IconNames
        {
            get => _IconNames;
            set => SetAndNotify(ref _IconNames, value);
        }
        #endregion
        #region ChallengeRatings
        private List<string> _ChallengeRatings;
        public List<string> ChallengeRatings
        {
            get => _ChallengeRatings;
            set => SetAndNotify(ref _ChallengeRatings, value);
        }
        #endregion
        #region DamageProclivities
        private List<string> _DamageProclivities;
        public List<string> DamageProclivities
        {
            get => _DamageProclivities;
            set => SetAndNotify(ref _DamageProclivities, value);
        }
        #endregion
        #region Themes
        private List<string> _Themes;
        public List<string> Themes
        {
            get => _Themes;
            set => SetAndNotify(ref _Themes, value);
        }
        #endregion
        #region VolumeSizes
        private List<string> _VolumeSizes;
        public List<string> VolumeSizes
        {
            get => _VolumeSizes;
            set => SetAndNotify(ref _VolumeSizes, value);
        }
        #endregion
        #region DrinkVolumes
        private List<string> _DrinkVolumes;
        public List<string> DrinkVolumes
        {
            get => _DrinkVolumes;
            set => SetAndNotify(ref _DrinkVolumes, value);
        }
        #endregion
        #region FishingEnvironments
        private List<string> _FishingEnvironments;
        public List<string> FishingEnvironments
        {
            get => _FishingEnvironments;
            set => SetAndNotify(ref _FishingEnvironments, value);
        }
        #endregion
        #region RarityLevels
        private List<string> _RarityLevels;
        public List<string> RarityLevels
        {
            get => _RarityLevels;
            set => SetAndNotify(ref _RarityLevels, value);
        }
        #endregion
        #region PlayerClasses
        private List<string> _PlayerClasses;
        public List<string> PlayerClasses
        {
            get => _PlayerClasses;
            set => SetAndNotify(ref _PlayerClasses, value);
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
        #region PlayerRaces
        private List<string> _PlayerRaces;
        public List<string> PlayerRaces
        {
            get => _PlayerRaces;
            set => SetAndNotify(ref _PlayerRaces, value);
        }
        #endregion
        #region PlayerBackgrounds
        private List<string> _PlayerBackgrounds;
        public List<string> PlayerBackgrounds
        {
            get => _PlayerBackgrounds;
            set => SetAndNotify(ref _PlayerBackgrounds, value);
        }
        #endregion
        #region SpellsKnownPerLevelOptions
        private List<string> _SpellsKnownPerLevelOptions;
        public List<string> SpellsKnownPerLevelOptions
        {
            get => _SpellsKnownPerLevelOptions;
            set => SetAndNotify(ref _SpellsKnownPerLevelOptions, value);
        }
        #endregion
        #region WeatherRepository
        private List<string> _WeatherRepository;
        public List<string> WeatherRepository
        {
            get => _WeatherRepository;
            set => SetAndNotify(ref _WeatherRepository, value);
        }
        #endregion
        #region CalendarRepository
        private List<string> _CalendarRepository;
        public List<string> CalendarRepository
        {
            get => _CalendarRepository;
            set => SetAndNotify(ref _CalendarRepository, value);
        }
        #endregion
        #region SourcebookRepository
        private List<string> _SourcebookRepository;
        public List<string> SourcebookRepository
        {
            get => _SourcebookRepository;
            set => SetAndNotify(ref _SourcebookRepository, value);
        }
        #endregion
        #region NoteTypeRepository
        private List<string> _NoteTypeRepository;
        public List<string> NoteTypeRepository
        {
            get => _NoteTypeRepository;
            set => SetAndNotify(ref _NoteTypeRepository, value);
        }
        #endregion
        #region LanguageTypes
        public static List<string> LanguageTypes
        {
            get => new() { "Standard", "Exotic" };
        }
        #endregion

        // Commands
        #region ProcessKeyboardShortcut
        public ICommand ProcessKeyboardShortcut => new RelayCommand(DoProcessKeyboardShortcut);
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
        public ICommand OpenWebDriver => new RelayCommand(param => DoOpenWebDriver());
        private void DoOpenWebDriver()
        {
            try
            {
                WebDriver = CreateWebDriver();
                bool connectionSuccessful = true;
                foreach (WebActionModel webAction in SettingsView.StartupWebActions)
                {
                    if (webAction.TargetElementStack.Count <= 0 && webAction.ShowTargetStack)
                    {
                        HelperMethods.NotifyUser("No elements provided for web action.");
                        connectionSuccessful = false;
                        break;
                    }
                    if (!webAction.PerformWebAction(ref WebDriver))
                    {
                        HelperMethods.NotifyUser("Web Action Failed: " + webAction.InteractionType + " > " + webAction.TargetElementStack.Last().TargetElementMatchText);
                        connectionSuccessful = false;
                        break;
                    }
                }
                if (connectionSuccessful == false) { return; }
                GameCharacterSelection game = Configuration.MainModelRef.SettingsView.Roll20GameCharacterList.FirstOrDefault(g => g.IsSelected);
                if (game == null) { return; }
                CharacterModel defChar = Configuration.MainModelRef.CharacterBuilderView.Characters.FirstOrDefault(chr => chr.Name.Contains(game.Character));
                if (defChar != null)
                {
                    Configuration.MainModelRef.TabSelected_Players = true;
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter = defChar;
                    Configuration.MainModelRef.CharacterBuilderView.ShowCharacterList = false;
                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ShowActionHistory = false;
                    defChar.OutputLinkedToRoll20 = true;
                }
            }
            catch (Exception e)
            {
                HelperMethods.NotifyUser(e.Message);
            }
            return;
            //try
            //{
            //    WebDriver = CreateWebDriver();
            //    //WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //    WebDriver.Navigate().GoToUrl("https://roll20.net/welcome");
            //    if (SettingsView.Roll20Email != "")
            //    {
            //        WebDriver.FindElement(By.Id("email")).SendKeys(SettingsView.Roll20Email);
            //    }
            //    if (SettingsView.Roll20Password != "")
            //    {
            //        WebDriver.FindElement(By.Id("password")).SendKeys(SettingsView.Roll20Password);
            //    }
            //    if (SettingsView.Roll20Email != "" && SettingsView.Roll20Password != "")
            //    {
            //        WebDriver.FindElement(By.Id("login")).Click();
            //        string game = "";
            //        string character = "";
            //        foreach (GameCharacterSelection pair in SettingsView.Roll20GameCharacterList)
            //        {
            //            if (pair.IsSelected == true)
            //            {
            //                game = pair.Game;
            //                character = pair.Character;
            //                break;
            //            }
            //        }
            //        if (game != "")
            //        {
            //            Thread.Sleep(1000);
            //            WebDriver.FindElement(By.LinkText(game)).Click();
            //            WebDriver.FindElement(By.Id("playButton")).Click();
            //            if (character != "")
            //            {
            //                CharacterModel defChar = Configuration.MainModelRef.CharacterBuilderView.Characters.FirstOrDefault(chr => chr.Name.Contains(character));
            //                if (defChar != null)
            //                {
            //                    Configuration.MainModelRef.TabSelected_Players = true;
            //                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter = defChar;
            //                    Configuration.MainModelRef.CharacterBuilderView.ShowCharacterList = false;
            //                    Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ShowActionHistory = false;
            //                    try
            //                    {
            //                        Thread.Sleep(3000);
            //                        IWebElement spkAs = WebDriver.FindElement(By.Id("speakingas"));
            //                        spkAs.Click();
            //                        spkAs.SendKeys(defChar.Name.Split()[0]);
            //                        spkAs.SendKeys("\n");
            //                    }
            //                    catch { }
            //                    defChar.OutputLinkedToRoll20 = true;
            //                    HelperMethods.AddToRoll20Chat("/me has connected with " + ApplicationVersion + ".");
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    HelperMethods.NotifyUser(e.Message);
            //}
        }
        #endregion
        #region ResetDriver
        public ICommand ResetDriver => new RelayCommand(param => DoResetDriver());
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
        public ICommand GetDriver => new RelayCommand(param => DoGetDriver());
        private static void DoGetDriver()
        {
            try
            {
                System.Diagnostics.Process.Start("explorer", "https://chromedriver.chromium.org/");
            }
            catch (Exception e)
            {
                HelperMethods.WriteToLogFile(e.Message, true);
            }
        }
        #endregion
        #region GenerateReport
        public ICommand GenerateReport => new RelayCommand(DoGenerateReport);
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
                HelperMethods.NotifyUser(message, HelperMethods.UserNotificationType.Report);
            }
            else
            {
                HelperMethods.NotifyUser("Invalid report type: " + param.ToString());
            }
        }
        #endregion

        // Public Methods

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
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                SettingsView.WebDriverStatus = "Active - Handle:" + driver.CurrentWindowHandle;
                return driver;
            }
            catch (Exception e)
            {
                string message = e.Message;
                if (message.Contains("Unable to locate element") && message.Contains("#speakingas")) { message = "Unable to interact with 'Speaking As' dropdown, please select manually in Roll20."; }
                if (message.Contains("This version of ChromeDriver only supports Chrome version")) { message += "\n\nUpdate Chrome or use the Get WebDriver button to download the most recent ChromeDriver."; }
                HelperMethods.NotifyUser(message);
                return null;
            }

        }

        // Private Methods - Reports
        private static string GenerateReport_Fish()
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
        private static string GenerateReport_CreaturesMissingSource()
        {
            string message = "Creature Sourcebook Report:";
            message += "\nTotal Creatures: " + Configuration.CreatureRepository.Count;
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

                if (spell.IsValidated && spell.SpellClasses.Count == 0) { validatedButNoClasses.Add(spell.Name); }
            }

            if (schoolCount.Count > 0)
            {
                
                message += "\n\nSpell count by school:\n";
                message += HelperMethods.GetStringFromDictionary(schoolCount);
            }

            if (levelCount.Count > 0)
            {
                message += "\n\nSpell count by level:\n";
                message += HelperMethods.GetStringFromDictionary(levelCount, "Level ");
            }

            if (validatedButNoClasses.Count > 0)
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
            message += GetStringFromList(spellAbilityIssues, "\n\nSpells", "\n");
            message += GetStringFromList(characterAbilityIssues, "\n\nCharacters", "\n");

            if (message == "GAMMA Custom Ability Report:") { message += "\n\nNo issues found."; }

            return message;

        }
        private static List<string> CheckCustomAbility(string ownerName, List<CustomAbility> abilities)
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
        private static string GetStringFromList(List<string> lines, string stringPrefix = "", string linePrefix = "")
        {
            string message = "";
            if (lines.Count > 0)
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
