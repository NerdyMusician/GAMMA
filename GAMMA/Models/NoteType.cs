using GAMMA.Toolbox;
using System.Collections.Generic;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class NoteType : BaseModel
    {
        // Constructors
        public NoteType()
        {
            Name = "New Note Type";
            SortSubNotes = true;
            NoteIcons = new()
            {
                "Icon_Book",
                "Icon_Crossed_Swords",
                "Icon_Hand",
                "Icon_Hex",
                "Icon_Home",
                "Icon_Map",
                "Icon_Mountain",
                "Icon_Pack",
                "Icon_Puzzle",
                "Icon_Rpg_Note",
                "Icon_Smile",
                "Icon_Trap",
                "Icon_Vendor",
            };
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(); }
        }
        #endregion
        #region Icon
        private string _Icon;
        [XmlSaveMode(XSME.Single)]
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; NotifyPropertyChanged();}
        }
        #endregion
        #region SortSubNotes
        private bool _SortSubNotes;
        [XmlSaveMode(XSME.Single)]
        public bool SortSubNotes
        {
            get { return _SortSubNotes; }
            set { _SortSubNotes = value; NotifyPropertyChanged(); }
        }
        #endregion

        // Readonly Properties
        public List<string> NoteIcons { get; set; }

        // Commands
        #region RemoveNoteType
        public ICommand RemoveNoteType => new RelayCommand(DoRemoveNoteType);
        private void DoRemoveNoteType(object param)
        {
            Configuration.MainModelRef.ToolsView.NoteTypes.Remove(this);
        }
        #endregion

    }
}
