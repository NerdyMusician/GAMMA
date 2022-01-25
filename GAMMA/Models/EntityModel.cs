using GAMMA.Toolbox;
using System;

namespace GAMMA.Models
{
    [Serializable]
    public class EntityModel : BaseModel
    {
        // Constructors
        public EntityModel()
        {
            IsBlinded_Tooltip = "- A blinded creature can’t see and automatically fails any ability check that requires sight.\n- Attack rolls against the creature have advantage, and the creature’s Attack rolls have disadvantage.";
            IsCharmed_Tooltip = "- A charmed creature can’t Attack the charmer or target the charmer with harmful Abilities or magical Effects.\n- The charmer has advantage on any ability check to interact socially with the creature.";
            IsDeafened_Tooltip = "- A deafened creature can’t hear and automatically fails any ability check that requires hearing.";
            IsFrightened_Tooltip = "- A frightened creature has disadvantage on Ability Checks and Attack rolls while the source of its fear is within line of sight.\n- The creature can’t willingly move closer to the source of its fear.";
            IsGrappled_Tooltip = "- A grappled creature’s speed becomes 0, and it can’t benefit from any bonus to its speed.\n- The condition ends if the Grappler is incapacitated.\n- The condition also ends if an Effect removes the grappled creature from the reach of the Grappler or Grappling Effect, such as when a creature is hurled away by the Thunderwave spell.";
            IsIncapacitated_Tooltip = "- An incapacitated creature can’t take Actions or Reactions.";
            IsInvisible_Tooltip = "- An invisible creature is impossible to see without the aid of magic or a Special sense. For the purpose of Hiding, the creature is heavily obscured. The creature’s location can be detected by any noise it makes or any tracks it leaves.\n- Attack rolls against the creature have disadvantage, and the creature’s Attack rolls have advantage.";
            IsParalyzed_Tooltip = "- A paralyzed creature is incapacitated (see the condition) and can’t move or speak.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- Attack rolls against the creature have advantage.\n- Any Attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
            IsPetrified_Tooltip = "- A petrified creature is transformed, along with any nonmagical object it is wearing or carrying, into a solid inanimate substance (usually stone). Its weight increases by a factor of ten, and it ceases aging.\n- The creature is incapacitated(see the condition), can’t move or speak, and is unaware of its surroundings.\n- Attack rolls against the creature have advantage.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- The creature has Resistance to all damage.\n- The creature is immune to poison and disease, although a poison or disease already in its system is suspended, not neutralized.";
            IsPoisoned_Tooltip = "- A poisoned creature has disadvantage on Attack rolls and Ability Checks.";
            IsProne_Tooltip = "- A prone creature’s only Movement option is to crawl, unless it stands up and thereby ends the condition.\n- The creature has disadvantage on Attack rolls.\n- An Attack roll against the creature has advantage if the attacker is within 5 feet of the creature. Otherwise, the Attack roll has disadvantage.";
            IsRestrained_Tooltip = "- A restrained creature’s speed becomes 0, and it can’t benefit from any bonus to its speed.\n- Attack rolls against the creature have advantage, and the creature’s Attack rolls have disadvantage.\n- The creature has disadvantage on Dexterity Saving Throws.";
            IsStunned_Tooltip = "- A stunned creature is incapacitated (see the condition), can’t move, and can speak only falteringly.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- Attack rolls against the creature have advantage.";
            IsUnconscious_Tooltip = "- An unconscious creature is incapacitated (see the condition), can’t move or speak, and is unaware of its surroundings.\n- The creature drops whatever it’s holding and falls prone.\n- The creature automatically fails Strength and Dexterity Saving Throws.\n- Attack rolls against the creature have advantage.\n- Any Attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
            Exhaustion_Tooltip = "Some Special Abilities and environmental Hazards, such as starvation and the long-­term Effects of freezing or scorching temperatures, can lead to a Special condition called exhaustion. Exhaustion is measured in six levels. An Effect can give a creature one or more levels of exhaustion, as specified in the effect’s description." +
                "\n" +
                "\n1. Disadvantage on Ability Checks" +
                "\n2. Speed halved" +
                "\n3. Disadvantage on Attack rolls and Saving Throws" +
                "\n4. Hit point maximum halved" +
                "\n5. Speed reduced to zero" +
                "\n6. Death" +
                "\n" +
                "\n- If an already exhausted creature suffers another Effect that causes exhaustion, its current level of exhaustion increases by the amount specified in the effect’s description." +
                "\n- A creature suffers the Effect of its current level of exhaustion as well as all lower levels. For example, a creature suffering level 2 exhaustion has its speed halved and has disadvantage on Ability Checks." +
                "\n- An Effect that removes exhaustion reduces its level as specified in the effect’s description, with all exhaustion Effects ending if a creature’s exhaustion level is reduced below 1." +
                "\n- Finishing a Long Rest reduces a creature’s exhaustion level by 1, provided that the creature has also ingested some food and drink.";
        }

        // Databound Properties
        #region IsBlinded_Tooltip
        private string _IsBlinded_Tooltip;
        public string IsBlinded_Tooltip
        {
            get => _IsBlinded_Tooltip;
            set => SetAndNotify(ref _IsBlinded_Tooltip, value);
        }
        #endregion
        #region IsCharmed_Tooltip
        private string _IsCharmed_Tooltip;
        public string IsCharmed_Tooltip
        {
            get => _IsCharmed_Tooltip;
            set => SetAndNotify(ref _IsCharmed_Tooltip, value);
        }
        #endregion
        #region IsDeafened_Tooltip
        private string _IsDeafened_Tooltip;
        public string IsDeafened_Tooltip
        {
            get => _IsDeafened_Tooltip;
            set => SetAndNotify(ref _IsDeafened_Tooltip, value);
        }
        #endregion
        #region IsFrightened_Tooltip
        private string _IsFrightened_Tooltip;
        public string IsFrightened_Tooltip
        {
            get => _IsFrightened_Tooltip;
            set => SetAndNotify(ref _IsFrightened_Tooltip, value);
        }
        #endregion
        #region IsGrappled_Tooltip
        private string _IsGrappled_Tooltip;
        public string IsGrappled_Tooltip
        {
            get => _IsGrappled_Tooltip;
            set => SetAndNotify(ref _IsGrappled_Tooltip, value);
        }
        #endregion
        #region IsIncapacitated_Tooltip
        private string _IsIncapacitated_Tooltip;
        public string IsIncapacitated_Tooltip
        {
            get => _IsIncapacitated_Tooltip;
            set => SetAndNotify(ref _IsIncapacitated_Tooltip, value);
        }
        #endregion
        #region IsInvisible_Tooltip
        private string _IsInvisible_Tooltip;
        public string IsInvisible_Tooltip
        {
            get => _IsInvisible_Tooltip;
            set => SetAndNotify(ref _IsInvisible_Tooltip, value);
        }
        #endregion
        #region IsParalyzed_Tooltip
        private string _IsParalyzed_Tooltip;
        public string IsParalyzed_Tooltip
        {
            get => _IsParalyzed_Tooltip;
            set => SetAndNotify(ref _IsParalyzed_Tooltip, value);
        }
        #endregion
        #region IsPetrified_Tooltip
        private string _IsPetrified_Tooltip;
        public string IsPetrified_Tooltip
        {
            get => _IsPetrified_Tooltip;
            set => SetAndNotify(ref _IsPetrified_Tooltip, value);
        }
        #endregion
        #region IsPoisoned_Tooltip
        private string _IsPoisoned_Tooltip;
        public string IsPoisoned_Tooltip
        {
            get => _IsPoisoned_Tooltip;
            set => SetAndNotify(ref _IsPoisoned_Tooltip, value);
        }
        #endregion
        #region IsProne_Tooltip
        private string _IsProne_Tooltip;
        public string IsProne_Tooltip
        {
            get => _IsProne_Tooltip;
            set => SetAndNotify(ref _IsProne_Tooltip, value);
        }
        #endregion
        #region IsRestrained_Tooltip
        private string _IsRestrained_Tooltip;
        public string IsRestrained_Tooltip
        {
            get => _IsRestrained_Tooltip;
            set => SetAndNotify(ref _IsRestrained_Tooltip, value);
        }
        #endregion
        #region IsStunned_Tooltip
        private string _IsStunned_Tooltip;
        public string IsStunned_Tooltip
        {
            get => _IsStunned_Tooltip;
            set => SetAndNotify(ref _IsStunned_Tooltip, value);
        }
        #endregion
        #region IsUnconscious_Tooltip
        private string _IsUnconscious_Tooltip;
        public string IsUnconscious_Tooltip
        {
            get => _IsUnconscious_Tooltip;
            set => SetAndNotify(ref _IsUnconscious_Tooltip, value);
        }
        #endregion
        #region Exhaustion_Tooltip
        private string _Exhaustion_Tooltip;
        public string Exhaustion_Tooltip
        {
            get => _Exhaustion_Tooltip;
            set => SetAndNotify(ref _Exhaustion_Tooltip, value);
        }
        #endregion

        #region L1SpellsAvailable
        private int _L1SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L1SpellsAvailable
        {
            get => _L1SpellsAvailable;
            set => SetAndNotify(ref _L1SpellsAvailable, value);
        }
        #endregion
        #region L1SpellsMax
        private int _L1SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L1SpellsMax
        {
            get => _L1SpellsMax;
            set => SetAndNotify(ref _L1SpellsMax, value);
        }
        #endregion
        #region L2SpellsAvailable
        private int _L2SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L2SpellsAvailable
        {
            get => _L2SpellsAvailable;
            set => SetAndNotify(ref _L2SpellsAvailable, value);
        }
        #endregion
        #region L2SpellsMax
        private int _L2SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L2SpellsMax
        {
            get => _L2SpellsMax;
            set => SetAndNotify(ref _L2SpellsMax, value);
        }
        #endregion
        #region L3SpellsAvailable
        private int _L3SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L3SpellsAvailable
        {
            get => _L3SpellsAvailable;
            set => SetAndNotify(ref _L3SpellsAvailable, value);
        }
        #endregion
        #region L3SpellsMax
        private int _L3SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L3SpellsMax
        {
            get => _L3SpellsMax;
            set => SetAndNotify(ref _L3SpellsMax, value);
        }
        #endregion
        #region L4SpellsAvailable
        private int _L4SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L4SpellsAvailable
        {
            get => _L4SpellsAvailable;
            set => SetAndNotify(ref _L4SpellsAvailable, value);
        }
        #endregion
        #region L4SpellsMax
        private int _L4SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L4SpellsMax
        {
            get => _L4SpellsMax;
            set => SetAndNotify(ref _L4SpellsMax, value);
        }
        #endregion
        #region L5SpellsAvailable
        private int _L5SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L5SpellsAvailable
        {
            get => _L5SpellsAvailable;
            set => SetAndNotify(ref _L5SpellsAvailable, value);
        }
        #endregion
        #region L5SpellsMax
        private int _L5SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L5SpellsMax
        {
            get => _L5SpellsMax;
            set => SetAndNotify(ref _L5SpellsMax, value);
        }
        #endregion
        #region L6SpellsAvailable
        private int _L6SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L6SpellsAvailable
        {
            get => _L6SpellsAvailable;
            set => SetAndNotify(ref _L6SpellsAvailable, value);
        }
        #endregion
        #region L6SpellsMax
        private int _L6SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L6SpellsMax
        {
            get => _L6SpellsMax;
            set => SetAndNotify(ref _L6SpellsMax, value);
        }
        #endregion
        #region L7SpellsAvailable
        private int _L7SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L7SpellsAvailable
        {
            get => _L7SpellsAvailable;
            set => SetAndNotify(ref _L7SpellsAvailable, value);
        }
        #endregion
        #region L7SpellsMax
        private int _L7SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L7SpellsMax
        {
            get => _L7SpellsMax;
            set => SetAndNotify(ref _L7SpellsMax, value);
        }
        #endregion
        #region L8SpellsAvailable
        private int _L8SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L8SpellsAvailable
        {
            get => _L8SpellsAvailable;
            set => SetAndNotify(ref _L8SpellsAvailable, value);
        }
        #endregion
        #region L8SpellsMax
        private int _L8SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L8SpellsMax
        {
            get => _L8SpellsMax;
            set => SetAndNotify(ref _L8SpellsMax, value);
        }
        #endregion
        #region L9SpellsAvailable
        private int _L9SpellsAvailable;
        [XmlSaveMode(XSME.Single)]
        public int L9SpellsAvailable
        {
            get => _L9SpellsAvailable;
            set => SetAndNotify(ref _L9SpellsAvailable, value);
        }
        #endregion
        #region L9SpellsMax
        private int _L9SpellsMax;
        [XmlSaveMode(XSME.Single)]
        public int L9SpellsMax
        {
            get => _L9SpellsMax;
            set => SetAndNotify(ref _L9SpellsMax, value);
        }
        #endregion

    }
}
