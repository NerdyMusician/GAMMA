using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GAMMA.Toolbox
{
    public static class AppData
    {
        private static List<string> GetPropertiesStartingWith(string prefix)
        {
            List<string> n = new();
            foreach (PropertyInfo pi in typeof(AppData).GetProperties())
            {
                if (pi.Name.StartsWith(prefix)) { n.Add(pi.Name); }
            }
            return n;
        }

        // Icons
        public const string IconBook = "Icon_Book";
        public const string IconCrossedSwords = "Icon_Crossed_Swords";
        public const string IconHand = "Icon_Hand";
        public const string IconHex = "Icon_Hex";
        public const string IconHome = "Icon_Home";
        public const string IconMap = "Icon_Map";
        public const string IconMountain = "Icon_Mountain";
        public const string IconPack = "Icon_Pack";
        public const string IconPuzzle = "Icon_Puzzle";
        public const string IconRpgNote = "Icon_Rpg_Note";
        public const string IconSmile = "Icon_Smile";
        public const string IconTrap = "Icon_Trap";
        public const string IconVendor = "Icon_Vendor";

        // Note Types
        public const string NoteTypeCharacter = "Character";
        public const string NoteTypeDistrict = "District";
        public const string NoteTypeEncounter = "Encounter";
        public const string NoteTypeFaction = "Faction";
        public const string NoteTypeItem = "Item";
        public const string NoteTypeLandmark = "Landmark";
        public const string NoteTypeLocation = "Location";
        public const string NoteTypeMap = "Map";
        public const string NoteTypeOther = "Other";
        public const string NoteTypePuzzle = "Puzzle";
        public const string NoteTypeQuest = "Quest";
        public const string NoteTypeTrap = "Trap";
        public const string NoteTypeVendor = "Vendor";
        public static readonly List<string> NoteTypes = GetPropertiesStartingWith("NoteType");
        public static readonly Dictionary<string, string> NoteTypeIcons = new()
        {
            { NoteTypeCharacter, IconSmile },
            { NoteTypeDistrict, IconHex },
            { NoteTypeEncounter, IconCrossedSwords },
            { NoteTypeItem, IconPack },
            { NoteTypeFaction, IconHand },
            { NoteTypeLandmark, IconMountain },
            { NoteTypeLocation, IconHome },
            { NoteTypeMap, IconMap },
            { NoteTypeOther, IconRpgNote },
            { NoteTypePuzzle, IconPuzzle },
            { NoteTypeQuest, IconBook },
            { NoteTypeTrap, IconTrap },
            { NoteTypeVendor, IconVendor },
        };
        
    }
}
