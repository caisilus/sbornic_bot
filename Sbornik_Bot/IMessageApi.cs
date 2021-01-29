using System.Collections.Generic;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public interface IMessageApi
    {
        IEnumerable<Message> NewMessages();
        void SendMessage(MessagesSendParams sendParams);
    }
}