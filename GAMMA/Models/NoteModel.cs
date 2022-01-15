using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            Content = "";
            SubNotes = new ObservableCollection<NoteModel>();
        }

        // Databound Properties
        #region Category
        private string _Category;
        [XmlSaveMode(XSME.Single)]
        public string Category
        {
            get
            {
                return _Category;
            }
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
            get
            {
                return _Header;
            }
            set
            {
                _Header = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Content
        private string _Content;
        [XmlSaveMode(XSME.Single)]
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SubNotes
        private ObservableCollection<NoteModel> _SubNotes;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<NoteModel> SubNotes
        {
            get
            {
                return _SubNotes;
            }
            set
            {
                _SubNotes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsExpanded
        private bool _IsExpanded;
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                _IsExpanded = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsSelected
        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsSearchMatch
        private bool _IsSearchMatch;
        public bool IsSearchMatch
        {
            get
            {
                return _IsSearchMatch;
            }
            set
            {
                _IsSearchMatch = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region AttachmentFileName
        private string _AttachmentFileName;
        [XmlSaveMode(XSME.Single)]
        public string AttachmentFileName
        {
            get
            {
                return _AttachmentFileName;
            }
            set
            {
                _AttachmentFileName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddSubNote
        private RelayCommand _AddSubNote;
        public ICommand AddSubNote
        {
            get
            {
                if (_AddSubNote == null)
                {
                    _AddSubNote = new RelayCommand(param => DoAddSubNote());
                }
                return _AddSubNote;
            }
        }
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
        private RelayCommand _DeleteNote;
        public ICommand DeleteNote
        {
            get
            {
                if (_DeleteNote == null)
                {
                    _DeleteNote = new RelayCommand(param => DoDeleteNote());
                }
                return _DeleteNote;
            }
        }
        private void DoDeleteNote()
        {
            if (this.SubNotes.Count() > 0)
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
        private RelayCommand _CopyNote;
        public ICommand CopyNote
        {
            get
            {
                if (_CopyNote == null)
                {
                    _CopyNote = new RelayCommand(param => DoCopyNote());
                }
                return _CopyNote;
            }
        }
        private void DoCopyNote()
        {
            Configuration.CopiedNote = HelperMethods.DeepClone(this);
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
            this.SubNotes.Add(HelperMethods.DeepClone(Configuration.CopiedNote));
            this.IsSelected = true;
            this.IsExpanded = true;
        }
        #endregion
        #region SelectAttachment
        private RelayCommand _SelectAttachment;
        public ICommand SelectAttachment
        {
            get
            {
                if (_SelectAttachment == null)
                {
                    _SelectAttachment = new RelayCommand(param => DoSelectAttachment());
                }
                return _SelectAttachment;
            }
        }
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
        private RelayCommand _ViewAttachment;
        public ICommand ViewAttachment
        {
            get
            {
                if (_ViewAttachment == null)
                {
                    _ViewAttachment = new RelayCommand(param => DoViewAttachment());
                }
                return _ViewAttachment;
            }
        }
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
                    AttachmentFileName = "";
                }
            }
        }
        #endregion
        #region RemoveAttachment
        private RelayCommand _RemoveAttachment;
        public ICommand RemoveAttachment
        {
            get
            {
                if (_RemoveAttachment == null)
                {
                    _RemoveAttachment = new RelayCommand(param => DoRemoveAttachment());
                }
                return _RemoveAttachment;
            }
        }
        private void DoRemoveAttachment()
        {
            try
            {
                string fileName = AttachmentFileName;
                AttachmentFileName = "";
                YesNoDialog question = new("Attachment unlinked, delete file?");
                if (question.ShowDialog() == true)
                {
                    if (question.Answer == true)
                    {
                        File.Delete(Environment.CurrentDirectory + "/NoteAttachments/" + fileName);
                        new NotificationDialog(fileName + " deleted.").ShowDialog();
                    }
                }
                
            }
            catch (Exception e)
            {
                new NotificationDialog(e.Message).ShowDialog();
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
