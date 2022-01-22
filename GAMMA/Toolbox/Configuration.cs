using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GAMMA.Toolbox
{
    public static class Configuration
    {
        public static bool LoadComplete = false;
        public static bool HasUsedCharacterBuilder = false;

        public static readonly List<string> CreatureTypes = new()
        {
            "",
            "D&D Creature",
            "D&D Player"
        };
        public static readonly List<string> CreatureCategories = new()
        {
            "",
            "Aberration",
            "Beast",
            "Celestial",
            "Construct",
            "Dragon",
            "Elemental",
            "Fey",
            "Fiend",
            "Giant",
            "Humanoid",
            "Monstrosity",
            "Ooze",
            "Plant",
            "Undead"
        };
        public static readonly List<string> CreatureSubCategories = new()
        {
            "",
            "Aarakocra",
            "Elf",
            "Goblinoid"
        };
        public static readonly List<string> Sources = new()
        {
            "",
            "Homebrew",
            "Wizards of the Coast"
        };
        public static readonly List<string> Sizes = new()
        {
            "Tiny",
            "Small",
            "Medium",
            "Large",
            "Huge",
            "Gargantuan"
        };
        public static readonly List<string> VolumeSizes = new()
        {
            "Barrel (40 gal)",
            "Keg (5 gal)",
            "Pitcher (1 gal)",
            "Bottle (1.5 pint)",
            "Glass or Flask (1 pint)",
            "Gill (1/4 pint)",
        };
        public static readonly List<string> DrinkVolumes = new()
        {
            "Glass or Flask (1 pint)",
            "Gill (1/4 pint)",
        };
        public static readonly List<string> Alignments = new()
        {
            "Lawful Good",
            "Lawful Neutral",
            "Lawful Evil",
            "Neutral Good",
            "Neutral",
            "Unaligned",
            "Neutral Evil",
            "Chaotic Good",
            "Chaotic Neutral",
            "Chaotic Evil"
        };
        public static readonly List<string> DiceSides = new()
        {
            "20","12","10","8","6","4","1"
        };
        public static readonly List<string> ItemTypes = new()
        {
            "Adventuring Gear",
            "Alcohol",
            "Ammunition",
            "Arcane Focus",
            "Art",
            "Artisan Tool",
            "Book",
            "Clothing",
            "Fish",
            "Firearms Ranged Weapon",
            "Food & Drink",
            "Gaming Set",
            "Gemstone",
            "Heavy Armor",
            "Holy Symbol",
            "Ingredient",
            "Instrument",
            "Jewelry",
            "Key",
            "Light Armor",
            "Magic Weapon",
            "Martial Melee Weapon",
            "Martial Ranged Weapon",
            "Medium Armor",
            "Other",
            "Poison",
            "Potion",
            "Resource",
            "Rune",
            "Scroll",
            "Shield",
            "Simple Melee Weapon",
            "Simple Ranged Weapon",
            "Tool",
            "Treasure",
            "Wondrous Item",
        };
        public static readonly List<string> WeaponItemTypes = new()
        {
            "Firearms Ranged Weapon",
            "Magic Weapon",
            "Martial Melee Weapon",
            "Martial Ranged Weapon",
            "Simple Melee Weapon",
            "Simple Ranged Weapon",
        };
        public static readonly List<string> ArmorItemTypes = new()
        {
            "Clothing",
            "Heavy Armor",
            "Light Armor",
            "Medium Armor",
            "Shield"
        };
        public static readonly List<string> DamageTypes = new()
        {
            "Acid",
            "Cold",
            "Fire",
            "Force",
            "Lightning",
            "Necrotic",
            "Poison",
            "Psychic",
            "Radiant",
            "Thunder",
            "Bludgeoning",
            "Slashing",
            "Piercing",
        };
        public static readonly List<string> DamageProclivities = new()
        {
            "Normal",
            "Vulnerable",
            "Resistant",
            "Immune",
            "Resistant if Non-Magical",
            "Resistant if Non-Adamantine",
            "Resistant if Non-Silvered",
            "Immune if Non-Magical",
            "Immune if Non-Adamantine",
            "Immune if Non-Silvered"
        };
        public static readonly List<string> AbilityTypes = new()
        {
            "Strength",
            "Dexterity",
            "Constitution",
            "Intelligence",
            "Wisdom",
            "Charisma"
        };
        public static readonly List<string> SchoolsOfMagic = new()
        {
            "Abjuration",
            "Conjuration",
            "Divination",
            "Enchantment",
            "Evocation",
            "Illusion",
            "Necromancy",
            "Transmutation"
        };
        public static readonly List<string> AoeShapes = new()
        {
            "Line",
            "Circle",
            "Cone",
            "Cube",
            "Sphere",
            "Square",
            "Cylinder"
        };
        public static readonly List<string> Environments = new()
        {
            "Grassland / Hillside",
            "Forest / Woodland",
            "Swamp / Jungle",
            "Desert / Badlands",
            "Mountain / Cave",
            "Ocean / Coast"
        };
        public static readonly List<string> CreatureEnvironments = new()
        {
            "Arctic","Coastal","Desert","Forest",
            "Grassland","Hill","Mountain","Swamp",
            "Underdark","Underwater","Urban"
        };
        public static readonly List<string> FishingEnvironments = new()
        {
            "Saltwater",
            "Freshwater",
        };
        public static readonly List<string> IconList = new()
        {
            "Icon_Construct",
            "Icon_Beast",
            "Icon_Fiend"
        };
        public static readonly List<string> ChallengeRatings = new()
        {
            "0", "1/8", "1/4", "1/2", 
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"
        };
        public static readonly List<string> AttackTypes = new()
        {
            "Melee","Ranged","Magic Weapon","Magic Ability","Other"
        };
        public static readonly List<string> AttackOptionTypes = new()
        {
            "Attack Modifier",
            "Damage Modifier", 
            "Use Advantage", 
            "Use Disadvantage",
            "Alternate Base Attack Damage",
            "Extra Damage on Hit - Roll", 
            "Extra Damage on Hit - Set",
            "Extra Damage on Critical - Roll",
            "Extra Damage on Critical - Set",
            "Reroll Attack Damage Die at or Below",
        };
        public static readonly List<string> RarityLevels = new()
        {
            "Common",
            "Uncommon",
            "Rare",
            "Very Rare",
            "Legendary"
        };
        public static readonly List<string> SpellsKnownPerLevelOptions = new()
        {
            "Set",
            "Any"
        };
        public static readonly List<SpellTableRowModel> MulticlassSpellSlotTable = new()
        {
            new(1,2),
            new(2,3),
            new(3,4,2),
            new(4,4,3),
            new(5,4,3,2),
            new(6,4,3,3),
            new(7,4,3,3,1),
            new(8,4,3,3,2),
            new(9,4,3,3,3,1),
            new(10,4,3,3,3,2),
            new(11,4,3,3,3,2,1),
            new(12,4,3,3,3,2,1),
            new(13,4,3,3,3,2,1,1),
            new(14,4,3,3,3,2,1,1),
            new(15,4,3,3,3,2,1,1,1),
            new(16,4,3,3,3,2,1,1,1),
            new(17,4,3,3,3,2,1,1,1,1),
            new(18,4,3,3,3,3,1,1,1,1),
            new(19,4,3,3,3,3,2,1,1,1),
            new(20,4,3,3,3,3,2,2,1,1)
        };
        public static readonly List<string> InternalAbilityVariables = new()
        {
            "[Is Critical Hit]",
            "[Attack with Advantage]",
            "[Attack with Disadvantage]"
        };
        public static readonly List<string> AlterantStats = new()
        {
            "Armor Class",
            "Speed",
            "Darkvision",
            "Strength",
            "Dexterity",
            "Constitution",
            "Intelligence",
            "Wisdom",
            "Charisma",
            "Acrobatics",
            "Animal Handling",
            "Arcana",
            "Athletics",
            "Deception",
            "History",
            "Insight",
            "Intimidation",
            "Investigation",
            "Medicine",
            "Nature",
            "Perception",
            "Performance",
            "Persuasion",
            "Religion",
            "Sleight of Hand",
            "Stealth",
            "Survival"
        };

        public static List<CreatureModel> CreatureRepository = new();
        public static List<ItemModel> ItemRepository = new();
        public static List<SpellModel> SpellRepository = new();

        public static readonly string CreatureDataFilePath = "Data/Creatures.xml";
        public static readonly string ItemDataFilePath = "Data/Items.xml";
        public static readonly string SpellDataFilePath = "Data/Spells.xml";
        public static readonly string CharacterDataFilePath = "Data/Characters.xml";
        public static readonly string LootBoxDataFilePath = "Data/LootBoxes.xml";
        public static readonly string RollTableDataFilePath = "Data/RollTables.xml";
        public static readonly string SettingsDataFilePath = "Data/Settings.xml";
        public static readonly string NpcDataFilePath = "Data/Npcs.xml";
        public static readonly string LanguageDataFilePath = "Data/Languages.xml";
        public static readonly string CreaturePackDataFilePath = "Data/CreaturePacks.xml";
        public static readonly string PlayerClassDataFilePath = "Data/PlayerClasses.xml";
        public static readonly string PlayerSubclassDataFilePath = "Data/PlayerSubclasses.xml";
        public static readonly string PlayerRaceDataFilePath = "Data/PlayerRaces.xml";
        public static readonly string PlayerSubraceDataFilePath = "Data/PlayerSubraces.xml";
        public static readonly string PlayerBackgroundDataFilePath = "Data/PlayerBackgrounds.xml";
        public static readonly string PlayerFeatDataFilePath = "Data/PlayerFeats.xml";
        public static readonly string ShopDataFilePath = "Data/Shops.xml";
        public static readonly string EldritchInvocationsDataFilePath = "Data/EldritchInvocations.xml";
        public static readonly string CampaignDataFilePath = "Data/Campaigns.xml";
        public static readonly string WeatherDataFilePath = "Data/Weathers.xml";
        public static readonly string CalendarDataFilePath = "Data/Calendars.xml";
        public static readonly string SourcebookDataFilePath = "Data/Sourcebooks.xml";
        public static readonly string NoteTypeDataFilePath = "Data/NoteTypes.xml";
        public static readonly DateTime StartDateTime = Convert.ToDateTime("1/1/2000 00:00:00 AM");

        public static readonly string XmlFileFilter = "XML files (*.xml)|*.xml";
        public static readonly string ImageFileFilter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif";
        public static readonly string DocFileFilter = "Document files|*.docx;*.pdf;";
        public static readonly string AllFileFilter = "All files|*.*";

        public static readonly string WebDriverLocation = Environment.CurrentDirectory + "\\WebDriver";

        public static readonly string SystemAudio_DiceRoll = Environment.CurrentDirectory + "/Audio/System/DiceRoll.mp3";
        public static readonly string SystemAudio_ShopItemMove = Environment.CurrentDirectory + "/Audio/System/ShopItemMove.wav";
        public static readonly string SystemAudio_ShopGreeting = Environment.CurrentDirectory + "/Audio/System/ShopGreeting.mp3";
        public static readonly string SystemAudio_ShopFarewell = Environment.CurrentDirectory + "/Audio/System/ShopFarewell.mp3";
        public static readonly string SystemAudio_MenuSelect = Environment.CurrentDirectory + "/Audio/System/MenuSelect.wav";

        public static string[] AlphaArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static MainViewModel MainModelRef;
        public static ShopViewModel ShopRef;
        public static FrameworkElement framework = new();
        public static Random RNG = new();
        public static NoteModel CopiedNote = null;
        public static CustomAbility CopiedAbility = null;

    }

}