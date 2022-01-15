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
            get => _Name;
            set => SetAndNotify(ref _Name, value);
        }
        #endregion
        #region MaxPoints
        private int _MaxPoints;
        [XmlSaveMode(XSME.Single)]
        public int MaxPoints
        {
            get => _MaxPoints;
            set => SetAndNotify(ref _MaxPoints, value);
        }
        #endregion
        #region PointsRemaining
        private int _PointsRemaining;
        public int PointsRemaining
        {
            get => _PointsRemaining;
            set => SetAndNotify(ref _PointsRemaining, value);
        }
        #endregion
        #region Strength
        private int _Strength;
        [XmlSaveMode(XSME.Single)]
        public int Strength
        {
            get => _Strength;
            set => SetAndNotify(ref _Strength, value);
        }
        #endregion
        #region Dexterity
        private int _Dexterity;
        [XmlSaveMode(XSME.Single)]
        public int Dexterity
        {
            get => _Dexterity;
            set => SetAndNotify(ref _Dexterity, value);
        }
        #endregion
        #region Constitution
        private int _Constitution;
        [XmlSaveMode(XSME.Single)]
        public int Constitution
        {
            get => _Constitution;
            set => SetAndNotify(ref _Constitution, value);
        }
        #endregion
        #region Intelligence
        private int _Intelligence;
        [XmlSaveMode(XSME.Single)]
        public int Intelligence
        {
            get => _Intelligence;
            set => SetAndNotify(ref _Intelligence, value);
        }
        #endregion
        #region Wisdom
        private int _Wisdom;
        [XmlSaveMode(XSME.Single)]
        public int Wisdom
        {
            get => _Wisdom;
            set => SetAndNotify(ref _Wisdom, value);
        }
        #endregion
        #region Charisma
        private int _Charisma;
        [XmlSaveMode(XSME.Single)]
        public int Charisma
        {
            get => _Charisma;
            set => SetAndNotify(ref _Charisma, value);
        }
        #endregion

        // Commands
        #region ChangeAttribute
        public ICommand ChangeAttribute => new RelayCommand(DoChangeAttribute);
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
