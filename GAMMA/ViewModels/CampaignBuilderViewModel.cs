using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace GAMMA.ViewModels
{
    public class CampaignBuilderViewModel : BaseModel
    {
        // Constructors
        public CampaignBuilderViewModel()
        {
            XmlMethods.XmlToList(Configuration.CampaignDataFilePath, out List<GameCampaign> campaigns);
            Campaigns = new(campaigns);
            foreach (GameCampaign campaign in Campaigns)
            {
                campaign.SortCombatants();
                campaign.UpdateActiveCombatant();
                foreach (CreatureModel creature in campaign.Combatants)
                {
                    if (creature.DisplayName == "" || creature.DisplayName == null)
                    {
                        creature.DisplayName = creature.Name + " " + creature.TrackerIndicator;
                    }
                    creature.UpdateStatus();
                    creature.SetFormattedTexts();
                    creature.UpdateToNewSpellSystem();
                    creature.ConnectSpellLinks();
                    creature.UpdateToNewLootSystem();
                    creature.ConnectItemLinks();
                    creature.UpdateToNewAttackSystem();
                    creature.GetPortraitFilepath();
                }
                campaign.UpdateCalendarAndWeather();
            }
        }

        // Databound Properties
        #region Campaigns
        private ObservableCollection<GameCampaign> _Campaigns;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<GameCampaign> Campaigns
        {
            get
            {
                return _Campaigns;
            }
            set
            {
                _Campaigns = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ActiveCampaign
        private GameCampaign _ActiveCampaign;
        [XmlSaveMode("Single")]
        public GameCampaign ActiveCampaign
        {
            get
            {
                return _ActiveCampaign;
            }
            set
            {
                _ActiveCampaign = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region LastSave
        private string _LastSave;
        [XmlSaveMode("None")]
        public string LastSave
        {
            get
            {
                return _LastSave;
            }
            set
            {
                _LastSave = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region AddCampaign
        private RelayCommand _AddCampaign;
        public ICommand AddCampaign
        {
            get
            {
                if (_AddCampaign == null)
                {
                    _AddCampaign = new RelayCommand(DoAddCampaign);
                }
                return _AddCampaign;
            }
        }
        private void DoAddCampaign(object param)
        {
            Campaigns.Add(new());
            ActiveCampaign = Campaigns.Last();
        }
        #endregion
        #region SortCampaigns
        public ICommand SortCampaigns => new RelayCommand(DoSortCampaigns);
        private void DoSortCampaigns(object param)
        {
            Campaigns = new(Campaigns.OrderBy(c => c.Name));
        }
        #endregion
        #region SaveCampaigns
        private RelayCommand _SaveCampaigns;
        public ICommand SaveCampaigns
        {
            get
            {
                if (_SaveCampaigns == null)
                {
                    _SaveCampaigns = new RelayCommand(param => DoSaveCampaigns());
                }
                return _SaveCampaigns;
            }
        }
        public void DoSaveCampaigns(bool notifyUser = true)
        {
            LastSave = DateTime.Now.ToString();
            if (Campaigns.Count() == 0)
            {
                // Prevents zero character save crash
                XDocument blankDoc = new XDocument();
                blankDoc.Add(new XElement("GameCampaignSet"));
                blankDoc.Save(Configuration.CampaignDataFilePath);
                return;
            }
            XDocument itemDocument = new XDocument();
            itemDocument.Add(XmlMethods.ListToXml(Campaigns.ToList()));
            itemDocument.Save(Configuration.CampaignDataFilePath);
            HelperMethods.WriteToLogFile("Campaigns Saved", notifyUser);
            return;
        }
        #endregion
        #region ImportCampaigns
        private RelayCommand _ImportCampaigns;
        public ICommand ImportCampaigns
        {
            get
            {
                if (_ImportCampaigns == null)
                {
                    _ImportCampaigns = new RelayCommand(param => DoImportCampaigns());
                }
                return _ImportCampaigns;
            }
        }
        private void DoImportCampaigns()
        {
            OpenFileDialog openWindow = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            YesNoDialog question = new YesNoDialog("Prior to import, the current campaign list must be saved.\nContinue?");
            question.ShowDialog();
            if (question.Answer == false) { return; }

            DoSaveCampaigns(false);

            if (openWindow.ShowDialog() == true)
            {
                DataImport.ImportData_Campaigns(openWindow.FileName, out string message);
                _ = new NotificationDialog(message).ShowDialog();
            }
        }
        #endregion

        // Public Methods

        // Private Methods

    }
}
