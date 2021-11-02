using GAMMA.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GAMMA.CustomControls
{
    public class SearchableDropdown : ComboBox
    {
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            IsDropDownOpen = true;
            base.OnGotFocus(e);
        }

        public IEnumerable OriginalSource
        {
            get
            {
                return (IEnumerable)GetValue(OriginalSourceProperty);
            }
            set
            {
                SetValue(OriginalSourceProperty, value);
            }
        }

        public static readonly DependencyProperty OriginalSourceProperty =
            DependencyProperty.Register("OriginalSource", typeof(IEnumerable), typeof(SearchableDropdown), new PropertyMetadata(default(IEnumerable)));

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (IsDropDownOpen == false) { IsDropDownOpen = true; }
            base.OnPreviewKeyDown(e);
        }

    }

    public class ObjectSearchDropdown : SearchableDropdown
    {
        protected override void OnKeyUp(KeyEventArgs e)
        {
            Type type = null;
            if (Text == "")
            {
                SelectedItem = null;
                SelectedValue = null;
            }
            foreach (object item in OriginalSource)
            {
                type = item.GetType();
                break;
            }
            if (type == typeof(ItemModel))
            {
                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (ItemModel item in OriginalSource)
                {
                    if (item.Name.ToUpper().Contains(Text.ToUpper()) || Text == "")
                    {
                        itemModels.Add(item);
                    }
                }
                ItemsSource = itemModels;
            }
            if (type == typeof(CreatureModel))
            {
                List<CreatureModel> CreatureModels = new List<CreatureModel>();
                foreach (CreatureModel Creature in OriginalSource)
                {
                    if (Creature.Name.ToUpper().Contains(Text.ToUpper()) || Text == "")
                    {
                        CreatureModels.Add(Creature);
                    }
                }
                ItemsSource = CreatureModels;
            }
            if (type == typeof(SpellModel))
            {
                List<SpellModel> spellModels = new List<SpellModel>();
                foreach (SpellModel spell in OriginalSource)
                {
                    if (spell.Name.ToUpper().Contains(Text.ToUpper()) || Text == "")
                    {
                        spellModels.Add(spell);
                    }
                }
                ItemsSource = spellModels;
            }
            if (type == typeof(CreaturePackModel))
            {
                List<CreaturePackModel> packs = new List<CreaturePackModel>();
                foreach (CreaturePackModel pack in OriginalSource)
                {
                    if (pack.Name.ToUpper().Contains(Text.ToUpper()) || Text == "")
                    {
                        packs.Add(pack);
                    }
                }
                ItemsSource = packs;
            }
            if (type == typeof(InventoryModel))
            {
                List<InventoryModel> inventories = new List<InventoryModel>();
                foreach (InventoryModel inventory in OriginalSource)
                {
                    if (inventory.Name.ToUpper().Contains(Text.ToUpper()) || Text == "")
                    {
                        inventories.Add(inventory);
                    }
                }
                ItemsSource = inventories;
            }

            base.OnKeyUp(e);
        }

    }

}
