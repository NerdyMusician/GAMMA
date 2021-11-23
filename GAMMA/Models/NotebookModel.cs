using GAMMA.Toolbox;
using GAMMA.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class NotebookModel : BaseModel
    {
        // Constructors
        public NotebookModel()
        {
            Name = "New Notebook";
            Notes = new ObservableCollection<NoteModel>();
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode("Single")]
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
        #region Notes
        private ObservableCollection<NoteModel> _Notes;
        [XmlSaveMode("Enumerable")]
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

        // Commands
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
                "\nNote: Quest type immediate sub notes are not sorted.";

            YesNoDialog question = new(message);
            question.ShowDialog();
            if (question.Answer == false) { return; }

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
        #region ClearSearchMatches
        private RelayCommand _ClearSearchMatches;
        public ICommand ClearSearchMatches
        {
            get
            {
                if (_ClearSearchMatches == null)
                {
                    _ClearSearchMatches = new RelayCommand(param => DoClearSearchMatches());
                }
                return _ClearSearchMatches;
            }
        }
        private void DoClearSearchMatches()
        {
            HelperMethods.ClearNoteSearch(Notes);
        }
        #endregion

        // Private Methods
        private List<NoteModel> SortNoteList(List<NoteModel> notes)
        {
            List<NoteModel> sortedNotes = notes.OrderBy(note =>
            note.Category == "Location" ? 1 :
            note.Category == "District" ? 2 :
            note.Category == "Faction" ? 3 :
            note.Category == "Character" ? 4 :
            note.Category == "Quest" ? 5 :
            note.Category == "Map" ? 6 :
            note.Category == "Vendor" ? 7 : 8).ThenBy(note => note.Header).ToList();
            foreach (NoteModel note in sortedNotes)
            {
                if (note.Category == "Quest") { continue; }
                note.SubNotes = new ObservableCollection<NoteModel>(SortNoteList(note.SubNotes.ToList()));
            }
            return sortedNotes;
        }
        

    }
}
