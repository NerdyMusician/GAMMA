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
            get
            {
                return _SpellsKnownMode;
            }
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
            get
            {
                return _ShowSpellsKnownField;
            }
            set
            {
                _ShowSpellsKnownField = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ShowSpellsCalcIcon
        private bool _ShowSpellsCalcIcon;
        [XmlSaveMode(XSME.Single)]
        public bool ShowSpellsCalcIcon
        {
            get
            {
                return _ShowSpellsCalcIcon;
            }
            set
            {
                _ShowSpellsCalcIcon = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ClassLevel
        private int _ClassLevel;
        [XmlSaveMode(XSME.Single)]
        public int ClassLevel
        {
            get
            {
                return _ClassLevel;
            }
            set
            {
                _ClassLevel = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region CantripsKnown
        private int _CantripsKnown;
        [XmlSaveMode(XSME.Single)]
        public int CantripsKnown
        {
            get
            {
                return _CantripsKnown;
            }
            set
            {
                _CantripsKnown = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellsKnown
        private int _SpellsKnown;
        [XmlSaveMode(XSME.Single)]
        public int SpellsKnown
        {
            get
            {
                return _SpellsKnown;
            }
            set
            {
                _SpellsKnown = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region SpellSlots_1st
        private int _SpellSlots_1st;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_1st
        {
            get
            {
                return _SpellSlots_1st;
            }
            set
            {
                _SpellSlots_1st = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_2nd
        private int _SpellSlots_2nd;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_2nd
        {
            get
            {
                return _SpellSlots_2nd;
            }
            set
            {
                _SpellSlots_2nd = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_3rd
        private int _SpellSlots_3rd;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_3rd
        {
            get
            {
                return _SpellSlots_3rd;
            }
            set
            {
                _SpellSlots_3rd = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_4th
        private int _SpellSlots_4th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_4th
        {
            get
            {
                return _SpellSlots_4th;
            }
            set
            {
                _SpellSlots_4th = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_5th
        private int _SpellSlots_5th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_5th
        {
            get
            {
                return _SpellSlots_5th;
            }
            set
            {
                _SpellSlots_5th = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_6th
        private int _SpellSlots_6th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_6th
        {
            get
            {
                return _SpellSlots_6th;
            }
            set
            {
                _SpellSlots_6th = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_7th
        private int _SpellSlots_7th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_7th
        {
            get
            {
                return _SpellSlots_7th;
            }
            set
            {
                _SpellSlots_7th = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_8th
        private int _SpellSlots_8th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_8th
        {
            get
            {
                return _SpellSlots_8th;
            }
            set
            {
                _SpellSlots_8th = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region SpellSlots_9th
        private int _SpellSlots_9th;
        [XmlSaveMode(XSME.Single)]
        public int SpellSlots_9th
        {
            get
            {
                return _SpellSlots_9th;
            }
            set
            {
                _SpellSlots_9th = value;
                NotifyPropertyChanged();
            }
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
