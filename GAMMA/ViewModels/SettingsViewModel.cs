using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class SettingsViewModel : BaseModel
    {
        // Constructors
        public SettingsViewModel()
        {
            Roll20GameCharacterList = new();
        }

        // Databound Properties
        // Settings - Application
        #region InDmModeModern
        private bool _InDmModeModern;
        [XmlSaveMode(XSME.Single)]
        public bool InDmModeModern
        {
            get => _InDmModeModern;
            set => SetAndNotify(ref _InDmModeModern, value);
        }
        #endregion
        #region InDmModeClassic
        private bool _InDmModeClassic;
        [XmlSaveMode(XSME.Single)]
        public bool InDmModeClassic
        {
            get => _InDmModeClassic;
            set => SetAndNotify(ref _InDmModeClassic, value);
        }
        #endregion
        #region ShowData
        private bool _ShowData;
        [XmlSaveMode(XSME.Single)]
        public bool ShowData
        {
            get => _ShowData;
            set => SetAndNotify(ref _ShowData, value);
        }
        #endregion
        #region ShowMiniatureUtilization
        private bool _ShowMiniatureUtilization;
        [XmlSaveMode(XSME.Single)]
        public bool ShowMiniatureUtilization
        {
            get => _ShowMiniatureUtilization;
            set => SetAndNotify(ref _ShowMiniatureUtilization, value);
        }
        #endregion

        // Settings - Gameplay
        #region ShowDiceRolls
        private bool _ShowDiceRolls;
        [XmlSaveMode(XSME.Single)]
        public bool ShowDiceRolls
        {
            get => _ShowDiceRolls;
            set => SetAndNotify(ref _ShowDiceRolls, value);
        }
        #endregion
        #region UseCriticalHitMaxDamage
        private bool _UseCriticalHitMaxDamage;
        [XmlSaveMode(XSME.Single)]
        public bool UseCriticalHitMaxDamage
        {
            get => _UseCriticalHitMaxDamage;
            set => SetAndNotify(ref _UseCriticalHitMaxDamage, value);
        }
        #endregion
        #region UsePlatinum
        private bool _UsePlatinum;
        [XmlSaveMode(XSME.Single)]
        public bool UsePlatinum
        {
            get => _UsePlatinum;
            set
            {
                _UsePlatinum = value;
                NotifyPropertyChanged();

                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                foreach (CharacterModel character in Configuration.MainModelRef.CharacterBuilderView.Characters)
                {
                    character.UpdateInventoryStats();
                }
            }
        }
        #endregion
        #region UseVariantEncumbrance
        private bool _UseVariantEncumbrance;
        [XmlSaveMode(XSME.Single)]
        public bool UseVariantEncumbrance
        {
            get => _UseVariantEncumbrance;
            set
            {
                _UseVariantEncumbrance = value;
                NotifyPropertyChanged();

                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                foreach (CharacterModel character in Configuration.MainModelRef.CharacterBuilderView.Characters)
                {
                    character.UpdateInventoryStats();
                }
            }
        }
        #endregion
        #region UseCoinWeight
        private bool _UseCoinWeight;
        [XmlSaveMode(XSME.Single)]
        public bool UseCoinWeight
        {
            get => _UseCoinWeight;
            set
            {
                _UseCoinWeight = value;
                NotifyPropertyChanged();

                if (Configuration.MainModelRef.CharacterBuilderView == null) { return; }
                foreach (CharacterModel character in Configuration.MainModelRef.CharacterBuilderView.Characters)
                {
                    character.UpdateInventoryStats();
                }
            }
        }
        #endregion
        #region UseAveragedHitPoints
        private bool _UseAveragedHitPoints;
        [XmlSaveMode(XSME.Single)]
        public bool UseAveragedHitPoints
        {
            get => _UseAveragedHitPoints;
            set => SetAndNotify(ref _UseAveragedHitPoints, value);
        }
        #endregion
        #region UseExperiencePoints
        private bool _UseExperiencePoints;
        [XmlSaveMode(XSME.Single)]
        public bool UseExperiencePoints
        {
            get => _UseExperiencePoints;
            set => SetAndNotify(ref _UseExperiencePoints, value);
        }
        #endregion
        #region EnforceSpellComponentConsumption
        private bool _EnforceSpellComponentConsumption;
        [XmlSaveMode(XSME.Single)]
        public bool EnforceSpellComponentConsumption
        {
            get => _EnforceSpellComponentConsumption;
            set => SetAndNotify(ref _EnforceSpellComponentConsumption, value);
        }
        #endregion
        #region EnforceCreatureSpellSlots
        private bool _EnforceCreatureSpellSlots;
        [XmlSaveMode(XSME.Single)]
        public bool EnforceCreatureSpellSlots
        {
            get => _EnforceCreatureSpellSlots;
            set => SetAndNotify(ref _EnforceCreatureSpellSlots, value);
        }
        #endregion

        // Settings - Autosave
        #region EnableCharacterAutosave
        private bool _EnableCharacterAutosave;
        [XmlSaveMode(XSME.Single)]
        public bool EnableCharacterAutosave
        {
            get => _EnableCharacterAutosave;
            set => SetAndNotify(ref _EnableCharacterAutosave, value);
        }
        #endregion
        #region EnableCampaignsAutosave
        private bool _EnableCampaignsAutosave;
        [XmlSaveMode(XSME.Single)]
        public bool EnableCampaignsAutosave
        {
            get => _EnableCampaignsAutosave;
            set => SetAndNotify(ref _EnableCampaignsAutosave, value);
        }
        #endregion

        // Settings - Exitsave
        #region EnableCharacterExitsave
        private bool _EnableCharacterExitsave;
        [XmlSaveMode(XSME.Single)]
        public bool EnableCharacterExitsave
        {
            get => _EnableCharacterExitsave;
            set => SetAndNotify(ref _EnableCharacterExitsave, value);
        }
        #endregion
        #region PromptCharacterExitsave
        private bool _PromptCharacterExitsave;
        [XmlSaveMode(XSME.Single)]
        public bool PromptCharacterExitsave
        {
            get => _PromptCharacterExitsave;
            set => SetAndNotify(ref _PromptCharacterExitsave, value);
        }
        #endregion

        // Settings - WebDriver
        #region WebDriverStatus
        private string _WebDriverStatus;
        public string WebDriverStatus
        {
            get => _WebDriverStatus;
            set => SetAndNotify(ref _WebDriverStatus, value);
        }
        #endregion
        #region Roll20Email
        private string _Roll20Email;
        [XmlSaveMode(XSME.Single)]
        public string Roll20Email
        {
            get => _Roll20Email;
            set => SetAndNotify(ref _Roll20Email, value);
        }
        #endregion
        #region Roll20Password
        private string _Roll20Password;
        [XmlSaveMode(XSME.Single)]
        public string Roll20Password
        {
            get => _Roll20Password;
            set => SetAndNotify(ref _Roll20Password, value);
        }
        #endregion
        #region Roll20GameCharacterList
        private ObservableCollection<GameCharacterSelection> _Roll20GameCharacterList;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<GameCharacterSelection> Roll20GameCharacterList
        {
            get => _Roll20GameCharacterList;
            set => SetAndNotify(ref _Roll20GameCharacterList, value);
        }
        #endregion
        #region ClearPasswordOnClose
        private bool _ClearPasswordOnClose;
        [XmlSaveMode(XSME.Single)]
        public bool ClearPasswordOnClose
        {
            get => _ClearPasswordOnClose;
            set => SetAndNotify(ref _ClearPasswordOnClose, value);
        }
        #endregion

        // Settings - Audio
        #region EnableSoundEffects
        private bool _EnableSoundEffects;
        [XmlSaveMode(XSME.Single)]
        public bool EnableSoundEffects
        {
            get => _EnableSoundEffects;
            set => SetAndNotify(ref _EnableSoundEffects, value);
        }
        #endregion
        #region EnableSfx_DiceRoll
        private bool _EnableSfx_DiceRoll;
        [XmlSaveMode(XSME.Single)]
        public bool EnableSfx_DiceRoll
        {
            get => _EnableSfx_DiceRoll;
            set => SetAndNotify(ref _EnableSfx_DiceRoll, value);
        }
        #endregion
        #region EnableSfx_ShopItemMove
        private bool _EnableSfx_ShopItemMove;
        [XmlSaveMode(XSME.Single)]
        public bool EnableSfx_ShopItemMove
        {
            get => _EnableSfx_ShopItemMove;
            set => SetAndNotify(ref _EnableSfx_ShopItemMove, value);
        }
        #endregion
        #region EnableSfx_ShopGreeting
        private bool _EnableSfx_ShopGreeting;
        [XmlSaveMode(XSME.Single)]
        public bool EnableSfx_ShopGreeting
        {
            get => _EnableSfx_ShopGreeting;
            set => SetAndNotify(ref _EnableSfx_ShopGreeting, value);
        }
        #endregion

        // Commands
        #region AddGameCharacterPair
        public ICommand AddGameCharacterPair => new RelayCommand(param => DoAddGameCharacterPair());
        private void DoAddGameCharacterPair()
        {
            Roll20GameCharacterList.Add(new GameCharacterSelection());
        }
        #endregion
        #region ViewFile
        public ICommand ViewFile => new RelayCommand(DoViewFile);
        private void DoViewFile(object param)
        {
            if (param == null) { return; }
            string fileType = param.ToString();
            string fileName = "";
            string[] files;
            string currentDirectory = Environment.CurrentDirectory + "\\";
            switch (fileType)
            {
                case "Documentation":
                    files = Directory.GetFiles(currentDirectory, "GAMMA*.pdf");
                    if (files.Length > 0)
                    {
                        fileName = files[0];
                    }
                    break;
                case "Change Log":
                    files = Directory.GetFiles(currentDirectory, "Change.log");
                    if (files.Length > 0)
                    {
                        fileName = files[0];
                    }
                    break;
                case "Patreon":
                    fileName = "https://www.patreon.com/gammatoolkit";
                    break;
                case "Reddit":
                    fileName = "https://www.reddit.com/r/GammaToolkit/";
                    break;
                case "GitHub":
                    fileName = "https://github.com/NerdyMusician/GAMMA";
                    break;
                default:
                    return;
            }
            if (fileName == "") { return; }
            try
            {
                System.Diagnostics.Process.Start("explorer",fileName);
            }
            catch (Exception e)
            {
                HelperMethods.WriteToLogFile(e.Message, true);
            }
        }
        #endregion
        #region SetReleaseSettings
        public ICommand SetReleaseSettings => new RelayCommand(param => DoSetReleaseSettings());
        private void DoSetReleaseSettings()
        {
            // Application Settings
            InDmModeModern = false;
            InDmModeClassic = false;
            ShowData = false;
            ShowMiniatureUtilization = false;

            // Gameplay Settings
            ShowDiceRolls = true;
            UseCriticalHitMaxDamage = true;
            UsePlatinum = false;
            UseVariantEncumbrance = false;
            UseCoinWeight = true;
            UseAveragedHitPoints = false;
            UseExperiencePoints = false;
            EnforceSpellComponentConsumption = true;
            EnforceCreatureSpellSlots = true;

            // Autosave Settings
            EnableCampaignsAutosave = false;
            EnableCharacterAutosave = false;

            // Exitsave Settings
            EnableCharacterExitsave = true;
            PromptCharacterExitsave = true;

            // Audio Settings
            EnableSoundEffects = true;
            EnableSfx_DiceRoll = true;
            EnableSfx_ShopGreeting = false;
            EnableSfx_ShopItemMove = true;

            // WebDriver Settings
            Roll20Email = "";
            Roll20Password = "";
            Roll20GameCharacterList.Clear();
        }
        #endregion
        #region ClearLogFile
        public ICommand ClearLogFile => new RelayCommand(param => DoClearLogFile());
        private void DoClearLogFile()
        {
            HelperMethods.ClearLogFile(true);
        }
        #endregion
        #region RunOperation
        public ICommand RunOperation => new RelayCommand(param => DoRunOperation());
        private void DoRunOperation()
        {
            string task = "SPELL CLASS CHECK";
            if (task == "DISABLED") 
            {
                HelperMethods.WriteToLogFile("If you're seeing this message, the dev forgot to remove a button. This method is simply used to run one-off data cleanup tasks.", true);
                return; 
            }
            

            if (task == "SPELL CLASS CHECK")
            {
                foreach (SpellModel spell in Configuration.MainModelRef.SpellBuilderView.AllSpells)
                {
                    if (spell.SpellClasses.Count() == 0) { spell.IsValidated = false; }
                }
                Configuration.MainModelRef.SpellBuilderView.ClearSpellSearch.Execute(null);
            }

            HelperMethods.WriteToLogFile("Task \"" + task + "\" completed.", true);
            
        }
        #endregion
        #region ImportPreviousRelease
        public ICommand ImportPreviousRelease => new RelayCommand(param => DoImportPreviousRelease());
        private void DoImportPreviousRelease()
        {
            FolderBrowserDialog folderDialog = new();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = folderDialog.SelectedPath.Split('\\').Last() == "Data" ? Directory.GetParent(folderDialog.SelectedPath).FullName + "\\" : folderDialog.SelectedPath + "\\";
                List<string> messages = new();

                // CORE DATA
                if (File.Exists(filepath + Configuration.ItemDataFilePath))
                {
                    DataImport.ImportData_Items(filepath + Configuration.ItemDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Item Data File not found."); }

                if (File.Exists(filepath + Configuration.NoteTypeDataFilePath))
                {
                    DataImport.ImportData_NoteTypes(filepath + Configuration.NoteTypeDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Note Type Data File not found."); }

                if (File.Exists(filepath + Configuration.SpellDataFilePath))
                {
                    DataImport.ImportData_Spells(filepath + Configuration.SpellDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Spell Data File not found."); }

                if (File.Exists(filepath + Configuration.CreatureDataFilePath))
                {
                    DataImport.ImportData_Creatures(filepath + Configuration.CreatureDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Creature Data File not found."); }

                if (File.Exists(filepath + Configuration.SettingsDataFilePath))
                {
                    DataImport.ImportData_Settings(filepath + Configuration.SettingsDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Settings Data File not found."); }

                // MISC DATA
                if (File.Exists(filepath + Configuration.ShopDataFilePath))
                {
                    DataImport.ImportData_Shops(filepath + Configuration.ShopDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Shop Data File not found."); }

                if (File.Exists(filepath + Configuration.LootBoxDataFilePath))
                {
                    DataImport.ImportData_LootBoxes(filepath + Configuration.LootBoxDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Loot Box Data File not found."); }

                if (File.Exists(filepath + Configuration.RollTableDataFilePath))
                {
                    DataImport.ImportData_RollTables(filepath + Configuration.RollTableDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Roll Table Data File not found."); }

                if (File.Exists(filepath + Configuration.LanguageDataFilePath))
                {
                    DataImport.ImportData_Languages(filepath + Configuration.LanguageDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Language Data File not found."); }

                if (File.Exists(filepath + Configuration.WeatherDataFilePath))
                {
                    DataImport.ImportData_Weather(filepath + Configuration.WeatherDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Weather Data File not found."); }

                if (File.Exists(filepath + Configuration.CalendarDataFilePath))
                {
                    DataImport.ImportData_Calendars(filepath + Configuration.CalendarDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Calendar Data File not found."); }

                // PLAYER DATA
                if (File.Exists(filepath + Configuration.PlayerClassDataFilePath))
                {
                    DataImport.ImportData_PlayerClasses(filepath + Configuration.PlayerClassDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Class Data File not found."); }

                if (File.Exists(filepath + Configuration.PlayerSubclassDataFilePath))
                {
                    DataImport.ImportData_PlayerSubclasses(filepath + Configuration.PlayerSubclassDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Subclass Data File not found."); }

                if (File.Exists(filepath + Configuration.PlayerRaceDataFilePath))
                {
                    DataImport.ImportData_PlayerRaces(filepath + Configuration.PlayerRaceDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Race Data File not found."); }

                if (File.Exists(filepath + Configuration.PlayerSubraceDataFilePath))
                {
                    DataImport.ImportData_PlayerSubraces(filepath + Configuration.PlayerSubraceDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Subrace Data File not found."); }

                if (File.Exists(filepath + Configuration.PlayerBackgroundDataFilePath))
                {
                    DataImport.ImportData_PlayerBackgrounds(filepath + Configuration.PlayerBackgroundDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Background Data File not found."); }

                if (File.Exists(filepath + Configuration.PlayerFeatDataFilePath))
                {
                    DataImport.ImportData_PlayerFeats(filepath + Configuration.PlayerFeatDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Feat Data File not found."); }

                if (File.Exists(filepath + Configuration.EldritchInvocationsDataFilePath))
                {
                    DataImport.ImportData_Invocations(filepath + Configuration.EldritchInvocationsDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Eldritch Invocation Data File not found."); }

                if (File.Exists(filepath + Configuration.CharacterDataFilePath))
                {
                    DataImport.ImportData_PlayerCharacters(filepath + Configuration.CharacterDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Player Character Data File not found."); }

                // DM
                if (File.Exists(filepath + Configuration.CampaignDataFilePath))
                {
                    DataImport.ImportData_Campaigns(filepath + Configuration.CampaignDataFilePath, out string msg); messages.Add(msg);
                }
                else { messages.Add("Campaign Data File not found."); }

                // IMAGES AND AUDIO
                DirectoryCopy(filepath + "Audio", Directory.GetCurrentDirectory() + "\\Audio\\", true);
                DirectoryCopy(filepath + "Images", Directory.GetCurrentDirectory() + "\\Images\\", true);
                messages.Add("Audio and Image files imported.");

                HelperMethods.NotifyUser(HelperMethods.GetStringFromList(messages), HelperMethods.UserNotificationType.Report);

                YesNoDialog question = new("Would you like to save all data now?");
                question.ShowDialog();
                if (question.Answer == false) { return; }

                // Core
                Configuration.MainModelRef.ItemBuilderView.DoSaveItems(false);
                Configuration.MainModelRef.SpellBuilderView.DoSaveSpells(false);
                Configuration.MainModelRef.CreatureBuilderView.DoSaveCreatures(false);

                // Misc
                Configuration.MainModelRef.ToolsView.DoSaveShops(false);
                Configuration.MainModelRef.ToolsView.DoSaveLootBoxes(false);
                Configuration.MainModelRef.ToolsView.DoSaveRollTables(false);
                Configuration.MainModelRef.ToolsView.DoSaveLanguages(false);
                Configuration.MainModelRef.ToolsView.DoSaveWeathers(false);
                Configuration.MainModelRef.ToolsView.DoSaveCalendars(false);

                // Player
                Configuration.MainModelRef.ToolsView.DoSavePlayerClasses(false);
                Configuration.MainModelRef.ToolsView.DoSavePlayerSubclasses(false);
                Configuration.MainModelRef.ToolsView.DoSavePlayerRaces(false);
                Configuration.MainModelRef.ToolsView.DoSavePlayerSubraces(false);
                Configuration.MainModelRef.ToolsView.DoSavePlayerBackgrounds(false);
                Configuration.MainModelRef.ToolsView.DoSavePlayerFeats(false);
                Configuration.MainModelRef.ToolsView.DoSaveEldritchInvocations(false);
                Configuration.MainModelRef.CharacterBuilderView.DoSaveCharacters(false);

                // DM
                Configuration.MainModelRef.CampaignView.DoSaveCampaigns(false);

                HelperMethods.NotifyUser("All Saves Complete");

            }
            folderDialog.Dispose();
        }
        #endregion
        #region SRDReleaseCleanup
        public ICommand SRDReleaseCleanup => new RelayCommand(DoSRDReleaseCleanup);
        private void DoSRDReleaseCleanup(object param)
        {
            List<string> allowedSources = new();
            foreach (Sourcebook sb in Configuration.MainModelRef.ToolsView.Sourcebooks)
            {
                if (sb.IsValidated == false || sb.PreserveFromDataWipe == false) { continue; }
                allowedSources.Add(sb.Name);
            }
            string message1 = "This will remove all data not designated under the following sourcebooks, remove audio and images, and then close the program:\n";
            message1 += HelperMethods.GetStringFromList(allowedSources, ", ");
            message1 += "\nProceed?";
            YesNoDialog question = new(message1);
            question.ShowDialog();
            if (question.Answer == false) { return; }
            string message = "Failed saves:";
            Dictionary<string, bool> validations = new();
            validations.Add("Campaigns", HelperMethods.SaveToXml(new List<GameCampaign>(), "GameCampaignSet", Configuration.CampaignDataFilePath));
            foreach (CreatureModel creature in Configuration.MainModelRef.CreatureBuilderView.AllCreatures)
            {
                creature.HasMiniature = false;
                creature.MiniatureQuantity = 0;
                creature.MiniatureLocation = "";
            }
            validations.Add("Creatures", HelperMethods.SaveToXml(Configuration.MainModelRef.CreatureBuilderView.AllCreatures.Where(crt => allowedSources.Contains(crt.Sourcebook)).ToList(), "CreatureModelSet", Configuration.CreatureDataFilePath));
            validations.Add("Items", HelperMethods.SaveToXml(Configuration.MainModelRef.ItemBuilderView.AllItems.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "ItemModelSet", Configuration.ItemDataFilePath));
            validations.Add("Spells", HelperMethods.SaveToXml(Configuration.MainModelRef.SpellBuilderView.AllSpells.Where(spell => allowedSources.Contains(spell.Sourcebook)).ToList(), "SpellModelSet", Configuration.SpellDataFilePath));
            validations.Add("Characters", HelperMethods.SaveToXml(new List<CharacterModel>(), "CharacterModelSet", Configuration.CharacterDataFilePath));
            validations.Add("Loot Chests", HelperMethods.SaveToXml(new List<LootBoxModel>(), "LootBoxModelSet", Configuration.LootBoxDataFilePath));
            validations.Add("Roll Tables", HelperMethods.SaveToXml(new List<RollTableModel>(), "RollTableModelSet", Configuration.RollTableDataFilePath));
            validations.Add("Languages", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.Languages.Where(lang => allowedSources.Contains(lang.Sourcebook)).ToList(), "LanguageModelSet", Configuration.LanguageDataFilePath));
            validations.Add("NPCs", HelperMethods.SaveToXml(new List<NpcModel>(), "NpcModelSet", Configuration.NpcDataFilePath));
            validations.Add("Creature Packs", HelperMethods.SaveToXml(new List<CreaturePackModel>(), "CreaturePackModelSet", Configuration.CreaturePackDataFilePath));
            validations.Add("Player Classes", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.PlayerClasses.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "PlayerClassModelSet", Configuration.PlayerClassDataFilePath));
            validations.Add("Player Subclasses", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.PlayerSubclasses.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "PlayerSubclassModelSet", Configuration.PlayerSubclassDataFilePath));
            validations.Add("Player Races", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.PlayerRaces.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "PlayerRaceModelSet", Configuration.PlayerRaceDataFilePath));
            validations.Add("Player Subraces", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.PlayerSubraces.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "PlayerSubraceModelSet", Configuration.PlayerSubraceDataFilePath));
            validations.Add("Player Backgrounds", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.PlayerBackgrounds.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "PlayerBackgroundModelSet", Configuration.PlayerBackgroundDataFilePath));
            validations.Add("Player Feats", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.PlayerFeats.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "PlayerFeatModelSet", Configuration.PlayerFeatDataFilePath));
            validations.Add("Eldritch Invocations", HelperMethods.SaveToXml(Configuration.MainModelRef.ToolsView.EldritchInvocations.Where(item => allowedSources.Contains(item.Sourcebook)).ToList(), "EldritchInvocationSet", Configuration.EldritchInvocationsDataFilePath));
            DoSetReleaseSettings();
            try
            {
                Directory.Delete(Environment.CurrentDirectory + "/Audio/Music", true);
                Directory.Delete(Environment.CurrentDirectory + "/Audio/Sfx", true);
                Directory.Delete(Environment.CurrentDirectory + "/Images", true);
                Directory.Delete(Environment.CurrentDirectory + "/NoteAttachments", true);
            }
            catch (Exception e)
            {
                HelperMethods.NotifyUser(e.Message);
            }
            foreach (KeyValuePair<string, bool> pair in validations)
            {
                if (pair.Value == false)
                {
                    message += "\n" + pair.Key;
                }
            }
            if (message.Contains("\n"))
            {
                HelperMethods.WriteToLogFile(message, true);
            }
            else
            {
                HelperMethods.ClearLogFile();
                HelperMethods.WriteToLogFile("Release Data Cleanup Completed Successfully\nThe application will now close.", true);
                System.Windows.Application.Current.MainWindow.Close();
            }
        }
        #endregion
        #region PTReleaseCleanup
        public ICommand PTReleaseCleanup => new RelayCommand(DoPTReleaseCleanup);
        private void DoPTReleaseCleanup(object param)
        {
            YesNoDialog question = new("This will remove all Campaign data, audio, and npc images then close the program.\nProceed?");
            question.ShowDialog();
            if (question.Answer == false) { return; }
            string message = "Failed saves:";
            Dictionary<string, bool> validations = new();
            validations.Add("Campaigns", HelperMethods.SaveToXml(new List<GameCampaign>(), "GameCampaignSet", Configuration.CampaignDataFilePath));
            foreach (CreatureModel creature in Configuration.MainModelRef.CreatureBuilderView.AllCreatures)
            {
                creature.HasMiniature = false;
                creature.MiniatureQuantity = 0;
                creature.MiniatureLocation = "";
            }
            validations.Add("Creatures", HelperMethods.SaveToXml(Configuration.MainModelRef.CreatureBuilderView.AllCreatures.ToList(), "CreatureModelSet", Configuration.CreatureDataFilePath));
            validations.Add("Characters", HelperMethods.SaveToXml(new List<CharacterModel>(), "CharacterModelSet", Configuration.CharacterDataFilePath));
            validations.Add("NPCs", HelperMethods.SaveToXml(new List<NpcModel>(), "NpcModelSet", Configuration.NpcDataFilePath));
            validations.Add("Creature Packs", HelperMethods.SaveToXml(new List<CreaturePackModel>(), "CreaturePackModelSet", Configuration.CreaturePackDataFilePath));
            DoSetReleaseSettings();
            try
            {
                Directory.Delete(Environment.CurrentDirectory + "/Audio/Music", true);
                Directory.Delete(Environment.CurrentDirectory + "/Audio/Sfx", true);
                Directory.Delete(Environment.CurrentDirectory + "/Images/Npcs", true);
                Directory.Delete(Environment.CurrentDirectory + "/NoteAttachments", true);
            }
            catch (Exception e)
            {
                HelperMethods.NotifyUser(e.Message);
            }
            foreach (KeyValuePair<string, bool> pair in validations)
            {
                if (pair.Value == false)
                {
                    message += "\n" + pair.Key;
                }
            }
            if (message.Contains("\n"))
            {
                HelperMethods.WriteToLogFile(message, true);
            }
            else
            {
                HelperMethods.ClearLogFile();
                HelperMethods.WriteToLogFile("Release Data Cleanup Completed Successfully\nThe application will now close.", true);
                System.Windows.Application.Current.MainWindow.Close();
            }
        }
        #endregion

        // Public Methods
        public void SaveSettings()
        {
            if (ClearPasswordOnClose) { Roll20Password = ""; }
            XDocument xmlDoc = new();
            xmlDoc.Add(XmlMethods.ListToXml(new List<SettingsViewModel> { this }));
            xmlDoc.Save(Configuration.SettingsDataFilePath);
        }

        // Private Methods
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new(sourceDirName);

            if (!dir.Exists)
            {
                // Changed because I don't wanna crash if directory not found
                //throw new DirectoryNotFoundException(
                //"Source directory does not exist or could not be found: "
                //+ sourceDirName);
                HelperMethods.WriteToLogFile("Source directory does not exist or could not be found: " + sourceDirName, true);
                return;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                if (File.Exists(tempPath)) { continue; } // Added to check for duplicate and skip if found
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

    }
}
