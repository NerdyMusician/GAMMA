using GAMMA.Models;
using GAMMA.Toolbox;
using GAMMA.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace GAMMA.Windows
{
    public partial class MultiObjectSelectionDialog : Window
    {
        // Constructors
        public MultiObjectSelectionDialog(List<CreatureModel> creatures, string mode, bool displayOocOption = false)
        {
            InitializeComponent();
            WindowTitle.Text = (mode == "Creatures") ? "Creature Selection" : "Player Selection";
            if (displayOocOption) { DIV_OocAddOption.Visibility = Visibility.Visible; }
            DataContext = new MultiObjectSelectionViewModel(creatures, mode);

            SourceItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).FilteredSourceCreatures
            });

            SelectedItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).SelectedCreatures
            });

        }
        public MultiObjectSelectionDialog(List<ItemModel> items, string title = "Item Selection")
        {
            InitializeComponent();
            WindowTitle.Text = title;
            DataContext = new MultiObjectSelectionViewModel(items);

            SourceItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).FilteredSourceItems
            });

            SelectedItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).SelectedItems
            });

        }
        public MultiObjectSelectionDialog(List<SpellModel> spells)
        {
            InitializeComponent();
            WindowTitle.Text = "Spell Selection";
            DataContext = new MultiObjectSelectionViewModel(spells);

            SourceItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).FilteredSourceSpells
            });

            SelectedItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).SelectedSpells
            });

        }
        public MultiObjectSelectionDialog(List<NpcModel> npcs)
        {
            InitializeComponent();
            WindowTitle.Text = "NPC Selection";
            DataContext = new MultiObjectSelectionViewModel(npcs);

            SourceItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).FilteredSourceNpcs
            });

            SelectedItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).SelectedNpcs
            });

        }
        public MultiObjectSelectionDialog(List<ConvertibleValue> cvs, string mode)
        {
            InitializeComponent();
            WindowTitle.Text = mode;
            DIV_OocAddOption.Visibility = Visibility.Visible;
            DataContext = new MultiObjectSelectionViewModel(cvs, mode);

            SourceItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).FilteredSourceCVs
            });

            SelectedItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).SelectedCVs
            });

        }
        public MultiObjectSelectionDialog(List<GameNote> records, string title)
        {
            InitializeComponent();
            WindowTitle.Text = $"{title} Selection";
            DataContext = new MultiObjectSelectionViewModel(records, title);
            SourceItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).FilteredSourceNotes
            });
            SelectedItems.SetBinding(ItemsControl.ItemsSourceProperty, new Binding
            {
                Source = (DataContext as MultiObjectSelectionViewModel).SelectedNotes
            });
        }

        // Private Methods
        private void AddItem_Clicked(object sender, RoutedEventArgs e)
        {
            Type objectType = (sender as Button).DataContext.GetType();
            if (objectType == typeof(CreatureModel))
            {
                CreatureModel creature = (sender as Button).DataContext as CreatureModel;
                foreach (CreatureModel selectedCreature in (DataContext as MultiObjectSelectionViewModel).SelectedCreatures)
                {
                    if (creature.Name == selectedCreature.Name && selectedCreature.IsHorde == false)
                    {
                        if ((DataContext as MultiObjectSelectionViewModel).Mode == "Players") { return; }
                        selectedCreature.QuantityToAdd++;
                        return;
                    }
                }
                CreatureModel newCreature = HelperMethods.DeepClone(creature);
                newCreature.QuantityToAdd = 1;
                (DataContext as MultiObjectSelectionViewModel).SelectedCreatures.Add(newCreature);
            }
            if (objectType == typeof(ItemModel))
            {
                ItemModel item = (sender as Button).DataContext as ItemModel;
                foreach (ItemModel selectedItem in (DataContext as MultiObjectSelectionViewModel).SelectedItems)
                {
                    if (item.Name == selectedItem.Name)
                    {
                        selectedItem.Quantity++;
                        return;
                    }
                }
                (DataContext as MultiObjectSelectionViewModel).SelectedItems.Add(item);
                item.Quantity = 1;
            }
            if (objectType == typeof(SpellModel))
            {
                SpellModel spell = (sender as Button).DataContext as SpellModel;
                foreach (SpellModel selectedSpell in (DataContext as MultiObjectSelectionViewModel).SelectedSpells)
                {
                    if (spell.Name == selectedSpell.Name)
                    {
                        return;
                    }
                }
                (DataContext as MultiObjectSelectionViewModel).SelectedSpells.Add(spell);
            }
            if (objectType == typeof(NpcModel))
            {
                NpcModel npc = (sender as Button).DataContext as NpcModel;
                foreach (NpcModel selectedNpc in (DataContext as MultiObjectSelectionViewModel).SelectedNpcs)
                {
                    if (npc.Name == selectedNpc.Name)
                    {
                        return;
                    }
                }
                (DataContext as MultiObjectSelectionViewModel).SelectedNpcs.Add(npc);
            }
            if (objectType == typeof(ConvertibleValue))
            {
                ConvertibleValue cv = (sender as Button).DataContext as ConvertibleValue;
                foreach (ConvertibleValue selectedCv in (DataContext as MultiObjectSelectionViewModel).SelectedCVs)
                {
                    if (cv.Value == selectedCv.Value)
                    {
                        return;
                    }
                }
                (DataContext as MultiObjectSelectionViewModel).SelectedCVs.Add(cv);
            }
        }
        private void RemoveItem_Clicked(object sender, RoutedEventArgs e)
        {
            Type objectType = (sender as Button).DataContext.GetType();
            if (objectType == typeof(CreatureModel))
            {
                CreatureModel creature = (sender as Button).DataContext as CreatureModel;
                creature.QuantityToAdd--;
                if (creature.QuantityToAdd <= 0)
                {
                    (DataContext as MultiObjectSelectionViewModel).SelectedCreatures.Remove(creature);
                }
            }
            if (objectType == typeof(ItemModel))
            {
                ItemModel item = (sender as Button).DataContext as ItemModel;
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    (DataContext as MultiObjectSelectionViewModel).SelectedItems.Remove(item);
                }
            }
            if (objectType == typeof(SpellModel))
            {
                SpellModel spell = (sender as Button).DataContext as SpellModel;
                (DataContext as MultiObjectSelectionViewModel).SelectedSpells.Remove(spell);
            }
            if (objectType == typeof(NpcModel))
            {
                NpcModel npc = (sender as Button).DataContext as NpcModel;
                (DataContext as MultiObjectSelectionViewModel).SelectedNpcs.Remove(npc);
            }
            if (objectType == typeof(ConvertibleValue))
            {
                ConvertibleValue cv = (sender as Button).DataContext as ConvertibleValue;
                (DataContext as MultiObjectSelectionViewModel).SelectedCVs.Remove(cv);
            }
        }
        private void EraserButton_Clicked(object sender, RoutedEventArgs e)
        {
            (DataContext as MultiObjectSelectionViewModel).SourceTextSearch = "";
        }
        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

    }
}
