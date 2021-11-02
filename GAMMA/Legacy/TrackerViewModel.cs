//using GAMMA.Models;
//using GAMMA.Toolbox;
//using GAMMA.Windows;
//using Microsoft.Win32;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows.Input;
//using System.Xml.Linq;

//namespace GAMMA.ViewModels
//{
//    public class TrackerViewModel : BaseModel
//    {
//        // Constructors
//        public TrackerViewModel()
//        {
//            ActiveCreatures = new ObservableCollection<CreatureModel>();
//            EventMessages = new ObservableCollection<string>();
//            EncounterTime = Configuration.StartDateTime;
//            LastWeatherChange = Configuration.StartDateTime;
//            WeatherIntensity = 30;
//            SetWeather();
//            DoChangeTime(0);
//            foreach (CreatureModel creature in ActiveCreatures)
//            {
//                creature.UpdateToNewSpellSystem();
//                creature.ConnectSpellLinks();
//                creature.UpdateToNewLootSystem();
//                creature.ConnectItemLinks();
//            }
//            FallDistance = 10;
//        }

//        // Databound Properties
//        #region EventMessages
//        private ObservableCollection<string> _EventMessages;
//        public ObservableCollection<string> EventMessages
//        {
//            get
//            {
//                return _EventMessages;
//            }
//            set
//            {
//                _EventMessages = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region ActiveCreature
//        private CreatureModel _ActiveCreature;
//        public CreatureModel ActiveCreature
//        {
//            get
//            {
//                return _ActiveCreature;
//            }
//            set
//            {
//                _ActiveCreature = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region ActiveCreatures
//        private ObservableCollection<CreatureModel> _ActiveCreatures;
//        public ObservableCollection<CreatureModel> ActiveCreatures
//        {
//            get
//            {
//                return _ActiveCreatures;
//            }
//            set
//            {
//                _ActiveCreatures = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region TimeDigits
//        private string _TimeDigits;
//        public string TimeDigits
//        {
//            get
//            {
//                return _TimeDigits;
//            }
//            set
//            {
//                _TimeDigits = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region TimeIndicator
//        private string _TimeIndicator;
//        public string TimeIndicator
//        {
//            get
//            {
//                return _TimeIndicator;
//            }
//            set
//            {
//                _TimeIndicator = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region CreatureSearch
//        private string _CreatureSearch;
//        public string CreatureSearch
//        {
//            get
//            {
//                return _CreatureSearch;
//            }
//            set
//            {
//                _CreatureSearch = value;
//                NotifyPropertyChanged();

//                if (value == null) { return; }
//                foreach (CreatureModel creature in ActiveCreatures)
//                {
//                    string name = (creature.IsPlayer) ? creature.Name : creature.DisplayName;
//                    if (name.Contains(value) && value != "")
//                    {
//                        creature.TrackerSearchMatch = true;
//                    }
//                    else
//                    {
//                        creature.TrackerSearchMatch = false;
//                    }
//                }
//            }
//        }
//        #endregion
//        #region LastRestDate
//        private string _LastRestDate;
//        public string LastRestDate
//        {
//            get
//            {
//                return _LastRestDate;
//            }
//            set
//            {
//                _LastRestDate = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region AdventureDayNumber
//        private int _AdventureDayNumber;
//        public int AdventureDayNumber
//        {
//            get
//            {
//                return _AdventureDayNumber;
//            }
//            set
//            {
//                _AdventureDayNumber = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region CurrentWeather
//        private string _CurrentWeather;
//        public string CurrentWeather
//        {
//            get
//            {
//                return _CurrentWeather;
//            }
//            set
//            {
//                _CurrentWeather = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region WeatherDescription
//        private string _WeatherDescription;
//        public string WeatherDescription
//        {
//            get
//            {
//                return _WeatherDescription;
//            }
//            set
//            {
//                _WeatherDescription = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region WeatherIntensity
//        private int _WeatherIntensity;
//        public int WeatherIntensity
//        {
//            get
//            {
//                return _WeatherIntensity;
//            }
//            set
//            {
//                _WeatherIntensity = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region DisplayPopup_ChangeTime
//        private bool _DisplayPopup_ChangeTime;
//        public bool DisplayPopup_ChangeTime
//        {
//            get
//            {
//                return _DisplayPopup_ChangeTime;
//            }
//            set
//            {
//                _DisplayPopup_ChangeTime = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region Notes
//        private string _Notes;
//        public string Notes
//        {
//            get
//            {
//                return _Notes;
//            }
//            set
//            {
//                _Notes = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region EncounterRound
//        private int _EncounterRound;
//        public int EncounterRound
//        {
//            get
//            {
//                return _EncounterRound;
//            }
//            set
//            {
//                _EncounterRound = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion

//        // Other Rolls
//        #region OtherRollsOpen
//        private bool _OtherRollsOpen;
//        public bool OtherRollsOpen
//        {
//            get
//            {
//                return _OtherRollsOpen;
//            }
//            set
//            {
//                _OtherRollsOpen = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region FallDistance
//        private int _FallDistance;
//        public int FallDistance
//        {
//            get
//            {
//                return _FallDistance;
//            }
//            set
//            {
//                _FallDistance = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion

//        // Public Properties
//        public DateTime EncounterTime;
//        public DateTime LastWeatherChange;

//        // Commands
//        #region ChangeTime
//        private RelayCommand _ChangeTime;
//        public ICommand ChangeTime
//        {
//            get
//            {
//                if (_ChangeTime == null)
//                {
//                    _ChangeTime = new RelayCommand(DoChangeTime);
//                }
//                return _ChangeTime;
//            }
//        }
//        public void DoChangeTime(object time)
//        {
//            EncounterTime = EncounterTime.AddMinutes(Convert.ToDouble(time));
//            TimeDigits = string.Format("{0}:{1}", 
//                (EncounterTime.Hour > 12) ? EncounterTime.Hour - 12 : (EncounterTime.Hour == 0) ? 12 : EncounterTime.Hour, 
//                (EncounterTime.Minute < 10) ? "0" + EncounterTime.Minute : EncounterTime.Minute.ToString());
//            TimeIndicator = (EncounterTime.Hour >= 12) ? "PM" : "AM";
//            AdventureDayNumber = (EncounterTime - Configuration.StartDateTime).Days + 1;
//            if ((EncounterTime - LastWeatherChange).Hours >= 4)
//            {
//                LastWeatherChange = EncounterTime;
//                UpdateWeather();
//            }
//        }
//        #endregion
//        #region RollDice
//        private RelayCommand _RollDice;
//        public ICommand RollDice
//        {
//            get
//            {
//                if (_RollDice == null)
//                {
//                    _RollDice = new RelayCommand(DoRollDice);
//                }
//                return _RollDice;
//            }
//        }
//        private void DoRollDice(object diceSides)
//        {
//            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
//            int result = Configuration.RNG.Next(1, Convert.ToInt32(diceSides) + 1);
//            EventMessages.Insert(0, "DM rolls 1d" + diceSides + "\nResult: " + result);
//        }
//        #endregion
//        #region FlipCoin
//        private RelayCommand _FlipCoin;
//        public ICommand FlipCoin
//        {
//            get
//            {
//                if (_FlipCoin == null)
//                {
//                    _FlipCoin = new RelayCommand(param => DoFlipCoin());
//                }
//                return _FlipCoin;
//            }
//        }
//        private void DoFlipCoin()
//        {
//            int result = Configuration.RNG.Next(1, 3);
//            EventMessages.Insert(0, string.Format("DM flips a coin.\nResult: {0}.", (result == 1) ? "Heads" : "Tails"));
//        }
//        #endregion
//        #region AddCreatures
//        private RelayCommand _AddCreatures;
//        public ICommand AddCreatures
//        {
//            get
//            {
//                if (_AddCreatures == null)
//                {
//                    _AddCreatures = new RelayCommand(DoAddCreatures);
//                }
//                return _AddCreatures;
//            }
//        }
//        private void DoAddCreatures(object parameter)
//        {
//            MultiObjectSelectionDialog selectionDialog;
            
//            if (parameter.ToString() == "Creatures")
//            {
//                selectionDialog = new MultiObjectSelectionDialog(Configuration.CreatureRepository.Where(creature => creature.IsValidated == true && creature.IsPlayer == false).ToList(), "Creatures");
//            }
//            else
//            {
//                selectionDialog = new MultiObjectSelectionDialog(Configuration.CreatureRepository.Where(creature => creature.IsPlayer == true).ToList(), "Players");
//            }
            
//            if (selectionDialog.ShowDialog() == true)
//            {
//                foreach (CreatureModel selectedCreature in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCreatures)
//                {
//                    for (int i = 0; i < selectedCreature.QuantityToAdd; i++)
//                    {
//                        CreatureModel newCreature = HelperMethods.DeepClone(selectedCreature);
//                        int existingCreatureCount = ActiveCreatures.Where(creature => creature.Name == newCreature.Name).Count();
//                        if (existingCreatureCount > 25) { break; }
//                        newCreature.RollHitPoints(Configuration.MainModelRef.SettingsView.UseAveragedHitPoints);
//                        newCreature.SetPassivePerception();
//                        newCreature.HasBeenLooted = false;
//                        if (newCreature.IsPlayer == false) { newCreature.TrackerIndicator = Configuration.AlphaArray[existingCreatureCount]; }
//                        newCreature.DisplayName = newCreature.Name + " " + newCreature.TrackerIndicator;
//                        newCreature.SetFormattedTexts();
//                        ActiveCreatures.Add(newCreature);
//                    }
//                }
//            }

//        }
//        #endregion
//        #region AddPack
//        private RelayCommand _AddPack;
//        public ICommand AddPack
//        {
//            get
//            {
//                if (_AddPack == null)
//                {
//                    _AddPack = new RelayCommand(param => DoAddPack());
//                }
//                return _AddPack;
//            }
//        }
//        private void DoAddPack()
//        {
//            ObjectSelectionDialog packSelect;

//            packSelect = new ObjectSelectionDialog(Configuration.MainModelRef.ToolsView.CreaturePacks.Where(pack => pack.CreatureList.Count() > 0).ToList());
//            if (packSelect.ShowDialog() == true)
//            {
//                if (packSelect.SelectedObject == null) { return; }

//                CreaturePackModel selectedPack = packSelect.SelectedObject as CreaturePackModel;

//                string msg = "";
//                foreach (PackCreatureModel creature in selectedPack.CreatureList)
//                {
//                    CreatureModel matchedCreature = Configuration.CreatureRepository.FirstOrDefault(crt => crt.Name == creature.CreatureName);
//                    if (matchedCreature == null) { msg += "\nCould not find creature \"" + creature.CreatureName + "\"."; continue; }

//                    for (int i = 0; i < creature.Quantity; i++)
//                    {
//                        CreatureModel newCreature = HelperMethods.DeepClone(matchedCreature);
//                        int existingCreatureCount = ActiveCreatures.Where(creature => creature.Name == newCreature.Name).Count();
//                        if (existingCreatureCount > 25) { break; }
//                        newCreature.RollHitPoints(Configuration.MainModelRef.SettingsView.UseAveragedHitPoints);
//                        newCreature.SetPassivePerception();
//                        newCreature.HasBeenLooted = false;
//                        if (newCreature.IsPlayer == false) { newCreature.TrackerIndicator = Configuration.AlphaArray[existingCreatureCount]; }
//                        newCreature.DisplayName = newCreature.Name + " " + newCreature.TrackerIndicator;
//                        newCreature.SetFormattedTexts();
//                        newCreature.IsAlly = selectedPack.IsAlly;
//                        ActiveCreatures.Add(newCreature);
//                    }
//                }
//                foreach (PackCreatureModel creature in selectedPack.NpcList)
//                {
//                    NpcModel npc = Configuration.MainModelRef.ToolsView.Npcs.FirstOrDefault(npc => npc.Name == creature.CreatureName);
//                    CreatureModel baseCreature = Configuration.CreatureRepository.FirstOrDefault(creature => creature.Name == npc.BaseCreatureName);
//                    if (baseCreature == null)
//                    {
//                        msg += "\nUnable to find base creature " + npc.BaseCreatureName + ", NPC " + npc.Name + " not added.";
//                        continue;
//                    }
//                    CreatureModel newCreature = HelperMethods.DeepClone(baseCreature);
//                    newCreature.DisplayName = npc.Name;
//                    newCreature.Name = npc.Name;
//                    newCreature.IsNpc = true;
//                    newCreature.IsAlly = npc.IsFriendly;
//                    newCreature.RollHitPoints(true);
//                    newCreature.SetPassivePerception();
//                    newCreature.HasBeenLooted = false;
//                    newCreature.SetFormattedTexts();
//                    ActiveCreatures.Add(newCreature);
//                }

//                if (msg != "")
//                {
//                    new NotificationDialog(msg).ShowDialog();
//                }

//            }

//        }
//        #endregion
//        #region AddNpcs
//        private RelayCommand _AddNpcs;
//        public ICommand AddNpcs
//        {
//            get
//            {
//                if (_AddNpcs == null)
//                {
//                    _AddNpcs = new RelayCommand(param => DoAddNpcs());
//                }
//                return _AddNpcs;
//            }
//        }
//        private void DoAddNpcs()
//        {
//            MultiObjectSelectionDialog selectionDialog = new MultiObjectSelectionDialog(Configuration.MainModelRef.ToolsView.Npcs.Where(npc => npc.BaseCreatureName != "").ToList());
//            if (selectionDialog.ShowDialog() == true)
//            {
//                string msg = "";
//                foreach (NpcModel selectedNpc in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedNpcs)
//                {
//                    CreatureModel baseCreature = Configuration.CreatureRepository.FirstOrDefault(creature => creature.Name == selectedNpc.BaseCreatureName);
//                    if (baseCreature == null) 
//                    { 
//                        msg += "\nUnable to find base creature " + selectedNpc.BaseCreatureName + ", NPC " + selectedNpc.Name + " not added.";
//                        continue;
//                    }
//                    CreatureModel newCreature = HelperMethods.DeepClone(baseCreature);
//                    newCreature.DisplayName = selectedNpc.Name;
//                    newCreature.Name = selectedNpc.Name;
//                    newCreature.IsNpc = true;
//                    newCreature.IsAlly = selectedNpc.IsFriendly;
//                    newCreature.RollHitPoints(true);
//                    newCreature.SetPassivePerception();
//                    newCreature.HasBeenLooted = false;
//                    newCreature.SetFormattedTexts();
//                    ActiveCreatures.Add(newCreature);
//                }
//                if (msg != "")
//                {
//                    new NotificationDialog(msg).ShowDialog();
//                }
//            }
//        }
//        #endregion
//        #region NewEncounter
//        private RelayCommand _NewEncounter;
//        public ICommand NewEncounter
//        {
//            get
//            {
//                if (_NewEncounter == null)
//                {
//                    _NewEncounter = new RelayCommand(param => DoNewEncounter());
//                }
//                return _NewEncounter;
//            }
//        }
//        private void DoNewEncounter()
//        {
//            YesNoDialog question = new YesNoDialog("This will reset the campaign day counter and clear all creatures.\nContinue?");
//            question.ShowDialog();
//            if (question.Answer == true)
//            {
//                ActiveCreatures = new ObservableCollection<CreatureModel>();
//                ActiveCreature = null;
//                EventMessages = new ObservableCollection<string>();
//                LastRestDate = "";
//                EncounterRound = 1;
//                Notes = "";
//                EncounterTime = Configuration.StartDateTime;
//                LastWeatherChange = Configuration.StartDateTime;
//                WeatherIntensity = 30;
//                SetWeather();
//                DoChangeTime(0);
//            }
//        }
//        #endregion
//        #region LoadEncounter
//        private RelayCommand _LoadEncounter;
//        public ICommand LoadEncounter
//        {
//            get
//            {
//                if (_LoadEncounter == null)
//                {
//                    _LoadEncounter = new RelayCommand(param => DoLoadEncounter());
//                }
//                return _LoadEncounter;
//            }
//        }
//        private void DoLoadEncounter()
//        {
//            OpenFileDialog openWindow = new OpenFileDialog
//            {
//                Filter = Configuration.XmlFileFilter,
//                InitialDirectory = Environment.CurrentDirectory + "\\Encounters"
//            };
//            if (openWindow.ShowDialog() == true)
//            {
//                LoadTrackerEncounter(openWindow.FileName);
//            }

//        }
//        #endregion
//        #region SaveEncounter
//        private RelayCommand _SaveEncounter;
//        public ICommand SaveEncounter
//        {
//            get
//            {
//                if (_SaveEncounter == null)
//                {
//                    _SaveEncounter = new RelayCommand(param => DoSaveEncounter());
//                }
//                return _SaveEncounter;
//            }
//        }
//        public void DoSaveEncounter()
//        {
//            if (ActiveCreatures.Count() == 0) 
//            {
//                new NotificationDialog("No combatants to save.").ShowDialog();
//                return; 
//            }
//            SaveFileDialog saveWindow = new SaveFileDialog
//            {
//                Filter = "XML Files (*.xml)|*.xml",
//                InitialDirectory = Environment.CurrentDirectory + "\\Encounters"
//            };
//            if (saveWindow.ShowDialog() == true)
//            {
//                SaveTrackerEncounter(saveWindow.FileName);
//                new NotificationDialog("Encounter Saved.").ShowDialog();
//            }
//        }
//        #endregion
//        #region RollInitiatives
//        private RelayCommand _RollInitiatives;
//        public ICommand RollInitiatives
//        {
//            get
//            {
//                if (_RollInitiatives == null)
//                {
//                    _RollInitiatives = new RelayCommand(DoRollInitiatives);
//                }
//                return _RollInitiatives;
//            }
//        }
//        private void DoRollInitiatives(object forceReroll)
//        {
//            bool reroll = Convert.ToBoolean(forceReroll);
//            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);

//            foreach (CreatureModel Creature in ActiveCreatures)
//            {
//                if (Creature.IsPlayer == false)
//                {
//                    if (Creature.Initiative == 0 || reroll)
//                    {
//                        Creature.Initiative = Configuration.RNG.Next(1, 21) + HelperMethods.GetAttributeModifier(Creature.Attr_Dexterity);
//                    }
//                }
//            }

//            SortCombatants();

//        }
//        #endregion
//        #region ResortByInitiative
//        private RelayCommand _ResortByInitiative;
//        public ICommand ResortByInitiative
//        {
//            get
//            {
//                if (_ResortByInitiative == null)
//                {
//                    _ResortByInitiative = new RelayCommand(param => DoResortByInitiative());
//                }
//                return _ResortByInitiative;
//            }
//        }
//        private void DoResortByInitiative()
//        {
//            SortCombatants();
//        }
//        #endregion
//        #region ClearCreatures
//        private RelayCommand _ClearCreatures;
//        public ICommand ClearCreatures
//        {
//            get
//            {
//                if (_ClearCreatures == null)
//                {
//                    _ClearCreatures = new RelayCommand(DoClearCreatures);
//                }
//                return _ClearCreatures;
//            }
//        }
//        private void DoClearCreatures(object parameter)
//        {

//            if (parameter.ToString() == "All")
//            {
//                YesNoDialog question = new YesNoDialog("Clear all creatures?");
//                question.ShowDialog();
//                if (question.Answer == false) { return; }

//                ActiveCreatures = new ObservableCollection<CreatureModel>();
//                ActiveCreature = null;
//                return;
//            }

//            if (parameter.ToString() == "Dead Creatures")
//            {
//                ActiveCreatures = new ObservableCollection<CreatureModel>(ActiveCreatures.Where(creature => creature.CurrentHitPoints > 0 || creature.IsPlayer == true));
//            }
            
//        }
//        #endregion
//        #region KillCreatures
//        private RelayCommand _KillCreatures;
//        public ICommand KillCreatures
//        {
//            get
//            {
//                if (_KillCreatures == null)
//                {
//                    _KillCreatures = new RelayCommand(param => DoKillCreatures());
//                }
//                return _KillCreatures;
//            }
//        }
//        private void DoKillCreatures()
//        {
//            YesNoDialog question = new YesNoDialog("Kill all enemies?");
//            question.ShowDialog();
//            if (question.Answer == false) { return; }

//            foreach (CreatureModel creature in ActiveCreatures)
//            {
//                if (creature.IsAlly == false)
//                {
//                    creature.CurrentHitPoints = 0;
//                }
//            }

//        }
//        #endregion
//        #region ClearHistory
//        private RelayCommand _ClearHistory;
//        public ICommand ClearHistory
//        {
//            get
//            {
//                if (_ClearHistory == null)
//                {
//                    _ClearHistory = new RelayCommand(param => DoClearHistory());
//                }
//                return _ClearHistory;
//            }
//        }
//        private void DoClearHistory()
//        {
//            EventMessages = new ObservableCollection<string>();
//        }
//        #endregion
//        #region LootAll
//        private RelayCommand _LootAll;
//        public ICommand LootAll
//        {
//            get
//            {
//                if (_LootAll == null)
//                {
//                    _LootAll = new RelayCommand(param => DoLootAll());
//                }
//                return _LootAll;
//            }
//        }
//        private void DoLootAll()
//        {
//            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
//            int totalGoldDrop = 0;
//            int totalSilverDrop = 0;
//            int totalCopperDrop = 0;
//            int totalXp = 0;
//            Dictionary<string, int> lootedItems = new Dictionary<string, int>();
//            string message = "Encounter loot found: ";

//            foreach (CreatureModel creature in ActiveCreatures)
//            {
//                if (creature.HasBeenLooted || creature.CurrentHitPoints > 0) { continue; }
//                if (creature.CoinDrop > 0)
//                {
//                    int newCoinDrop = Configuration.RNG.Next(creature.CoinDrop / 2, creature.CoinDrop + 1);
//                    int newGoldDrop = newCoinDrop / 100;
//                    int newSilverDrop = (newCoinDrop - (newGoldDrop * 100)) / 10;
//                    int newCopperDrop = newCoinDrop - (newGoldDrop * 100) - (newSilverDrop * 10);
//                    totalGoldDrop += newGoldDrop;
//                    totalSilverDrop += newSilverDrop;
//                    totalCopperDrop += newCopperDrop;
//                }
//                foreach (ItemLink item in creature.ItemLinks)
//                {
//                    int dropQty = 0;
//                    for (int i = 0; i < item.Quantity; i++)
//                    {
//                        dropQty += (Configuration.RNG.Next(1, 101) <= item.DropChance) ? 1 : 0;
//                    }
//                    if (dropQty > 0) 
//                    {
//                        if (lootedItems.ContainsKey(item.Name)) { lootedItems[item.Name] += dropQty; }
//                        else { lootedItems.Add(item.Name, dropQty); }
//                    }

//                    //int dropRoll = Configuration.RNG.Next(1, 101);
//                    //if (dropRoll <= item.DropChance)
//                    //{
//                    //    int quantityRoll = Configuration.RNG.Next(1, item.Quantity + 1);
//                    //    if (lootedItems.ContainsKey(item.Name)) { lootedItems[item.Name] += quantityRoll; }
//                    //    else { lootedItems.Add(item.Name, quantityRoll); }
//                    //}
//                }
//                creature.HasBeenLooted = true;
//                if (creature.IsAlly == false) { totalXp += creature.ExperienceValue; }
//            }

//            message += string.Format("\nMoney: {0}{1}{2}{3}{4}",
//                (totalGoldDrop > 0) ? totalGoldDrop + "gp" : "",
//                (totalGoldDrop > 0 && totalSilverDrop > 0) ? " " : "",
//                (totalSilverDrop > 0) ? totalSilverDrop + "sp" : "",
//                (totalSilverDrop > 0 && totalCopperDrop > 0) ? " " : "",
//                (totalCopperDrop > 0) ? totalCopperDrop + "cp" : "");
//            foreach (KeyValuePair<string, int> item in lootedItems)
//            {
//                message += "\n" + item.Value + " x " + item.Key;
//            }

//            if (totalGoldDrop == 0 && totalSilverDrop == 0 && totalCopperDrop == 0 && lootedItems.Count() == 0) { message = "No loot found."; }

//            if (Configuration.MainModelRef.SettingsView.UseExperiencePoints) { message += "\n" + totalXp + " experience points gained."; }

//            HelperMethods.AddToEncounterLog(message);

//        }
//        #endregion
//        #region ProcessGroupSave
//        private RelayCommand _ProcessGroupSave;
//        public ICommand ProcessGroupSave
//        {
//            get
//            {
//                if (_ProcessGroupSave == null)
//                {
//                    _ProcessGroupSave = new RelayCommand(param => DoProcessGroupSave());
//                }
//                return _ProcessGroupSave;
//            }
//        }
//        private void DoProcessGroupSave()
//        {
//            EncounterMultiTargetDialog targetDialog = new EncounterMultiTargetDialog(ActiveCreatures.Where(creature => creature.IsPlayer == false && creature.IsOoc == false && creature.CurrentHitPoints > 0).ToList());
//            if (targetDialog.ShowDialog() == true)
//            {
//                if (targetDialog.SelectedCreatures.Count() <= 0) { return; }
//                string message = "Multiple creatures made a saving throw.";
//                message += "\nSave Ability: " + targetDialog.SaveAbility;
//                message += "\nSave Difficulty: " + targetDialog.SaveDifficulty;
//                if (targetDialog.ComboBox_EffectType.SelectedItem.ToString() == "Attack")
//                {
//                    message += "\nDamage on Fail: " + targetDialog.PrimaryDamageOnFail + " " + targetDialog.PrimaryDamageType;
//                    if (targetDialog.SecondaryDamageOnFail > 0) { message += ", " + targetDialog.SecondaryDamageOnFail + " " + targetDialog.SecondaryDamageType + " damage"; } else { message += " damage"; }
//                    message += (targetDialog.HalfOnSave) ? "\nDamage on Save: " + (targetDialog.PrimaryDamageOnFail / 2) + " " + targetDialog.PrimaryDamageType : "";
//                    if (targetDialog.SecondaryDamageOnFail > 0 && targetDialog.HalfOnSave) { message += ", " + (targetDialog.SecondaryDamageOnFail / 2) + " " + targetDialog.SecondaryDamageType + " damage"; }
//                    else if (targetDialog.SecondaryDamageOnFail == 0 && targetDialog.HalfOnSave) { message += " damage"; }
//                }
//                foreach (CreatureModel creature in targetDialog.SelectedCreatures)
//                {
//                    int firstThrow = Configuration.RNG.Next(1, 21);
//                    int secondThrow = Configuration.RNG.Next(1, 21);
//                    int savingThrow = 0;

//                    if (creature.HasGroupSaveAdvantage)
//                    {
//                        savingThrow = (firstThrow > secondThrow) ? firstThrow : secondThrow;
//                    }
//                    else if (creature.HasGroupSaveDisadvantage)
//                    {
//                        savingThrow = (firstThrow < secondThrow) ? firstThrow : secondThrow;
//                    }
//                    else
//                    {
//                        savingThrow = firstThrow;
//                    }

//                    savingThrow += targetDialog.SaveAbility switch
//                    {
//                        "Strength" => creature.StrengthSave,
//                        "Dexterity" => creature.DexteritySave,
//                        "Constitution" => creature.ConstitutionSave,
//                        "Wisdom" => creature.WisdomSave,
//                        "Intelligence" => creature.IntelligenceSave,
//                        "Charisma" => creature.CharismaSave,
//                        _ => 0
//                    };

//                    bool passedThrow = (savingThrow >= targetDialog.SaveDifficulty);
//                    if (passedThrow)
//                    {
//                        message += "\n" + creature.DisplayName + ((creature.HasGroupSaveAdvantage) ? " (adv)" : "") + ((creature.HasGroupSaveDisadvantage) ? " (dis)" : "") + ": Pass";
//                        creature.CurrentHitPoints -= HelperMethods.CalculateDamage(true, targetDialog, creature);
//                    }
//                    else
//                    {
//                        message += "\n" + creature.DisplayName + ((creature.HasGroupSaveAdvantage) ? " (adv)" : "") + ((creature.HasGroupSaveDisadvantage) ? " (dis)" : "") + ": Fail";
//                        creature.CurrentHitPoints -= HelperMethods.CalculateDamage(false, targetDialog, creature);
//                    }
//                    if (creature.CurrentHitPoints <= 0) 
//                    { 
//                        creature.CurrentHitPoints = 0;
//                        message += "\n" + creature.DisplayName + " has died.";
//                        continue;
//                    }
//                    if (passedThrow) { continue; } // Skip condition check
//                    switch (targetDialog.ConditionOnFail)
//                    {
//                        case "Special":
//                            if (creature.Notes.Length > 0) { creature.Notes += "\n"; }
//                            creature.Notes += targetDialog.SpecialCondition;
//                            break;
//                        case "Blinded":
//                            creature.IsBlinded = (!creature.IsImmune_Blinded);
//                            if (creature.IsBlinded) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Charmed":
//                            creature.IsCharmed = (!creature.IsImmune_Charmed);
//                            if (creature.IsCharmed) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Deafened":
//                            creature.IsDeafened = (!creature.IsImmune_Deafened);
//                            if (creature.IsDeafened) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Frightened":
//                            creature.IsFrightened = (!creature.IsImmune_Frightened);
//                            if (creature.IsFrightened) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Grappled":
//                            creature.IsGrappled = (!creature.IsImmune_Grappled);
//                            if (creature.IsGrappled) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Paralyzed":
//                            creature.IsParalyzed = (!creature.IsImmune_Paralyzed);
//                            if (creature.IsParalyzed) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Petrified":
//                            creature.IsPetrified = (!creature.IsImmune_Petrified);
//                            if (creature.IsPetrified) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Poisoned":
//                            creature.IsPoisoned = (!creature.IsImmune_Poisoned);
//                            if (creature.IsPoisoned) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Prone":
//                            creature.IsProne = (!creature.IsImmune_Prone);
//                            if (creature.IsProne) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Restrained":
//                            creature.IsRestrained = (!creature.IsImmune_Restrained);
//                            if (creature.IsRestrained) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Stunned":
//                            creature.IsStunned = (!creature.IsImmune_Stunned);
//                            if (creature.IsStunned) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        case "Unconscious":
//                            creature.IsUnconscious = (!creature.IsImmune_Unconscious);
//                            if (creature.IsUnconscious) { message += "\n" + creature.DisplayName + " has become " + targetDialog.ConditionOnFail + "."; }
//                            break;
//                        default:
//                            break;
//                    }
//                }
//                HelperMethods.AddToEncounterLog(message);
//            }
//        }
//        #endregion
//        #region MarkRestDate
//        private RelayCommand _MarkRestDate;
//        public ICommand MarkRestDate
//        {
//            get
//            {
//                if (_MarkRestDate == null)
//                {
//                    _MarkRestDate = new RelayCommand(param => DoMarkRestDate());
//                }
//                return _MarkRestDate;
//            }
//        }
//        private void DoMarkRestDate()
//        {
//            LastRestDate = "Day " + AdventureDayNumber + ", " + TimeDigits + TimeIndicator;
//        }
//        #endregion
//        #region ChangeWeather
//        private RelayCommand _ChangeWeather;
//        public ICommand ChangeWeather
//        {
//            get
//            {
//                if (_ChangeWeather == null)
//                {
//                    _ChangeWeather = new RelayCommand(DoChangeWeather);
//                }
//                return _ChangeWeather;
//            }
//        }
//        private void DoChangeWeather(object param)
//        {
//            WeatherIntensity = Convert.ToInt32(param);
//            SetWeather();
//        }
//        #endregion
//        #region ChangeActiveCreature
//        private RelayCommand _ChangeActiveCreature;
//        public ICommand ChangeActiveCreature
//        {
//            get
//            {
//                if (_ChangeActiveCreature == null)
//                {
//                    _ChangeActiveCreature = new RelayCommand(DoChangeActiveCreature);
//                }
//                return _ChangeActiveCreature;
//            }
//        }
//        private void DoChangeActiveCreature(object param)
//        {
//            if (ActiveCreatures.Count <= 0) { return; }
//            CreatureModel activeCreature = ActiveCreatures.FirstOrDefault(crt => crt.IsActive);
//            CreatureModel firstCreature = ActiveCreatures.First();
//            CreatureModel lastCreature = ActiveCreatures.Last();
//            if (activeCreature == null) { param = "Reset"; }
//            string action = param.ToString();
//            switch (action)
//            {
//                case "Next":
//                    bool foundNext = false;
//                    int indexOfNext = -1;
//                    do
//                    {
//                        if (indexOfNext == -1)
//                        {
//                            indexOfNext = (activeCreature == lastCreature) ? 0 : ActiveCreatures.IndexOf(activeCreature) + 1;
//                        }
//                        if (indexOfNext >= ActiveCreatures.Count()) { indexOfNext = 0; }
//                        if (indexOfNext == 0) { EncounterRound++; }
//                        if (ActiveCreatures[indexOfNext] == activeCreature) { return; } // if it makes a full round and finds nothing
//                        if ((ActiveCreatures[indexOfNext].IsOoc || ActiveCreatures[indexOfNext].CurrentHitPoints <= 0) && ActiveCreatures[indexOfNext].IsPlayer == false) { indexOfNext++; }
//                        else { ActiveCreatures[indexOfNext].IsActive = true; foundNext = true; }
                        
//                    }
//                    while (foundNext == false);                  
//                    break;
//                case "Previous":
//                    bool foundPrev = false;
//                    int indexOfPrevious = -1;
//                    do
//                    {
//                        if (indexOfPrevious == -1)
//                        {
//                            indexOfPrevious = (activeCreature == firstCreature) ? ActiveCreatures.IndexOf(lastCreature) : ActiveCreatures.IndexOf(activeCreature) - 1;
//                        }
//                        if (indexOfPrevious == (ActiveCreatures.IndexOf(lastCreature))) { EncounterRound--; }
//                        if (EncounterRound == 0) { EncounterRound = 1; return; }
//                        if (ActiveCreatures[indexOfPrevious] == activeCreature) { return; } // if it makes a full round and finds nothing
//                        if ((ActiveCreatures[indexOfPrevious].IsOoc || ActiveCreatures[indexOfPrevious].CurrentHitPoints <= 0) && ActiveCreatures[indexOfPrevious].IsPlayer == false) { indexOfPrevious--; }
//                        else { ActiveCreatures[indexOfPrevious].IsActive = true; foundPrev = true; }
//                    }
//                    while (foundPrev == false);
//                    break;
//                case "Reset":
//                    CreatureModel resetCreature = ActiveCreatures.FirstOrDefault(crt => (crt.IsOoc == false && crt.CurrentHitPoints > 0) || crt.IsPlayer);
//                    if (resetCreature == null) { UpdateActiveCreature(); return; }
//                    else { resetCreature.IsActive = true; }
//                    EncounterRound = 1;
//                    UpdateActiveCreature();
//                    break;
//                default:
//                    break;
//            }
//        }
//        #endregion
//        #region RollFallDamage
//        private RelayCommand _RollFallDamage;
//        public ICommand RollFallDamage
//        {
//            get
//            {
//                if (_RollFallDamage == null)
//                {
//                    _RollFallDamage = new RelayCommand(param => DoRollFallDamage());
//                }
//                return _RollFallDamage;
//            }
//        }
//        private void DoRollFallDamage()
//        {
//            int rolls = FallDistance switch
//            {
//                int n when (n < 10) => 0,
//                int n when (n >= 10 && n <= 19) => 1,
//                int n when (n >= 20 && n <= 29) => 2,
//                int n when (n >= 30 && n <= 39) => 3,
//                int n when (n >= 40 && n <= 49) => 4,
//                int n when (n >= 50 && n <= 59) => 5,
//                int n when (n >= 60 && n <= 69) => 6,
//                int n when (n >= 70 && n <= 79) => 7,
//                int n when (n >= 80 && n <= 89) => 8,
//                int n when (n >= 90 && n <= 99) => 9,
//                int n when (n >= 100 && n <= 109) => 10,
//                int n when (n >= 110 && n <= 119) => 11,
//                int n when (n >= 120 && n <= 129) => 12,
//                int n when (n >= 130 && n <= 139) => 13,
//                int n when (n >= 140 && n <= 149) => 14,
//                int n when (n >= 150 && n <= 159) => 15,
//                int n when (n >= 160 && n <= 169) => 16,
//                int n when (n >= 170 && n <= 179) => 17,
//                int n when (n >= 180 && n <= 189) => 18,
//                int n when (n >= 190 && n <= 199) => 19,
//                _ => 20
//            };
//            if (rolls == 0) { new NotificationDialog("Insufficient height for fall damage.").ShowDialog(); return; }
//            HelperMethods.PlaySystemAudio(Configuration.SystemAudio_DiceRoll);
//            int result = 0;
//            string diceRolls = "\nRoll: [";
//            for (int i = 0; i < rolls; i++)
//            {
//                int roll = Configuration.RNG.Next(1, 7);
//                if (i > 0) { diceRolls += " + "; }
//                diceRolls += roll;
//                result += roll;
//            }
//            diceRolls += "]";
//            string message = "Creature falls " + FallDistance + " feet and takes " + result + " bludgeoning damage.";
//            if (Configuration.MainModelRef.SettingsView.ShowDiceRolls)
//            {
//                message += diceRolls;
//            }
//            HelperMethods.AddToEncounterLog(message);
//        }
//        #endregion

//        // Public Methods
//        public void LoadTrackerEncounter(string filePath)
//        {
//            XmlMethods.XmlToList(filePath, out List<CreatureModel> encounterCreatures, out Dictionary<string, string> data);
//            ActiveCreatures = new ObservableCollection<CreatureModel>(encounterCreatures);
//            EncounterTime = data.ContainsKey("Time") ? Convert.ToDateTime(data["Time"]) : Configuration.StartDateTime;
//            LastRestDate = data.ContainsKey("LastRestDate") ? data["LastRestDate"] : "";
//            LastWeatherChange = data.ContainsKey("LastWeatherChange") ? Convert.ToDateTime(data["LastWeatherChange"]) : Configuration.StartDateTime;
//            WeatherIntensity = data.ContainsKey("WeatherIntensity") ? Convert.ToInt32(data["WeatherIntensity"]) : 30;
//            Notes = data.ContainsKey("Notes") ? data["Notes"] : "";
//            EncounterRound = data.ContainsKey("EncounterRound") ? Convert.ToInt32(data["EncounterRound"]) : 1;
//            DoChangeTime(0);
//            SetWeather();
//            foreach (CreatureModel creature in ActiveCreatures)
//            {
//                if (creature.DisplayName == "" || creature.DisplayName == null)
//                {
//                    creature.DisplayName = creature.Name + " " + creature.TrackerIndicator;
//                }
//                creature.UpdateStatus();
//                creature.SetFormattedTexts();
//                creature.UpdateToNewSpellSystem();
//                creature.ConnectSpellLinks();
//                creature.UpdateToNewLootSystem();
//                creature.ConnectItemLinks();
//            }
//        }
//        public void SaveTrackerEncounter(string filePath)
//        {
//            XDocument playerDocument = new XDocument();
//            playerDocument.Add(XmlMethods.ListToXml(ActiveCreatures.ToList()));
//            playerDocument.Root.Add(new XAttribute("Time", EncounterTime.ToString()));
//            playerDocument.Root.Add(new XAttribute("LastRestDate", LastRestDate));
//            playerDocument.Root.Add(new XAttribute("LastWeatherChange", LastWeatherChange.ToString()));
//            playerDocument.Root.Add(new XAttribute("WeatherIntensity", WeatherIntensity.ToString()));
//            playerDocument.Root.Add(new XAttribute("Notes", Notes));
//            playerDocument.Root.Add(new XAttribute("EncounterRound", EncounterRound.ToString()));
//            playerDocument.Save(filePath);
//        }
//        public void SortCombatants()
//        {
//            ActiveCreatures = new ObservableCollection<CreatureModel>(ActiveCreatures.OrderBy(item => (item.CurrentHitPoints <= 0 && item.IsPlayer == false)).ThenBy(item => item.IsOoc).ThenByDescending(item => item.Initiative).ToList());
//        }
//        public void UpdateActiveCreature()
//        {
//            ActiveCreature = ActiveCreatures.FirstOrDefault(crt => crt.IsActive);
//        }

//        // Private Methods
//        private void UpdateWeather()
//        {
//            int result = Configuration.RNG.Next(-20, 20);
//            WeatherIntensity += Convert.ToInt32((50 - WeatherIntensity) * ((WeatherIntensity > 50) ? 0.15 : 0.38)); // Adjust weather intensity towards center
//            WeatherIntensity += result;
//            if (WeatherIntensity > 100) { WeatherIntensity = 100; }
//            if (WeatherIntensity < 1) { WeatherIntensity = 1; }
//            SetWeather();
//        }
//        private void SetWeather()
//        {
//            string oldWeather = CurrentWeather;
//            if (WeatherIntensity >= 1 && WeatherIntensity <= 34)
//            {
//                CurrentWeather = "Clear";
//                WeatherDescription = "No effects.";
//            }
//            if (WeatherIntensity >= 35 && WeatherIntensity <= 49)
//            {
//                CurrentWeather = "Partly Cloudy";
//                WeatherDescription = "No effects.";
//            }
//            if (WeatherIntensity >= 50 && WeatherIntensity <= 64)
//            {
//                CurrentWeather = "Overcast";
//                WeatherDescription = "Creatures no longer affected by Sunlight Sensitivity.";
//            }
//            if (WeatherIntensity >= 65 && WeatherIntensity <= 76)
//            {
//                CurrentWeather = "Light Rain";
//                WeatherDescription = "Creatures no longer affected by Sunlight Sensitivity.\n-1 to Passive Perception outdoors.\nUnable to rest outdoors.";
//            }
//            if (WeatherIntensity >= 77 && WeatherIntensity <= 88)
//            {
//                CurrentWeather = "Moderate Rain";
//                WeatherDescription = "Creatures no longer affected by Sunlight Sensitivity.\n-2 to Passive Perception outdoors.\nUnable to rest outdoors.";
//            }
//            if (WeatherIntensity >= 89)
//            {
//                CurrentWeather = "Heavy Storm";
//                WeatherDescription = "Creatures no longer affected by Sunlight Sensitivity.\n-5 to Passive Perception outdoors.\nUnable to rest outdoors.\nEvery hour without shelter raises exhaustion by 1.";
//            }
//            if (CurrentWeather != oldWeather) { HelperMethods.AddToEncounterLog("Weather has changed to " + CurrentWeather + "."); }
//        }

//    }
//}
