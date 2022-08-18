using GAMMA.Models;
using GAMMA.Models.GameplayComponents;
using GAMMA.Models.WebAutomation;
using GAMMA.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace GAMMA.Toolbox
{
    public static class XmlMethods
    {
        // Public Methods
        public static XElement ListToXml(IEnumerable itemList, string enumName = "")
        {
            string elementName = string.Empty;
            List<XElement> items = new();
            foreach (object item in itemList)
            {
                elementName = item.GetType().ToString().Split('.').Last();
                items.Add(new XElement(elementName));
                foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                {
                    foreach (CustomAttributeData attr in propertyInfo.CustomAttributes)
                    {
                        if (attr.AttributeType.Name == "XmlSaveMode")
                        {
                            if (attr.ConstructorArguments[0].Value.ToString() == "0")
                            {
                                if (propertyInfo.GetValue(item, null) == null || string.IsNullOrEmpty(propertyInfo.GetValue(item, null).ToString())) { continue; } // don't write blanks to data
                                if (propertyInfo.PropertyType == typeof(bool)) { if (propertyInfo.GetValue(item, null).ToString() == "False") { continue; } } // don't write falses (bool default)
                                if (propertyInfo.PropertyType == typeof(int)) { if (propertyInfo.GetValue(item, null).ToString() == "0") { continue; } } // don't write zeroes (int default)
                                items.Last().Add(new XAttribute(propertyInfo.Name, (propertyInfo.GetValue(item, null) != null) ? propertyInfo.GetValue(item, null).ToString() : ""));
                            }
                            if (attr.ConstructorArguments[0].Value.ToString() == "1")
                            {
                                items.Last().Add(ListToXml(propertyInfo.GetValue(item, null) as IEnumerable, propertyInfo.Name));
                            }
                        }
                    }
                }
            }

            if (items.Count() == 0) { return null; }
            return new XElement(elementName + "Set", items, new XAttribute("Name", enumName));

        }

        public static void NodeToObject(XmlNode tableNode, out RollTableModel table)
        {
            RollTableModel newTable = SetObjectPropertiesFromNode(tableNode, new RollTableModel()) as RollTableModel;

            foreach (XmlNode childNode in tableNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "TableRows")
                {
                    foreach (XmlNode rowNode in childNode.ChildNodes)
                    {
                        NodeToObject(rowNode, out RollTableRowModel row);
                        newTable.TableRows.Add(row);
                    }
                }
            }

            table = newTable;

        }
        public static void NodeToObject(XmlNode rowNode, out RollTableRowModel row)
        {
            row = SetObjectPropertiesFromNode(rowNode, new RollTableRowModel()) as RollTableRowModel;
        }
        public static void NodeToObject(XmlNode characterNode, out CharacterModel character)
        {
            CharacterModel newCharacter = SetObjectPropertiesFromNode(characterNode, new CharacterModel()) as CharacterModel;

            foreach (XmlNode childNode in characterNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Counters")
                {
                    foreach (XmlNode counterNode in childNode.ChildNodes)
                    {
                        NodeToObject(counterNode, out CounterModel counter);
                        newCharacter.Counters.Add(counter);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Traits")
                {
                    foreach (XmlNode traitNode in childNode.ChildNodes)
                    {
                        NodeToObject(traitNode, out TraitModel trait);
                        newCharacter.Traits.Add(trait);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Notes")
                {
                    foreach (XmlNode noteNode in childNode.ChildNodes)
                    {
                        NodeToObject(noteNode, out NoteModel note);
                        newCharacter.Notes.Add(note);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "CraftingBench")
                {
                    foreach (XmlNode itemNode in childNode.ChildNodes)
                    {
                        NodeToObject(itemNode, out ItemModel item);
                        newCharacter.CraftingBench.Add(item);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Minions")
                {
                    foreach (XmlNode creatureNode in childNode.ChildNodes)
                    {
                        NodeToObject(creatureNode, out CreatureModel creature);
                        newCharacter.Minions.Add(creature);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "CreaturePen")
                {
                    foreach (XmlNode creatureNode in childNode.ChildNodes)
                    {
                        NodeToObject(creatureNode, out CreatureModel creature);
                        newCharacter.CreaturePen.Add(creature);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ToolProficiencies")
                {
                    foreach (XmlNode itemNode in childNode.ChildNodes)
                    {
                        NodeToObject(itemNode, out ItemModel item);
                        newCharacter.ToolProficiencies.Add(item);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "HitDiceSets")
                {
                    foreach (XmlNode hitDiceSetNode in childNode.ChildNodes)
                    {
                        NodeToObject(hitDiceSetNode, out HitDiceSet hitDiceSet);
                        newCharacter.HitDiceSets.Add(hitDiceSet);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "CustomDiceSets")
                {
                    foreach (XmlNode customDiceSetNode in childNode.ChildNodes)
                    {
                        NodeToObject(customDiceSetNode, out CustomDiceModel customDiceSet);
                        newCharacter.CustomDiceSets.Add(customDiceSet);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "PlayerClasses")
                {
                    foreach (XmlNode playerClassNode in childNode.ChildNodes)
                    {
                        NodeToObject(playerClassNode, out PlayerClassLinkModel playerClass);
                        newCharacter.PlayerClasses.Add(playerClass);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Inventories")
                {
                    foreach (XmlNode inventoryNode in childNode.ChildNodes)
                    {
                        NodeToObject(inventoryNode, newCharacter, out InventoryModel inventory);
                        newCharacter.Inventories.Add(inventory);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpellLinks")
                {
                    foreach (XmlNode linkNode in childNode.ChildNodes)
                    {
                        NodeToObject(linkNode, out SpellLink sl);
                        newCharacter.SpellLinks.Add(sl);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "LanguageChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.LanguageChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ToolChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.ToolChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "WeaponChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.WeaponChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ArmorChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.ArmorChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "AttributeFeatChoices")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.AttributeFeatChoices.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "FeatChoices")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out BoolOption obj);
                        newCharacter.FeatChoices.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "CCAttributeSets")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CharacterAttributeSet obj);
                        newCharacter.CCAttributeSets.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ChosenEquipment")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ItemLink obj);
                        newCharacter.ChosenEquipment.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "TraitChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.TraitChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "StatBonusChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.StatBonusChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpellChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.SpellChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SkillChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.SkillChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ExpertiseChoiceSegments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ChoiceSet obj);
                        newCharacter.ExpertiseChoiceSegments.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Messages")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out GameMessage obj);
                        newCharacter.Messages.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Abilities")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CustomAbility obj);
                        newCharacter.Abilities.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ActiveEffectAbilities")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CustomAbility obj);
                        newCharacter.ActiveEffectAbilities.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Alterants")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CharacterAlterant obj);
                        newCharacter.Alterants.Add(obj);
                    }
                }
            }

            character = newCharacter;

        }
        public static void NodeToObject(XmlNode creatureNode, out CreatureModel creature)
        {
            CreatureModel newCreature = SetObjectPropertiesFromNode(creatureNode, new CreatureModel()) as CreatureModel;

            foreach (XmlNode childNode in creatureNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Traits")
                {
                    foreach (XmlNode traitNode in childNode.ChildNodes)
                    {
                        NodeToObject(traitNode, out TraitModel trait);
                        newCreature.Traits.Add(trait);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpellLinks")
                {
                    foreach (XmlNode linkNode in childNode.ChildNodes)
                    {
                        NodeToObject(linkNode, out SpellLink sl);
                        newCreature.SpellLinks.Add(sl);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ItemLinks")
                {
                    foreach (XmlNode linkNode in childNode.ChildNodes)
                    {
                        NodeToObject(linkNode, out ItemLink link);
                        newCreature.ItemLinks.Add(link);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Abilities")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CustomAbility obj);
                        newCreature.Abilities.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Counters")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CounterModel obj);
                        newCreature.Counters.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Environments")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                        newCreature.Environments.Add(obj);
                    }
                }
            }

            creature = newCreature;

        }
        public static void NodeToObject(XmlNode counterNode, out CounterModel counter)
        {
            counter = SetObjectPropertiesFromNode(counterNode, new CounterModel()) as CounterModel;
        }
        public static void NodeToObject(XmlNode traitNode, out TraitModel trait)
        {
            trait = SetObjectPropertiesFromNode(traitNode, new TraitModel()) as TraitModel;
        }
        public static void NodeToObject(XmlNode spellNode, out SpellModel spell)
        {
            SpellModel newSpell = SetObjectPropertiesFromNode(spellNode, new SpellModel()) as SpellModel;

            foreach (XmlNode childNode in spellNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ConsumedMaterials")
                {
                    foreach (XmlNode attackNode in childNode.ChildNodes)
                    {
                        NodeToObject(attackNode, out ItemModel item);
                        newSpell.ConsumedMaterials.Add(item);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpellClasses")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue val);
                        newSpell.SpellClasses.Add(val);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "PrimaryAbilities")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CustomAbility val);
                        newSpell.PrimaryAbilities.Add(val);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SecondaryAbilities")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CustomAbility val);
                        newSpell.SecondaryAbilities.Add(val);
                    }
                }

            }

            spell = newSpell;

        }
        public static void NodeToObject(XmlNode noteNode, out NoteModel note)
        {
            NoteModel newNote = SetObjectPropertiesFromNode(noteNode, new NoteModel()) as NoteModel;

            foreach (XmlNode childNode in noteNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SubNotes")
                {
                    foreach (XmlNode subNoteNode in childNode.ChildNodes)
                    {
                        NodeToObject(subNoteNode, out NoteModel subNote);
                        newNote.SubNotes.Add(subNote);
                    }
                }
            }

            note = newNote;

        }
        public static void NodeToObject(XmlNode noteNode, out GameNote note)
        {
            GameNote newNote = SetObjectPropertiesFromNode(noteNode, new GameNote()) as GameNote;
            foreach (XmlNode childNode in noteNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "AssociatedNotes")
                {
                    foreach (XmlNode assNoteNode in childNode.ChildNodes)
                    {
                        NodeToObject(assNoteNode, out GameNote assNote);
                        newNote.AssociatedNotes.Add(assNote);
                    }
                }
            }
            note = newNote;
        }
        public static void NodeToObject(XmlNode itemNode, out ItemModel item)
        {
            ItemModel newItem = SetObjectPropertiesFromNode(itemNode, new ItemModel()) as ItemModel;

            foreach (XmlNode childNode in itemNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "CraftingComponents")
                {
                    foreach (XmlNode componentNode in childNode.ChildNodes)
                    {
                        NodeToObject(componentNode, out ItemModel component);
                        newItem.CraftingComponents.Add(component);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "AcquiredComponents")
                {
                    foreach (XmlNode componentNode in childNode.ChildNodes)
                    {
                        NodeToObject(componentNode, out ItemModel component);
                        newItem.AcquiredComponents.Add(component);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "EnchantingRunes")
                {
                    foreach (XmlNode runeNode in childNode.ChildNodes)
                    {
                        NodeToObject(runeNode, out ItemModel rune);
                        newItem.EnchantingRunes.Add(rune);
                    }
                }
            }

            item = newItem;

        }
        public static void NodeToObject(XmlNode lootBoxNode, out LootBoxModel lootBox)
        {
            LootBoxModel newLootBox = SetObjectPropertiesFromNode(lootBoxNode, new LootBoxModel()) as LootBoxModel;

            foreach (XmlNode childNode in lootBoxNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Items")
                {
                    foreach (XmlNode itemNode in childNode.ChildNodes)
                    {
                        NodeToObject(itemNode, out ItemModel item);
                        newLootBox.Items.Add(item);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ItemLinks")
                {
                    foreach (XmlNode itemNode in childNode.ChildNodes)
                    {
                        NodeToObject(itemNode, out ItemLink item);
                        newLootBox.ItemLinks.Add(item);
                    }
                }
            }

            lootBox = newLootBox;

        }
        public static void NodeToObject(XmlNode hitDiceSetNode, out HitDiceSet hitDiceSet)
        {
            hitDiceSet = SetObjectPropertiesFromNode(hitDiceSetNode, new HitDiceSet()) as HitDiceSet;
        }
        public static void NodeToObject(XmlNode customDiceSetNode, out CustomDiceModel customDiceSet)
        {
            customDiceSet = SetObjectPropertiesFromNode(customDiceSetNode, new CustomDiceModel()) as CustomDiceModel;
        }
        public static void NodeToObject(XmlNode settingsNode, out SettingsViewModel settingsSet)
        {
            SettingsViewModel newSettingsSet = SetObjectPropertiesFromNode(settingsNode, new SettingsViewModel()) as SettingsViewModel;

            foreach (XmlNode childNode in settingsNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Roll20GameCharacterList")
                {
                    foreach (XmlNode pairNode in childNode.ChildNodes)
                    {
                        NodeToObject(pairNode, out GameCharacterSelection pair);
                        newSettingsSet.Roll20GameCharacterList.Add(pair);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "StartupWebActions")
                {
                    foreach (XmlNode pairNode in childNode.ChildNodes)
                    {
                        NodeToObject(pairNode, out WebActionModel action);
                        newSettingsSet.StartupWebActions.Add(action);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "OutputWebActions")
                {
                    foreach (XmlNode pairNode in childNode.ChildNodes)
                    {
                        NodeToObject(pairNode, out WebActionModel action);
                        newSettingsSet.OutputWebActions.Add(action);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SwitchbackWebActions")
                {
                    foreach (XmlNode pairNode in childNode.ChildNodes)
                    {
                        NodeToObject(pairNode, out WebActionModel action);
                        newSettingsSet.SwitchbackWebActions.Add(action);
                    }
                }
            }

            settingsSet = newSettingsSet;

        }
        public static void NodeToObject(XmlNode webActionNode, out WebActionModel webAction)
        {
            WebActionModel newWebAction = SetObjectPropertiesFromNode(webActionNode, new WebActionModel()) as WebActionModel;
            webAction = newWebAction;

        }
        public static void NodeToObject(XmlNode gamePairNode, out GameCharacterSelection gamePair)
        {
            gamePair = SetObjectPropertiesFromNode(gamePairNode, new GameCharacterSelection()) as GameCharacterSelection;
        }
        public static void NodeToObject(XmlNode npcNode, out NpcModel npc)
        {
            npc = SetObjectPropertiesFromNode(npcNode, new NpcModel()) as NpcModel;
        }
        public static void NodeToObject(XmlNode packNode, out CreaturePackModel pack)
        {
            CreaturePackModel newPack = SetObjectPropertiesFromNode(packNode, new CreaturePackModel()) as CreaturePackModel;

            foreach (XmlNode childNode in packNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "CreatureList")
                {
                    foreach (XmlNode creatureNode in childNode.ChildNodes)
                    {
                        NodeToObject(creatureNode, out PackCreatureModel creature);
                        newPack.CreatureList.Add(creature);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "NpcList")
                {
                    foreach (XmlNode creatureNode in childNode.ChildNodes)
                    {
                        NodeToObject(creatureNode, out PackCreatureModel creature);
                        newPack.NpcList.Add(creature);
                    }
                }
            }

            pack = newPack;

        }
        public static void NodeToObject(XmlNode creatureNode, out PackCreatureModel creature)
        {
            creature = SetObjectPropertiesFromNode(creatureNode, new PackCreatureModel()) as PackCreatureModel;
        }
        public static void NodeToObject(XmlNode playerClassNode, out PlayerClassModel playerClass)
        {
            PlayerClassModel newPlayerClass = SetObjectPropertiesFromNode(playerClassNode, new PlayerClassModel()) as PlayerClassModel;

            foreach (XmlNode childNode in playerClassNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "EquipmentChoices")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue cVal);
                        newPlayerClass.EquipmentChoices.Add(cVal);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpellTableRows")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out SpellTableRowModel obj);
                        newPlayerClass.SpellTableRows.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Traits")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out TraitModel obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpecificWeaponProficiencies")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpecificToolProficiencies")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Features")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureModel feat);
                        newPlayerClass.Features.Add(feat);
                    }
                }
            }

            playerClass = newPlayerClass;

        }
        public static void NodeToObject(XmlNode playerSubclassNode, out PlayerSubclassModel playerSubclass)
        {
            PlayerSubclassModel newPlayerSubclass = SetObjectPropertiesFromNode(playerSubclassNode, new PlayerSubclassModel()) as PlayerSubclassModel;

            foreach (XmlNode childNode in playerSubclassNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Traits")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out TraitModel obj);
                        newPlayerSubclass.Traits.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Features")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureModel feat);
                        newPlayerSubclass.Features.Add(feat);
                    }
                }
            }

            playerSubclass = newPlayerSubclass;

        }
        public static void NodeToObject(XmlNode playerClassNode, out PlayerClassLinkModel playerClass)
        {
            playerClass = SetObjectPropertiesFromNode(playerClassNode, new PlayerClassLinkModel()) as PlayerClassLinkModel;
        }
        public static void NodeToObject(XmlNode playerRaceNode, out PlayerRaceModel playerRace)
        {
            PlayerRaceModel newPlayerRace = SetObjectPropertiesFromNode(playerRaceNode, new PlayerRaceModel()) as PlayerRaceModel;

            foreach (XmlNode childNode in playerRaceNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Traits")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out TraitModel obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpecificLanguageProficiencies")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Features")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureModel feat);
                        newPlayerRace.Features.Add(feat);
                    }
                }

            }

            playerRace = newPlayerRace;

        }
        public static void NodeToObject(XmlNode playerSubraceNode, out PlayerSubraceModel playerSubrace)
        {
            PlayerSubraceModel newPlayerSubrace = SetObjectPropertiesFromNode(playerSubraceNode, new PlayerSubraceModel()) as PlayerSubraceModel;

            foreach (XmlNode childNode in playerSubraceNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Traits")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out TraitModel obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Features")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureModel feat);
                        newPlayerSubrace.Features.Add(feat);
                    }
                }
            }

            playerSubrace = newPlayerSubrace;

        }
        public static void NodeToObject(XmlNode playerBackgroundNode, out PlayerBackgroundModel playerBackground)
        {
            PlayerBackgroundModel newPlayerBackground = SetObjectPropertiesFromNode(playerBackgroundNode, new PlayerBackgroundModel()) as PlayerBackgroundModel;

            foreach (XmlNode childNode in playerBackgroundNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "MandatoryEquipment")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ItemModel obj);
                        newPlayerBackground.MandatoryEquipment.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "EquipmentChoices")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                        newPlayerBackground.EquipmentChoices.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpecificLanguageProficiencies")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "SpecificToolProficiencies")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Features")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureModel feat);
                        newPlayerBackground.Features.Add(feat);
                    }
                }
            }

            playerBackground = newPlayerBackground;

        }
        public static void NodeToObject(XmlNode spellRowNode, out SpellTableRowModel spellRow)
        {
            spellRow = SetObjectPropertiesFromNode(spellRowNode, new SpellTableRowModel()) as SpellTableRowModel;
        }
        public static void NodeToObject(XmlNode convertibleNode, out ConvertibleValue convertibleValue)
        {
            convertibleValue = SetObjectPropertiesFromNode(convertibleNode, new ConvertibleValue()) as ConvertibleValue;
        }
        public static void NodeToObject(XmlNode playerFeatNode, out PlayerFeatModel playerFeat)
        {
            PlayerFeatModel newPlayerFeat = SetObjectPropertiesFromNode(playerFeatNode, new PlayerFeatModel()) as PlayerFeatModel;

            foreach (XmlNode childNode in playerFeatNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Features")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureModel feat);
                        newPlayerFeat.Features.Add(feat);
                    }
                }
            }

            playerFeat = newPlayerFeat;

        }
        public static void NodeToObject(XmlNode languageNode, out LanguageModel language)
        {
            language = SetObjectPropertiesFromNode(languageNode, new LanguageModel()) as LanguageModel;
        }
        public static void NodeToObject(XmlNode inventoryNode, CharacterModel character, out InventoryModel inventory)
        {
            InventoryModel newInventory = SetObjectPropertiesFromNode(inventoryNode, new InventoryModel()) as InventoryModel;
            //InventoryModel newInventory = SetObjectPropertiesFromNode(inventoryNode, new InventoryModel(character)) as InventoryModel;

            foreach (XmlNode childNode in inventoryNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "AllItems")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ItemModel item);
                        newInventory.AllItems.Add(item);
                    }
                }
            }

            inventory = newInventory;

        }
        public static void NodeToObject(XmlNode shopNode, out ShopModel shop)
        {
            ShopModel newShop = SetObjectPropertiesFromNode(shopNode, new ShopModel()) as ShopModel;

            foreach (XmlNode childNode in shopNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "ItemTypes")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out BoolOption item);
                        newShop.ItemTypes.Add(item);
                    }
                }
            }

            shop = newShop;

        }
        public static void NodeToObject(XmlNode spellLinkNode, out SpellLink spellLink)
        {
            SpellLink newSpellLink = SetObjectPropertiesFromNode(spellLinkNode, new SpellLink()) as SpellLink;
            spellLink = newSpellLink;
        }
        public static void NodeToObject(XmlNode itemLinkNode, out ItemLink itemLink)
        {
            ItemLink newItemLink = SetObjectPropertiesFromNode(itemLinkNode, new ItemLink()) as ItemLink;
            itemLink = newItemLink;
        }
        public static void NodeToObject(XmlNode optionNode, out BoolOption option)
        {
            BoolOption newOption = SetObjectPropertiesFromNode(optionNode, new BoolOption()) as BoolOption;


            option = newOption;

        }
        public static void NodeToObject(XmlNode featureNode, out FeatureModel feature)
        {
            FeatureModel newFeature = SetObjectPropertiesFromNode(featureNode, new FeatureModel()) as FeatureModel;

            foreach (XmlNode childNode in featureNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Choices")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out FeatureData opt);
                        newFeature.Choices.Add(opt);
                    }
                }
            }

            feature = newFeature;

        }
        public static void NodeToObject(XmlNode featureDataNode, out FeatureData data)
        {
            FeatureData newData = SetObjectPropertiesFromNode(featureDataNode, new FeatureData()) as FeatureData;



            data = newData;

        }
        public static void NodeToObject(XmlNode invocationNode, out EldritchInvocation invocation)
        {
            EldritchInvocation newInvocation = SetObjectPropertiesFromNode(invocationNode, new EldritchInvocation()) as EldritchInvocation;

            invocation = newInvocation;

        }
        public static void NodeToObject(XmlNode choiceSetNode, out ChoiceSet choiceSet)
        {
            ChoiceSet newChoiceSet = SetObjectPropertiesFromNode(choiceSetNode, new ChoiceSet()) as ChoiceSet;

            foreach (XmlNode childNode in choiceSetNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Choices")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out BoolOption opt);
                        newChoiceSet.Choices.Add(opt);
                    }
                }
            }

            choiceSet = newChoiceSet;

        }
        public static void NodeToObject(XmlNode characterAttributeSetNode, out CharacterAttributeSet characterAttributeSet)
        {
            CharacterAttributeSet newCharacterAttributeSet = SetObjectPropertiesFromNode(characterAttributeSetNode, new CharacterAttributeSet()) as CharacterAttributeSet;

            characterAttributeSet = newCharacterAttributeSet;

        }
        public static void NodeToObject(XmlNode campaignNode, out GameCampaign campaign)
        {
            GameCampaign newCampaign = SetObjectPropertiesFromNode(campaignNode, new GameCampaign()) as GameCampaign;

            foreach (XmlNode childNode in campaignNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Combatants")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CreatureModel obj);
                        newCampaign.Combatants.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Npcs")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out NpcModel obj);
                        newCampaign.Npcs.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Players")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CreatureModel obj);
                        newCampaign.Players.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Messages")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out GameMessage obj);
                        newCampaign.Messages.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Timestamps")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out Timestamp obj);
                        newCampaign.Timestamps.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Packs")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CreaturePackModel obj);
                        newCampaign.Packs.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Notes")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out NoteModel obj);
                        newCampaign.Notes.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "NewNotes")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out GameNote obj);
                        newCampaign.NewNotes.Add(obj);
                    }
                }
            }

            campaign = newCampaign;

        }
        public static void NodeToObject(XmlNode messageNode, out GameMessage message)
        {
            GameMessage newMessage = SetObjectPropertiesFromNode(messageNode, new GameMessage()) as GameMessage;


            message = newMessage;

        }
        public static void NodeToObject(XmlNode timestampNode, out Timestamp timestamp)
        {
            Timestamp newTimestamp = SetObjectPropertiesFromNode(timestampNode, new Timestamp()) as Timestamp;


            timestamp = newTimestamp;

        }
        public static void NodeToObject(XmlNode weatherNode, out Weather weather)
        {
            Weather newWeather = SetObjectPropertiesFromNode(weatherNode, new Weather()) as Weather;

            foreach (XmlNode childNode in weatherNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "WeatherEntries")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out WeatherRow obj);
                        newWeather.WeatherEntries.Add(obj);
                    }
                }
            }

            weather = newWeather;

        }
        public static void NodeToObject(XmlNode weatherRowNode, out WeatherRow weatherRow)
        {
            WeatherRow newWeatherRow = SetObjectPropertiesFromNode(weatherRowNode, new WeatherRow()) as WeatherRow;

            weatherRow = newWeatherRow;

        }
        public static void NodeToObject(XmlNode calendarNode, out GameCalendar calendar)
        {
            GameCalendar newCalendar = SetObjectPropertiesFromNode(calendarNode, new GameCalendar()) as GameCalendar;

            foreach (XmlNode childNode in calendarNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Days")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                        newCalendar.Days.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Months")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                        newCalendar.Months.Add(obj);
                    }
                }
            }

            calendar = newCalendar;

        }
        public static void NodeToObject(XmlNode abilityNode, out CustomAbility ability)
        {
            CustomAbility newAbility = SetObjectPropertiesFromNode(abilityNode, new CustomAbility()) as CustomAbility;

            foreach (XmlNode childNode in abilityNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Variables")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CAVariable obj);
                        newAbility.Variables.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "PreActions")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CAPreAction obj);
                        newAbility.PreActions.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "PostActions")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CAPostAction obj);
                        newAbility.PostActions.Add(obj);
                    }
                }
            }

            ability = newAbility;

        }
        public static void NodeToObject(XmlNode variableNode, out CAVariable variable)
        {
            CAVariable newVariable = SetObjectPropertiesFromNode(variableNode, new CAVariable()) as CAVariable;

            variable = newVariable;

        }
        public static void NodeToObject(XmlNode actionNode, out CAPreAction action)
        {
            CAPreAction newAction = SetObjectPropertiesFromNode(actionNode, new CAPreAction()) as CAPreAction;

            foreach (XmlNode childNode in actionNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Answers")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out ConvertibleValue obj);
                        newAction.Answers.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Pairs")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out StringPair obj);
                        newAction.Pairs.Add(obj);
                    }
                }
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Conditions")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CACondition obj);
                        newAction.Conditions.Add(obj);
                    }
                }
            }

            action = newAction;

        }
        public static void NodeToObject(XmlNode actionNode, out CAPostAction action)
        {
            CAPostAction newAction = SetObjectPropertiesFromNode(actionNode, new CAPostAction()) as CAPostAction;

            foreach (XmlNode childNode in actionNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "Conditions")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out CACondition obj);
                        newAction.Conditions.Add(obj);
                    }
                }
            }

            action = newAction;

        }
        public static void NodeToObject(XmlNode conditionNode, out CACondition condition)
        {
            CACondition newCondition = SetObjectPropertiesFromNode(conditionNode, new CACondition()) as CACondition;

            condition = newCondition;

        }
        public static void NodeToObject(XmlNode alterantNode, out CharacterAlterant alterant)
        {
            CharacterAlterant newAlterant = SetObjectPropertiesFromNode(alterantNode, new CharacterAlterant()) as CharacterAlterant;

            foreach (XmlNode childNode in alterantNode.ChildNodes)
            {
                if (childNode.Attributes.GetNamedItem("Name").InnerText == "StatChanges")
                {
                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        NodeToObject(node, out LabeledNumber obj);
                        newAlterant.StatChanges.Add(obj);
                    }
                }
            }

            alterant = newAlterant;

        }
        public static void NodeToObject(XmlNode numberNode, out LabeledNumber number)
        {
            LabeledNumber newNumber = SetObjectPropertiesFromNode(numberNode, new LabeledNumber()) as LabeledNumber;

            number = newNumber;

        }
        public static void NodeToObject(XmlNode sourcebookNode, out Sourcebook sourcebook)
        {
            Sourcebook newSourcebook = SetObjectPropertiesFromNode(sourcebookNode, new Sourcebook()) as Sourcebook;

            sourcebook = newSourcebook;

        }
        public static void NodeToObject(XmlNode noteTypeNode, out NoteType noteType)
        {
            noteType = SetObjectPropertiesFromNode(noteTypeNode, new NoteType()) as NoteType;
        }
        public static void NodeToObject(XmlNode pairNode, out StringPair pair)
        {
            pair = SetObjectPropertiesFromNode(pairNode, new StringPair()) as StringPair;
        }

        public static void XmlToList(string filePath, out List<CreatureModel> creatures, out Dictionary<string,string> encounterData, XmlDocument xmlDoc = null)
        {
            List<CreatureModel> newCreatures = new();
            XmlNodeList creatureNodes;
            encounterData = new Dictionary<string, string>();

            if (xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                if (xmlDoc.ChildNodes[1].Name != "CreatureModelSet")
                {
                    HelperMethods.NotifyUser("Invalid XML for Import");
                    creatures = new List<CreatureModel>();
                    return;
                }
                if (xmlDoc.ChildNodes.Count > 1)
                {
                    foreach (XmlAttribute attr in xmlDoc.ChildNodes[1].Attributes)
                    {
                        encounterData.Add(attr.Name, attr.InnerText);
                    }
                }
                creatureNodes = xmlDoc.ChildNodes[1].ChildNodes;
            }
            else
            {
                creatureNodes = xmlDoc.ChildNodes[0].ChildNodes;
            }

            foreach (XmlNode creatureNode in creatureNodes)
            {
                NodeToObject(creatureNode, out CreatureModel creature);
                newCreatures.Add(creature);
            }

            creatures = newCreatures;

        }
        public static void XmlToList(string filePath, out List<ItemModel> items, XmlDocument xmlDoc = null)
        {
            List<ItemModel> newItems = new();

            foreach (XmlNode itemNode in GetXmlNodeListFromXmlDoc(filePath, "ItemModelSet", xmlDoc))
            {
                NodeToObject(itemNode, out ItemModel item);
                newItems.Add(item);
            }

            items = newItems;

        }
        public static void XmlToList(string filePath, out List<SpellModel> spells, XmlDocument xmlDoc = null)
        {
            List<SpellModel> newSpells = new();

            foreach (XmlNode spellNode in GetXmlNodeListFromXmlDoc(filePath, "SpellModelSet", xmlDoc))
            {
                NodeToObject(spellNode, out SpellModel spell);
                newSpells.Add(spell);
            }

            spells = newSpells;

        }
        public static void XmlToList(string filePath, out List<CharacterModel> characters, XmlDocument xmlDoc = null)
        {
            List<CharacterModel> newCharacters = new();

            foreach (XmlNode characterNode in GetXmlNodeListFromXmlDoc(filePath, "CharacterModelSet", xmlDoc))
            {
                NodeToObject(characterNode, out CharacterModel character);
                newCharacters.Add(character);
            }

            characters = newCharacters;

        }
        public static void XmlToList(string filePath, out List<NoteModel> notes, XmlDocument xmlDoc = null)
        {
            List<NoteModel> newNotes = new();

            foreach (XmlNode noteNode in GetXmlNodeListFromXmlDoc(filePath, "NoteModelSet", xmlDoc))
            {
                NodeToObject(noteNode, out NoteModel note);
                newNotes.Add(note);
            }

            notes = newNotes;

        }
        public static void XmlToList(string filePath, out List<LootBoxModel> lootBoxes, XmlDocument xmlDoc = null)
        {
            List<LootBoxModel> newLootBoxes = new();

            foreach (XmlNode lootBoxNode in GetXmlNodeListFromXmlDoc(filePath, "LootBoxModelSet", xmlDoc))
            {
                NodeToObject(lootBoxNode, out LootBoxModel lootBox);
                newLootBoxes.Add(lootBox);
            }

            lootBoxes = newLootBoxes;

        }
        public static void XmlToList(string filePath, out List<RollTableModel> rollTables, XmlDocument xmlDoc = null)
        {
            List<RollTableModel> newRollTables = new();

            foreach (XmlNode tableNode in GetXmlNodeListFromXmlDoc(filePath, "RollTableModelSet", xmlDoc))
            {
                NodeToObject(tableNode, out RollTableModel table);
                newRollTables.Add(table);
            }

            rollTables = newRollTables;

        }
        public static void XmlToList(string filePath, out List<NpcModel> npcs, XmlDocument xmlDoc = null)
        {
            List<NpcModel> newNpcs = new();

            foreach (XmlNode npcNode in GetXmlNodeListFromXmlDoc(filePath, "NpcModelSet", xmlDoc))
            {
                NodeToObject(npcNode, out NpcModel npc);
                newNpcs.Add(npc);
            }

            npcs = newNpcs;

        }
        public static void XmlToList(string filePath, out List<CreaturePackModel> packs, XmlDocument xmlDoc = null)
        {
            List<CreaturePackModel> newPacks = new();

            foreach (XmlNode npcNode in GetXmlNodeListFromXmlDoc(filePath, "CreaturePackModelSet", xmlDoc))
            {
                NodeToObject(npcNode, out CreaturePackModel npc);
                newPacks.Add(npc);
            }

            packs = newPacks;

        }
        public static void XmlToList(string filePath, out List<PlayerClassModel> playerClasses, XmlDocument xmlDoc = null)
        {
            List<PlayerClassModel> newPlayerClasses = new();

            foreach (XmlNode classNode in GetXmlNodeListFromXmlDoc(filePath, "PlayerClassModelSet", xmlDoc))
            {
                NodeToObject(classNode, out PlayerClassModel pc);
                newPlayerClasses.Add(pc);
            }

            playerClasses = newPlayerClasses;

        }
        public static void XmlToList(string filePath, out List<PlayerSubclassModel> playerClasses, XmlDocument xmlDoc = null)
        {
            List<PlayerSubclassModel> newPlayerSubclasses = new();

            foreach (XmlNode classNode in GetXmlNodeListFromXmlDoc(filePath, "PlayerSubclassModelSet", xmlDoc))
            {
                NodeToObject(classNode, out PlayerSubclassModel pc);
                newPlayerSubclasses.Add(pc);
            }

            playerClasses = newPlayerSubclasses;

        }
        public static void XmlToList(string filePath, out List<PlayerRaceModel> playerRaces, XmlDocument xmlDoc = null)
        {
            List<PlayerRaceModel> newPlayerRaces = new();

            foreach (XmlNode raceNode in GetXmlNodeListFromXmlDoc(filePath, "PlayerRaceModelSet", xmlDoc))
            {
                NodeToObject(raceNode, out PlayerRaceModel pr);
                newPlayerRaces.Add(pr);
            }

            playerRaces = newPlayerRaces;

        }
        public static void XmlToList(string filePath, out List<PlayerSubraceModel> playerSubraces, XmlDocument xmlDoc = null)
        {
            List<PlayerSubraceModel> newPlayerSubraces = new();

            foreach (XmlNode raceNode in GetXmlNodeListFromXmlDoc(filePath, "PlayerSubraceModelSet", xmlDoc))
            {
                NodeToObject(raceNode, out PlayerSubraceModel pr);
                newPlayerSubraces.Add(pr);
            }

            playerSubraces = newPlayerSubraces;

        }
        public static void XmlToList(string filePath, out List<PlayerBackgroundModel> playerBackgrounds, XmlDocument xmlDoc = null)
        {
            List<PlayerBackgroundModel> newPlayerBackgrounds = new();

            foreach (XmlNode backgroundNode in GetXmlNodeListFromXmlDoc(filePath, "PlayerBackgroundModelSet", xmlDoc))
            {
                NodeToObject(backgroundNode, out PlayerBackgroundModel pb);
                newPlayerBackgrounds.Add(pb);
            }

            playerBackgrounds = newPlayerBackgrounds;

        }
        public static void XmlToList(string filePath, out List<PlayerFeatModel> playerFeats, XmlDocument xmlDoc = null)
        {
            List<PlayerFeatModel> newPlayerFeats = new();
            
            foreach (XmlNode featNode in GetXmlNodeListFromXmlDoc(filePath, "PlayerFeatModelSet", xmlDoc))
            {
                NodeToObject(featNode, out PlayerFeatModel pf);
                newPlayerFeats.Add(pf);
            }

            playerFeats = newPlayerFeats;

        }
        public static void XmlToList(string filePath, out List<LanguageModel> languages, XmlDocument xmlDoc = null)
        {
            List<LanguageModel> newLanguages = new();

            foreach (XmlNode languageNode in GetXmlNodeListFromXmlDoc(filePath, "LanguageModelSet", xmlDoc))
            {
                NodeToObject(languageNode, out LanguageModel lang);
                newLanguages.Add(lang);
            }

            languages = newLanguages;

        }
        public static void XmlToList(string filePath, out List<ShopModel> shops, XmlDocument xmlDoc = null)
        {
            List<ShopModel> newShops = new();

            foreach (XmlNode shopNode in GetXmlNodeListFromXmlDoc(filePath, "ShopModelSet", xmlDoc))
            {
                NodeToObject(shopNode, out ShopModel shop);
                newShops.Add(shop);
            }

            shops = newShops;

        }
        public static void XmlToList(string filePath, out List<EldritchInvocation> invocations, XmlDocument xmlDoc = null)
        {
            List<EldritchInvocation> newInvocations = new();

            foreach (XmlNode invocationNode in GetXmlNodeListFromXmlDoc(filePath, "EldritchInvocationSet", xmlDoc))
            {
                NodeToObject(invocationNode, out EldritchInvocation invocation);
                newInvocations.Add(invocation);
            }

            invocations = newInvocations;

        }
        public static void XmlToList(string filePath, out List<GameCampaign> campaigns, XmlDocument xmlDoc = null)
        {
            List<GameCampaign> newCampaigns = new();

            foreach (XmlNode campaignNode in GetXmlNodeListFromXmlDoc(filePath, "GameCampaignSet", xmlDoc))
            {
                NodeToObject(campaignNode, out GameCampaign campaign);
                newCampaigns.Add(campaign);
            }

            campaigns = newCampaigns;

        }
        public static void XmlToList(string filePath, out List<Weather> weathers, XmlDocument xmlDoc = null)
        {
            List<Weather> newWeathers = new();

            foreach (XmlNode weatherNode in GetXmlNodeListFromXmlDoc(filePath, "WeatherSet", xmlDoc))
            {
                NodeToObject(weatherNode, out Weather weather);
                newWeathers.Add(weather);
            }

            weathers = newWeathers;

        }
        public static void XmlToList(string filePath, out List<GameCalendar> calendars, XmlDocument xmlDoc = null)
        {
            List<GameCalendar> newCalendars = new();

            foreach (XmlNode calendarNode in GetXmlNodeListFromXmlDoc(filePath, "GameCalendarSet", xmlDoc))
            {
                NodeToObject(calendarNode, out GameCalendar calendar);
                newCalendars.Add(calendar);
            }

            calendars = newCalendars;

        }
        public static void XmlToList(string filePath, out List<Sourcebook> sourcebooks, XmlDocument xmlDoc = null)
        {
            List<Sourcebook> newSourcebooks = new();

            foreach (XmlNode sourcebookNode in GetXmlNodeListFromXmlDoc(filePath, "SourcebookSet", xmlDoc))
            {
                NodeToObject(sourcebookNode, out Sourcebook sourcebook);
                newSourcebooks.Add(sourcebook);
            }

            sourcebooks = newSourcebooks;

        }
        public static void XmlToList(string filePath, out List<NoteType> noteTypes, XmlDocument xmlDoc = null)
        {
            List<NoteType> newNoteTypes = new();

            foreach (XmlNode noteTypeNode in GetXmlNodeListFromXmlDoc(filePath, "NoteTypeSet", xmlDoc))
            {
                NodeToObject(noteTypeNode, out NoteType noteType);
                newNoteTypes.Add(noteType);
            }

            noteTypes = newNoteTypes;

        }

        public static void XmlToObject(string filePath, out SettingsViewModel settingsSet, XmlDocument xmlDoc = null)
        {
            XmlNode settingsNode;

            if (xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                if (xmlDoc.ChildNodes[1].Name != "SettingsViewModelSet")
                {
                    HelperMethods.NotifyUser("Invalid XML for Import");
                    settingsSet = new SettingsViewModel();
                    return;
                }
                settingsNode = xmlDoc.ChildNodes[1].FirstChild;
            }
            else
            {
                settingsNode = xmlDoc.ChildNodes[0].FirstChild;
            }

            NodeToObject(settingsNode, out SettingsViewModel sv);
            settingsSet = sv;

        }

        // Private Methods
        private static object SetObjectPropertiesFromNode(XmlNode node, object newObject)
        {
            foreach (PropertyInfo propertyInfo in newObject.GetType().GetProperties())
            {
                if (node.Attributes[propertyInfo.Name] != null)
                {
                    string value = node.Attributes[propertyInfo.Name].InnerText;
                    Type propType = propertyInfo.PropertyType;
                    if (propType == typeof(int)) { propertyInfo.SetValue(newObject, Convert.ToInt32(value)); }
                    if (propType == typeof(decimal)) { propertyInfo.SetValue(newObject, Convert.ToDecimal(value)); }
                    if (propType == typeof(bool)) { propertyInfo.SetValue(newObject, Convert.ToBoolean(value)); }
                    if (propType == typeof(long)) { propertyInfo.SetValue(newObject, Convert.ToInt64(value)); }
                    if (propType == typeof(string)) { propertyInfo.SetValue(newObject, value); }
                }
            }
            return newObject;
        }
        private static XmlNodeList GetXmlNodeListFromXmlDoc(string filePath, string setName, XmlDocument xmlDoc)
        {
            XmlNodeList xmlNodes;
            if (xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                if (xmlDoc.ChildNodes[1].Name != setName)
                {
                    HelperMethods.WriteToLogFile("Invalid XML for Import: " + filePath, true);
                    return null;
                }
                xmlNodes = xmlDoc.ChildNodes[1].ChildNodes;
            }
            else
            {
                xmlNodes = xmlDoc.ChildNodes[0].ChildNodes;
            }
            return xmlNodes;
        }

    }
}
