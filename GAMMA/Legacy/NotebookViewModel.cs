//using GAMMA.Models;
//using GAMMA.Toolbox;
//using GAMMA.Windows;
//using Microsoft.Win32;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows.Input;
//using System.Xml.Linq;

//namespace GAMMA.ViewModels
//{
//    public class NotebookViewModel : BaseModel
//    {
//        // Constructors
//        public NotebookViewModel()
//        {
//            XmlMethods.XmlToList(Configuration.NotebookDataFilePath, out List<NotebookModel> characters);
//            Notebooks = new ObservableCollection<NotebookModel>(characters);
//        }

//        // Databound Properties
//        #region Notebooks
//        private ObservableCollection<NotebookModel> _Notebooks;
//        public ObservableCollection<NotebookModel> Notebooks
//        {
//            get
//            {
//                return _Notebooks;
//            }
//            set
//            {
//                _Notebooks = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//        #region ActiveNotebook
//        private NotebookModel _ActiveNotebook;
//        public NotebookModel ActiveNotebook
//        {
//            get
//            {
//                return _ActiveNotebook;
//            }
//            set
//            {
//                _ActiveNotebook = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion

//        // Commands
//        #region AddNotebook
//        private RelayCommand _AddNotebook;
//        public ICommand AddNotebook
//        {
//            get
//            {
//                if (_AddNotebook == null)
//                {
//                    _AddNotebook = new RelayCommand(param => DoAddNotebook());
//                }
//                return _AddNotebook;
//            }
//        }
//        private void DoAddNotebook()
//        {
//            Notebooks.Add(new NotebookModel());
//            ActiveNotebook = Notebooks.Last();
//        }
//        #endregion
//        #region SaveNotebooks
//        private RelayCommand _SaveNotebooks;
//        public ICommand SaveNotebooks
//        {
//            get
//            {
//                if (_SaveNotebooks == null)
//                {
//                    _SaveNotebooks = new RelayCommand(param => DoSaveNotebooks());
//                }
//                return _SaveNotebooks;
//            }
//        }
//        public bool DoSaveNotebooks()
//        {
//            if (Notebooks.Count() == 0)
//            {
//                // Prevents zero notebook save crash
//                XDocument blankDoc = new XDocument();
//                blankDoc.Add(new XElement("NotebookModelSet"));
//                blankDoc.Save("Data/Notebooks.xml");
//            }
//            else
//            {
//                XDocument itemDocument = new XDocument();
//                itemDocument.Add(XmlMethods.ListToXml(Notebooks.ToList()));
//                itemDocument.Save("Data/Notebooks.xml");
//            }
//            new NotificationDialog("Notebooks Saved.").ShowDialog();
//            return true;
//        }
//        #endregion
//        #region ImportNotebooks
//        private RelayCommand _ImportNotebooks;
//        public ICommand ImportNotebooks
//        {
//            get
//            {
//                if (_ImportNotebooks == null)
//                {
//                    _ImportNotebooks = new RelayCommand(param => DoImportNotebooks());
//                }
//                return _ImportNotebooks;
//            }
//        }
//        private void DoImportNotebooks()
//        {
//            OpenFileDialog openWindow = new OpenFileDialog
//            {
//                Filter = "XML Files (*.xml)|*.xml"
//            };

//            YesNoDialog question = new YesNoDialog("Prior to import, the current notebook list must be saved.\nContinue?");
//            question.ShowDialog();
//            if (question.Answer == false) { return; }

//            if (DoSaveNotebooks() == false) { return; }

//            if (openWindow.ShowDialog() == true)
//            {
//                XmlMethods.XmlToList(openWindow.FileName, out List<NotebookModel> importedNotebooks);
//                XmlMethods.XmlToList(Configuration.NotebookDataFilePath, out List<NotebookModel> currentNotebooks);
//                List<NotebookModel> combinedNotebookList = new List<NotebookModel>();
//                string message = "Notebooks Imported:";

//                List<string> notebookNames = new List<string>();
//                foreach (NotebookModel notebook in currentNotebooks)
//                {
//                    if (notebookNames.Contains(notebook.Name) == false) { notebookNames.Add(notebook.Name); }
//                }
//                foreach (NotebookModel notebook in importedNotebooks)
//                {
//                    if (notebookNames.Contains(notebook.Name) == false) { notebookNames.Add(notebook.Name); }
//                }

//                foreach (string name in notebookNames)
//                {
//                    NotebookModel currentNotebook = currentNotebooks.FirstOrDefault(notebook => notebook.Name == name);
//                    NotebookModel importedNotebook = importedNotebooks.FirstOrDefault(notebook => notebook.Name == name);

//                    if (currentNotebook == null) { combinedNotebookList.Add(importedNotebook); message += "\n" + name; continue; }
//                    if (importedNotebook == null) { combinedNotebookList.Add(currentNotebook); continue; }
//                    combinedNotebookList.Add(currentNotebook);
//                }

//                Notebooks = new ObservableCollection<NotebookModel>(combinedNotebookList.OrderBy(notebook => notebook.Name));

//                if (message == "Notebooks Imported:") { message = "No notebooks imported."; }

//                new NotificationDialog(message).ShowDialog();

//            }
//        }
//        #endregion

//    }
//}
