using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class NoteModel : BaseModel
    {
        // Constructors
        public NoteModel()
        {
            Category = "Other";
            Header = "New Note";
            Content = string.Empty;
            SubNotes = new ObservableCollection<NoteModel>();
        }

        // Databound Properties
        #region Category
        private string _Category;
        [XmlSaveMode(XSME.Single)]
        public string Category
        {
            get => _Category;
            set
            {
                _Category = value;
                NotifyPropertyChanged();
                SetIcon();
            }
        }
        #endregion
        #region Icon
        private string _Icon;
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; NotifyPropertyChanged(); }
        }
        #endregion
        #region Header
        private string _Header;
        [XmlSaveMode(XSME.Single)]
        public string Header
        {
            get => _Header;
            set => SetAndNotify(ref _Header, value);
        }
        #endregion
        #region Content
        private string _Content;
        [XmlSaveMode(XSME.Single)]
        public string Content
        {
            get => _Content;
            set => SetAndNotify(ref _Content, value);
        }
        #endregion
        #region SubNotes
        private ObservableCollection<NoteModel> _SubNotes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<NoteModel> SubNotes
        {
            get => _SubNotes;
            set => SetAndNotify(ref _SubNotes, value);
        }
        #endregion
        #region IsExpanded
        private bool _IsExpanded;
        public bool IsExpanded
        {
            get => _IsExpanded;
            set => SetAndNotify(ref _IsExpanded, value);
        }
        #endregion
        #region IsSelected
        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set => SetAndNotify(ref _IsSelected, value);
        }
        #endregion
        #region IsSearchMatch
        private bool _IsSearchMatch;
        public bool IsSearchMatch
        {
            get => _IsSearchMatch;
            set => SetAndNotify(ref _IsSearchMatch, value);
        }
        #endregion
        #region AttachmentFileName
        private string _AttachmentFileName;
        [XmlSaveMode(XSME.Single)]
        public string AttachmentFileName
        {
            get => _AttachmentFileName;
            set => SetAndNotify(ref _AttachmentFileName, value);
        }
        #endregion

        // Commands
        #region AddSubNote
        public ICommand AddSubNote => new RelayCommand(param => DoAddSubNote());
        private void DoAddSubNote()
        {
            IsExpanded = true;
            SubNotes.Add(new NoteModel());
            if (Configuration.MainModelRef.TabSelected_Players)
            {
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveNote = SubNotes.Last();
            }
            else if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNote = SubNotes.Last();
            }
            SubNotes.Last().IsSelected = true;
        }
        #endregion
        #region DeleteNote
        public ICommand DeleteNote => new RelayCommand(param => DoDeleteNote());
        private void DoDeleteNote()
        {
            if (this.SubNotes.Count > 0)
            {
                YesNoDialog question = new("Are you sure you want to delete this note? All sub notes will also be deleted.");
                question.ShowDialog();
                if (question.Answer == false) { return; }
            }
            else if (this.Content.Length > 0)
            {
                YesNoDialog question = new("Are you sure you want to delete this note?");
                question.ShowDialog();
                if (question.Answer == false) { return; }
            }
            if (Configuration.MainModelRef.TabSelected_Players)
            {
                FindAndDeleteNote(Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Notes, this, out _);
                Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.ActiveNote = null;
            }
            else if (Configuration.MainModelRef.TabSelected_Campaigns)
            {
                FindAndDeleteNote(Configuration.MainModelRef.CampaignView.ActiveCampaign.Notes, this, out _);
                Configuration.MainModelRef.CampaignView.ActiveCampaign.ActiveNote = null;
            }
        }
        #endregion
        #region CopyNote
        public ICommand CopyNote => new RelayCommand(param => DoCopyNote());
        private void DoCopyNote()
        {
            Configuration.CopiedNote = HelperMethods.DeepClone(this);
        }
        #endregion
        #region PasteNote
        public ICommand PasteNote => new RelayCommand(param => DoPasteNote());
        private void DoPasteNote()
        {
            if (Configuration.CopiedNote == null) { return; }
            this.SubNotes.Add(HelperMethods.DeepClone(Configuration.CopiedNote));
            this.IsSelected = true;
            this.IsExpanded = true;
        }
        #endregion
        #region SelectAttachment
        public ICommand SelectAttachment => new RelayCommand(param => DoSelectAttachment());
        private void DoSelectAttachment()
        {
            OpenFileDialog openWindow = new()
            {
                Filter = Configuration.ImageFileFilter + "|" + Configuration.DocFileFilter + "|" + Configuration.AllFileFilter
            };
            if (openWindow.ShowDialog() == true)
            {
                string noteDirectory = Environment.CurrentDirectory + "/NoteAttachments/";
                if (File.Exists(noteDirectory + openWindow.SafeFileName))
                {
                    YesNoDialog question = new(openWindow.SafeFileName + " already exists in the note attachments directory, overwrite?");
                    if (question.ShowDialog() == true) 
                    {
                        if (question.Answer == false) 
                        {
                            YesNoDialog linkQuestion = new("Link existing file to this note?");
                            if (linkQuestion.ShowDialog() == true)
                            {
                                if (linkQuestion.Answer == true)
                                {
                                    AttachmentFileName = openWindow.SafeFileName;
                                }
                            }
                            return; 
                        }
                    }
                    else { return; }
                }
                File.Copy(openWindow.FileName, noteDirectory + openWindow.SafeFileName, true);
                AttachmentFileName = openWindow.SafeFileName;
            }
        }
        #endregion
        #region ViewAttachment
        public ICommand ViewAttachment => new RelayCommand(param => DoViewAttachment());
        private void DoViewAttachment()
        {
            try
            {
                System.Diagnostics.Process.Start("explorer", Environment.CurrentDirectory + "\\NoteAttachments\\" + AttachmentFileName);
            }
            catch (Exception e)
            {
                YesNoDialog question = new(e.Message + "\nUnlink?");
                question.ShowDialog();
                if (question.Answer == true)
                {
                    AttachmentFileName = string.Empty;
                }
            }
        }
        #endregion
        #region RemoveAttachment
        public ICommand RemoveAttachment => new RelayCommand(param => DoRemoveAttachment());
        private void DoRemoveAttachment()
        {
            try
            {
                string fileName = AttachmentFileName;
                AttachmentFileName = string.Empty;
                YesNoDialog question = new("Attachment unlinked, delete file?");
                if (question.ShowDialog() == true)
                {
                    if (question.Answer == true)
                    {
                        File.Delete(Environment.CurrentDirectory + "/NoteAttachments/" + fileName);
                        HelperMethods.NotifyUser(fileName + " deleted.");
                    }
                }
                
            }
            catch (Exception e)
            {
                HelperMethods.NotifyUser(e.Message);
            }
        }
        #endregion

        // Private Methods
        private void FindAndDeleteNote(ObservableCollection<NoteModel> notes, NoteModel noteToDelete, out bool complete)
        {
            if (notes.Remove(noteToDelete)) { complete = true; return; }
            foreach (NoteModel note in notes)
            {
                FindAndDeleteNote(note.SubNotes, noteToDelete, out complete);
                if (complete) { return; }
            }
            complete = false;
        }
        private void SetIcon()
        {
            NoteType matchedNote = Configuration.MainModelRef.ToolsView.NoteTypes.FirstOrDefault(n => n.Name == Category);
            if (matchedNote != null) { Icon = matchedNote.Icon; }
            else { Icon = "Icon_Rpg_Note"; }
        }

    }
}
