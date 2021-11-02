using System.Collections.Generic;

namespace GAMMA.Models
{
    public class WebActionModel : BaseModel
    {
        // Constructors
        public WebActionModel()
        {
            ElementMatchIteration = 0;
            InteractionTypes = new List<string> { "Click", "Text Input" };
            TargetElementHandles = new List<string> { "ID", "Class", "Link Text" };
        }

        // Databound Properties
        #region InteractionType
        private string _InteractionType;
        public string InteractionType
        {
            get
            {
                return _InteractionType;
            }
            set
            {
                _InteractionType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region InteractionTypes
        private List<string> _InteractionTypes;
        public List<string> InteractionTypes
        {
            get
            {
                return _InteractionTypes;
            }
            set
            {
                _InteractionTypes = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TargetElementHandle
        private string _TargetElementHandle;
        public string TargetElementHandle
        {
            get
            {
                return _TargetElementHandle;
            }
            set
            {
                _TargetElementHandle = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TargetElementHandles
        private List<string> _TargetElementHandles;
        public List<string> TargetElementHandles
        {
            get
            {
                return _TargetElementHandles;
            }
            set
            {
                _TargetElementHandles = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TargetElementMatchText
        private string _TargetElementMatchText;
        public string TargetElementMatchText
        {
            get
            {
                return _TargetElementMatchText;
            }
            set
            {
                _TargetElementMatchText = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region ElementMatchIteration
        private int _ElementMatchIteration;
        public int ElementMatchIteration
        {
            get
            {
                return _ElementMatchIteration;
            }
            set
            {
                _ElementMatchIteration = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region TextInputValue
        private string _TextInputValue;
        public string TextInputValue
        {
            get
            {
                return _TextInputValue;
            }
            set
            {
                _TextInputValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }
}
