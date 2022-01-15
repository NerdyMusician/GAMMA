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
        [XmlSaveMode(XSME.Single)]
        public string Source
        {
            get => _Source;
            set => SetAndNotify(ref _Source, value);
        }
        #endregion
        #region ChoiceRestricted
        private bool _ChoiceRestricted;
        [XmlSaveMode(XSME.Single)]
        public bool ChoiceRestricted
        {
            get => _ChoiceRestricted;
            set => SetAndNotify(ref _ChoiceRestricted, value);
        }
        #endregion
        #region ChoicesRemaining
        private int _ChoicesRemaining;
        [XmlSaveMode(XSME.Single)]
        public int ChoicesRemaining
        {
            get => _ChoicesRemaining;
            set => SetAndNotify(ref _ChoicesRemaining, value);
        }
        #endregion
        #region MaxChoices
        private int _MaxChoices;
        [XmlSaveMode(XSME.Single)]
        public int MaxChoices
        {
            get => _MaxChoices;
            set => SetAndNotify(ref _MaxChoices, value);
        }
        #endregion
        #region Choices
        private ObservableCollection<BoolOption> _Choices;
        [XmlSaveMode(XSME.Enumerable)]
        public ObservableCollection<BoolOption> Choices
        {
            get => _Choices;
            set => SetAndNotify(ref _Choices, value);
        }
        #endregion

    }
}
