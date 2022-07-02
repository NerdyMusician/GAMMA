using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class GameNote : BaseModel
    {
        // Constructors
        public GameNote()
        {
            AssociatedNotes = new();
            Name = string.Empty;
            Content = string.Empty;
        }

        // Properties
        private string _Id;
        [XmlSaveMode(XSME.Single)]
        public string Id
        {
            get => _Id;
            set => SetAndNotify(ref _Id, value);
        }
        private string _Name;
        [XmlSaveMode(XSME.Single)]
        public string Name
        {
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        private string _Type;
        [XmlSaveMode(XSME.Single)]
        public string Type
        {
            get => _Type;
            set => SetAndNotify(ref _Type, value);
        }
        private string _Content;
        [XmlSaveMode(XSME.Single)]
        public string Content
        {
            get => _Content;
            set => SetAndNotify(ref _Content, value);
        }
        private ObservableCollection<GameNote> _AssociatedNotes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<GameNote> AssociatedNotes
        {
            get => _AssociatedNotes;
            set => SetAndNotify(ref _AssociatedNotes, value);
        }

        // Commands
        public ICommand AddNewAssociatedNote => new RelayCommand(DoAddNewAssociatedNote);
        private void DoAddNewAssociatedNote(object param)
        {
            GameNote newNote = new();
            newNote.SetNewNoteValues();
            AssociatedNotes.Add(newNote.DeepClone());
            Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes.Add(newNote);
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNewNote = newNote;
            GameNote copyOfThis = this.DeepClone();
            copyOfThis.AssociatedNotes.Clear();
            newNote.AssociatedNotes.Add(copyOfThis);
        }
        public ICommand AddAssociations => new RelayCommand(DoAddAssociations);
        private void DoAddAssociations(object param)
        {
            List<GameNote> recordOptions = new();
            foreach (GameNote note in Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes)
            {
                if (note.Id == Id) { continue; } // don't associated with self
                GameNote existingNote = AssociatedNotes.FirstOrDefault(n => n.Id == note.Id);
                if (existingNote == null)
                {
                    GameNote noteToAdd = note.DeepClone();
                    noteToAdd.AssociatedNotes.Clear();
                    recordOptions.Add(noteToAdd);
                }
            }
            MultiObjectSelectionDialog multiAdd = new(recordOptions, "Associated Notes");
            if (multiAdd.ShowDialog() == true)
            {
                foreach (GameNote note in (multiAdd.DataContext as MultiObjectSelectionViewModel).SelectedNotes)
                {
                    AssociatedNotes.Add(note);
                    GameNote otherNote = Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes.First(n => n.Id == note.Id);
                    GameNote backLink = this.DeepClone();
                    backLink.AssociatedNotes.Clear();
                    otherNote.AssociatedNotes.Add(backLink);
                }
                SortAssociatedNotes();
            }
        }
        public ICommand RemoveNote => new RelayCommand(DoRemoveNote);
        private void DoRemoveNote(object param)
        {
            if (param == null) { return; }
            if (param.ToString() == "Campaign")
            {
                string id = Id;
                Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes.Remove(this);
                foreach (GameNote note in Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes)
                {
                    note.AssociatedNotes = new(note.AssociatedNotes.Where(n => n.Id != id));
                }
            }
            if (param.ToString() == "Note")
            {
                string otherId = Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNewNote.Id;
                GameNote otherNote = Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes.First(n => n.Id == Id);
                otherNote.AssociatedNotes = new(otherNote.AssociatedNotes.Where(n => n.Id != otherId));
                Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNewNote.AssociatedNotes.Remove(this);
            }
        }
        public ICommand GoToNote => new RelayCommand(DoGoToNote);
        private void DoGoToNote(object param)
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNewNote = Configuration.MainModelRef.CampaignView.ActiveCampaign.NewNotes.First(n => n.Id == Id);
        }
        public ICommand SortNotes => new RelayCommand(DoSortNotes);
        private void DoSortNotes(object param)
        {
            AssociatedNotes = new(AssociatedNotes.OrderBy(n => n.Type).ThenBy(n => n.Name));
        }

        // Public Methods
        public void SetNewNoteValues()
        {
            Id = HelperMethods.GetUniqueId();
            Name = "New Note";
        }

        // Private Methods
        private void SortAssociatedNotes()
        {
            AssociatedNotes = new(AssociatedNotes.OrderBy(n => n.Type).ThenBy(n => n.Name));
        }

    }
}
