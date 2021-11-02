using GAMMA.Models;
using System.Windows;
using System.Windows.Controls;

namespace GAMMA.Toolbox
{
    public class CombatTrackerTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate DndCreatureDataTemplate { get; set; }
        public DataTemplate DndPlayerDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) { return DefaultDataTemplate; }
            if ((item as CreatureModel).IsPlayer == false) { return DndCreatureDataTemplate; }
            if ((item as CreatureModel).IsPlayer) { return DndPlayerDataTemplate; }
            return DefaultDataTemplate;
        }
    }

    public class ObjectTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }
        public DataTemplate CreatureTemplate { get; set; }
        public DataTemplate SpellTemplate { get; set; }
        public DataTemplate PlayerTemplate { get; set; }
        public DataTemplate NpcTemplate { get; set; }
        public DataTemplate PackTemplate { get; set; }
        public DataTemplate InventoryTemplate { get; set; }
        public DataTemplate ConvertibleValueTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item.GetType() == typeof(ItemModel)) { return ItemTemplate; }
            if (item.GetType() == typeof(CreatureModel)) { return CreatureTemplate; }
            if (item.GetType() == typeof(SpellModel)) { return SpellTemplate; }
            if (item.GetType() == typeof(CharacterModel)) { return PlayerTemplate; }
            if (item.GetType() == typeof(NpcModel)) { return NpcTemplate; }
            if (item.GetType() == typeof(CreaturePackModel)) { return PackTemplate; }
            if (item.GetType() == typeof(ConvertibleValue)) { return ConvertibleValueTemplate; }
            return DefaultDataTemplate;

        }

    }

}
