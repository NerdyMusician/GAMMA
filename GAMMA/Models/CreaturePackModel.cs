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
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region CreatureList
        private ObservableCollection<PackCreatureModel> _CreatureList;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<PackCreatureModel> CreatureList
        {
            get => _CreatureList;
            set => SetAndNotify(ref _CreatureList, value);
        }
        #endregion
        #region NpcList
        private ObservableCollection<PackCreatureModel> _NpcList;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<PackCreatureModel> NpcList
        {
            get => _NpcList;
            set => SetAndNotify(ref _NpcList, value);
        }
        #endregion
        #region IsAlly
        private bool _IsAlly;
        [XmlSaveMode(XSME.Single)]
        public bool IsAlly
        {
            get => _IsAlly;
            set => SetAndNotify(ref _IsAlly, value);
        }
        #endregion
        #region IsActive
        private bool _IsActive;
        [XmlSaveMode(XSME.Single)]
        public bool IsActive
        {
            get => _IsActive;
            set => SetAndNotify(ref _IsActive, value);
        }
        #endregion

        // Commands
        #region AddCreatures
        public ICommand AddCreatures => new RelayCommand(param => DoAddCreatures());
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
                            if (selectedCreature.IsHorde == packCreature.IsHorde)
                            {
                                if (selectedCreature.MaxHordeSize == packCreature.MaxHordeSize)
                                {
                                    packCreature.Quantity += selectedCreature.QuantityToAdd;
                                    matchFound = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (matchFound == false)
                    {
                        CreatureList.Add(new() { 
                            CreatureName = selectedCreature.Name, 
                            Quantity = selectedCreature.QuantityToAdd, 
                            IsHorde = selectedCreature.IsHorde, 
                            MaxHordeSize = selectedCreature.MaxHordeSize });
                    }

                    // cleanup
                    selectedCreature.IsHorde = false;
                    selectedCreature.MaxHordeSize = 0;

                }
            }

        }
        #endregion
        #region AddNpcs
        public ICommand AddNpcs => new RelayCommand(param => DoAddNpcs());
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
        public ICommand RemovePackFromCampaign => new RelayCommand(param => DoRemovePackFromCampaign());
        private void DoRemovePackFromCampaign()
        {
            Configuration.MainModelRef.CampaignView.ActiveCampaign.Packs.Remove(this);
        }
        #endregion
        #region SortPackLists
        public ICommand SortPackLists => new RelayCommand(DoSortPackLists);
        private void DoSortPackLists(object param)
        {
            CreatureList = new(CreatureList.OrderBy(c => c.CreatureName));
            NpcList = new(NpcList.OrderBy(n => n.CreatureName));
        }
        #endregion

    }
}
