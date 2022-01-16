using GAMMA.Toolbox;
using System;
using System.Windows.Input;

namespace GAMMA.Models
{
    [Serializable]
    public class GameMessage : BaseModel
    {
        // Constructors
        public GameMessage()
        {
        }
        public GameMessage(string type, string content)
        {
            MessageType = type;
            MessageContent = content;
        }

        // Databound Properties
        #region MessageType
        private string _MessageType;
        [XmlSaveMode(XSME.Single)]
        public string MessageType
        {
            get
            {
                return _MessageType;
            }
            set
            {
                _MessageType = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        #region MessageContent
        private string _MessageContent;
        [XmlSaveMode(XSME.Single)]
        public string MessageContent
        {
            get
            {
                return _MessageContent;
            }
            set
            {
                _MessageContent = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        // Commands
        #region RemoveMessage
        private RelayCommand _RemoveMessage;
        public ICommand RemoveMessage
        {
            get
            {
                if (_RemoveMessage == null)
                {
                    _RemoveMessage = new RelayCommand(DoRemoveMessage);
                }
                return _RemoveMessage;
            }
        }
        private void DoRemoveMessage(object param)
        {
            if (param == null) { return; }
            if (param.ToString() == "Campaign") { Configuration.MainModelRef.CampaignView.ActiveCampaign.Messages.Remove(this); }
            if (param.ToString() == "Character") { Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Messages.Remove(this); }
        }
        #endregion

        // Public Methods

        // Private Methods

    }
}
