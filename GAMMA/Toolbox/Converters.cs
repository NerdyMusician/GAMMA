using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace GAMMA.Toolbox
{
    public class CollapsedIfTrueOtherwiseVisible : ConverterMarkupExtension<CollapsedIfTrueOtherwiseVisible>
    {
        public CollapsedIfTrueOtherwiseVisible()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool visibility = (bool)value;
            return !visibility ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Visible);
        }

    }
    public class CollapsedIfFalseOtherwiseVisible : ConverterMarkupExtension<CollapsedIfFalseOtherwiseVisible>
    {
        public CollapsedIfFalseOtherwiseVisible()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool visibility = (bool)value;
            return !visibility ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfIngredient : ConverterMarkupExtension<VisibleIfIngredient>
    {
        public VisibleIfIngredient()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value.ToString() == "Ingredient") ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfPotion : ConverterMarkupExtension<VisibleIfPotion>
    {
        public VisibleIfPotion()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value.ToString() == "Potion") ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfItemType : ConverterMarkupExtension<VisibleIfItemType>
    {
        public VisibleIfItemType()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value.ToString() == parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfMatch : ConverterMarkupExtension<VisibleIfMatch>
    {
        public VisibleIfMatch()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value.ToString() == parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfEqual : ConverterMarkupExtension<VisibleIfEqual>
    {
        public VisibleIfEqual()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int intVal) == false) { return Visibility.Collapsed; }
            if (int.TryParse(parameter.ToString(), out int threshold) == false) { return Visibility.Collapsed; }
            return (intVal == threshold) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfEqualOrMore : ConverterMarkupExtension<VisibleIfEqualOrMore>
    {
        public VisibleIfEqualOrMore()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int intVal) == false) { return Visibility.Collapsed; }
            if (int.TryParse(parameter.ToString(), out int threshold) == false) { return Visibility.Collapsed; }
            return (intVal >= threshold) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfEqualOrLess : ConverterMarkupExtension<VisibleIfEqualOrLess>
    {
        public VisibleIfEqualOrLess()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int intVal) == false) { return Visibility.Collapsed; }
            if (int.TryParse(parameter.ToString(), out int threshold) == false) { return Visibility.Collapsed; }
            return (intVal <= threshold) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class VisibleIfEqualOrLessButNotZero : ConverterMarkupExtension<VisibleIfEqualOrLessButNotZero>
    {
        public VisibleIfEqualOrLessButNotZero()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int intVal) == false) { return Visibility.Collapsed; }
            if (int.TryParse(parameter.ToString(), out int threshold) == false) { return Visibility.Collapsed; }
            return (intVal <= threshold && intVal > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class CollapsedIfNull : ConverterMarkupExtension<CollapsedIfNull>
    {
        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value == null) ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }
    }
    public class CollapsedIfNotNull : ConverterMarkupExtension<CollapsedIfNotNull>
    {
        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value == null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Visible);
        }
    }
    public class CollapsedIfZero : ConverterMarkupExtension<CollapsedIfZero>
    {
        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return ((int)value <= 0) ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }
    }
    public class CollapsedIfNullOrEmpty : ConverterMarkupExtension<CollapsedIfNullOrEmpty>
    {
        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value == null || value.ToString() == "") ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }
    }
    public class CollapsedIfCollectionEmpty : ConverterMarkupExtension<CollapsedIfCollectionEmpty>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(ObservableCollection<ItemModel>))
            {
                return (value as ObservableCollection<ItemModel>).Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            if (value.GetType() == typeof(ObservableCollection<RollTableModel>))
            {
                return (value as ObservableCollection<RollTableModel>).Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            if (value.GetType() == typeof(ObservableCollection<CAVariable>))
            {
                return (value as ObservableCollection<CAVariable>).Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            if (value.GetType() == typeof(ObservableCollection<CAPreAction>))
            {
                return (value as ObservableCollection<CAPreAction>).Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            if (value.GetType() == typeof(ObservableCollection<CAPostAction>))
            {
                return (value as ObservableCollection<CAPostAction>).Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
    }
    public class CollapsedIfCollectionHasItems : ConverterMarkupExtension<CollapsedIfCollectionHasItems>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(ObservableCollection<ItemModel>))
            {
                if ((value as ObservableCollection<ItemModel>).Count() > 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            if (value.GetType() == typeof(ObservableCollection<RollTableModel>))
            {
                if ((value as ObservableCollection<RollTableModel>).Count() > 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }
    }
    public class HiddenIfNull : ConverterMarkupExtension<HiddenIfNull>
    {
        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value == null) ? Visibility.Hidden : Visibility.Visible;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Hidden);
        }
    }
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ModSymbol : ConverterMarkupExtension<ModSymbol>
    {
        public ModSymbol()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value >= 0)
            {
                return "+";
            }
            else
            {
                return "";
            }
        }
    }
    public class EnemyAlly : ConverterMarkupExtension<EnemyAlly>
    {
        public EnemyAlly()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "Ally";
            }
            else
            {
                return "Enemy";
            }
        }
    }
    public class ColorBasedOnStatus : ConverterMarkupExtension<ColorBasedOnStatus>
    {
        public ColorBasedOnStatus()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return null; }
            return value.ToString() switch
            {
                "Fine" => System.Windows.Media.Brushes.OliveDrab,
                "Bruised" => System.Windows.Media.Brushes.YellowGreen,
                "Bloodied" => System.Windows.Media.Brushes.DarkOrange,
                _ => System.Windows.Media.Brushes.Red
            };
        }
    }
    public class ValidationFontColor : ConverterMarkupExtension<ValidationFontColor>
    {
        public ValidationFontColor()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return null; }
            if ((bool)value)
            {
                return System.Windows.Media.Brushes.White;
            }
            else
            {
                return System.Windows.Media.Brushes.DimGray;
            }
        }

    }
    public class SpellLevelIfNonZero : ConverterMarkupExtension<SpellLevelIfNonZero>
    {
        public SpellLevelIfNonZero()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0)
            {
                return "Level " + value.ToString() + " ";
            }
            else
            {
                return "";
            }
        }
    }
    public class SpellLevelIfNonZeroAbridged : ConverterMarkupExtension<SpellLevelIfNonZeroAbridged>
    {
        public SpellLevelIfNonZeroAbridged()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0)
            {
                return "[Lv. " + value.ToString() + "]";
            }
            else
            {
                return "[C]";
            }
        }
    }
    public class SpellCantripIfZeroLevel : ConverterMarkupExtension<SpellCantripIfZeroLevel>
    {
        public SpellCantripIfZeroLevel()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return "Cantrip";
            }
            else
            {
                return "";
            }
        }
    }
    public class VisibleIfSpellLevelAndScale : ConverterMarkupExtension<VisibleIfSpellLevelAndScale>
    {
        public VisibleIfSpellLevelAndScale()
        {

        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null) { return Visibility.Collapsed; }
            if ((value as SpellModel) == null) { return Visibility.Collapsed; }
            if (int.TryParse(parameter.ToString(), out int threshold) == false) { return Visibility.Collapsed; }
            SpellModel spell = value as SpellModel;
            return (spell.SpellLevel <= threshold && spell.HasScaling) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Collapsed);
        }

    }
    public class ImageBasedOnIconName : ConverterMarkupExtension<ImageBasedOnIconName>
    {
        public ImageBasedOnIconName()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { value = "Icon_Cube"; }
            if (value.ToString() == "") { value = "Icon_Cube"; }
            object image = Configuration.framework.TryFindResource(value.ToString());
            if (image == null) { value = "Icon_Cube"; }
            return Configuration.framework.TryFindResource(value.ToString()) as Style;
        }
    }
    public class CreaturePortrait : ConverterMarkupExtension<CreaturePortrait>
    {
        public CreaturePortrait()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return ""; }
            CreatureModel creature = value as CreatureModel;
            if (creature == null) { return ""; }
            string directory = Environment.CurrentDirectory + "/Images/Creatures/";
            string imagePng = directory + creature.Name + ".png";
            string imageJpg = directory + creature.Name + ".jpg";
            string imageGif = directory + creature.Name + ".gif";
            if (File.Exists(imagePng)) { return imagePng; }
            if (File.Exists(imageJpg)) { return imageJpg; }
            if (File.Exists(imageGif)) { return imageGif; }
            else
            {
                string iconName = creature.CreatureCategory switch
                {
                    "Aberration" => "/Images/Icons/aberration.png",
                    "Beast" => "/Images/Icons/beast.png",
                    "Celestial" => "/Images/Icons/celestial.png",
                    "Construct" => "/Images/Icons/construct.png",
                    "Dragon" => "/Images/Icons/dragonclaw.png",
                    "Elemental" => "/Images/Icons/spell.png",
                    "Fey" => "/Images/Icons/fey.png",
                    "Fiend" => "/Images/Icons/fiend.png",
                    "Giant" => "/Images/Icons/step.png",
                    "Humanoid" => "/Images/Icons/smile.png",
                    "Monstrosity" => "/Images/Icons/monster.png",
                    "Ooze" => "/Images/Icons/orb.png",
                    "Plant" => "/Images/Icons/tree.png",
                    "Undead" => "/Images/Icons/dm_skull.png",
                    _ => "/Images/Icons/cube.png"
                };
                return iconName;
            }
        }
    }
    public class NpcPortrait : ConverterMarkupExtension<NpcPortrait>
    {
        public NpcPortrait()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return ""; }
            CreatureModel creature = value as CreatureModel;
            NpcModel npc = value as NpcModel;
            if (npc == null && creature == null) { return ""; }
            string name = (creature != null) ? creature.Name : npc.Name;

            string directory = Environment.CurrentDirectory + "/Images/Npcs/";
            string imagePng = directory + name + ".png";
            string imageJpg = directory + name + ".jpg";
            string imageGif = directory + name + ".gif";
            if (File.Exists(imagePng)) { return imagePng; }
            if (File.Exists(imageJpg)) { return imageJpg; }
            if (File.Exists(imageGif)) { return imageGif; }
            else
            {
                string category = (creature != null) ? creature.CreatureCategory : "";
                string iconName = category switch
                {
                    "Aberration" => "/Images/Icons/aberration.png",
                    "Beast" => "/Images/Icons/beast.png",
                    "Celestial" => "/Images/Icons/celestial.png",
                    "Construct" => "/Images/Icons/construct.png",
                    "Dragon" => "/Images/Icons/dragonclaw.png",
                    "Elemental" => "/Images/Icons/spell.png",
                    "Fey" => "/Images/Icons/fey.png",
                    "Fiend" => "/Images/Icons/fiend.png",
                    "Giant" => "/Images/Icons/step.png",
                    "Humanoid" => "/Images/Icons/smile.png",
                    "Monstrosity" => "/Images/Icons/monster.png",
                    "Ooze" => "/Images/Icons/orb.png",
                    "Plant" => "/Images/Icons/tree.png",
                    "Undead" => "/Images/Icons/dm_skull.png",
                    _ => "/Images/Icons/cube.png"
                };
                return iconName;
            }
        }
    }
    public class PlayerPortrait : ConverterMarkupExtension<PlayerPortrait>
    {
        public PlayerPortrait()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return ""; }
            CreatureModel creature = value as CreatureModel;
            CharacterModel character = value as CharacterModel;
            string name = (creature != null) ? creature.Name : character.Name;
            string directory = Environment.CurrentDirectory + "/Images/Players/";
            string imagePng = directory + name + ".png";
            string imageJpg = directory + name + ".jpg";
            string imageGif = directory + name + ".gif";
            if (File.Exists(imagePng)) { return imagePng; }
            if (File.Exists(imageJpg)) { return imageJpg; }
            if (File.Exists(imageGif)) { return imageGif; }
            else
            {
                return "/Images/Icons/smile.png";
            }
        }
    }
    public class ImageBasedOnValidated : ConverterMarkupExtension<ImageBasedOnValidated>
    {
        public ImageBasedOnValidated()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Configuration.framework.FindResource("Icon_Pass") as Style : Configuration.framework.FindResource("Icon_Fail") as Style;
        }
    }
    public class ImageBasedOnItemType : ConverterMarkupExtension<ImageBasedOnItemType>
    {
        public ImageBasedOnItemType()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Adventuring Gear" => "Icon_Bag",
                "Alcohol" => "Icon_Alcohol",
                "Ammunition" => "Icon_Rpg_Arrow",
                "Arcane Focus" => "Icon_Rpg_Staff",
                "Armor" => "Icon_Shield",
                "Art" => "Icon_Art",
                "Artisan Tool" => "Icon_PaintBrush",
                "Book" => "Icon_Book",
                "Clothing" => "Icon_Rpg_Clothing",
                "Firearms Ranged Weapon" => "Icon_Gun",
                "Fish" => "Icon_Rpg_Fish",
                "Food & Drink" => "Icon_Rpg_Food",
                "Gaming Set" => "Icon_Gamepad",
                "Gemstone" => "Icon_Rpg_Gemstone",
                "Heavy Armor" => "Icon_Rpg_Heavy_Armor",
                "Holy Symbol" => "Icon_Rpg_Holy_Symbol",
                "Ingredient" => "Icon_Rpg_Plant",
                "Instrument" => "Icon_Musicnote",
                "Jewelry" => "Icon_Rpg_Ring",
                "Key" => "Icon_Rpg_Key",
                "Light Armor" => "Icon_Rpg_Light_Armor",
                "Magic Item" => "Icon_MagicItem",
                "Magic Weapon" => "Icon_MagicWeapon",
                "Martial Melee Weapon" => "Icon_Rpg_Sword",
                "Martial Ranged Weapon" => "Icon_Rpg_Fancy_Bow",
                "Medium Armor" => "Icon_Rpg_Medium_Armor",
                "Melee Weapon" => "Icon_Sword",
                "Ranged Weapon" => "Icon_Bow",
                "Poison" => "Icon_Poison",
                "Potion" => "Icon_Potion",
                "Resource" => "Icon_Rpg_Resource",
                "Rune" => "Icon_Rune",
                "Scroll" => "Icon_Rpg_Scroll",
                "Shield" => "Icon_Rpg_Shield",
                "Simple Melee Weapon" => "Icon_Rpg_Axe",
                "Simple Ranged Weapon" => "Icon_Rpg_Bow",
                "Tool" => "Icon_Hammer",
                "Treasure" => "Icon_Treasure",
                "Wondrous Item" => "Icon_Rpg_Wand",
                _ => "Icon_Cube"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnWeather : ConverterMarkupExtension<ImageBasedOnWeather>
    {
        public ImageBasedOnWeather()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Clear" => "Icon_Weather_Clear",
                "Partly Cloudy" => "Icon_Weather_PartlyCloudy",
                "Overcast" => "Icon_Weather_Cloudy",
                "Light Rain" => "Icon_Weather_Raining",
                "Moderate Rain" => "Icon_Weather_HeavyRain",
                "Heavy Storm" => "Icon_Weather_Storm",
                _ => "Icon_Cube"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnEffectType : ConverterMarkupExtension<ImageBasedOnEffectType>
    {
        public ImageBasedOnEffectType()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Attack" => "Icon_Sword",
                "Buff" => "Icon_Buff",
                "Debuff" => "Icon_Debuff",
                "Damage" => "Icon_Explosion",
                "Healing" => "Icon_Plus_Red",
                _ => "Icon_Cube"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnAttackType : ConverterMarkupExtension<ImageBasedOnAttackType>
    {
        public ImageBasedOnAttackType()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Melee" => "Icon_Sword",
                "Ranged" => "Icon_Bow",
                "Magic Weapon" => "Icon_MagicWeapon",
                "Magic Effect" => "Icon_Spell",
                _ => "Icon_Cube"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnAbilityType : ConverterMarkupExtension<ImageBasedOnAbilityType>
    {
        public ImageBasedOnAbilityType()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Melee" => "Icon_Sword",
                "Ranged" => "Icon_Bow",
                "Magic Weapon" => "Icon_MagicWeapon",
                "Magic Effect" => "Icon_Spell",
                "Spell" => "Icon_Spell",
                "Other Ability" => "Icon_Hand",
                _ => "Icon_Cube"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnSpellSchool : ConverterMarkupExtension<ImageBasedOnSpellSchool>
    {
        public ImageBasedOnSpellSchool()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Abjuration" => "Icon_Abjuration",
                "Conjuration" => "Icon_Conjuration",
                "Divination" => "Icon_Divination",
                "Enchantment" => "Icon_Enchantment",
                "Evocation" => "Icon_Evocation",
                "Illusion" => "Icon_Illusion",
                "Necromancy" => "Icon_Necromancy",
                "Transmutation" => "Icon_Transmutation",
                _ => "Icon_Orb"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnCreatureType : ConverterMarkupExtension<ImageBasedOnCreatureType>
    {
        public ImageBasedOnCreatureType()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Aberration" => "Icon_Aberration",
                "Beast" => "Icon_Beast",
                "Celestial" => "Icon_Celestial",
                "Construct" => "Icon_Construct",
                "Dragon" => "Icon_Dragonclaw",
                "Elemental" => "Icon_Spell",
                "Fey" => "Icon_Fey",
                "Fiend" => "Icon_Fiend",
                "Giant" => "Icon_Step",
                "Humanoid" => "Icon_Smile",
                "Monstrosity" => "Icon_Monster",
                "Ooze" => "Icon_Orb",
                "Plant" => "Icon_Tree",
                "Undead" => "Icon_Rpg_Skull",
                _ => "Icon_Cube"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnMessageType : ConverterMarkupExtension<ImageBasedOnMessageType>
    {
        public ImageBasedOnMessageType()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Ability Check" => "Icon_Hex_A",
                "Attack" => "Icon_Crossed_Swords",
                "Coin Flip" => "Icon_CopperCoin",
                "DM Roll" => "Icon_Dice",
                "Fall Damage" => "Icon_Fall",
                "Initiative" => "Icon_Initiative",
                "Loot" => "Icon_Pack",
                "Saving Throw" => "Icon_Letter_S",
                "Skill Check" => "Icon_Hand",
                "Spell" => "Icon_Rpg_Staff",
                "Weather Change" => "Icon_Weather_PartlyCloudy",
                _ => "Icon_Rpg_Note"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class ImageBasedOnNoteCategory : ConverterMarkupExtension<ImageBasedOnNoteCategory>
    {
        public ImageBasedOnNoteCategory()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName = value switch
            {
                "Character" => "Icon_Smile",
                "Faction" => "Icon_Hand",
                "Location" => "Icon_Home",
                "District" => "Icon_Hex",
                "Encounter" => "Icon_Crossed_Swords",
                "Item" => "Icon_Pack",
                "Landmark" => "Icon_Mountain",
                "Map" => "Icon_Map",
                "Puzzle" => "Icon_Puzzle",
                "Quest" => "Icon_Book",
                "Vendor" => "Icon_Vendor",
                "Trap" => "Icon_Trap",
                _ => "Icon_Rpg_Note"
            };
            return Configuration.framework.FindResource(iconName) as Style;
        }
    }
    public class HiddenIfFalseOtherwiseVisible : ConverterMarkupExtension<HiddenIfFalseOtherwiseVisible>
    {
        public HiddenIfFalseOtherwiseVisible()
        {
        }

        public override object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool visibility = (bool)value;
            return !visibility ? Visibility.Hidden : Visibility.Visible;
        }

        public override object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility != Visibility.Hidden);
        }

    }
    public class ReverseBoolean : ConverterMarkupExtension<ReverseBoolean>
    {
        public ReverseBoolean()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value);
        }

    }
    public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter, IMultiValueConverter
        where T : class, new()
    {

        private static T _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new T();
            }

            return _converter;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}