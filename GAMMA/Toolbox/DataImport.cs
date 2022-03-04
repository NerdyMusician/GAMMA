using GAMMA.Models;
using GAMMA.Models.WebAutomation;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GAMMA.Toolbox
{
    public static class DataImport
    {
        // Core
        public static void ImportData_Sourcebooks(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<Sourcebook> importedSourcebooks);
            XmlMethods.XmlToList(Configuration.SourcebookDataFilePath, out List<Sourcebook> currentSourcebooks);
            List<Sourcebook> combinedSourcebookList = new();
            message = "Sourcebooks Imported:";

            List<string> sourcebookNames = new();
            foreach (Sourcebook sourcebook in currentSourcebooks)
            {
                if (sourcebookNames.Contains(sourcebook.Name) == false) { sourcebookNames.Add(sourcebook.Name); }
            }
            foreach (Sourcebook sourcebook in importedSourcebooks)
            {
                if (sourcebookNames.Contains(sourcebook.Name) == false) { sourcebookNames.Add(sourcebook.Name); }
            }

            foreach (string name in sourcebookNames)
            {
                Sourcebook currentSourcebook = currentSourcebooks.FirstOrDefault(sb => sb.Name == name);
                Sourcebook importedSourcebook = importedSourcebooks.FirstOrDefault(sb => sb.Name == name);

                if (currentSourcebook == null) { combinedSourcebookList.Add(importedSourcebook); message += "\n" + name; continue; }
                if (importedSourcebook == null) { combinedSourcebookList.Add(currentSourcebook); continue; }
                if (currentSourcebook.IsValidated == false && importedSourcebook.IsValidated == true) { combinedSourcebookList.Add(importedSourcebook); message += "\n" + name; continue; }
                combinedSourcebookList.Add(currentSourcebook);
            }

            Configuration.MainModelRef.ToolsView.Sourcebooks = new(combinedSourcebookList.OrderBy(sb => sb.Name));

            if (message == "Sourcebooks Imported:") { message = "No sourcebooks imported."; }

            return;

        }
        public static void ImportData_NoteTypes(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<NoteType> importedNoteTypes);
            XmlMethods.XmlToList(Configuration.NoteTypeDataFilePath, out List<NoteType> currentNoteTypes);
            List<NoteType> combinedNoteTypeList = new();
            message = "Note Types Imported:";

            List<string> noteTypeNames = new();
            foreach (NoteType noteType in currentNoteTypes)
            {
                if (noteTypeNames.Contains(noteType.Name) == false) { noteTypeNames.Add(noteType.Name); }
            }
            foreach (NoteType noteType in importedNoteTypes)
            {
                if (noteTypeNames.Contains(noteType.Name) == false) { noteTypeNames.Add(noteType.Name); }
            }

            foreach (string name in noteTypeNames)
            {
                NoteType currentNoteType = currentNoteTypes.FirstOrDefault(sb => sb.Name == name);
                NoteType importedNoteType = importedNoteTypes.FirstOrDefault(sb => sb.Name == name);

                if (currentNoteType == null) { combinedNoteTypeList.Add(importedNoteType); message += "\n" + name; continue; }
                if (importedNoteType == null) { combinedNoteTypeList.Add(currentNoteType); continue; }
                combinedNoteTypeList.Add(currentNoteType);
            }

            Configuration.MainModelRef.ToolsView.NoteTypes = new(combinedNoteTypeList.OrderBy(sb => sb.Name));

            if (message == "Note Types Imported:") { message = "No note types imported."; }

            return;

        }
        public static void ImportData_Items(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<ItemModel> importedItems);
            XmlMethods.XmlToList(Configuration.ItemDataFilePath, out List<ItemModel> currentItems);
            List<ItemModel> combinedItemList = new();
            message = "Items Imported:";

            List<string> itemNames = new();
            foreach (ItemModel item in currentItems)
            {
                if (itemNames.Contains(item.Name) == false) { itemNames.Add(item.Name); }
            }
            foreach (ItemModel item in importedItems)
            {
                if (itemNames.Contains(item.Name) == false) { itemNames.Add(item.Name); }
            }

            foreach (string name in itemNames)
            {
                ItemModel currentItem = currentItems.FirstOrDefault(item => item.Name == name);
                ItemModel importedItem = importedItems.FirstOrDefault(item => item.Name == name);

                if (currentItem == null) { combinedItemList.Add(importedItem); message += "\n" + name; continue; }
                if (importedItem == null) { combinedItemList.Add(currentItem); continue; }
                if (currentItem.IsValidated == false && importedItem.IsValidated == true) { combinedItemList.Add(importedItem); message += "\n" + name; continue; }
                combinedItemList.Add(currentItem);
            }

            Configuration.MainModelRef.ItemBuilderView.AllItems = new ObservableCollection<ItemModel>(combinedItemList.OrderBy(item => item.Name));
            Configuration.MainModelRef.ItemBuilderView.FilteredItems = new ObservableCollection<ItemModel>(Configuration.MainModelRef.ItemBuilderView.AllItems.ToList());

            Configuration.MainModelRef.ItemBuilderView.UpdateItemFilter();

            if (message == "Items Imported:") { message = "No items imported."; }

            return;

        }
        public static void ImportData_Spells(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<SpellModel> importedSpells);
            XmlMethods.XmlToList(Configuration.SpellDataFilePath, out List<SpellModel> currentSpells);
            List<SpellModel> combinedSpellList = new();
            message = "Spells Imported:";

            List<string> spellNames = new();
            foreach (SpellModel spell in currentSpells)
            {
                if (spellNames.Contains(spell.Name) == false) { spellNames.Add(spell.Name); }
            }
            foreach (SpellModel spell in importedSpells)
            {
                if (spellNames.Contains(spell.Name) == false) { spellNames.Add(spell.Name); }
            }

            foreach (string name in spellNames)
            {
                SpellModel currentSpell = currentSpells.FirstOrDefault(spell => spell.Name == name);
                SpellModel importedSpell = importedSpells.FirstOrDefault(spell => spell.Name == name);

                if (currentSpell == null) { combinedSpellList.Add(importedSpell); message += "\n" + name; continue; }
                if (importedSpell == null) { combinedSpellList.Add(currentSpell); continue; }
                if (currentSpell.IsValidated == false && importedSpell.IsValidated == true) { combinedSpellList.Add(importedSpell); message += "\n" + name; continue; }
                combinedSpellList.Add(currentSpell);
            }

            Configuration.MainModelRef.SpellBuilderView.AllSpells = new ObservableCollection<SpellModel>(combinedSpellList.OrderBy(spell => spell.Name));
            Configuration.MainModelRef.SpellBuilderView.FilteredSpells = new ObservableCollection<SpellModel>(Configuration.MainModelRef.SpellBuilderView.AllSpells.ToList());

            if (message == "Spells Imported:") { message = "No spells imported."; }

            return;

        }
        public static void ImportData_Creatures(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<CreatureModel> importedCreatures, out _);
            XmlMethods.XmlToList(Configuration.CreatureDataFilePath, out List<CreatureModel> currentCreatures, out _);
            List<CreatureModel> combinedCreatureList = new();
            message = "Creatures Imported:";

            List<string> creatureNames = new();
            foreach (CreatureModel creature in currentCreatures)
            {
                if (creatureNames.Contains(creature.Name) == false) { creatureNames.Add(creature.Name); }
            }
            foreach (CreatureModel creature in importedCreatures)
            {
                if (creatureNames.Contains(creature.Name) == false) { creatureNames.Add(creature.Name); }
            }

            foreach (string name in creatureNames)
            {
                CreatureModel currentCreature = currentCreatures.FirstOrDefault(creature => creature.Name == name);
                CreatureModel importedCreature = importedCreatures.FirstOrDefault(creature => creature.Name == name);

                if (currentCreature == null) { combinedCreatureList.Add(importedCreature); message += "\n" + name; continue; }
                if (importedCreature == null) { combinedCreatureList.Add(currentCreature); continue; }
                if (currentCreature.IsValidated == false && importedCreature.IsValidated == true) { combinedCreatureList.Add(importedCreature); message += "\n" + name; continue; }
                combinedCreatureList.Add(currentCreature);
            }

            Configuration.MainModelRef.CreatureBuilderView.AllCreatures = new ObservableCollection<CreatureModel>(combinedCreatureList.OrderBy(creature => creature.Name));
            Configuration.MainModelRef.CreatureBuilderView.FilteredCreatures = new ObservableCollection<CreatureModel>(Configuration.MainModelRef.CreatureBuilderView.AllCreatures.ToList());

            foreach (CreatureModel creature in Configuration.MainModelRef.CreatureBuilderView.AllCreatures)
            {
                creature.ConnectSpellLinks();
                creature.ConnectItemLinks();
            }

            if (message == "Creatures Imported:") { message = "No creatures imported."; }

            return;

        }
        public static void ImportData_Settings(string filepath, out string message)
        {
            XmlMethods.XmlToObject(filepath, out SettingsViewModel sv);

            // Prevents nulling of web sequences importing data from 1.28 to 1.29
            if (sv.StartupWebActions.Count == 0) { sv.StartupWebActions = Configuration.MainModelRef.SettingsView.StartupWebActions; }
            if (sv.OutputWebActions.Count == 0) { sv.OutputWebActions = Configuration.MainModelRef.SettingsView.OutputWebActions; }
            if (sv.SwitchbackWebActions.Count == 0) { sv.SwitchbackWebActions = Configuration.MainModelRef.SettingsView.SwitchbackWebActions; }
            if (string.IsNullOrEmpty(sv.OutputNameSwap)) { sv.OutputNameSwap = Configuration.MainModelRef.SettingsView.OutputNameSwap; }

            Configuration.MainModelRef.SettingsView = sv;
            message = "Settings Imported.";

        }

        // Misc
        public static void ImportData_Shops(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<ShopModel> importedShops);
            XmlMethods.XmlToList(Configuration.ShopDataFilePath, out List<ShopModel> currentShops);
            List<ShopModel> combinedShopList = new();
            message = "Shops Imported:";

            List<string> shops = new();
            foreach (ShopModel shop in currentShops)
            {
                if (shops.Contains(shop.Name) == false) { shops.Add(shop.Name); }
            }
            foreach (ShopModel shop in importedShops)
            {
                if (shops.Contains(shop.Name) == false) { shops.Add(shop.Name); }
            }

            foreach (string name in shops)
            {
                ShopModel currentShop = currentShops.FirstOrDefault(item => item.Name == name);
                ShopModel importedShop = importedShops.FirstOrDefault(item => item.Name == name);

                if (currentShop == null) { combinedShopList.Add(importedShop); message += "\n" + name; continue; }
                if (importedShop == null) { combinedShopList.Add(currentShop); continue; }
                combinedShopList.Add(currentShop);
            }

            Configuration.MainModelRef.ToolsView.Shops = new ObservableCollection<ShopModel>(combinedShopList.OrderBy(box => box.Name));

            if (message == "Shops Imported:") { message = "No shops imported."; }

            return;

        }
        public static void ImportData_LootBoxes(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<LootBoxModel> importedLootBoxes);
            XmlMethods.XmlToList(Configuration.LootBoxDataFilePath, out List<LootBoxModel> currentLootBoxes);
            List<LootBoxModel> combinedLootBoxList = new();
            message = "LootBoxes Imported:";

            List<string> boxNames = new();
            foreach (LootBoxModel box in currentLootBoxes)
            {
                if (boxNames.Contains(box.Name) == false) { boxNames.Add(box.Name); }
            }
            foreach (LootBoxModel box in importedLootBoxes)
            {
                if (boxNames.Contains(box.Name) == false) { boxNames.Add(box.Name); }
            }

            foreach (string name in boxNames)
            {
                LootBoxModel currentLootBox = currentLootBoxes.FirstOrDefault(item => item.Name == name);
                LootBoxModel importedLootBox = importedLootBoxes.FirstOrDefault(item => item.Name == name);

                if (currentLootBox == null) { combinedLootBoxList.Add(importedLootBox); message += "\n" + name; continue; }
                if (importedLootBox == null) { combinedLootBoxList.Add(currentLootBox); continue; }
                combinedLootBoxList.Add(currentLootBox);
            }

            Configuration.MainModelRef.ToolsView.LootBoxes = new ObservableCollection<LootBoxModel>(combinedLootBoxList.OrderBy(box => box.Name));

            if (message == "LootBoxes Imported:") { message = "No boxes imported."; }

            return;

        }
        public static void ImportData_RollTables(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<RollTableModel> importedRollTables);
            XmlMethods.XmlToList(Configuration.RollTableDataFilePath, out List<RollTableModel> currentRollTables);
            List<RollTableModel> combinedRollTableList = new();
            message = "RollTables Imported:";

            List<string> rollTableNames = new();
            foreach (RollTableModel table in currentRollTables)
            {
                if (rollTableNames.Contains(table.Name) == false) { rollTableNames.Add(table.Name); }
            }
            foreach (RollTableModel table in importedRollTables)
            {
                if (rollTableNames.Contains(table.Name) == false) { rollTableNames.Add(table.Name); }
            }

            foreach (string name in rollTableNames)
            {
                RollTableModel currentRollTable = currentRollTables.FirstOrDefault(item => item.Name == name);
                RollTableModel importedRollTable = importedRollTables.FirstOrDefault(item => item.Name == name);

                if (currentRollTable == null) { combinedRollTableList.Add(importedRollTable); message += "\n" + name; continue; }
                if (importedRollTable == null) { combinedRollTableList.Add(currentRollTable); continue; }
                combinedRollTableList.Add(currentRollTable);
            }

            Configuration.MainModelRef.ToolsView.RollTables = new ObservableCollection<RollTableModel>(combinedRollTableList.OrderBy(box => box.Name));

            if (message == "RollTables Imported:") { message = "No roll tables imported."; }

            return;

        }
        public static void ImportData_Languages(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<LanguageModel> importedLanguages);
            XmlMethods.XmlToList(Configuration.LanguageDataFilePath, out List<LanguageModel> currentLanguages);
            List<LanguageModel> combinedLanguageList = new();
            message = "Languages Imported:";

            List<string> languages = new();
            foreach (LanguageModel language in currentLanguages)
            {
                if (languages.Contains(language.Name) == false) { languages.Add(language.Name); }
            }
            foreach (LanguageModel language in importedLanguages)
            {
                if (languages.Contains(language.Name) == false) { languages.Add(language.Name); }
            }

            foreach (string name in languages)
            {
                LanguageModel currentLanguage = currentLanguages.FirstOrDefault(item => item.Name == name);
                LanguageModel importedLanguage = importedLanguages.FirstOrDefault(item => item.Name == name);

                if (currentLanguage == null) { combinedLanguageList.Add(importedLanguage); message += "\n" + name; continue; }
                if (importedLanguage == null) { combinedLanguageList.Add(currentLanguage); continue; }
                if (currentLanguage.IsValidated == false && importedLanguage.IsValidated == true) { combinedLanguageList.Add(importedLanguage); message += "\n" + name; continue; }
                combinedLanguageList.Add(currentLanguage);
            }

            Configuration.MainModelRef.ToolsView.Languages = new ObservableCollection<LanguageModel>(combinedLanguageList.OrderBy(box => box.Name));

            if (message == "Languages Imported:") { message = "No languages imported."; }

            return;

        }
        public static void ImportData_Weather(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<Weather> importedWeathers);
            XmlMethods.XmlToList(Configuration.WeatherDataFilePath, out List<Weather> currentWeathers);
            List<Weather> combinedWeatherList = new();
            message = "Weathers Imported:";

            List<string> weathers = new();
            foreach (Weather weather in currentWeathers)
            {
                if (weathers.Contains(weather.Name) == false) { weathers.Add(weather.Name); }
            }
            foreach (Weather weather in importedWeathers)
            {
                if (weathers.Contains(weather.Name) == false) { weathers.Add(weather.Name); }
            }

            foreach (string name in weathers)
            {
                Weather currentWeather = currentWeathers.FirstOrDefault(item => item.Name == name);
                Weather importedWeather = importedWeathers.FirstOrDefault(item => item.Name == name);

                if (currentWeather == null) { combinedWeatherList.Add(importedWeather); message += "\n" + name; continue; }
                if (importedWeather == null) { combinedWeatherList.Add(currentWeather); continue; }
                if (currentWeather.IsValidated == false && importedWeather.IsValidated == true) { combinedWeatherList.Add(importedWeather); message += "\n" + name; continue; }
                combinedWeatherList.Add(currentWeather);
            }

            Configuration.MainModelRef.ToolsView.Weathers = new ObservableCollection<Weather>(combinedWeatherList.OrderBy(box => box.Name));

            if (message == "Weathers Imported:") { message = "No weathers imported."; }

            return;

        }
        public static void ImportData_Calendars(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<GameCalendar> importedCalendars);
            XmlMethods.XmlToList(Configuration.CalendarDataFilePath, out List<GameCalendar> currentCalendars);
            List<GameCalendar> combinedCalendarList = new();
            message = "Calendars Imported:";

            List<string> calendars = new();
            foreach (GameCalendar calendar in currentCalendars)
            {
                if (calendars.Contains(calendar.Name) == false) { calendars.Add(calendar.Name); }
            }
            foreach (GameCalendar calendar in importedCalendars)
            {
                if (calendars.Contains(calendar.Name) == false) { calendars.Add(calendar.Name); }
            }

            foreach (string name in calendars)
            {
                GameCalendar currentCalendar = currentCalendars.FirstOrDefault(item => item.Name == name);
                GameCalendar importedCalendar = importedCalendars.FirstOrDefault(item => item.Name == name);

                if (currentCalendar == null) { combinedCalendarList.Add(importedCalendar); message += "\n" + name; continue; }
                if (importedCalendar == null) { combinedCalendarList.Add(currentCalendar); continue; }
                if (currentCalendar.IsValidated == false && importedCalendar.IsValidated == true) { combinedCalendarList.Add(importedCalendar); message += "\n" + name; continue; }
                combinedCalendarList.Add(currentCalendar);
            }

            Configuration.MainModelRef.ToolsView.Calendars = new ObservableCollection<GameCalendar>(combinedCalendarList.OrderBy(box => box.Name));

            if (message == "Calendars Imported:") { message = "No calendars imported."; }

            return;

        }

        // Player
        public static void ImportData_PlayerClasses(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<PlayerClassModel> importedPlayerClasses);
            XmlMethods.XmlToList(Configuration.PlayerClassDataFilePath, out List<PlayerClassModel> currentPlayerClasses);
            List<PlayerClassModel> combinedPlayerClassList = new();
            message = "Player Classes Imported:";

            List<string> classNames = new();
            foreach (PlayerClassModel pClass in currentPlayerClasses)
            {
                if (classNames.Contains(pClass.Name) == false) { classNames.Add(pClass.Name); }
            }
            foreach (PlayerClassModel pClass in importedPlayerClasses)
            {
                if (classNames.Contains(pClass.Name) == false) { classNames.Add(pClass.Name); }
            }

            foreach (string name in classNames)
            {
                PlayerClassModel currentPlayerClass = currentPlayerClasses.FirstOrDefault(item => item.Name == name);
                PlayerClassModel importedPlayerClass = importedPlayerClasses.FirstOrDefault(item => item.Name == name);

                if (currentPlayerClass == null) { combinedPlayerClassList.Add(importedPlayerClass); message += "\n" + name; continue; }
                if (importedPlayerClass == null) { combinedPlayerClassList.Add(currentPlayerClass); continue; }
                if (currentPlayerClass.IsValidated == false && importedPlayerClass.IsValidated == true) { combinedPlayerClassList.Add(importedPlayerClass); message += "\n" + name; continue; }
                combinedPlayerClassList.Add(currentPlayerClass);
            }

            Configuration.MainModelRef.ToolsView.PlayerClasses = new ObservableCollection<PlayerClassModel>(combinedPlayerClassList.OrderBy(box => box.Name));

            if (message == "Player Classes Imported:") { message = "No player classes imported."; }

            Configuration.MainModelRef.ToolsView.DataCleanup_SpellsKnownPerLevel();

            return;

        }
        public static void ImportData_PlayerSubclasses(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<PlayerSubclassModel> importedPlayerSubclasses);
            XmlMethods.XmlToList(Configuration.PlayerSubclassDataFilePath, out List<PlayerSubclassModel> currentPlayerSubclasses);
            List<PlayerSubclassModel> combinedPlayerSubclassList = new();
            message = "Player Subclasses Imported:";

            List<string> classNames = new();
            foreach (PlayerSubclassModel pClass in currentPlayerSubclasses)
            {
                if (classNames.Contains(pClass.Name) == false) { classNames.Add(pClass.Name); }
            }
            foreach (PlayerSubclassModel pClass in importedPlayerSubclasses)
            {
                if (classNames.Contains(pClass.Name) == false) { classNames.Add(pClass.Name); }
            }

            foreach (string name in classNames)
            {
                PlayerSubclassModel currentPlayerClass = currentPlayerSubclasses.FirstOrDefault(item => item.Name == name);
                PlayerSubclassModel importedPlayerClass = importedPlayerSubclasses.FirstOrDefault(item => item.Name == name);

                if (currentPlayerClass == null) { combinedPlayerSubclassList.Add(importedPlayerClass); message += "\n" + name; continue; }
                if (importedPlayerClass == null) { combinedPlayerSubclassList.Add(currentPlayerClass); continue; }
                if (currentPlayerClass.IsValidated == false && importedPlayerClass.IsValidated == true) { combinedPlayerSubclassList.Add(importedPlayerClass); message += "\n" + name; continue; }
                combinedPlayerSubclassList.Add(currentPlayerClass);
            }

            Configuration.MainModelRef.ToolsView.PlayerSubclasses = new ObservableCollection<PlayerSubclassModel>(combinedPlayerSubclassList.OrderBy(box => box.Name));

            if (message == "Player Subclasses Imported:") { message = "No player subclasses imported."; }

            return;

        }
        public static void ImportData_PlayerRaces(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<PlayerRaceModel> importedPlayerRaces);
            XmlMethods.XmlToList(Configuration.PlayerRaceDataFilePath, out List<PlayerRaceModel> currentPlayerRaces);
            List<PlayerRaceModel> combinedPlayerRaceList = new();
            message = "Player Races Imported:";

            List<string> raceNames = new();
            foreach (PlayerRaceModel race in currentPlayerRaces)
            {
                if (raceNames.Contains(race.Name) == false) { raceNames.Add(race.Name); }
            }
            foreach (PlayerRaceModel race in importedPlayerRaces)
            {
                if (raceNames.Contains(race.Name) == false) { raceNames.Add(race.Name); }
            }

            foreach (string name in raceNames)
            {
                PlayerRaceModel currentPlayerRace = currentPlayerRaces.FirstOrDefault(item => item.Name == name);
                PlayerRaceModel importedPlayerRace = importedPlayerRaces.FirstOrDefault(item => item.Name == name);

                if (currentPlayerRace == null) { combinedPlayerRaceList.Add(importedPlayerRace); message += "\n" + name; continue; }
                if (importedPlayerRace == null) { combinedPlayerRaceList.Add(currentPlayerRace); continue; }
                if (currentPlayerRace.IsValidated == false && importedPlayerRace.IsValidated == true) { combinedPlayerRaceList.Add(importedPlayerRace); message += "\n" + name; continue; }
                combinedPlayerRaceList.Add(currentPlayerRace);
            }

            Configuration.MainModelRef.ToolsView.PlayerRaces = new ObservableCollection<PlayerRaceModel>(combinedPlayerRaceList.OrderBy(box => box.Name));

            if (message == "Player Races Imported:") { message = "No player races imported."; }

            return;

        }
        public static void ImportData_PlayerSubraces(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<PlayerSubraceModel> importedPlayerSubraces);
            XmlMethods.XmlToList(Configuration.PlayerSubraceDataFilePath, out List<PlayerSubraceModel> currentPlayerSubraces);
            List<PlayerSubraceModel> combinedPlayerSubraceList = new();
            message = "Player Subraces Imported:";

            List<string> subraceNames = new();
            foreach (PlayerSubraceModel race in currentPlayerSubraces)
            {
                if (subraceNames.Contains(race.Name) == false) { subraceNames.Add(race.Name); }
            }
            foreach (PlayerSubraceModel race in importedPlayerSubraces)
            {
                if (subraceNames.Contains(race.Name) == false) { subraceNames.Add(race.Name); }
            }

            foreach (string name in subraceNames)
            {
                PlayerSubraceModel currentPlayerSubrace = currentPlayerSubraces.FirstOrDefault(item => item.Name == name);
                PlayerSubraceModel importedPlayerSubrace = importedPlayerSubraces.FirstOrDefault(item => item.Name == name);

                if (currentPlayerSubrace == null) { combinedPlayerSubraceList.Add(importedPlayerSubrace); message += "\n" + name; continue; }
                if (importedPlayerSubrace == null) { combinedPlayerSubraceList.Add(currentPlayerSubrace); continue; }
                if (currentPlayerSubrace.IsValidated == false && importedPlayerSubrace.IsValidated == true) { combinedPlayerSubraceList.Add(importedPlayerSubrace); message += "\n" + name; continue; }
                combinedPlayerSubraceList.Add(currentPlayerSubrace);
            }

            Configuration.MainModelRef.ToolsView.PlayerSubraces = new ObservableCollection<PlayerSubraceModel>(combinedPlayerSubraceList.OrderBy(box => box.Name));

            if (message == "Player Subraces Imported:") { message = "No player subraces imported."; }

            return;

        }
        public static void ImportData_PlayerBackgrounds(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<PlayerBackgroundModel> importedPlayerBackgrounds);
            XmlMethods.XmlToList(Configuration.PlayerBackgroundDataFilePath, out List<PlayerBackgroundModel> currentPlayerBackgrounds);
            List<PlayerBackgroundModel> combinedPlayerBackgroundList = new();
            message = "Player Backgrounds Imported:";

            List<string> subraceNames = new();
            foreach (PlayerBackgroundModel race in currentPlayerBackgrounds)
            {
                if (subraceNames.Contains(race.Name) == false) { subraceNames.Add(race.Name); }
            }
            foreach (PlayerBackgroundModel race in importedPlayerBackgrounds)
            {
                if (subraceNames.Contains(race.Name) == false) { subraceNames.Add(race.Name); }
            }

            foreach (string name in subraceNames)
            {
                PlayerBackgroundModel currentPlayerBackground = currentPlayerBackgrounds.FirstOrDefault(item => item.Name == name);
                PlayerBackgroundModel importedPlayerBackground = importedPlayerBackgrounds.FirstOrDefault(item => item.Name == name);

                if (currentPlayerBackground == null) { combinedPlayerBackgroundList.Add(importedPlayerBackground); message += "\n" + name; continue; }
                if (importedPlayerBackground == null) { combinedPlayerBackgroundList.Add(currentPlayerBackground); continue; }
                if (currentPlayerBackground.IsValidated == false && importedPlayerBackground.IsValidated == true) { combinedPlayerBackgroundList.Add(importedPlayerBackground); message += "\n" + name; continue; }
                combinedPlayerBackgroundList.Add(currentPlayerBackground);
            }

            Configuration.MainModelRef.ToolsView.PlayerBackgrounds = new ObservableCollection<PlayerBackgroundModel>(combinedPlayerBackgroundList.OrderBy(box => box.Name));

            if (message == "Player Backgrounds Imported:") { message = "No player backgrounds imported."; }

            return;

        }
        public static void ImportData_PlayerFeats(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<PlayerFeatModel> importedPlayerFeats);
            XmlMethods.XmlToList(Configuration.PlayerFeatDataFilePath, out List<PlayerFeatModel> currentPlayerFeats);
            List<PlayerFeatModel> combinedPlayerFeatList = new();
            message = "Player Feats Imported:";

            List<string> subraceNames = new();
            foreach (PlayerFeatModel race in currentPlayerFeats)
            {
                if (subraceNames.Contains(race.Name) == false) { subraceNames.Add(race.Name); }
            }
            foreach (PlayerFeatModel race in importedPlayerFeats)
            {
                if (subraceNames.Contains(race.Name) == false) { subraceNames.Add(race.Name); }
            }

            foreach (string name in subraceNames)
            {
                PlayerFeatModel currentPlayerFeat = currentPlayerFeats.FirstOrDefault(item => item.Name == name);
                PlayerFeatModel importedPlayerFeat = importedPlayerFeats.FirstOrDefault(item => item.Name == name);

                if (currentPlayerFeat == null) { combinedPlayerFeatList.Add(importedPlayerFeat); message += "\n" + name; continue; }
                if (importedPlayerFeat == null) { combinedPlayerFeatList.Add(currentPlayerFeat); continue; }
                if (currentPlayerFeat.IsValidated == false && importedPlayerFeat.IsValidated == true) { combinedPlayerFeatList.Add(importedPlayerFeat); message += "\n" + name; continue; }
                combinedPlayerFeatList.Add(currentPlayerFeat);
            }

            Configuration.MainModelRef.ToolsView.PlayerFeats = new ObservableCollection<PlayerFeatModel>(combinedPlayerFeatList.OrderBy(box => box.Name));

            if (message == "Player Feats Imported:") { message = "No player backgrounds imported."; }

            return;

        }
        public static void ImportData_Invocations(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<EldritchInvocation> importedEldritchInvocations);
            XmlMethods.XmlToList(Configuration.EldritchInvocationsDataFilePath, out List<EldritchInvocation> currentEldritchInvocations);
            List<EldritchInvocation> combinedEldritchInvocationList = new();
            message = "Eldritch Invocations Imported:";

            List<string> eldritchInvocations = new();
            foreach (EldritchInvocation invocation in currentEldritchInvocations)
            {
                if (eldritchInvocations.Contains(invocation.Name) == false) { eldritchInvocations.Add(invocation.Name); }
            }
            foreach (EldritchInvocation invocation in importedEldritchInvocations)
            {
                if (eldritchInvocations.Contains(invocation.Name) == false) { eldritchInvocations.Add(invocation.Name); }
            }

            foreach (string name in eldritchInvocations)
            {
                EldritchInvocation currentEldritchInvocation = currentEldritchInvocations.FirstOrDefault(item => item.Name == name);
                EldritchInvocation importedEldritchInvocation = importedEldritchInvocations.FirstOrDefault(item => item.Name == name);

                if (currentEldritchInvocation == null) { combinedEldritchInvocationList.Add(importedEldritchInvocation); message += "\n" + name; continue; }
                if (importedEldritchInvocation == null) { combinedEldritchInvocationList.Add(currentEldritchInvocation); continue; }
                if (currentEldritchInvocation.IsValidated == false && importedEldritchInvocation.IsValidated == true) { combinedEldritchInvocationList.Add(importedEldritchInvocation); message += "\n" + name; continue; }
                combinedEldritchInvocationList.Add(currentEldritchInvocation);
            }

            Configuration.MainModelRef.ToolsView.EldritchInvocations = new ObservableCollection<EldritchInvocation>(combinedEldritchInvocationList.OrderBy(box => box.Name));

            if (message == "Eldritch Invocations Imported:") { message = "No eldritch invocations imported."; }

            return;

        }
        public static void ImportData_PlayerCharacters(string filepath, out string message)
        {
            Configuration.HasUsedCharacterBuilder = true;
            XmlMethods.XmlToList(filepath, out List<CharacterModel> importedCharacters);
            XmlMethods.XmlToList(Configuration.CharacterDataFilePath, out List<CharacterModel> currentCharacters);
            List<CharacterModel> combinedCharacterList = new();
            message = "Characters Imported:";

            List<string> characterNames = new();
            foreach (CharacterModel character in currentCharacters)
            {
                if (characterNames.Contains(character.Name) == false) { characterNames.Add(character.Name); }
            }
            foreach (CharacterModel character in importedCharacters)
            {
                if (characterNames.Contains(character.Name) == false) { characterNames.Add(character.Name); }
            }

            foreach (string name in characterNames)
            {
                CharacterModel currentCharacter = currentCharacters.FirstOrDefault(character => character.Name == name);
                CharacterModel importedCharacter = importedCharacters.FirstOrDefault(character => character.Name == name);

                if (currentCharacter != null && importedCharacter != null)
                {
                    YesNoDialog dupQuestion = new("Duplicate found for \"" + name + "\"\nOverwrite existing?");
                    dupQuestion.ShowDialog();
                    if (dupQuestion.Answer == true)
                    {
                        combinedCharacterList.Add(importedCharacter); message += "\n" + name + " (overwrite)"; continue;
                    }
                }
                if (currentCharacter == null) { combinedCharacterList.Add(importedCharacter); message += "\n" + name + " (new)"; continue; }
                combinedCharacterList.Add(currentCharacter); continue;
            }

            Configuration.MainModelRef.CharacterBuilderView.Characters = new ObservableCollection<CharacterModel>(combinedCharacterList.OrderBy(character => character.Name));

            if (message == "Characters Imported:") { message = "No characters imported."; }
            else
            {
                if (filepath.Contains("Data") == true)
                {
                    string attachmentsPath = filepath.Remove(filepath.IndexOf("Data"));
                    attachmentsPath += "NoteAttachments";
                    if (Directory.Exists(attachmentsPath))
                    {
                        bool copiedAttachments = false;
                        foreach (string file in Directory.GetFiles(attachmentsPath))
                        {
                            string safeFileName = file.Split('\\').Last();
                            string destinationFileName = Environment.CurrentDirectory + "/NoteAttachments/" + safeFileName;
                            if (File.Exists(destinationFileName) == false)
                            {
                                File.Copy(file, destinationFileName);
                                copiedAttachments = true;
                            }
                        }
                        if (copiedAttachments == true)
                        {
                            message += "\nAttachments copied from " + attachmentsPath;
                        }
                    }
                }
            }

            foreach (CharacterModel character in Configuration.MainModelRef.CharacterBuilderView.Characters)
            {
                character.SetTotalLevel();
                character.SetClassAutoText();
                character.SetSubclassAutoText();
                character.UpdateInventoryStats();
                character.ConnectSpellLinks();
                character.ReinitializeEventHandlers();
                character.UpdateModifiers();
                character.MaxHealth = character.GetCalculatedMaxHitPoints(character.ConstitutionModifier);
                foreach (InventoryModel inventory in character.Inventories)
                {
                    inventory.UpdateFilteredList();
                }
                character.UpdateInventoryItemCategories();
                character.ValidateCharacterCreation();
                character.SetAlterantSuggestedValues();
            }

            Configuration.MainModelRef.CharacterBuilderView.RunNullSpellLinkCheck();

            return;

        }

        // DM
        public static void ImportData_Campaigns(string filepath, out string message)
        {
            XmlMethods.XmlToList(filepath, out List<GameCampaign> importedCampaigns);
            XmlMethods.XmlToList(Configuration.CampaignDataFilePath, out List<GameCampaign> currentCampaigns);
            List<GameCampaign> combinedCampaignList = new();
            message = "Campaigns Imported:";

            List<string> campaignNames = new();
            foreach (GameCampaign campaign in currentCampaigns)
            {
                if (campaignNames.Contains(campaign.Name) == false) { campaignNames.Add(campaign.Name); }
            }
            foreach (GameCampaign campaign in importedCampaigns)
            {
                if (campaignNames.Contains(campaign.Name) == false) { campaignNames.Add(campaign.Name); }
            }

            foreach (string name in campaignNames)
            {
                GameCampaign currentCampaign = currentCampaigns.FirstOrDefault(campaign => campaign.Name == name);
                GameCampaign importedCampaign = importedCampaigns.FirstOrDefault(campaign => campaign.Name == name);

                if (currentCampaign != null && importedCampaign != null)
                {
                    YesNoDialog dupQuestion = new("Duplicate found for \"" + name + "\"\nOverwrite existing?");
                    dupQuestion.ShowDialog();
                    if (dupQuestion.Answer == true)
                    {
                        combinedCampaignList.Add(importedCampaign); message += "\n" + name + " (overwrite)"; continue;
                    }
                }
                if (currentCampaign == null) { combinedCampaignList.Add(importedCampaign); message += "\n" + name + " (new)"; continue; }
                combinedCampaignList.Add(currentCampaign); continue;
            }

            Configuration.MainModelRef.CampaignView.Campaigns = new ObservableCollection<GameCampaign>(combinedCampaignList.OrderBy(campaign => campaign.Name));

            if (message == "Campaigns Imported:") { message = "No campaigns imported."; }
            else
            {
                if (filepath.Contains("Data") == true)
                {
                    string attachmentsPath = filepath.Remove(filepath.IndexOf("Data"));
                    attachmentsPath += "NoteAttachments";
                    if (Directory.Exists(attachmentsPath))
                    {
                        bool copiedAttachments = false;
                        foreach (string file in Directory.GetFiles(attachmentsPath))
                        {
                            string safeFileName = file.Split('\\').Last();
                            string destinationFileName = Environment.CurrentDirectory + "/NoteAttachments/" + safeFileName;
                            if (File.Exists(destinationFileName) == false)
                            {
                                File.Copy(file, destinationFileName);
                                copiedAttachments = true;
                            }
                        }
                        if (copiedAttachments == true)
                        {
                            message += "\nAttachments copied from " + attachmentsPath;
                        }
                    }
                }
            }

            foreach (GameCampaign campaign in Configuration.MainModelRef.CampaignView.Campaigns)
            {
                foreach (CreatureModel combatant in campaign.Combatants)
                {
                    combatant.ConnectSpellLinks();
                    combatant.GetPortraitFilepath();
                    combatant.SetFormattedTexts();
                    combatant.SetHordeStats();
                }
                campaign.SortCombatants();
                campaign.UpdateCalendarAndWeather();
                campaign.UpdateActiveCombatant();
            }

            return;

        }


    }
}
