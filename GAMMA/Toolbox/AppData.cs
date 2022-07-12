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
            foreach (MemberInfo mi in typeof(AppData).GetMembers())
            {
                if (mi.Name.StartsWith(prefix)) { n.Add(mi.Name.Replace(prefix, string.Empty).Replace("_"," ")); }
            }
            return n;
        }

        // Misc


        // Icons
        public const string Icon_Book = "Icon_Book";
        public const string Icon_CrossedSwords = "Icon_Crossed_Swords";
        public const string Icon_Hand = "Icon_Hand";
        public const string Icon_Hex = "Icon_Hex";
        public const string Icon_Home = "Icon_Home";
        public const string Icon_Map = "Icon_Map";
        public const string Icon_Mountain = "Icon_Mountain";
        public const string Icon_Pack = "Icon_Pack";
        public const string Icon_Puzzle = "Icon_Puzzle";
        public const string Icon_RpgNote = "Icon_Rpg_Note";
        public const string Icon_Smile = "Icon_Smile";
        public const string Icon_Trap = "Icon_Trap";
        public const string Icon_Vendor = "Icon_Vendor";

        // Note Types
        public const string NoteType_Character = "Character";
        public const string NoteType_District = "District";
        public const string NoteType_Encounter = "Encounter";
        public const string NoteType_Faction = "Faction";
        public const string NoteType_Item = "Item";
        public const string NoteType_Landmark = "Landmark";
        public const string NoteType_Location = "Location";
        public const string NoteType_Map = "Map";
        public const string NoteType_Other = "Other";
        public const string NoteType_Puzzle = "Puzzle";
        public const string NoteType_Quest = "Quest";
        public const string NoteType_Trap = "Trap";
        public const string NoteType_Vendor = "Vendor";
        public static readonly List<string> NoteTypes = GetPropertiesStartingWith("NoteType_");
        public static readonly Dictionary<string, string> NoteTypeIcons = new()
        {
            { string.Empty, Icon_RpgNote },
            { NoteType_Character, Icon_Smile },
            { NoteType_District, Icon_Hex },
            { NoteType_Encounter, Icon_CrossedSwords },
            { NoteType_Item, Icon_Pack },
            { NoteType_Faction, Icon_Hand },
            { NoteType_Landmark, Icon_Mountain },
            { NoteType_Location, Icon_Home },
            { NoteType_Map, Icon_Map },
            { NoteType_Other, Icon_RpgNote },
            { NoteType_Puzzle, Icon_Puzzle },
            { NoteType_Quest, Icon_Book },
            { NoteType_Trap, Icon_Trap },
            { NoteType_Vendor, Icon_Vendor },
        };

        // Variable Types
        public const string VarType_Text = "Text";
        public const string VarType_Number = "Number";
        public const string VarType_Toggled_Option = "Toggled Option";
        public static readonly List<string> VarTypes = GetPropertiesStartingWith("VarType_");
        public const string SpecType_Internal_Bool = "Internal Bool";

        // Pre-Action Types
        public const string PreAction_Add_Roll = "Add Roll";
        public const string PreAction_Add_Set_Value = "Add Set Value";
        public const string PreAction_Add_Stat_Value = "Add Stat Value";
        public const string PreAction_Add_Calculated_Value = "Add Calculated Value";
        public const string PreAction_Append_Text = "Append Text";
        public const string PreAction_QA_Prompt = "QA Prompt";
        public const string PreAction_Make_Attack_Roll = "Make Attack Roll";
        public const string PreAction_Numeric_Value_Prompt = "Numeric Value Prompt";
        public const string PreAction_Translate_Value = "Translate Value";
        public static readonly List<string> PreActions = GetPropertiesStartingWith("PreAction_");

        // Post-Action Types
        public const string PostAction_Activate_Concentration = "Activate Concentration";
        public const string PostAction_Activate_Alterant = "Activate Alterant";
        public const string PostAction_Add_Minions = "Add Minions";
        public const string PostAction_Add_to_Current_HP = "Add to Current HP";
        public const string PostAction_Add_to_Temporary_HP = "Add to Temporary HP";
        public const string PostAction_Add_Active_Effect = "Add Active Effect";
        public const string PostAction_Expend_Counter = "Expend Counter";
        public const string PostAction_Roll_Table = "Roll Table";
        public const string PostAction_Subtract_from_Current_HP = "Subtract from Current HP";
        public static readonly List<string> PostActions = GetPropertiesStartingWith("PostAction_");

    }
}
