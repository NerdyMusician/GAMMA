using GAMMA.Toolbox;
using System;

namespace GAMMA.Models
{
    [Serializable]
    public class SpellTableRowModel : BaseModel
    {
        // Constructors
        public SpellTableRowModel()
        {

        }
        public SpellTableRowModel(int level, int s1, int s2 = 0, int s3 = 0, int s4 = 0, int s5 = 0, int s6 = 0, int s7 = 0, int s8 = 0, int s9 = 0)
        {
            ClassLevel = level;
            SpellSlots_1st = s1;
            SpellSlots_2nd = s2;
            SpellSlots_3rd = s3;
            SpellSlots_4th = s4;
            SpellSlots_5th = s5;
            SpellSlots_6th = s6;
            SpellSlots_7th = s7;
            SpellSlots_8th = s8;
            SpellSlots_9th = s9;
        }

        // Databound Properties
        #region SpellsKnownMode
        private string _SpellsKnownMode;
        [XmlSaveMode(XSME.Single)]
        public string SpellsKnownMode
        {
            get => _SpellsKnownMode;
            set
            {
                _SpellsKnownMode = value;
                NotifyPropertyChanged();
                UpdateSpellsKnownMode();
            }
        }
        #endregion
        #region ShowSpellsKnownField
        private bool _ShowSpellsKnownField;
        public bool ShowSpellsKnownField
        {
            get => _ShowSpellsKnownField;
            set => SetAndNotify(ref _ShowSpellsKnownField, value);
        }
        #endregion
        #region ShowSpellsCalcIcon
        private bool _ShowSpellsCalcIcon;
        [XmlSaveMode(XSME.Single)]
        public bool ShowSpellsCalcIcon
        {
            get => _ShowSpellsCalcIcon;
            set => SetAndNotify(ref _ShowSpellsCalcIcon, value);
        }
        #endregion

        #region ClassLevel
        private int _ClassLevel;
        [XmlSaveMode(XSME.Single)]
        public int ClassLevel
        {
            get => _ClassLevel;
            set => SetAndNotify(ref _ClassLevel, value);
        }
        #endregion
        #region CantripsKnown
        private int _CantripsKnown;
        [XmlSaveMode(XSME.Single)]
        public int CantripsKnown
        {
            get => _CantripsKnown;
            set => SetAndNotify(ref _CantripsKnown, value);
        }
        #endregion
        #region SpellsKnown
        private int _SpellsKnown;
        [XmlSaveMode(XSME.Single)]
        public int SpellsKnown
        {
            get => _SpellsKnown;
            set => SetAndNotify(ref _SpellsKnown, value);
        }
        #endregion

        #region SpellSlots_1st
        private int _SpellSlots_1st;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_1st
        {
            get => _SpellSlots_1st;
            set => SetAndNotify(ref _SpellSlots_1st, value);
        }
        #endregion
        #region SpellSlots_2nd
        private int _SpellSlots_2nd;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_2nd
        {
            get => _SpellSlots_2nd;
            set => SetAndNotify(ref _SpellSlots_2nd, value);
        }
        #endregion
        #region SpellSlots_3rd
        private int _SpellSlots_3rd;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_3rd
        {
            get => _SpellSlots_3rd;
            set => SetAndNotify(ref _SpellSlots_3rd, value);
        }
        #endregion
        #region SpellSlots_4th
        private int _SpellSlots_4th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_4th
        {
            get => _SpellSlots_4th;
            set => SetAndNotify(ref _SpellSlots_4th, value);
        }
        #endregion
        #region SpellSlots_5th
        private int _SpellSlots_5th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_5th
        {
            get => _SpellSlots_5th;
            set => SetAndNotify(ref _SpellSlots_5th, value);
        }
        #endregion
        #region SpellSlots_6th
        private int _SpellSlots_6th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_6th
        {
            get => _SpellSlots_6th;
            set => SetAndNotify(ref _SpellSlots_6th, value);
        }
        #endregion
        #region SpellSlots_7th
        private int _SpellSlots_7th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_7th
        {
            get => _SpellSlots_7th;
            set => SetAndNotify(ref _SpellSlots_7th, value);
        }
        #endregion
        #region SpellSlots_8th
        private int _SpellSlots_8th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_8th
        {
            get => _SpellSlots_8th;
            set => SetAndNotify(ref _SpellSlots_8th, value);
        }
        #endregion
        #region SpellSlots_9th
        private int _SpellSlots_9th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_9th
        {
            get => _SpellSlots_9th;
            set => SetAndNotify(ref _SpellSlots_9th, value);
        }
        #endregion

        // Private Methods
        private void UpdateSpellsKnownMode()
        {
            ShowSpellsKnownField = (SpellsKnownMode == "Set");
            ShowSpellsCalcIcon = (SpellsKnownMode == "Calculated");
        }

    }

}
