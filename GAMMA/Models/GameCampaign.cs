using GAMMA.Models.GameplayComponents;
using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class GameCampaign : BaseModel
    {
        // Constructors
        public GameCampaign()
        {
            Combatants = new();
            CombatantsByName = new();
            CombatantsByIsNpc = new();
            CombatantsByIsPlayer = new();
            Messages = new();
            Timestamps = new();
            Npcs = new();
            Players = new();
            Packs = new();
            Notes = new();
            CalendarStart = 0;
            CalendarProgress = 0;
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CalendarType
        private string _CalendarType;
        [XmlSaveMode(XSME.Single)]
        public string CalendarType
        {
            get
            {
                return _CalendarType;
            }
            set
            {
                _CalendarType = value;
                NotifyPropertyChanged();
                CalculateCalendar();
            }
        }
        #endregion
        #region CalendarStart
        private long _CalendarStart;
        [XmlSaveMode(XSME.Single)]
        public long CalendarStart
        {
            get
            {
                return _CalendarStart;
            }
            set
            {
                _CalendarStart = value;
                NotifyPropertyChanged();
                CalculateCalendar();
            }
        }
        #endregion
        #region CalendarProgress
        private long _CalendarProgress;
        [XmlSaveMode(XSME.Single)]
        public long CalendarProgress
        {
            get
            {
                return _CalendarProgress;
            }
            set
            {
                _CalendarProgress = value;
                NotifyPropertyChanged();
                CalculateCalendar();
            }
        }
        #endregion
        #region CalendarStart_Processed
        private string _CalendarStart_Processed;
        public string CalendarStart_Processed
        {
            get
            {
                return _CalendarStart_Processed;
            }
            set
            {
                _CalendarStart_Processed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CalendarStart_Time
        private string _CalendarStart_Time;
        public string CalendarStart_Time
        {
            get
            {
                return _CalendarStart_Time;
            }
            set
            {
                _CalendarStart_Time = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CalendarProgress_Processed
        private string _CalendarProgress_Processed;
        public string CalendarProgress_Processed
        {
            get
            {
                return _CalendarProgress_Processed;
            }
            set
            {
                _CalendarProgress_Processed = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AdventureDate
        private string _AdventureDate;
        public string AdventureDate
        {
            get
            {
                return _AdventureDate;
            }
            set
            {
                _AdventureDate = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AdventureDayCount
        private int _AdventureDayCount;
        
        public int AdventureDayCount
        {
            get
            {
                return _AdventureDayCount;
            }
            set
            {
                _AdventureDayCount = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TimeDigits
        private string _TimeDigits;
        
        public string TimeDigits
        {
            get
            {
                return _TimeDigits;
            }
            set
            {
                _TimeDigits = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TimeIndicator
        private string _TimeIndicator;
        
        public string TimeIndicator
        {
            get
            {
                return _TimeIndicator;
            }
            set
            {
                _TimeIndicator = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Display_ChangeTime
        private bool _Display_ChangeTime;
        
        public bool Display_ChangeTime
        {
            get
            {
                return _Display_ChangeTime;
            }
            set
            {
                _Display_ChangeTime = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LastWeatherChange
        private long _LastWeatherChange;
        [XmlSaveMode(XSME.Single)]
        public long LastWeatherChange
        {
            get
            {
                return _LastWeatherChange;
            }
            set
            {
                _LastWeatherChange = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeatherType
        private string _WeatherType;
        [XmlSaveMode(XSME.Single)]
        public string WeatherType
        {
            get
            {
                return _WeatherType;
            }
            set
            {
                _WeatherType = value;
                NotifyPropertyChanged();
                if (Configuration.MainModelRef.ToolsView == null) { return; }
                LinkedWeather = Configuration.MainModelRef.ToolsView.Weathers.FirstOrDefault(w => w.Name == value);
                SetWeather();
            }
        }
        #endregion
        #region WeatherIntensity
        private int _WeatherIntensity;
        [XmlSaveMode(XSME.Single)]
        public int WeatherIntensity
        {
            get
            {
                return _WeatherIntensity;
            }
            set
            {
                _WeatherIntensity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LinkedWeather
        private Weather _LinkedWeather;
        public Weather LinkedWeather
        {
            get
            {
                return _LinkedWeather;
            }
            set
            {
                _LinkedWeather = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeatherIcon
        private string _WeatherIcon;
        public string WeatherIcon
        {
            get
            {
                return _WeatherIcon;
            }
            set
            {
                _WeatherIcon = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeatherName
        private string _WeatherName;
        public string WeatherName
        {
            get
            {
                return _WeatherName;
            }
            set
            {
                _WeatherName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region WeatherInfo
        private string _WeatherInfo;
        public string WeatherInfo
        {
            get
            {
                return _WeatherInfo;
            }
            set
            {
                _WeatherInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Notes
        private ObservableCollection<NoteModel> _Notes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<NoteModel> Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveNote
        private NoteModel _ActiveNote;
        public NoteModel ActiveNote
        {
            get
            {
                return _ActiveNote;
            }
            set
            {
                _ActiveNote = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region EncounterRound
        private int _EncounterRound;
        [XmlSaveMode(XSME.Single)]
        public int EncounterRound
        {
            get
            {
                return _EncounterRound;
            }
            set
            {
                _EncounterRound = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Display_OtherRolls
        private bool _Display_OtherRolls;
        
        public bool Display_OtherRolls
        {
            get
            {
                return _Display_OtherRolls;
            }
            set
            {
                _Display_OtherRolls = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Timestamps
        private ObservableCollection<Timestamp> _Timestamps;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<Timestamp> Timestamps
        {
            get
            {
                return _Timestamps;
            }
            set
            {
                _Timestamps = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region RetainMessageHistory
        private bool _RetainMessageHistory;
        [XmlSaveMode(XSME.Single)]
        public bool RetainMessageHistory
        {
            get
            {
                return _RetainMessageHistory;
            }
            set
            {
                _RetainMessageHistory = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Messages
        private ObservableCollection<GameMessage> _Messages;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<GameMessage> Messages
        {
            get
            {
                return _Messages;
            }
            set
            {
                _Messages = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LinkOutputToWeb
        private bool _LinkOutputToWeb;
        [XmlSaveMode(XSME.Single)]
        public bool LinkOutputToWeb
        {
            get
            {
                return _LinkOutputToWeb;
            }
            set
            {
                _LinkOutputToWeb = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Combatants
        private ObservableCollection<CreatureModel> _Combatants;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CreatureModel> Combatants
        {
            get
            {
                return _Combatants;
            }
            set
            {
                _Combatants = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CombatantsByName
        private ObservableCollection<CreatureModel> _CombatantsByName;
        
        public ObservableCollection<CreatureModel> CombatantsByName
        {
            get
            {
                return _CombatantsByName;
            }
            set
            {
                _CombatantsByName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CombatantsByIsNpc
        private ObservableCollection<CreatureModel> _CombatantsByIsNpc;
        
        public ObservableCollection<CreatureModel> CombatantsByIsNpc
        {
            get
            {
                return _CombatantsByIsNpc;
            }
            set
            {
                _CombatantsByIsNpc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CombatantsByIsPlayer
        private ObservableCollection<CreatureModel> _CombatantsByIsPlayer;
        
        public ObservableCollection<CreatureModel> CombatantsByIsPlayer
        {
            get
            {
                return _CombatantsByIsPlayer;
            }
            set
            {
                _CombatantsByIsPlayer = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveCombatant
        private CreatureModel _ActiveCombatant;
        
        public CreatureModel ActiveCombatant
        {
            get
            {
                return _ActiveCombatant;
            }
            set
            {
                _ActiveCombatant = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Players
        private ObservableCollection<CreatureModel> _Players;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CreatureModel> Players
        {
            get
            {
                return _Players;
            }
            set
            {
                _Players = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePlayer
        private CreatureModel _ActivePlayer;
        
        public CreatureModel ActivePlayer
        {
            get
            {
                return _ActivePlayer;
            }
            set
            {
                _ActivePlayer = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Npcs
        private ObservableCollection<NpcModel> _Npcs;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<NpcModel> Npcs
        {
            get
            {
                return _Npcs;
            }
            set
            {
                _Npcs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveNpc
        private NpcModel _ActiveNpc;
        
        public NpcModel ActiveNpc
        {
            get
            {
                return _ActiveNpc;
            }
            set
            {
                _ActiveNpc = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Packs
        private ObservableCollection<CreaturePackModel> _Packs;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<CreaturePackModel> Packs
        {
            get
            {
                return _Packs;
            }
            set
            {
                _Packs = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActivePack
        private CreaturePackModel _ActivePack;
        public CreaturePackModel ActivePack
        {
            get
            {
                return _ActivePack;
            }
            set
            {
                _ActivePack = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Etc Rolls
        #region FallDistance
        private int _FallDistance;
        [XmlSaveMode(XSME.Single)]
        public int FallDistance
        {
            get
            {
                return _FallDistance;
            }
            set
            {
                _FallDistance = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CustomRollNumber
        private int _CustomRollNumber;
        [XmlSaveMode(XSME.Single)]
        public int CustomRollNumber
        {
            get
            {
                return _CustomRollNumber;
            }
            set
            {
                _CustomRollNumber = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CustomRollSides
        private int _CustomRollSides;
        [XmlSaveMode(XSME.Single)]
        public int CustomRollSides
        {
            get
            {
                return _CustomRollSides;
            }
            set
            {
                _CustomRollSides = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CustomRollModifier
        private int _CustomRollModifier;
        [XmlSaveMode(XSME.Single)]
        public int CustomRollModifier
        {
            get
            {
                return _CustomRollModifier;
            }
            set
            {
                _CustomRollModifier = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveCampaign
        private RelayCommand _RemoveCampaign;
        public ICommand RemoveCampaign
        {
            get
            {
                if (_RemoveCampaign == null)
                {
                    _RemoveCampaign = new RelayCommand(DoRemoveCampaign);
                }
                return _RemoveCampaign;
            }
        }
        private void DoRemoveCampaign(object param)
        {
            YesNoDialog question = new("Are you sure you want to delete this campaign?");
            question.ShowDialog();
            if (question.Answer == false) { return; }
            Configuration.MainModelRef.CampaignView.Campaigns.Remove(this);
        }
        #endregion
        #region ModifyCalendar
        private RelayCommand _ModifyCalendar;
        public ICommand ModifyCalendar
        {
            get
            {
                if (_ModifyCalendar == null)
                {
                    _ModifyCalendar = new RelayCommand(DoModifyCalendar);
                }
                return _ModifyCalendar;
            }
        }
        private void DoModifyCalendar(object param)
        {
        }
        #endregion
        #region ClearHistory
        private RelayCommand _ClearHistory;
        public ICommand ClearHistory
        {
            get
            {
                if (_ClearHistory == null)
                {
                    _ClearHistory = new RelayCommand(DoClearHistory);
                }
                return _ClearHistory;
            }
        }
        private void DoClearHistory(object param)
        {
        }
        #endregion
        #region AddTimestamp
        private RelayCommand _AddTimestamp;
        public ICommand AddTimestamp
        {
            get
            {
                if (_AddTimestamp == null)
                {
                    _AddTimestamp = new RelayCommand(DoAddTimestamp);
                }
                return _AddTimestamp;
            }
        }
        private void DoAddTimestamp(object param)
        {
            Timestamps.Add(new() { Name = "New Timestamp", TimeInfo = "Day " + AdventureDayCount + ", " + TimeDigits + " " + TimeIndicator });
        }
        #endregion
        #region RollDice
        private RelayCommand _RollDice;
        public ICommand RollDice
        {
            get
            {
                if (_RollDice == null)
                {
                    _RollDice = new RelayCommand(DoRollDice);
                }
                return _RollDice;
            }
        }
        private void DoRollDice(object param)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int result = Configuration.RNG.Next(1, Convert.ToInt32(param) + 1);
            string message = "DM rolls 1d" + param + "\nResult: " + result;
            HelperMethods.AddToCampaignMessages(message, "DM Roll");
        }
        #endregion
        #region FlipCoin
        private RelayCommand _FlipCoin;
        public ICommand FlipCoin
        {
            get
            {
                if (_FlipCoin == null)
                {
                    _FlipCoin = new RelayCommand(DoFlipCoin);
                }
                return _FlipCoin;
            }
        }
        private void DoFlipCoin(object param)
        {
            int result = Configuration.RNG.Next(1, 3);
            HelperMethods.AddToCampaignMessages(string.Format("DM flips a coin.\nResult: {0}.", (result == 1) ? "Heads" : "Tails"), "Coin Flip");
        }
        #endregion
        #region AddCreatures
        private RelayCommand _AddCreatures;
        public ICommand AddCreatures
        {
            get
            {
                if (_AddCreatures == null)
                {
                    _AddCreatures = new RelayCommand(DoAddCreatures);
                }
                return _AddCreatures;
            }
        }
        private void DoAddCreatures(object param)
        {
            MultiObjectSelectionDialog selectionDialog = new(Configuration.CreatureRepository.Where(creature => creature.IsValidated == true && creature.IsPlayer == false).ToList(), "Creatures");

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (CreatureModel selectedCreature in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCreatures)
                {
                    for (int i = 0; i < selectedCreature.QuantityToAdd; i++)
                    {
                        CreatureModel newCreature = HelperMethods.DeepClone(selectedCreature);
                        int existingCreatureCount = Combatants.Where(creature => creature.Name == newCreature.Name).Count();
                        if (existingCreatureCount > 25) { break; }
                        newCreature.RollHitPoints(Configuration.MainModelRef.SettingsView.UseAveragedHitPoints);
                        newCreature.SetPassivePerception();
                        newCreature.HasBeenLooted = false;
                        if (newCreature.IsPlayer == false) { newCreature.TrackerIndicator = Configuration.AlphaArray[existingCreatureCount]; }
                        newCreature.DisplayName = newCreature.Name + " " + newCreature.TrackerIndicator;
                        newCreature.SetFormattedTexts();
                        newCreature.RefreshSpellSlots();
                        newCreature.RefreshCounters();
                        newCreature.GetPortraitFilepath();
                        newCreature.SetHighestSpeedValues();
                        Combatants.Add(newCreature);
                    }
                }
                SortCombatants();
            }
        }
        #endregion
        #region AddImprovCreatures
        public ICommand AddImprovCreatures => new RelayCommand(DoAddImprovCreatures);
        private void DoAddImprovCreatures(object param)
        {
            ImprovCreatureDialog creatureDialog = new();
            if (creatureDialog.ShowDialog() == true)
            {
                for (int i = 0; i < creatureDialog.Quantity; i++)
                {
                    CreatureModel newCreature = new();

                    newCreature.Name = creatureDialog.CreatureName;
                    int existingCreatureCount = Combatants.Where(creature => creature.Name == newCreature.Name).Count();
                    newCreature.TrackerIndicator = Configuration.AlphaArray[existingCreatureCount];
                    newCreature.DisplayName = newCreature.Name + " " + newCreature.TrackerIndicator;
                    newCreature.CreatureCategory = "Improvised";
                    newCreature.GetPortraitFilepath();

                    newCreature.Attr_Strength = creatureDialog.StrengthScore;
                    newCreature.Attr_Dexterity = creatureDialog.DexterityScore;
                    newCreature.Attr_Constitution = creatureDialog.ConstitutionScore;
                    newCreature.Attr_Intelligence = creatureDialog.IntelligenceScore;
                    newCreature.Attr_Wisdom = creatureDialog.WisdomScore;
                    newCreature.Attr_Charisma = creatureDialog.CharismaScore;

                    newCreature.MaxHitPoints = creatureDialog.HitPoints;
                    newCreature.CurrentHitPoints = creatureDialog.HitPoints;
                    newCreature.ArmorClass = creatureDialog.ArmorClass;
                    newCreature.Speed = creatureDialog.Speed;

                    CustomAbility basicAttack = HelperMethods.DeepClone(creatureDialog.Attack);
                    basicAttack.SetGeneratedDescription(newCreature);
                    newCreature.Abilities.Add(basicAttack);

                    newCreature.SetFormattedTexts();

                    Combatants.Add(newCreature);

                }
                
                SortCombatants();

            }
        }
        #endregion
        #region AddPack
        private RelayCommand _AddPack;
        public ICommand AddPack
        {
            get
            {
                if (_AddPack == null)
                {
                    _AddPack = new RelayCommand(DoAddPack);
                }
                return _AddPack;
            }
        }
        private void DoAddPack(object param)
        {
            ObjectSelectionDialog packSelect = new(Packs.Where(p => p.IsActive).ToList());
            if (packSelect.ShowDialog() == true)
            {
                if (packSelect.SelectedObject == null) { return; }

                CreaturePackModel selectedPack = packSelect.SelectedObject as CreaturePackModel;

                string msg = "";
                foreach (PackCreatureModel creature in selectedPack.CreatureList)
                {
                    CreatureModel matchedCreature = Configuration.CreatureRepository.FirstOrDefault(crt => crt.Name == creature.CreatureName);
                    if (matchedCreature == null) { msg += "\nCould not find creature \"" + creature.CreatureName + "\"."; continue; }

                    for (int i = 0; i < creature.Quantity; i++)
                    {
                        CreatureModel newCreature = HelperMethods.DeepClone(matchedCreature);
                        int existingCreatureCount = Combatants.Where(creature => creature.Name == newCreature.Name).Count();
                        if (existingCreatureCount > 25) { break; }
                        newCreature.RollHitPoints(Configuration.MainModelRef.SettingsView.UseAveragedHitPoints);
                        newCreature.SetPassivePerception();
                        newCreature.HasBeenLooted = false;
                        if (newCreature.IsPlayer == false) { newCreature.TrackerIndicator = Configuration.AlphaArray[existingCreatureCount]; }
                        newCreature.DisplayName = newCreature.Name + " " + newCreature.TrackerIndicator;
                        newCreature.SetFormattedTexts();
                        newCreature.IsAlly = selectedPack.IsAlly;
                        newCreature.RefreshSpellSlots();
                        newCreature.RefreshCounters();
                        newCreature.GetPortraitFilepath();
                        newCreature.SetHighestSpeedValues();
                        Combatants.Add(newCreature);
                    }
                }
                foreach (PackCreatureModel creature in selectedPack.NpcList)
                {
                    NpcModel npc = Configuration.MainModelRef.CampaignView.ActiveCampaign.Npcs.FirstOrDefault(npc => npc.Name == creature.CreatureName);
                    if (npc == null) { msg += "\nUnable to find NPC " + creature.CreatureName + "."; continue; }
                    CreatureModel baseCreature = Configuration.CreatureRepository.FirstOrDefault(creature => creature.Name == npc.BaseCreatureName);
                    if (baseCreature == null)
                    {
                        msg += "\nUnable to find base creature " + npc.BaseCreatureName + ", NPC " + npc.Name + " not added.";
                        continue;
                    }
                    CreatureModel newCreature = HelperMethods.DeepClone(baseCreature);
                    newCreature.DisplayName = npc.Name;
                    newCreature.Name = npc.Name;
                    newCreature.IsNpc = true;
                    newCreature.IsAlly = npc.IsFriendly;
                    newCreature.RollHitPoints(true);
                    newCreature.SetPassivePerception();
                    newCreature.HasBeenLooted = false;
                    newCreature.SetFormattedTexts();
                    newCreature.RefreshSpellSlots();
                    newCreature.RefreshCounters();
                    newCreature.GetPortraitFilepath();
                    newCreature.SetHighestSpeedValues();
                    Combatants.Add(newCreature);
                }

                SortCombatants();

                if (msg != "")
                {
                    HelperMethods.WriteToLogFile(msg, true);
                }

            }
        }
        #endregion
        #region AddNpcs
        private RelayCommand _AddNpcs;
        public ICommand AddNpcs
        {
            get
            {
                if (_AddNpcs == null)
                {
                    _AddNpcs = new RelayCommand(DoAddNpcs);
                }
                return _AddNpcs;
            }
        }
        private void DoAddNpcs(object param)
        {
            MultiObjectSelectionDialog selectionDialog = new (Npcs.Where(npc => npc.BaseCreatureName != "" && npc.IsActive).ToList());
            if (selectionDialog.ShowDialog() == true)
            {
                string msg = "";
                foreach (NpcModel selectedNpc in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedNpcs)
                {
                    CreatureModel baseCreature = Configuration.CreatureRepository.FirstOrDefault(creature => creature.Name == selectedNpc.BaseCreatureName);
                    if (baseCreature == null)
                    {
                        msg += "\nUnable to find base creature " + selectedNpc.BaseCreatureName + ", NPC " + selectedNpc.Name + " not added.";
                        continue;
                    }
                    CreatureModel newCreature = HelperMethods.DeepClone(baseCreature);
                    newCreature.DisplayName = selectedNpc.Name;
                    newCreature.Name = selectedNpc.Name;
                    newCreature.IsNpc = true;
                    newCreature.IsAlly = selectedNpc.IsFriendly;
                    newCreature.RollHitPoints(true);
                    newCreature.SetPassivePerception();
                    newCreature.HasBeenLooted = false;
                    newCreature.SetFormattedTexts();
                    newCreature.Lore = selectedNpc.Description;
                    newCreature.RefreshSpellSlots();
                    newCreature.RefreshCounters();
                    newCreature.GetPortraitFilepath();
                    Combatants.Add(newCreature);
                }
                if (msg != "")
                {
                    new NotificationDialog(msg).ShowDialog();
                }
                SortCombatants();
            }
        }
        #endregion
        #region AddPlayers
        private RelayCommand _AddPlayers;
        public ICommand AddPlayers
        {
            get
            {
                if (_AddPlayers == null)
                {
                    _AddPlayers = new RelayCommand(DoAddPlayers);
                }
                return _AddPlayers;
            }
        }
        private void DoAddPlayers(object param)
        {
            MultiObjectSelectionDialog selectionDialog = new (Configuration.CreatureRepository.Where(creature => creature.IsPlayer == true).ToList().Concat(Players.ToList()).ToList(), "Players");

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (CreatureModel selectedCreature in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCreatures)
                {
                    for (int i = 0; i < selectedCreature.QuantityToAdd; i++)
                    {
                        CreatureModel newCreature = HelperMethods.DeepClone(selectedCreature);
                        int existingCreatureCount = Combatants.Where(creature => creature.Name == newCreature.Name).Count();
                        if (existingCreatureCount > 0) { break; }
                        if (newCreature.PlayerPassivePerception == 0) { newCreature.PlayerPassivePerception = newCreature.PassivePerception; }
                        if (newCreature.PlayerSpellSaveDc == 0) { newCreature.PlayerSpellSaveDc = newCreature.SpellSaveDc; }
                        newCreature.GetPortraitFilepath();
                        Combatants.Add(newCreature);
                    }
                }
                SortCombatants();
            }
        }
        #endregion
        #region RollInitiatives
        private RelayCommand _RollInitiatives;
        public ICommand RollInitiatives
        {
            get
            {
                if (_RollInitiatives == null)
                {
                    _RollInitiatives = new RelayCommand(DoRollInitiatives);
                }
                return _RollInitiatives;
            }
        }
        private void DoRollInitiatives(object param)
        {
            bool reroll = Convert.ToBoolean(param);
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            string message = "";

            foreach (CreatureModel creature in Combatants)
            {
                if (creature.IsPlayer == false)
                {
                    if (creature.Initiative == 0 || reroll)
                    {
                        creature.Initiative = Configuration.RNG.Next(1, 21) + HelperMethods.GetAttributeModifier(creature.Attr_Dexterity);
                        message += "\n" + creature.DisplayName + ": " + creature.Initiative;
                    }
                }
            }

            if (message.Length > 0)
            {
                message = message.Insert(0, "Combatant initiatives rolled.");
            }
            else
            {
                message = "No new initiatives rolled.";
            }

            HelperMethods.AddToCampaignMessages(message, "Initiative");
            SortCombatants();

        }
        #endregion
        #region SortByInitiative
        private RelayCommand _SortByInitiative;
        public ICommand SortByInitiative
        {
            get
            {
                if (_SortByInitiative == null)
                {
                    _SortByInitiative = new RelayCommand(DoSortByInitiative);
                }
                return _SortByInitiative;
            }
        }
        private void DoSortByInitiative(object param)
        {
            SortCombatants();
        }
        #endregion
        #region RestNpcs
        public ICommand RestNpcs => new RelayCommand(DoRestNpcs);
        private void DoRestNpcs(object param)
        {
            YesNoDialog question = new("Reset all allied NPC hit points and spell slots?");
            if(question.ShowDialog() == true)
            {
                foreach (CreatureModel creature in Combatants)
                {
                    if (creature.IsNpc && creature.IsAlly)
                    {
                        creature.CurrentHitPoints = creature.MaxHitPoints;
                        creature.RefreshSpellSlots();
                    }
                }
                HelperMethods.AddToCampaignMessages("Allied NPC hit points and spell slots have been reset.", "Other");
            }
        }
        #endregion
        #region ClearCreatures
        private RelayCommand _ClearCreatures;
        public ICommand ClearCreatures
        {
            get
            {
                if (_ClearCreatures == null)
                {
                    _ClearCreatures = new RelayCommand(DoClearCreatures);
                }
                return _ClearCreatures;
            }
        }
        private void DoClearCreatures(object param)
        {
            if (param.ToString() == "All")
            {
                YesNoDialog question = new("Clear all creatures, NPCs, and players?");
                question.ShowDialog();
                if (question.Answer == false) { return; }

                Combatants = new ();
                ActiveCombatant = null;
                SortCombatants();
                return;
            }

            if (param.ToString() == "Dead Creatures")
            {
                Combatants = new (Combatants.Where(creature => creature.CurrentHitPoints > 0 || creature.IsPlayer == true));
                SortCombatants();
            }
            
        }
        #endregion
        #region KillCreatures
        private RelayCommand _KillCreatures;
        public ICommand KillCreatures
        {
            get
            {
                if (_KillCreatures == null)
                {
                    _KillCreatures = new RelayCommand(param => DoKillCreatures());
                }
                return _KillCreatures;
            }
        }
        private void DoKillCreatures()
        {
            YesNoDialog question = new("Kill all enemies?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            foreach (CreatureModel creature in Combatants)
            {
                if (creature.IsAlly == false)
                {
                    creature.CurrentHitPoints = 0;
                }
            }

        }
        #endregion
        #region LootAll
        public ICommand LootAll => new RelayCommand(DoLootAll);
        private void DoLootAll(object param)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int totalGoldDrop = 0;
            int totalSilverDrop = 0;
            int totalCopperDrop = 0;
            int totalXp = 0;
            Dictionary<string, int> lootedItems = new();
            string message = "Encounter loot found: ";

            foreach (CreatureModel creature in Combatants)
            {
                if (creature.HasBeenLooted || creature.CurrentHitPoints > 0) { continue; }
                if (creature.CoinDrop > 0)
                {
                    int newCoinDrop = Configuration.RNG.Next(creature.CoinDrop / 2, creature.CoinDrop + 1);
                    int newGoldDrop = newCoinDrop / 100;
                    int newSilverDrop = (newCoinDrop - (newGoldDrop * 100)) / 10;
                    int newCopperDrop = newCoinDrop - (newGoldDrop * 100) - (newSilverDrop * 10);
                    totalGoldDrop += newGoldDrop;
                    totalSilverDrop += newSilverDrop;
                    totalCopperDrop += newCopperDrop;
                }
                foreach (ItemLink item in creature.ItemLinks)
                {
                    int dropQty = 0;
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        dropQty += (Configuration.RNG.Next(1, 101) <= item.DropChance) ? 1 : 0;
                    }
                    if (dropQty > 0)
                    {
                        if (lootedItems.ContainsKey(item.Name)) { lootedItems[item.Name] += dropQty; }
                        else { lootedItems.Add(item.Name, dropQty); }
                    }
                }
                creature.HasBeenLooted = true;
                if (creature.IsAlly == false) { totalXp += creature.ExperienceValue; }
            }

            message += string.Format("\nMoney: {0}{1}{2}{3}{4}",
                (totalGoldDrop > 0) ? totalGoldDrop + "gp" : "",
                (totalGoldDrop > 0 && totalSilverDrop > 0) ? " " : "",
                (totalSilverDrop > 0) ? totalSilverDrop + "sp" : "",
                (totalSilverDrop > 0 && totalCopperDrop > 0) ? " " : "",
                (totalCopperDrop > 0) ? totalCopperDrop + "cp" : "");
            foreach (KeyValuePair<string, int> item in lootedItems)
            {
                message += "\n" + item.Value + " x " + item.Key;
            }

            if (totalGoldDrop == 0 && totalSilverDrop == 0 && totalCopperDrop == 0 && lootedItems.Count == 0) { message = "No loot found."; }

            if (Configuration.MainModelRef.SettingsView.UseExperiencePoints) { message += "\n" + totalXp + " experience points gained."; }

            HelperMethods.AddToCampaignMessages(message, "Loot");

            if (param == null) { return; }
            bool.TryParse(param.ToString(), out bool remove);
            if (remove == true)
            {
                Combatants = new(Combatants.Where(creature => creature.CurrentHitPoints > 0 || creature.IsPlayer == true));
                SortCombatants();
            }

        }
        #endregion
        #region ProcessGroupSave
        private RelayCommand _ProcessGroupSave;
        public ICommand ProcessGroupSave
        {
            get
            {
                if (_ProcessGroupSave == null)
                {
                    _ProcessGroupSave = new RelayCommand(param => DoProcessGroupSave());
                }
                return _ProcessGroupSave;
            }
        }
        private void DoProcessGroupSave()
        {
            EncounterMultiTargetDialog targetDialog = new(Combatants.Where(creature => creature.IsPlayer == false && creature.IsOoc == false && creature.CurrentHitPoints > 0).ToList());
            if (targetDialog.ShowDialog() == true)
            {
                if (targetDialog.SelectedCreatures.Count <= 0) { return; }
                string message = "Multiple creatures made a saving throw.";
                message += "\nSave Ability: " + targetDialog.SaveAbility;
                message += "\nSave Difficulty: " + targetDialog.SaveDifficulty;
                if (targetDialog.ComboBox_EffectType.SelectedItem.ToString() == "Attack")
                {
                    message += "\nDamage on Fail: " + targetDialog.PrimaryDamageOnFail + " " + targetDialog.PrimaryDamageType;
                    if (targetDialog.SecondaryDamageOnFail > 0) { message += ", " + targetDialog.SecondaryDamageOnFail + " " + targetDialog.SecondaryDamageType + " damage"; } else { message += " damage"; }
                    message += (targetDialog.HalfOnSave) ? "\nDamage on Save: " + (targetDialog.PrimaryDamageOnFail / 2) + " " + targetDialog.PrimaryDamageType : "";
                    if (targetDialog.SecondaryDamageOnFail > 0 && targetDialog.HalfOnSave) { message += ", " + (targetDialog.SecondaryDamageOnFail / 2) + " " + targetDialog.SecondaryDamageType + " damage"; }
                    else if (targetDialog.SecondaryDamageOnFail == 0 && targetDialog.HalfOnSave) { message += " damage"; }
                }
                foreach (CreatureModel creature in targetDialog.SelectedCreatures)
                {
                    int firstThrow = Configuration.RNG.Next(1, 21);
                    int secondThrow = Configuration.RNG.Next(1, 21);
                    int savingThrow = 0;

                    if (creature.HasGroupSaveAdvantage)
                    {
                        savingThrow = (firstThrow > secondThrow) ? firstThrow : secondThrow;
                    }
                    else if (creature.HasGroupSaveDisadvantage)
                    {
                        savingThrow = (firstThrow < secondThrow) ? firstThrow : secondThrow;
                    }
                    else
                    {
                        savingThrow = firstThrow;
                    }

                    savingThrow += targetDialog.SaveAbility switch
                    {
                        "Strength" => creature.StrengthSave,
                        "Dexterity" => creature.DexteritySave,
                        "Constitution" => creature.ConstitutionSave,
                        "Wisdom" => creature.WisdomSave,
                        "Intelligence" => creature.IntelligenceSave,
                        "Charisma" => creature.CharismaSave,
                        _ => 0
                    };

                    bool passedThrow = (savingThrow >= targetDialog.SaveDifficulty);
                    if (passedThrow)
                    {
                        message += "\n" + creature.DisplayName + ((creature.HasGroupSaveAdvantage) ? " (adv)" : "") + ((creature.HasGroupSaveDisadvantage) ? " (dis)" : "") + ": Pass";
                        creature.CurrentHitPoints -= HelperMethods.CalculateDamage(true, targetDialog, creature);
                    }
                    else
                    {
                        message += "\n" + creature.DisplayName + ((creature.HasGroupSaveAdvantage) ? " (adv)" : "") + ((creature.HasGroupSaveDisadvantage) ? " (dis)" : "") + ": Fail";
                        creature.CurrentHitPoints -= HelperMethods.CalculateDamage(false, targetDialog, creature);
                    }
                    if (creature.CurrentHitPoints <= 0)
                    {
                        creature.CurrentHitPoints = 0;
                        message += "\n" + creature.DisplayName + " has died.";
                        continue;
                    }
                    if (passedThrow) { continue; } // Skip condition check
                    switch (targetDialog.ConditionOnFail)
                    {
                        case "Special":
                            if (creature.Notes.Length > 0) { creature.Notes += "\n"; }
                            creature.Notes += targetDialog.SpecialCondition;
                            break;
                        case "Blinded":
                            creature.IsBlinded = (!creature.IsImmune_Blinded);
                            if (creature.IsBlinded) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Charmed":
                            creature.IsCharmed = (!creature.IsImmune_Charmed);
                            if (creature.IsCharmed) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Deafened":
                            creature.IsDeafened = (!creature.IsImmune_Deafened);
                            if (creature.IsDeafened) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Frightened":
                            creature.IsFrightened = (!creature.IsImmune_Frightened);
                            if (creature.IsFrightened) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Grappled":
                            creature.IsGrappled = (!creature.IsImmune_Grappled);
                            if (creature.IsGrappled) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Paralyzed":
                            creature.IsParalyzed = (!creature.IsImmune_Paralyzed);
                            if (creature.IsParalyzed) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Petrified":
                            creature.IsPetrified = (!creature.IsImmune_Petrified);
                            if (creature.IsPetrified) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Poisoned":
                            creature.IsPoisoned = (!creature.IsImmune_Poisoned);
                            if (creature.IsPoisoned) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Prone":
                            creature.IsProne = (!creature.IsImmune_Prone);
                            if (creature.IsProne) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Restrained":
                            creature.IsRestrained = (!creature.IsImmune_Restrained);
                            if (creature.IsRestrained) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Stunned":
                            creature.IsStunned = (!creature.IsImmune_Stunned);
                            if (creature.IsStunned) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        case "Unconscious":
                            creature.IsUnconscious = (!creature.IsImmune_Unconscious);
                            if (creature.IsUnconscious) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
                            break;
                        default:
                            break;
                    }
                }
                HelperMethods.AddToCampaignMessages(message, "Saving Throw");
            }
        }
        #endregion
        #region ChangeTime
        private RelayCommand _ChangeTime;
        public ICommand ChangeTime
        {
            get
            {
                if (_ChangeTime == null)
                {
                    _ChangeTime = new RelayCommand(DoChangeTime);
                }
                return _ChangeTime;
            }
        }
        public void DoChangeTime(object time)
        {
            CalendarProgress += Convert.ToInt64(time);
            if ((CalendarProgress - LastWeatherChange) > 240)
            {
                LastWeatherChange = CalendarProgress;
                UpdateWeather();
            }
        }
        #endregion
        #region ChangeWeather
        private RelayCommand _ChangeWeather;
        public ICommand ChangeWeather
        {
            get
            {
                if (_ChangeWeather == null)
                {
                    _ChangeWeather = new RelayCommand(DoChangeWeather);
                }
                return _ChangeWeather;
            }
        }
        private void DoChangeWeather(object param)
        {
            WeatherIntensity = Convert.ToInt32(param);
            LastWeatherChange = CalendarProgress;
            SetWeather();
        }
        #endregion
        #region ChangeActiveCreature
        private RelayCommand _ChangeActiveCreature;
        public ICommand ChangeActiveCreature
        {
            get
            {
                if (_ChangeActiveCreature == null)
                {
                    _ChangeActiveCreature = new RelayCommand(DoChangeActiveCreature);
                }
                return _ChangeActiveCreature;
            }
        }
        private void DoChangeActiveCreature(object param)
        {
            if (Combatants.Count <= 0) { return; }
            CreatureModel activeCreature = Combatants.FirstOrDefault(crt => crt.IsActive);
            CreatureModel firstCreature = Combatants.First();
            CreatureModel lastCreature = Combatants.Last();
            if (activeCreature == null) { param = "Reset"; }
            string action = param.ToString();
            switch (action)
            {
                case "Next":
                    bool foundNext = false;
                    int indexOfNext = -1;
                    do
                    {
                        if (indexOfNext == -1)
                        {
                            indexOfNext = (activeCreature == lastCreature) ? 0 : Combatants.IndexOf(activeCreature) + 1;
                        }
                        if (indexOfNext >= Combatants.Count) { indexOfNext = 0; }
                        if (indexOfNext == 0) { EncounterRound++; }
                        if (Combatants[indexOfNext] == activeCreature) { return; } // if it makes a full round and finds nothing
                        if ((Combatants[indexOfNext].IsOoc || Combatants[indexOfNext].CurrentHitPoints <= 0) && Combatants[indexOfNext].IsPlayer == false) { indexOfNext++; }
                        else { Combatants[indexOfNext].IsActive = true; foundNext = true; }

                    }
                    while (foundNext == false);
                    break;
                case "Previous":
                    bool foundPrev = false;
                    int indexOfPrevious = -1;
                    do
                    {
                        if (indexOfPrevious == -1)
                        {
                            indexOfPrevious = (activeCreature == firstCreature) ? Combatants.IndexOf(lastCreature) : Combatants.IndexOf(activeCreature) - 1;
                        }
                        if (indexOfPrevious == (Combatants.IndexOf(lastCreature))) { EncounterRound--; }
                        if (EncounterRound == 0) { EncounterRound = 1; return; }
                        if (Combatants[indexOfPrevious] == activeCreature) { return; } // if it makes a full round and finds nothing
                        if ((Combatants[indexOfPrevious].IsOoc || Combatants[indexOfPrevious].CurrentHitPoints <= 0) && Combatants[indexOfPrevious].IsPlayer == false) { indexOfPrevious--; }
                        else { Combatants[indexOfPrevious].IsActive = true; foundPrev = true; }
                    }
                    while (foundPrev == false);
                    break;
                case "Reset":
                    YesNoDialog question = new("Reset combat to round 1?");
                    question.ShowDialog();
                    if (question.Answer == false) { return; }
                    CreatureModel resetCreature = Combatants.FirstOrDefault(crt => (crt.IsOoc == false && crt.CurrentHitPoints > 0) || crt.IsPlayer);
                    if (resetCreature == null) { UpdateActiveCombatant(); return; }
                    else { resetCreature.IsActive = true; }
                    EncounterRound = 1;
                    UpdateActiveCombatant();
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region RollFallDamage
        private RelayCommand _RollFallDamage;
        public ICommand RollFallDamage
        {
            get
            {
                if (_RollFallDamage == null)
                {
                    _RollFallDamage = new RelayCommand(param => DoRollFallDamage());
                }
                return _RollFallDamage;
            }
        }
        private void DoRollFallDamage()
        {
            int rolls = FallDistance switch
            {
                int n when (n < 10) => 0,
                int n when (n >= 10 && n <= 19) => 1,
                int n when (n >= 20 && n <= 29) => 2,
                int n when (n >= 30 && n <= 39) => 3,
                int n when (n >= 40 && n <= 49) => 4,
                int n when (n >= 50 && n <= 59) => 5,
                int n when (n >= 60 && n <= 69) => 6,
                int n when (n >= 70 && n <= 79) => 7,
                int n when (n >= 80 && n <= 89) => 8,
                int n when (n >= 90 && n <= 99) => 9,
                int n when (n >= 100 && n <= 109) => 10,
                int n when (n >= 110 && n <= 119) => 11,
                int n when (n >= 120 && n <= 129) => 12,
                int n when (n >= 130 && n <= 139) => 13,
                int n when (n >= 140 && n <= 149) => 14,
                int n when (n >= 150 && n <= 159) => 15,
                int n when (n >= 160 && n <= 169) => 16,
                int n when (n >= 170 && n <= 179) => 17,
                int n when (n >= 180 && n <= 189) => 18,
                int n when (n >= 190 && n <= 199) => 19,
                _ => 20
            };
            if (rolls == 0) { new NotificationDialog("Insufficient height for fall damage.").ShowDialog(); return; }
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            int result = 0;
            string diceRolls = "\nRoll: [";
            for (int i = 0; i < rolls; i++)
            {
                int roll = Configuration.RNG.Next(1, 7);
                if (i > 0) { diceRolls += " + "; }
                diceRolls += roll;
                result += roll;
            }
            diceRolls += "]";
            string message = "Creature falls " + FallDistance + " feet and takes " + result + " bludgeoning damage.";
            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
            {
                message += diceRolls;
            }
            HelperMethods.AddToCampaignMessages(message, "Fall Damage");
        }
        #endregion
        #region RollCustomDice
        public ICommand RollCustomDice => new RelayCommand(DoRollCustomDice);
        private void DoRollCustomDice(object param)
        {
            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
            HelperMethods.RollDice(CustomRollNumber, CustomRollSides, out int result, out List<string> rolls);
            string message = "Custom roll " + CustomRollNumber + "d" + CustomRollSides + "+" + CustomRollModifier;
            message += "\nResult: " + (result + CustomRollModifier);
            message += "\nRoll: [" + HelperMethods.GetStringFromList(rolls, " + ") + "] + " + CustomRollModifier;
            HelperMethods.AddToCampaignMessages(message, "DM Roll");
        }
        #endregion
        #region ClearMessages
        public ICommand ClearMessages => new RelayCommand(DoClearMessages);
        private void DoClearMessages(object param)
        {
            if (param == null) { HelperMethods.WriteToLogFile("No parameter passed for GameCampaign.DoClearMessages.", true); return; }
            switch (param.ToString())
            {
                case "All":
                    YesNoDialog question = new("Clear message history?");
                    question.ShowDialog();
                    if (question.Answer == false) { return; }
                    Messages.Clear();
                    break;
                case "After10":
                    Messages = new(Messages.Take(10));
                    break;
                case "After50":
                    Messages = new(Messages.Take(50));
                    break;
                default:
                    HelperMethods.WriteToLogFile("Invalid parameter " + param.ToString() + " passed to GameCampaign.DoClearMessages.", true);
                    return;
            }
            
        }
        #endregion

        #region AddNote
        private RelayCommand _AddNote;
        public ICommand AddNote
        {
            get
            {
                if (_AddNote == null)
                {
                    _AddNote = new RelayCommand(param => DoAddNote());
                }
                return _AddNote;
            }
        }
        private void DoAddNote()
        {
            Notes.Add(new NoteModel());
            ActiveNote = Notes.Last();
        }
        #endregion
        #region SortNotes
        private RelayCommand _SortNotes;
        public ICommand SortNotes
        {
            get
            {
                if (_SortNotes == null)
                {
                    _SortNotes = new RelayCommand(param => DoSortNotes());
                }
                return _SortNotes;
            }
        }
        private void DoSortNotes()
        {
            string message = "Are you sure you want to auto-sort your notes?" +
                "\nSort Order" +
                "\n1. Category: Location" +
                "\n2. Category: Faction" +
                "\n3. Category: Vendor" +
                "\n4. Category: Character" +
                "\n5. Category: Quest" +
                "\n6. Category: Miscellaneous" +
                "\n7. Header" +
                "\nQuest sub notes are not sorted.";
            YesNoDialog question = new(message);
            if (question.ShowDialog() == false) { return; }

            Notes = new ObservableCollection<NoteModel>(SortNoteList(Notes.ToList()));

        }
        #endregion
        #region PasteNote
        private RelayCommand _PasteNote;
        public ICommand PasteNote
        {
            get
            {
                if (_PasteNote == null)
                {
                    _PasteNote = new RelayCommand(param => DoPasteNote());
                }
                return _PasteNote;
            }
        }
        private void DoPasteNote()
        {
            if (Configuration.CopiedNote == null) { return; }
            Notes.Add(HelperMethods.DeepClone(Configuration.CopiedNote));
        }
        #endregion
        #region SearchNotes
        private RelayCommand _SearchNotes;
        public ICommand SearchNotes
        {
            get
            {
                if (_SearchNotes == null)
                {
                    _SearchNotes = new RelayCommand(param => DoSearchNotes());
                }
                return _SearchNotes;
            }
        }
        private void DoSearchNotes()
        {
            NoteSearchDialog searchDialog = new();
            if (searchDialog.ShowDialog() == true)
            {
                HelperMethods.CheckNoteSearch(Notes, searchDialog.TBX_SearchText.Text, searchDialog.CBX_UseCaseMatch.IsChecked, searchDialog.CBX_LookInHeader.IsChecked, searchDialog.CBX_LookInContent.IsChecked, out _);
            }
        }
        #endregion

        #region AddNpc
        private RelayCommand _AddNpc;
        public ICommand AddNpc
        {
            get
            {
                if (_AddNpc == null)
                {
                    _AddNpc = new RelayCommand(param => DoAddNpc());
                }
                return _AddNpc;
            }
        }
        private void DoAddNpc()
        {
            Npcs.Add(new NpcModel());
            ActiveNpc = Npcs.Last();
        }
        #endregion
        #region SortNpcs
        public ICommand SortNpcs => new RelayCommand(param => DoSortNpcs());
        private void DoSortNpcs()
        {
            Npcs = new(Npcs.OrderBy(npc => !npc.IsActive).ThenBy(npc => npc.Name));
        }
        #endregion
        #region SyncNpcData
        public ICommand SyncNpcData => new RelayCommand(DoSyncNpcData);
        private void DoSyncNpcData(object param)
        {
            foreach (CreatureModel npc in Combatants.Where(c => c.IsNpc))
            {
                NpcModel matchedNpc = Npcs.FirstOrDefault(n => n.Name == npc.DisplayName);
                if (matchedNpc == null) { continue; }
                else
                {
                    npc.Lore = matchedNpc.Description;
                }
            }
            HelperMethods.NotifyUser("NPC Data Synced to Gameplay");
        }
        #endregion

        #region AddCreaturePack
        private RelayCommand _AddCreaturePack;
        public ICommand AddCreaturePack
        {
            get
            {
                if (_AddCreaturePack == null)
                {
                    _AddCreaturePack = new RelayCommand(param => DoAddCreaturePack());
                }
                return _AddCreaturePack;
            }
        }
        private void DoAddCreaturePack()
        {
            Packs.Add(new CreaturePackModel());
            ActivePack = Packs.Last();
        }
        #endregion
        #region SortCreaturePacks
        public ICommand SortCreaturePacks => new RelayCommand(param => DoSortCreaturePacks());
        private void DoSortCreaturePacks()
        {
            Packs = new(Packs.OrderBy(p => !p.IsActive).ThenBy(p => p.Name));
        }
        #endregion
        #region SortPacks
        public ICommand SortPacks => new RelayCommand(DoSortPacks);
        private void DoSortPacks(object param)
        {
            Packs = new(Packs.OrderBy(pack => !pack.IsActive).ThenBy(pack => pack.Name));
        }
        #endregion

        #region AddPlayer
        private RelayCommand _AddPlayer;
        public ICommand AddPlayer
        {
            get
            {
                if (_AddPlayer == null)
                {
                    _AddPlayer = new RelayCommand(param => DoAddPlayer());
                }
                return _AddPlayer;
            }
        }
        private void DoAddPlayer()
        {
            Players.Add(new() { Name = "New Player", IsPlayer = true });
            ActivePlayer = Players.Last();
        }
        #endregion
        #region SortPlayers
        private RelayCommand _SortPlayers;
        public ICommand SortPlayers
        {
            get
            {
                if (_SortPlayers == null)
                {
                    _SortPlayers = new RelayCommand(param => DoSortPlayers());
                }
                return _SortPlayers;
            }
        }
        private void DoSortPlayers()
        {
            Players = new ObservableCollection<CreatureModel>(Players.OrderBy(crt => crt.Name));
        }
        #endregion
        #region SyncPlayerData
        public ICommand SyncPlayerData => new RelayCommand(DoSyncPlayerData);
        private void DoSyncPlayerData(object param)
        {
            foreach (CreatureModel player in Combatants.Where(c => c.IsPlayer))
            {
                CreatureModel matchedPlayer = Players.FirstOrDefault(p => p.Name == player.Name);
                if (matchedPlayer == null) { continue; }
                else
                {
                    player.PlayerRaceAndClass = matchedPlayer.PlayerRaceAndClass;
                    player.ArmorClass = matchedPlayer.ArmorClass;
                    player.PassivePerception = matchedPlayer.PassivePerception;
                    player.SpellSaveDc = matchedPlayer.SpellSaveDc;
                    player.Description = matchedPlayer.Description;
                }
            }
            HelperMethods.NotifyUser("Player Data Synced to Gameplay");
        }
        #endregion

        // Public Methods
        public void UpdateActiveCombatant()
        {
            ActiveCombatant = Combatants.FirstOrDefault(c => c.IsActive);
        }
        public void SortCombatants()
        {
            if (Combatants.Count == 0) 
            {
                CombatantsByName = new();
                CombatantsByIsNpc = new();
                CombatantsByIsPlayer = new();
            }
            Combatants = new(Combatants.OrderBy(item => (item.CurrentHitPoints <= 0 && item.IsPlayer == false)).ThenBy(item => item.IsOoc).ThenByDescending(item => item.Initiative).ToList());
            CombatantsByName = new(Combatants.OrderBy(item => (item.CurrentHitPoints <= 0 && item.IsPlayer == false)).ThenBy(item => item.IsOoc).ThenBy(c => c.DisplayName));
            CombatantsByIsPlayer = new(Combatants.Where(c => c.IsPlayer).OrderBy(c => c.Name));
            CombatantsByIsNpc = new(Combatants.Where(c => c.IsNpc).OrderBy(item => item.IsOoc).ThenBy(c => c.Name));
        }
        public void UpdateCalendarAndWeather()
        {
            WeatherType = WeatherType;
            CalendarProgress = CalendarProgress;
        }

        // Private Methods
        private void CalculateCalendar()
        {
            if (CalendarType == null || CalendarType == "") { return; }
            if (Configuration.MainModelRef.ToolsView == null) { return; }
            GameCalendar linkedCalendar = Configuration.MainModelRef.ToolsView.Calendars.FirstOrDefault(c => c.Name == CalendarType);
            if (linkedCalendar == null) { return; }

            long minutesPerYear = 60 * 24 * linkedCalendar.Days.Count * linkedCalendar.WeeksPerMonth * linkedCalendar.Months.Count;
            long minutesPerMonth = 60 * 24 * linkedCalendar.Days.Count * linkedCalendar.WeeksPerMonth;
            long minutesPerDay = 60 * 24; // 1440
            List<string> fullDayList = new();
            for (int i = 0; i < linkedCalendar.WeeksPerMonth; i++)
            {
                foreach (ConvertibleValue day in linkedCalendar.Days)
                {
                    fullDayList.Add(day.Value);
                }
            }

            if (CalendarStart >= 0)
            {
                long years = CalendarStart / minutesPerYear;
                long eras = (linkedCalendar.UseEras) ? years / linkedCalendar.YearsPerEra : 0;
                long months = (CalendarStart - (years * minutesPerYear)) / minutesPerMonth;
                long days = (CalendarStart - (years * minutesPerYear) - (months * minutesPerMonth)) / minutesPerDay;
                long hours = (CalendarStart - (years * minutesPerYear) - (months * minutesPerMonth) - (days * minutesPerDay)) / 60;
                long minutes = CalendarStart - (years * minutesPerYear) - (months * minutesPerMonth) - (days * minutesPerDay) - (hours * 60);
                string processedValue = fullDayList[(int)days] +
                    ", " + (days + 1) + HelperMethods.GetSuffixFromNumber((days + 1)) + " of " + linkedCalendar.Months[Convert.ToInt32(months)].Value +
                    ", " + ((linkedCalendar.UseEras) ? eras + "E" + (years - (eras * linkedCalendar.YearsPerEra) + 1) : (years + 1));
                CalendarStart_Processed = processedValue;
                CalendarStart_Time = (((hours > 12) ? (hours - 12) : ((hours == 0) ? 12 : hours)) + ":" + ((minutes < 10) ? ("0" + minutes) : minutes)) + " " + ((hours > 12) ? "PM" : "AM");
            }

            if (CalendarProgress >= 0)
            {
                long years = CalendarProgress / minutesPerYear;
                long eras = (linkedCalendar.UseEras) ? years / linkedCalendar.YearsPerEra : 0;
                long months = (CalendarProgress - (years * minutesPerYear)) / minutesPerMonth;
                long days = (CalendarProgress - (years * minutesPerYear) - (months * minutesPerMonth)) / minutesPerDay;
                long hours = (CalendarProgress - (years * minutesPerYear) - (months * minutesPerMonth) - (days * minutesPerDay)) / 60;
                long minutes = CalendarProgress - (years * minutesPerYear) - (months * minutesPerMonth) - (days * minutesPerDay) - (hours * 60);
                string processedValue = fullDayList[(int)days] +
                    ", " + (days + 1) + HelperMethods.GetSuffixFromNumber((days + 1)) + " of " + linkedCalendar.Months[Convert.ToInt32(months)].Value +
                    ", " + ((linkedCalendar.UseEras) ? eras + "E" + (years - (eras * linkedCalendar.YearsPerEra) + 1) : (years + 1));
                CalendarProgress_Processed = processedValue;
                TimeDigits = ((hours > 12) ? (hours - 12) : ((hours == 0) ? 12 : hours)) + ":" + ((minutes < 10) ? ("0" + minutes) : minutes);
                TimeIndicator = (hours >= 12) ? "PM" : "AM";
            }

            if (CalendarStart < CalendarProgress)
            {
                AdventureDayCount = Convert.ToInt32((CalendarProgress / minutesPerDay) - (CalendarStart / minutesPerDay));
            }

        }
        private void UpdateWeather()
        {
            int result = Configuration.RNG.Next(-20, 20);
            WeatherIntensity += Convert.ToInt32((50 - WeatherIntensity) * ((WeatherIntensity > 50) ? 0.15 : 0.38)); // Adjust weather intensity towards center
            WeatherIntensity += result;
            if (WeatherIntensity > 100) { WeatherIntensity = 100; }
            if (WeatherIntensity < 1) { WeatherIntensity = 1; }
            SetWeather();
        }
        private void SetWeather()
        {
            if (LinkedWeather == null) { return; }
            string oldWeather = WeatherName;
            foreach (WeatherRow wr in LinkedWeather.WeatherEntries)
            {
                if (WeatherIntensity >= wr.LowValue && WeatherIntensity <= wr.HighValue)
                {
                    WeatherName = wr.Name;
                    WeatherInfo = wr.Description;
                    WeatherIcon = wr.Icon;
                }
            }
            if (WeatherName != oldWeather) { HelperMethods.AddToCampaignMessages("Weather has changed to " + WeatherName + ".", "Weather Change"); }
        }
        private List<NoteModel> SortNoteList(List<NoteModel> notes)
        {
            List<NoteModel> sortedNotes = notes.OrderBy(n => n.Category).ThenBy(n => n.Header).ToList();
            foreach (NoteModel note in sortedNotes)
            {
                NoteType nt = Configuration.MainModelRef.ToolsView.NoteTypes.FirstOrDefault(n => n.Name == note.Category);
                if (nt == null)
                {
                    note.SubNotes = new(SortNoteList(note.SubNotes.ToList()));
                }
                else if (nt.SortSubNotes)
                {
                    note.SubNotes = new(SortNoteList(note.SubNotes.ToList()));
                }
            }
            return sortedNotes;
        }

    }
}
