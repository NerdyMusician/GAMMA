using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class CharacterAttributeSet : BaseModel
    {
        // Constructors
        public CharacterAttributeSet()
        {
            
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
        #region MaxPoints
        private int _MaxPoints;
        [XmlSaveMode(XSME.Single)]
        public int MaxPoints
        {
            get
            {
                return _MaxPoints;
            }
            set
            {
                _MaxPoints = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region PointsRemaining
        private int _PointsRemaining;
        public int PointsRemaining
        {
            get
            {
                return _PointsRemaining;
            }
            set
            {
                _PointsRemaining = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Strength
        private int _Strength;
        [XmlSaveMode(XSME.Single)]
        public int Strength
        {
            get
            {
                return _Strength;
            }
            set
            {
                _Strength = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Dexterity
        private int _Dexterity;
        [XmlSaveMode(XSME.Single)]
        public int Dexterity
        {
            get
            {
                return _Dexterity;
            }
            set
            {
                _Dexterity = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Constitution
        private int _Constitution;
        [XmlSaveMode(XSME.Single)]
        public int Constitution
        {
            get
            {
                return _Constitution;
            }
            set
            {
                _Constitution = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Intelligence
        private int _Intelligence;
        [XmlSaveMode(XSME.Single)]
        public int Intelligence
        {
            get
            {
                return _Intelligence;
            }
            set
            {
                _Intelligence = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Wisdom
        private int _Wisdom;
        [XmlSaveMode(XSME.Single)]
        public int Wisdom
        {
            get
            {
                return _Wisdom;
            }
            set
            {
                _Wisdom = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Charisma
        private int _Charisma;
        [XmlSaveMode(XSME.Single)]
        public int Charisma
        {
            get
            {
                return _Charisma;
            }
            set
            {
                _Charisma = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region ChangeAttribute
        private RelayCommand _ChangeAttribute;
        public ICommand ChangeAttribute
        {
            get
            {
                if (_ChangeAttribute == null)
                {
                    _ChangeAttribute = new RelayCommand(DoChangeAttribute);
                }
                return _ChangeAttribute;
            }
        }
        private void DoChangeAttribute(object param)
        {
            if (param == null)
            {
                HelperMethods.WriteToLogFile("No parameter given for CharacterAttributeSet.DoChangeAttribute.", true);
                return;
            }
            if (param.ToString().Contains(",") == false)
            {
                HelperMethods.WriteToLogFile("Invalid parameter " + param.ToString() + " given for CharacterAttributeSet.DoChangeAttribute.\nFormat Expected: \"Attribute,ChangeType\"", true);
                return;
            }
            string attribute = param.ToString().Split(',')[0];
            string changeType = param.ToString().Split(',')[1];
            
            int nextStr = Strength;
            int nextDex = Dexterity;
            int nextCon = Constitution;
            int nextInt = Intelligence;
            int nextWis = Wisdom;
            int nextCha = Charisma;
            if (changeType != "Increment" && changeType != "Decrement")
            {
                HelperMethods.WriteToLogFile("Invalid change type " + changeType + " given for CharacterModelDoChangeAttribute.", true);
                return;
            }
            switch (attribute)
            {
                case "Strength":
                    _ = (changeType == "Increment") ? nextStr++ : nextStr--;
                    break;
                case "Dexterity":
                    _ = (changeType == "Increment") ? nextDex++ : nextDex--;
                    break;
                case "Constitution":
                    _ = (changeType == "Increment") ? nextCon++ : nextCon--;
                    break;
                case "Intelligence":
                    _ = (changeType == "Increment") ? nextInt++ : nextInt--;
                    break;
                case "Wisdom":
                    _ = (changeType == "Increment") ? nextWis++ : nextWis--;
                    break;
                case "Charisma":
                    _ = (changeType == "Increment") ? nextCha++ : nextCha--;
                    break;
                default:
                    HelperMethods.WriteToLogFile("Invalid attribute " + attribute + " given for CharacterModelDoChangeAttribute.", true);
                    return;
            }
            if (nextStr < 0 || nextDex < 0 || nextCon < 0 || nextInt < 0 || nextWis < 0 || nextCha < 0) { return; }
            int pointsUsed = 0;
            pointsUsed += nextStr;
            pointsUsed += nextDex;
            pointsUsed += nextCon;
            pointsUsed += nextInt;
            pointsUsed += nextWis;
            pointsUsed += nextCha;
            if (pointsUsed > MaxPoints)
            {
                return;
            }
            else
            {
                Strength = nextStr;
                Dexterity = nextDex;
                Constitution = nextCon;
                Intelligence = nextInt;
                Wisdom = nextWis;
                Charisma = nextCha;
                PointsRemaining = MaxPoints - pointsUsed;
            }
        }
        #endregion

    }
}
