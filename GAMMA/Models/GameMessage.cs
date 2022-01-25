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
            get => _MessageType;
            set => SetAndNotify(ref _MessageType, value);
        }
        #endregion
        #region MessageContent
        private string _MessageContent;
        [XmlSaveMode(XSME.Single)]
        public string MessageContent
        {
            get => _MessageContent;
            set => SetAndNotify(ref _MessageContent, value);
        }
        #endregion

        // Commands
        #region RemoveMessage
        public ICommand RemoveMessage => new RelayCommand(DoRemoveMessage);
        private void DoRemoveMessage(object param)
        {
            if (param == null) { return; }
            if (param.ToString() == "Campaign") { Configuration.MainModelRef.CampaignView.ActiveCampaign.Messages.Remove(this); }
            if (param.ToString() == "Character") { Configuration.MainModelRef.CharacterBuilderView.ActiveCharacter.Messages.Remove(this); }
        }
        #endregion

    }
}
