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
            get => _InteractionType;
            set => SetAndNotify(ref _InteractionType, value);
        }
        #endregion
        #region InteractionTypes
        private List<string> _InteractionTypes;
        public List<string> InteractionTypes
        {
            get => _InteractionTypes;
            set => SetAndNotify(ref _InteractionTypes, value);
        }
        #endregion
        #region TargetElementHandle
        private string _TargetElementHandle;
        public string TargetElementHandle
        {
            get => _TargetElementHandle;
            set => SetAndNotify(ref _TargetElementHandle, value);
        }
        #endregion
        #region TargetElementHandles
        private List<string> _TargetElementHandles;
        public List<string> TargetElementHandles
        {
            get => _TargetElementHandles;
            set => SetAndNotify(ref _TargetElementHandles, value);
        }
        #endregion
        #region TargetElementMatchText
        private string _TargetElementMatchText;
        public string TargetElementMatchText
        {
            get => _TargetElementMatchText;
            set => SetAndNotify(ref _TargetElementMatchText, value);
        }
        #endregion
        #region ElementMatchIteration
        private int _ElementMatchIteration;
        public int ElementMatchIteration
        {
            get => _ElementMatchIteration;
            set => SetAndNotify(ref _ElementMatchIteration, value);
        }
        #endregion
        #region TextInputValue
        private string _TextInputValue;
        public string TextInputValue
        {
            get => _TextInputValue;
            set => SetAndNotify(ref _TextInputValue, value);
        }
        #endregion

    }
}
