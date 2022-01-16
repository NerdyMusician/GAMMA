using GAMMA.Toolbox;
using GAMMA.ViewModels;
using GAMMA.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GAMMA.Models
{
    public class CreaturePackModel : BaseModel
    {
        // Constructors
        public CreaturePackModel()
        {
            Name = "New Creature Pack";
            CreatureList = new ObservableCollection<PackCreatureModel>();
            NpcList = new ObservableCollection<PackCreatureModel>();
            IsActive = true;
        }

        // Databound Properties
        #region Name
        private string _Name;
        [XmlSaveMode(XSME.Single)]
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
        #region CreatureList
        private ObservableCollection<PackCreatureModel> _CreatureList;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<PackCreatureModel> CreatureList
        {
            get
            {
                return _CreatureList;
            }
            set
            {
                _CreatureList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region NpcList
        private ObservableCollection<PackCreatureModel> _NpcList;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<PackCreatureModel> NpcList
        {
            get
            {
                return _NpcList;
            }
            set
            {
                _NpcList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsAlly
        private bool _IsAlly;
        [XmlSaveMode(XSME.Single)]
        public bool IsAlly
        {
            get
            {
                return _IsAlly;
            }
            set
            {
                _IsAlly = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode(XSME.Single)]
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; NotifyPropertyChanged(); }
        }
        #endregion

        // Commands
        #region AddCreatures
        private RelayCommand _AddCreatures;
        public ICommand AddCreatures
        {
            get
            {
                if (_AddCreatures == null)
                {
                    _AddCreatures = new RelayCommand(param => DoAddCreatures());
                }
                return _AddCreatures;
            }
        }
        private void DoAddCreatures()
        {
            MultiObjectSelectionDialog selectionDialog;
            selectionDialog = new MultiObjectSelectionDialog(Configuration.CreatureRepository.Where(creature => creature.IsValidated == true && creature.IsPlayer == false).ToList(), "Creatures");

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (CreatureModel selectedCreature in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedCreatures)
                {
                    bool matchFound = false;
                    foreach (PackCreatureModel packCreature in CreatureList)
                    {
                        if (selectedCreature.Name == packCreature.CreatureName)
                        {
                            packCreature.Quantity += selectedCreature.QuantityToAdd;
                            matchFound = true;
                            break;
                        }
                    }
                    if (matchFound == false)
                    {
                        CreatureList.Add(new PackCreatureModel { CreatureName = selectedCreature.Name, Quantity = selectedCreature.QuantityToAdd });
                    }
                }
            }

        }
        #endregion
        #region AddNpcs
        private RelayCommand _AddNpcs;
        public ICommand AddNpcs
        {
            get
            {
                if (_AddNpcs == null)
                {
                    _AddNpcs = new RelayCommand(param => DoAddNpcs());
                }
                return _AddNpcs;
            }
        }
        private void DoAddNpcs()
        {
            List<NpcModel> npcs = new();
            npcs.AddRange(Configuration.MainModelRef.CampaignView.ActiveCampaign.Npcs.Where(npc => npc.BaseCreatureName != ""));
            MultiObjectSelectionDialog selectionDialog = new(npcs);

            if (selectionDialog.ShowDialog() == true)
            {
                foreach (NpcModel selectedNpc in (selectionDialog.DataContext as MultiObjectSelectionViewModel).SelectedNpcs)
                {
                    bool matchFound = false;
                    foreach (PackCreatureModel npc in NpcList)
                    {
                        if (selectedNpc.Name == npc.CreatureName)
                        {
                            matchFound = true;
                            break;
                        }
                    }
                    if (matchFound == false)
                    {
                        NpcList.Add(new PackCreatureModel { CreatureName = selectedNpc.Name });
                    }
                }
            }
        }
        #endregion
        #region RemovePackFromCampaign
        private RelayCommand _RemovePackFromCampaign;
        public ICommand RemovePackFromCampaign
        {
            get
            {
                if (_RemovePackFromCampaign == null)
                {
                    _RemovePackFromCampaign = new RelayCommand(param => DoRemovePackFromCampaign());
                }
                return _RemovePackFromCampaign;
            }
        }
        private void DoRemovePackFromCampaign()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.Packs.Remove(this);
        }
        #endregion

    }
}
