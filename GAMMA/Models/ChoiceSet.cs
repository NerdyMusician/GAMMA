using GAMMA.Toolbox;
using System;
using System.Collections.ObjectModel;

namespace GAMMA.Models
{
    [Serializable]
    public class ChoiceSet : BaseModel
    {
        // Constructors
        public ChoiceSet()
        {
            Choices = new();
            ChoiceRestricted = true;
        }

        // Databound Properties
        #region Source
        private string _Source;
        [XmlSaveMode("Single")]
        public string Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ChoiceRestricted
        private bool _ChoiceRestricted;
        [XmlSaveMode("Single")]
        public bool ChoiceRestricted
        {
            get
            {
                return _ChoiceRestricted;
            }
            set
            {
                _ChoiceRestricted = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ChoicesRemaining
        private int _ChoicesRemaining;
        [XmlSaveMode("Single")]
        public int ChoicesRemaining
        {
            get
            {
                return _ChoicesRemaining;
            }
            set
            {
                _ChoicesRemaining = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MaxChoices
        private int _MaxChoices;
        [XmlSaveMode("Single")]
        public int MaxChoices
        {
            get
            {
                return _MaxChoices;
            }
            set
            {
                _MaxChoices = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region Choices
        private ObservableCollection<BoolOption> _Choices;
        [XmlSaveMode("Enumerable")]
        public ObservableCollection<BoolOption> Choices
        {
            get
            {
                return _Choices;
            }
            set
            {
                _Choices = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }
}
